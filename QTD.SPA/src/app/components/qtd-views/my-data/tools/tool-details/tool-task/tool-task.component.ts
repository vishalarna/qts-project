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
  selector: 'app-tool-task',
  templateUrl: './tool-task.component.html',
  styleUrls: ['./tool-task.component.scss']
})
export class ToolTaskComponent implements OnInit {
  @Input() toolName:any;
  @Input() isActive:any;
  displayColumns: string[] = ['id','number', 'description', 'linkageCount'];
  alreadyLinked: any[];
  subscription = new SubSink();
  toolId:any;
  srcList: any[] = [];
  selection = new SelectionModel<any>(true, []);
  DataSource: MatTableDataSource<any>;
  unlinkDescription: string;
  toolIds: any;
  title: any;
  taskIdToShow: any;
  taskNumber: any;
  paginatorData: any;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
    this.paginatorData = paginator;
  }
  unlinkIds: any[] = [];

  constructor(
    public flypanelSrvc:FlyInPanelService,
    private vcf: ViewContainerRef,
    private toolService:ToolsService,
    private route:ActivatedRoute,
    private alert:SweetAlertService,
    public dialog: MatDialog,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      this.toolId = res.id;
      this.getTaskLinkages();
    })
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
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

  getTaskLinkages() {
    
    let tempSrc: any[] = [];
    this.alreadyLinked = [];
    this.toolService.getLinkedTasks(this.toolId).then((res)=>{

       res.forEach((data:any)=>{
        tempSrc.push({number:data.number,description:data.description,linkageCount:data.linkCount,id:data.id, active:data.active});
      });
      this.alreadyLinked = tempSrc.map((data:any)=>{
        return data.id;
      });
      this.srcList = tempSrc;
        this.DataSource = new MatTableDataSource(tempSrc);
        this.DataSource.paginator = this.tblPaging === undefined ? this.paginatorData : this.tblPaging;;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching Linked "+ await this.transformTitle('Task')+" s Data ");
    });
  }

  sortData(sort: Sort) {
    if (sort.active == 'number'){       
      this.DataSource.data = this.DataSource.data.sort((a, b) => {
        const aValue = a[sort.active].match(/\d+/g).map(Number);
        const bValue = b[sort.active].match(/\d+/g).map(Number);

        for (let i = 0; i < Math.min(aValue.length, bValue.length); i++) {
          if (aValue[i] !== bValue[i]) {
            return aValue[i] - bValue[i];
          }
        }
        return aValue.length - bValue.length;
      });
    }
    this.DataSource.sort = this.sort;
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following '+ await this.transformTitle('Task') + '\n';
    this.toolIds = [];

    if (id) {
      this.toolIds.push(id);
      this.unlinkIds = this.unlinkIds.filter((myId:any)=>{
        return myId !== id;
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
    this.unlinkDescription += '\n' + 'from ' + await this.labelPipe.transform('Tool') + ' ' + this.toolName;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  getData(e: any) {
    
    if (this.toolIds.length > 0) {
      var data = JSON.parse(e);
      var options = new Link_Tool_Options();
      options.toolIds = [];
      options.toolIds.push(this.toolId);
      options.linkedIds = this.toolIds;

      this.toolService.unlinkTasks(options).then(async (res:any)=>{
        this.alert.successToast( await this.transformTitle('Task') + "s Unlinked From " + await this.labelPipe.transform('Tool') + "s");
        this.selection.clear();
        this.unlinkIds = [];
        this.srcList = [];
        this.getTaskLinkages();
      })
    }
  }

  openProcedureLinkedViewFlyPanel(templateRef: any, data: any) {

    this.title = data.description;
    this.taskIdToShow = data.id;
    this.taskNumber = data.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

}
