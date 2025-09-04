import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { TrainingProgramFilterOptions } from 'src/app/_DtoModels/TrainingProgram/TrainingProgramFilterOptions';
import { TrainingProgram_HistoryCreateOptions } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram_HistoryCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TrainingProgramsService } from 'src/app/_Services/QTD/training-programs.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
export interface PeriodicElement {
  name: string;
  position: number;
  weight: string;
  symbol: string;
  modifiedate:string;
}

@Component({
  selector: 'app-training-program-overview',
  templateUrl: './training-program-overview.component.html',
  styleUrls: ['./training-program-overview.component.scss']
})
export class TrainingProgramOverviewComponent implements OnInit,AfterViewInit {
  isLoading: boolean = false;
  isLoadingStats: boolean = false;
  catCompleted: any
  catIncompleted: any
  displayedColumns: string[] = ['positionTitle', 'trainingProgramTypeTitle', 'tpVersionNo', 'startDate','active','id'];
  dataSource: PeriodicElement[];
  selection = new SelectionModel<PeriodicElement>(true, []);
  trainingProgramList:any;
  trainingProgramsVM:any;
  dataSourceTrainingPrograms: MatTableDataSource<any>;
  modalHeader = '';
  modalDescription = '';
  isActive: boolean = true;
  activeStatus: string;
  tpId : any;
  deleteDescription:any;
  datePipe = new DatePipe('en-us');
  filterValue: any;
  deleteCheck: boolean = false;
  filterText:string='';
  linkedTrainingProgramIds: string[] = [];

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSourceTrainingPrograms.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSourceTrainingPrograms.paginator = paginator;
  }
  constructor(private _router: Router,
    public vcf:ViewContainerRef,
    private alert:SweetAlertService,
    public flyPanelService:FlyInPanelService,
    private trainingProgramsrvc: TrainingProgramsService,
    public dialog:MatDialog,
    private store: Store<{ toggle: string }>,
    private databroadCastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe
    ) { }

  ngAfterViewInit(): void {
    this.store.dispatch(sideBarOpen());
    let searchText = localStorage.getItem('searchText');
    if(searchText){
      //this.filterText = JSON.parse(searchText);
    }
  }


  async ngOnInit(): Promise<void> {

    this.catCompleted = 10
    this.catIncompleted = 24
    this.getTrainingProgramStats();
    this.getallTrainingPrograms();
    await this.getTrainingProgramIlaLinksAndReviewsAsync();
    this.dataSource  = [
      {position: 12, name: 'John Smith', weight: 'Change ' + await this.labelPipe.transform('Instructor') + ' admin status', symbol: 'Sara Johnson',modifiedate:"25-09-1994"},
      {position: 13, name: 'Jessica Albert', weight:'Change ' + await this.labelPipe.transform('Instructor') + ' Email', symbol: 'Tara Johnson',modifiedate:"28-09-1994"},
    ];

    // var searchText = localStorage.getItem('searchText');
    // this.filterText = JSON.parse(searchText);
  }


  clearSearch:any;
  clearFilter(){
    this.clearSearch = '';
    this.filterText = '';
    this.dataSourceTrainingPrograms.data = this.trainingProgramList;
  }

  CreateTrainingProgram()
  {
    if(this.filterText !== '')
    {
        localStorage.setItem('searchText', JSON.stringify(this.filterText));
    }
    this._router.navigate(['/dnd/trainingprogram/create']);
  }

  async getTrainingProgramIlaLinksAndReviewsAsync() {
    const result = await this.trainingProgramsrvc.getTrainingProgramIlaLinksAndReviewsAsync();
    this.linkedTrainingProgramIds = [];
    result.forEach(tp => {
      if ((tp.trainingProgram_ILA_Links && tp.trainingProgram_ILA_Links.length > 0) ||
          (tp.trainingProgramReviews && tp.trainingProgramReviews.length > 0)) {
        this.linkedTrainingProgramIds.push(tp.id);
      }
    });
  }

  isDeleteDisabled(row: any): boolean {
    return this.linkedTrainingProgramIds.includes(row.id);
  }  

  filterName:any;
  openFlyPanel(templateRef: any, name:any) {
    this.filterName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.forEach(row => this.selection.select(row));
  }

  getallTrainingPrograms()
  {

    var modifiedData = [];
    this.isLoading = true;
    this.trainingProgramsrvc.getAll().then((res) => {
      this.dataSourceTrainingPrograms = new MatTableDataSource();
      this.dataSourceTrainingPrograms.paginator = this.tblPaging;
      res.forEach((x)=>{
        modifiedData.push({...x,positionTitle:x.position.positionTitle,trainingProgramTypeTitle:x.trainingProgramType.trainingProgramTypeTitle})
        x.position.employeePositions.forEach((pos)=>{
          if(pos.trainingProgramVersion === x.tpVersionNo){
            this.deleteCheck = true;
          }
        })
      })
      this.trainingProgramList = modifiedData;
      //this.dataSourceTrainingPrograms.data = this.trainingProgramList;
      const activeTrainingProgram =modifiedData.filter(r=>r.active);
      this.dataSourceTrainingPrograms = new MatTableDataSource(activeTrainingProgram);
    }).finally(() => {
      this.isLoading = false;
      this.filterTrainingPrograms(new Event("t"));
      localStorage.removeItem('searchText');
    });
  }

  openTrainingProgramPage(mode: string, id: any) {
    if(this.filterText !== '')
    {
        localStorage.setItem('searchText', JSON.stringify(this.filterText));
    }
    if(mode === 'edit')
    {
      this._router
      .navigate(['/dnd/trainingprogram/edit/' + id]);
    }
    else
    {
      this._router
      .navigate(['/dnd/trainingprogram/copy/' + id]);
    }

  }
//   async changeStatus(status: boolean, id: any) {
//
//     var options = new TrainingProgram_HistoryCreateOptions();
//     if (status === true)
//     {
//       options.ActionType = 'inactive'
//     }
//     else
//     {
//       options.ActionType = 'active'
//     }

//     await this.trainingProgramsrvc
//       .makeActiveInactiveOrDelete(id,options)
//       .then(
//         (x) => {
//           if (x) {
//             this.getallTrainingPrograms();
//           }
//         },
//         () => this.alert.successToast("Training program status changed successfully")
//       );



//   }
  changeStatus(templateRef: any, row: any) {
    // ${this.name}

    if(row.active === true)
    {
       this.isActive = false;
       this.activeStatus = 'Inactive'
    }
    else
    {
      this.isActive = true;
      this.activeStatus = 'Active'
    }
    this.tpId = row.id;

    this.modalHeader = row.active
      ? 'Make Inactive'
      : 'Active' + ' Training Program';
    this.modalDescription = `You are selecting to make the ${row.trainingProgramType.trainingProgramTypeTitle} ${this.datePipe.transform(row.year,"MM-dd-yyyy")} training program for ${row.position.positionTitle} ${this.activeStatus}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  MakeActive(e: any, active: boolean) {



    // var idarray :any =[];
    // idarray.push(this.id);
    var options = new TrainingProgram_HistoryCreateOptions();

    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.ChangeNotes = data['reason'];
    options.ActionType = this.activeStatus;
    this.trainingProgramsrvc
      .makeActiveInactiveOrDelete(this.tpId, options)
      .then((res: any) => {
        this.getallTrainingPrograms();
        this.getTrainingProgramStats();
        this.alert.successToast("Training program status changed successfully")
      });
  }
  async getTrainingProgramStats() {
    this.isLoadingStats = true;
    await this.trainingProgramsrvc.getStatsCount()
      .then((res:any) => {
        this.trainingProgramsVM = res;

      })
      .finally(() => (this.isLoadingStats = false));
  }

  filterCheck:boolean=false;
  clearFilterName(){
    this.filterName = null;
    this.getallTrainingPrograms();
    this.filterCheck = false;
    this.startDate = null;
    this.endDate = null;
    this.positionTitle = null;
    this.noPositionCheck = false;
  }

  getFilteredTrainingProgram(trainingProgramtitle:any)
  {

  this.filterName = trainingProgramtitle;
  this.isLoading = true;
  var options = new TrainingProgramFilterOptions()
  options.trainingProgramTitle = trainingProgramtitle;
  var modifiedData:any[] = [];
  this.trainingProgramsrvc.getTrainingProgramByFilter('trainingProogramType',options).then((res:any) => {
    res.forEach((x) => {
      modifiedData.push({...x,positionTitle:x.position.positionTitle,trainingProgramTypeTitle:x.trainingProgramType.trainingProgramTypeTitle})
    });
    this.trainingProgramList = modifiedData.filter(r=>r.active);
    this.dataSourceTrainingPrograms = new MatTableDataSource(this.trainingProgramList);
  }).finally(() => (
    this.isLoading = false));
}
startDate:any;
endDate:any;
noPositionCheck:boolean
filterTrainingProgramByYears(eventData: { startYear: string, endYear:string })
{

  this.isLoading = true;
  var options = new TrainingProgramFilterOptions()
  options.startYear = eventData.startYear;
  options.endYear = eventData.endYear;
  var modifiedData:any[] = [];
  this.trainingProgramsrvc.getTrainingProgramByFilter('year',options).then((res:any) => {
    this.flyPanelService.close();
    //this.trainingProgramList = res;
    res.forEach((x) => {
      modifiedData.push({...x,positionTitle:x.position.positionTitle,trainingProgramTypeTitle:x.trainingProgramType.trainingProgramTypeTitle})
    });
    this.trainingProgramList = modifiedData.filter(r=>r.active);;
    this.dataSourceTrainingPrograms = new MatTableDataSource(this.trainingProgramList);
  }).finally(() => {
    this.isLoading = false;
    this.startDate = eventData.startYear;
    this.endDate = eventData.endYear;
  });
}
positionTitle:any;
filterTrainingProgramByPositions(eventData: {positionIds:any[]})
{
  this.isLoading = true;
  var options = new TrainingProgramFilterOptions();
  options.positionIds = eventData.positionIds;
  var modifiedData:any[] = [];
  this.trainingProgramsrvc.getTrainingProgramByFilter('position',options).then((res:any) => {
    this.flyPanelService.close();
    if(res.length === 0){
      this.noPositionCheck = true;
}
      res.forEach((x) => {
        modifiedData.push({...x,positionTitle:x.position.positionTitle,trainingProgramTypeTitle:x.trainingProgramType.trainingProgramTypeTitle})
      });
      this.trainingProgramList = modifiedData.filter(r=>r.active);
      this.dataSourceTrainingPrograms = new MatTableDataSource(this.trainingProgramList);

  }).finally(() => (this.isLoading = false));
}

filterTrainingPrograms(e: Event) {

  //let filter = (e.target as HTMLInputElement).value;
  //  let newValue = dataSourceTrainingPrograms.data ? value.data:[]
    let resultArray:any = []
     if(this.filterText === '' || this.filterText === null){
      this.dataSourceTrainingPrograms.data = this.trainingProgramList.filter(r=>r.active);
    }
    else{

    for(var item of this.trainingProgramList){
      if(item['positionTitle'].toLowerCase().includes(this.filterText) ||
      item['trainingProgramTypeTitle'].toLowerCase().includes(this.filterText)
      ){
         resultArray.push(item);
      }
   }
    this.dataSourceTrainingPrograms.data= resultArray;
    }



 // this.filterValue = filter;
}

deleteTrainingProgram(templateRef:any,row)
{
  this.tpId = row.id;
  this.deleteDescription = `You are selecting to delete Training Program ${row.trainingProgramType.trainingProgramTypeTitle} for ${row.position.positionTitle}`;
   const dialogRef = this.dialog.open(templateRef, {
     width: '600px',
     height: 'auto',
     hasBackdrop: true,
     disableClose: true,
   });
 }
 Delete(e: any)
  {

    var options = new TrainingProgram_HistoryCreateOptions();
    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.ChangeNotes = data['reason'];
    options.ActionType = 'delete';


    this.trainingProgramsrvc
    .makeActiveInactiveOrDelete(this.tpId, options)
    .then((res: any) => {
      this.getallTrainingPrograms();
      this.getTrainingProgramStats();
      this.alert.successToast("Training program deleted successfully")
    });

  }

  tpTypeName:string;
  tpStatus:boolean;
  async openFlyInPanel(templateRef: any, name: string, status:boolean) {
    this.tpTypeName = name;
    this.tpStatus = status
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
}
