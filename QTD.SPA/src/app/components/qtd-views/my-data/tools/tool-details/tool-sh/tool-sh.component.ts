import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { Link_Tool_Options } from 'src/app/_DtoModels/Tool/Link_Tool_Options';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-tool-sh',
  templateUrl: './tool-sh.component.html',
  styleUrls: ['./tool-sh.component.scss']
})
export class ToolShComponent implements OnInit {
  displayColumns: string[] = ['id','number', 'description', 'linkageCount'];
  subscription = new SubSink();
  toolId: any;

  @Input() toolName:any;
  @Input() isActive:any;

  alreadyLinked: any[];
  srcList: any[] = [];
  selection = new SelectionModel<any>(true, []);
  DataSource: MatTableDataSource<any>;
  unlinkDescription: string;
  toolIds: any;
  title: any;
  taskIdToShow: any;
  taskNumber: any;
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  unlinkIds: any[] = [];

  constructor(
    public flypanelSrvc:FlyInPanelService,
    private vcf : ViewContainerRef,
    private toolService:ToolsService,
    private route:ActivatedRoute,
    private alert:SweetAlertService,
    public dialog: MatDialog,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      this.toolId = res.id;
      this.getSHLinkages();
    })
  }

  sortData(sort: Sort) {
    this.DataSource.sort = this.sort;
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }


  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }


  openFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }
  getSHLinkages() {
    let tempSrc: any[] = [];
    this.alreadyLinked = [];
    this.toolService.getLinkedSHs(this.toolId).then((res)=>{
       res.forEach((data:any)=>{
        tempSrc.push({number:data.number,description:data.title,linkageCount:data.linkCount,id:data.id, active:data.active});
      });
      this.alreadyLinked = tempSrc.map((data:any)=>{
        return data.id;
      });
      this.srcList = tempSrc;
        this.DataSource = new MatTableDataSource(tempSrc);
        this.DataSource.paginator = this.tblPaging;
    }).catch(async (err:any)=>{
      this.alert.errorToast(`Error Fetching Linked ${await this.labelPipe.transform("Safety Hazard")}`);
    });
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = `You are selecting to unlink the following ${await this.labelPipe.transform("Safety Hazard")}\n`;
    this.toolIds = [];

    if (id) {
      
      this.toolIds.push(id);
      this.unlinkIds = this.unlinkIds.filter((myId:any)=>{
        return myId === id;
      });
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + '\n';
    } else {
      this.unlinkIds.forEach((d:any,idx:number) => {
        this.toolIds.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + " - "  +this.srcList.find((x) => x.id == d).description + '\n';
      });

      //this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription += '\n' + 'from ' + await this.labelPipe.transform('Tool') + 's ' + this.toolName;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  getData(e: any) {
    
      var data = JSON.parse(e);
      var options = new Link_Tool_Options();
      options.toolIds = [];
      options.toolIds.push(this.toolId);
      options.linkedIds = this.toolIds;
      this.toolService.unlinkSHs(options).then(async (res:any)=>{
        this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard")}s Unlinked From ` + await this.labelPipe.transform('Tool') + `s`);
        this.selection.clear();
        this.unlinkIds = [];
        this.srcList = [];
        this.getSHLinkages();
      });
  }


  openProcedureLinkedViewFlyPanel(templateRef: any, data: any) {

    this.title = data.description;
    this.taskIdToShow = data.id;
    this.taskNumber = data.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

}
