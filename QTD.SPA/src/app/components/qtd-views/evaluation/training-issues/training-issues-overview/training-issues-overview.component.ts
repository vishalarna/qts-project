import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { TrainingIssueOverview_TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssueOverview_TrainingIssue_VM';
import { TrainingIssueOverview_VM } from '@models/TrainingIssues/TrainingIssueOverview_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { Store } from '@ngrx/store';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-training-issues-overview',
  templateUrl: './training-issues-overview.component.html',
  styleUrls: ['./training-issues-overview.component.scss'],
})
export class TrainingIssuesOverviewComponent implements OnInit {
  dataSource: MatTableDataSource<TrainingIssueOverview_TrainingIssue_VM>;
  @ViewChild(MatSort) sort: MatSort;
  tableColumns: string[];
  searchText: string;
  trainingIssueOverviewstats: TrainingIssueOverview_VM;
  isOverviewLoading: boolean = false;
  filterValues: any ={};
  filteredTrainingIssue: TrainingIssueOverview_TrainingIssue_VM[];
  filterTrainingIssueOnDashboard: TrainingIssueOverview_TrainingIssue_VM[]
  pendingActionItem:boolean;
  trainingIssue_Vms: TrainingIssue_VM[] =[];
  trainingIssueId:any;
  appliedFilters: any[] = [];
  @ViewChild('metaPaginator') set metaPaginator(paging: MatPaginator) {
    if (paging) this.dataSource.paginator = paging;
  }
  constructor(
    private _router: Router,
    private store: Store<{ toggle: string }>,
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    private trainingIssuesService: TrainingIssuesService,
    private alert: SweetAlertService,
     public dialog: MatDialog
  ) {}

  async ngOnInit(): Promise<void> {
    this.store.dispatch(sideBarOpen());
    this.dataSource = new MatTableDataSource<TrainingIssueOverview_TrainingIssue_VM>([]);
    this.tableColumns = [
      'CheckBox',
      'issueCode',
      'dueDate',
      'issueTitle',
      'severity',
      'driverType',
      'driverSubType',
      'pendingActionItem',
      'status',
      'action',
    ];
    this.searchText = '';
    await this.loadAsync();
  }

  async loadAsync() {
    await this.getOverviewAsync();
  }

  async getOverviewAsync() {
    this.isOverviewLoading = true;
    this.appliedFilters = [];
    await this.trainingIssuesService.getOverviewAsync().then(async (res) => {
      this.trainingIssueOverviewstats = res;
      this.filterTrainingIssueOnDashboard = this.trainingIssueOverviewstats.trainingIssues;
        this.dataSource = new MatTableDataSource(this.filterTrainingIssueOnDashboard);
        this.filterValues.status ='Open';
        this.filterValues.activeStatus='Active';
        this.appliedFilters.push(this.filterValues)
        this.filterData();
        this.isOverviewLoading = false;
        setTimeout(() => {this.dataSource.sort = this.sort;}, 1);
      })
      .catch((error) => {
        this.isOverviewLoading = false;
      });
  }

  async getTrainingIssuesWithPendingActionItemsOverview(){
    await this.trainingIssuesService.getWithPendingActionItemsAsync().then(async (res) => {
     this.trainingIssue_Vms= res;
      })
  }

  async getTrainingIssuesWithNoActionItemsOverview(){
    await this.trainingIssuesService.getWithNoActionItemsAsync().then(async (res) => {
    this.trainingIssue_Vms= res;
      })
  }

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf, {sideBarBackDrop: true,});
    this.flyPanelService.open(portal);
  }

  async openPendingActionItemFlyPanel(templateRef: any, value:any){
    const portal = new TemplatePortal(templateRef, this.vcf, {sideBarBackDrop: true,});
    this.flyPanelService.open(portal);
    if(value)
    await this.getTrainingIssuesWithPendingActionItemsOverview();
    else
    await this.getTrainingIssuesWithNoActionItemsOverview();
    this.pendingActionItem=value;
  }

  openTrainingIssuesWizard() {
    this._router.navigate(['/evaluation/training-issues/create']);
  }

  searchUpdate(event: any) {
    const searchText = event.target.value.toLowerCase();
    this.searchText = searchText;
    this.filterData();
  }

  editTrainingIssue(row: TrainingIssueOverview_TrainingIssue_VM) {
    this._router.navigate(['/evaluation/training-issues/edit/' + row?.id]);
  }

  viewActionItem(row: TrainingIssueOverview_TrainingIssue_VM) {
    this._router.navigate(['/evaluation/training-issues/' + row?.id + '/actionItems']);
  }

  async copyTrainingIssue(id: string) {
    await this.trainingIssuesService.copyTrainingIssueByIdAsync(id).then(async (res) => {
        this.alert.successToast('Training Issue Successfully Copied');
        this._router.navigate(['/evaluation/training-issues/edit/' + res?.id]);
      })
      .catch((error) => {
        this.alert.errorToast('Failed to copy simulator scenarios');
      });
  }

  async inctivateTrainingIssue(row:any) {
    await this.trainingIssuesService.inactiveAsync(this.trainingIssueId).then(async (res) => {
      await this.getOverviewAsync();
      this.alert.successToast('Training Issue Inactivated Successfully');
    })
    .catch((error) => {
      this.alert.errorToast('Failed to Inactivate Training Issue');
    });
  }

  async activateTrainingIssue(row: any) {
    await this.trainingIssuesService.activeAsync(row.id).then(async (res) => {
      row.active=true;
      this.alert.successToast('Training Issue Activated Successfully');
    })
    .catch((error) => {
      this.alert.errorToast('Failed to Activate Training Issue');
    });
  }

  openDialog(templateRef: any, row) {
    this.trainingIssueId = row.id;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteTrainingIssueById() {
    await this.trainingIssuesService.deleteTrainingIssueByIdAsync(this.trainingIssueId).then(async (res) => {
        this.alert.successToast('Training Issue Deleted Successfully');
        await this.loadAsync();
      })
      .catch((error) => {
        this.alert.errorToast('Failed to delete training issue');
      });
  }

  filterData(){
    this.filteredTrainingIssue = this.trainingIssueOverviewstats.trainingIssues;

    if (this.searchText) {
      const searchTextLower = this.searchText.toLowerCase();
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((item) => {
        return (
          item?.issueCode?.toLowerCase().includes(searchTextLower) ||
          item?.issueTitle?.toLowerCase().includes(searchTextLower) ||
          item?.pendingActionItems?.toLowerCase().includes(searchTextLower)
        );
      });
    }

    if (this.filterValues?.status) {
      this.applyStatusFilter();
    }

    if (this.filterValues?.driver) {
      this.applyDriverFilter();
    }

    if (this.filterValues?.driverSubType) {
      this.applyDriverSubFilter(this.filterValues?.driverSubType);
    }

    if (this.filterValues?.severity) {
      this.applySeverityFilter();
    }

    if (this.filterValues?.dueDate) {
      this.applyDueDateFilter(this.filterValues?.dueDate);
    }
    if (this.filterValues?.activeStatus) {
      this.applyActiveStatusFilter();
    }

    this.dataSource = new MatTableDataSource(this.filteredTrainingIssue);
    setTimeout(()=>{
      this.dataSource.sort = this.sort;
    },1);
  
  }

  getTrainingIssuesFilterValues(value: any) {
    this.appliedFilters=[];
    this.filterValues = value;
    if (this.isAnyPropertySet(value)) {
      this.appliedFilters = [{ ...value }];
    } else {
      this.appliedFilters = [];
    }
    this.filterData();
    if (Object.values(this.filterValues).every(value => value === null || value === undefined || value === '')){
      this.dataSource = new MatTableDataSource(this.filterTrainingIssueOnDashboard);
    }
   }

  applyStatusFilter() {
    if (this.filterValues?.status === 'Open') {
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.status === 'Open');
    }
    if (this.filterValues?.status === 'Closed') {
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.status === 'Closed');
    }
  }

  applyDriverFilter() {
    if (this.filterValues?.driver === 'Other') {
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.driverType === 'Other');
    }
    if (this.filterValues?.driver === 'Survey Results') {
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.driverType === 'Survey Results');
    }
    if (this.filterValues?.driver === 'Team Feedback') {
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.driverType === 'Team Feedback');
    }
  }

  applyDriverSubFilter(subType:string){
    this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.driverSubType === subType);
  }

  applySeverityFilter() {
    if (this.filterValues?.severity === 'Low') {
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.severity === 'Low');
    }
    if (this.filterValues?.severity === 'Medium') {
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.severity === 'Medium');
    }
    if (this.filterValues?.severity === 'High') {
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.severity === 'High');
    }
  }

  applyDueDateFilter(dueDate: string) {
    this.filteredTrainingIssue = this.filteredTrainingIssue.filter((issue) => {
      const dueDateWithoutTime = issue.dueDate.substring(0, 10);
      return dueDateWithoutTime === dueDate;
    });
  }

  applyActiveStatusFilter() {
    if (this.filterValues?.activeStatus == 'Active') {
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.active == true);
    }
    if (this.filterValues?.activeStatus == 'Inactive') {
      this.filteredTrainingIssue = this.filteredTrainingIssue.filter((x) => x.active == false);
    }
  }

  filterByTilesClick(value: string) {
    this.clearFlypanelFilters();
    this.filteredTrainingIssue = this.trainingIssueOverviewstats.trainingIssues;;
    if(value == "Open"){
      this.filteredTrainingIssue =this.filteredTrainingIssue.filter((x) => x.status === 'Open');
      this.filterValues.status=value;
    }
    else if(value == "Closed"){
      this.filteredTrainingIssue =this.filteredTrainingIssue.filter((x) => x.status === 'Closed');
      this.filterValues.status=value;
    }
    this.dataSource = new MatTableDataSource(this.filteredTrainingIssue);
  }
  clearFlypanelFilters(){
    this.filterValues ={};
    this.appliedFilters =[];
  }

  clearAllFilters(): void {
    this.appliedFilters = [];
    this.filterValues.status ='';
    this.filterValues.activeStatus='';
    this.filterValues.driver ='';
    this.filterValues.driverSubType ='';
    this.filterValues.severity='';
    this.filterValues.dueDate='';
    this.filterData();
  }

  formatFilter(filter: any): string[] {
    const parts = [];
    if (filter.driver) parts.push(`Driver: ${filter.driver}`);
    if (filter.status) parts.push(`Status: ${filter.status}`);
    if (filter.activeStatus) parts.push(`Active: ${filter.activeStatus}`);
    if (filter.dueDate) parts.push(`Due: ${filter.dueDate}`);
    if (filter.severity) parts.push(`Severity: ${filter.severity}`);
    if (filter.driverSubType) parts.push(`Subtype: ${filter.driverSubType}`);
    return parts;
  }
  
  private isAnyPropertySet(obj: Record<string, any>): boolean {
    return Object.values(obj)
      .some(v => v !== null && v !== undefined && v !== '');
  }
  
}
