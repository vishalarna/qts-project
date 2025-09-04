import { TitleCasePipe } from '@angular/common';
import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
  ViewContainerRef,
  ViewEncapsulation,
} from '@angular/core';
import { async } from '@angular/core/testing';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { ClientUser } from 'src/app/_DtoModels/ClientUser/ClientUser';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeeOrganization } from 'src/app/_DtoModels/EmployeeOrganization/EmployeeOrganization';
import { Organization } from 'src/app/_DtoModels/Organization/Organization';
import { ClientUsersService } from 'src/app/_Services/QTD/client-users.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { DeleteEmpPositionComponent } from '../emp-position/delete-emp-position/delete-emp-position.component';
import { DeleteEmpComponent } from '../delete-emp/delete-emp.component';
import { PersonUpdateOption } from 'src/app/_DtoModels/Person/PersonUpdateOption';
import { Person } from 'src/app/_DtoModels/Person/Person';
import { PersonsService } from 'src/app/_Services/QTD/persons.service';
import { DeleteEmpCertificationComponent } from '../emp-certification/delete-emp-certification/delete-emp-certification.component';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { trigger, transition, style, animate } from '@angular/animations';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-edit-view-emp',
  templateUrl: './edit-view-emp.component.html',
  styleUrls: ['./edit-view-emp.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: [
    trigger('fadeSlideInOut', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-100px)' }),
        animate('500ms', style({ opacity: 1, transform: 'translateY(0)' })),
      ]),
      transition(':leave', [
        animate(
          '500ms',
          style({ opacity: 0, transform: 'translateY(-100px)' })
        ),
      ]),
    ]),
  ],
})
export class EditViewEmpComponent implements AfterViewInit, OnDestroy, OnInit {
  activeAccordian: string = 'employee details';
  mode = 'edit';
  title = '';
  isOrganizationDropdownVisible: boolean = false;

  empModel: Employee = new Employee();
  empOrgs: Map<any, string> = new Map<any, string>();
  orgModel: Organization[] = [];
  empId: any;
  personUpdateOpt: PersonUpdateOption = new PersonUpdateOption();
  positions: any[] = [];

  editData: any;
  empCertDataSource: MatTableDataSource<any>;
  empPosDataSource: MatTableDataSource<any>;
  empCertCols: string[] = [
    'certBody',
    'certNo',
    'certArea',
    'issueDate',
    'recertDate',
    'expireDate',
    'obj',
  ];
  empPosCols: string[] = [
    'posName',
    'startDate',
    'Trainee',
    'qualificationDate',
    'tp_version',
    'endDate',
    'obj',
  ];
  @ViewChild('empForm') empForm!: NgForm;
  @ViewChild('certSort') set certSort(sort: MatSort) {
    if (sort) {
      this.empCertDataSource.sort = sort;
    }
  }
  @ViewChild('posSort') set posSort(sort: MatSort) {
    if (sort) {
      this.empPosDataSource.sort = sort;
    }
  }

  @ViewChild('certPaging') set certPaging(paging: MatPaginator) {
    if (paging) this.empCertDataSource.paginator = paging;
  }
  @ViewChild('posPaging') set posPaging(paging: MatPaginator) {
    if (paging) this.empPosDataSource.paginator = paging;
  }

  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private route: Router,
    private dataBroadcastService: DataBroadcastService,
    private empService: EmployeesService,
    private orgService: OrganizationsService,
    private personService: PersonsService,
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);

    this.dataBroadcastService.empFormMode.subscribe((x) => {
      this.mode = x;
      this.title = 'Employee';
    });

    this.empId = this.route.url.substring(this.route.url.lastIndexOf('/') + 1);

    this.empModel.person = new Person();
    this.empModel.employeeCertifications = [];
    this.empModel.employeeOrganizations = [];
    this.empModel.employeePositions = [];

    this.dataBroadcastService.refreshTblName.subscribe((val) => {
      switch (val) {
        case 'cert':
          this.getEmpCertifications();
          break;
        case 'pos':
          this.getEmpPositions();
          break;
      }
    });
  }
  ngAfterViewInit(): void {}
  ngOnDestroy(): void {}

  ngOnInit(): void {
    this.getEmpById();
  }

  async getEmpById() {
    await this.empService.get(this.empId).then((data) => {
      this.empModel = data;
      this.positions = [];
      this.empModel.employeePositions.forEach((item) =>
        this.positions.push(item.position.name)
      );
      this.empOrgs = new Map<any, string>();
      this.empModel.employeeOrganizations.forEach((item) => {
        this.empOrgs.set(item.organization.id, item.organization.name);
      });

      let tempSrc: any[] = [];

      this.empModel.employeeCertifications.forEach((c) => {
        tempSrc.push({
          certBody: c.certification.certifyingBody.name,
          certNo: c.certificationNumber,
          certArea: c.certificationArea,
          issueDate: c.issueDate,
          recertDate: '',
          expireDate: c.expirationDate,
          obj: c,
        });
      });
      this.empCertDataSource = new MatTableDataSource(tempSrc);
      tempSrc = [];

      this.empModel.employeePositions.forEach((p) => {
        tempSrc.push({
          posName: p.position.name,
          startDate: p.startDate,
          Trainee: p.trainee,
          qualificationDate: p.qualificationDate,
          tp_version: '',
          endDate: p.endDate,
          obj: p,
        });
      });
      this.empPosDataSource = new MatTableDataSource(tempSrc);
    });
  }

  async getEmpCertifications() {
    await this.empService.getCertifications(this.empId).then((data) => {
      this.empModel.employeeCertifications = data;
      let tempSrc: any[] = [];

      this.empModel.employeeCertifications.forEach((c) => {
        tempSrc.push({
          certBody: c.certification.certifyingBody.name,
          certNo: c.certificationNumber,
          certArea: c.certificationArea,
          issueDate: c.issueDate,
          recertDate: '',
          expireDate: c.expirationDate,
          obj: c,
        });
      });
      this.empCertDataSource = new MatTableDataSource(tempSrc);
    });
  }

  async getEmpPositions() {
    await this.empService.getPositions(this.empId,'all').then((data) => {
      this.empModel.employeePositions = data;
      this.positions = [];
      data.forEach((item) => this.positions.push(item.position.name));
      let tempSrc: any[] = [];
      this.empModel.employeePositions.forEach((p) => {
        tempSrc.push({
          posName: p.position.name,
          startDate: p.startDate,
          Trainee: p.trainee,
          qualificationDate: p.qualificationDate,
          tp_version: '',
          endDate: p.endDate,
          obj: p,
        });
      });
      this.empPosDataSource = new MatTableDataSource(tempSrc);
    });
  }

  async getEmpOrganizations() {
    await this.empService.getOrganizations(this.empId).then((data) => {
      data.forEach((item) =>
        this.empOrgs.set(item.organization.id, item.organization.name)
      );
    });
  }
  getActiveDetail(detail: string) {
    if (this.activeAccordian === detail) {
      this.activeAccordian = '';
    } else {
      this.activeAccordian = detail;
    }
  }

  deleteEmp() {
    const dialogRef = this.dialog.open(DeleteEmpComponent, {
      hasBackdrop: true,
    });
    dialogRef.componentInstance.empId = this.empId;
    dialogRef.afterClosed().subscribe((result) => {
      
      this.getEmpById();
    });
  }

  async changeEmpStatus(action: string) {
    await this.empService.delete(this.empId, action).then((res) => {
      if (res) {
        this.alert.successToast(this.translate.instant('L.' + res));
        this.getEmpById();
      }
    });
  }

  openPositionModal(mode: string, posId: any) {
    if (mode == 'delete') {
      const dialogRef = this.dialog.open(DeleteEmpPositionComponent, {
        hasBackdrop: true,
      });
      dialogRef.componentInstance.empId = this.empId;
      dialogRef.componentInstance.positionId = posId;
      dialogRef.afterClosed().subscribe((result) => {
        
        this.getEmpPositions();
      });
    }
  }

  openCertificationModal(mode: string, certId: any) {
    if (mode == 'delete') {
      const dialogRef = this.dialog.open(DeleteEmpCertificationComponent, {
        hasBackdrop: true,
      });
      dialogRef.componentInstance.empId = this.empId;
      dialogRef.componentInstance.certificationId = certId;
      dialogRef.afterClosed().subscribe((result) => {
        
        this.getEmpCertifications();
      });
    }
  }

  toggleOrganizationDropdown() {
    this.isOrganizationDropdownVisible = !this.isOrganizationDropdownVisible;
    if (this.isOrganizationDropdownVisible) {
      this.orgService.getAll().then((data) => (this.orgModel = data));
    }
  }

  openCertPanel(certObj?: any, callbackToOpenPortal?: any) {
    this.editData = certObj;

    if (callbackToOpenPortal) callbackToOpenPortal;
  }
  openPosPanel(posObj?: any, callbackToOpenPortal?: any) {
    this.editData = posObj;
    if (callbackToOpenPortal) callbackToOpenPortal;
  }

  addEmpOrgtoList(id: any, name: string) {
    if (this.empOrgs.has(id)) this.empOrgs.delete(id);
    else this.empOrgs.set(id, name);
  }

  async addEmpOrg() {
    for (const org of this.empModel.employeeOrganizations) {
      await this.empService.deleteOrganization(this.empId, org.organization.id);
    }

    for (const org of this.empOrgs.keys()) {
      await this.empService.createOrganization(this.empId, {
        organizationId: org,
      });
    }
    this.isOrganizationDropdownVisible = false;
    this.getEmpById();
  }

  async submitEmpForm() {
    if (this.empForm.invalid) {
      this.alert.warningAlert('Incomplete Data');
      return;
    }

    Object.assign(this.personUpdateOpt, this.empModel.person);
    await this.personService
      .update(this.empModel.person.id, this.personUpdateOpt)
      .then(async (x) => {
        if (x) {
          await this.addEmpOrg().then((_) =>
            this.alert.successToast(this.translate.instant('L.recUpdated'))
          );
        }
      });
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  filterEmpCert(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.empCertDataSource.filter = filter;
  }

  filterEmpPos(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.empPosDataSource.filter = filter;
  }
}
