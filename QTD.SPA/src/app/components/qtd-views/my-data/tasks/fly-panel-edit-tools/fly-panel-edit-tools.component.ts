import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormControl } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { ToolAddOptions } from 'src/app/_DtoModels/Tool/ToolAddOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-edit-tools',
  templateUrl: './fly-panel-edit-tools.component.html',
  styleUrls: ['./fly-panel-edit-tools.component.scss']
})
export class FlyPanelEditToolsComponent implements OnInit {
  toolList : any[] = [];
  tools : Tool[];
  toolControl = new UntypedFormControl();
  spinner = false;
  selection = new SelectionModel<any>(true, []);
  DataSource: MatTableDataSource<any>;
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
  @Input() taskId = "";
  @Input() data :any[] = [];

  constructor(
    private toolService : ToolsService,
    private taskService : TasksService,
    public flyPanelSrvc : FlyInPanelService,
    private vcf : ViewContainerRef,
    private alert : SweetAlertService,
    public dialog : MatDialog,
    private labelPipe: LabelReplacementPipe
  ) { }

  async ngOnInit(): Promise<void> {
    this.readyToolsData();
    this.dialogTitle= "Unlink " + await this.labelPipe.transform('Tool') + "s From " +  + await this.labelPipe.transform('Task');
  }

  async readyToolsData(){
    this.unlinkIds = [];
    this.selection.clear();
    this.tools = await this.taskService.getTools(this.taskId);
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

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async unlinkTools(){
    this.spinner = true;
    var options = new TaskOptions();
    options.toolIds = this.unlinkIds;
    await this.taskService.UnlinkMultipleTools(this.taskId,options).then(async (_)=>{
      this.alert.successToast("Selected " + await this.labelPipe.transform('Tool') + "s Unlinked From " + await this.transformTitle('Task'));
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
