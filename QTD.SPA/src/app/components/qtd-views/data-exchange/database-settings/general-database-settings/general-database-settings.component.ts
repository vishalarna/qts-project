import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ClientSettings_GeneralSettings } from 'src/app/_DtoModels/ClientSettingsGeneralSetting/ClientSettings_GeneralSettings';
import { Observable, Subscription } from "rxjs";
import {
  ClientSettings_GeneralSettings_UpdateOptions
} from "../../../../../_DtoModels/ClientSettingsGeneralSetting/ClientSettings_GeneralSettings_UpdateOptions";
import { DomSanitizer } from '@angular/platform-browser';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import { ClientSettings_TimeZoneVM } from '@models/ClientSettingsGeneralSetting/ClientSettings_TimeZoneVM';
import { InstanceService } from 'src/app/_Services/Auth/instance.service';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';

@Component({
  selector: 'app-general-database-settings',
  templateUrl: './general-database-settings.component.html',
  styleUrls: ['./general-database-settings.component.scss']
})
export class GeneralDatabaseSettingsComponent implements OnInit {
  public mode: string;
  @Input() generalSettings;
  @Input() completeEvent: Observable<void>;
  @Output() OnSaveClickedEvent: EventEmitter<any> = new EventEmitter();
  @Input() publicUrl;
  @Input() publicUrlSetting: Observable<string>;
  instanceName: string;
  domain: string;
  isChecked: boolean;
  public generalSettingsFormGroup: UntypedFormGroup;
  public generalSettingsUpdateOptions: ClientSettings_GeneralSettings_UpdateOptions;
  public dragAreaClass: String;
  public imagePath:any;
  public isImage:boolean=false;
  public isTimeZone:boolean=true;
  public timeZones:ClientSettings_TimeZoneVM[];
  

  error: string;

  private completeSubscription: Subscription;

  constructor(private fb: UntypedFormBuilder, private sanitizer: DomSanitizer,private clientSettingsService: ApiClientSettingsService,
               private instanceService: InstanceService, private dataBroadcastService:DataBroadcastService) { }

  onFileChange(event: any) {
    let filesList: FileList = event.target.files;
    let file = event.target.files[0];
    this.getBase64(file);
    this.saveFiles(filesList);
  }

  getBase64(file) {
    let reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.imagePath = reader.result;
      this.updateGeneralSettingModel("companyLogo",this.imagePath);
    };
  }

  ngOnInit(): void {
    const self = this;
    self.mode = "read";
    self.dragAreaClass = "dragarea";
    self.generalSettingForm();
    self.generalSettingsFormGroup.disable();
    self.completeSubscription = this.completeEvent.subscribe((generalSettings) => {
      self.generalSettings = generalSettings;
      self.mode = 'read'
      self.generalSettingsFormGroup.disable();
      self.generalSettingForm();
      this.isImage= false;
      this.isTimeZone =true;
    })
    self.imagePath=self.generalSettings.companyLogo;
    self.getAlltimeZones();
    this.publicUrlSetting.subscribe((publicUrl) => {
      this.publicUrl = publicUrl;
      this.bindPublicUrl(this.publicUrl);
    })
    this.bindPublicUrl(this.publicUrl);
    this.createDomainInstanceUrl();
    this.dataBroadcastService.publicClassEnabled.subscribe((x) => {
      this.isChecked = x;
    })
  }

  async getAlltimeZones(){
    this.timeZones =await this.clientSettingsService.getAllTimeZones();
  }

  @HostListener("dragover", ["$event"]) onDragOver(event: any) {
    this.dragAreaClass = "droparea";
    event.preventDefault();
    event.stopPropagation();
  }
  @HostListener("dragenter", ["$event"]) onDragEnter(event: any) {
    this.dragAreaClass = "droparea";
    event.preventDefault();
  }
  @HostListener("dragend", ["$event"]) onDragEnd(event: any) {
    this.dragAreaClass = "dragarea";
    event.preventDefault();
  }
  @HostListener("dragleave", ["$event"]) onDragLeave(event: any) {
    this.dragAreaClass = "dragarea";
    event.preventDefault();
  }
  @HostListener("drop", ["$event"]) onDrop(event: any) {
    this.dragAreaClass = "dragarea";
    event.preventDefault();
    event.stopPropagation();
    if (event.dataTransfer.files) {
      let file: FileList = event.dataTransfer.files[0];
      this.getBase64(file);
      this.saveFiles(file);
    }
  }

  saveFiles(files: FileList) {
    if (files.length > 1) this.error = "Only one file at time allow";
    else {
      this.error = "";
    }
  }

  generalSettingForm() {
    this.generalSettingsFormGroup = this.fb.group({
      companyName: new UntypedFormControl(this.generalSettings.companyName, [Validators.required]),
      companyLogo: new UntypedFormControl('', [Validators.required]),
      dateFormat: new UntypedFormControl(this.generalSettings.dateFormat, [Validators.required]),
      classStartEndTimeFormat: new UntypedFormControl(this.generalSettings.classStartEndTimeFormat, [Validators.required]),
      defaultTimeZone:new UntypedFormControl (this.generalSettings.defaultTimeZone, [Validators.required]),
      passingScore: new UntypedFormControl(this.generalSettings.companySpecificCoursePassingScore <= 1 ? this.generalSettings.companySpecificCoursePassingScore * 100 : 
        this.generalSettings.companySpecificCoursePassingScore,[Validators.required]
    ),
    publicUrl: new UntypedFormControl('', [Validators.required]),
    });
  }

  updateGeneralSettingModel(name: string, value: any) {
    if (!this.generalSettingsUpdateOptions) this.generalSettingsUpdateOptions = new ClientSettings_GeneralSettings_UpdateOptions();
    this.generalSettingsUpdateOptions.UpdateSetting(name, value);
  }

  OnSaveButtonClick() {
    this.OnSaveClickedEvent.emit({options:this.generalSettingsUpdateOptions,url:this.generalSettingsFormGroup.get('publicUrl').value});
  }

  OnEditButtonClick() {
    this.isImage=true;
    this.isTimeZone = false;
    this.mode = "write";
    this.generalSettingsFormGroup.enable();
  }

  OnCancelButtonClick() {
    this.generalSettingsFormGroup.reset({
      companyName: this.generalSettings.companyName,
      companyLogo: this.generalSettings.companyLogo,
      dateFormat: this.generalSettings.dateFormat,
      classStartEndTimeFormat: this.generalSettings.classStartEndTimeFormat,
      defaultTimeZone:this.generalSettings.defaultTimeZone,
      passingScore: this.generalSettings.companySpecificCoursePassingScore,
      publicUrl: this.publicUrl
    });
    this.isTimeZone = true;
    this.isImage=false;
    this.mode = 'read';
    this.generalSettingsFormGroup.disable();
  }

  onTimeZoneChange(name: string, value: any) {
    if (!this.generalSettingsUpdateOptions) this.generalSettingsUpdateOptions = new ClientSettings_GeneralSettings_UpdateOptions();
    this.generalSettingsUpdateOptions.UpdateSetting(name, value);
  }

  bindPublicUrl(value:any){
    this.generalSettingsFormGroup.get('publicUrl').setValue(value);
  } 

  createDomainInstanceUrl(){
    var fullPath = window.location.href;
    var path = fullPath.split("/");
    this.domain = path.slice(0,-2).join("/");
    this.instanceName = jwtAuthHelper.SelectedInstance;
  }

}
