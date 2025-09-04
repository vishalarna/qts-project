import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Position } from '@models/Position/Position';
import { Organization } from 'src/app/_DtoModels/Organization/Organization';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';

@Component({
  selector: 'app-fly-in-filter-add-dif-survey-employees',
  templateUrl: './fly-in-filter-add-dif-survey-employees.component.html',
  styleUrls: ['./fly-in-filter-add-dif-survey-employees.component.scss']
})
export class FlypanelFilterAddDifSurveyEmpsComponent implements OnInit {
  filterForm = new UntypedFormGroup({});
  @Output() closed = new EventEmitter<any>();
  @Input() selectedOrg: { id: string, name: string } | null = null;
  @Input() selectedPos: { id: string, name: string } | null = null;
  @Output() orgSelected = new EventEmitter<{ id: string, name: string }>();
  @Output() posSelected = new EventEmitter<{ id: string, name: string }>();

  @Output() setFilters = new EventEmitter();
  organizations:Organization[] = [];
  selectedOrganizationName: string = "";
  positions: Position[] = [];
  selectedPositionName : string ="";

  constructor(
    private orgService : OrganizationsService,
    private posService: PositionsService,
  ) { }

  ngOnInit(): void {
    this.filterForm.addControl('org', new UntypedFormControl(this.selectedOrg?.id));
    this.filterForm.addControl('pos', new UntypedFormControl(this.selectedPos?.id));
    this.readyOrgsAsync();
    this.readyPosAsync();
  }

  async readyOrgsAsync(){
    this.organizations = await this.orgService.getAll();
  }
  async readyPosAsync() {
    this.positions = await this.posService.getAllWithoutIncludes();
  }

  applyFilters(){
    const orgId = this.filterForm.get('org')?.value;
    const orgObj = this.organizations.find(x => x.id === orgId);
    if (orgObj) {
      this.orgSelected.emit({ id: orgObj.id, name: orgObj.name });
    }else{
      this.orgSelected.emit(null);
    }
    const posId = this.filterForm.get('pos')?.value;
    const posObj = this.positions.find(x => x.id === posId);
    if (posObj) {
    this.posSelected.emit({ id: posObj.id, name: posObj.positionTitle });
    }else{
      this.posSelected.emit(null);
    }
    this.setFilters.emit();
  }

}
