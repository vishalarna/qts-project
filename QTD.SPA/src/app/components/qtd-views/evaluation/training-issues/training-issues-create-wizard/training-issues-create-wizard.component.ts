import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatStepper } from '@angular/material/stepper';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { TrainingIssuesDetailsComponent } from './training-issues-details/training-issues-details.component';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { Location } from '@angular/common';
import { TrainingIssuesDriversAndTrainingComponent } from './training-issues-drivers-and-training/training-issues-drivers-and-training.component';
import { TrainingIssuesActionItemsComponent } from './training-issues-action-items/training-issues-action-items.component';
import { TrainingIssue_ActionItems_VM } from '@models/TrainingIssues/TrainingIssue_ActionItems_VM';
import { TrainingIssuesReviewAndPublishComponent } from './training-issues-review-and-publish/training-issues-review-and-publish.component';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';

@Component({
  selector: 'app-training-issues-create-wizard',
  templateUrl: './training-issues-create-wizard.component.html',
  styleUrls: ['./training-issues-create-wizard.component.scss']
})
export class TrainingIssuesCreateWizardComponent implements OnInit {
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('exitTrainingIssue') exitTrainingIssue: TemplateRef<any>;
  @ViewChild('trainingIssuesDeatils') trainingIssuesDeatils: TrainingIssuesDetailsComponent;
  @ViewChild('trainingIssuesDriversAndTraining') trainingIssuesDriversAndTraining: TrainingIssuesDriversAndTrainingComponent;
  @ViewChild('trainingIssuesActionItems') trainingIssuesActionItems: TrainingIssuesActionItemsComponent;
  @ViewChild('trainingIssuesReviewAndPublish') trainingIssuesReviewAndPublish: TrainingIssuesReviewAndPublishComponent;
  @ViewChild('allStepsCompletedTemplate') allStepsCompletedTemplate!: TemplateRef<any>;
  trainingIssue_Vm: TrainingIssue_VM = new TrainingIssue_VM()
  currentIndex: number = 0;
  previousIndex: number = 0;
  mode: string;
  trainingIssueId: string;
  goToNext: boolean = false;
  checkStatus:boolean =false;
  hasShownCompletionPopup:boolean = false;
  get isStep1FormValid(): boolean {
    return (this.trainingIssuesDeatils?.issuesDetailsForm?.valid ?? false);
  }

  constructor(
    private store: Store<{ toggle: string }>,
    private router: Router,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private trainingIssuesService: TrainingIssuesService,
    private location: Location,
  ) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    let segments = this.router.url.split('/');
    if (segments.includes('create')) {
      this.mode = 'create';
    }
    else if ((segments.includes('edit'))) {
      this.mode = 'edit';
      this.trainingIssueId = segments.reverse()[0];
    }
  }

  ngAfterViewInit(): void {
    var checkRedirect: any = this.location.getState();
    if (checkRedirect?.goToNext && this.trainingIssueId) {
      this.trainingIssue_Vm = checkRedirect?.data;
      this.goToNext = checkRedirect.goToNext;
      this.patchIssueDetails();
      if (this.stepper) {
        setTimeout(() => this.stepper.next(), 1);
      }
    } else if (this.trainingIssueId) {
      this.loadAsync();
    }
  }

  async loadAsync() {
    this.scrollToTop();
    await this.getTrainingIssueAsync();
  }

  private scrollToTop() {
    const stepperContainer = document.querySelector('.mat-sidenav-content');
    if (stepperContainer) {
      stepperContainer.scrollTop = 0;
    }
  }

  async getTrainingIssueAsync() {
    await this.trainingIssuesService.getAsync(this.trainingIssueId).then(async (res) => {
      this.trainingIssue_Vm = res;
      this.patchIssueDetails();
    }).catch((error) => {
      this.alert.errorToast("Failed to get training issue details.");
    });
  }

  async onSelectionChange(event: any) {
    this.currentIndex = event.selectedIndex;
    this.previousIndex = event.previouslySelectedIndex;
    if ((event.previouslySelectedIndex == 0 || event.previouslySelectedIndex == 1) && !this.goToNext) {
      if (this.mode == 'create') {
        await this.createTrainingIssueAsync();
      }
      else {
        await this.updateTrainingIssueAsync();
      }
    }
    if (event.selectedIndex < 3) {
      this.hasShownCompletionPopup = false;
    }
    if (this.previousIndex == 2 && this.currentIndex == 3) {
      await this.updateTrainingIssueAsync();
      await this.updateActionItems();
      this.trainingIssuesReviewAndPublish.dataSource.data = this.trainingIssue_Vm.actionItems;
      const allCompleted = this.checkIfAllActionStepsCompleted();
      if (allCompleted &&  !this.hasShownCompletionPopup && this.trainingIssue_Vm.status != "Closed") {
        event.preventDefault?.();
        this.stepper.selectedIndex = 2;
        this.showActionStepPopup();
        return;
      }
    }
    this.goToNext = false;
  }

  checkIfAllActionStepsCompleted(): boolean {
    const hasActionItems = this.trainingIssue_Vm.actionItems.length > 0;
    const result = this.trainingIssue_Vm.actionItems.every(item => item.status === 'Completed' && item.dateCompleted);
    return result && hasActionItems;
  }

  async onContinueClick() {
    this.stepper.next();
  }

  async updateActionItems() {
    var actionItems = new TrainingIssue_ActionItems_VM();
    actionItems.actionItem_VMs = this.trainingIssuesActionItems?.actionItemTable?.actionItemDataSource.data;
    await this.trainingIssuesService.updateActionItemsAsync(actionItems, this.trainingIssueId,this.checkStatus).then((res) => {
      this.trainingIssue_Vm.actionItems = res;
      this.alert.successToast("Action Items Updated Successfully");
    })
  }

  goBack() {
    if (this.currentIndex == 0 && this.trainingIssuesDeatils?.issuesDetailsForm?.valid) {
      const dialogRef = this.dialog.open(this.exitTrainingIssue, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }
    else {
      this.router.navigate(['evaluation/training-issues/overview']);
    }
  }

  async exitAndSaveTraining() {
    if (this.mode == 'create') {
      await this.createTrainingIssueAsync();
    }
    else if (this.mode == 'edit') {
      await this.updateTrainingIssueAsync();
    }
    this.router.navigate(['evaluation/training-issues/overview']);
  }

  async createTrainingIssueAsync() {
    await this.trainingIssuesService.createAsync(this.trainingIssue_Vm).then(async (res) => {
      await this.router.navigate(['/evaluation/training-issues/edit/' + res.id], { state: { goToNext: true, data: res } });
      this.alert.successToast("Training Issue Saved Successfully");
    }).catch((error) => {
      this.alert.errorToast("Training Issue Not Saved");
    });
  }

  async updateTrainingIssueAsync() {
    await this.trainingIssuesService.updateAsync(this.trainingIssue_Vm, this.trainingIssueId).then(async (res) => {
      this.trainingIssue_Vm = res;
      this.alert.successToast("Training Issue Updated Successfully");
    }).catch((error) => {
      this.alert.errorToast("Training Issue Not Updated");
    });
  }

  patchIssueDetails() {
    if (this.trainingIssuesDeatils && typeof this.trainingIssuesDeatils.initializeIssuesDetailsForm == 'function') {
      this.trainingIssuesDeatils.inputTrainingIssue_Vm = this.trainingIssue_Vm;
      this.trainingIssuesDeatils.initializeIssuesDetailsForm();
    }
    if (this.trainingIssuesDriversAndTraining && typeof this.trainingIssuesDriversAndTraining.initializeDriversDetailsForm == 'function') {
      this.trainingIssuesDriversAndTraining.inputTrainingIssue_Vm = this.trainingIssue_Vm;
      this.trainingIssuesDriversAndTraining.initializeDriversDetailsForm();
    }
    if (this.trainingIssuesActionItems && typeof this.trainingIssuesActionItems.onPlannedResponseChange == 'function') {
      this.trainingIssuesActionItems.inputTrainingIssue_Vm = this.trainingIssue_Vm;
    }
    if (this.trainingIssuesReviewAndPublish) {
      this.trainingIssuesReviewAndPublish.inputTrainingIssue_Vm = this.trainingIssue_Vm;
    }
  }

  onPublish(){
    this.router.navigate(['evaluation/training-issues/overview']);
  }

  public showActionStepPopup(): void {
    const dialogRef = this.dialog.open(this.allStepsCompletedTemplate, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async markTrainingIssueClosed() {
    this.hasShownCompletionPopup = true;
    this.checkStatus=true;
    await this.updateActionItems();
    await this.getTrainingIssueAsync();
    this.stepper.selectedIndex = 3;
  }

  cancelPopUp(){
    this.hasShownCompletionPopup = true;
    this.stepper.selectedIndex = 3;
  }
}
