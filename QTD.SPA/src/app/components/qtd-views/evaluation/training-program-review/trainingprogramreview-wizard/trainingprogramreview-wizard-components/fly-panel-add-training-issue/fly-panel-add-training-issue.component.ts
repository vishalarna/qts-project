import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { TrainingIssue_Severity_VM } from '@models/TrainingIssues/TrainingIssue_Severity_VM';
import { TrainingIssue_Status_VM } from '@models/TrainingIssues/TrainingIssue_Status_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-training-issue',
  templateUrl: './fly-panel-add-training-issue.component.html',
  styleUrls: ['./fly-panel-add-training-issue.component.scss']
})
export class FlyPanelAddTrainingIssueComponent implements OnInit
{
  @Input() dataElementTypeId = "";
  @Input() dataElement = new TrainingIssue_DataElement_VM();
  trainingIssueVM:TrainingIssue_VM = new TrainingIssue_VM();
  trainingIssueForm: UntypedFormGroup;
  @Output() closed = new EventEmitter<any>();
  @Output() updatedIssue = new EventEmitter<TrainingIssue_VM>();
  trainingIssueSeverityList: TrainingIssue_Severity_VM[];
  trainingIssueStatusList: TrainingIssue_Status_VM[];
  showLoader:boolean = false;

  constructor(
    private fb: UntypedFormBuilder,
    private trainingIssuesService:TrainingIssuesService,
    private alert:SweetAlertService
  ) { }

  ngOnInit(): void {
    this.initializeIssuesDetailsForm();
    this.loadAsync();
  }

  async loadAsync() {
    await this.getAllSeveritiesAsync();
    await this.getAllStatusAsync();
    this.setDataElement();
  }

  async getAllSeveritiesAsync() {
    await this.trainingIssuesService.getAllSeveritiesAsync().then(async (res) => {
      this.trainingIssueSeverityList = res;
    }).catch((error) => {
    });
  }

  async getAllStatusAsync() {
    await this.trainingIssuesService.getAllStatusesAsync().then(async (res) => {
      this.trainingIssueStatusList = res;
    }).catch((error) => {
    });
  }

  initializeIssuesDetailsForm() {
    this.trainingIssueForm = this.fb.group({
      issueId: new UntypedFormControl(null, [Validators.required]),
      title: new UntypedFormControl(null, [Validators.required]),
      description: new UntypedFormControl(null),
      status: new UntypedFormControl(null, [Validators.required]),
      severity: new UntypedFormControl(null, [Validators.required]),
      createdDate: new UntypedFormControl(null, [Validators.required]),
      dueDate: new UntypedFormControl(null, [Validators.required])
    });
  }

  setDataElement(){
    this.dataElement.dataElementId = this.dataElementTypeId;
  }

  onIssueIdInput() {
    this.trainingIssueVM.issueCode = this.trainingIssueForm.get('issueId')?.value;
  }

  onTitleInput() {
    this.trainingIssueVM.issueTitle = this.trainingIssueForm.get('title')?.value;
  }

  onDescriptionInput() {
    this.trainingIssueVM.description = this.trainingIssueForm.get('description')?.value;
  }

  onCreatedDateSelect() {
    this.trainingIssueVM.createdDate = this.trainingIssueForm.get('createdDate')?.value;
  }

  onDueDateSelect() {
    this.trainingIssueVM.dueDate = this.trainingIssueForm.get('dueDate')?.value;
  }

  onStatusSelect() {
    this.trainingIssueVM.statusId = this.trainingIssueForm.get('status')?.value;
  }

  onSeveritySelect() {
    this.trainingIssueVM.severityId = this.trainingIssueForm.get('severity')?.value;
  }

  async createTrainingIssue(){
    this.showLoader = true;
    var trainingIssue = await this.trainingIssuesService.createAsync(this.trainingIssueVM);
    var dataElement = await this.trainingIssuesService.UpdateDataElementAsync(this.dataElement,trainingIssue.id);
    this.trainingIssueVM = trainingIssue;
    this.trainingIssueVM.dataElement = dataElement;
    var updatedTrainingIssue = await this.trainingIssuesService.updateAsync(this.trainingIssueVM,this.trainingIssueVM.id);
    this.alert.successToast("Training Issue created successfully");
    this.updatedIssue.emit(updatedTrainingIssue);
    this.closed.emit();
    this.showLoader = false;
  }

}

