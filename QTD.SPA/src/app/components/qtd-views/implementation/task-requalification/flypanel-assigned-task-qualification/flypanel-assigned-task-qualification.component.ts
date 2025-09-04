import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskWithNumberVM } from 'src/app/_DtoModels/Task/TaskWithNumberVM';
import { TQEmpWithTasksVM } from 'src/app/_DtoModels/TaskQualification/TQEmpWithTasksVM';
import { TQEvaluatorWithCount } from 'src/app/_DtoModels/TaskQualification/TQEvaluatorWithCount';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';

@Component({
  selector: 'app-flypanel-assigned-task-qualification',
  templateUrl: './flypanel-assigned-task-qualification.component.html',
  styleUrls: ['./flypanel-assigned-task-qualification.component.scss']
})
export class FlypanelAssignedTaskQualificationComponent implements OnInit {
  mode: 'all' | 'specific' = 'specific';

  @Output() closed = new EventEmitter<any>();
  @Output() saved = new EventEmitter<any>();
  @Input() evalData!: TQEvaluatorWithCount;
  taskList: any = [];

  selectedView: 'emp' | 'task' = 'emp';

  isLoading = false;


  displayedColumns = ["eval", "pending"];
  filterEmpString = "";
  selectedData!: AssignedTQVM[];
  totalTasks = 0;
  totalEmployees = 0;

  employees!: TQEmpWithTasksVM[];
  qualRequiredTasks!: TaskWithNumberVM[];
  assignedTQSource: AssignedTQVM[] = [];
  originalAssignedTQSource: AssignedTQVM[] = [];
  assignedTaskSource:AssignedTQVM[] =[];
  originalAssignedTaskSource :AssignedTQVM[]=[];
  placeHolder = '/assets/img/ImageNotFound.jpg';

  selection = new SelectionModel<any>(true, []);
  taskSelection = new SelectionModel<any>(true, []);

  searchTask:string='';
  description = "";

  constructor(
    private empService: EmployeesService,
    private tqService: TaskRequalificationService,
    public dialog: MatDialog,
    private labelPipe: LabelReplacementPipe,
    private dynamicLabelPipe: DynamicLabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.readyData();
  }

  async readyData() {
    this.isLoading = true;
    this.employees = await this.tqService.GetEmpWithTasksForTQEvaluator(this.evalData.evaluatorId);
    this.createSpecialData();
    this.qualRequiredTasks = await this.tqService.getRequalTasksForEMP(this.evalData.evaluatorId);
    this.createSpecialDataForTask();
    this.isLoading = false;
  }

  
  createSpecialDataForTask() {
    this.qualRequiredTasks.forEach((data: TaskWithNumberVM) => {
      var assignedTQ = new AssignedTQVM();
      assignedTQ.taskId = data.task.id;
      assignedTQ.taskDescription = data.task.description; 
      assignedTQ.taskNumber = `${data.daNumber}.${data.sdaNumber}.${data.task.number}`;
      assignedTQ.checked = false;
      assignedTQ.indeterminate = false;

      assignedTQ.children =  data.tqEmpWithTasksVM?.map((empData: TQEmpWithTasksVM) => {
        var child = new AssignedTQVM();
        child.empId = empData.empId;
        child.empName = `${empData.empFName} ${empData.empLName}`;
        child.empImage = empData.empImage;
        child.empEmail = empData.empEmail;
        child.checked = false;
        child.taskId = assignedTQ.taskId;
        child.taskDescription = assignedTQ.taskDescription;
        child.taskNumber = assignedTQ.taskNumber;
        child.status = empData.status;
        child.releaseDate = empData.releaseDate;
        child.dueDate = empData.dueDate; 
        child.parent = assignedTQ;
        child.tqId = empData.tqId;
        this.totalEmployees +=1;
        return child;
      });
      this.assignedTaskSource.push(assignedTQ);
    });
    this.originalAssignedTaskSource = Object.assign( this.assignedTaskSource);
  }
  
  
  createSpecialData() {
    this.employees.forEach((data: TQEmpWithTasksVM) => {
      var assignedTQ = new AssignedTQVM();
      assignedTQ.empId = data.empId;
      assignedTQ.empName = data.empFName + " " + data.empLName;
      assignedTQ.empImage = data.empImage;
      assignedTQ.checked = false;
      assignedTQ.indeterminate = false;
      assignedTQ.empEmail = data.empEmail;
      assignedTQ.children = data.tasksWithNumber.map((tn: TaskWithNumberVM) => {
        var child = new AssignedTQVM();
        child.taskDescription = tn.task.description;
        child.taskNumber = tn.daNumber + '.' + tn.sdaNumber + '.' + tn.task.number;
        child.dueDate =tn.dueDate;
        child.releaseDate=tn.releaseDate;
        child.status=tn.status;
        child.checked = false;
        child.taskId = tn.task.id;
        child.tqId = tn.tqId;
        child.empId = assignedTQ.empId;
        this.totalTasks += 1;
        child.empName = assignedTQ.empName;
        child.parent = assignedTQ;
        return child;
      });

      this.assignedTQSource.push(assignedTQ);
    });

    this.originalAssignedTQSource = Object.assign(this.assignedTQSource);
  }

  filterData() {
    this.assignedTQSource = this.originalAssignedTQSource.filter((data: AssignedTQVM) => {
      return data.empEmail?.trim().toLowerCase().includes(this.filterEmpString.trim().toLowerCase())
        || data.empName?.trim().toLowerCase().includes(this.filterEmpString.trim().toLowerCase());
    });
  }

  checkTask(event: any, data: AssignedTQVM) {
    if (event.checked) {
      data.checked = true;
      this.selection.select(data);
    }
    else {
      data.checked = false;
      this.selection.deselect(data);
    }

    this.checkParent(data.parent);
  }

  checkParent(data: AssignedTQVM) {
    if (data.children.every(x => x.checked)) {
      data.checked = true;
      data.indeterminate = false;
    }
    else if (data.children.some(x => x.checked)) {
      data.indeterminate = true;
      data.checked = false;
    }
    else {
      data.indeterminate = false;
      data.checked = false;
    }
  }

  toggleAll(event: any, data: AssignedTQVM) {
    if (event.checked) {
      data.children.forEach((x: AssignedTQVM) => {
        x.checked = true;
        this.selection.select(x);
      });
      data.checked = true;
      data.indeterminate = false;
    }
    else {
      data.children.forEach((x: AssignedTQVM) => {
        x.checked = false;
        this.selection.deselect(x);
      })
      data.checked = false;
      data.indeterminate = false;
    }
  }

  toggleAllTasks(event: any, data: AssignedTQVM) {
    if (event.checked) {
      data.children.forEach((x: AssignedTQVM) => {
        x.checked = true;
        this.taskSelection.select(x);
      });
      data.checked = true;
      data.indeterminate = false;
    }
    else {
      data.children.forEach((x: AssignedTQVM) => {
        x.checked = false;
        this.taskSelection.deselect(x);
      })
      data.checked = false;
      data.indeterminate = false;
    }
  }

  masterToggle(event: any, data: AssignedTQVM[]) {
    data.forEach((tq: AssignedTQVM) => {
      this.selectedView=='emp'?this.toggleAll(event, tq):this.toggleAllTasks(event,tq);
    })
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async openConfirmDialog(templateRef: any) {
    this.description = `You are selecting to remove Evaluator <b>${this.evalData.evaluatorName }</b> as the ` + await this.transformTitle('Task') +` qualification evaluator for\n`
    if(this.selectedView == 'emp'){
      var empIds = this.selection.selected.map((data: AssignedTQVM) => {
        return data.empId;
      })
      empIds = [...new Set(empIds)];
      empIds.forEach(async (id: any) => {
        var empName = this.selection.selected.find((tqData) => {
          return tqData.empId === id;
        }).empName;

        var tasks = this.selection.selected.filter((tq: AssignedTQVM) => {
          return tq.empId === id;
        }).map((data) => {
          return data.taskNumber;
        });

        tasks = [...new Set(tasks)];
        var taskDescription = "";
        tasks.forEach((data: AssignedTQVM) => {
          taskDescription += data + ", ";
        });

        taskDescription = taskDescription.substring(0, taskDescription.length - 2);

        this.description += `<br> Employee`+ ` <b>${empName}</b> and Task(s) <b>${taskDescription}\n</b>.`;
      });
  }else{
    var taskIds = this.taskSelection.selected.map((data: AssignedTQVM) => {
      return data.taskId;
    })
    taskIds = [...new Set(taskIds)];
    taskIds.forEach(async (id: any) => {
      var taskNumber = this.taskSelection.selected.find((tqData) => {
        return tqData.taskId === id;
      }).taskNumber;

      var emps = this.taskSelection.selected.filter((tq: AssignedTQVM) => {
        return tq.taskId === id;
      }).map((data) => {
        return data.empName;
      });

      emps = [...new Set(emps)];
      var emp = "";
      emps.forEach((data: AssignedTQVM) => {
        emp += data + ", ";
      });
      emp = emp.substring(0, emp.length - 2);
      this.description += `<br> Task`+ ` <b>${taskNumber}</b> and Employee(s) <b>${emp}\n</b>.`;
    });
  }
    this.description = await this.dynamicLabelPipe.transform(this.description);
    const dialogRef = this.dialog.open(templateRef, {
      width: '1000px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openReassignDialog(templateRef: any) {
    const dialogRef = this.dialog.open(templateRef, {
      width: '1000px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  checkEmployee(event: any, data: AssignedTQVM) {
    if (event.checked) {
      data.checked = true;
      this.taskSelection.select(data);
    }
    else {
      data.checked = false;
      this.taskSelection.deselect(data);
    }

    this.checkParent(data.parent);
  }

  inputTaskSearch(e:any){
    const searchTerm = e.target?.value
    this.searchTask = searchTerm;
    this.assignedTaskSource = this.originalAssignedTaskSource.filter(x=>x.taskDescription?.toLowerCase().includes(this.searchTask?.toLowerCase()));
  }
}

export class AssignedTQVM {
  tqId?: any;
  taskId?: any;
  empId?: any;
  empName?: string;
  taskDescription?: string;
  taskNumber?: string;
  dueDate?:Date;
  releaseDate?:Date;
  status:string;
  empImage?: string;
  empEmail?: string;
  checked: boolean = false;
  indeterminate = false;
  children: AssignedTQVM[] = [];
  parent!: AssignedTQVM;
}
