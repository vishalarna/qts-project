import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { EmployeeCreateOptions } from '@models/Employee/EmployeeCreateOptions';
import { EmployeeLinkCertification } from '@models/Employee/EmployeeLinkCertification';
import { EmployeePositionCreateOptions } from '@models/EmployeePosition/EmployeePositionCreateOptions';
import { Organization } from '@models/Organization/Organization';
import { PersonCreateOption } from '@models/Person/PersonCreateOption';
import { PublicClassScheduleIlaVM } from '@models/PublicClasses/PublicClassScheduleIlaVM';
import { PublicClassScheduleRequestVM } from '@models/PublicClasses/PublicClassScheduleRequestVM';
import { RequestedAction } from '@models/PublicClasses/PublicClassScheduleRequestVM';
import { PublicClassScheduleVM } from '@models/PublicClasses/PublicClassScheduleVM';
import { TrainingStudentCreationOptions } from '@models/SchedulesClassses/training-creation-options';
import { User } from '@models/User/User';
import { Store } from '@ngrx/store';
import { UserService } from 'src/app/_Services/Auth/user.service';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { PersonsService } from 'src/app/_Services/QTD/persons.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { PublicClassScheduleRequestService } from 'src/app/_Services/QTD/public-class-schedule-request.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-public-requests',
  templateUrl: './public-requests.component.html',
  styleUrls: ['./public-requests.component.scss']
})
export class PublicRequestsComponent implements OnInit {
  publicRequestDataSource = new MatTableDataSource<any>([]);
  publicCourseDataSource = new MatTableDataSource<any>([]);
  publicClassScheduleDataSource = new MatTableDataSource<any>([]);
  requests: PublicClassScheduleRequestVM[] = [];
  expandedClassScheduleElement: any | null;
  expandedRequestElement: any | null;
  showLoader: boolean = false;
  requestedAction = RequestedAction;
  @ViewChild('publicCourseSort') set publicCourseSort(sorting: MatSort) {
    if (sorting) this.publicCourseDataSource.sort = sorting;
  }
  @ViewChildren('scheduleSort') scheduleSorts!: QueryList<MatSort>;
  @ViewChildren('requestSort') requestSorts!: QueryList<MatSort>;

  courseColumnsToDisplay = ["expandClassSchedule", 'ilaNumber', 'ilaTitle'];
  classScheduleColumnsToDisplay = ['expandRequests', 'classStartDateTime', 'instructorName', 'locationName'];
  requestColumnsToDisplay = ['firstName', 'lastName', 'emailAddress', 'company', 'nercCertNumber', 'nercCertType', 'nercCertExpiration', 'action']
  editedCell: { rowId: string, column: string } | null = null;
  organizations: Organization[] = [];
  nercCerts: any[] = [];
  organizationPlaceholder: string;
  constructor(private publicClassScheduleRequestService: PublicClassScheduleRequestService,
    private store: Store<{ toggle: string }>,
    private userService: UserService,
    private alert: SweetAlertService,
    private personService: PersonsService,
    private empSrvc: EmployeesService,
    private trService: TrainingService,
    private positionService: PositionsService,
    private organizationsService: OrganizationsService,
    private certificationService: CertificationService) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarClose());
    this.getAllPublicRequests()
    this.readyPublicOrgs();
    this.readyNercCerts();
  }

  async getAllPublicRequests() {
    this.showLoader = true;
    this.requests = await this.publicClassScheduleRequestService.getPublicRequestsAsync();
    await this.readyClassScheduleRequest();
    this.showLoader = false;
  }

  async readyClassScheduleRequest() {
    const groupByIla = this.requests.reduce(
      (acc: Record<number, { ila: PublicClassScheduleIlaVM; classes: PublicClassScheduleRequestVM[] }>,
        item: PublicClassScheduleRequestVM) => {
        const ilaId = item.publicClassSchedule.ilaId;
        if (ilaId == null) return acc;

        if (!acc[ilaId]) {
          acc[ilaId] = { ila: item.publicClassScheduleIla, classes: [] };
        }
        acc[ilaId].classes.push(item);
        return acc;
      },
      {}
    );

    const groupedArray = Object.entries(groupByIla).map(([ilaId, ilaGroup]) => {
      const groupByClass = ilaGroup.classes.reduce(
        (acc: Record<number, { info: PublicClassScheduleVM; requests: PublicClassScheduleRequestVM[] }>,
          item: PublicClassScheduleRequestVM) => {
          const classId = item.publicClassSchedule.classId!;
          if (!acc[classId]) {
            acc[classId] = { info: item.publicClassSchedule, requests: [] };
          }

          acc[classId].requests.push({
            ...item,
            classId: classId,
          });

          return acc;
        },
        {}
      );

      const classes = Object.values(groupByClass).map(clsGroup => ({
        ilaId: clsGroup.info.ilaId,
        classId: clsGroup.info.classId!,
        locationName: clsGroup.info.locationName,
        instructorName: clsGroup.info.instructorName,
        startDateTime: new Date(clsGroup.info.startDateTime + "Z").toLocaleString(),
        endDateTime: new Date(clsGroup.info.endDateTime + "Z").toLocaleString(),
        requests: clsGroup.requests
      }));

      return {
        ilaId: ilaId,
        ilaNumber: ilaGroup.ila.ilaNumber,
        ilaTitle: ilaGroup.ila.ilaTitle,
        classes
      };
    });

    this.publicCourseDataSource = new MatTableDataSource(groupedArray);
  }


  getExpandedClassScheduleElement(row: { ilaId: any; classes: any[] }) {
    this.expandedClassScheduleElement = this.expandedClassScheduleElement === row ? null : row;

    this.publicClassScheduleDataSource.data = this.expandedClassScheduleElement
      ? this.expandedClassScheduleElement.classes
      : [];

    setTimeout(() => {
      this.classScheduleNestedSort(row.ilaId);
    }, 500);
  }

  getExpandedRequestElement(row: { classId: any; requests: any[] }) {

    this.expandedRequestElement = this.expandedRequestElement === row ? null : row;
    this.publicRequestDataSource.data = this.expandedRequestElement
      ? this.expandedRequestElement.requests
      : [];
    setTimeout(() => {
      this.requestNestedSort(row.classId);
    }, 500);
  }

  async updateRequest(row: PublicClassScheduleRequestVM, response: RequestedAction) {
    row.requestedAction = response;
    if (response == this.requestedAction.Deny) {
      try {
        await this.publicClassScheduleRequestService.updatePublicClassScheduleRequestAsync(row.id, row);
        await this.getAllPublicRequests();
      }
      catch (error) {
        this.alert.errorToast('Something went wrong while processing your request. Please try again later or contact support if the issue persists.');
      }
    }
    else if (response == this.requestedAction.Accept) {
      try {
        await this.createAspNetUserAsync(jwtAuthHelper.SelectedInstance, row);
        const personId = await this.createPersonAsync(row);
        const employeeId = await this.createEmployeeAsync(personId, row);
        const position = await this.positionService.getPositionByNameAsync('External');
        if (position != null) {
          await this.linkPosition(employeeId, position.id);
        }
        if (row.nercCertType != null) {
          await this.createCertification(employeeId, row);
        }
        if (row.nercCertType == null) {
          row.nercCertNumber = null;
          row.expirationDate = null;
        }
        if (row.company != null) {
          await this.linkOrganization(employeeId, row);
        }
        const csEmpId = await this.linkEmployees(employeeId, row.publicClassSchedule.classId)
        row.classScheduleEmployeeId = csEmpId;
        const result = await this.publicClassScheduleRequestService.updatePublicClassScheduleRequestAsync(row.id, row);
        await this.getAllPublicRequests();
      }
      catch (error) {
        if (error.status == 409) {
          try {
            const personId = await this.createPersonAsync(row);
            const employeeId = await this.createEmployeeAsync(personId, row);
            const position = await this.positionService.getPositionByNameAsync('External');
            if (position != null) {
              await this.linkPosition(employeeId, position.id);
            }
            if (row.nercCertType != null) {
              await this.createCertification(employeeId, row);
            }
            if (row.nercCertType == null) {
              row.nercCertNumber = null;
              row.expirationDate = null;
            }
            if (row.company != null) {
              await this.linkOrganization(employeeId, row);
            }
            const csEmpId = await this.linkEmployees(employeeId, row.publicClassSchedule.classId)
            row.classScheduleEmployeeId = csEmpId;
            const result = await this.publicClassScheduleRequestService.updatePublicClassScheduleRequestAsync(row.id, row);
            await this.getAllPublicRequests();
          }
          catch (error) {
            if (error.status == 409) {
              try {
                const person = await this.personService.getPersonByUserNameAsync(row.emailAddress);
                const employeeId = await this.createEmployeeAsync(person.id, row);
                const position = await this.positionService.getPositionByNameAsync('External');
                if (position != null) {
                  await this.linkPosition(employeeId, position.id);
                }
                if (row.nercCertType != null) {
                  await this.createCertification(employeeId, row);
                }
                if (row.nercCertType == null) {
                  row.nercCertNumber = null;
                  row.expirationDate = null;
                }
                if (row.company != null) {
                  await this.linkOrganization(employeeId, row);
                }
                const csEmpId = await this.linkEmployees(employeeId, row.publicClassSchedule.classId)
                row.classScheduleEmployeeId = csEmpId;
                const result = await this.publicClassScheduleRequestService.updatePublicClassScheduleRequestAsync(row.id, row);
                await this.getAllPublicRequests();
              }
              catch (error) {
                if (error.status == 409) {
                  const employee = await this.empSrvc.getEmployeeByUserNameAsync(row.emailAddress);
                  const position = await this.positionService.getPositionByNameAsync('External');
                  if (position != null) {
                    await this.linkPosition(employee.id, position.id);
                  }
                  if (row.nercCertType != null) {
                    await this.createCertification(employee.id, row);
                  }
                  if (row.nercCertType == null) {
                    row.nercCertNumber = null;
                    row.expirationDate = null;
                  }
                  if (row.company != null) {
                    await this.linkOrganization(employee.id, row);
                  }
                  const csEmpId = await this.linkEmployees(employee.id, row.publicClassSchedule.classId)
                  row.classScheduleEmployeeId = csEmpId;
                  const result = await this.publicClassScheduleRequestService.updatePublicClassScheduleRequestAsync(row.id, row);
                  await this.getAllPublicRequests();
                }
                else if (error.status != 409) {
                  if (typeof error == 'string') {
                    this.alert.errorToast(error);
                  }
                  if (typeof error == 'object') {
                    this.alert.errorToast(error.displayMessage);
                  }
                }
              }
            }
            else if (error.status != 409) {
              if (typeof error == 'string') {
                this.alert.errorToast(error);
              }
              if (typeof error == 'object') {
                this.alert.errorToast(error.displayMessage);
              }
            }
          }
        }
        else if (error.status != 409) {
          if (typeof error == 'string') {
            this.alert.errorToast(error);
          }
          if (typeof error == 'object') {
            this.alert.errorToast(error.displayMessage);
          }
        }
      }
    }
  }

  async createAspNetUserAsync(instanceName: string, options: any) {
    let user = new User();
    user.name = options.emailAddress;
    user.password = '';
    user.instanceName = instanceName;
    await this.userService.createUser(user);
  }

  async createPersonAsync(options: any) {
    let person = new PersonCreateOption();
    person.firstName = options.firstName;
    person.lastName = options.lastName;
    person.username = options.emailAddress;
    const createdPerson = await this.personService.create(person);
    return createdPerson.id;
  }

  async createEmployeeAsync(id: any, options: any) {
    let employee = new EmployeeCreateOptions();
    employee.personId = id;
    employee.TQEqulator = false;
    employee.PublicUser = true;
    const createdEmployee = await this.empSrvc.create(employee);
    return createdEmployee.id;
  }

  async createCertification(id: string, options: any) {
    const date = new Date()
    const renewalDate = new Date(date.getFullYear() + 1, date.getMonth(), date.getDate())
    let certificate = new EmployeeLinkCertification();
    const result = await this.certificationService.getNercCertList();
    const certMatched = result.find(cert => cert.name == options.nercCertType);
    certificate.certificationId = certMatched.id;
    certificate.certificationNumber = options.nercCertNumber;
    certificate.issueDate = (new Date).toISOString().split('T')[0];;
    certificate.expirationDate = options.expirationDate?.split("T")[0];
    certificate.renewalDate = renewalDate.toISOString().split('T')[0];;
    await this.empSrvc.createCertification(id, certificate)
  }

  async linkOrganization(id: string, options: any) {
    const result = await this.organizationsService.getPublicOrganizationsList();
    const orgMatched = result.find(org => org.name == options.company);
    let orgOptions = {
      organizationIds: [orgMatched.id]
    };
    await this.empSrvc.LinkOrganizationtoEmployee(id, orgOptions);
  }

  async linkEmployees(empId, classId) {
    const createOptions: TrainingStudentCreationOptions = {
      classScheduleId: classId,
      employeeIds: [empId]
    }
    const result = await this.trService.createEmployees(createOptions);
    return result.linkEmployeeResult.linkedEmployees[0].classScheduleEmployeeId;
  }

  async linkPosition(empId: string, positionId: string) {
    const positionOptions = new EmployeePositionCreateOptions();
    positionOptions.positionId = positionId;
    positionOptions.startDate = new Date().toISOString().split('T')[0];
    const result = await this.empSrvc.createPosition(empId, positionOptions);
  }

  classScheduleNestedSort(ilaId: string) {
    const index = this.publicCourseDataSource.data.findIndex(items => items.ilaId === ilaId);
    if (index == -1) return;
    const sorts = this.scheduleSorts.toArray()[index];
    if (sorts) {
      this.publicClassScheduleDataSource.sort = sorts;
    }
  }

  requestNestedSort(classId: string) {
    const index = this.publicClassScheduleDataSource.data.findIndex(items => items.classId === classId);
    if (index == -1) return;
    const sorts = this.requestSorts.toArray()[index];
    if (sorts) {
      setTimeout(() => {
        this.publicRequestDataSource.sort = sorts;
      }, 500);

    }
  }
  async readyPublicOrgs() {
    this.organizations = await this.organizationsService.getPublicOrganizationsList();
    this.organizationPlaceholder = this.organizations.length > 0 ? 'Select Organization' : 'No Public Organization Available'
  }

  onChangeValue(row: PublicClassScheduleRequestVM, column: string, event: any) {
    if (column == 'firstName') {
      row.firstName = event.target.value;
      this.editedCell = { rowId: row.id, column };
    }
    else if (column == 'lastName') {
      row.lastName = event.target.value;
      this.editedCell = { rowId: row.id, column };
    }
    else if (column == 'emailAddress') {
      row.emailAddress = event.target.value;
      this.editedCell = { rowId: row.id, column };
    }
    else if (column == 'nercCertNumber') {
      row.nercCertNumber = event.target.value;
      this.editedCell = { rowId: row.id, column };
    }
    else if (column == 'company') {
      row.company = event.value;
      this.editedCell = { rowId: row.id, column };
    }
    else if (column == 'nercCertType') {
      row.nercCertType = event.value;
      if (!event.value) {
        row.nercCertNumber = null;
        row.expirationDate = null;
      }
      this.editedCell = { rowId: row.id, column };
    }
    else if (column == 'nercCertExpiration') {
      row.expirationDate = event.target.value
      this.editedCell = { rowId: row.id, column };
    }

  }

  formatDateToYyyyMmDd(dateString: string): string {
    return dateString?.split('T')[0] ?? '';
  }

  isCellEditing(row: any, column: string): boolean {
    return this.editedCell?.rowId === row.id && this.editedCell?.column === column;
  }

  async readyNercCerts() {
    this.nercCerts = await this.certificationService.getNercCertList();
  }

  async saveChanges(row: PublicClassScheduleRequestVM) {
    await this.publicClassScheduleRequestService.updatePublicClassScheduleRequestAsync(row.id, row);
    var index = this.requests.findIndex(x => x.id == row.id);
    if (index != -1) {
      Object.assign(this.requests[index], row);
    }
    this.editedCell = null;
  }

  undoChanges(row: PublicClassScheduleRequestVM): void {
    var originalRow = this.requests.find(x => x.id == row.id);
    Object.assign(row, originalRow);
    this.editedCell = null;
  }
}


