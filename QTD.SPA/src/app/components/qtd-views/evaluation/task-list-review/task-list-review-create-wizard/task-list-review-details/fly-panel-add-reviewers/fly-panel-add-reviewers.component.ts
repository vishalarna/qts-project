import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Organization } from '@models/Organization/Organization';
import { Position } from '@models/Position/Position';
import { QtdUserVM } from '@models/QtdUser/QtdUserVM';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
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
  organizations: Organization[];
  positions: Position[];
  selectedOrganization: Organization;
  selectedPosition: Position;
  organizationSearchText: string = '';
  positionSearchText: string = '';
  filteredOrganizations: any[] = [];
  filteredPositions: any[] = [];

  constructor(
    private flypanelSrvc: FlyInPanelService,
    private qtdSrvc: QTDService,
    private alert: SweetAlertService,
    private orgService: OrganizationsService,
    private positionService: PositionsService,
  ) {}

  ngOnInit(): void {
    this.displayedColumns = ['id', 'allReviewers'];
    this.getReviewersData();
    this.GetPositionAndOrgsForFilter();
    this.isFlyPanelAddNewQTDUser = false;
  }

  async getReviewersData() {
    this.selection.clear();
    this.reviewerData = await this.qtdSrvc.getAllActiveWithEmployeeDataAsync();
    this.reviewerDataSource = new MatTableDataSource(this.reviewerData);
  }

  async GetPositionAndOrgsForFilter() {
    await this.orgService.getAllOrderBy('name').then((x) => {
      this.organizations = x;
      this.filteredOrganizations = [...x]; 
    });

    await this.positionService.getAllOrderBy('name').then((x) => {
      this.positions = x;
      this.filteredPositions = [...x]; 
    });
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

  private getSelectableRows() {
    return this.reviewerDataSource.filteredData
      .filter(row => !this.alreadyLinkedReviewers?.includes(row.id));
  }

  isAllSelected() {
    const selectableRows = this.getSelectableRows();
    const numSelected = selectableRows.filter(row => this.selection.isSelected(row)).length;
    return numSelected > 0 && numSelected === selectableRows.length;
  }

  isSomeSelected() {
    const selectableRows = this.getSelectableRows();
    const numSelected = selectableRows.filter(row => this.selection.isSelected(row)).length;
    return numSelected > 0 && numSelected < selectableRows.length;
  }

  masterToggle() {
    if (this.isAllSelected()) {
      this.reviewerDataSource.filteredData.forEach(row => {
        this.selection.deselect(row);
      });
    } else {
      this.reviewerDataSource.filteredData
        .filter(row => !this.alreadyLinkedReviewers?.includes(row.id))
        .forEach(row => {
          this.selection.select(row);
        });
    }
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

  currentSearchText: string = '';

  filterQTDUsers(){
    var searchText = this.currentSearchText.toLowerCase();

    this.reviewerDataSource.data = this.reviewerData.filter(qtdUser => {
      var matchesSearch =
        `${qtdUser.person.firstName} ${qtdUser.person.lastName}`.toLowerCase().includes(searchText);

      var matchesPosition = this.selectedPosition
        ? qtdUser.person.employee?.employeePositions?.some(p => p.positionId === this.selectedPosition.id)
        : true;

      var matchesOrganization = this.selectedOrganization
        ? qtdUser.person.employee?.employeeOrganizations?.some(o => o.organizationId === this.selectedOrganization.id)
        : true;

      return matchesSearch && matchesPosition && matchesOrganization;
    });
  }

  searchQTDUsers(event: any) {
    this.currentSearchText = event.target.value;
    this.filterQTDUsers();
  }

  setDropdownValue(event: any, type: string) {
    if (type === 'organization') {
      this.selectedOrganization = event.value;
    } else if (type === 'position') {
      this.selectedPosition = event.value;
    }

    this.filterQTDUsers();
  }

  filterOrganizations() {
    var search = this.organizationSearchText.toLowerCase();
    this.filteredOrganizations = this.organizations.filter(org =>
      org.name.toLowerCase().includes(search)
    );
  }

  filterPositions() {
    var search = this.positionSearchText.toLowerCase();
    this.filteredPositions = this.positions.filter(pos =>
      pos.positionTitle.toLowerCase().includes(search)
    );
  }

  clearOrganization() {
    this.selectedOrganization = null;
    this.filteredOrganizations = [...this.organizations];
    this.filterQTDUsers();
  }

  clearPosition() {
    this.selectedPosition = null;
    this.filteredPositions = [...this.positions];
    this.filterQTDUsers();
  }
}
