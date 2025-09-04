import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TrainingIssue_Severity_VM } from '@models/TrainingIssues/TrainingIssue_Severity_VM';
import { TrainingIssue_Status_VM } from '@models/TrainingIssues/TrainingIssue_Status_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';

@Component({
  selector: 'app-training-issues-details',
  templateUrl: './training-issues-details.component.html',
  styleUrls: ['./training-issues-details.component.scss']
})
export class TrainingIssuesDetailsComponent implements OnInit {
  @Input() inputTrainingIssue_Vm: TrainingIssue_VM = new TrainingIssue_VM();
  issuesDetailsForm: UntypedFormGroup;
  loader: boolean = false;
  trainingIssueSeverityList: TrainingIssue_Severity_VM[];
  trainingIssueStatusList: TrainingIssue_Status_VM[];

  constructor(
    private fb: UntypedFormBuilder,
    private datePipe: DatePipe,
    private trainingIssuesService: TrainingIssuesService,
  ) { }

  async ngOnInit(): Promise<void> {
    this.initializeIssuesDetailsForm()
    await this.loadAsync();
  }

  initializeIssuesDetailsForm() {
    this.issuesDetailsForm = this.fb.group({
      issueId: new UntypedFormControl(this.inputTrainingIssue_Vm.issueCode ?? null, [Validators.required]),
      title: new UntypedFormControl(this.inputTrainingIssue_Vm.issueTitle ?? null, [Validators.required]),
      description: new UntypedFormControl(this.inputTrainingIssue_Vm.description ?? null),
      status: new UntypedFormControl(this.inputTrainingIssue_Vm.statusId ?? null, [Validators.required]),
      severity: new UntypedFormControl(this.inputTrainingIssue_Vm.severityId ?? null, [Validators.required]),
      createdDate: new UntypedFormControl(this.inputTrainingIssue_Vm.createdDate != null ? this.datePipe.transform(this.inputTrainingIssue_Vm.createdDate, "yyyy-MM-dd") : null, [Validators.required]),
      dueDate: new UntypedFormControl(this.inputTrainingIssue_Vm.dueDate != null ? this.datePipe.transform(this.inputTrainingIssue_Vm.dueDate, "yyyy-MM-dd") : null, [Validators.required])
    });
  }

  async loadAsync() {
    await this.getAllSeveritiesAsync();
    await this.getAllStatusAsync();
  }

  async getAllSeveritiesAsync() {
    this.loader = true;
    await this.trainingIssuesService.getAllSeveritiesAsync().then(async (res) => {
      this.trainingIssueSeverityList = res;
    }).catch((error) => {
      this.loader = false;
    });
  }

  async getAllStatusAsync() {
    await this.trainingIssuesService.getAllStatusesAsync().then(async (res) => {
      this.trainingIssueStatusList = res;
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
    });
  }

  onIssueIdInput() {
    this.inputTrainingIssue_Vm.issueCode = this.issuesDetailsForm.get('issueId')?.value;
  }

  onTitleInput() {
    this.inputTrainingIssue_Vm.issueTitle = this.issuesDetailsForm.get('title')?.value;
  }

  onDescriptionInput() {
    this.inputTrainingIssue_Vm.description = this.issuesDetailsForm.get('description')?.value;
  }

  onCreatedDateSelect() {
    this.inputTrainingIssue_Vm.createdDate = this.issuesDetailsForm.get('createdDate')?.value;
  }

  onDueDateSelect() {
    this.inputTrainingIssue_Vm.dueDate = this.issuesDetailsForm.get('dueDate')?.value;
  }

  onStatusSelect() {
    this.inputTrainingIssue_Vm.statusId = this.issuesDetailsForm.get('status')?.value;
  }

  onSeveritySelect() {
    this.inputTrainingIssue_Vm.severityId = this.issuesDetailsForm.get('severity')?.value;
  }


}
