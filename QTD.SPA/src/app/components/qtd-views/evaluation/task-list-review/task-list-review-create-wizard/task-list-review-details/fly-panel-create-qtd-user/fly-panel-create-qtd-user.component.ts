import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Organization } from '@models/Organization/Organization';
import { Person } from '@models/Person/Person';
import { Position } from '@models/Position/Position';
import { QtdUserVM } from '@models/QtdUser/QtdUserVM';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { PersonsService } from 'src/app/_Services/QTD/persons.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { QTDService } from 'src/app/_Services/QTD/qtd.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-create-qtd-user',
  templateUrl: './fly-panel-create-qtd-user.component.html',
  styleUrls: ['./fly-panel-create-qtd-user.component.scss'],
})
export class FlyPanelCreateQtdUserComponent implements OnInit {
  @Output() closed = new EventEmitter<boolean>();
  @Output() newReviewers = new EventEmitter<QtdUserVM[]>();
  isNewPersonFlyPanel: boolean;
  selection = new SelectionModel<Person>(true, []);
  personData: Person[];
  organizations: Organization[];
  positions: Position[];
  selectedOrganization: Organization;
  selectedPosition: Position;
  organizationSearchText: string = '';
  positionSearchText: string = '';
  filteredOrganizations: any[] = [];
  filteredPositions: any[] = [];
  personDataSource: MatTableDataSource<Person>;
  displayedColumns: string[];
  constructor(
    private personSrvc: PersonsService,
    private qtdService: QTDService,
    private orgService: OrganizationsService,
    private positionService: PositionsService,
    private alert: SweetAlertService
  ) {}

  ngOnInit(): void {
    this.displayedColumns = ['id', 'allPersons'];
    this.getAllPersons();
    this.GetPositionAndOrgsForFilter();
    this.isNewPersonFlyPanel = false;
  }

  async getAllPersons() {
    this.selection.clear();
    this.personData = await this.personSrvc.getPersonsWithoutQtdUser();
    this.personDataSource = new MatTableDataSource(this.personData);
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

  closeQtdFlyPanel() {
    this.closed.emit(false);
  }

  async CreateQtdUser() {
    var newQtdUsers : QtdUserVM[] = [] ;
    for(var person of this.selection.selected){
      var qtdUserOption = new QtdUserVM();
      qtdUserOption.person = person;
      await this.qtdService.createAsync(qtdUserOption).then(user => {
        newQtdUsers.push(user);
      });
    }
    this.alert.successToast('QTD User(s) Created Successfully');
    this.newReviewers.emit(newQtdUsers);
    this.closeQtdFlyPanel();
  }

  isAllSelected() {
    var filteredRows = this.personDataSource.filteredData;
    var numSelected = filteredRows.filter(row => this.selection.isSelected(row)).length;
    var numRows = filteredRows.length;
    return numSelected === numRows && numRows > 0;
  }

  masterToggle() {
    if (this.isAllSelected()) {
      this.personDataSource.filteredData.forEach(row => this.selection.deselect(row));
    } else {
      this.personDataSource.filteredData.forEach(row => this.selection.select(row));
    }
  }

  openAddNewUserFlyPanel() {
    this.isNewPersonFlyPanel = true;
  }
  closeNewUserFlyPanel() {
    this.isNewPersonFlyPanel = false;
  }

  getNewUser(person: Person) {
    this.personData.push(person);
    this.selection.select(person);
    this.personDataSource.data = this.personData;
  }

  currentSearchText: string = '';

  filterPersons(){
    var searchText = this.currentSearchText.toLowerCase();

    this.personDataSource.data = this.personData.filter(person => {
      var matchesSearch =
        `${person.firstName} ${person.lastName}`.toLowerCase().includes(searchText);

      var matchesPosition = this.selectedPosition
        ? person.employee?.employeePositions?.some(p => p.positionId === this.selectedPosition.id)
        : true;

      var matchesOrganization = this.selectedOrganization
        ? person.employee?.employeeOrganizations?.some(o => o.organizationId === this.selectedOrganization.id)
        : true;

      return matchesSearch && matchesPosition && matchesOrganization;
    });
  }

  searchPersons(event: any) {
    this.currentSearchText = event.target.value;
    this.filterPersons();
  }

  setDropdownValue(event: any, type: string) {
    if (type === 'organization') {
      this.selectedOrganization = event.value;
    } else if (type === 'position') {
      this.selectedPosition = event.value;
    }

    this.filterPersons();
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
    this.filterPersons();
  }

  clearPosition() {
    this.selectedPosition = null;
    this.filteredPositions = [...this.positions];
    this.filterPersons();
  }
}
