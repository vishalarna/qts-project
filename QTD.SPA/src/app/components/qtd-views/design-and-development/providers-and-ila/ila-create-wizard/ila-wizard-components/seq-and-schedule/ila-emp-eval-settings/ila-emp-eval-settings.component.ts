import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, Input, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { EmpSettingsReleaseTypeVM } from '@models/EmpSettingsReleaseType/EmpSettingsReleaseTypeVM';
import { Store } from '@ngrx/store';
import { EMPSettingEvaluationCreationOptions, EMPSettingStudentEval } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { EMPSettingStudentEvaluationCreationOption } from 'src/app/_DtoModels/StudentEvaluationQuestions/StudentEvaluation_Question_LinkCreateOptions';
import { EmpSettingsReleaseTypeService } from 'src/app/_Services/QTD/empSettingsReleaseType.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-ila-emp-eval-settings',
  templateUrl: './ila-emp-eval-settings.component.html',
  styleUrls: ['./ila-emp-eval-settings.component.scss']
})
export class IlaEmpEvalSettingsComponent implements OnInit, OnDestroy {
  student_Evaluation_List: any[] = [];

  dataSourceTQEval = new MatTableDataSource<any>();
  testAvailabiilityTime: any[] = [
    {
      timeSpan: '60 minutes prior to class end time',
      time: 60,
      prior: true,
    },
    {
      timeSpan: '30 minutes prior to class end time',
      time: 30,
      prior: true,
    },
    {
      timeSpan: '15 minutes prior to class end time',
      time: 15,
      prior: true,
    },
    {
      timeSpan: '15 minutes after class end time',
      time: 15,
      prior: false,
    },
    {
      timeSpan: '30 minutes after class end time',
      time: 30,
      prior: false,
    },
    {
      timeSpan: '60 minutes after class end time',
      time: 60,
      prior: false,
    },
  ];

  evalForm = new UntypedFormGroup({
    setAvailabilityTimeEvaluate: new UntypedFormControl(null),

    evaluationPK: new UntypedFormControl(''),
    evaluationRequired: new UntypedFormControl(false),
    evaluationUsedToDeployStudentEvaluation: new UntypedFormControl(false),
    evaluationAvailableOnStartDate: new UntypedFormControl(false),
    evaluationAvailableOnEndDate: new UntypedFormControl(false),
    finalGradeRequired: new UntypedFormControl(false),
    releaseOnSpecificTimeAfterClassEndDate: new UntypedFormControl(false),
    releaseAfterEndTime: new UntypedFormControl(false),
    evaluationDueDate:new UntypedFormControl(1,Validators.required),
    empSettingsReleaseType:new UntypedFormControl("",Validators.required),
    releasePrior: new UntypedFormControl(false),
    releaseAfterGradeAssigned: new UntypedFormControl(false),
  })

  @Input() ilaId = "";
  @Input() mode: string;
  evaluationReleaseCheckNUll: boolean = false;
  displayedColumnsStudentEvaluators: string[] = ['id', 'date'];
  classes_List: any[] = [];
  numberOfDays: number[] = [1,2,3,4,5,6,7,8,9,10];
  selectedStudentEvaluationId: number = 0;
  datePipe = new DatePipe('en-us');
  evalSelection = new SelectionModel(false);
  updatedEvalValues: any = {};
  evalValues: any = {};
  saveSpinner = false;
  evalReleaseSetting: any | null = null;
  subscription = new SubSink();
  empSettingsReleaseTypes : EmpSettingsReleaseTypeVM[];
  defaultEmpSettingReleaseTypeId : string = "";

  constructor(
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private ilaService: IlaService,
    private alert: SweetAlertService,
    private studentEvaluationService: StudentEvaluationService,
    private empSettingsReleaseTypeService :EmpSettingsReleaseTypeService,
    private store:Store<{saveIla:any}>,
  ) { }

  ngOnInit(): void {
    this.evalValues = this.evalForm.value;
    if (this.mode === 'view') {
      this.evalForm.disable({ emitEvent: false });
    }
    this.empSettingsReleaseTypeService.getEmpSettingsReleaseTypes().then(res=>{
      this.empSettingsReleaseTypes = res;
      this.defaultEmpSettingReleaseTypeId = this.empSettingsReleaseTypes.find(x=> x.typeName == "Days")?.typeId;
      this.evalForm.get('empSettingsReleaseType')?.setValue(this.defaultEmpSettingReleaseTypeId);
      this.checkEvalSettings();
    })
  }

  ngOnDestroy(): void{
  }

  async checkEvalSettings(){
    this.evalReleaseSetting = await this.ilaService.getTestEvaluation(this.ilaId);

    if(this.evalReleaseSetting !== null){
      this.patchEvalRelease(this.evalReleaseSetting);
    }
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  keyPressNumbers(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  async getEvalAvailabilityTime(event) {
    this.evalForm.patchValue({
      releaseAfterEndTime: event.value.time,
      releasePrior: event.value.prior,
    });
  }

  async saveInfoEvalRelease() {
    this.saveSpinner = true;
    let createOpt: EMPSettingEvaluationCreationOptions = {
      ilaId: this.ilaId,
      evaluationAvailableOnEndDate: this.evalForm.get('evaluationAvailableOnEndDate')?.value,
      evaluationAvailableOnStartDate: this.evalForm.get('evaluationAvailableOnStartDate')?.value,
      evaluationDueDate: this.evalForm.get('evaluationDueDate')?.value ?? 1,
      empSettingsReleaseTypeId: this.evalForm.get('empSettingsReleaseType')?.value,
      evaluationRequired: this.evalForm.get('evaluationRequired')?.value,
      evaluationUsedToDeployStudentEvaluation: this.evalForm.get('evaluationUsedToDeployStudentEvaluation')?.value,
      finalGradeRequired: this.evalForm.get('finalGradeRequired')?.value,
      releaseAfterEndTime: this.evalForm.get('setAvailabilityTimeEvaluate')?.value?.time,
      releaseAfterGradeAssigned: this.evalForm.get('releaseAfterGradeAssigned')?.value,
      releaseOnSpecificTimeAfterClassEndDate: this.evalForm.get('releaseOnSpecificTimeAfterClassEndDate')?.value,
      releasePrior: this.evalForm.get('setAvailabilityTimeEvaluate')?.value?.prior ?? false,
    };
    if (this.evalReleaseSetting !== null) {
      this.ilaService.updateTestEvaluation(this.evalForm.get('evaluationPK')?.value, createOpt).then((res) => {
        this.patchEvalRelease(res)
        this.alert.successToast(`Evaluation Release Setting updated `);
      }).finally(()=>{
        this.saveSpinner = false;
      })
    }
    else {
      this.ilaService.createTestEvaluation(createOpt).then((res) => {
        this.patchEvalRelease(res)
        this.alert.successToast(`Evaluation Release Setting created `);
      }).finally(()=>{
        this.saveSpinner = false;
      })
    }

  }

  async patchEvalRelease(values) {

    this.evalForm.patchValue({
      evaluationPK: values.id,
      evaluationRequired: values.evaluationRequired,
      evaluationUsedToDeployStudentEvaluation: values.evaluationUsedToDeployStudentEvaluation,
      evaluationAvailableOnStartDate: values.evaluationAvailableOnStartDate,
      evaluationAvailableOnEndDate: values.evaluationAvailableOnEndDate,
      finalGradeRequired: values.finalGradeRequired,
      releaseOnSpecificTimeAfterClassEndDate: values.releaseOnSpecificTimeAfterClassEndDate,
      releaseAfterEndTime: values.releaseAfterEndTime,
      ////
      releasePrior: values.releasePrior,
      releaseAfterGradeAssigned: values.releaseAfterGradeAssigned,
      evaluationDueDate: values.evaluationDueDate,
      empSettingsReleaseType: values.empSettingsReleaseTypeId ?? this.defaultEmpSettingReleaseTypeId,
    });

    let setValue = this.testAvailabiilityTime.find(x => x.time == this.evalForm.get('releaseAfterEndTime')?.value && x.prior == this.evalForm.get('releasePrior')?.value)
    this.evalForm.controls['setAvailabilityTimeEvaluate'].setValue(setValue);
    this.updatedEvalValues = this.evalForm.value;

    Object.keys(values).forEach((key)=>{
      if(this.evalForm.contains(key)){
        if(key !== 'releasePrior' && key !== 'evaluationRequired' && key !== 'evaluationUsedToDeployStudentEvaluation'){
          values[key] === true ? this.evalSelection.select(key):null;
        }
      }
    })
  }

  uncheckOthers(title: string, event: any) {
    this.updatedEvalValues = this.evalForm.value;
    if (event.checked) {
      this.evalSelection.select(title);
      this.evalForm.reset(this.evalValues);
      this.evalForm.patchValue({
        [title]:this.updatedEvalValues[title],
      });
      this.evalForm.updateValueAndValidity();
      switch(title){
        case "evaluationAvailableOnStartDate":
          this.evalForm.patchValue({
            finalGradeRequired:this.updatedEvalValues['finalGradeRequired'],
          })
          break;
        case "evaluationAvailableOnEndDate":
          this.evalForm.patchValue({
            finalGradeRequired:this.updatedEvalValues['finalGradeRequired'],
          })
          break;
      }
    }
    else{
      this.evalSelection.clear();
      this.evalForm.reset(this.evalValues);
      switch(title){
        case "evaluationAvailableOnStartDate":
          this.evalForm.patchValue({
            finalGradeRequired:false,
          })
          break;
        case "evaluationAvailableOnEndDate":
          this.evalForm.patchValue({
            finalGradeRequired:false,
          })
          break;
      }
    }

    this.evalForm.patchValue({
      evaluationDueDate:this.updatedEvalValues['evaluationDueDate'],
      empSettingsReleaseType:this.updatedEvalValues['empSettingsReleaseType'],
      evaluationPK:this.updatedEvalValues['evaluationPK'],
    });
  }
}
