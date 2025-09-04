import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { Person } from 'src/app/_DtoModels/Person/Person';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { AssignedTQVM } from '../flypanel-assigned-task-qualification/flypanel-assigned-task-qualification.component';
import { TQEvaluatorWithCount } from 'src/app/_DtoModels/TaskQualification/TQEvaluatorWithCount';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { ReassignTQVM } from 'src/app/_DtoModels/TaskQualification/ReassignTQVM';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'app-task-requal-reassign-eval-dialog',
  templateUrl: './task-requal-reassign-eval-dialog.component.html',
  styleUrls: ['./task-requal-reassign-eval-dialog.component.scss']
})
export class TaskRequalReassignEvalDialogComponent implements OnInit {
  @Input() selectedData!: any[];
  @Input() evalId!: any;
  @Input() selectedView: any;

  @Output() canceled = new EventEmitter<any>();
  @Output() saved = new EventEmitter<any>();

  description = "";

  saveSpinner = false;

  qualificationForm = new UntypedFormGroup({});
  evaluators: Employee[] = [];

  constructor(
    private empService: EmployeesService,
    private tqService: TaskRequalificationService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
    private dataBroadcastService : DataBroadcastService,
    private dynamicLabelPipe: DynamicLabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.readyForm();
    this.readyEvaluators();
    this.readyDescription();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  readyForm() {
    this.qualificationForm.addControl('evaluatorId', new UntypedFormControl('', [Validators.required]));
    this.qualificationForm.addControl('reassign', new UntypedFormControl(false));
  }

  async readyDescription() {
    var myData: any[] = [];
    this.description = `To Reassign ` + await this.labelPipe.transform('Employee') + `(s):<br><br>`
    if(this.selectedView=='emp'){
      var empIds = this.selectedData.map((data: AssignedTQVM) => {
        return data.empId;
      })
      empIds = [...new Set(empIds)];
      empIds.forEach(async (id: any) => {
        var empName = this.selectedData.find((tqData) => {
          return tqData.empId === id;
        }).empName;

        var tasks = this.selectedData.filter((tq: AssignedTQVM) => {
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

        this.description +=  ` Employee <b>${empName}</b> and Tasks <b>${taskDescription}</b><br>`;
      });
      this.description = this.description.substring(0, this.description.length - 4)
    }else{
      var taskIds = this.selectedData.map((data: AssignedTQVM) => {
        return data.taskId;
      })
      taskIds = [...new Set(taskIds)];
      taskIds.forEach(async (id: any) => {
        var taskNumber = this.selectedData.find((tqData) => {
          return tqData.taskId === id;
        }).taskNumber;
  
        var emps = this.selectedData.filter((tq: AssignedTQVM) => {
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
        this.description += `<br> Task`+ ` <b>${taskNumber}</b> and Employee(s) <b>${emp}</b>.`;
       
      });
    }
    
    this.description += `<br><br>To a different evaluator select the Evaluators name from the dropdown or select Do Not Reassign at this time to leave the Evaluator blank.<br>`
    this.description = await this.dynamicLabelPipe.transform(this.description);
  }

  async readyEvaluators() {
    this.evaluators = await this.empService.getAllEvaluators();
  }

  validate(event: any) {
    if (event.checked) {
      this.qualificationForm.get('evaluatorId')?.setValidators(null);
    }
    else {
      this.qualificationForm.get('evaluatorId')?.setValidators([Validators.required]);
    }
    this.qualificationForm.get('evaluatorId')?.updateValueAndValidity();
  }

  removeData(data: Person) {
    var values = this.qualificationForm.get('evaluatorId')?.value.filter((per: Employee) => {
      return per.id !== data.id;
    })
    this.qualificationForm.get('evaluatorId')?.setValue(values)
  }

  async emitData() {
    this.saveSpinner = false;
    var tqIds = this.selectedData.map((data: AssignedTQVM) => {
      return data.tqId;
    })

    tqIds = [...new Set(tqIds)];
    var options = new ReassignTQVM();
    options.evaluatorId = this.evalId;
    options.tqIds = tqIds;
    if (!this.qualificationForm.get('reassign')?.value) {
      options.reassignEvalIds = this.qualificationForm.get('evaluatorId')?.value.map((data: Employee) => data.id)
    }
    await this.tqService.reassignEvaluator(options)
    this.alert.successToast(await this.transformTitle('Task') +" Qualifications Reassigned");
    this.dataBroadcastService.refreshTQStats.next(null);
    this.saveSpinner = true;
    this.saved.emit();
  }

}
