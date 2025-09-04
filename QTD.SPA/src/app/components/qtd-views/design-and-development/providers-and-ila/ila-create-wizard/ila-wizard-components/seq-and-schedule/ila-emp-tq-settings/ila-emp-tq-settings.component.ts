import { SelectionModel } from '@angular/cdk/collections';
import { moveItemInArray } from '@angular/cdk/drag-drop';
import { TemplatePortal } from '@angular/cdk/portal';
import { ChangeDetectorRef, Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { EmpSettingsReleaseTypeVM } from '@models/EmpSettingsReleaseType/EmpSettingsReleaseTypeVM';
import { TQILAEmpSetting } from '@models/ILA/TQILAEmpSetting';
import { ILATaskObjectiveLinkUpdateOptions } from '@models/ILA_TaskObjective_Link/ILATaskObjectiveLinkUpdateOptions';
import { ClassScheduleTQEMPSettingsVM } from '@models/SchedulesClassses/ClassScheduleTQEMPSettingsVM';
import { ClassScheduleTQEMPSettingsCreateOptions, EMPSettingTQCreationOptions, EMPSettingsTQTaskEvaluation } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiClassScheduleTqReleaseSettingService } from 'src/app/_Services/QTD/ClassScheduleTestReleaseSettings/api.classScheduleTqReleaseSetting.service';
import { EmpSettingsReleaseTypeService } from 'src/app/_Services/QTD/empSettingsReleaseType.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-ila-emp-tq-settings',
  templateUrl: './ila-emp-tq-settings.component.html',
  styleUrls: ['./ila-emp-tq-settings.component.scss']
})
export class IlaEmpTqSettingsComponent implements OnInit {
  @Input() ilaId = "";
  @Input() classScheduleId;
  isReordered: boolean;
  spinner = false;
  dataSource = new MatTableDataSource<any>();
  displayedColumnsTask: string[] = ['order', 'tasknumber', 'desc'];
  dataSourceTaskEval = new MatTableDataSource<any>();
  displayedColumnsTaskEvaluators: string[] = ['name', 'position'];
  tqSelection = new SelectionModel<string>(true, []);
  qualInfo:any
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

  tqReleaseCheckNUll: boolean = false;
  numberOfDays: any[] = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
  dataSourceTask = new MatTableDataSource<any>();
  notSelectedTaskDataSource :MatTableDataSource<any> = new MatTableDataSource(); 
  tqPk:string;
  originalInitialValues: any;

  TQForm = new UntypedFormGroup({
    ilaId: new UntypedFormControl('', Validators.required),
    setAvailabilityTimeTQ: new UntypedFormControl(null),

    tqPK: new UntypedFormControl(null),
    tqRequired: new UntypedFormControl(false),
    releaseAtOnce: new UntypedFormControl(false),
    releaseOneAtTime: new UntypedFormControl(false),
    releaseOnClassStart: new UntypedFormControl(false),
    releaseOnClassEnd: new UntypedFormControl(false),
    specificTime: new UntypedFormControl(null),
    priorToSpecificTime: new UntypedFormControl(false),
    oneSignOffRequired: new UntypedFormControl(false),
    multipleSignOffRequired: new UntypedFormControl(0),
    multipleSignOffRequiredCheck: new UntypedFormControl(false),
    tqDueDate: new UntypedFormControl(1, Validators.required),
    empSettingsReleaseType: new UntypedFormControl("", Validators.required),
    suggestions: new UntypedFormControl(false),
    questions: new UntypedFormControl(false)
  })
  empSettingsReleaseTypes : EmpSettingsReleaseTypeVM[];
  defaultEmpSettingReleaseTypeId : string = "";
  isComingFromClassSchedule:boolean;
  tqILAReleaseSetting : TQILAEmpSetting = new TQILAEmpSetting();
  tqClassReleaseSetting : ClassScheduleTQEMPSettingsVM = new ClassScheduleTQEMPSettingsVM();
  constructor(
    public flyPanelService: FlyInPanelService,
    private ilaService: IlaService,
    private alert: SweetAlertService,
    private vcf: ViewContainerRef,
    private cdf: ChangeDetectorRef,
    private labelPipe: LabelReplacementPipe,
    private empSettingsReleaseTypeService :EmpSettingsReleaseTypeService,
    private classTQSettingService : ApiClassScheduleTqReleaseSettingService
  ) { }

  ngOnInit(): void {
    if(this.classScheduleId != null){
      this.isComingFromClassSchedule = true;
    }else{
      this.isComingFromClassSchedule = false;
    }
    this.empSettingsReleaseTypeService.getEmpSettingsReleaseTypes().then(res=>{
      this.empSettingsReleaseTypes = res;
      this.defaultEmpSettingReleaseTypeId = this.empSettingsReleaseTypes.find(x=> x.typeName == "Days")?.typeId;
      this.TQForm.get('empSettingsReleaseType')?.setValue(this.defaultEmpSettingReleaseTypeId);

      this.TQForm.get('ilaId')?.setValue(this.ilaId);
      this.TQForm.updateValueAndValidity();
      this.readyTQforILA();
      this.originalInitialValues = this.TQForm.value;
    });

  }

  async readyTQforILA(){
    var tasks = await this.ilaService.GetTQForILA(this.ilaId);
    this.dataSourceTask.data = tasks.filter((task)=>task.isUsedForTQ==true);
    this.notSelectedTaskDataSource.data = tasks.filter((task)=>task.isUsedForTQ==false);
    this.readySettingData();
  }

  async readySettingData() {
    await this.getTQReleaseSettingByILA();
    if(this.isComingFromClassSchedule){
      await this.getTQReleaseSettingByClass();
      await this.getTQTaskEvaluationsByClass();
    }else{
      await this.getTQTaskEvaluationsByILA();
   }
   this.patchTQRelease();
  }

  async getTQReleaseSettingByClass(){
    this.tqClassReleaseSetting = await this.classTQSettingService.getClassScheduleTQEMPSettings(this.classScheduleId);
  }
  async getTQTaskEvaluationsByClass(){
    this.dataSourceTaskEval.data = await this.classTQSettingService.getClassScheduleTQEvaluators(this.classScheduleId);
  }
  async getTQReleaseSettingByILA(){
    this.tqILAReleaseSetting = await this.ilaService.getTQRelease(this.ilaId);
  }
  async getTQTaskEvaluationsByILA(){
    this.dataSourceTaskEval.data = await this.ilaService.getTQTaskEvaluations(this.ilaId);
  }

  async dropTable(event: any) {
    moveItemInArray(this.dataSourceTask.data, event.previousIndex , event.currentIndex);
    var totalUserForTQIds = this.dataSourceTask.data.map((x)=>x.id);
    var totalNotUserForTQIds = this.notSelectedTaskDataSource.data.map(x=>x.id);
    this.dataSourceTask = new MatTableDataSource(this.dataSourceTask.data);
    var totalLinkedIds = Array.from(new Set([...totalUserForTQIds,...totalNotUserForTQIds]));
    var taskLinkUpdateOptions = new ILATaskObjectiveLinkUpdateOptions();
    taskLinkUpdateOptions.taskLinks =totalLinkedIds.map((taskId, index) => ({ taskId,sequence: index + 1 }));
    await this.ilaService.updateILATaskObjectiveLinksAsync(this.ilaId,taskLinkUpdateOptions);
    this.isReordered = true;
  }

  async openFlyInPanel(templateRef: any) {
    this.qualInfo= this.dataSourceTaskEval.data;
    if (!this.TQForm.get('tqRequired')?.value) {
      const portal = new TemplatePortal(templateRef, this.vcf);
      this.flyPanelService.open(portal);
    }
  }
  keyPressNumbers(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      // if(Number.parseInt(this.TQForm.get('tqDueDate')?.value + String.fromCharCode(charCode)) <= 365){
      //   return true
      // }
      // else{
      //   return false;
      // }

      return true;
    }
  }

  async saveTQRelease() {
    this.spinner = true;
    if(this.isComingFromClassSchedule){
      await this.saveInfoTQClassRelease();
    }
    else{
      await this.saveInfoTQILARelease();
    }
    this.patchTQRelease();
  }

  async saveInfoTQILARelease(){
    let createOpt: EMPSettingTQCreationOptions = {
      ilaId: this.TQForm.get('ilaId')?.value,
      multipleSignOffRequired: this.TQForm.get('multipleSignOffRequired')?.value == 0 ? null : this.TQForm.get('multipleSignOffRequired')?.value,
      oneSignOffRequired: this.TQForm.get('oneSignOffRequired')?.value,
      priorToSpecificTime: this.TQForm.get('priorToSpecificTime')?.value,
      releaseAtOnce: this.TQForm.get('releaseAtOnce')?.value,
      releaseOnClassEnd: this.TQForm.get('releaseOnClassEnd')?.value,
      releaseOnClassStart: this.TQForm.get('releaseOnClassStart')?.value,
      releaseOneAtTime: this.TQForm.get('releaseOneAtTime')?.value,
      specificTime: this.TQForm.get('specificTime')?.value,
      tqDueDate: this.TQForm.get('tqDueDate')?.value,
      tqRequired: this.TQForm.get('tqRequired')?.value,
      empSettingsReleaseTypeId:this.TQForm.get('empSettingsReleaseType')?.value,
      showTaskSuggestions: this.TQForm.get('suggestions')?.value,
      showTaskQuestions: this.TQForm.get('questions')?.value
    };
    if (this.tqPk !== null && this.tqPk !== undefined) {
      this.ilaService.updateTQRelease(this.tqPk, createOpt).then(async (res) => {
        this.tqILAReleaseSetting = res;
        this.alert.successToast(await this.labelPipe.transform('ILA') +` Settings updated `);
      }).finally(() => {
        this.spinner = false;
      });
    } else {
      this.ilaService.createTQRelease(createOpt).then(async (res) => {
        this.tqILAReleaseSetting = res;
        this.alert.successToast(await this.labelPipe.transform('ILA') +` Settings created `);
      }).finally(() => {
        this.spinner = false;
      });
    }
  }

  async saveInfoTQClassRelease(){
    let createOpt: ClassScheduleTQEMPSettingsCreateOptions = {
      classScheduleId: this.classScheduleId,
      priorToSpecificTime: this.TQForm.get('priorToSpecificTime')?.value,
      releaseOnClassEnd: this.TQForm.get('releaseOnClassEnd')?.value,
      releaseOnClassStart: this.TQForm.get('releaseOnClassStart')?.value,
      specificTime: this.TQForm.get('specificTime')?.value,
      tqRequired: this.TQForm.get('tqRequired')?.value,
      showTaskSuggestions: this.TQForm.get('suggestions')?.value,
      showTaskQuestions:this.TQForm.get('questions')?.value
    };
    let isUpdateMode = this.tqPk !== null && this.tqPk !== undefined ;
    this.tqClassReleaseSetting =await this.classTQSettingService.updateClassScheduleTQEMPSettings(this.classScheduleId,createOpt);
    this.alert.successToast(`Class TQ Release Settings has been ${isUpdateMode ? "updated" : "created"}`);
    this.spinner = false;
  }


  patchTQRelease() {
    if (this.tqILAReleaseSetting !== null) {
      this.tqPk= this.isComingFromClassSchedule ? this.tqClassReleaseSetting?.id : this.tqILAReleaseSetting?.id;
      this.TQForm.patchValue({
        tqPK: this.tqILAReleaseSetting?.id,
        tqRequired: this.tqILAReleaseSetting.tqRequired,
        releaseAtOnce: this.tqILAReleaseSetting.releaseAtOnce,
        releaseOneAtTime: this.tqILAReleaseSetting.releaseOneAtTime,
        releaseOnClassStart: this.tqILAReleaseSetting.releaseOnClassStart,
        releaseOnClassEnd: this.tqILAReleaseSetting.releaseOnClassEnd,
        specificTime: this.tqILAReleaseSetting.specificTime,
        priorToSpecificTime: this.tqILAReleaseSetting.priorToSpecificTime,
        oneSignOffRequired: this.tqILAReleaseSetting.oneSignOffRequired,
        multipleSignOffRequired: this.tqILAReleaseSetting.multipleSignOffRequired,
        tqDueDate: this.tqILAReleaseSetting.tqDueDate,
        empSettingsReleaseType: this.tqILAReleaseSetting.empSettingsReleaseTypeId ?? this.defaultEmpSettingReleaseTypeId,
        suggestions: this.tqILAReleaseSetting.showTaskSuggestions,
        questions: this.tqILAReleaseSetting.showTaskQuestions
      });

      let setValue = this.testAvailabiilityTime.find(x => x.time == this.TQForm.get('specificTime')?.value && x.prior == this.TQForm.get('priorToSpecificTime')?.value)
      this.TQForm.controls['setAvailabilityTimeTQ'].setValue(setValue);
      if (this.TQForm.get('multipleSignOffRequired')?.value > 0) {
        this.TQForm.get('multipleSignOffRequiredCheck')?.setValue(true);
      }
    }
    if(this.dataSourceTask.data.length > 0 && !this.TQForm.get('releaseAtOnce')?.value && !this.TQForm.get('releaseOneAtTime')?.value && !this.TQForm.get('tqRequired')?.value){
      this.TQForm.get('releaseAtOnce').setValue(true);
    }
    if(this.isComingFromClassSchedule && this.tqClassReleaseSetting != null){
      this.TQForm.patchValue({
        tqPK: this.tqClassReleaseSetting?.id,
        tqRequired: this.tqClassReleaseSetting.tqRequired,
        releaseOnClassStart: this.tqClassReleaseSetting.releaseOnClassStart,
        releaseOnClassEnd: this.tqClassReleaseSetting.releaseOnClassEnd,
        specificTime: this.tqClassReleaseSetting.specificTime,
        priorToSpecificTime: this.tqClassReleaseSetting.priorToSpecificTime,
        suggestions: this.tqClassReleaseSetting.showTaskSuggestions,
        questions: this.tqClassReleaseSetting.showTaskQuestions
      });
      let setValue = this.testAvailabiilityTime.find(x => x.time == this.TQForm.get('specificTime')?.value && x.prior == this.TQForm.get('priorToSpecificTime')?.value)
      this.TQForm.controls['setAvailabilityTimeTQ'].setValue(setValue);
    }
    if(!this.TQForm.get('tqRequired')?.value){
      this.tqSelection.select('order');
      this.tqSelection.select('release');
      this.tqSelection.select('signOff');
    }
    this.TQForm.updateValueAndValidity();
  }

  async getTQAvailabilityTime(event) {
    this.TQForm.patchValue({
      specificTime: event.value.time,
      priorToSpecificTime: event.value.prior,
    });
  }

  tqSelected(event: any) {
    this.dataSourceTaskEval.data = event;
    if (this.dataSourceTaskEval.data.length > 0) {
      this.addTQTaskEvaluation();
    }
  }
 async refreshTQ(event: any) {
  if(this.isComingFromClassSchedule){
    await this.getTQTaskEvaluationsByClass();
  }else{
    await this.getTQTaskEvaluationsByILA();
  }
  }
  addTQTaskEvaluation() {
    let createStudentEvaluation: EMPSettingsTQTaskEvaluation = {
      ilaId: this.TQForm.get('ilaId')?.value,
      evaluatorIds: this.dataSourceTaskEval.data.map(x => x.id)
    }
    this.ilaService.createTQTaskEvaluations(createStudentEvaluation).then((res) => {
      this.alert.successToast(`Link has been created `);
    }).catch((res: any) => {


      this.alert.errorToast(res);
    })

  }

  orderOfCompletion(event: any, type: string) {
    if (event.checked) {
      switch (type) {
        case "releaseOneAtTime":
          this.TQForm.get('releaseAtOnce')?.setValue(false);
          break;
        case "releaseAtOnce":
          this.TQForm.get('releaseOneAtTime')?.setValue(false);
          break;
      }
      this.TQForm.updateValueAndValidity();
      this.tqSelection.select('order');
    }
    else {
      this.tqSelection.deselect('order');
    }
  }

  release(event: any, type: string) {
    if (event.checked) {
      switch (type) {
        case "releaseOnClassStart":
          this.TQForm.get('releaseOnClassEnd')?.setValue(false);
          this.TQForm.get('specificTime')?.setValue(null);
          this.TQForm.get('setAvailabilityTimeTQ')?.setValue(null);
          this.TQForm.get('setAvailabilityTimeTQ')?.clearValidators();
          this.TQForm.get('setAvailabilityTimeTQ')?.setErrors(null);
          break;
        case "releaseOnClassEnd":
          this.TQForm.get('releaseOnClassStart')?.setValue(false);
          this.TQForm.get('specificTime')?.setValue(null);
          this.TQForm.get('setAvailabilityTimeTQ')?.setValue(null);
          this.TQForm.get('setAvailabilityTimeTQ')?.clearValidators();
          this.TQForm.get('setAvailabilityTimeTQ')?.setErrors(null);
          break;
        case "specificTime":
          this.TQForm.get('releaseOnClassStart')?.setValue(false);
          this.TQForm.get('releaseOnClassEnd')?.setValue(false);
          this.TQForm.get('setAvailabilityTimeTQ')?.setValidators([Validators.required]);
          this.TQForm.get('setAvailabilityTimeTQ')?.setErrors({ required: true });
          break;
      }
      this.tqSelection.select('release');
    }
    else {
      this.TQForm.get('setAvailabilityTimeTQ')?.setValue(null);
      this.TQForm.get('setAvailabilityTimeTQ')?.clearValidators();
      this.TQForm.get('setAvailabilityTimeTQ')?.setErrors(null);
      this.tqSelection.deselect('release');
    }
    this.TQForm.updateValueAndValidity();
  }

  signOff(event: any, type: string) {
    if (event.checked) {
      switch (type) {
        case "oneSignOffRequired":
          this.TQForm.get('multipleSignOffRequiredCheck')?.setValue(false);
          this.TQForm.get('multipleSignOffRequired')?.setValue(null);
          this.TQForm.get('multipleSignOffRequired')?.clearValidators();
          this.TQForm.get('multipleSignOffRequired')?.setErrors(null);
          break;
        case "multipleSignOffRequiredCheck":
          this.TQForm.get('oneSignOffRequired')?.setValue(false);
          this.TQForm.get('multipleSignOffRequired')?.setValidators([Validators.required]);
          this.TQForm.get('multipleSignOffRequired')?.setErrors({ required: true });
          break;
      }
      this.tqSelection.select('signOff');
    }
    else {
      this.TQForm.get('multipleSignOffRequired')?.setValue(null);
      this.TQForm.get('multipleSignOffRequired')?.clearValidators();
      this.TQForm.get('multipleSignOffRequired')?.setErrors(null);
      this.tqSelection.deselect('signOff');
    }
    this.TQForm.updateValueAndValidity();
  }

  masterChange(event: any) {
    if (event.checked) {
      this.TQForm.clearValidators();
      var dueDate = this.TQForm.get('tqDueDate')?.value;
      var empSettingsReleaseType = this.TQForm.get('empSettingsReleaseType')?.value;
      this.TQForm.reset(this.originalInitialValues);
      this.TQForm.get('tqDueDate')?.setValue(dueDate);
      this.TQForm.get('empSettingsReleaseType')?.setValue(empSettingsReleaseType);
      this.TQForm.get('tqRequired')?.setValue(true);
      this.TQForm.get('tqPk')?.setValue(this.tqPk);
      // this.TQForm.get('tqDueDate')?.setValidators([Validators.required]);
      // this.TQForm.get('tqDueDate')?.setErrors({required:true});
      this.TQForm.get('multipleSignOffRequired')?.clearValidators();
      this.TQForm.get('multipleSignOffRequired')?.setErrors(null);
      this.TQForm.get('setAvailabilityTimeTQ')?.clearValidators();
      this.TQForm.get('setAvailabilityTimeTQ')?.setErrors(null);
      this.TQForm.updateValueAndValidity();
      this.tqSelection.clear();
    }
  }

  getSelectedDueDateType(){
    return this.empSettingsReleaseTypes?.find(x=>x.typeId == this.TQForm.get('empSettingsReleaseType')?.value)?.typeName;
  }

  
}
