import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { ToolAddOptions } from 'src/app/_DtoModels/Tool/ToolAddOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-eo-edit-tool',
  templateUrl: './flypanel-eo-edit-tool.component.html',
  styleUrls: ['./flypanel-eo-edit-tool.component.scss']
})
export class FlypanelEoEditToolComponent implements OnInit {
  toolList : any[] = [];
  /// Make this undefied for loader
  tools : Tool[] = [];
  toolControl = new UntypedFormControl();
  spinner = false;
  selection = new SelectionModel<any>(true, []);
  /// also make this undefined
  DataSource: MatTableDataSource<any> = new MatTableDataSource();
  unlinkIds: any[] = [];
  displayColumns: string[] = ['id', 'name'];
  alreadyLinked : any[] = [];
  linkTool = false;
  showTool = true;
  dialogTitle:string;
  dialogDesc = "";
  toolIds:any[] = [];

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() eoId = "";
  @Input() data :any[] = [];

  constructor(
    private toolService : ToolsService,
    private eoService: EnablingObjectivesService,
    public flyPanelSrvc : FlyInPanelService,
    private vcf : ViewContainerRef,
    private alert : SweetAlertService,
    public dialog : MatDialog,
    private labelPipe: LabelReplacementPipe,
  ) { }

  async ngOnInit(): Promise<void> {
    this.readyToolsData()
    this.dialogTitle= "Unlink " + await this.labelPipe.transform('Tool') + "s From Task";
  }

  async readyToolsData(){
    this.unlinkIds = [];
    this.selection.clear();
    this.tools = await this.eoService.getTools(this.eoId);
    this.alreadyLinked = [];
    this.tools.forEach((tool:Tool)=>{
      this.alreadyLinked.push(tool.id);
    });

    this.DataSource = new MatTableDataSource(this.tools);
  }

  refreshTools(){
    this.readyToolsData();
    this.refresh.emit();
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  openLinkPanel(templateRef:any){
    const portal = new TemplatePortal(templateRef,this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  async unlinkTools(){
    this.spinner = true;
    
    var options = new ToolAddOptions();
    options.toolIds = this.unlinkIds;
    await this.eoService.UnlinkMultipleTools(this.eoId,options).then(async (_)=>{
      this.alert.successToast("Selected " + await this.labelPipe.transform('Tool') + "(s) Unlinked From EO");
      this.refreshTools();
    }).finally(()=>{
      this.spinner = false;
    })
  }

  unlinkItemsModal(templateRef: any) {
    this.dialogDesc = 'You are selecting to unlink\n';
    this.toolIds = [];

      this.unlinkIds.forEach((d, i) => {
        this.toolIds.push(d);
        this.dialogDesc +=
          `${i + 1} - ` + this.tools.find((x) => x.id == d)?.name + '\n';
      });

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  getDeleteData(data:any){
    this.unlinkTools();
  }

}
