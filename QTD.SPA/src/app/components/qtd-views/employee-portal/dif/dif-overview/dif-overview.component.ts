import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort} from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { DIFSurveyEmployeeVM } from '@models/DIFSurvey/DIFSurveyEmployeeVM';
import { Store } from '@ngrx/store';
import { ApiDifSurveyService } from 'src/app/_Services/QTD/DifSurvey/api.difsurvey.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-dif-overview',
  templateUrl: './dif-overview.component.html',
  styleUrls: ['./dif-overview.component.scss'],
})
export class DifOverviewComponent implements OnInit {
  url: string = 'Dashboard / Surveys/ DIF';
  completedSurveysdataSource : MatTableDataSource<DIFSurveyEmployeeVM>;
  displayedCompletedSurveys: string[];
  pendingSurveysdataSource :MatTableDataSource<DIFSurveyEmployeeVM>;
  displayedPendingSurveys: string[];
  difSurveyCompletedEmployeeList: DIFSurveyEmployeeVM[];
  difSurveyPendingEmployeeList: DIFSurveyEmployeeVM[];
  pendingCount:number;
  completedCount:number;
  @ViewChild(MatSort) sort: MatSort;
  difSurveyEmployeeVm:DIFSurveyEmployeeVM[];
  
  constructor(private dIFSurveyService: ApiDifSurveyService,
    private router: Router, private store: Store) {}

  ngOnInit(): void {
    this.store.dispatch(sideBarOpen());
    this.getAllDifSurveyCompleteddata();
    this.getAllDifSurveyPendingData();
    this.displayedCompletedSurveys = ['title', 'completionDate','action'];
    this.displayedPendingSurveys = ['title', 'dueDate', 'status'];
  }
  
  async getAllDifSurveyCompleteddata() {
    this.difSurveyCompletedEmployeeList = await this.dIFSurveyService.getAllDIFSurveyCompleted();
    this.completedSurveysdataSource = new MatTableDataSource<DIFSurveyEmployeeVM>(this.difSurveyCompletedEmployeeList);
    this.completedCount =this.difSurveyCompletedEmployeeList.length;
    setTimeout(()=>{
      this.completedSurveysdataSource.sort = this.sort;
    },1);
  }

  async getAllDifSurveyPendingData() {
   this.difSurveyPendingEmployeeList = await this.dIFSurveyService.getAllDIFSurveyPending();
   this.pendingSurveysdataSource = new MatTableDataSource<DIFSurveyEmployeeVM>(this.difSurveyPendingEmployeeList);
   this.pendingCount=this.difSurveyPendingEmployeeList.length;
   setTimeout(() => {
    this.pendingSurveysdataSource.sort = this.sort; 
  },1)

  }

  filterDifSurveys(e: any) {
    const filterValue = e.target.value.trim().toLowerCase();
    if (this.difSurveyCompletedEmployeeList) {
      const filteredData = this.difSurveyCompletedEmployeeList.filter(
        (item) =>
          item.title.toLowerCase().includes(filterValue)
      );
      this.completedSurveysdataSource = new MatTableDataSource<DIFSurveyEmployeeVM>(filteredData);
    }
  }

  filterPendingDifSurveys(e: any) {
    const filterValue = e.target.value.trim().toLowerCase();
    if (this.pendingSurveysdataSource) {
        const filteredData = this.difSurveyPendingEmployeeList.filter(item =>
            item.title.toLowerCase().includes(filterValue)
        );
        this.pendingSurveysdataSource = new MatTableDataSource<DIFSurveyEmployeeVM>(filteredData);
    }
  }
  
  sortDataPendingSurveys(sort: Sort) {
    this.pendingSurveysdataSource.sort = this.sort;
    const data = this.pendingSurveysdataSource.data;
    if (!sort.active || sort.direction === '') {
      this.pendingSurveysdataSource.data = data;
      return;
    }
 
    this.pendingSurveysdataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'title':
          return this.compare(a.title, b.title, isAsc);
        case 'dueDate':
          return this.compare(a.dueDate, b.dueDate, isAsc);
        case 'status':
          return this.compare(a.status, b.status, isAsc);
        default:
          return 0;
      }
    });
  }
 
  compare(a: number | string |Date , b: number | string | Date, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }
 
  viewResponseClick(item:any){
    this.router.navigate(['/emp/dif-survey', item.difSurveyId, 'view-response']);
  }

  viewDifSurveyPage(item:any){
    this.router.navigate(['/emp/dif-survey', item.difSurveyId, 'dif-survey-page']);
  }
}
