import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { QtdUserVM } from '@models/QtdUser/QtdUserVM';
import { QTDService } from 'src/app/_Services/QTD/qtd.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-reviewers',
  templateUrl: './fly-panel-add-reviewers.component.html',
  styleUrls: ['./fly-panel-add-reviewers.component.scss'],
})
export class FlyPanelAddReviewersComponent implements OnInit {
  @Input() alreadyLinkedReviewers: string[];
  @Output() reviewerList = new EventEmitter<QtdUserVM[]>();
  @Output() qtdUsersCreated = new EventEmitter<QtdUserVM[]>();
  isFlyPanelAddNewQTDUser: boolean;
  selection = new SelectionModel<QtdUserVM>(true, []);
  reviewerData: QtdUserVM[];
  displayedColumns: string[];
  reviewerDataSource: MatTableDataSource<QtdUserVM>;

  constructor(
    private flypanelSrvc: FlyInPanelService,
    private qtdSrvc: QTDService,
    private alert: SweetAlertService
  ) {}

  ngOnInit(): void {
    this.displayedColumns = ['id', 'allReviewers'];
    this.getReviewersData();
    this.isFlyPanelAddNewQTDUser = false;
  }

  async getReviewersData() {
    this.selection.clear();
    this.reviewerData = await this.qtdSrvc.getAllActiveAsync();
    this.reviewerDataSource = new MatTableDataSource(this.reviewerData);
  }

  isLinked(rId: string) {
    return this.alreadyLinkedReviewers?.some((item) => item == rId);
  }

  closeFlyPanel() {
    this.flypanelSrvc.close();
  }

  linkToReview() {
    this.reviewerList.emit(this.selection.selected);
    this.alert.successToast('Reviewer Added successfully');
    this.closeFlyPanel();
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.reviewerData.filter(
      (x) => !this.alreadyLinkedReviewers?.some((z) => z == x.id)
    ).length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.reviewerData
          .filter(
            (x) => !this.alreadyLinkedReviewers?.some((z) => z == x.id)
          )
          .forEach((row) => {
            this.selection.select(row);
          });
  }

  openAddNewReviewerFlyPanel() {
    this.isFlyPanelAddNewQTDUser = true;
  }

  closeNewReviewerFlyPanel(value: any) {
    this.isFlyPanelAddNewQTDUser = value;
  }

  getNewReviewers(reviewers: QtdUserVM[]) {
    this.reviewerData = [...this.reviewerData, ...reviewers];
    this.selection.select(...reviewers);
    this.reviewerDataSource = new MatTableDataSource(this.reviewerData);
    this.qtdUsersCreated.emit(this.reviewerData);
  }
}
