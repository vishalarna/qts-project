import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { TaskListReviewOverview_TaskListReview_VM } from '@models/TaskListReview/TaskListReviewOverview_TaskListReview_VM';
import { TaskListReviewOverview_VM } from '@models/TaskListReview/TaskListReviewOverview_VM';
import { Store } from '@ngrx/store';
import { ApiTaskListReviewService } from 'src/app/_Services/QTD/TaskListReview/api.tasklistreview.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
import { cloneDeep } from 'lodash';

@Component({
  selector: 'app-task-list-review-overview',
  templateUrl: './task-list-review-overview.component.html',
  styleUrls: ['./task-list-review-overview.component.scss']
})
export class TaskListReviewOverviewComponent implements OnInit {
  dataSource:MatTableDataSource<TaskListReviewOverview_TaskListReview_VM>;
  tableColumns: string[];
  searchText:string;
  taskLisReviewtOverview_VM: TaskListReviewOverview_VM; 
  expandedData: TaskListReviewOverview_TaskListReview_VM | null;
  @ViewChild(MatSort) sort: MatSort;
  allTaskListReviews : TaskListReviewOverview_TaskListReview_VM[];
  filterValues:any = {};
  currentTaskListReviewId:string;
  currentTaskListReviewStatus:boolean;
  isOverviewLoading:boolean;
  appliedFilters: any[] = [];
  constructor(private _router: Router,
    private store: Store<{ toggle: string }>, private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService, private taskListReviewService :ApiTaskListReviewService) { }

  ngOnInit(): void {
    this.isOverviewLoading = false;
    this.expandedData = null;
    this.store.dispatch(sideBarOpen());
    this.tableColumns= ['expandIcon','title','position', 'type', 'startDate', 'endDate','reviewStatus', 'approvalDate', 'status', 'action'];
    this.dataSource = new MatTableDataSource<TaskListReviewOverview_TaskListReview_VM>();
    this.searchText = '';
    this.loadAsync()
  }

  async loadAsync(){
    this.isOverviewLoading = true;
    this.appliedFilters = [];
   await this.taskListReviewService.getOverviewAsync().then(res=>{
     this.isOverviewLoading = false;
    this.taskLisReviewtOverview_VM = res;
    this.allTaskListReviews = this.taskLisReviewtOverview_VM.taskListReviewOverview_TaskListReview_VMs;
    this.dataSource =new MatTableDataSource(this.taskLisReviewtOverview_VM.taskListReviewOverview_TaskListReview_VMs);
    this.filterValues.status ='Active';
    this.appliedFilters.push(this.filterValues)
    this.getTaskListReviewFilterValues();
  });
    setTimeout(()=>{
      this.dataSource.sort = this.sort;
    },1);
    this.dataSource.sortingDataAccessor=this.customSortAccessor;
  }

  customSortAccessor = (response: TaskListReviewOverview_TaskListReview_VM, column: string): string  => {
    switch (column) {
      case 'reviewStatus':
        return response.status;
      case 'status':
        return String(response.active);
      case 'position':
        return response.positions[0];
      default:
        return response[column];
    }
  };

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf, {sideBarBackDrop: true});
    this.flyPanelService.open(portal);
  }

  openTaskListReviewWizard(){
    this._router.navigate(['/evaluation/task-list-review/create']);
  }
 
  openTaskListReviewEditWizard(id:string) {
    this._router.navigate(['/evaluation/task-list-review/edit/'+id]);
  }

  async copyTaskListReview(id:string){
    var copiedId = await this.taskListReviewService.copyAsync(id);
    if(copiedId){
      this.openTaskListReviewEditWizard(copiedId);
    }
  }
  
  async deleteTaskListReview(id:string){
    await this.taskListReviewService.deleteAsync(id).then(res=>{
      this.loadAsync();
    });
  }

  async activateTaskListReview(row:TaskListReviewOverview_TaskListReview_VM){
    await this.taskListReviewService.activateAsync(row.id).then(res=>{
      row.active = true;
    });
  }

  async inactivateTaskListReview(row:TaskListReviewOverview_TaskListReview_VM){
    await this.taskListReviewService.inactivateAsync(row.id).then(res=>{
      row.active = false;
    });
  }

  getTaskListReviewsCountByStatus(active:boolean){
    return this.taskLisReviewtOverview_VM?.taskListReviewOverview_TaskListReview_VMs?.filter(x=>x?.active == active)?.length ?? 0;
  }

  searchUpdate(event: any) {
    const searchText = event.target.value.toLowerCase();
    this.searchText = searchText;
    this.getTaskListReviewFilterValues();
  }

  getTaskListReviewFilterValues(){
    let filteredTaskListReviews = cloneDeep(this.allTaskListReviews);
    if(this.searchText){
      filteredTaskListReviews = filteredTaskListReviews.filter(item=>{
        return item.title?.toLowerCase()?.includes(this.searchText) || item.type?.toLowerCase()?.includes(this.searchText);
      });  
    }
    if(this.filterValues?.type){
      filteredTaskListReviews = filteredTaskListReviews.filter(x => x.type?.toLowerCase() == this.filterValues.type.toLowerCase());
    }
    if(this.filterValues?.reviewStatus){
      filteredTaskListReviews = filteredTaskListReviews.filter(x => x.status?.toLowerCase() == this.filterValues.reviewStatus.toLowerCase());
    }
    if(this.filterValues?.reviewPeriodStart){
      filteredTaskListReviews = filteredTaskListReviews.filter(x => x.startDate != null && new Date(x.startDate) >= new Date(this.filterValues.reviewPeriodStart + 'T00:00:00'));
    }
    if(this.filterValues?.reviewPeriodEnd){
      filteredTaskListReviews = filteredTaskListReviews.filter(x => x.endDate != null && new Date(x.endDate) <= new Date(this.filterValues.reviewPeriodEnd + 'T00:00:00'));
    }
    if(this.filterValues?.reviewApprovalDate){
      filteredTaskListReviews = filteredTaskListReviews.filter(x => x.approvalDate != null && x.approvalDate.toString().split("T")[0] == this.filterValues.reviewApprovalDate);
    }
    if(this.filterValues?.status){
      filteredTaskListReviews = filteredTaskListReviews.filter(x => x.active == (this.filterValues.status.toLowerCase() == "active"));
    }
    if(this.filterValues?.taskReviewStatus){
      filteredTaskListReviews.forEach(x=>x.taskReviews = x.taskReviews.filter(m=>m.status?.toLowerCase() == this.filterValues.taskReviewStatus.toLowerCase()));
      filteredTaskListReviews = filteredTaskListReviews.filter(m=>m.taskReviews.length>0)
    }
    if(this.filterValues?.onlyMyTaskReviews){
      filteredTaskListReviews.forEach(x=>x.taskReviews = x.taskReviews.filter(m=>m.reviewers.some(y=>y.isMe)));
      filteredTaskListReviews = filteredTaskListReviews.filter(m=>m.taskReviews.length>0)
    }
    if(this.filterValues?.position){
      filteredTaskListReviews = filteredTaskListReviews.filter(x => x.positions?.map(i=>i.toLowerCase().trim()).includes(this.filterValues.position.toLowerCase().trim()));
    }
    this.dataSource = new MatTableDataSource(filteredTaskListReviews);
    setTimeout(()=>{
      this.dataSource.sort = this.sort;
      this.dataSource.sortingDataAccessor=this.customSortAccessor;
    },1);
  }

  getTaskReviewFilterValues(){
    let filteredTaskReviews = this.expandedData.taskReviews;
    if(this.filterValues?.taskReviewStatus){
      filteredTaskReviews = filteredTaskReviews.filter(x=>x.status?.toLowerCase() == this.filterValues.taskReviewStatus.toLowerCase())
    }
    if(this.filterValues?.onlyMyTaskReviews){
      filteredTaskReviews = filteredTaskReviews.filter(x=>x.reviewers.some(y=>y.isMe));
    }
    return filteredTaskReviews;
  }

  formatFilter(filter: any): string[] {
    const parts: string[] = [];
  
    if (filter.type) {
      parts.push(`Type: ${filter.type}`);
    }
  
    if (filter.reviewStatus) {
      parts.push(`Review Status: ${filter.reviewStatus}`);
    }
  
    if (filter.reviewPeriodStart) {
      parts.push(`Review Period Start: ${filter.reviewPeriodStart}`);
    }
  
    if (filter.reviewPeriodEnd) {
      parts.push(`Review Period End: ${filter.reviewPeriodEnd}`);
    }
  
    if (filter.reviewApprovalDate) {
      parts.push(`Approval Date: ${filter.reviewApprovalDate}`);
    }
  
    if (filter.status) {
      parts.push(`Status: ${filter.status}`);
    }
  
    if (filter.taskReviewStatus) {
      parts.push(`Task Review Status: ${filter.taskReviewStatus}`);
    }
  
    if (filter.onlyMyTaskReviews) {
      parts.push(`Only My Task Reviews: ${filter.onlyMyTaskReviews}`);
    }

    if (filter.position) {
      parts.push(`Position: ${filter.position}`);
    }
  
    return parts;
  }
  
  clearAllFilters(): void {
    this.appliedFilters = [];
    this.filterValues = {
      type: '',
      reviewStatus: '',
      reviewPeriodStart: '',
      reviewPeriodEnd: '',
      reviewApprovalDate: '',
      status: '',
      taskReviewStatus: '',
      onlyMyTaskReviews: false,
      position:''
    };
    this.getTaskListReviewFilterValues();
  }

  setFilters(filterValues: any) {
    this.appliedFilters = [];
    this.filterValues = filterValues;
    const isMeaningful = Object.entries(filterValues).some(([key, value]) => {
      if (value === null || value === undefined || value === '') return false;
      if (key === 'onlyMyTaskReviews' && value === false) return false;
      return true;
    });
    if (isMeaningful) {
      this.appliedFilters = [{ ...filterValues }];
    }
    this.getTaskListReviewFilterValues();
  }
}
