import {
  AfterViewInit,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  TemplateRef,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { BehaviorSubject, Subject } from 'rxjs';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { DeleteEmpComponent } from '../delete-emp/delete-emp.component';
import { AddEmpComponent } from '../add-emp/add-emp.component';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeeUpdateOptions } from 'src/app/_DtoModels/Employee/EmployeeUpdateOptions';
import { Router } from '@angular/router';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { Organization } from 'src/app/_DtoModels/Organization/Organization';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatSort, Sort } from '@angular/material/sort';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { EmployeeOptions } from 'src/app/_DtoModels/Employee/EmployeeOptions';
import { DatePipe } from '@angular/common';
import { EmployeeDashboardStatistics } from 'src/app/_DtoModels/Employee/EmployeeDashboardStatistics';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'app-list-emps',
  templateUrl: './list-emps.component.html',
  styleUrls: ['./list-emps.component.scss'],
})
export class ListEmpsComponent implements AfterViewInit, OnInit {
  isVisible: boolean = false;
  employees: any[] = [];
  searchData: string = '';
  catCompleted: any
  catIncompleted: any
  empUpdateOpt: EmployeeUpdateOptions = {} as EmployeeUpdateOptions;
  tempSrc: any[] = [];
  empIdActive: any[] = [];
  empActiveStatus: any;
  empData: any;
  statusinfo: any;

  organizations: Organization[];
  organizationManager: any[] = [];
  taskEvaluator: any[] = [];
  positions: Position[];
  isLoading: boolean = false;
  empStatusFilter: string = 'active';
  empPositionFilter: string = 'All';
  empOrgFilter: string = 'All';
  empOrgManagerFilter: string = 'All';
  empTaskEvaluatorFilter: string = 'All';
  activeCount: number;
  inactiveCount: number;
  deleteCheck: boolean = false;
  tqAndcsCheck: boolean = false;
  @ViewChild('empFilters', { static: true }) empFilters: TemplateRef<any>;
  displayedColumns: string[] = [
    'index',
    'image',
    'name',
    'position',
    'employeeNum',
    'organization',
    'status',
    'organizationManager',
    'tqEvaluator',
    'id'
  ];
  empDataSource: MatTableDataSource<any>;
  empOriginalDataSource: MatTableDataSource<any>;
  modalHeader: string;
  modalReason: string;
  modalDescription: string;
  statisticsData: EmployeeDashboardStatistics;
  effectiveDateActive: any;
  isManager: boolean = false;
  filterText: string = "";
  constructor(
    private translate: TranslateService,
    public dialog: MatDialog,
    private alert: SweetAlertService,
    private DataBroadcastService: DataBroadcastService,
    private empService: EmployeesService,
    private route: Router,
    private orgService: OrganizationsService,
    private positionService: PositionsService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private datePipe: DatePipe,
    private labelPipe:LabelReplacementPipe,
    private dynamicLabelPipe:DynamicLabelReplacementPipe
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);

    if (this.DataBroadcastService.ShowMenuSideBar.value == false) {
      this.DataBroadcastService.ShowMenuSideBar.next(true);
    }
  }



  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.empDataSource.paginator = paginator;
  }

  ngAfterViewInit() {
    // var searchText = localStorage.getItem('searchText');
    // var searchData = JSON.parse(searchText);
    // if(searchData !== null && searchData !== undefined){
    //   this.empStatusFilter = searchData?.status;
    //   this.empPositionFilter = searchData?.pos;
    //   this.empOrgFilter = searchData?.org;
    //   this.empOrgManagerFilter = searchData?.managerOrg;
    //   this.empTaskEvaluatorFilter = searchData?.tqEvaluator;
    //   this.filterText = searchData?.searchText;
    //   this.filterDataCheck = searchData?.filterDataCheck
    // }
  }

  async ngOnInit() {
    // -- Call data -- //
    this.isLoading = true;
    this.statisticsData = await this.empService.getEmployeesStatistics().finally(() => {
      this.isLoading = false;
    });
    this.getEmployeeList();
    this.organizationManager = [
      {
        num: 1,
        action: 'YES'
      },
      {
        num: 2,
        action: 'NO'
      }
    ];

    this.taskEvaluator = [
      {
        num: 1,
        action: 'YES'
      },
      {
        num: 2,
        action: 'NO'
      }
    ];


  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  clearSearch: any = '';
  clearFilter() {
    this.empDataSource.filter = null;
    this.clearSearch = '';
    this.getEmployeeList();
  }

  clearFilterFunction() {
    this.empStatusFilter = 'active';
    this.empPositionFilter = 'All';
    this.empOrgFilter = 'All';
    this.empOrgManagerFilter = 'All';
    this.empTaskEvaluatorFilter = 'All';
    this.filterDataCheck = false;
    var filter = JSON.stringify({
      status: this.empStatusFilter?.trim() ?? "All",
      pos: this.empPositionFilter?.trim() ?? "All",
      org: this.empOrgFilter?.trim() ?? "All",
      managerOrg: this.empOrgManagerFilter?.trim() ?? "All",
      tqEvaluator: this.empTaskEvaluatorFilter?.trim() ?? "All",
    });

    this.empDataSource.filter = filter;
    //this.empDataSource = new MatTableDataSource(this.tempSrc);
  }

  sortData(sort: Sort) {
    this.empDataSource.sort = this.sort;
  }

  async getEmployeeList() {
    this.activeCount = 0;
    this.inactiveCount = 0;
    this.isLoading = true;
    this.tempSrc = [];
    await this.empService
      .getAll()
      .then((data) => {
        if (data && data.length > 0) {
          this.employees = data;

          this.employees?.forEach((e, i) => {
            if (e.taskQualifications.length > 0 || e.classSchedule_Employee> 0) {
              this.tempSrc.push({
                index: i + 1,
                id: e.id,
                image: e.image,
                name: e.fullName,
                email: e.userName,
                position: e.employeePositions,//.map((p) => p.position?.positionTitle),
                employeeNum: e.employeeNumber,
                organization: e.employeeOrganizations.map(
                  (o) => o.name
                ),
                status: e.active ? 'Active' : 'Inactive',
                statusBack: e.active,

                tqEvaluator: e.tqEqulator,
                organizationManager: e.employeeOrganizations.map(i => {
                  if (i.isManager === true) {
                    return 'YES';
                  } else {
                    return 'NO';
                  }
                }),
                classSh: e.classSchedule_Employee,
                tq: e.taskQualifications.length,
                condition: true
              });
            }

            else {
              this.tempSrc.push({
                index: i + 1,
                id: e.id,
                image: e.image,
                name: e.fullName,
                email: e.userName,
                position: e.employeePositions,//.map((p) => p.position?.positionTitle),
                employeeNum: e.employeeNumber,
                organization: e.employeeOrganizations.map(
                  (o) => o.name
                ),
                status: e.active ? 'Active' : 'Inactive',
                statusBack: e.active,

                tqEvaluator: e.tqEqulator,
                organizationManager: e.employeeOrganizations.map(i => {
                  if (i.isManager === true) {
                    return 'YES';
                  } else {
                    return 'NO';
                  }
                }),
                classSh: e.classSchedule_Employee,
                tq: e.taskQualifications.length,
                condition: false
              });
            }

            if (e.active === true) {
              this.activeCount = this.activeCount + 1;
            } else {
              this.inactiveCount = this.inactiveCount + 1;
            }
          });
          this.catCompleted = this.statisticsData.totalEmployeeRecordsAvailable;
          this.empDataSource = new MatTableDataSource(this.tempSrc);
          this.empOriginalDataSource = new MatTableDataSource(this.tempSrc);
          // this.empDataSource.sort = this.sort;
          this.empDataSource.paginator = this.tblPaging;
        } else {
          this.empDataSource = new MatTableDataSource();
          this.empDataSource.paginator = this.tblPaging;
        }


        this.empDataSource.filterPredicate = (row: any, filter: string) => {
          const filterObject = JSON.parse(filter);
          if (filterObject.status === 'All' && filterObject.pos === 'All' && filterObject.org === 'All' && filterObject.managerOrg === 'All' && filterObject.tqEvaluator === 'All') {
            return true;
          } else {
            var data = (
              (filterObject.status === 'All' ? true : row.statusBack == (filterObject.status === 'active' ? true : false))
              &&
              (filterObject.pos === 'All' ? true : row.position.some(item => item.positionTitle === filterObject.pos))
              &&
              (filterObject.managerOrg === 'All' ? true : row.organizationManager.some(item => item === filterObject.managerOrg))
              &&
              (filterObject.org === 'All' ? true : row.organization.some(item => item === filterObject.org))
              &&
              (filterObject.tqEvaluator === 'All' ? true : row.tqEvaluator == (filterObject.tqEvaluator === 'YES' ? true : false))
            )
            return data;
          }




        }
        this.applyFilters();
        this.filterEmps(new Event("t"));
        localStorage.removeItem('searchText');
      })
      .finally(() => (this.isLoading = false));
  }

  deleteEmp(id: any) {
    const dialogRef = this.dialog.open(DeleteEmpComponent, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
    });


    dialogRef.componentInstance.empId = id;
    dialogRef.afterClosed().subscribe((result) => {
       ;
      this.getEmployeeList();
    });
  }

  filterEmps(e: Event) {
    var filteredList: any[] = [];
    filteredList = [
      ...this.empOriginalDataSource.data.filter((x) =>
        x.name.toLowerCase().includes(String(this.clearSearch).toLowerCase()) ||
        (x.employeeNum ?? "").toLowerCase().includes(String(this.clearSearch).toLowerCase()) ||
        x.email.toLowerCase().includes(String(this.clearSearch).toLowerCase())
      ),
    ];
    let temparr =
      this.empOriginalDataSource.data.filter((element) => {
        element.name.toLowerCase().match(this.clearSearch.toLowerCase()) ||
        (element.employeeNum ?? "" ).match(this.clearSearch)

      });
    this.empDataSource.data = filteredList;
    //this.empDataSource.filter = filter["name"];
  }

  /* changeStatus(templateRef: any, status:any,id:any,name:any) {
     ;

    if(status === 'Active'){
      this.modalHeader = 'Make Employee Inactive';
    }
    else{
      this.modalHeader = 'Make Employee Active';
    }
    this.modalReason = 'Please provide Effective Date and Reason (if you want to) for this change'
    this.modalDescription = `You are selecting to make Employee ${name.split("\n")[0]} ${status} ?` ;
    this.empIdActive = id;
    this.empActiveStatus = status;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  } */

  async GetData(e: any) {
    this.empData = JSON.parse(e);
    this.effectiveDateActive = this.empData.data.effectiveDate;
    if (this.statusinfo.status === 'Active') {
      this.modalDescription = `You are selecting to make ` + await this.labelPipe.transform('Employee') + ` ${this.statusinfo.name} Inactive on the selected date ${this.datePipe.transform(this.effectiveDateActive, 'MM-dd-yyyy')}. Performing this action will make ` + await this.labelPipe.transform('Employee') + ` Inactive and all current ` + await this.transformTitle('Position') +`s will be moved to ` + await this.transformTitle('Position') +` History. The ` + await this.transformTitle('Position') +` End Date will be selected Inactive Date ${this.datePipe.transform(this.effectiveDateActive, 'MM-dd-yyyy')}`;
    }
    else {
      this.modalDescription = `You are selecting to make ` + await this.labelPipe.transform('Employee') + `  ${this.statusinfo.name} Active on the selected date ${this.datePipe.transform(this.effectiveDateActive, 'MM-dd-yyyy')}. Performing this action will make the ` + await this.labelPipe.transform('Employee') + ` Active`;
    }
  }

  async MakeActive(e: any) {

    let options = new EmployeeOptions();
    options.employeeIds = this.empIdActive;
    options.actionType = this.empActiveStatus.toLowerCase() == 'active' ? 'inactive' : 'active';
    options.changeEffectiveDate = this.empData['data']['effectiveDate'];
    options.changeNotes = this.empData['data']['reason'];

    await this.empService
      .deleteEmployee(options)
      .then(
        async (x) => {
          if (x) {
            this.alert.successToast(await this.dynamicLabelPipe.transform(x.value));
            this.getEmployeeList();
          }
        }
      ).catch(async (err) => {
        this.alert.errorToast("Error in updating " + await this.labelPipe.transform('Employee') + " Status");
      });
    this.empIdActive = [];
  }

  async changeStatus(templateRef: any, status: any, name: any, id: any) {
    this.statusinfo = {};
    let statusNew: any;
    statusNew = status;
    var currentDate = new Date();
    var datee = this.datePipe.transform(this.effectiveDateActive, 'MM/dd/yyyy');
    this.statusinfo = { 'status': status, 'name': name.split("\n")[0] };


    if (status === 'Active') {
      this.modalHeader = 'Make ' + await this.labelPipe.transform('Employee') + ' Inactive';

    }
    else {
      this.modalHeader = 'Make ' + await this.labelPipe.transform('Employee') + ' Active';
    }
    this.modalReason = 'Please provide Effective Date and Reason for this change'
    this.empIdActive.push(id);
    this.empActiveStatus = status;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  /* async changeStatus(status: string, empId: any) {
    await this.empService
      .delete(empId, status.toLowerCase() == 'active' ? 'inactive' : 'active')
      .then(
        (x) => {
          if (x) {
            this.getEmployeeList();
          }
        },
        () => this.alert.successToast(this.translate.instant('L.EmpUpdated'))
      );
  } */

  clearFilterData() {
     ;
    this.filterDataCheck = false;
    this.getEmployeeList();
  }


  filterData: any;
  filterDataCheck: boolean = false;
  applyFilters() {
     ;
    let filters: any = [];
    filters = JSON.stringify({
      status: this.empStatusFilter?.trim() ?? "All",
      pos: this.empPositionFilter?.trim() ?? "All",
      org: this.empOrgFilter?.trim() ?? "All",
      managerOrg: this.empOrgManagerFilter?.trim() ?? "All",
      tqEvaluator: this.empTaskEvaluatorFilter?.trim() ?? "All",
    });
    const data = JSON.parse(filters);

    this.empDataSource.filter = filters;


    //

    //
    //
    //
    //

    //
    // let tempSrc: any[] = [];
    // let manager = this.empOrgManagerFilter === 'NO' ? false : true;
    // let status = this.empStatusFilter === 'active' ? true : false;
    // if (data.status === 'active'){
    //   let newData=this.tempSrc;
    //   this.empDataSource.data = newData.filter(x=>x.status==='Active');
    // }
    // if (data.status === 'inactive'){
    //   let newData=this.tempSrc;
    //   this.empDataSource.data = newData.filter(x=>x.status==='Inactive');
    // }
    /*  this.employees?.forEach((e, i) => {
       ;
      if(status === e.active && (manager == e.employeeOrganizations[i].isManager) && (this.empPositionFilter == e.employeePositions[i].position.positionTitle)){
        tempSrc.push({
        index : i+1,
        id: e.id,
        image : e.person.image,
        name : e.person.firstName + ' ' + e.person.lastName  + '\n' + e.person.username,
        position: e.employeePositions.map((p) => p.position?.positionTitle),
        employeeNum: e.employeeNumber,
        organization: e.employeeOrganizations.map(
          (o) => o.organization?.name
        ),
        status: e.active ? 'Active' : 'Inactive',
        tqEvaluator : e.tqEqulator,
        organizationManager : e.employeeOrganizations.forEach((i)=>i.isManager)
      });
      this.empDataSource = new MatTableDataSource(tempSrc);
      this.empDataSource.sort = this.sort;
      this.empDataSource.paginator = this.paginator;

      }
      else if(this.empStatusFilter === 'All' && this.empTaskEvaluatorFilter === 'All' && this.empOrgManagerFilter === 'All' && this.empPositionFilter === 'All' && this.empOrgFilter === 'All'){
        this.empDataSource = new MatTableDataSource(this.tempSrc);
      }
      else {
        this.empDataSource = new MatTableDataSource();
      }

    });  */
    this.flyPanelService.close();
  }

  openEmpPage(mode: string, id: any) {

    var data = {
      status: this.empStatusFilter?.trim() ?? "All",
      pos: this.empPositionFilter?.trim() ?? "All",
      org: this.empOrgFilter?.trim() ?? "All",
      managerOrg: this.empOrgManagerFilter?.trim() ?? "All",
      tqEvaluator: this.empTaskEvaluatorFilter?.trim() ?? "All",
      searchText: this.filterText?.trim()?.toLowerCase() ?? "",
      filterDataCheck:this.filterDataCheck,
    }
    localStorage.setItem('searchText', JSON.stringify(data));

    this.route
      .navigate(['/implementation/employee/edit/' + id])
      .then((_) => this.DataBroadcastService.empFormMode.next(mode));
  }

  async GetPositionAndOrgsForFilter() {
    await this.orgService.getAllOrderBy('name').then((x) => {
      this.organizations = x;
    });

    await this.positionService.getAllOrderBy('name').then((x) => {
      this.positions = x;
    });
  }

  async openFlyInPanel() {
    await this.GetPositionAndOrgsForFilter();
    const portal = new TemplatePortal(this.empFilters, this.vcf);
    this.flyPanelService.open(portal);
  }

  onActionClick(tQEvaluator: any, classSh: any, tq: any) {
    if (tQEvaluator === false) {
      this.deleteCheck = true;
    }

    /*  if(classSh > 0 || tq > 0){
       this.tqAndcsCheck = true;
     } */
  }
  openEmpIDP(id: any, name: any) {
    //get search text and store in local storage
    //localStorage.setItem('searchText', JSON.stringify());
    var data = {
      status: this.empStatusFilter?.trim() ?? "All",
      pos: this.empPositionFilter?.trim() ?? "All",
      org: this.empOrgFilter?.trim() ?? "All",
      managerOrg: this.empOrgManagerFilter?.trim() ?? "All",
      tqEvaluator: this.empTaskEvaluatorFilter?.trim() ?? "All",
      searchText: this.filterText?.trim()?.toLowerCase() ?? "",
      filterDataCheck:this.filterDataCheck,
    }
    localStorage.setItem('searchText', JSON.stringify(data));

    this.route
      .navigate(['/implementation/employees/idp/' + id])
  }

  AddEmployeeManually() {

    var data = {
      status: this.empStatusFilter?.trim() ?? "All",
      pos: this.empPositionFilter?.trim() ?? "All",
      org: this.empOrgFilter?.trim() ?? "All",
      managerOrg: this.empOrgManagerFilter?.trim() ?? "All",
      tqEvaluator: this.empTaskEvaluatorFilter?.trim() ?? "All",
      searchText: this.filterText?.trim()?.toLowerCase() ?? "",
      filterDataCheck:this.filterDataCheck,
    }
    localStorage.setItem('searchText', JSON.stringify(data));

    this.route
      .navigate(['implementation/employee'])
  }
  UploadCsv() {
    var data = {
      status: this.empStatusFilter?.trim() ?? "All",
      pos: this.empPositionFilter?.trim() ?? "All",
      org: this.empOrgFilter?.trim() ?? "All",
      managerOrg: this.empOrgManagerFilter?.trim() ?? "All",
      tqEvaluator: this.empTaskEvaluatorFilter?.trim() ?? "All",
      searchText: this.filterText?.trim()?.toLowerCase() ?? "",
      filterDataCheck:this.filterDataCheck,
    }
    localStorage.setItem('searchText', JSON.stringify(data));

    this.route
      .navigate(['upload-csv'])
  }
  AddByApi() {
    var data = {
      status: this.empStatusFilter?.trim() ?? "All",
      pos: this.empPositionFilter?.trim() ?? "All",
      org: this.empOrgFilter?.trim() ?? "All",
      managerOrg: this.empOrgManagerFilter?.trim() ?? "All",
      tqEvaluator: this.empTaskEvaluatorFilter?.trim() ?? "All",
      searchText: this.filterText?.trim()?.toLowerCase() ?? "",
      filterDataCheck:this.filterDataCheck,
    }
    localStorage.setItem('searchText', JSON.stringify(data));

    this.route
      .navigate(['api'])
  }

  ListName: any;
  async openFlyInPanelList(templateRef: any, name: string) {
    this.ListName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
}
