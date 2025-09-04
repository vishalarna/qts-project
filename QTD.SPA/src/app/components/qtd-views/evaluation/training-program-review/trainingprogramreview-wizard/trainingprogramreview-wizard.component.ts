import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatStep, MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Observable } from 'rxjs';
import { BreakpointObserver } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { sideBarBackDrop, sideBarClose, sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { TrainingProgramReview_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_ViewModel';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ApiTrainingProgramReviewService } from 'src/app/_Services/QTD/TrainingProgramReview/api.trainingProgramReview.Service';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';


@Component({
  selector: 'app-trainingprogramreview-wizard',
  templateUrl: './trainingprogramreview-wizard.component.html',
  styleUrls: ['./trainingprogramreview-wizard.component.scss'],
})
export class TrainingProgramReviewWizardComponent implements OnInit {

  @Input() inputTPRId :string;
  @Input() handleLoad :() => void;
  @Input() handleBackClick :(e) => void;
  @Input() handleCancelClick :(e) => void;
  @Input() handleShareClick :(e) => void;
  @Input() handlContinueClick :(e) => void;
  @Input() handlePublishClick :(e) => void;
  tPRViewModel :TrainingProgramReview_ViewModel=new TrainingProgramReview_ViewModel();
  reportSkeletonId : string;
  stepperOrientation: Observable<StepperOrientation>;
  currentStep :number=1;
  shareDialogHeader: string;
  @ViewChild('saveProgramReview') saveProgramReview:any;
  @ViewChild('stepper') stepper: MatStepper;

  constructor(
    private router: Router, 
    private dialog: MatDialog,
    private store: Store<{ toggle: string }>,
    private breakpointObserver: BreakpointObserver,
    private programReviewService:ApiTrainingProgramReviewService,
    private reportService: ApiReportsService,
    private route:ActivatedRoute,
    private alert: SweetAlertService
  ) {
    this.stepperOrientation = breakpointObserver
    .observe('(min-width: 800px)')
    .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
    
  }

  ngOnInit(): void {
    this.shareDialogHeader = "Share Training Program Review";
    this.store.dispatch(sideBarClose());
    this.route.queryParams.subscribe((res)=>{
      if(res.data !== undefined){
        this.inputTPRId = res.data;
      }
    });
    this.loadAsync();
  }

  _handleLoad() {
    if (this.handleLoad &&typeof this.handleLoad === 'function') {
      this.handleLoad();
    }
  }

  _handleBackClick(e) {
    if (this.handleBackClick &&typeof this.handleBackClick === 'function') {
      this.handleBackClick(e);
    }
  }

  _handleCancelClick(e) {
    if (this.handleCancelClick &&typeof this.handleCancelClick === 'function') {
      this.handleCancelClick(e);
    }
  }

  _handleShareClick(e) {
    if (this.handleShareClick &&typeof this.handleShareClick === 'function') {
      this.handleShareClick(e);
    }
  }

  _handlContinueClick(e) {
    if (this.handlContinueClick &&typeof this.handlContinueClick === 'function') {
      this.handlContinueClick(e);
    }
  }

  _handlePublishClick(e) {
    if (this.handlePublishClick &&typeof this.handlePublishClick === 'function') {
      this.handlePublishClick(e);
    }
  }

  async loadAsync() {
    this.scrollToTop();
    this.tPRViewModel.published=false;
    this.tPRViewModel.reviewers=[];
    if(this.inputTPRId){
       await this.programReviewService.getTrainingProgramReviewAsync(this.inputTPRId).then(
        res=>{
          var revDate = new Date(res.reviewDate + "Z");
          var reviewDate = new Date(revDate).toLocaleString();
          var newStartDate= new Date(res.startDate + "Z");
          var startDate = new Date(newStartDate).toLocaleString();
          var newEndDate= new Date(res.endDate + "Z");
          var endDate = new Date(newEndDate).toLocaleString();
          res.reviewDate = reviewDate;
          res.startDate = startDate;
          res.endDate = endDate;
          this.tPRViewModel =res;
        }
      );
      let reportSkeletonData = await this.reportService.getReportSkeletonsAsync();
      let reportSkeleton = reportSkeletonData.filter(x=>x.defaultTitle == "Training Program Review");
      if(reportSkeleton.length >0){
        this.reportSkeletonId = reportSkeleton[0].id;
      }
      else{
        let error = "Report not found with this name";
      }
    }
    this._handleLoad();
  }

  async backClickAsync(){
    if(this.tPRViewModel.id){
      this.tPRViewModel = await this.programReviewService.updateAsync(this.tPRViewModel,this.tPRViewModel.id);
      this.alert.successToast("Training Program Review Updated");
    }
    else{
      this.tPRViewModel = await this.programReviewService.createAsync(this.tPRViewModel);
      this.alert.successToast("Training Program Review Saved as Draft");
    }
    this._handleBackClick(null);
    this.router.navigate(['evaluation/trainingprogram-review/overview']);
  }
  
  cancelClick(e:any){
    this.router.navigate(['evaluation/trainingprogram-review/overview']);
    this._handleCancelClick(e);
  }

  shareClick(e:any,templateRef: any){
    this._handleShareClick(e);
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async continueClickAsync(e:any){
    if(this.currentStep == 1 && !this.tPRViewModel.id){
      await this.programReviewService.createAsync(this.tPRViewModel).then(res=>{
        this.tPRViewModel = res;
        this.currentStep++;
        this.alert.successToast("Training Program Review Saved as Draft");
        setTimeout(() => { 
          this.stepper.next();
        }, 1);
      });
    }
    else{
      await this.programReviewService.updateAsync(this.tPRViewModel,this.tPRViewModel.id).then(res=>{
        this.tPRViewModel = res;
        this.currentStep++;
        this.alert.successToast("Training Program Review Updated");
        setTimeout(() => { this.stepper.next()}, 1);
      });
    }
    this.scrollToTop();
    this._handlContinueClick(e);
  }

  async publishClickAsync(e:any){
    this.tPRViewModel.published=true;
    this.tPRViewModel = await this.programReviewService.updateAsync(this.tPRViewModel,this.tPRViewModel.id);
    this.alert.successToast("Training Program Review Published Successfully");
    this._handlePublishClick(e);
    this.router.navigate(['evaluation/trainingprogram-review/overview']);
  }

  goBack() {
    if((this.currentStep === 1 && this.tPRViewModel.trainingProgramId) || this.tPRViewModel.id){
      const dialogRef = this.dialog.open(this.saveProgramReview, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }
    else{
      this.router.navigate(['evaluation/trainingprogram-review/overview']);
    }
  }
  
  onSelectionChange(event: any) {
    this.currentStep = event.selectedIndex +1;
    let stepDifference= event.selectedIndex - event.previouslySelectedIndex;
    if(stepDifference>1){
      let matSteps:MatStep[] = this.stepper.steps.toArray();
      for(let i=event.previouslySelectedIndex+1;i<event.selectedIndex;i++){
        matSteps[i].interacted=true;
        matSteps[i].completed=true;
      }
    }
    this.scrollToTop();
  }

  private scrollToTop() {
    const stepperContainer = document.querySelector('.mat-sidenav-content');
    if (stepperContainer) {
      stepperContainer.scrollTop = 0;
    }
  }

}
