import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EvaluationMethod } from 'src/app/_DtoModels/EvaluationMethod/EvaluationMethod';
import { Person } from 'src/app/_DtoModels/Person/Person';
import { TaskQualification } from 'src/app/_DtoModels/TaskQualification/TaskQualification';
import { TaskQualificationCreateOptions } from 'src/app/_DtoModels/TaskQualification/TaskQualificationCreateOptions';
import { TaskQualificationEmpVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationEmpVM';
import { TaskQualificationWithEvalsVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationWithEvalsVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { EvaluationMethodService } from 'src/app/_Services/QTD/evaluation-method.service';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-add-task-qualification',
  templateUrl: './flypanel-add-task-qualification.component.html',
  styleUrls: ['./flypanel-add-task-qualification.component.scss']
})
export class FlypanelAddTaskQualificationComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() mode: 'add' | 'edit' | 'special' = 'add';
  @Input() data!: TaskQualificationEmpVM;

  taskQualificationData!: TaskQualificationWithEvalsVM;
  qualificationForm = new UntypedFormGroup({});
  spinner = false;
  evaluationMethods: EvaluationMethod[] = [];
  datePipe = new DatePipe('en-us');
  initialValues: any = {};
  evaluators: Employee[] = [];

  constructor(
    private evaluationService: EvaluationMethodService,
    private taskQualService: TaskRequalificationService,
    private alert: SweetAlertService,
    private employeeService: EmployeesService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.readyForm();
    this.readyEvaluationMethods();
    if (this.mode === 'add' || this.mode === 'special') {
      this.readyEvaluators()
    }
  }

  readyForm() {
    this.qualificationForm.addControl('name', new UntypedFormControl(this.data.empName));
    this.qualificationForm.addControl('number', new UntypedFormControl(this.data.taskNumber));
    this.qualificationForm.addControl('qualDate', new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")));
    this.qualificationForm.addControl('qualTime', new UntypedFormControl('00:00'));
    this.qualificationForm.addControl('evalMethod', new UntypedFormControl(''));
    this.qualificationForm.addControl('criteria', new UntypedFormControl('', Validators.required));
    this.qualificationForm.addControl('dueDate', new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), Validators.required));
    this.qualificationForm.addControl('comments', new UntypedFormControl(''));
    this.qualificationForm.addControl('another', new UntypedFormControl(false));
    this.qualificationForm.addControl('evaluatorId', new UntypedFormControl([]));
    this.initialValues = Object.assign(this.qualificationForm.value)
    if (this.mode === 'edit') {
      this.insertData();
    }
  }

  async insertData() {
    if (this.data.id) {
      this.taskQualificationData = await this.taskQualService.get(this.data.id);
      var taskQualificationDate = this.taskQualificationData.taskQualification.taskQualificationDate;
      var taskQualDate;
      if(taskQualificationDate != null){
        var qualDate = new Date(this.taskQualificationData.taskQualification.taskQualificationDate + "Z");
        taskQualDate = new Date(qualDate).toLocaleString();
      }else{
        taskQualDate = "";
      }
      this.taskQualificationData.taskQualification.taskQualificationDate = taskQualDate
      this.qualificationForm.patchValue({
        evalMethod: this.taskQualificationData.taskQualification.evaluationId ?? null,
        criteria: this.taskQualificationData.taskQualification.criteriaMet,
        dueDate: this.datePipe.transform(this.taskQualificationData.taskQualification.dueDate, 'yyyy-MM-dd'),
        comments: this.taskQualificationData.taskQualification.comments,
        qualDate: this.formatDateToYYYYMMDD(this.taskQualificationData.taskQualification.taskQualificationDate),
        qualTime:this.formatTimeToHHMMSS(this.taskQualificationData.taskQualification.taskQualificationDate),
      });
      if (this.mode === 'edit') {
        this.readyEvaluators();
      }
    }
    else {
      this.mode = 'special';
    }
  }

  formatDateToYYYYMMDD(date) {
    if (!date) return null;

    let parsedDate = new Date(date);
    let year = parsedDate.getFullYear();
    let month = ("0" + (parsedDate.getMonth() + 1)).slice(-2);
    let day = ("0" + parsedDate.getDate()).slice(-2);

    return `${year}-${month}-${day}`;
}

  formatTimeToHHMMSS(date: string | Date): string | null {
    if (!date) return null;

    const parsedDate = new Date(date);
    if (isNaN(parsedDate.getTime())) return null;
    const hours = ("0" + parsedDate.getHours()).slice(-2);
    const minutes = ("0" + parsedDate.getMinutes()).slice(-2);
    const seconds = ("0" + parsedDate.getSeconds()).slice(-2);
    return `${hours}:${minutes}:${seconds}`;
  }

  async readyEvaluationMethods() {
    this.evaluationMethods = await this.evaluationService.getAll();
  }

  async readyEvaluators() {
    this.evaluators = await this.employeeService.getAllEvaluators();
    if (this.mode === 'edit') {
      var ids = this.taskQualificationData.evaluators.map((data) => {
        return data.id;
      })
      var values = Object.assign(this.evaluators.filter((eval1) => {
        return ids.includes(eval1.id);
      }))

      this.qualificationForm.get('evaluatorId')?.setValue(values);
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async addQualification() {
    this.spinner = true;
    var options = new TaskQualificationCreateOptions();
    options.empId = this.data.empId;
    options.taskId = this.data.taskId;
    options.comments = this.qualificationForm.get('comments')?.value;
    options.dueDate = this.qualificationForm.get('dueDate')?.value;
    options.criteriaMet = this.qualificationForm.get('criteria')?.value;
    options.evaluationId = this.qualificationForm.get('evalMethod')?.value === null || this.qualificationForm.get('evalMethod')?.value === '' ? null : this.qualificationForm.get('evalMethod')?.value;
    options.taskQualificationEvaluator = '';
    const qualDate = this.qualificationForm.get('qualDate')?.value;
    const qualTime = this.qualificationForm.get('qualTime')?.value; 
    const startDateTIme = new Date(`${qualDate}T${qualTime}`);
    options.taskQualificationDate = startDateTIme;
    var evals = this.qualificationForm.get('evaluatorId')?.value;
    if (evals) {
      options.evaluatorIds = evals.map((data) => {
        return data.id;
      });
    }
    await this.taskQualService.create(options).then(async (res: TaskQualification) => {
      this.alert.successToast(await this.transformTitle('Task') +` Qualification ${this.mode === 'special' ? 'Updated' : 'Added'}`);
      var close = this.qualificationForm.get('another')?.value;
      if (close) {
        
        this.qualificationForm.reset(this.initialValues);
      }
      this.refresh.emit({ close: !close });
    }).finally(() => {
      this.spinner = false;
    })
  }


  async updateData() {
    this.spinner = true;
    var options = new TaskQualificationCreateOptions();
    options.empId = this.data.empId;
    options.taskId = this.data.taskId;
    options.comments = this.qualificationForm.get('comments')?.value;
    options.dueDate = this.qualificationForm.get('dueDate')?.value;
    options.criteriaMet = this.qualificationForm.get('criteria')?.value;
    options.evaluationId = this.qualificationForm.get('evalMethod')?.value === null || this.qualificationForm.get('evalMethod')?.value === '' ? null : this.qualificationForm.get('evalMethod')?.value;
    options.taskQualificationEvaluator = this.qualificationForm.get('evaluator')?.value;
    const qualDate = this.qualificationForm.get('qualDate')?.value;
    const qualTime = this.qualificationForm.get('qualTime')?.value; 
    const startDateTIme = new Date(`${qualDate}T${qualTime}`);
    options.taskQualificationDate = startDateTIme;
    var evals = this.qualificationForm.get('evaluatorId')?.value;
    if (evals) {
      options.evaluatorIds = evals.map((data) => {
        return data.id;
      });
    }
    await this.taskQualService.update(this.taskQualificationData.taskQualification.id, options).then(async (res: any) => {
      this.alert.successToast(await this.transformTitle('Task') +" Qualification Updated");
      this.refresh.emit({ close: true });
    }).finally(() => {
      this.spinner = false;
    })
  }

  removeData(data: Person) {
    var values = this.qualificationForm.get('evaluatorId')?.value.filter((per) => {
      return per.id !== data.id;
    })
    this.qualificationForm.get('evaluatorId')?.setValue(values)
  }

  taskQualificationDateChanged(e: Event){
    var newValue = (e.target as HTMLInputElement).value;
    if(!newValue){
      this.qualificationForm.patchValue({
        qualTime:null
      });
    } else if(!this.qualificationForm.get('qualTime')?.value){
      this.qualificationForm.patchValue({
        qualTime:"00:00"
      });
    }
  }
}
