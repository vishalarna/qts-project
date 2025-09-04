import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Organization } from 'src/app/_DtoModels/Organization/Organization';
import { OrganizationUpdateOptions } from 'src/app/_DtoModels/Organization/OrganizationUpdateOptions';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flyPanel-organizations',
  templateUrl: './flyPanel-organizations.component.html',
  styleUrls: ['./flyPanel-organizations.component.scss'],
})
export class FlyPanelOrganizationComponent implements OnInit {
  addOrganizationModalVisible: boolean = false;
  organizations: Organization[] = [];
  orgName: string = '';
  orgHeader: string = '';
  showlistOrganization: boolean = true;
  listOrganization: Organization[] = [];
  tempOrgList: Organization[] = [];
  editOrgId!: any;

  OrgUpdateOpt!: OrganizationUpdateOptions;

  constructor(
    private orgService: OrganizationsService,
    public translate: TranslateService,

    private alert: SweetAlertService,
    private DataBroadcastService: DataBroadcastService,
    public flyInPanelSrvc: FlyInPanelService
  ) {}

  ngOnInit(): void {
    this.getOrganizationList();
  }

  async createNewOrganization() {
    await this.orgService.create({ name: this.orgName, publicOrganization:false }).then((res) => {
      if (res) {
        this.listOrganization = [];
        this.getOrganizationList();
        this.orgName = '';
        this.DataBroadcastService.refreshListName.next('organizations');
        this.alert.successToast(this.translate.instant(`L.recAdded`));
      }
    });
  }

  async updateOrganization() {
    await this.orgService
      .update(this.editOrgId, this.OrgUpdateOpt)
      .then((res) => {
        if (res) {
          // this.orgName = '';
          this.listOrganization = [];
          this.DataBroadcastService.refreshListName.next('organizations');
          this.alert.successToast(this.translate.instant(`L.recUpdated`));
        }
      });
  }

  async getOrganizationList() {
    if (this.listOrganization.length == 0) {
      await this.orgService
        .getAll()
        .then((data) => (this.listOrganization = this.tempOrgList = data));
    }
    this.orgHeader = 'Organizations';
    this.showlistOrganization = true;
  }

  async deleteOrganization(id: any) {
    await this.orgService.delete(id).then((res) => {
      if (res) {
        this.showlistOrganization = true;
        this.listOrganization = [];
        this.getOrganizationList();
        this.orgName = '';
        this.DataBroadcastService.refreshListName.next('organizations');
        this.alert.successToast(this.translate.instant(`L.recdelete`));
      }
    });
  }

  editOrganization(org: Organization) {
    this.orgHeader = 'Edit Organization';

    this.OrgUpdateOpt = new OrganizationUpdateOptions();
    this.OrgUpdateOpt.name = org.name;
    this.editOrgId = org.id;
    this.showlistOrganization = false;
  }

  addOrganization() {
    this.orgName = '';
    this.orgHeader = 'Add New Organization';
    this.showlistOrganization = false;
  }

  filterOrganziations(filter: any) {
    let f = filter.target as HTMLInputElement;
    this.listOrganization = this.tempOrgList;
    this.listOrganization = this.listOrganization.filter(
      (item) => item.name.toLowerCase().indexOf(f.value.toLowerCase()) > -1
    );
  }

  toggleAddOrganizationModal() {
    this.addOrganizationModalVisible = !this.addOrganizationModalVisible;

    if (this.addOrganizationModalVisible) {
      this.showlistOrganization = true;
      this.getOrganizationList();
    }

    if (!this.showlistOrganization)
      this.addOrganizationModalVisible
        ? ((this.orgHeader = 'Add New Organization'), (this.orgName = ''))
        : (this.orgHeader = 'Edit Organization');
    else this.orgHeader = 'Organizations';
  }
}
