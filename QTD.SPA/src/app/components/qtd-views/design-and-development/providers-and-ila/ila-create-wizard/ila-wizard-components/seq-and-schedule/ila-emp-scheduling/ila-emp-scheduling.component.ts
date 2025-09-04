import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { EMPSettingCBTCreationOptions } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ila-emp-scheduling',
  templateUrl: './ila-emp-scheduling.component.html',
  styleUrls: ['./ila-emp-scheduling.component.scss']
})
export class IlaEmpSchedulingComponent implements OnInit {
  @Input() ilaId = "";
  editor = ckcustomBuild;
  public Editor = ckcustomBuild;
  cbtReleaseCheckNUll: false;

  constructor(
    private ilaService: IlaService,
    private alert: SweetAlertService,
    private router:Router
  ) { }


  ngOnInit(): void {
  }
  trainingForm: UntypedFormGroup = new UntypedFormGroup({
    providerId: new UntypedFormControl('', Validators.required),
    ilaId: new UntypedFormControl('', Validators.required),
    locationId: new UntypedFormControl('', Validators.required),
    instructorId: new UntypedFormControl('', Validators.required),
    startDate: new UntypedFormControl('', Validators.required),
    startTime: new UntypedFormControl('', Validators.required),
    endDate: new UntypedFormControl('', Validators.required),
    endTime: new UntypedFormControl('', Validators.required),
    courseInstruction: new UntypedFormControl(''),
    webLink: new UntypedFormControl(''),

    // To Send Server
    startDateTime: new UntypedFormControl(''),
    endDateTime: new UntypedFormControl(''),

    /// self registration
    selfRegPk: new UntypedFormControl(0),
    makeAvailableForSelfReg: new UntypedFormControl(false),
    requireAdminApproval: new UntypedFormControl(false),
    acknowledgeRegDisclaimer: new UntypedFormControl(false),
    regDisclaimer: new UntypedFormControl(''),
    limitForLinkedPositions: new UntypedFormControl(false),
    closeRegOnStartDate: new UntypedFormControl(false),
    classSize: new UntypedFormControl(''),
    enableWaitlist: new UntypedFormControl(false),



    /// EMP Test Release

    setAvailabilityTimeTest: new UntypedFormControl(),

    testPK: new UntypedFormControl(0),
    finalTestId: new UntypedFormControl(0),
    preTestId: new UntypedFormControl(0),
    usePreTestAndTest: new UntypedFormControl(false),
    preTestRequired: new UntypedFormControl(false),
    preTestAvailableOnEnrollment: new UntypedFormControl(false),
    preTestAvailableOneStartDate: new UntypedFormControl(false),
    showStudentSubmittedPreTestAnswers: new UntypedFormControl(false),
    showCorrectIncorrectPreTestAnswers: new UntypedFormControl(false),
    makeAvailableBeforeDays: new UntypedFormControl(0),
    finalTestPassingScore: new UntypedFormControl(''),
    makeFinalTestAvailableImmediatelyAfterStartDate: new UntypedFormControl(false),
    makeFinalTestAvailableOnClassEndDate: new UntypedFormControl(false),
    makeFinalTestAvailableAfterCBTCompleted: new UntypedFormControl(false),
    makeFinalTestAvailableOnSpecificTime: new UntypedFormControl(0),
    finalTestSpecificTimePrior: new UntypedFormControl(false),
    finalTestDueDate: new UntypedFormControl(0),
    showStudentSubmittedFinalTestAnswers: new UntypedFormControl(false),
    showStudentSubmittedRetakeTestAnswers: new UntypedFormControl(false),
    showCorrectIncorrectFinalTestAnswers: new UntypedFormControl(false),
    showCorrectIncorrectRetakeTestAnswers: new UntypedFormControl(false),
    autoReleaseRetake: new UntypedFormControl(false),
    retakeEnabled: new UntypedFormControl(false),
    numberOfRetakes: new UntypedFormControl(0),
    preTestScore: new UntypedFormControl(0),
    //retakesTestIds: number[];




    // EMP CBT Release
    cbtPK: new UntypedFormControl(0),
    cbtRequiredForCource: new UntypedFormControl(false),
    releaseCBTLearningContract: new UntypedFormControl(false),
    cbtLearningContractInstructions: new UntypedFormControl(''),
    makeAvailableOnClassStartDate: new UntypedFormControl(false),
    makeAvailableOnClassEndDate: new UntypedFormControl(false),
    makeAvailableAfterPretestCompleted: new UntypedFormControl(false),
    cbtDueDate: new UntypedFormControl(0),





    // EMP Evaluation Release
    setAvailabilityTimeEvaluate: new UntypedFormControl(),

    evaluationPK: new UntypedFormControl(0),
    evaluationRequired: new UntypedFormControl(false),
    evaluationUsedToDeployStudentEvaluation: new UntypedFormControl(false),
    evaluationAvailableOnStartDate: new UntypedFormControl(false),
    evaluationAvailableOnEndDate: new UntypedFormControl(false),
    finalGradeRequired: new UntypedFormControl(false),
    releaseOnSpecificTimeAfterClassEndDate: new UntypedFormControl(false),
    releaseAfterEndTime: new UntypedFormControl(0),
    ////
    releasePrior: new UntypedFormControl(false),
    releaseAfterGradeAssigned: new UntypedFormControl(false),
    evaluationDueDate: new UntypedFormControl(0),


    // TQ
    setAvailabilityTimeTQ: new UntypedFormControl(),

    tqPK: new UntypedFormControl(0),
    tqRequired: new UntypedFormControl(false),
    releaseAtOnce: new UntypedFormControl(false),
    releaseOneAtTime: new UntypedFormControl(false),
    releaseOnClassStart: new UntypedFormControl(false),
    releaseOnClassEnd: new UntypedFormControl(false),
    specificTime: new UntypedFormControl(0),
    priorToSpecificTime: new UntypedFormControl(false),
    oneSignOffRequired: new UntypedFormControl(false),
    multipleSignOffRequired: new UntypedFormControl(0),
    tqDueDate: new UntypedFormControl(0),
  });

  async saveInfoCBTRelease() {
    let createOpt: EMPSettingCBTCreationOptions = {
      ilaId: this.trainingForm.get('ilaId')?.value,
      cbtDueDate: this.trainingForm.get('cbtDueDate')?.value,
      cbtLearningContractInstructions: this.trainingForm.get('cbtLearningContractInstructions')?.value,
      cbtRequiredForCource: this.trainingForm.get('cbtRequiredForCource')?.value,
      makeAvailableAfterPretestCompleted: this.trainingForm.get('makeAvailableAfterPretestCompleted')?.value,
      makeAvailableOnClassEndDate: this.trainingForm.get('makeAvailableOnClassEndDate')?.value,
      makeAvailableOnClassStartDate: this.trainingForm.get('makeAvailableOnClassStartDate')?.value,
      releaseCBTLearningContract: this.trainingForm.get('releaseCBTLearningContract')?.value

    };
    if (!this.cbtReleaseCheckNUll) {
      this.ilaService.updateIlaCBT(this.trainingForm.get('cbtPK')?.value, createOpt).then((res) => {
        this.patchCBTRelease(res);
        this.alert.successToast(`cbt has been updated `);
      }).catch((res: any) => {
        
        
        this.alert.errorToast(res);
      })
    }
    else {
      this.ilaService.createIlaCBT(createOpt).then((res) => {
        this.patchCBTRelease(res);
        this.alert.successToast(`Cbt has been created `);
      }).catch((res: any) => {
        
        
        this.alert.errorToast(res);
      })
    }

  }


  async patchCBTRelease(values) {
    this.trainingForm.patchValue({
      cbtPK: values.id,
      cbtRequiredForCource: values.cbtRequiredForCource,
      releaseCBTLearningContract: values.releaseCBTLearningContract,
      cbtLearningContractInstructions: values.cbtLearningContractInstructions,
      makeAvailableOnClassStartDate: values.makeAvailableOnClassStartDate,
      makeAvailableOnClassEndDate: values.makeAvailableOnClassEndDate,
      makeAvailableAfterPretestCompleted: values.makeAvailableAfterPretestCompleted,
      cbtDueDate: values.cbtDueDate,
    });
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

  onReady(editor: any) {
    // 
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }


  redirectTOSC(){

    this.router.navigate([`implementation/sc/addTraining`]);
    
    }
}
