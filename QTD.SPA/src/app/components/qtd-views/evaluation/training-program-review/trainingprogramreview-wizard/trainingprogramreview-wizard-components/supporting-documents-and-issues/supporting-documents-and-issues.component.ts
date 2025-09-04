import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TrainingProgramTrainingIssueLinkOption } from '@models/TrainingProgram/TrainingProgramTrainingIssueLinkOption';
import { TrainingProgramReview_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_ViewModel';
import { ApiTrainingProgramReviewService } from 'src/app/_Services/QTD/TrainingProgramReview/api.trainingProgramReview.Service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-supporting-documents-and-issues',
  templateUrl: './supporting-documents-and-issues.component.html',
  styleUrls: ['./supporting-documents-and-issues.component.scss'],
})
export class SupportingDocumentsAndIssuesComponent implements OnInit, OnDestroy {
  @Input() inputTPRViewModel:TrainingProgramReview_ViewModel;
  @Input() handleLoad: () => void | undefined;
  linkTrainingIssuesData :MatTableDataSource<any> = new MatTableDataSource();
  linkTrainingIssueColumns: string[] = ['id','issueCode','issueTitle', 'description','createdDate','dueDate','severityLevel','status','action'];
  linkedTrainingIssueIds:any[];
  selectedTrainingIssueId:any[] = [];
  description:string='';
  pendingUnlinkRows:any[]=[];
  selection = new SelectionModel<any>(true, []);
  constructor(
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    private trainingProgramReviewService:ApiTrainingProgramReviewService,
    public dialog: MatDialog,
  ) {}

  ngOnInit(): void {
    this.load();
  }
  ngOnDestroy(): void {}
  
  _handleLoad() {
    if (this.handleLoad &&typeof this.handleLoad === 'function') {
      this.handleLoad();
    }
  }

  async load() {
    this._handleLoad();
    await this.refreshTrainingIssueTable();
  }

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async unlinkTrainingIssue(){
    var option = new TrainingProgramTrainingIssueLinkOption();
    option.trainingIssueIds = this.pendingUnlinkRows.map(r => r.id);
    await this.trainingProgramReviewService.removeTrainingProgramTrainingIssuesLinks(this.inputTPRViewModel?.trainingProgramId,option);
    this.refreshTrainingIssueTable();
  }
  
  removeTrainingIssueDialog(templateRef: any,singleRow?:any){
    if (singleRow) {
      this.pendingUnlinkRows = [ singleRow ];
    } else {
      this.pendingUnlinkRows = this.selection.selected.slice();
    }

    if (this.pendingUnlinkRows.length === 1) {
      const r = this.pendingUnlinkRows[0];
      this.description = `You are about to remove ${r.issueCode} - ${r.issueTitle} from this Training Program.`;
    } else {
      this.description =
        `You are about to remove ${this.pendingUnlinkRows.length} issues:` +
        `\n• ` + this.pendingUnlinkRows.map(r => `${r.issueCode} - ${r.issueTitle}`).join('\n• ');
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  closeLinkTrainingissueFlyIn(){
    this.flyPanelService.close();
    this.refreshTrainingIssueTable();
  }

  async refreshTrainingIssueTable(){
    var data = await this.trainingProgramReviewService.getTrainingProgramTrainingIssuesLinks(this.inputTPRViewModel?.trainingProgramId);
    this.linkTrainingIssuesData = new MatTableDataSource(data)
    this.linkedTrainingIssueIds = data.map(m=>m.id);
    this.selection.clear();
    this.pendingUnlinkRows = [];
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.linkTrainingIssuesData.data.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.linkTrainingIssuesData.data.forEach((row) => {
          this.selection.select(row);
        });
  }
}
