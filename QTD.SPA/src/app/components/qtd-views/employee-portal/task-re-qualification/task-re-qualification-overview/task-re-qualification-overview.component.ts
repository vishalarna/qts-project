import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import {
  ChangeDetectorRef,
  Component,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTable as MatTable, MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { TQEmpViewVM } from '@models/TaskQualification/TQEmpViewVM';
import { TQTaskViewVM } from '@models/TaskQualification/TQTaskViewVM';
import { TaskQualificationPengingEvaluatorVM } from '@models/TaskQualification/TaskQualificationPengingEvaluatorVM';
import { TaskQualsByEmpVM } from '@models/TaskQualification/TaskQualsByEmpVM';
import { TaskQualsByTaskVM } from '@models/TaskQualification/TaskQualsByTaskVM';
import { Store } from '@ngrx/store';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import {
  sideBarClose,
  sideBarOpen,
} from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-task-re-qualification-overview',
  templateUrl: './task-re-qualification-overview.component.html',
  styleUrls: ['./task-re-qualification-overview.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      ),
    ]),
  ],
})

export class qualification implements OnInit {
  url: string = 'Dashboard / Task Re-qualification';
  completedDataSource = new MatTableDataSource<any>();
  completedDataSourceTrainee = new MatTableDataSource<any>();
  pendingDataSourceTrainee = new MatTableDataSource<any>();
  pendingDataSourceEval = new MatTableDataSource<any>();
  completedDataSourceEval = new MatTableDataSource<any>();
  pendingDataSource = new MatTableDataSource<any>();
  pendingDataEmpView = new MatTableDataSource<any>();
  isLoading: boolean = true;
  fontStyle?: string;
  currentView: string = 'Trainee';
  CompleteCount = 0;
  PendingCount = 0;
  selectedTabIndex = 1;
  taskQualificationList: any[] = [];
  taskQualificationTraineeCompletedList: any[] = [];
  taskQualificationTraineePendingList: any[] = [];
  taskQualificationEvalCompletedList: any[] = [];
  taskQualificationEvalPendingList: any[] = [];
  taskQualificationPendingTaskViewList: any[] = [];
  taskEvalQualificationList: any[] = [];
  taskEvalEMPQualificationList: any[] = [];
  tempPendingEvalTasks: any[] = [];

  @ViewChild(MatSort) traineeCompletedSort: MatSort;
  @ViewChild(MatSort) evalCompletedSort: MatSort;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  expandedElement: any | null;
  expandedElement1: any | null;
  @ViewChild('evalEmpSort') evalEmpSort: MatSort;
  @ViewChildren('evalTaskSortExpand') evalTaskSortExpand: QueryList<MatSort>;
  @ViewChildren('evalTaskInnerTables') evalTaskInnerTables: QueryList<MatTable<TaskQualsByTaskVM>>;
  dataSourceEvalPending = new MatTableDataSource<any>();
  dataSourceEMPPending = new MatTableDataSource<any>();
  tqTaskWithEmployeesList = new MatTableDataSource<any>();

  @ViewChild('evalTaskSort') set evalTaskSort(sorting: MatSort) {
    if (sorting) this.pendingDataSourceEval.sort = sorting;
  }
  @ViewChild('traineePendingSort') set traineePendingSort(sorting: MatSort) {
    if (sorting) this.pendingDataSourceTrainee.sort = sorting;
  }
  isEmpView: boolean = false;
  isRedirectToEval: boolean = false;
  displayedCompletedColumns: string[] = [
    'number',
    'taskDescription',
    'posNames',
    'empReleaseDate',
    'dueDate',
    'completedDate',
    'criteriaMet',
    'totalRequiredSignOffs',
    'evaluatorName',
    'feedbackAndComments',
  ];
   displayedPendingColumns: string[] = [
    'taskSkillNumber',
    'taskSkillDescription',
    'posNames',
    'empReleaseDate',
    'dueDate',
    'totalRequiredSignOffs',
    'evaluatorName',
    'previewTask',
  ];

  displayedCompletedColumnsEval: string[] = [
    'employeeName',
    'number',
    'taskDescription',
    'empReleaseDate',
    'dueDate',
    'completedDate',
    'criteriaMet',
    'feedbackAndComments',
  ];
  columnsToDisplayEval = ['expand', 'taskSkillNumber', 'taskSkillDescription'];

  columnsToDisplayEMP = ['expand', 'empFName', 'positions'];

  isExpansionDetailRow = (i: number, row: Object) =>
    row.hasOwnProperty('detailRow');

  innerDisplayedColumnsEval = [
    'empName',
    'positions',
    'releaseDate',
    'dueDate',
    'requiredRequals',
    'evaluatorName',
    'tqStatus',
    'action',
  ];
  innerDisplayedColumnsEMP = [
    'taskSkillNumber',
    'taskSkillDescription',
    'releaseDate',
    'dueDate',
    'requiredRequals',
    'action',
  ];
  isEvaluatorViewVisible :boolean=false;
  constructor(
    public dialog: MatDialog,
    private cd: ChangeDetectorRef,
    private taskService: TasksService,
    private store: Store<{ toggle: string }>,
    private route: ActivatedRoute,
    private _router: Router,
    private _taskPipe: TaskSortPipePipe,
    private _taskRequalificationService: TaskRequalificationService
  ) {}

  async ngOnInit() {
    await this.getTQEvaluatorBitAsync();
    await this.getEMPTasksTraineeCompletedAsync();
    await this.getEMPTasksTraineePendingAsync();
    this.route.queryParams.subscribe(params => {
      params.isRedirectToEval == 'true' ? this.isRedirectToEval = true : this.isRedirectToEval;
      if(this.isRedirectToEval){
        this.changeView('Evaluator')
        this.setActiveTab(1);
      }
    });
  }
  
  async getTQEvaluatorBitAsync(){
    await this._taskRequalificationService.getTQEvaluatorBitAsync().then((res)=>{
      this.isEvaluatorViewVisible = res;
    });
  }

  toggleRowEMP(element: any, toggleIndex : number, toggleExpandedRowId: number) {    
    element.taskQualsByEmpVMs &&
    (element.taskQualsByEmpVMs as MatTableDataSource<TaskQualsByEmpVM>).data.length
      ? (this.expandedElement =
          this.expandedElement === element ? null : element)
      : null;
   this.cd.detectChanges();

  }

  filterCompletedTest(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.completedDataSource.filter = filter.trim().toLowerCase();
  }

  filterCompletedTaskTrainee(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.completedDataSourceTrainee.filter = filter.trim().toLowerCase();
  }

 filterPendingEvalTasks(e: Event) {
  let filter = (e.target as HTMLInputElement).value.toLowerCase();
  var filteredList: any[] = [];

  filteredList = this.pendingDataSourceEval.data.filter(
    (x) =>(x.taskDescription && x.taskDescription.toLowerCase().includes(filter)) ||
      (x.skillDescription && x.skillDescription.toLowerCase().includes(filter))
  );

  this.dataSourceEvalPending.data = filteredList;
 }


  sortTraineeCompletedQualifiactions(sort: Sort) {
    this.completedDataSourceTrainee.data = this.completedDataSourceTrainee.data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'completedDate':
          return this.compare(a.taskQualificationDate, b.taskQualificationDate, isAsc);
        case 'evaluatorName':
          return this.compare((a.tqEvalSignOffModels[0]?.evaluatorName || '').toUpperCase(), (b.tqEvalSignOffModels[0]?.evaluatorName || '').toUpperCase(), isAsc);
        default:
          return 0;
      }
    });
    this.completedDataSourceTrainee.sort = this.traineeCompletedSort
  }

  sortEvaluatorCompletedQualifications(sort: Sort) {
     this.completedDataSourceEval.sort = this.evalCompletedSort;
     this.completedDataSourceEval.data = this.completedDataSourceEval.data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'completedDate':
          return this.compare(a.taskQualificationDate, b.taskQualificationDate, isAsc);
        default:
          return 0;
      }
    });
  }

  compare(a: number | string |Date , b: number | string | Date, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  sortEvaluatorPendingEmpQualifications(sort: Sort) {
    this.pendingDataEmpView.sort = this.evalEmpSort;
 }
 sortEvaluatorPendingEmpTaskQualifications(sort: Sort,element:TQEmpViewVM) {
  element.taskQualsByEmpVMs = this._taskPipe.transform(
    element.taskQualsByEmpVMs.data,
    sort.direction,
    sort.active
    );
}
 
 sortEvaluatorPendingTaskQualifications(sort:Sort){
    this.pendingDataSourceEval = this._taskPipe.transform(
      this.pendingDataSourceEval.data,
      sort.direction,
      sort.active
      );
 }

 customSortAccessor(item: TaskQualsByTaskVM, property: string): string | number {
    if (property === 'evaluatorName') {
      return item.evaluatorListWithStatus.map(x=>x.evaluatorName.toLocaleLowerCase()).toString() 
    }
    else if(property === 'tqStatus'){
      var tqStatus = item.evaluatorListWithStatus.map(a=>(a.status=== "Pending"?"In Progress":a.status));
      return tqStatus.toString()
    } else {
      return item[property];
    }
}

  filterPendingTest(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.pendingDataSource.filter = filter.trim().toLowerCase();
  }

  async changeView(key) {
    this.isLoading = true;
    switch (key) {
      case 'All':
        this.currentView = 'All';
        break;
      case 'Trainee':
        this.currentView = 'Trainee';
        await this.getEMPTasksTraineeCompletedAsync();
        await this.getEMPTasksTraineePendingAsync();
        break;
      case 'Evaluator':
        this.currentView = 'Evaluator';
        this.fontStyle = 'taskView';
        await this.getEMPTasksEvalCompletedAsync();
        await this.getEMPTasks();
        break;
      default:
        break;
    }
  }

  changePendingView(event) {
    if (event === 'empView') {
      this.isEmpView = true;
    } else {
      this.isEmpView = false;
    }
  }

goToTaskDetail(row: any) {
  let id: number | null = null;
  let type: string = '';
  let routeSegment: string = '';
  if (row.taskId) {
    id = row.taskId;
    type = 'task';

    if (row.number) {
      const taskLetterRegx: RegExpMatchArray | null = row.number.match(/^[^\d]*/);
      const taskLetter: string = taskLetterRegx ? taskLetterRegx[0] : '';
      const splitValues: string[] = row.number.split(new RegExp(`^${taskLetter}`));

      routeSegment = `${id}-${taskLetter}_${splitValues[1]}-${row.id}-${type}`;
    } else {
      routeSegment = `${id}-${row.id}-${type}`;
    }
  } else if (row.enablingObjectiveId) {
    id = row.enablingObjectiveId;
    type = 'eo';

    if (row.enablingObjectiveNumber) {
      routeSegment = `${id}-${row.enablingObjectiveNumber}-${row.id}-${type}`;
    } else {
      routeSegment = `${id}-${row.id}-${type}`;
    }
  }

  this.store.dispatch(sideBarClose());

  this._router.navigate([ 'emp/task-re-qualification/view-task/feedback', routeSegment, ]);
}

  startTask(row, parentRow) {
    const taskNumber = row.taskNumber;
    const skillNumber = row.skillNumber;
    if(row.taskId != null){
      this._router.navigate(['emp/task-re-qualification/task-suggestions', row.tqId + '-' + '§' + '_' + row.taskId + '.' + parentRow.empId + '.' + taskNumber + '.task']);
    }
     if (row.skillId != null) {
      this._router.navigate(['emp/task-re-qualification/task-suggestions', row.skillQualificationId + '-' + '§' + '_' + row.skillId + '.' + parentRow.empId + '.' + skillNumber + '.eo']);
    }
  }

  startTaskEvalPending(row, parentRow) {
    this.store.dispatch(sideBarClose());
    const taskNumber = parentRow.taskNumber;
    const skillNumber = parentRow.skillFullNumber;
    if (parentRow.taskId != null) {
     this._router.navigate(['emp/task-re-qualification/task-suggestions',row.tqId + '-' + '§' + '_' + parentRow.taskId + '.' + row.empId + '.' + taskNumber + '.task']);
    }

    if (parentRow.skillId != null) {
      this._router.navigate(['emp/task-re-qualification/task-suggestions',row.skillQualificationId + '-' + '§' + '_' + parentRow.skillId + '.' + row.empId + '.' + skillNumber + '.eo']);
    }
  }

  async getEMPTasksTraineeCompletedAsync() {
    this.taskQualificationTraineeCompletedList = [];
    await this._taskRequalificationService
      .getCompletedTaskQualificationsAsTraineeAsync()
      .then((res) => {
        this.isLoading = false;
        this.taskQualificationTraineeCompletedList = res;
        this.completedDataSourceTrainee.data =
          this.taskQualificationTraineeCompletedList.map((item)=>{
            let taskQualDate = item.taskQualificationDate
          ? new Date(item.taskQualificationDate + "Z").toLocaleString()
          : null;

            let sqDate = item.skillQualificationDate
            ? new Date(item.skillQualificationDate + "Z").toLocaleString()
            : null;
            return {...item,taskQualificationDate:taskQualDate, skillQualificationDate:sqDate}
          });
        this.CompleteCount = this.completedDataSourceTrainee.data.length;
      })
      .catch((res: any) => {
        console.error('Error fetching completed tasks: ', res);
      });
  }

  async getEMPTasksTraineePendingAsync() {
    this.taskQualificationTraineePendingList = [];
    await this._taskRequalificationService
      .getPendingTaskQualificationsAsTraineeAsync()
      .then((res) => {
        this.isLoading = false;
        this.taskQualificationTraineePendingList = res;
        this.pendingDataSourceTrainee.data =
          this.taskQualificationTraineePendingList;
        this.PendingCount = this.pendingDataSourceTrainee.data.length;
      })
      .catch((res: any) => {
        console.error('Error fetching completed tasks: ', res);
      });
  }

  async getEMPTasksEvalCompletedAsync() {
    this.taskQualificationEvalCompletedList = [];
    await this._taskRequalificationService
      .getCompletedTaskQualificationsAsEvaluatorAsync()
      .then((res) => {
        this.isLoading = false;
        this.taskQualificationEvalCompletedList = res;
        this.completedDataSourceEval.data =
          this.taskQualificationEvalCompletedList.map((item)=>{
            let taskQualDate = item.taskQualificationDate
          ? new Date(item.taskQualificationDate + "Z").toLocaleString()
          : null;

            let sqDate = item.skillQualificationDate
            ? new Date(item.skillQualificationDate + "Z").toLocaleString()
            : null;
            return {...item,taskQualificationDate:taskQualDate, skillQualificationDate:sqDate}
          });
        this.CompleteCount = this.completedDataSourceEval.data.length;
      })
      .catch((res: any) => {
        console.error('Error fetching completed tasks: ', res);
      });
  }

  removeDuplicates(array, key) {
    return array.filter((obj, index, self) =>
      index === self.findIndex((o) => o[key] === obj[key])
    );
  }


  getexpandedDetails(element: TQTaskViewVM, toggleIndex : number) {
    element.taskQualsByTaskVMs &&
    (element.taskQualsByTaskVMs as MatTableDataSource<TaskQualsByTaskVM>).data.length
      ? (this.expandedElement =
          this.expandedElement === element ? null : element)
      : null;
    this.cd.detectChanges();
    this.evalTaskInnerTables.forEach(
      (table, index) =>{

        (table.dataSource as MatTableDataSource<TaskQualsByTaskVM>).sort =
        this.evalTaskSortExpand.toArray()[index];
        
        (table.dataSource as MatTableDataSource<TaskQualsByTaskVM>).sortingDataAccessor=this.customSortAccessor;
      }
    );
  }

  async getEMPTasks() {
    await this.taskService
      .getEMPTask()
      .then((res) => {
        var empViewdata = this.setEvalPendingEmpView(res);
        this.pendingDataEmpView = new MatTableDataSource<TQEmpViewVM>(empViewdata);
        var taskViewdata = this.setEvalPendingTaskView(res);
        this.pendingDataSourceEval = new MatTableDataSource<TQTaskViewVM>(taskViewdata);
        this.PendingCount = res.length;
      })
      .catch((res: any) => {});
  }

  setEvalPendingEmpView(taskQualsVMs:TaskQualificationPengingEvaluatorVM[]):TQEmpViewVM[] {
    var empIds = Array.from(new Set(taskQualsVMs.map(item => item.empId)));
    var tQEmpViewVMs :TQEmpViewVM[] = [];
    for(var empId of empIds){
      var taskQualsByEmp = taskQualsVMs.filter(x=>x.empId == empId);
      if(taskQualsByEmp.length>0){
        var emp = taskQualsByEmp[0];
        var tqEmpVm : TQEmpViewVM = new TQEmpViewVM();
        tqEmpVm.empEmail=emp.empEmail;
        tqEmpVm.empFName=emp.empFName;
        tqEmpVm.empLName=emp.empLastName;
        tqEmpVm.empId=emp.empId;
        tqEmpVm.empImage=emp.empImage;
        tqEmpVm.employeeNumber=emp.empNumber;
        tqEmpVm.positions=emp.empPositions;
        var tqByEmpVMs : TaskQualsByEmpVM[] = []; 
        for(var taskQual of taskQualsByEmp){
          var tqByEmpVM : TaskQualsByEmpVM = new TaskQualsByEmpVM();
          tqByEmpVM.taskId=taskQual.taskId;
          tqByEmpVM.letter = taskQual.taskLetter;
          tqByEmpVM.taskNumber = taskQual.taskFullNumber;
          tqByEmpVM.number = taskQual.taskNumber;
          tqByEmpVM.taskDescription= taskQual.taskDescription;
          tqByEmpVM.tqId= taskQual.id;
          tqByEmpVM.dueDate = taskQual.dueDate;
          tqByEmpVM.releaseDate = taskQual.empReleaseDate;
          tqByEmpVM.requiredRequals= taskQual.requiredRequals;
          tqByEmpVM.canStart = taskQual.canStart;
          tqByEmpVM.skillId = taskQual.skillId;
          tqByEmpVM.skillNumber = taskQual.skillFullNumber;
          tqByEmpVM.skillDescription = taskQual.skillDescription;
          tqByEmpVM.sqDueDate = taskQual.sqDueDate;
          tqByEmpVM.skillQualificationId =taskQual.skillQualificationId;
          tqByEmpVMs.push(tqByEmpVM);
        }
        tqEmpVm.taskQualsByEmpVMs = new MatTableDataSource(tqByEmpVMs);
        tQEmpViewVMs.push(tqEmpVm);
      }
    }
    return tQEmpViewVMs;
  }

  setEvalPendingTaskView(taskQualsVMs:TaskQualificationPengingEvaluatorVM[]):TQTaskViewVM[]{
    var taskIds = Array.from(new Set(taskQualsVMs.map(item => item.taskId))).filter(item => item != null);
    var skillIds = Array.from(new Set(taskQualsVMs.map(item => item.skillId))).filter(item => item != null);
    var tQTaskViewVMs :TQTaskViewVM[] = [];
    for(var taskId of taskIds){
      var taskQualsByTask = taskQualsVMs.filter(x=>x.taskId == taskId);
      if(taskQualsByTask.length>0){
        var task = taskQualsByTask[0];
        var tqTaskVm : TQTaskViewVM = new TQTaskViewVM();
        tqTaskVm.taskId=task.taskId;
        tqTaskVm.letter=task.taskLetter;
        tqTaskVm.taskNumber=task.taskFullNumber;
        tqTaskVm.number=task.taskNumber;
        tqTaskVm.taskDescription=task.taskDescription;
        var tqByTaskVMs : TaskQualsByTaskVM[] = []; 
        for(var taskQual of taskQualsByTask){
          var tqByTaskVM : TaskQualsByTaskVM = new TaskQualsByTaskVM();
          tqByTaskVM.empId=taskQual.empId;
          tqByTaskVM.empImage = taskQual.empImage;
          tqByTaskVM.empName = taskQual.empFName + " " + taskQual.empLastName;
          tqByTaskVM.positions = taskQual.empPositions;
          tqByTaskVM.releaseDate= taskQual.empReleaseDate;
          tqByTaskVM.dueDate=taskQual.dueDate;
          tqByTaskVM.requiredRequals= taskQual.requiredRequals;
          tqByTaskVM.tqId = taskQual.id;
          tqByTaskVM.tqStatus = taskQual.status;
          tqByTaskVM.evaluatorListWithStatus=taskQual.evaluatorListWithStatus;
          tqByTaskVM.releaseToAllSingleSignOff=taskQual.releaseToAllSingleSignOff;
          tqByTaskVM.signOffOrderEnabled=taskQual.signOffOrderEnabled;
          tqByTaskVM.canStart=taskQual.canStart;
          tqByTaskVMs.push(tqByTaskVM);
        }
        tqTaskVm.taskQualsByTaskVMs = new MatTableDataSource(tqByTaskVMs);
        tQTaskViewVMs.push(tqTaskVm);
      }
    }

    for(var skillId of skillIds){
      var taskQualsByTask = taskQualsVMs.filter(x=>x.skillId == skillId);
      if(taskQualsByTask.length>0){
        var task = taskQualsByTask[0];
        var tqTaskVm : TQTaskViewVM = new TQTaskViewVM();
        tqTaskVm.skillId=task.skillId;
        tqTaskVm.skillFullNumber = task.skillFullNumber;
        tqTaskVm.skillDescription = task.skillDescription;
        var tqByTaskVMs : TaskQualsByTaskVM[] = []; 
        for(var taskQual of taskQualsByTask){
          var tqByTaskVM : TaskQualsByTaskVM = new TaskQualsByTaskVM();
          tqByTaskVM.empId=taskQual.empId;
          tqByTaskVM.empImage = taskQual.empImage;
          tqByTaskVM.empName = taskQual.empFName + " " + taskQual.empLastName;
          tqByTaskVM.positions = taskQual.empPositions;
          tqByTaskVM.releaseDate= taskQual.empReleaseDate;
          tqByTaskVM.dueDate=taskQual.dueDate;
          tqByTaskVM.requiredRequals= taskQual.requiredRequals;
          tqByTaskVM.tqId = taskQual.id;
          tqByTaskVM.tqStatus = taskQual.status;
          tqByTaskVM.evaluatorListWithStatus=taskQual.evaluatorListWithStatus;
          tqByTaskVM.releaseToAllSingleSignOff=taskQual.releaseToAllSingleSignOff;
          tqByTaskVM.signOffOrderEnabled=taskQual.signOffOrderEnabled;
          tqByTaskVM.canStart=taskQual.canStart;
          tqByTaskVM.skillQualificationId=taskQual.skillQualificationId;
          tqByTaskVMs.push(tqByTaskVM);
        }
        tqTaskVm.taskQualsByTaskVMs = new MatTableDataSource(tqByTaskVMs);
        tQTaskViewVMs.push(tqTaskVm);
      }
    }
    return tQTaskViewVMs;
  }

  goToTaskFeedback(row) {
    this.store.dispatch(sideBarClose());
    if(row.id != null){
    this._router.navigate(['emp/task-re-qualification/view-feedback',
      row.id +
        '-' +
        '§' +
        '_' +
        row.empId + '.task',
    ]);
   }
   if(row.skillQualificationId != null){
   this._router.navigate(['emp/task-re-qualification/view-feedback',
      row.skillQualificationId +
        '-' +
        '§' +
        '_' +
        row.empId + '.eo',
    ]);
   }
  }

  sortEvaluatorPendingQualificationsEmpView(sort: Sort, index: number) {
    this.expandedElement.taskQualsByEmpVMs = this._taskPipe.transform(
      this.expandedElement.taskQualsByEmpVMs,
      sort.direction,
      sort.active
    ).data;
    this.cd.detectChanges();
  }

  setActiveTab(index: number) {
    this.selectedTabIndex = index;
  }
}



export interface Address {
  employeeName: Date;
  position: Date;
  releaseDate: string;
  dueDate: string;
  totalSignOff: string;
  evalNameSingOff: string;
  tqStatus: string;
  action: string;
}
