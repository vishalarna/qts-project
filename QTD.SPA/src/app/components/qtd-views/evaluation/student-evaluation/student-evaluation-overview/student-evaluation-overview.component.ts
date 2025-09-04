import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { StudentEvaluationStatsVM } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationStatsVM';
import { StudentEvaluationHistoryCreateOptions } from 'src/app/_DtoModels/StudentEvaluationHistory/StudentEvaluationHistoryCreateOptions';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose, sideBarBackDrop, sideBarDisableClose, sideBarMode, sideBarOpen, freezeMenu } from 'src/app/_Statemanagement/action/state.menutoggle';
export interface PeriodicElement {
  name: string;
  position: string;
  weight: number;
  symbol: string;
  modifiedate: string;
}
const ELEMENT_DATA: PeriodicElement[] = [
  { position: 'QTS_036', name: 'Migrate System Disturbances', weight: 52, symbol: 'Published', modifiedate: "25-09-1994" },
  { position: 'QTS_032', name: 'Power Sytem Protection', weight: 45, symbol: 'Inactive', modifiedate: "28-09-1994" },
];
@Component({
  selector: 'app-student-evaluation-overview',
  templateUrl: './student-evaluation-overview.component.html',
  styleUrls: ['./student-evaluation-overview.component.scss']
})
export class StudentEvaluationOverviewComponent implements OnInit, AfterViewInit {
  url: string = 'Evaluation / Student Evaluation'
  isLoading: boolean = false;
  isLoadingStats: boolean = false;
  catCompleted: any
  catIncompleted: any
  displayedColumns: string[] = ['studentEvaluationId', 'title', 'questionsNum', 'symbol', 'active', 'id'];
  studentEvaluationStats: StudentEvaluationStatsVM;
  dataSource: MatTableDataSource<any> = new MatTableDataSource();
  FiltereddataSource: MatTableDataSource<any> = new MatTableDataSource();
  studentEvalId: 0;
  isActive: boolean = true;
  activeStatus: string;
  deleteDescription: string;
  modalHeader = '';
  modalDescription = '';
  isDelete: boolean = false;
  sortAscending: boolean = true;
  filterName:any;
  @Input() mode: 'Add' | 'Edit' | 'Copy' = 'Add';
  filterText:any;
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }
  @ViewChild(MatSort) sort: MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */

  clearSearch:any;
  constructor(public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private _router: Router,
    private studentEvaluationService: StudentEvaluationService,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private store: Store<{ toggle: string }>) { }

    ngAfterViewInit(): void {

      // var searchText = localStorage.getItem('searchText');
      // this.filterText = JSON.parse(searchText);
      //this.filterTrainingPrograms(new Event("t"));
    }

  ngOnInit(): void {
    this.store.dispatch(sideBarOpen());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    // this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    // this.store.dispatch(sideBarMode({ mode: 'over' }));
    this.getStatsCount();

    this.getAllEvaluations();


  }

  clearFilter(){
    this.clearSearch = '';
    this.dataSource.filter = null;
  }

  getStatsCount() {
    this.studentEvaluationService.getStatsCount().then((res: StudentEvaluationStatsVM) => {
      this.isLoadingStats = true
      this.studentEvaluationStats = res;
    }).finally(() => {
      this.isLoadingStats = false
    });
  }

  sortData(sort: Sort) {
    this.dataSource.sort = this.sort;
  }

  sortStudentEvaluationStatus(data: any[]): any[] {
    // Sort the data based on the "isPublished" property (0 and 1)
    return data.sort((a, b) => {
      if (this.sortAscending) {
        return a.isPublished - b.isPublished;
      } else {
        return b.isPublished - a.isPublished;
      }
    });
  }
  toggleSortOrder() {
    this.sortAscending = !this.sortAscending;
  }
  async openFlyInPanel(templateRef: any, id: any, mode: any) {

    this.mode = mode;
    this.studentEvalId = id;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  getAllEvaluations(statusChange = false) {
    this.isLoading = true
    this.studentEvaluationService.getAll().then((res: any) => {
      this.dataSource.data = res;
      this.FiltereddataSource.data = res;
      res.forEach((data) => {
        if (data.classRoaster > 0) {
          this.isDelete = true;
        }
      });
      if (statusChange) {
        this.dataSourceStatusFilter(this.activeStatus);
      } else {
        this.dataSourceStatusFilter('Active');
      }
    }).finally(() => {
      this.isLoading = false
      this.filterEvaluation(new Event("t"));
      localStorage.removeItem('searchText');
    });
  }
  OpenQuestionBank() {
    this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    if(this.filterText !== '')
    {
        localStorage.setItem('searchText', JSON.stringify(this.filterText));
    }
    this._router.navigate(['/evaluation/studentevaluation/questionBank']);
  }
  CreateNewEvaluation() {
    this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    if(this.filterText !== '')
    {
        localStorage.setItem('searchText', JSON.stringify(this.filterText));
    }
    this._router.navigate(['/evaluation/studentevaluation/create']);
  }

  EditNewEvaluation(id:any){
    this.store.dispatch(freezeMenu({doFreeze:false}))
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: true }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    if(this.filterText !== ''){
        localStorage.setItem('searchText', JSON.stringify(this.filterText));
    }
    this._router.navigate(['/evaluation/studentevaluation/create'], { queryParams: { id: id } });
  }

  changeStatus(templateRef: any, row: any) {
    if (row.active === true) {
      this.isActive = false;
      this.activeStatus = 'Inactive'
    }
    else {
      this.isActive = true;
      this.activeStatus = 'Active';
    }
    this.studentEvalId = row.id;
    this.modalHeader = row.active
      ? 'Make Inactive'
      : 'Active' + ' Student Evaluation';

    this.modalDescription = `You are selecting to make this Student Evaluation ${row.title} ${this.activeStatus}`

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  MakeActive(e: any, active: boolean) {
    var options = new StudentEvaluationHistoryCreateOptions();
    var studentEvalArray: number[] = []
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.studentEvaluationNotes = data['reason'];
    options.actionType = this.activeStatus.toLowerCase();
    studentEvalArray.push(this.studentEvalId);
    options.studentEvaluationIds = studentEvalArray;
    this.studentEvaluationService
      .makeActiveInactiveOrDelete(this.studentEvalId, options)
      .then((res: any) => {
        this.getAllEvaluations(true);
        this.getStatsCount();
        this.alert.successToast("Student evaluation question status changed successfully");
      });
  }
  deleteStudentEvaluation(templateRef: any, row) {
    this.studentEvalId = row.id;
    this.deleteDescription = `You are selecting to delete the Student Evaluation ${row.title}. This cannot be undone.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  Delete(e: any) {
    var studentEvalArray: number[] = []
    var options = new StudentEvaluationHistoryCreateOptions();
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.studentEvaluationNotes = data['reason'];
    options.actionType = 'delete';
    studentEvalArray.push(this.studentEvalId)
    options.studentEvaluationIds = studentEvalArray;
    this.studentEvaluationService
      .makeActiveInactiveOrDelete(this.studentEvalId, options)
      .then((res: any) => {
        this.getAllEvaluations();
        this.alert.successToast("Student Evaluation delete successfully")
      });

  }
  filterEvaluation(e: Event) {
    //let filter = (e.target as HTMLInputElement).value;
    this.dataSource.filter = this.clearSearch;
  }

  dataSourceStatusFilter(status: any) {
    this.dataSource.data = this.FiltereddataSource.data
    if (status === 'Inactive') {
      this.dataSource.data = this.dataSource.data.filter(item => item.active === false);
    } else if (status === 'Active') {
      this.dataSource.data = this.dataSource.data.filter(item => item.active === true);
    }
  }

  BulkEditStudentEvaluation()
  {

    if(this.filterText !== '')
    {
        localStorage.setItem('searchText', JSON.stringify(this.filterText));
    }
    this._router.navigate(['/evaluation/bulkedit/studentevaluation']);

  }

  moduleName:string;
  openFlyInPanelList(templateRef: any,name:string){
    this.moduleName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
}
