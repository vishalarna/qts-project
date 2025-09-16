import { TemplatePortal } from '@angular/cdk/portal';
import { HttpResponse } from '@angular/common/http';
import {
  Component,
  Input,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportExportOptions, ReportExportType } from '@models/Report/ReportExportOptions';
import { ReportFilterOption } from '@models/Report/ReportFilterOption';
import { ReportUpdateOptions } from '@models/Report/ReportUpdateOptions';
import { ReportSkeleton } from '@models/ReportSkeleton/ReportSkeleton';
import { ReportSkeletonColumn } from '@models/ReportSkeleton/ReportSkeletonColumn';
import { GetTaskWithAllLinkData } from '@models/Task/GetTaskWithAllLinkData';
import { Task } from '@models/Task/Task';
import { TaskReviewActionItemPriority_VM } from '@models/Task_Review/TaskReviewActionItemPriority_VM';
import { TaskReviewActionItem_VM } from '@models/Task_Review/TaskReviewActionItem_VM';
import { TaskReviewFinding_VM } from '@models/Task_Review/TaskReviewFinding_VM';
import { TaskReview_ReviewerOption } from '@models/Task_Review/TaskReview_ReviewerOption';
import { TaskReview_Reviewer_VM } from '@models/Task_Review/TaskReview_Reviewer_VM';
import { TaskReview_TaskReviewActionItem_VM } from '@models/Task_Review/TaskReview_TaskReviewActionItem_VM';
import { TaskReview_VM } from '@models/Task_Review/TaskReview_VM';
import { TrainingIssue_ActionItem_VM } from '@models/TrainingIssues/TrainingIssue_ActionItem_VM';
import { TrainingIssue_ActionItems_VM } from '@models/TrainingIssues/TrainingIssue_ActionItems_VM';
import { TrainingIssue_DataElementCategory_VM } from '@models/TrainingIssues/TrainingIssue_DataElementCategory_VM';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { TrainingIssue_DriverType_VM } from '@models/TrainingIssues/TrainingIssue_DriverType_VM';
import { TrainingIssue_Severity_VM } from '@models/TrainingIssues/TrainingIssue_Severity_VM';
import { TrainingIssue_Status_VM } from '@models/TrainingIssues/TrainingIssue_Status_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { VersionTaskModel } from '@models/Version_Task/VersionTaskModel';
import { Store } from '@ngrx/store';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { ApiTaskListReviewService } from 'src/app/_Services/QTD/TaskListReview/api.tasklistreview.service';
import { ApiTaskReviewService } from 'src/app/_Services/QTD/TaskReview/api.taskReview.service';
import { TaskReviewActionItemService } from 'src/app/_Services/QTD/TaskReviewActionItem/api.task-review-actionItem.service';
import { ApiTaskReviewFindingService } from 'src/app/_Services/QTD/TaskReviewFindings/api.taskReviewFinding.service';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { QTDService } from 'src/app/_Services/QTD/qtd.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-task-review',
  templateUrl: './task-review.component.html',
  styleUrls: ['./task-review.component.scss'],
})
export class TaskReviewComponent implements OnInit {
  header: string;
  description: string;
  actionItemsTypes: string[];
  @ViewChild('closeTaskReviewDialog') closeTaskReviewDialog: any;
  selectedActionItemType: string;
  findings: TaskReviewFinding_VM[];
  taskDetails: Task;
  taskAllDetails: GetTaskWithAllLinkData;
  inputTaskReviewId: string;
  regex = /<img (.*?)>/;
  filteredReviewers:Array<any>;
  reviewers: Array<any>;
  taskReviewDetail: TaskReview_VM;
  selectedTaskReviewers: Array<TaskReview_Reviewer_VM>;
  taskReviewForm: UntypedFormGroup;
  lastModified: { name; dt_stamp };
  @ViewChild('selectControl', { static: false }) selectControl!: MatSelect;
  taskId:string;
  selectedActionItemId:string;
  findingRequalId:string;
  taskReviewObjectVM:TaskReview_VM;
  isComingFromWizard:boolean | string;
  inputTaskListReviewId:string;
  isTaskReviewLoading:boolean;
  reportSkeleton: ReportSkeleton;
  reportSkeletonName: string;
  reportCreateorUpdate:ReportUpdateOptions;
  displayColumns:ReportSkeletonColumn[];
  trainingIssueForm: UntypedFormGroup;
  trainingIssueSeverityList: TrainingIssue_Severity_VM[];
  trainingIssueStatusList: TrainingIssue_Status_VM[];
  trainingIssue_VM: TrainingIssue_VM;
  showTrainingIssueForm:boolean = false;
  noChangeId:string;
  nextTaskFullNumber:any;
  trainingIssueFormInitialized = false;
  actionItemData: TaskReviewActionItem_VM = {} as TaskReviewActionItem_VM;
  priorities:TaskReviewActionItemPriority_VM[];
  checkStatus:boolean = false;
  trainingIssueDataElementCategoryList: TrainingIssue_DataElementCategory_VM[] = [];
  trainingIssueDriverTypeList: TrainingIssue_DriverType_VM[] = [];
  trainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();

  constructor(
    public dialog: MatDialog,
    private labelPipe: LabelReplacementPipe,
    public flyPanelService: FlyInPanelService,
    private actionItemSrvc: TaskReviewActionItemService,
    private taskReviewFindingSrvc: ApiTaskReviewFindingService,
    private taskService: TasksService,
    private dynamicLabelPipe: DynamicLabelReplacementPipe,
    private qtdUserSrvc: QTDService,
    private route: ActivatedRoute,
    private taskReviewService: ApiTaskReviewService,
    private fb: UntypedFormBuilder,
    public alert:SweetAlertService,
    private _router:Router,
    private taskListReviewService:ApiTaskListReviewService,
    private store: Store<{ toggle: string }>,
    private apiReportService: ApiReportsService,
    private trainingIssuesService: TrainingIssuesService
  ) {}

  ngOnInit() {
    this.isTaskReviewLoading = false;
    this.store.dispatch(sideBarClose());
    this.loadAsync();
    this.selectedTaskReviewers = [];
    this.reportSkeletonName = 'Task History By Task';
    this.getReportSkeletonData();
    this.initializeTrainingIssueForm();
  }

  async getIdFromRoute() {
    this.route.params.subscribe(async (params) => {
      this.inputTaskReviewId = params['id'];
      this.inputTaskListReviewId = params['taskListReviewId'];
      if (this.inputTaskReviewId) {
        await this.getTaskReviewDetails();
        await this.getTaskDetailsById(this.taskId);
        await this.getlastModifiedBy(this.taskId);
        this.updateFormDataValues();
        await this.getTrainingIssueAsync();
        await this.getAllWithSubTypesAsync();
        await this.getAllDataElementsWithCategoriesAsync();
      }
    });
  }
  
  async loadAsync(){
    await this.initializeFormData();
    await this.getAllFindings();
    await this.getIdFromRoute();
     this.getAllReviewers();
     this.route.queryParams.subscribe(params => {
      this.isComingFromWizard = params?.isComingFromWizard;
     });
     this.getAllSeveritiesAsync();
     this.getAllStatusAsync();
     await this.getAllActionItemPriority();
  }

  initializeFormData() {
    this.taskReviewForm = this.fb.group({
      reviewDate: new UntypedFormControl(null),
      reviewedBy: new UntypedFormControl([]),
      finding: new UntypedFormControl(null),
      requalificationDueDate: new UntypedFormControl(null),
      notes: new UntypedFormControl(null),
      searchReviewerTxt: new UntypedFormControl(''),
    });
  }

  initializeTrainingIssueForm(){
    if (!this.trainingIssueFormInitialized) {
      this.trainingIssueForm = this.fb.group({
        issueId: new UntypedFormControl(null, [Validators.required]),
        title:new UntypedFormControl(null, [Validators.required]),
        status:new UntypedFormControl(null, [Validators.required]),
        severity:new UntypedFormControl(null, [Validators.required]),
        createdDate:new UntypedFormControl(null, [Validators.required]),
        dueDate:new UntypedFormControl(null, [Validators.required])
      });
      this.trainingIssueFormInitialized = true;
    }
  }

  async createTrainingIssue(){
    const createOptions: TrainingIssue_VM = new TrainingIssue_VM();
    createOptions.issueTitle = this.trainingIssueForm.get('title').value;
    createOptions.issueCode = this.trainingIssueForm.get('issueId').value;
    createOptions.statusId = this.trainingIssueForm.get('status').value;
    createOptions.severityId = this.trainingIssueForm.get('severity').value;
    createOptions.createdDate = this.trainingIssueForm.get('createdDate').value;
    createOptions.dueDate = this.trainingIssueForm.get('dueDate').value;
    createOptions.taskReviewId = this.inputTaskReviewId;
    const result = await this.trainingIssuesService.createAsync(createOptions);
    this.trainingIssue_VM =result;
   if (result?.id && this.taskReviewDetail.taskReviewActionItems?.length > 0) {
      await this.updateActionItems();
      await this.linkElementDetails();
      await this.updateTrainingIssueAsync(result.id);
    }
  }

  async updateTrainingIssueAsync(id:string) {
    const updateOptions: TrainingIssue_VM = new TrainingIssue_VM();
    updateOptions.issueTitle = this.trainingIssueForm.get('title').value;
    updateOptions.issueCode = this.trainingIssueForm.get('issueId').value;
    updateOptions.statusId = this.trainingIssueForm.get('status').value;
    updateOptions.severityId = this.trainingIssueForm.get('severity').value;
    updateOptions.createdDate = this.trainingIssueForm.get('createdDate').value;
    updateOptions.dueDate = this.trainingIssueForm.get('dueDate').value;
    updateOptions.taskReviewId = this.inputTaskReviewId;
    const teamFeedback = this.trainingIssueDriverTypeList.find(x => x.type === "Team Feedback");
    const trainingDeptPersonnel = teamFeedback?.subTypes.find(s => s.subType === "Training Dept. Personnel");
    updateOptions.driverTypeId = teamFeedback.id;
    updateOptions.driverSubTypeId = trainingDeptPersonnel?.id;
    updateOptions.dataElement = this.trainingIssueDataElementVM;

    await this.trainingIssuesService.updateAsync(updateOptions, id).then(async (res) => {
      this.updateTrainingIssueFormValues();
      this.alert.successToast("Training Issue Updated Successfully");
    }).catch((error) => {
      this.alert.errorToast("Training Issue Not Updated");
    });
  }

  async linkElementDetails() {
  const myDataCategory = this.trainingIssueDataElementCategoryList.find(x => x.name === "My Data");
  const taskElement = myDataCategory?.dataElementVMs.find((d: TrainingIssue_DataElement_VM) => d.dataElementType === "Task");
    const dataElement = new TrainingIssue_DataElement_VM();
    dataElement.dataElementType = taskElement.dataElementType;
    dataElement.dataElementId = this.taskId;
    dataElement.dataElementDisplay = taskElement.dataElementDisplay;
    dataElement.dataElementCategory = myDataCategory?.name ?? "";
    dataElement.dataElementDescription = `${this.taskDetails.number} - ${this.taskDetails.description}`;
    await this.trainingIssuesService.UpdateDataElementAsync(dataElement, this.trainingIssue_VM?.id).then(async (res) => {
        this.trainingIssueDataElementVM = res;
        this.trainingIssue_VM.dataElement = res;
      });
  }

  async updateTaskWithVersionTask() {
    const selectedFindingId = this.taskReviewForm.get('finding')?.value;
    const requalFindingId = this.findings.find(x => x.finding === "Changes Required - Training/Requalification Required")?.id;

    if (selectedFindingId !== requalFindingId) {
      return null;
    }
    const updateOptions: VersionTaskModel = new VersionTaskModel();
    updateOptions.requalificationDueDate = this.trainingIssueForm.get('dueDate')?.value;
    updateOptions.requalificationRequired = true;
    updateOptions.taskId = this.taskId;
    await this.taskService.updateTaskAndVersionTaskAsync(updateOptions);
  }

  async getTrainingIssueAsync() {
     const res = await this.trainingIssuesService.getTrainingIssueByTaskReviewIdAsync(this.inputTaskReviewId);
     this.trainingIssue_VM = res;
     this.updateTrainingIssueFormValues();
  }

  async getAllWithSubTypesAsync() {
    await this.trainingIssuesService.getAllWithSubTypesAsync().then((res) => {
      this.trainingIssueDriverTypeList = res;
    });
  }

   async getAllDataElementsWithCategoriesAsync() {
    await this.trainingIssuesService.getAllDataElementsWithCategoriesAsync().then(async (res) => {
      this.trainingIssueDataElementCategoryList = res;
    });
  }

  updateTrainingIssueFormValues(){
    this.trainingIssueForm.patchValue({
    issueId:this.trainingIssue_VM?.issueCode,
    title:this.trainingIssue_VM?.issueTitle,
    createdDate:this.formatDateToYyyyMmDd(this.trainingIssue_VM?.createdDate.toString()),
    dueDate:this.formatDateToYyyyMmDd(this.trainingIssue_VM?.dueDate.toString()),
    status:this.trainingIssue_VM?.statusId,
    severity:this.trainingIssue_VM?.severityId,
    });
  }

  updateFormDataValues(){
    this.taskReviewForm.patchValue({
      reviewDate :this.taskReviewDetail?.reviewDate ? this.formatDateToYyyyMmDd(this.taskReviewDetail?.reviewDate):null,
      finding: this.taskReviewDetail?.findingId,
      requalificationDueDate:this.taskReviewDetail?.requalificationDueDate ? this.formatDateToYyyyMmDd(this.taskReviewDetail?.requalificationDueDate) : null,
      notes: this.taskReviewDetail?.notes,
    });
    this.taskReviewForm.get('reviewedBy').setValue(this.reviewers.filter(x=> this.selectedTaskReviewers.some(y=> x.id == y.id)));
  }

  async getActionItemTypes() {
    this.actionItemsTypes = await this.actionItemSrvc.getActionItemTypes();
  }

  async getAllFindings() {
    this.findings = await this.taskReviewFindingSrvc.getAllAsync();
    this.findingRequalId = this.findings?.find(x=>x.finding=='Changes Required - Training/Requalification Required')?.id;
    this.noChangeId = this.findings?.find(x=>x.finding=='No Changes Required')?.id
  }

  async getAllReviewers() {
    const allReviewers = await this.qtdUserSrvc.getAllActiveAsync();
    this.reviewers = allReviewers.map((res) => {
      return {
        id: res?.id,
        name: res?.person?.firstName + ' ' + res?.person?.lastName,
      };
    });
    this.filteredReviewers = Object.assign(this.reviewers);
  }

  async getTaskReviewDetails() {
    this.isTaskReviewLoading = true;
    await this.taskReviewService.getAsync(this.inputTaskReviewId).then(res=>{
      this.taskReviewDetail = res;
      this.taskId = this.taskReviewDetail?.taskId;
      this.selectedTaskReviewers = this.taskReviewDetail.reviewers;
      this.nextTaskFullNumber = this.taskReviewDetail.fullNumber;
      this.taskReviewForm.updateValueAndValidity();
    });
    if (this.taskReviewDetail?.findingId != this.noChangeId && this.taskReviewDetail?.findingId != null) {
      this.showTrainingIssueForm = true;
    } else {
      this.showTrainingIssueForm = false;
    }
    this.isTaskReviewLoading = false;
  }

  formatDateToYyyyMmDd(dateString: string): string {
    const date = new Date(dateString);
  
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
  
    return `${year}-${month}-${day}`;
  }

  saveAndClose() {
    this.goBack();
  }

  async getTaskDetailsById(id: any) {
    this.isTaskReviewLoading = true;
    this.taskDetails = await this.taskService.get(id);
    this.taskAllDetails = await this.taskService.GetAllTaskData(id);
    this.isTaskReviewLoading = false;
  }

  async saveAndNext() {
    this.bindTaskReviewObjectVM();
    var result = await this.taskReviewService.updateAsync(this.inputTaskReviewId,this.taskReviewObjectVM);
    this.alert.successToast(await this.dynamicLabelPipe.transform(result));
    if(this.showTrainingIssueForm && this.trainingIssueForm.valid){
      if (this.trainingIssue_VM?.id) {
      await this.updateTrainingIssueAsync(this.trainingIssue_VM?.id);
     } else {
      await this.createTrainingIssue();
      await this.updateTaskWithVersionTask();
     }
    }
    this._router.navigate([`/evaluation/task-list-review/${this.inputTaskListReviewId}/edit/taskReview/${this.taskReviewObjectVM?.nextTaskReviewId}`],{ queryParams: { isComingFromWizard: this.isComingFromWizard}})
  }

async closeTaskReview() {
  if (this.isComingFromWizard && this.isComingFromWizard == "true") {
    this.isTaskReviewLoading = true;
    var res = await this.taskListReviewService.getAsync(this.inputTaskListReviewId);
    if (this.showTrainingIssueForm && this.trainingIssueForm.valid) {
      if (this.trainingIssue_VM?.id) {
        await this.updateTrainingIssueAsync(this.trainingIssue_VM?.id);
        await this.updateActionItems();
      } else {
        await this.createTrainingIssue();
        await this.updateTaskWithVersionTask();
      }
    }
    this.bindTaskReviewObjectVM();
    var result = await this.taskReviewService.updateAsync(this.inputTaskReviewId, this.taskReviewObjectVM);
    this.alert.successToast(await this.dynamicLabelPipe.transform(result));
    this.isTaskReviewLoading = false;
    this._router.navigate( ['evaluation/task-list-review/edit/' + this.inputTaskListReviewId], { state: { goToNext: true, data: res } } );
  } else {
    if (this.showTrainingIssueForm && this.trainingIssueForm.valid) {
      if (this.trainingIssue_VM?.id) {
        await this.updateTrainingIssueAsync(this.trainingIssue_VM?.id);
        await this.updateActionItems();
      } else {
        await this.createTrainingIssue();
        await this.updateTaskWithVersionTask();
      }
    }
    this.bindTaskReviewObjectVM();
    var result = await this.taskReviewService.updateAsync(this.inputTaskReviewId, this.taskReviewObjectVM);
    this.alert.successToast(await this.dynamicLabelPipe.transform(result));
    this._router.navigate(['evaluation/task-list-review/overview']);
  }
}

  openActionItemFlyIn(templateRef: TemplateRef<any>) {
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);
  }

  async goBack() {
    this.header =
      'Close ' + (await this.labelPipe.transform('Task')) + ' Review';
    this.description =
      'Leaving will save the ' +
      (await this.labelPipe.transform('Task')) +
      ' Review.';
    const dialogRef = this.dialog.open(this.closeTaskReviewDialog, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getlastModifiedBy(id: any) {
    this.isTaskReviewLoading = true;
    await this.taskService.getTaskHistory(id).then((res) => {
      let data = res[0];

      this.lastModified = {
        name: data?.createdBy ?? 'NO CREATOR',
        dt_stamp: data?.createdDate ?? Date.now(),
      };
    });
    this.isTaskReviewLoading = false;
  }

  async onReviewedByClick(id: any) {
    const selectedReviewer = this.selectedTaskReviewers.find((k) => k.id == id);
    if (!selectedReviewer) {
      const option = new TaskReview_ReviewerOption();
      option.qtdUserId = id;
      await this.taskReviewService.createTaskReviewReviewerAsync(
        this.taskReviewDetail.id,option);
      this.selectedTaskReviewers.push(id);
    }
  }

  removeReviewer(i: any) {
    const reviewers = this.taskReviewForm.get('reviewedBy')?.value;
    this.taskReviewService.deleteTaskReviewReviewerAsync(
      this.taskReviewDetail.id,
      i.id
    );
    this.removeFirst(reviewers, i);
    this.taskReviewForm.get('reviewedBy')?.setValue(reviewers);
  }

  private removeFirst(array: any[], toRemove: any): void {
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }

  getActionItemData(actionItem:TaskReviewActionItem_VM){
    var actionItem_alreadyPresent = this.taskReviewDetail.taskReviewActionItems.findIndex(x=>x.id==actionItem?.id);
    if(actionItem_alreadyPresent!=-1){
      this.taskReviewDetail.taskReviewActionItems.splice(actionItem_alreadyPresent,1)
    }
    var action_item = new TaskReview_TaskReviewActionItem_VM();
    action_item.id = actionItem.id;
    action_item.type = actionItem.type;
    action_item.assigneeName =actionItem.assignees;
    action_item.dueDate=actionItem.dueDate;
    action_item.assignedDate =actionItem.assignedDate;
    actionItem.priorityId=actionItem.priorityId;
    this.taskReviewDetail.taskReviewActionItems.push(action_item);
    if(this.trainingIssue_VM?.id !== null && this.trainingIssue_VM?.id !== undefined){
    this.updateActionItems();
    }
  }

  async deleteActionItem(id:string){
    var result = await this.actionItemSrvc.deleteAsync(id);
    var deletedActionItemIndex = this.taskReviewDetail.taskReviewActionItems.findIndex(x=>x.id==id);
    this.taskReviewDetail.taskReviewActionItems.splice(deletedActionItemIndex,1)
    this.alert.successToast(result);
  }

  async onFindingChange(val:string){
    if(val !=this.findingRequalId){
      this.taskReviewForm.get('requalificationDueDate').setValue(null);
    }
    var noChange = this.findings.find(x=>x.finding == "No Changes Required");
    this.showTrainingIssueForm = noChange.id != val ? true : false;
    if (noChange?.id === val && this.trainingIssue_VM?.id !== null && this.trainingIssue_VM?.id !== undefined ) {
       await this.deleteTrainingIssueById(this.trainingIssue_VM?.id);
    }
    await this.onActionItemSave();
    if (this.showTrainingIssueForm && !this.trainingIssueForm) {
      this.initializeTrainingIssueForm();
    }
  }
  
  bindTaskReviewObjectVM(){
    var newtaskReviewVM = new TaskReview_VM();
    newtaskReviewVM.id = this.taskReviewDetail?.id;
    newtaskReviewVM.number = this.taskReviewDetail?.number;
    newtaskReviewVM.statement = this.taskReviewDetail?.statement;
    newtaskReviewVM.reviewDate = this.taskReviewForm.get('reviewDate')?.value;
    newtaskReviewVM.reviewers = this.taskReviewForm.get('reviewedBy')?.value;
    newtaskReviewVM.findingId = this.taskReviewForm.get('finding')?.value;
    newtaskReviewVM.requalificationDueDate = this.taskReviewForm.get('requalificationDueDate')?.value;
    newtaskReviewVM.notes = this.taskReviewForm.get('notes')?.value;
    newtaskReviewVM.taskReviewActionItems = this.taskReviewDetail?.taskReviewActionItems;
    newtaskReviewVM.nextTaskReviewId = this.taskReviewDetail?.nextTaskReviewId;
    newtaskReviewVM.trainingIssueId=this.trainingIssue_VM?.id;
    this.taskReviewObjectVM = newtaskReviewVM;
  }

  addSpacesBetweenCapitalLetters(input: string): string {
    return input.replace(/([a-z])([A-Z])/g, '$1 $2');
  }

  reviewerSearch(){
    var searchValue = this.taskReviewForm.get('searchReviewerTxt')?.value;
    if (searchValue !== undefined && searchValue !== null) {
      searchValue = String(searchValue).toLowerCase();
    } else {
      searchValue = "";
    }
    this.reviewers =  this.filteredReviewers.filter((x) => {
      return x.name.trim().toLowerCase().includes(searchValue)
  })
  }

  handleKeydown(event: KeyboardEvent) {
    this.selectControl._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

  resetSearch(){
    setTimeout(() => {
      this.taskReviewForm.get('searchReviewerTxt')?.setValue('');
      this.reviewerSearch();
    }, 500);
  }

  public async downloadTaskHistoryByTasksReport(){
      this.getCreateUpdateOptions();
      var reportExportOption = new ReportExportOptions();
      reportExportOption.exportType = ReportExportType.Pdf;
      reportExportOption.options = this.reportCreateorUpdate;
      await this.taskService.generateTaskHistoryByTaskReportAsync(reportExportOption).then((res) => {
            this.handleFileDownload(res);
      });
  }

  getCreateUpdateOptions(){
        var reportCreateOptions = new ReportUpdateOptions();
        this.displayColumns.map(item=>{
          reportCreateOptions.getDisplayColumns(item.columnName)
        })
        var reportFilters = Array<ReportFilterOption>();
        var taskFilter  = this.reportSkeleton?.availableFilters?.find(x=>x.name.toLowerCase() =='select tasks');
        const taskIdFilter = new ReportFilterOption(taskFilter.name,this.taskId);
        reportFilters.push(taskIdFilter);
        reportCreateOptions.filters = reportFilters;
        reportCreateOptions.reportSkeletonId = this.reportSkeleton?.id;
        reportCreateOptions.internalReportTitle = this.reportSkeletonName;
        this.reportCreateorUpdate = reportCreateOptions;
  }

  async getReportSkeletonData() {
    this.reportSkeleton = await this.apiReportService.getReportSkeletonByNameAsync(this.reportSkeletonName);
    this.displayColumns =  Object.assign(this.reportSkeleton?.displayColumns);
  }

  private handleFileDownload(response: HttpResponse<Blob>) {
      const contentDispositionHeader = response.headers.get('content-disposition');
  
      const fileName = contentDispositionHeader
        ? contentDispositionHeader.split(';')[1].trim().split('=')[1].replace(/["']/g, "")
        : 'downloaded-file.csv';
  
      const blob = new Blob([response.body!], { type: 'application/octet-stream' });
      const url = window.URL.createObjectURL(blob);
  
      const link = document.createElement('a');
      link.href = url;
      link.download = fileName;
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }

  async getAllSeveritiesAsync() {
      await this.trainingIssuesService.getAllSeveritiesAsync().then(async (res) => {
        this.trainingIssueSeverityList = res;
      });
  }
  
  async getAllStatusAsync() {
      await this.trainingIssuesService.getAllStatusesAsync().then(async (res) => {
        this.trainingIssueStatusList = res;
      });
  }

 async getAllActionItemPriority(){
    this.priorities = await this.actionItemSrvc.getAllActionItemPriorities();
  }

 async onActionItemSave() {
  const selectedFindingId = this.taskReviewForm.get('finding')?.value;
  const selectedFinding = this.findings?.find(f => f.id === selectedFindingId);

  const actionItems = this.taskReviewDetail.taskReviewActionItems ?? [];
  const prepareItem = actionItems.find(ai => ai.type === 'PrepareForTaskRequalification');
  const metaTaskItem = actionItems.find(ai => ai.type === 'MakeTaskInactive');

  const isPrepareRequired = selectedFinding?.finding === 'Changes Required - Training/Requalification Required';
  const isMakeInactive = selectedFinding?.finding === 'Make Task Inactive';
  const highPriorityId = this.priorities?.find(p => p.type?.toLowerCase() === 'high')?.id;

  if (!isPrepareRequired && !isMakeInactive) {
    if (prepareItem) await this.deleteActionItem(prepareItem.id);
    if (metaTaskItem) await this.deleteActionItem(metaTaskItem.id);
    return;
  }

  if (isPrepareRequired) {
    if (metaTaskItem) await this.deleteActionItem(metaTaskItem.id);
    if (!prepareItem) {
      this.actionItemData.type = 'PrepareForTaskRequalification';
    } else {
      return;
    }
  }
  if (isMakeInactive) {
    if (prepareItem) await this.deleteActionItem(prepareItem.id);
    if (!metaTaskItem) {
      this.actionItemData.type = 'MakeTaskInactive';
    } else {
      return;
    }
  }
  this.actionItemData.priorityId = highPriorityId;
  this.actionItemData.assignedDate = new Date();
  this.actionItemData.dueDate = new Date();
  const actionItemDataResult = await this.taskReviewService.createTaskReviewActionItemAsync(this.taskReviewDetail.id, this.actionItemData);
  this.taskReviewDetail.taskReviewActionItems.push(actionItemDataResult);
 }

  async deleteTrainingIssueById(id:string) {
    await this.trainingIssuesService.deleteTrainingIssueByIdAsync(id).then(async (res) => { 
       this.trainingIssue_VM = {} as TrainingIssue_VM;
       this.trainingIssueForm.reset(); 
       this.alert.successToast('Training Issue Deleted Successfully');
      })
      .catch((error) => {
        this.alert.errorToast('Failed to delete training issue');
      });
  }

 async updateActionItems() {
  const actionItems = new TrainingIssue_ActionItems_VM();

  let currentMaxOrder = 0;
  if (this.trainingIssue_VM?.actionItems?.length > 0) {
    currentMaxOrder = Math.max(...this.trainingIssue_VM.actionItems.map(a => a.order || 0));
  }

  actionItems.actionItem_VMs = this.taskReviewDetail.taskReviewActionItems.map((item, index) => {
    const vm = new TrainingIssue_ActionItem_VM();
    vm.actionItemName = item.type;
    vm.order = currentMaxOrder + index + 1;
    vm.assigneeName =item.assigneeName;
    vm.dateAssigned=item.assignedDate;
    vm.dueDate=item.dueDate;
    vm.priorityId=item.priorityId;
    return vm;
  });
  await this.trainingIssuesService.updateActionItemsAsync(actionItems, this.trainingIssue_VM.id, this.checkStatus).then((res) => {
      this.alert.successToast("Action Items Updated Successfully"); 
    });
 }
}
