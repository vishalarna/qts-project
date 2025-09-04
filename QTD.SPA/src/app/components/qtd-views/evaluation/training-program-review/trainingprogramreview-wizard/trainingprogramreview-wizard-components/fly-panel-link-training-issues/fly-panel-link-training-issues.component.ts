import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { TrainingIssue_VM } from '@models/TrainingIssues/TrainingIssue_VM';
import { TrainingProgramTrainingIssueLinkOption } from '@models/TrainingProgram/TrainingProgramTrainingIssueLinkOption';
import { TrainingIssuesService } from 'src/app/_Services/QTD/TrainingIssues/training-issues.service';
import { ApiTrainingProgramReviewService } from 'src/app/_Services/QTD/TrainingProgramReview/api.trainingProgramReview.Service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-link-training-issues',
  templateUrl: './fly-panel-link-training-issues.component.html',
  styleUrls: ['./fly-panel-link-training-issues.component.scss'],
})
export class FlyPanelLinkTrainingIssuesComponent implements OnInit {
  @Input() dataElementTypeId = '';
  @Input() dataElementType = '';
  @Input() alreadyLinkedIds: any;
  @Output() closed = new EventEmitter<any>();
  trainingIssuesListData: MatTableDataSource<any> = new MatTableDataSource();
  trainingIssuesColumns: string[] = [
    'index',
    'issueCode',
    'issueTitle',
    'description',
    'createdDate',
    'dueDate',
    'severityLevel',
    'status',
  ];
  selectedTrainingIssueId: string;
  description: string = '';
  selection = new SelectionModel<any>(true, []);
  trainingIssues: TrainingIssue_VM[];
  dataElement: TrainingIssue_DataElement_VM =new TrainingIssue_DataElement_VM();
  addTrainingIssueFlyIn : boolean = false;
  constructor(
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    private trainingissueService: TrainingIssuesService,
    private trainingProgramReviewService:ApiTrainingProgramReviewService,
    private alertService:SweetAlertService
  ) {}

  ngOnInit(): void {
    this.getTrainingIssuesByDataElementType(
      this.dataElementTypeId,
      this.dataElementType
    );
    this.getAllDataElementWithCategories();
  }

  async getTrainingIssuesByDataElementType(id: string, type: string) {
    this.trainingIssues =
      await this.trainingissueService.getTrainingissueByDataElementTypeAsync(
        id,
        type
      );
    this.makeTrainingIssueTable(this.trainingIssues);
  }

  async getAllDataElementWithCategories() {
    var dataElementCategories =
      await this.trainingissueService.getAllDataElementsWithCategoriesAsync();
    this.dataElement = dataElementCategories
      .reduce((acc, curr) => acc.concat(curr.dataElementVMs), [])
      .find((vm) => vm.dataElementType === this.dataElementType);
  }

  makeTrainingIssueTable(trainingIssue: TrainingIssue_VM[]) {
    this.trainingIssuesListData = new MatTableDataSource(trainingIssue);
  }

  refreshTable(e: any) {
    this.trainingIssues.push(e);
    this.makeTrainingIssueTable(this.trainingIssues);
  }

  removeTrainingIssueDialog(templateRef: any, trainingIssue: any) {
    this.selectedTrainingIssueId = trainingIssue.id;
    this.description = `You are selecting to remove ${
      trainingIssue.issueCode + ' - ' + trainingIssue.issueTitle
    } from the Training Program.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async unlinkTrainingIssue(){
    var option = new TrainingProgramTrainingIssueLinkOption();
    option.trainingIssueIds.push(this.selectedTrainingIssueId);
    await this.trainingProgramReviewService.removeTrainingProgramTrainingIssuesLinks(this.dataElementTypeId,option);
    this.alreadyLinkedIds = this.alreadyLinkedIds.filter(r=>!option.trainingIssueIds.includes(r));
    this.alertService.successToast("Training Issue Unlinked to the Training Program Review");
    this.getTrainingIssuesByDataElementType(this.dataElementTypeId,this.dataElementType);
  }

  async linkTrainingIssueTotrainingProgram()
  {
    var options = new TrainingProgramTrainingIssueLinkOption();
    var selected = this.selection.selected;
    options.trainingIssueIds = selected.map(m=>m.id);
    await this.trainingProgramReviewService.createTrainingProgramTrainingIssuesLinks(this.dataElementTypeId,options);
    this.alertService.successToast("Training Issue Linked to the Training Program Review");
    this.closed.emit();
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.trainingIssues.filter(
      (x) => !this.alreadyLinkedIds?.some((z) => z == x.id)
    ).length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.trainingIssues
          .filter(
            (x) => !this.alreadyLinkedIds?.some((z) => z == x.id)
          )
          .forEach((row) => {
            this.selection.select(row);
          });
  }
}
