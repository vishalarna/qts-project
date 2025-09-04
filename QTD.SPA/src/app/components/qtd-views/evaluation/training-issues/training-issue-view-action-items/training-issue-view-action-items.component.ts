import { Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { Store } from '@ngrx/store';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { TrainingIssue_ActionItems_VM } from '@models/TrainingIssues/TrainingIssue_ActionItems_VM';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { TrainingIssueActionItemsTableComponent } from '../training-issue-action-items-table/training-issue-action-items-table.component';

@Component({
  selector: 'app-training-issue-view-action-items',
  templateUrl: './training-issue-view-action-items.component.html',
  styleUrls: ['./training-issue-view-action-items.component.scss']
})
export class TrainingIssueViewActionItemsComponent implements OnInit {
  @ViewChild('closeTrainingIssueStatusDialog') closeStatusDialog: TemplateRef<any>;
  mode:'edit' | 'view';
  trainingIssueDetail:TrainingIssue_VM;
  trainingIssueId : string;
  isActionItemLoading:boolean;
  allCompleted:boolean;
  checkStatus:boolean =false;
  @ViewChild('actionItemTable') actionItemTable: TrainingIssueActionItemsTableComponent;
  constructor(
    private store: Store<{ toggle: string }>,
    private router: Router,
    public dialog: MatDialog,
    private route: ActivatedRoute,
    private apiTrainingIssueService: TrainingIssuesService,
    private alert: SweetAlertService,
  ) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    this.loadAsync();
    this.isActionItemLoading = false;
  }

  async loadAsync(){
    this.mode = 'view';
    await this.getIdFromRoute();
  }

  async getIdFromRoute() {
    await this.route.params.subscribe(async (params) => {
      this.trainingIssueId = params['id'];
    })
    await this.getTrainingIssueDetails(this.trainingIssueId);
  }

  async getTrainingIssueDetails(id:string){
    this.isActionItemLoading = true;
    this.trainingIssueDetail = await this.apiTrainingIssueService.getAsync(id);
    this.isActionItemLoading = false;
  }

  goBack(){
    this.router.navigate(['evaluation/training-issues/overview']);
  }
  
  changeActionItemMode(){
    this.mode = 'edit';
    this.actionItemTable.filterValues = null;
    this.actionItemTable.actionItemDataSource.data = this.trainingIssueDetail.actionItems;
  }

  async closeAndSaveTrainingIssueStatus(updateTrainingissueStatus:boolean){
    var actionItems = new TrainingIssue_ActionItems_VM();
    actionItems.actionItem_VMs = this.trainingIssueDetail.actionItems;
    const allCompleted = this.checkIfAllActionStepsCompleted();
    if(allCompleted && updateTrainingissueStatus){
      this.checkStatus=true;
    }
    var result = await this.apiTrainingIssueService.updateActionItemsAsync(actionItems,this.trainingIssueId,this.checkStatus);
    await this.getIdFromRoute();
    this.alert.successToast("Training Issue Action Items Saved Successfully");
    this.mode = 'view';
  }

  checkIfAllActionStepsCompleted(): boolean {
    const hasActionItems = this.trainingIssueDetail.actionItems.length > 0;
    const result = this.trainingIssueDetail.actionItems.every(item => item.status === 'Completed' && item.dateCompleted);
    return result && hasActionItems;
  }

  async saveActionItem(){
    if(this.actionItemTable != null && this.actionItemTable != undefined){
      this.trainingIssueDetail.actionItems = this.actionItemTable.actionItemDataSource.data;
    }
    this.allCompleted = this.trainingIssueDetail.actionItems.length > 0 && this.trainingIssueDetail.actionItems.every(element => element.statusId == '3q');
    if (this.allCompleted) {
        await this.openClosedStatusDialog();
    } else {
      await this.closeAndSaveTrainingIssueStatus(true)
    }
  }

  openClosedStatusDialog(){
    this.dialog.open(this.closeStatusDialog, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    })
  }

}
