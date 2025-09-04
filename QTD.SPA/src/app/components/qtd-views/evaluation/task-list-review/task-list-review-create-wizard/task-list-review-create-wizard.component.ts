import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatStep, MatStepper } from '@angular/material/stepper';
import { Router } from '@angular/router';
import { TaskListReview_VM } from '@models/TaskListReview/TaskListReview_VM';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiTaskListReviewService } from 'src/app/_Services/QTD/TaskListReview/api.tasklistreview.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { Location } from '@angular/common';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { TaskListReviewDetailsComponent } from './task-list-review-details/task-list-review-details.component';
import { ApiTaskListReviewStatusService } from 'src/app/_Services/QTD/TaskListReviewStatus/api.tasklistreviewstatus.service';
import { TaskListReviewStatus_VM } from '@models/TaskListReview/TaskListReviewStatus_VM';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';

@Component({
  selector: 'app-task-list-review-create-wizard',
  templateUrl: './task-list-review-create-wizard.component.html',
  styleUrls: ['./task-list-review-create-wizard.component.scss'],
})
export class TaskListReviewCreateWizardComponent implements OnInit {
  currentStep: number;
  header: string;
  description: string;
  @ViewChild('closeTaskListReviewDialog') closeTaskListReviewDialog: any;
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('reviewDetails') reviewDetails: TaskListReviewDetailsComponent;
  stepperOrientation: Observable<StepperOrientation>;
  taskListReviewVM : TaskListReview_VM = new TaskListReview_VM();
  inputTaskListReviewId:string;
  mode:string = "create";
  isUpdatingTaskList: boolean = false;
  taskListReviewStatuses : TaskListReviewStatus_VM[] = [];
  get isStep1FormValid(): boolean {
    const positions = this.reviewDetails?.createTaskListReviewForm?.get('positions')?.value;
    return (this.reviewDetails?.createTaskListReviewForm?.valid ?? false) && Array.isArray(positions) && positions?.length > 0;
  }
  @ViewChild('generateReportFlyIn') generateReportFlyIn: any;
  constructor(
    private store: Store<{ toggle: string }>,
    public dialog: MatDialog,
    private breakpointObserver: BreakpointObserver,
    private route: Router,
    private labelPipe:LabelReplacementPipe,
    private taskListReviewService:ApiTaskListReviewService,
    private location: Location,
    private alert: SweetAlertService,
    private taskListReviewStatusService : ApiTaskListReviewStatusService,
    public flypanelService:FlyInPanelService,
    private vcf: ViewContainerRef,
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }
  
  ngOnInit(): void {
    this.currentStep = 1;
    this.store.dispatch(sideBarClose());
    let urlSegment = this.route.url.split("/");
    if (urlSegment.includes('edit')) {
      this.inputTaskListReviewId = urlSegment.reverse()[0];
    }
    this.setStatus();
  }
  
  ngAfterViewInit(): void {
    var checkRedirect:any = this.location.getState();
      if(checkRedirect?.goToNext && this.inputTaskListReviewId){
        this.taskListReviewVM = checkRedirect?.data;
        this.patchReviewDetails();
        this.mode = "edit";
        if(this.stepper){
           setTimeout(() => this.stepper.next(), 1);
        }
      }else if (this.inputTaskListReviewId){
        this.mode="edit";
        this.loadAsync();
      }
  }

  async setStatus(){
    this.taskListReviewStatuses = await this.taskListReviewStatusService.getAllAsync();
    if(this.mode == 'create' && !this.inputTaskListReviewId){
      this.taskListReviewVM.statusId = this.taskListReviewStatuses.find(x=> x.type == "Draft")?.id;
    }
  }

  async loadAsync(){
    this.taskListReviewService.getAsync(this.inputTaskListReviewId).then(res=>{
      this.taskListReviewVM = res;
      this.patchReviewDetails();
    })
  }

  patchReviewDetails(){
    if(this.reviewDetails && typeof this.reviewDetails.setFormValues =='function'){
      this.reviewDetails.inputTaskListReviewVM=this.taskListReviewVM;
      this.reviewDetails.setFormValues();
    }
  }
  
  async publishClickAsync() {
    this.taskListReviewVM.statusId = this.taskListReviewStatuses.find(x=> x.type == "Published")?.id;
    await this.updateTaskListReviewAsync();
    this.closeTaskListReviewWizard();
  }

  continueClickAsync(e: Event) {
    this.stepper.next();
  }

  async goBack() {
    if(this.isStep1FormValid){
      var status = this.taskListReviewStatuses.find(x=> x.id == this.taskListReviewVM.statusId)?.type ?? "" ;
      this.header = 'Exit ' + await this.labelPipe.transform('Task') + ' List Review';
      this.description =`Leaving will save the ${await this.labelPipe.transform('Task')} List Review in a ${status} state.`;
      const dialogRef = this.dialog.open(this.closeTaskListReviewDialog, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }
    else{
      this.closeTaskListReviewWizard();
    }
  }

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf, {sideBarBackDrop: true});
    this.flypanelService.open(portal);
  }

  async confirmedBackToOverview(){
    if(this.mode == 'create'){
      await this.createTaskListReviewAsync(); 
    }
    else{
      await this.updateTaskListReviewAsync();
    }
    this.closeTaskListReviewWizard();
  }

  closeTaskListReviewWizard(){
    this.route.navigate(['evaluation/task-list-review/overview']);
  }

  onSelectionChange(event:any) {
    let stepDifference= event.selectedIndex - event.previouslySelectedIndex;
    let matSteps:MatStep[] = this.stepper.steps.toArray();
      if(stepDifference>1){
        for(let i=event.previouslySelectedIndex+1;i<event.selectedIndex;i++){
          matSteps[i].interacted=true;
          matSteps[i].completed=true;
        }
    }
    this.currentStep = event.selectedIndex + 1;
    var checkRedirect:any = this.location.getState();
    if(event.previouslySelectedIndex == 0 && !checkRedirect?.goToNext){
      if(this.mode == 'create'){
        this.createTaskListReviewAsync(); 
      }
      else{
        this.updateTaskListReviewAsync();
      }
    }
    else if (event.previouslySelectedIndex == 4){
      this.updateTaskListReviewAsync();
    }
  }

  async createTaskListReviewAsync(){
    await this.taskListReviewService.createAsync(this.taskListReviewVM).then(async res =>{
      await this.route.navigate(['evaluation/task-list-review/edit/' + res.id],{ state: { goToNext: true,data:res }});
      this.alert.successToast(await this.labelPipe.transform('Task') + " List Review Created Successfully");
    });
  }

 async updateTaskListReviewAsync() {
  this.isUpdatingTaskList = true;
  try {
    const res = await this.taskListReviewService.updateAsync(this.inputTaskListReviewId, this.taskListReviewVM);
    this.taskListReviewVM = res;
  } finally {
    this.isUpdatingTaskList = false;
  }
 }

  async updateAndGenerateReport(){
    await this.updateTaskListReviewAsync();
    this.openFlyInPanel(this.generateReportFlyIn);
  }
}
