import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Organization } from 'src/app/_DtoModels/Organization/Organization';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';

@Component({
  selector: 'app-flypanel-filter-emp-by-org',
  templateUrl: './flypanel-filter-emp-by-org.component.html',
  styleUrls: ['./flypanel-filter-emp-by-org.component.scss']
})
export class FlypanelFilterEmpByOrgComponent implements OnInit {
  filterForm = new UntypedFormGroup({});
  @Output() closed = new EventEmitter<any>();
  @Input() selectedOrg = "";
  @Output() orgSelected = new EventEmitter<any>();
  @Output() selectedOrganization = new EventEmitter<string>();

  organizations:Organization[] = [];
  selectedOrganizationName: string = "";

  constructor(
    private orgService : OrganizationsService,
  ) { }

  ngOnInit(): void {
    this.filterForm.addControl('org', new UntypedFormControl(this.selectedOrg,Validators.required));
    this.readyOrgs();
  }

  async readyOrgs(){
    this.organizations = await this.orgService.getAll();
  }

  selectOrg(){
    var org = this.filterForm.get('org')?.value;
    this.selectedOrganizationName = this.organizations?.find(x => x.id == org)?.name;
    this.orgSelected.emit(org);
    this.selectedOrganization.emit(this.selectedOrganizationName);
  }

}
