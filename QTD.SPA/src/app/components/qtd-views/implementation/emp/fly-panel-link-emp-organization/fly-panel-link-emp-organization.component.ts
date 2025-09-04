import {TemplatePortal} from '@angular/cdk/portal';
import {Component, EventEmitter, Input, OnInit, Output, ViewContainerRef} from '@angular/core';
import {MatLegacyDialog as MatDialog} from '@angular/material/legacy-dialog';
import {ActivatedRoute, Router} from '@angular/router';
import {Organization} from 'src/app/_DtoModels/Organization/Organization';
import {OrganizationIdAndNameVM} from 'src/app/_DtoModels/Organization/OrganizationIdAndNameVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import {EmployeesService} from 'src/app/_Services/QTD/employees.service';
import {OrganizationsService} from 'src/app/_Services/QTD/organizations.service';
import {PositionsService} from 'src/app/_Services/QTD/positions.service';
import {TasksService} from 'src/app/_Services/QTD/tasks.service';
import {FlyInPanelService} from 'src/app/_Shared/services/flyInPanel.service';
import {SweetAlertService} from 'src/app/_Shared/services/sweetalert.service';
import {SubSink} from 'subsink';

@Component({
  selector: 'app-fly-panel-link-emp-organization',
  templateUrl: './fly-panel-link-emp-organization.component.html',
  styleUrls: ['./fly-panel-link-emp-organization.component.scss']

})
export class FlyPanelLinkEmpOrganizationComponent implements OnInit {
  linkPos: boolean = true;
  addOrganization: boolean = false;
  showActive: boolean = true;
  isLoading: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinkedIds: any[] = [];
  @Input() empfromAddView: any
  linkedIds: any[] = [];
  organizations: OrganizationIdAndNameVM[];
  filteredList: OrganizationIdAndNameVM[];
  subscription = new SubSink();
  empID = '';
  deleteDescription = "";
  linkedEmps: any[] = [];
  modalType: "Add" | "Edit";
  employeeIdDelete: any;
  org: any;
  @Output() confirmed = new EventEmitter<any>();

  constructor(
    private posSrvc: PositionsService,
    private orgService: OrganizationsService,
    private empSrvc: EmployeesService,
    private taskSrvc: TasksService,
    private activatedRoute: ActivatedRoute,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private route: Router,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private labelPipe: LabelReplacementPipe
  ) {
  }

  ngOnInit(): void {
    let segments = this.route.url.split('/')

    // if (segments.includes('edit')) {
    //   this.empID = segments[segments.length - 1];
    // }
    // else {
    this.empID = this.empfromAddView;
    // }
    this.getOrganizations();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }    

  linkToEmployee() {

    let option =
      {
        organizationIds: this.linkedIds,
      };
    this.empSrvc.LinkOrganizationtoEmployee(this.empID, option).then(async (res) => {
      this.refresh.emit('refresh position tbl');
      this.closed.emit('fp-link-pos-task-closed');
      this.alert.successToast('Organizations linked to ' + await this.transformTitle('Employee'));

    });


  }

  orgChecked(checked: boolean, id: any) {
    if (checked) {
      this.linkedIds.push(id);
    } else {
      this.linkedIds.splice(this.linkedIds.indexOf(id), 1);
    }
    this.linkedIds = [...new Set(this.linkedIds)];
  }

  async getPositions() {
    await this.posSrvc.getAll().then((res) => {
      // this.positions = res;
      // this.filteredList = res;
    });
  }

  async deleteProcedure(templateRef: any, orgid: any) {

    this.deleteDescription = `The following Organization Manager and ` + await this.labelPipe.transform('Employee') + `s have been assigned to this organization`;
    await this.orgService.get(orgid).then((res) => {
      this.org = res;

    });
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  filteredAndSortedList: any;

  async getOrganizations() {

    await this.orgService.getAllSimplified().then((res) => {

      res.forEach((item, idx) => {
        item.employeeOrganizations.sort((a, b) => a.empFirstName > b.empFirstName ? 1 : -1);
      })

      this.organizations = res.sort((a,b) => a.name.toLowerCase() > b.name.toLowerCase() ? 1 : -1 );
      this.filteredList = res.sort((a,b) => a.name.toLowerCase() > b.name.toLowerCase() ? 1 : -1 );
    });
  }

  editOragnizationCheck: boolean = false;

  async openFlyInPanel(orgid: any) {
    this.modalType = "Edit";
    await this.orgService.get(orgid).then((res) => {
      this.org = res;
    });
    this.editOragnizationCheck = true;
    /*   const portal = new TemplatePortal(templateRef, this.vcf);
      this.flyPanelService.open(portal); */
  }

  newOrganizationCheck: boolean = false;

  openFlyInPanelOrganiiztion(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  searchFilter(event: any) {
    let filter = (event.target as HTMLInputElement).value;
    this.filteredList = 
      this.organizations.filter((c) =>
            c.name.toLowerCase().includes(filter.toLowerCase())
          )
  }
}
