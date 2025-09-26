import { SelectionModel } from '@angular/cdk/collections';
import { Component, Input, OnInit, isDevMode } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EmpSettingsReleaseTypeVM } from '@models/EmpSettingsReleaseType/EmpSettingsReleaseTypeVM';
import { Store } from '@ngrx/store';
import { de } from 'date-fns/locale';
import { EMPSettingTestReleaseCreationOptions } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { ApiClassScheduleTestReleaseSettingService } from 'src/app/_Services/QTD/ClassScheduleTestReleaseSettings/api.classScheduleTestReleaseSetting.service';
import { EmpSettingsReleaseTypeService } from 'src/app/_Services/QTD/empSettingsReleaseType.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-ila-emp-test-settings',
  templateUrl: './ila-emp-test-settings.component.html',
  styleUrls: ['./ila-emp-test-settings.component.scss']
})
export class IlaEmpTestSettingsComponent implements OnInit {
  @Input() ilaId = "";
  @Input() classScheduleId;
  testAvailabiilityTime: any[];
  testSpinner = false;
  empSettingsReleaseTypes : EmpSettingsReleaseTypeVM[];
  defaultEmpSettingReleaseTypeId : string = "";
  isComingFromClassSchedule:boolean;
  @Input() mode: string;

  constructor(
    private saveStore: Store<{ saveIla: any }>,
    private ilaService: IlaService,
    private alert: SweetAlertService,
    private testService: TestsService,
    private router: Router,
    private empSettingsReleaseTypeService :EmpSettingsReleaseTypeService,
    private classEmpTestReleaseService:ApiClassScheduleTestReleaseSettingService
  ) { }

  ngOnInit(): void {
    if(this.classScheduleId != null){
      this.isComingFromClassSchedule = true;
    }else{
      this.isComingFromClassSchedule = false;
    }
    if (this.mode === 'view') {
      this.trainingForm.disable({ emitEvent: false });
    } else {
      this.trainingForm.enable({ emitEvent: false });
    }
    this.testAvailabiilityTime = [
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

    this.defaultFormValues = this.trainingForm.value;

    // this.saveStore.select('saveIla').pipe().subscribe((res) => {
    //   if (res['saveData']['result'] !== undefined && res['tabIndex'] === 1) {
    //     this.ilaId = res['saveData']['result']['id'];
    //
    //   }
    // })
    this.empSettingsReleaseTypeService.getEmpSettingsReleaseTypes().then(res=>{
      this.empSettingsReleaseTypes = res;
      this.defaultEmpSettingReleaseTypeId = this.empSettingsReleaseTypes.find(x=> x.typeName == "Days")?.typeId;
      this.defaultFormValues.empSettingsReleaseType =this.defaultEmpSettingReleaseTypeId;
    })
    this.loadAsync();
  }

  async loadAsync(){
    this.testSpinner = true;
    await this.getTestLinkedILAs(this.ilaId);
    if(this.isComingFromClassSchedule){
      await this.getTestReleaseSettingByClass();
    }else{
      await this.getTestReleaseByILAId();
   }
  }

  defaultFormValues!: any;
  TestLinkedILAsPreTest_List: any[] = [];
  TestLinkedILAsReTakeTest_List: any[] = [];
  numberOfTimeRetake: any[] = [];
  TestLinkedILAsTest_List: any[] = [];
  numberOfDays: any[] = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31];
  updateMode: boolean
  selectedRetakeId: any[] = [];
  numberOfWeeks: any[] = [1, 2, 3, 4];
  weekOrDays: any[] = ["Days", "Weeks"];

  pretestSettingSelection = new SelectionModel(false);
  testSettingSelectionModel = new SelectionModel(false);


  trainingForm: UntypedFormGroup = new UntypedFormGroup({

    // /// EMP Test Release

    setAvailabilityTimeTest: new UntypedFormControl(null),
    finalTestId: new UntypedFormControl(null),
    preTestId: new UntypedFormControl(null),
    usePreTestAndTest: new UntypedFormControl(false),
    preTestRequired: new UntypedFormControl(false),
    preTestAvailableOnEnrollment: new UntypedFormControl(false),
    preTestAvailableOneStartDate: new UntypedFormControl(false),
    showStudentSubmittedPreTestAnswers: new UntypedFormControl(false),
    showCorrectIncorrectPreTestAnswers: new UntypedFormControl(false),
    makeAvailableBeforeDays: new UntypedFormControl(0),
    finalTestPassingScore: new UntypedFormControl('', Validators.required),
    makeFinalTestAvailableImmediatelyAfterStartDate: new UntypedFormControl(false),
    makeFinalTestAvailableOnClassEndDate: new UntypedFormControl(false),
    makeFinalTestAvailableAfterCBTCompleted: new UntypedFormControl(false),
    makeFinalTestAvailableOnSpecificTime: new UntypedFormControl(false),
    finalTestDueDate: new UntypedFormControl(1, Validators.required),
    empSettingsReleaseType: new UntypedFormControl("", Validators.required),
    showStudentSubmittedFinalTestAnswers: new UntypedFormControl(false),
    showStudentSubmittedRetakeTestAnswers: new UntypedFormControl(false),
    showCorrectIncorrectFinalTestAnswers: new UntypedFormControl(false),
    showCorrectIncorrectRetakeTestAnswers: new UntypedFormControl(false),
    autoReleaseRetake: new UntypedFormControl(false),
    retakeEnabled: new UntypedFormControl(false),
    numberOfRetakes: new UntypedFormControl(0),
    preTestScore: new UntypedFormControl(0),
    finalTestSpecificTimePrior: new UntypedFormControl(false),
    testPK: new UntypedFormControl(null),
    days: new UntypedFormControl(0),
    weeks: new UntypedFormControl(0)
  });

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

  async getTestReleaseByILAId() {
    this.testSpinner = true;
    await this.ilaService.getTestRelease(this.ilaId).then((res: any) => {

      if (res !== null) {
        this.updateFormValuse(res);
        this.updateMode = true;
      }
      else {
        this.updateMode = false;
        this.trainingForm.patchValue({
          empSettingsReleaseType : this.defaultEmpSettingReleaseTypeId
        });
      }

    }).finally(() => {
      this.testSpinner = false;
    })
  }

  option: any;
  onOptionChange(event: any) {
    this.option = event.target.value;

  }

  updateFormValuse(values: any) {
    this.trainingForm.patchValue({

      // /// EMP Test Release
      finalTestId: values.finalTestId,
      preTestId: values.preTestId,
      usePreTestAndTest: values.usePreTestAndTest,
      preTestRequired: values.preTestRequired,
      preTestAvailableOnEnrollment: values.preTestAvailableOnEnrollment,
      preTestAvailableOneStartDate: values.preTestAvailableOneStartDate,
      showStudentSubmittedPreTestAnswers: values.showStudentSubmittedPreTestAnswers,
      showCorrectIncorrectPreTestAnswers: values.showCorrectIncorrectPreTestAnswers,
      makeAvailableBeforeDays: values.daysOrWeeks===1?"Days":values.daysOrWeeks===2?"Weeks":0,
      weeks: values.makeAvailableBeforeWeeks,
      days: values.makeAvailableBeforeDays,
      finalTestPassingScore: values.finalTestPassingScore,
      finalTestSpecificTimePrior: values.finalTestSpecificTimePrior,
      makeFinalTestAvailableImmediatelyAfterStartDate: values.makeFinalTestAvailableImmediatelyAfterStartDate,
      makeFinalTestAvailableOnClassEndDate: values.makeFinalTestAvailableOnClassEndDate,
      makeFinalTestAvailableAfterCBTCompleted: values.makeFinalTestAvailableAfterCBTCompleted,
      makeFinalTestAvailableOnSpecificTime: values.makeFinalTestAvailableOnSpecificTime === null ? false : true,
      finalTestDueDate: values.finalTestDueDate,
      empSettingsReleaseType : values.empSettingsReleaseTypeId ?? this.defaultEmpSettingReleaseTypeId, 
      showStudentSubmittedFinalTestAnswers: values.showStudentSubmittedFinalTestAnswers,
      showStudentSubmittedRetakeTestAnswers: values.showStudentSubmittedRetakeTestAnswers,
      showCorrectIncorrectFinalTestAnswers: values.showCorrectIncorrectFinalTestAnswers,
      showCorrectIncorrectRetakeTestAnswers: values.showCorrectIncorrectRetakeTestAnswers,
      autoReleaseRetake: values.autoReleaseRetake,
      retakeEnabled: values.retakeEnabled,
      numberOfRetakes:values.numberOfRetakes,
      preTestScore: values.preTestScore,
      testPK: values.id
    });

    this.numberOfTimeRetake = [];
    this.selectedRetakeId = [];
    if (values.retakeEnabled === true && (values.numberOfRetakes === values.testReleaseEMPSetting_Retake_Links.length)) {
      for (let i = 0; i < values.numberOfRetakes; i++) {
        this.numberOfTimeRetake.push(i);
        this.selectedRetakeId.push(values.testReleaseEMPSetting_Retake_Links[i].retakeTestId);
      }
    }

    if (values.preTestRequired) {
      if (values.preTestAvailableOnEnrollment) {
        this.pretestSettingSelection.select('preTestAvailableOnEnrollment')
      }
      else {
        this.pretestSettingSelection.select('preTestAvailableOneStartDate');
      }
    }

    if (!values.usePreTestAndTest) {
      if (values.makeFinalTestAvailableImmediatelyAfterStartDate) {
        this.testSettingSelectionModel.select('makeFinalTestAvailableImmediatelyAfterStartDate');
      }
      else if (values.makeFinalTestAvailableOnClassEndDate) {
        this.testSettingSelectionModel.select('makeFinalTestAvailableOnClassEndDate');
      }
      else if (values.makeFinalTestAvailableAfterCBTCompleted) {
        this.testSettingSelectionModel.select('makeFinalTestAvailableAfterCBTCompleted');
      }
      else {
        this.testSettingSelectionModel.select('makeFinalTestAvailableOnSpecificTime');
      }
    }
    let setValue = this.testAvailabiilityTime.find(x => x.time === values.makeFinalTestAvailableOnSpecificTime && x.prior === values.finalTestSpecificTimePrior)
    this.trainingForm.controls['setAvailabilityTimeTest'].setValue(setValue);

    //this.usePreTestChanged({checked:values.usePreTestAndTest});
  }

  async saveInfoTestRelease() {
    let daysOrweeks=this.trainingForm.get('makeAvailableBeforeDays')?.value;
    daysOrweeks=daysOrweeks==='Days'?1:daysOrweeks==='Weeks'?2:0;
    let createOpt: EMPSettingTestReleaseCreationOptions = {
      ilaId: this.ilaId,
      autoReleaseRetake: this.trainingForm.get('autoReleaseRetake')?.value,
      finalTestDueDate: this.trainingForm.get('finalTestDueDate')?.value,
      empSettingsReleaseTypeId : this.trainingForm.get('empSettingsReleaseType')?.value,
      finalTestId: this.trainingForm.get('finalTestId')?.value === 0 ? null : this.trainingForm.get('finalTestId')?.value,
      finalTestPassingScore: this.trainingForm.get('finalTestPassingScore')?.value,
      finalTestSpecificTimePrior: this.trainingForm.get('makeFinalTestAvailableOnSpecificTime')?.value === false ? false : this.trainingForm.get('setAvailabilityTimeTest')?.value?.prior,
      //makeAvailableBeforeDays: this.trainingForm.get('makeAvailableBeforeDays')?.value,
      daysOrWeeks: daysOrweeks,
      makeAvailableBeforeDays:this.trainingForm.get('days')?.value,
      makeAvailableBeforeWeeks:this.trainingForm.get('weeks')?.value,
      makeFinalTestAvailableAfterCBTCompleted: this.trainingForm.get('makeFinalTestAvailableAfterCBTCompleted')?.value,
      makeFinalTestAvailableImmediatelyAfterStartDate: this.trainingForm.get('makeFinalTestAvailableImmediatelyAfterStartDate')?.value,
      makeFinalTestAvailableOnClassEndDate: this.trainingForm.get('makeFinalTestAvailableOnClassEndDate')?.value,
      makeFinalTestAvailableOnSpecificTime: this.trainingForm.get('makeFinalTestAvailableOnSpecificTime')?.value === false ? "" : this.trainingForm.get('setAvailabilityTimeTest')?.value?.time,
      numberOfRetakes: this.trainingForm.get('numberOfRetakes')?.value === 0 ? null : this.trainingForm.get('numberOfRetakes')?.value,
      preTestAvailableOnEnrollment: this.trainingForm.get('preTestAvailableOnEnrollment')?.value,
      preTestAvailableOneStartDate: this.trainingForm.get('preTestAvailableOneStartDate')?.value,
      preTestId: this.trainingForm.get('preTestId')?.value === 0 ? null : this.trainingForm.get('preTestId')?.value,
      preTestRequired: this.trainingForm.get('preTestRequired')?.value,
      retakeEnabled: this.trainingForm.get('retakeEnabled')?.value === 0 ? null : this.trainingForm.get('retakeEnabled')?.value,
      retakesTestIds: this.selectedRetakeId,
      showCorrectIncorrectFinalTestAnswers: this.trainingForm.get('showCorrectIncorrectFinalTestAnswers')?.value,
      showCorrectIncorrectPreTestAnswers: this.trainingForm.get('showCorrectIncorrectPreTestAnswers')?.value,
      showCorrectIncorrectRetakeTestAnswers: this.trainingForm.get('showCorrectIncorrectRetakeTestAnswers')?.value,
      showStudentSubmittedFinalTestAnswers: this.trainingForm.get('showStudentSubmittedFinalTestAnswers')?.value,
      showStudentSubmittedPreTestAnswers: this.trainingForm.get('showStudentSubmittedPreTestAnswers')?.value,
      showStudentSubmittedRetakeTestAnswers: this.trainingForm.get('showStudentSubmittedRetakeTestAnswers')?.value,
      usePreTestAndTest: this.trainingForm.get('usePreTestAndTest')?.value,
      preTestScore: this.trainingForm.get('preTestScore')?.value
    };

    if (!this.trainingForm.get('retakeEnabled')?.value) {
      createOpt.retakesTestIds = [];
      createOpt.numberOfRetakes = 0;
    }

    if (this.updateMode) {
      if(this.isComingFromClassSchedule){
        this.classEmpTestReleaseService.updateTestReleaseSettings(this.classScheduleId,createOpt).then(()=>{
          this.alert.successToast(`Test Release has been updated `);
        }).catch((res:any)=>{
          this.alert.errorToast(res);
        })
      }else{
        this.ilaService.updateTestRelease(createOpt.ilaId, createOpt).then((res) => {
          //this.updateFormValuse(res);
          this.alert.successToast(`Test Release has been updated `);
        }).catch((res: any) => {
          this.alert.errorToast(res);
        })
      }
    }
    else {
      if(this.isComingFromClassSchedule){
        this.classEmpTestReleaseService.updateTestReleaseSettings(this.classScheduleId,createOpt).then(()=>{
          this.alert.successToast(`Test Release has been created `);
        }).catch((res:any)=>{
          this.alert.errorToast(res);
        })
      }else{
        this.ilaService.createTestRelease(createOpt).then((res) => {
          //this.updateFormValuse(res);
          this.updateMode = true;
          this.alert.successToast(`Test Release has been created `);
        }).catch((res: any) => {


          this.alert.errorToast(res);
        })
      }
    }

  }

  usePreTestChanged(event: any) {
    this.testSettingSelectionModel.clear();
    this.pretestSettingSelection.clear();
    this.trainingForm.reset(this.defaultFormValues);
    this.trainingForm.get('usePreTestAndTest')?.setValue(event.checked);
    if (event.checked) {
      this.trainingForm.get('finalTestId')?.setValue(null);
      this.trainingForm.get('finalTestId')?.clearValidators();
      this.trainingForm.get('finalTestId')?.setErrors({ required: false });

      this.trainingForm.get('finalTestPassingScore')?.setValue(0);
      this.trainingForm.get('finalTestPassingScore')?.clearValidators();
      this.trainingForm.get('finalTestPassingScore')?.setErrors({ required: false });

      this.trainingForm.get('finalTestDueDate')?.setValue(1);
      this.trainingForm.get('finalTestDueDate')?.clearValidators();
      this.trainingForm.get('finalTestDueDate')?.setErrors({ required: false });
    }
    else {
      this.trainingForm.get('finalTestId')?.setValue(null);
      this.trainingForm.get('finalTestId')?.setValidators([Validators.required]);
      this.trainingForm.get('finalTestId')?.setErrors({ required: true });
      this.trainingForm.get('finalTestPassingScore')?.setValidators([Validators.required]);
      this.trainingForm.get('finalTestPassingScore')?.setErrors({ required: true });
      this.trainingForm.get('finalTestDueDate')?.setValidators([Validators.required]);
      this.trainingForm.get('finalTestDueDate')?.setErrors({ required: true });
    }
    this.trainingForm.updateValueAndValidity();
  }

  async getTestLinkedILAs(id: any) {
    await this.testService.getTestLinkedtoILA(id).then((res: any) => {
      let temp: any[] = [];
      res.forEach((data) => {
        temp.push({
          id: data.id,
          testTitle: data.testTitle,
          testType: data.testType,
          testNum: Number(data.testNum),
          numOfQuestion: data.numberOfQuestions,
          testStatus: data.testStatus,
          active: data.active,
          isPublished: data.isPublished,
        });
      })
      this.TestLinkedILAsReTakeTest_List = temp.filter(x => x.testType === 'Retake');
      this.TestLinkedILAsPreTest_List = temp.filter(x => x.testType === 'Pretest');
      this.TestLinkedILAsTest_List = temp.filter(x => x.testType === 'Final Test');
    });
  }

  getTestReTakeId(event, index: any) {
    this.selectedRetakeId[index] = event;
  }
  updateReTake(item) {

    return 'Retake ' + (item + 1);
  }

  valueChanged() {
    this.numberOfTimeRetake = [];
    var numbers = this.trainingForm.get('numberOfRetakes')?.value;
    for (let i = 0; i < numbers; i++) {
      this.numberOfTimeRetake.push(i);
    }
    if (this.numberOfTimeRetake.length < this.selectedRetakeId.length) {
      this.selectedRetakeId.splice(this.numberOfTimeRetake.length, this.selectedRetakeId.length - this.numberOfTimeRetake.length);
    }
  }

  keyPressNumbersRetake(event: any) {
    //this.numberOfTimeRetake = Array.from({ length: event.key }, (x, i) => i);
    
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 51)) {
      event.preventDefault();
      return false;
    } else {
      if (Number.parseInt(((this.trainingForm.get("numberOfRetakes").value ?? 0) + String.fromCharCode(event.keyCode))) <= 3) {

        return true;
      }
      else {
        event.preventDefault();
        return false;
      }
    }
  }

  preTestRequiredChangeed(event: any) {
    if (event.checked === false) {
      this.pretestSettingSelection.clear();
      this.trainingForm.get("preTestId")?.clearValidators();
      this.trainingForm.get("preTestId")?.setErrors({ required: false });
      this.trainingForm.get("preTestId")?.setValue(null);
      this.trainingForm.get("preTestAvailableOnEnrollment")?.setValue(false);
      this.trainingForm.get("preTestAvailableOneStartDate")?.setValue(false);

      this.trainingForm.get("makeAvailableBeforeDays")?.setValue(0);
      this.trainingForm.get("days")?.setValue(0);
      this.trainingForm.get("weeks")?.setValue(0)
      this.trainingForm.get("showStudentSubmittedPreTestAnswers")?.setValue(false);
      this.trainingForm.get("showCorrectIncorrectPreTestAnswers")?.setValue(false);
      this.trainingForm.get('preTestScore')?.clearValidators();
      this.trainingForm.get('preTestScore')?.setErrors({ required: false });
      this.trainingForm.get("preTestScore")?.setValue(0);
    } else {
      this.trainingForm.get("preTestId")?.setValidators([Validators.required]);
      this.trainingForm.get("preTestId")?.setErrors({ required: true });
      this.trainingForm.get('preTestScore')?.setValidators([Validators.required]);
      this.trainingForm.get('preTestScore')?.setErrors({ required: true });
    }
    this.trainingForm.updateValueAndValidity();
  }

  toggleEnrollmentOption(val: any, formName: any) {
    if (val === true)
      this.trainingForm.get(formName)?.setValue(false);

    if ((val === false && formName === "preTestAvailableOnEnrollment") || (val === true && formName === "preTestAvailableOneStartDate"))
      this.trainingForm.get("makeAvailableBeforeDays")?.setValue(0);
    this.trainingForm.get("days")?.setValue(0);
    this.trainingForm.get("weeks")?.setValue(0);

  }

  pretestToggle(event: any, name: any) {

    if (event.checked) {
      this.pretestSettingSelection.select(name);
      switch (name) {
        case 'preTestAvailableOnEnrollment':
          this.trainingForm.get('makeAvailableBeforeDays')?.clearValidators();
          this.trainingForm.get('makeAvailableBeforeDays')?.setErrors({ required: false });
          this.trainingForm.get('makeAvailableBeforeDays')?.setValue(0);
          this.trainingForm.get('preTestAvailableOneStartDate')?.setValue(false);
          break;
        case 'preTestAvailableOneStartDate':
          this.trainingForm.get('makeAvailableBeforeDays')?.setValidators([Validators.required]);
          this.trainingForm.get('makeAvailableBeforeDays')?.setErrors({ required: true });
          this.trainingForm.get('preTestAvailableOnEnrollment')?.setValue(false);
          break;
      }
    }
    else {
      this.pretestSettingSelection.clear();
      this.trainingForm.get('makeAvailableBeforeDays')?.clearValidators();
      this.trainingForm.get('makeAvailableBeforeDays')?.setErrors({ required: false });
      this.trainingForm.get('makeAvailableBeforeDays')?.setValue(0);
    }
    this.trainingForm.updateValueAndValidity();
  }

  toggleFinalTest(event: any, formName: any) {
    if (event.checked) {
      this.testSettingSelectionModel.select(formName);
    }
    else {
      this.testSettingSelectionModel.clear();
    }
    this.trainingForm.get("makeFinalTestAvailableImmediatelyAfterStartDate")?.setValue(false);
    this.trainingForm.get("makeFinalTestAvailableOnClassEndDate")?.setValue(false);
    this.trainingForm.get("makeFinalTestAvailableOnSpecificTime")?.setValue(false);
    this.trainingForm.get("makeFinalTestAvailableAfterCBTCompleted")?.setValue(false);
    this.trainingForm.get(formName)?.setValue(event.checked);
    switch (formName) {
      case 'makeFinalTestAvailableOnSpecificTime':
        if (event.checked) {
          this.trainingForm.get('setAvailabilityTimeTest')?.setValue(null);
          this.trainingForm.get('setAvailabilityTimeTest')?.setValidators([Validators.required]);
          this.trainingForm.get('setAvailabilityTimeTest')?.setErrors({ required: true });
          this.trainingForm.get('setAvailabilityTimeTest')?.updateValueAndValidity();
        }
        else {
          this.trainingForm.get('setAvailabilityTimeTest')?.setValue(null);
          this.trainingForm.get('setAvailabilityTimeTest')?.clearValidators();
          this.trainingForm.get('setAvailabilityTimeTest')?.setErrors({ required: false });
          this.trainingForm.get('setAvailabilityTimeTest')?.updateValueAndValidity();
        }
        break;
      default:
        this.trainingForm.get('setAvailabilityTimeTest')?.setValue(null);
        this.trainingForm.get('setAvailabilityTimeTest')?.clearValidators();
        this.trainingForm.get('setAvailabilityTimeTest')?.setErrors({ required: false });
        this.trainingForm.get('setAvailabilityTimeTest')?.updateValueAndValidity();
        break;
    }
    this.trainingForm.updateValueAndValidity();
  }

  async getTestAvailabilityTime(event) {
    this.trainingForm.patchValue({
      makeFinalTestAvailableOnSpecificTime: event.value.time,
      finalTestSpecificTimePrior: event.value.prior,
    });
  }

  previewTest(type: string, idx: number = 0) {
    switch (type) {
      case 'test':
        var testId = this.trainingForm.get('finalTestId')?.value;
        var url = '';

        if (isDevMode) {
          url = `/dnd/tests/publish/${testId}/${false}`
        }
        else {
          url = `QTD2/App/dnd/tests/publish/${testId}/${false}`
        }
        window.open(url, '_blank');
        break;
      case 'retake':
        var testId = this.selectedRetakeId[idx];
        var url = `/dnd/tests/publish/${testId}/${false}`;
        window.open(url, '_blank');
        break;
      case 'pretest':
        var testId = this.trainingForm.get('preTestId')?.value;
        var url = `/dnd/tests/publish/${testId}/${false}`;
        window.open(url, '_blank');
        break;
    }
  }

  async getTestReleaseSettingByClass() {
    this.testSpinner = true;
    await this.classEmpTestReleaseService.getClassScheduleTestEmpSettings(this.classScheduleId).then((res: any) => {

      if (res !== null) {
        this.updateFormValuse(res);
        this.updateMode = true;
      }
      else {
        this.updateMode = false;
        this.trainingForm.patchValue({
          empSettingsReleaseType : this.defaultEmpSettingReleaseTypeId
        });
      }

    }).finally(() => {
      this.testSpinner = false;
    })
  }
}
