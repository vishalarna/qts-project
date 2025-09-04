import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, MatSortable, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EOWithCountOptions } from 'src/app/_DtoModels/EnablingObjective/EOWithCountOptions';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-enabling-objectives',
  templateUrl: './task-enabling-objectives.component.html',
  styleUrls: ['./task-enabling-objectives.component.scss'],
})
export class TaskEnablingObjectivesComponent
  implements OnInit, OnDestroy, AfterViewInit
{
  @Input() taskTitle:any;
  @Input() isMeta:any;
  @Input() isEMPView = false;
  displayColumns: string[] = ['id', 'number', 'description', 'linkCount'];
  DataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  subscription = new SubSink();
  taskId = '';
  enablingObjectives: EOWithCountOptions[];
  linkedTasks: any[] = [];
  eoIDs: any[] = [];
  title = '';
  eoId = '';
  EONumber:any;
  showLinkLoader: boolean = false;

  unlinkDescription = '';
  srcList: any[] = [];
  @Input() isActive;

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
    private taskService: TasksService,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res) => {
      if(this.router.url.includes('task-suggestions')){
        this.taskId = String(res.id).split('-')[1].replace('§_', '').split('.')[0];;
       }
       else{
         this.taskId = String(res.id).split('-')[0];
        }
      this.refreshData();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
   

  sortData(sort: Sort) { 
   // this.sort.sort(({ id: 'number', start: 'asc'}) as MatSortable);
    this.DataSource.sort = this.sort; 
  }

  async getLinkData() {
    this.linkedTasks = [];
    if (!this.isMeta) {
      this.enablingObjectives = await this.taskService.getLinkedTaskWithMetaEOCount(
        this.taskId
      );
      this.enablingObjectives.forEach((eo) => {
        this.linkedTasks.push(eo.id);
      });
      
      this.enablingObjectives = this.sortEnablingObjectives(this.enablingObjectives);
    }else{
      this.enablingObjectives = await this.taskService.getLinkedEOWithCount(
        this.taskId
      );
      this.enablingObjectives.forEach((eo) => {
        this.linkedTasks.push(eo.id);
      });
      this.enablingObjectives = this.sortEnablingObjectives(this.enablingObjectives);
    }
    this.DataSource = new MatTableDataSource(this.enablingObjectives);
  }

  sortEnablingObjectives(inputArr : any[]){
    return  inputArr.sort((a, b) => {
       const inputA = a.number.split('.').map(Number);
       const inputB = b.number.split('.').map(Number);
     
       for (let i = 0; i < Math.max(inputA.length, inputB.length); i++) {
         const partA = inputA[i] || 0;
         const partB = inputB[i] || 0;
     
         if (partA !== partB) {
           return partA - partB;
         }
       }
     
       return 0;
     });
   }

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

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = "You are selecting to unlink the following " + await this.transformTitle('Enabling Objective') + "s\n";
    this.eoIDs = [];
    if (id) {
      this.eoIDs.push(id);
      this.unlinkDescription +=
      this.enablingObjectives.find((x) => x.id == id)?.number + ' - ' + this.enablingObjectives.find((x) => x.id == id)?.description + '\n';
    } else {
      this.unlinkIds.forEach((d, i) => {
        this.eoIDs.push(d);
        this.unlinkDescription +=
        this.enablingObjectives.find((x) => x.id == d)?.number + ' - ' +
          this.enablingObjectives.find((x) => x.id == d)?.description +
          '\n';
      });
    } 
    this.unlinkDescription += ' \n' + 'from ' + await this.transformTitle('Task') + this.taskTitle;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  getData(e: any) {
    this.showLinkLoader = true;
    
    var options = new TaskOptions();
    options.enablingObjectiveIds = this.eoIDs;
    var data = JSON.parse(e);
    
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    this.taskService.UnlinkEnablingObjective(this.taskId, options).then(async (_) => {
      this.alert.successToast('Selected ' + await this.transformTitle('Enabling Objective') + ' Unlinked from ' +  await this.transformTitle('Task'));
      this.refreshData();
    }).finally(() => {
      this.showLinkLoader = false;
    });
  }

  openLinkedFlypanel(templateRef: any, row:any) {
    this.EONumber = row.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openLinkFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  refreshData() {
    this.enablingObjectives = [];
    this.selection.clear();
    this.unlinkIds = [];
    this.eoIDs = [];
    this.getLinkData();
    this.dataBroadcastService.refreshTaskStats.next(null);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }  
}
