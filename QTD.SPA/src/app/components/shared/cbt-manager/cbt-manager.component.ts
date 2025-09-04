import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ScormUpload } from 'src/app/_DtoModels/Scorm/ScormUpload';
import { ScormUploadAddOptions } from 'src/app/_DtoModels/Scorm/ScormUploadAddOptions';
import { ScormUploadDeleteOptions } from 'src/app/_DtoModels/Scorm/ScormUploadDeleteOptions';
import * as ckcustomBuild from '../../../ckcustomBuild/build/ckeditor.js';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { CBTCreateOptions } from 'src/app/_DtoModels/ILA_CBT/CBTCreateOptions';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular';
import { CBT } from 'src/app/_DtoModels/ILA_CBT/CBT.js';
import { TranslateService } from '@ngx-translate/core';
import { SweetAlertService } from '../../../_Shared/services/sweetalert.service';
import { CBTAvailablity } from 'src/app/_DtoModels/ILA_CBT/CBTAvailablity';
import { CBTUpdateOptions } from 'src/app/_DtoModels/ILA_CBT/CBTUpdateOptions';
import { Person } from '../../../_DtoModels/Person/Person';
import { EmpSettingsReleaseTypeVM } from '@models/EmpSettingsReleaseType/EmpSettingsReleaseTypeVM';
import { EmpSettingsReleaseTypeService } from 'src/app/_Services/QTD/empSettingsReleaseType.service';
import { ILADetailsVM } from '@models/ILA/ILADetailsVM';

@Component({
  selector: 'app-cbt-manager',
  templateUrl: './cbt-manager.component.html',
  styleUrls: ['./cbt-manager.component.scss'],
})
export class CbtManagerComponent implements OnInit {
  @Input() ila: ILADetailsVM;
  public openDisconnectScormPopup: boolean = false;
  cbtCreateOptions: CBTCreateOptions;
  cbtUpdateOptions: CBTUpdateOptions;
  currentScormUpload: ScormUpload;
  public availability: CBTAvailablity;
  public addOptions: ScormUploadAddOptions;
  public deleteOptions: ScormUploadDeleteOptions;
  public editor = ckcustomBuild;
  public config;
  public studentsList: Array<Person>;
  public allScormUploads: ScormUpload[];
  public cbtData: CBT;
  public cbtLearningInstructions: string;
  public cbtAvailabilityEnum: Array<any>;
  public fileName: string;
  public dateImported: string;
  public isCBTRequired: boolean;
  public hasError: boolean;
  public spinner: boolean;
  public cbtDueDateData: string;
  public cbtDueDateType: string;
  public empSettingsReleaseTypes : EmpSettingsReleaseTypeVM[];
  public defaultEmpSettingReleaseTypeId : string = "";
  openImportScormPopup = false;
  public launchedStudentsList: any[] = [];

  @Output()
  OnAttachCourseBegin: EventEmitter<any> = new EventEmitter();
  @Output()
  OnAttachCourseSuccess: EventEmitter<any> = new EventEmitter();
  @Output()
  OnAttachCourseError: EventEmitter<any> = new EventEmitter();
  @Output()
  OnDisconnectBegin: EventEmitter<any> = new EventEmitter();
  @Output()
  OnDisconnectSuccess: EventEmitter<any> = new EventEmitter();
  @Output()
  OnDisconnectError: EventEmitter<any> = new EventEmitter();

  constructor(
    private _ilaService: IlaService,
    private alertService: SweetAlertService,
    private translate: TranslateService,
    private empSettingsReleaseTypeService :EmpSettingsReleaseTypeService
  ) {}

  async ngOnInit(): Promise<void> {
    this.spinner = false;
    this.isCBTRequired = this.ila.cbtRequiredForCourse;
    this.hasError = false;
    this.fileName = '';
    this.cbtLearningInstructions = '';
    this.dateImported = '';
    this.setCkEditorConfig();
    this.cbtAvailabilityEnum = [
      {"id": 2, "name": "On class start date and time", "value": "OnClassStartDateTime"},
      {"id": 3, "name": "On class end date and time", "value": "OnClassEndDateTime"},
      {"id": 4, "name": "After pretest is completed", "value": "AfterPretestComplete"}];
    this.initializeAddOrUpdateOptions();
    await this.empSettingsReleaseTypeService.getEmpSettingsReleaseTypes().then(res=>{
      this.empSettingsReleaseTypes = res;
      this.defaultEmpSettingReleaseTypeId = this.empSettingsReleaseTypes.find(x=> x.typeName == "Days")?.typeId;
      this.cbtCreateOptions.setEmpSettingsReleaseTypeId(this.defaultEmpSettingReleaseTypeId)
    });
    this.cbtCreateOptions.setCbtRequiredForCource(this.isCBTRequired);
    this.studentsList = await this._ilaService.getStudentsForILAAsync(
      this.ila.id
    );
    await this.getCBTWithILAIdAsync();
  }

  public initializeAddOrUpdateOptions() {
    this.cbtCreateOptions = new CBTCreateOptions();
    this.deleteOptions = new ScormUploadDeleteOptions();
  }

  public async getCBTWithILAIdAsync(): Promise<void> {
    this.cbtData = await this._ilaService.GetCBTForILAAsync(this.ila.id);
    if (this.cbtData) {
      this.cbtLearningInstructions =
        this.cbtData.cbtLearningContractInstructions;
      this.setCbtUpdateOptions();
    }
    else{
      this.cbtDueDateData = "1";
      this.cbtDueDateType = this.defaultEmpSettingReleaseTypeId;
    }
    await this.getScormCurrentUploadAsync();
    this.editor.data = this.cbtData?.cbtLearningContractInstructions;
  }

  public async getScormCurrentUploadAsync() {
    this.allScormUploads = [];
    if (this.cbtData) {
      this.allScormUploads = await this._ilaService.GetScormUploadsAsync(
        this.cbtData.id,
        true
      );
      this.currentScormUpload = this.allScormUploads?.filter(
        (dd) => dd.active === true
      )[0];
      if (this.currentScormUpload) {
        this.fileName = this.currentScormUpload.name;
        this.dateImported = this.currentScormUpload.connectedDate.toString();
      } else {
        this.fileName = '';
        this.dateImported = '';
      }
    }

    this.setLaunchedStudentsList();
  }

  public setLaunchedStudentsList(){
    if(this.currentScormUpload){
      this.launchedStudentsList = this.studentsList.filter(student => {
        const launchedRegistration = this.currentScormUpload.cbT_ScormRegistration.find(reg => {
          const studentId = student.employee.id.trim();
          const registrationId = reg.classScheduleEmployee.employeeId.trim(); 
          return reg.classScheduleEmployee && registrationId === studentId && reg.launchLink !== null;
        });
        return launchedRegistration != null;
      });
    }else{
      this.launchedStudentsList = [];
    }
  }

  public setCkEditorConfig() {
    this.config = {
      toolbar: [
        'bold',
        'italic',
        'link',
        'numberedList',
        'bulletedList',
        'imageUpload',
        'mediaEmbed',
      ],
      ui: {
        height: '30rem',
        resize_dir: 'vertical',
        resize_minHeight: '30rem',
        resize_enabled: false,
      },
    };
  }

  public setCbtRequired(event: any) {
    this.isCBTRequired = event.target.checked;
    this.cbtCreateOptions.setCbtRequiredForCource(event.target.checked);
    this.cbtUpdateOptions?.setCbtRequiredForCource(event.target.checked);
    if(this.cbtData){
      this.availability = this.cbtData?.availablity;
      this.cbtDueDateData = this.cbtData?.dueDateAmount.toString();
      this.cbtDueDateType = this.cbtData?.empSettingsReleaseTypeId.toString();
    }
  }

  public setCbtInstructionContent({ editor }: ChangeEvent) {
    const data = editor.getData();
    this.cbtCreateOptions.setCbtLearningInstructions(data);
    this.cbtUpdateOptions?.setCbtLearningInstructions(data);
  }

  public getCbtRequiredForCourseCurrentValue() {
    return this.isCBTRequired;
  }

  public getDueDateValues(name: string) {
    let value;
    if (name == 'amount') {
        value = this.cbtData?.dueDateAmount ?? 1;
    } else if (name == 'dateInterval') {
        value = this.cbtData?.empSettingsReleaseTypeId ?? this.defaultEmpSettingReleaseTypeId;
    }
    return value;
}

  public getCbtAvailability() {
    return this.cbtData?.availablity;
  }

  public setCbtRequiredValue() {
    return this.ila.cbtRequiredForCourse === true ? true : false;
  }

  public setCbtAvailabilityChange(event: any) {
    this.availability = event.value;
    this.cbtCreateOptions.setCbtAvailability(this.availability);
    this.cbtUpdateOptions?.setCbtAvailability(this.availability);
  }

  public async onScormImportClickEvent(event: any) {
    this.openImportScormPopup=false;
    this.spinner = true;

    const file: File = event.target.files[0];
    this.fileName = file.name;
    const formData = new FormData();
    formData.append('file', file);
    var currentDate = new Date();
    const dateImported = currentDate.toLocaleDateString();
    event.target.value = null;
  await this.onAttachCourseAsync(formData);
    this.dateImported = dateImported;
  }

  public setCbtUpdateOptions() {
    this.cbtUpdateOptions = new CBTUpdateOptions(
      this.ila.cbtRequiredForCourse,
      this.cbtData.cbtLearningContractInstructions,
      this.cbtData.availablity,
      this.cbtData.dueDateAmount,
      this.cbtData.empSettingsReleaseTypeId ?? this.defaultEmpSettingReleaseTypeId,
      false
    );
    this.availability = this.cbtData.availablity;
    this.cbtDueDateData = this.cbtData.dueDateAmount.toString();
    this.cbtDueDateType = this.cbtData.empSettingsReleaseTypeId.toString();
  }

  public handleDueDateIntervalChange(event: any) {
    this.cbtCreateOptions.setEmpSettingsReleaseTypeId(event.value);
    this.cbtUpdateOptions?.setChangeDueDate(
      this.cbtUpdateOptions.dueDateAmount,
      event.value
    );
    this.cbtDueDateType = event.value;
  }

  public handleDueDateAmountChange(value: any) {
    this.cbtCreateOptions.setCbtDueDateAmount(Number(value));
    this.cbtUpdateOptions?.setChangeDueDate(
      Number(value),
      this.cbtUpdateOptions.empSettingsReleaseTypeId
    );
    this.cbtDueDateData = value;
  }

  async onAttachCourseAsync(file: FormData): Promise<void> {
    this.OnAttachCourseBegin.emit('Attach Course Begin'); //hardcoded this bacause there is nothing which we can emit.
    try {
      const res = await this._ilaService.AddScormUploadAsync(
        this.cbtData.id,
        file
      );
      this.OnAttachCourseSuccess.emit(res);
      this.currentScormUpload = await this._ilaService.GetScormUploadAsync(
        this.cbtData.id,
        res.id
      );
      this.hasError = false;
    } catch (err) {
      this.hasError = true;

      if(this.currentScormUpload){
        this.onFailUploadAsync();
      }
     
      this.OnAttachCourseError.emit(err);
    }
    this.spinner = false;
  }

  public async onDisconnectCourseAsync() {
    this.OnDisconnectBegin.emit('Disconnect Course Begin'); //hardcoded this bacause there is nothing which we can emit.
    try {
      const res = await this._ilaService.disconnectScormUploadAsync(
        this.currentScormUpload.id,
        this.deleteOptions
      );
      this.OnDisconnectSuccess.emit(res);
      await this.getScormCurrentUploadAsync();
    } catch (err) {
      this.OnDisconnectError.emit(err);
    }
    this.openDisconnectScormPopup = false;
  }

  public async onFailUploadAsync() {
    await this._ilaService.failUploadAsync(
      this.currentScormUpload?.id,
        this.deleteOptions
      );
  }

  public onDisconnectPackageClick(val: any) {
    this.openDisconnectScormPopup = val;
  }

  public async onSaveButtonClickAsync() {
    if (this.cbtData) {
      await this.onUpdateCBTAsync();
    } else {
      await this.onCreateCBTAsync();
    }
  }

  public async onCreateCBTAsync(): Promise<void> {
    this.cbtData = await this._ilaService.createCBTAsync(
      this.ila.id,
      this.cbtCreateOptions
    );
    this.alertService.notificationSuccessToast(
      this.translate.instant('CBT Saved Successfully')
    );
    this.cbtUpdateOptions = new CBTUpdateOptions(this.cbtCreateOptions?.cBTRequiredForCource, this.cbtCreateOptions?.cBTLearningContractInstructions, 
      this.cbtCreateOptions?.availablity, this.cbtCreateOptions?.dueDateAmount, this.cbtData?.empSettingsReleaseTypeId, true);
  }

  public async onUpdateCBTAsync(): Promise<void> {
    await this._ilaService.updateCBTAsync(
      this.ila.id,
      this.cbtData.id,
      this.cbtUpdateOptions
    );
    this.alertService.notificationSuccessToast(
      this.translate.instant('CBT Updated Successfully')
    );
  }

  public disableSaveButton(){
    if((this.availability == null || this.cbtDueDateData == null || this.cbtDueDateData == '' || this.cbtDueDateType == null || this.cbtDueDateType == '') && this.isCBTRequired){
      return true;
    } else{
      return false;
    }
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

  onImportScormClick(isOpen: boolean) {
    this.openImportScormPopup = isOpen;
  }

  closePopup(): void {
    this.openDisconnectScormPopup = false;
  }
}
