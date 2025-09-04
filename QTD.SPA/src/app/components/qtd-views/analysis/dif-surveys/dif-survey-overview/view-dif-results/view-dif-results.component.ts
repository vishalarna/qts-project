import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { DIFResult_UpdateOptions } from '@models/DIFSurvey/DIFResult_UpdateOptions';
import { DIFSurvey } from '@models/DIFSurvey/DIFSurvey';
import { DIFSurvey_Task } from '@models/DIFSurvey/DIFSurvey_Task';
import { DIFSurvey_Task_Status } from '@models/DIFSurvey/DIFSurvey_Task_Status';
import { DIFSurvey_Task_TrainingFrequency } from '@models/DIFSurvey/DIFSurvey_Task_TrainingFrequency';
import { Store } from '@ngrx/store';
import { ApiDifSurveyService } from 'src/app/_Services/QTD/DifSurvey/api.difsurvey.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-view-dif-results',
  templateUrl: './view-dif-results.component.html',
  styleUrls: ['./view-dif-results.component.scss'],
})
export class ViewDifResultsComponent implements OnInit {
  dataSource: any;
  displayedColumns: string[];
  id: string;
  difSurveyResults: DIFSurvey;
  taskTrainingFrequency: DIFSurvey_Task_TrainingFrequency[];
  difSurveyTaskStatus: DIFSurvey_Task_Status[];
  startDate: string;
  editId: any;
  dueDate: string;
  valueToUpdate: string;
  editType: 'overrideStatus' | 'taskFrequency' | 'none' | 'comment';
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('dataSort') set dataSort(sorting: MatSort) {
    if (sorting) this.dataSource.sort = sorting;
  }
  
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private store: Store<{ toggle: string }>,
    private difSurveyService: ApiDifSurveyService
  ) {}

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<DIFSurvey_Task>([]);
    this.displayedColumns = [
      'fullNumber',
      'description',
      'status',
      'averageDifficulty',
      'averageImportance',
      'averageFrequency',
      'defaultTrainingStatus',
      'override',
      'trainingFrequency',
      'comments',
    ];
    this.store.dispatch(sideBarClose());
    this.route.params.subscribe((params) => {
      this.id = params['id'];
    });
    this.loadAsync();
    this.getTaskTrainingFrequency();
    this.getDifSurveyTask();
    this.taskTrainingFrequency = [];
    this.difSurveyTaskStatus = [];
    this.editId = '';
    this.startDate = '';
    this.dueDate = '';
    this.valueToUpdate = '';
    this.editType = 'none';
  }

  goBack() {
    this.router.navigate(['/analysis/dif-survey/overview']);
  }

  viewEnrollments() {
    this.router.navigate(['/analysis/dif-survey', this.id, 'enrollments']);
  }

  async loadAsync() {
    this.valueToUpdate = null;
    this.editType = 'none';
    await this.difSurveyService.getResultByIdAsync(this.id).then((res) => {
      this.difSurveyResults = res;
      this.dataSource = new MatTableDataSource<DIFSurvey_Task>(this.difSurveyResults.tasks);
      this.dataSource.data =  this.dataSource.data.sort((a,b)=> a.task.fullNumber.localeCompare(b.task.fullNumber, undefined, { numeric: true }));
      setTimeout(()=>{
        this.dataSource.sort = this.sort;
      },1);
      this.dataSource.sortingDataAccessor=this.customSortAccessor;
      this.getStartDate();
      this.getDueDate();
    });
  }

  async getTaskTrainingFrequency() {
    await this.difSurveyService.getTaskTrainingFrequency().then((res) => {
      this.taskTrainingFrequency = res;
    });
  }

  async getDifSurveyTask() {
    await this.difSurveyService.getDifSurveyTaskStatus().then((res) => {
      this.difSurveyTaskStatus = res;
    });
  }

  onCommentChange(row: string, e: any) {
    this.editType = 'comment';
    this.valueToUpdate = e.target.value;
  }

  getStartDate() {
    const date = new Date(this.difSurveyResults.startDate);
    const day = date.getDate();
    const month = date.toLocaleString('default', { month: 'short' });
    const year = date.getFullYear();

    const formattedDate = `${day} ${month} ${year}`;
    this.startDate = formattedDate;
  }

  getDueDate() {
    const date = new Date(this.difSurveyResults.dueDate);
    const day = date.getDate();
    const month = date.toLocaleString('default', { month: 'short' });
    const year = date.getFullYear();

    const formattedDate = `${day} ${month} ${year}`;
    this.dueDate = formattedDate;
  }

  onOverrideTaskStatusChange(item: any, row: any) {
    this.editType = 'overrideStatus';
    this.valueToUpdate = item.target.value;
  }

  getTaskStatusOverrideStatus(row: any) {
    var status = this.difSurveyTaskStatus?.filter(
      (r) => r.id === row.trainingStatus_OverrideId
    )[0]?.status;
    return status?.toUpperCase();
  }

  async updateValue(taskId: string) {
    var options = new DIFResult_UpdateOptions();
    switch (this.editType) {
      case 'overrideStatus':
        options.trainingStatus_OverrideId = this.valueToUpdate;
        await this.difSurveyService.updateDIFResultsAsync(taskId, options);
        this.loadAsync();
        break;
      case 'taskFrequency':
        options.difSurvey_Task_TrainingFrequencyId = this.valueToUpdate;
        await this.difSurveyService.updateDIFResultsAsync(taskId, options);
        this.loadAsync();
        break;
      case 'comment':
        options.comments = this.valueToUpdate;
        await this.difSurveyService.updateDIFResultsAsync(taskId, options);
        this.loadAsync();
        break;
    }
  }

  onTaskFrequencyChange(item: any, row: any) {
    this.editType = 'taskFrequency';
    this.valueToUpdate = item.target.value;
  }

  getTrainingTaskFrequency(row: any) {
    var status = this.taskTrainingFrequency?.filter(
      (r) => r.id === row.difSurvey_Task_TrainingFrequencyId
    )[0]?.status;
    return status;
  }
  
  customSortAccessor = (difSurveyTask: DIFSurvey_Task, column: string): string  => {
    switch (column) {
      case 'description':
        return difSurveyTask.task.description;
      case 'status':
        return difSurveyTask.task.active.toString();
      case 'defaultTrainingStatus':
          return difSurveyTask.trainingStatus_Calculated?.status.toUpperCase(); 
       case 'override':
       return difSurveyTask.trainingStatusOverrideId === null ? "NA": this.getTaskStatusOverrideStatus(difSurveyTask)?.toString();
       case 'trainingFrequency':
        return  difSurveyTask.difSurvey_Task_TrainingFrequencyId == null ? "-"  : this.getTrainingTaskFrequency(difSurveyTask).toString();
      default:
        return difSurveyTask[column];
    }
  };

  sortDataSource(sort: Sort) {
    if(sort.active == 'fullNumber' && sort.direction == 'asc'){
      const sortedArray = this.dataSource.data.sort((a,b)=> a?.task?.fullNumber.localeCompare(b?.task?.fullNumber, undefined, { numeric: true }));
      this.dataSource.data = sortedArray;
    }
    else if(sort.active == 'fullNumber' && sort.direction == 'desc'){
      const sortedArray = this.dataSource.data.sort((a,b)=> b?.task?.fullNumber.localeCompare(a?.task?.fullNumber, undefined, { numeric: true }));
      this.dataSource.data = sortedArray;
    }
 }

}
