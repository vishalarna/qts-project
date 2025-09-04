import { AnimationKeyframesSequenceMetadata } from '@angular/animations';
import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { RR_Task_LinkOptions } from 'src/app/_DtoModels/RR_Task_Link/RR_Task_LinkOptions';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskWithCountOptions } from 'src/app/_DtoModels/Task/TaskWithCountOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-rr-task',
  templateUrl: './rr-task.component.html',
  styleUrls: ['./rr-task.component.scss'],
})
export class RRTaskComponent implements OnInit, AfterViewInit,OnDestroy {
  @Output() regulationDeleteCheck  = new EventEmitter<any>();
  @Input() rrTaskTitle:any;
  @Input() issuingAuthorityCheck:any;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  alreadyLinked:any[] = [];
  addTask: boolean = false;
  unlinkDescription = '';
  subscription = new SubSink();
  taskId:any[] = [];

  taskNumber:any;

  taskTitle = "";
  selectedTaskId = "";

  rrId = "";

  srcList: any[] = [];
  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }
  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private rrService: RegulatoryRequirementService,
    private route:ActivatedRoute,
    private alert:SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      this.rrId = res.id;
      this.getTaskLinkages();
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  sortData(sort: Sort) { 
    this.DataSource.sort = this.sort; 
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
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

  getTaskLinkages() {
    let tempSrc: any[] = [];
    this.alreadyLinked = [];
    this.rrService.getLinkedTasks(this.rrId).then((res:TaskWithCountOptions[])=>{
      res.forEach((data:TaskWithCountOptions)=>{
        tempSrc.push({number:data.number,description:data.description,linkageCount:data.linkCount,id:data.id,active:data.active});
      });
      this.alreadyLinked = tempSrc.map((data:any)=>{
        return data.id;
      });
      
      Object.assign(this.srcList,tempSrc);
      this.DataSource = new MatTableDataSource(tempSrc);
      this.DataSource.paginator = this.tblPaging;

      
      if(tempSrc.length > 0){
        this.regulationDeleteCheck.emit(true);
      }
      else{
        this.regulationDeleteCheck.emit(false);
      }
      //this.DataSource.sort = this.tblSort;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching Linked "+ await this.transformTitle('Task') + "s Data " + err?.toString());
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

  getData(e: any) {
    
    if (this.taskId.length > 0) {
      var data = JSON.parse(e);
      var options = new RR_Task_LinkOptions();
      options.regRequirementId = this.rrId;
      options.taskIds = this.taskId;
      options.changeNotes = data['reason'];
      options.effectiveDate = data['effectiveDate'];
      this.rrService.unlinkTasks(this.rrId,options).then(async (res:any)=>{
        this.alert.successToast(await this.transformTitle('Task') + "s Unlinked From " + await this.labelPipe.transform('Regulatory Requirement'));
        this.selection.clear();
        this.unlinkIds = [];
        this.srcList = [];
        this.getTaskLinkages();
      })
    }
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following ' + await this.transformTitle('Task') + '\n';
    this.taskId = [];
    
    if (id) {
      this.taskId.push(id);
      this.unlinkIds = this.unlinkIds.filter((myId:any)=>{
        return myId !== id;
      });
      this.unlinkDescription +=
      this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + '\n';
    } else {
      this.unlinkIds.forEach((d:any,idx:number) => {
        this.taskId.push(d);
        this.unlinkDescription +=
        this.srcList.find((x) => x.id == d).number + " - "  +this.srcList.find((x) => x.id == d).description + '\n';
      });
      
      //this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription += '\n' + 'from Regulation ' + this.rrTaskTitle;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  removeLastWord(str: string) {
    const lastIndexOfSpace = str.lastIndexOf(' ');

    if (lastIndexOfSpace === -1) {
      return str;
    }
    return str.substring(0, lastIndexOfSpace);
  }

  refreshData(){
    this.getTaskLinkages();
  }

  openLinkedToRRFlyPanel(templateRef:any, row:any){
    
    this.selectedTaskId = row.id;
    this.taskTitle = row.description;
    this.taskNumber = row.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }
}
