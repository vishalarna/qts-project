import { loggedInReducer } from './../../../../../_Statemanagement/reducer/state.signInReducer';
import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  Renderer2,
  TemplateRef,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import {
  AbstractControl,
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  NgForm,
  Validators,
} from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { Certification } from 'src/app/_DtoModels/Certification/Certification';
import { CertifyingBody } from 'src/app/_DtoModels/CertifyingBody/CertifyingBody';
import { ClientUser } from 'src/app/_DtoModels/ClientUser/ClientUser';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeeCreateOptions } from 'src/app/_DtoModels/Employee/EmployeeCreateOptions';
import { EmployeeCertification } from 'src/app/_DtoModels/EmployeeCertification/EmployeeCertification';
import { EmployeePosition } from 'src/app/_DtoModels/EmployeePosition/EmployeePosition';
import { EmployeePositionCreateOptions } from 'src/app/_DtoModels/EmployeePosition/EmployeePositionCreateOptions';
import { Organization } from 'src/app/_DtoModels/Organization/Organization';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { ClientService } from 'src/app/_Services/Auth/client.service';
import { CertifyingBodiesService } from 'src/app/_Services/QTD/certifying-bodies.service';
import { ClientUsersService } from 'src/app/_Services/QTD/client-users.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import {
  ActivatedRoute,
  ActivatedRouteSnapshot,
  Router,
} from '@angular/router';
import { PersonsService } from 'src/app/_Services/QTD/persons.service';
import { PersonCreateOption } from 'src/app/_DtoModels/Person/PersonCreateOption';
import { Person } from 'src/app/_DtoModels/Person/Person';
import { finalize, map } from 'rxjs/operators';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { SelectionModel } from '@angular/cdk/collections';
import { User } from 'src/app/_DtoModels/User/User';
import { UserService } from 'src/app/_Services/Auth/user.service';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { EmployeeDocumentOptions } from 'src/app/_DtoModels/Employee/EmployeeDocumentOptions';
import { PersonUpdateOption } from 'src/app/_DtoModels/Person/PersonUpdateOption';
import { EmployeeUpdateOptions } from 'src/app/_DtoModels/Employee/EmployeeUpdateOptions';
import { ActivityNotificationService } from 'src/app/_Services/QTD/activity-notification.service';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Observable } from 'rxjs/internal/Observable';
import { BreakpointObserver } from '@angular/cdk/layout';
import { MatStepper } from '@angular/material/stepper';
import { link } from 'fs';
import { DatePipe, ViewportScroller } from '@angular/common';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import { UserUpdateOptions } from 'src/app/_DtoModels/User/UserUpdateOptions';
import { AuthService } from 'src/app/_Services/Auth/auth.service';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { Store } from '@ngrx/store';
import { freezeMenu, sideBarBackDrop, sideBarClose, sideBarDisableClose, sideBarMode } from 'src/app/_Statemanagement/action/state.menutoggle';
import { EMPCertificationVM } from 'src/app/_DtoModels/EmployeeCertification/EMPCertificationVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { MatLegacyCheckbox as MatCheckbox } from '@angular/material/legacy-checkbox';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { PersonActivityNotificationOptions_VM } from '@models/Person/PersonActivityNotificationOptions_VM';
import { DocumentCreateOptions } from '@models/Document/DocumentCreationOptions';
import { ApiDocumentTypesService } from 'src/app/_Services/QTD/DocumentTypes/api.documentTypes.Service';
import { ApiDocumentsService } from 'src/app/_Services/QTD/Documents/api.document.Service';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: string;
  symbol: string;
  modifiedate: string;
  version: string;
}
export interface CertificationData {
  name: string;
  issuedate: any;
  renewaldate: string;
  expirationdate: string;
}
export interface OrganizationTbl {
  orgname: string;
}
const ELEMENT_DATA: PeriodicElement[] = [
  {
    position: 12,
    name: '25-09-1995',
    weight: 'Yes',
    symbol: '25-09-1995',
    modifiedate: '25-09-1994',
    version: 'QTS-001',
  },
  {
    position: 13,
    name: '25-08-1999',
    weight: 'No',
    symbol: '25-09-1995',
    modifiedate: '28-09-1994',
    version: 'QTS-002',
  },
];


const ORG_DATA: OrganizationTbl[] = [
  { orgname: 'North Division' },
  { orgname: 'West Division' },
];

@Component({
  selector: 'app-add-emp',
  templateUrl: './add-emp.component.html',
  styleUrls: ['./add-emp.component.scss'],
})
export class AddEmpComponent implements OnInit, AfterViewInit, OnDestroy {
  processing = false;
  mode: string = 'add';
  organization: Organization[] = [];
  certifications: Certification[] = [];
  positions: Position[] = [];
  person!: Person;
  certBodies: CertifyingBody[] = [];
  CertBody: CertifyingBody | undefined = undefined;
  Org: any;
  Pos: any;
  cert: any;
  employeeNumber: any;
  empPos!: EmployeePosition;
  empCert!: EmployeeCertification;
  createclientUser: boolean = false;
  files: any[] = [];
  MAX_SIZE: number = 12000000;
  fileInBase64: any;
  empCheck: boolean;
  selected: any;
  empIndex: any = [];
  uploadCheck: boolean = false;
  //certificationsnotrequired:boolean=false;
  isLoading: boolean = false;
  optionOrganizationManager: any;
  licenceNumber: any;
  @ViewChild('empAddForm') empAddForm!: NgForm;
  @ViewChild('allActivity') allActivity!: MatCheckbox;
  @ViewChild('posPaginator') posPaginator!: MatPaginator;
  editor = ckcustomBuild;
  deleteDescription = '';
  unlinkOrgDescription: any;
  imageInBase64: string | undefined = '';
  uploadedImage: string;
  url: any;
  img = '';
  image_name: string = 'No image selected';
  options = new EmployeeDocumentOptions();
  employeeID: any;
  emp: any = [];
  navigation = "";
  acceptedTypes =
    'application/msword, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/pdf';
  allowedTypes = [
    'image/jpg',
    'image/jpeg',
    'image/bmp',
    'image/png',
    'image/svg',
  ];
  id: any;
  linkedIds: any[] = [];
  modalType: 'Add' | 'Edit';
  posId: any;
  empCertId: any;
  certId: any;
  stepperOrientation: Observable<StepperOrientation>;
  step1Form: UntypedFormGroup;
  //step2Form: FormGroup;
  deletecertDescription: any;
  datePipe = new DatePipe('en-us');
  modalHeader = '';
  employeeName: string;
  organizationName: string;
  modalDescription: string;
  limitExceedRef !: TemplateRef<any>;
  orgToUnlink: any;
  hidePassword: boolean = false;
  hasFormValue: boolean;
  @ViewChild('stepper') stepper: MatStepper;
  certFilter: 'current' | 'all' | 'past' = 'current';
  posFilter: 'current' | 'all' | 'past' = 'current';
  empCertification:any;
  isSaveUpdateFailed:boolean;
  empLicensedAvailable:boolean;
  userClaimList:string[];
  alreadyExistUsernameDesc:string;
  @ViewChild('alreadyExistUsername') alreadyExistUsername: any;
  empPosId:string;
  currentStepIndex = 0;
  documentTypeId:string;
  isChecked: boolean;

  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private certBodyService: CertifyingBodiesService,
    private posSrvc: PositionsService,
    private orgSrvc: OrganizationsService,
    private empSrvc: EmployeesService,
    private personService: PersonsService,
    private clientUserService: ClientUsersService,
    public route: Router,
    public dialog: MatDialog,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private userService: UserService,
    private activityNotificationservice: ActivityNotificationService,
    private fb: UntypedFormBuilder,
    public breakpointObserver: BreakpointObserver,
    private clientSettingService: ApiClientSettingsService,
    private authService: AuthService,
    private alertService: SweetAlertService,
    private store: Store<{ toggle: string }>,
    private activatedRoute: ActivatedRoute,
    private labelPipe: LabelReplacementPipe,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe,
    private elRef : ElementRef,
    private renderer :Renderer2,
    private apiDocumentTypeService:ApiDocumentTypesService,
    private documentService: ApiDocumentsService
    ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
    this.person = new Person();
    this.empPos = new EmployeePosition();
    this.empCert = new EmployeeCertification();
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));

  }

  ngOnDestroy(): void {
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: true }));
    this.store.dispatch(sideBarMode({ mode: 'side' }));
  }

    displayColumns: string[] = [
      'positionTitle',
      'startDate',
      'trainee',
      'qualificationDate',
      'trainingProgramVersion',
      'endDate',
      'id',
    ];
    //displayedColumns: string[] = ['position', 'name', 'weight', 'symbol', 'version', 'modifiedate'];
  DataSourcePositions: MatTableDataSource<any> = new MatTableDataSource();
  @ViewChild('posSort') posSort!: MatSort;
  displayedColumnsCert: string[] = [
    'name',
    'certificationNumber',
    'issueDate',
    'renewalDate',
    'expirationDate',
    'isExpired',
    'id'
  ];
  dataSourceCert: MatTableDataSource<any> = new MatTableDataSource();
  @ViewChild('certSort') certSort!: MatSort;
  displayedColumnsOrg: string[] = ['name', 'select', 'id'];
  dataSourceOrg: MatTableDataSource<any> = new MatTableDataSource();
  @ViewChild('orgSort') orgSort!: MatSort;
  selection: SelectionModel<any> = new SelectionModel(true);
  positionsTableData: any = [];
  certificationsTableData: EMPCertificationVM[] = [];
  posIdtoDelete: any;
  certIdtoDelete: any;
  editCertObj: any;
  oldUserName: any;
  // @ViewChild(MatSort) set tblSort(sort: MatSort) {
  //   if (sort) this.DataSourcePositions.sort = sort;
  // }

  @ViewChild(MatSort) sort: MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  // @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    //   if (paginator) this.DataSourcePositions.paginator = paginator;
    // }
  @ViewChild('orgpaginator') organizationpagintor!: MatPaginator;
  alreadyExistsUserName:string = "";
  alreadyExistsEmpNumber:string = "";

  ngOnInit() : void {
    this.isSaveUpdateFailed = true;
    this.hasFormValue = true;
    this.readyStep1Form();
    let segments = this.route.url.split('/');
    this.getDocumentTypeAsync();
    if (segments.includes('edit')) {
      this.activatedRoute.params.subscribe((data) => {
        this.id = data.id;
      });
      this.mode = 'edit';
    }

    this.activatedRoute.queryParams.subscribe((data) => {
      if (data.navigate === 'certifications') {
        this.navigation = 'certifications';
      }
    });

    this.dataBroadcastService.publicClassEnabled.subscribe((x) => {
      this.isChecked = x
    })
  }

  async ngAfterViewInit() : Promise<void> {
    this.store.dispatch(sideBarClose());
    this.store.dispatch(sideBarBackDrop({ backdrop: false }));
    this.store.dispatch(sideBarDisableClose({ disableClose: false }));
    this.store.dispatch(sideBarMode({ mode: 'side' }));

    this.isLoading = true;
    await this.loadDataAsync();

    if(this.mode === 'edit'){
      await this.loadEditDataAsync();
    }
    this.isLoading = false;

    this.setStepEventListeners();
  }

  async loadDataAsync() : Promise<void> {
    await this.loadActivityNotificationTypes();
  }

  async loadEditDataAsync() : Promise<void> {
    await this.getEmpById();
    await this.getPositionsByEmployeeId(this.posFilter);
    await this.getPersonActivityNotifications();
    await this.getOrganizationsByEmployeeId();
    await this.getcertificationsByEmployeeId(this.certFilter);
  }

  setStepEventListeners() {
    setTimeout(() => {
      const stepHeaders = this.elRef.nativeElement.querySelectorAll('.mat-step-header');
      stepHeaders.forEach((stepHeader, index) => {
        this.renderer.listen(stepHeader, 'click', (event) => {
          this.stepClick(event, index);
        });
      });
    }, 1);
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only

    if (pattern.test(control.value)) {
      return null;
    } else {
      return { whitespaceOnly: true };
    }
  }

  alreadyExistsEmailValidator(control: AbstractControl): { [key: string]: any } | null {
    return (control.value != "" && control.value == this.alreadyExistsUserName) ? { alreadyExists: true } : null;
  }
  alreadyExistsNumberValidator(control: AbstractControl): { [key: string]: any } | null {
    return (control.value != "" && control.value == this.alreadyExistsEmpNumber) ? { alreadyExists: true } : null;
  }

  sortData(sort: Sort) {
    this.dataSourceCert.sort = this.sort;
  }

  async getCertBodies() {
    await this.certBodyService.getAll().then((data) => {
      this.certBodies = data;
    });
  }

  async getCertTypes() {
    if (!this.CertBody) return;

    await this.certBodyService
    .getAllCertifications(this.CertBody.id)
    .then((data) => {
      this.certifications = data;
    });
  }
  async getAllPositions() {
    await this.posSrvc.getAllWithoutIncludes().then((data) => {
      this.positions = data;
    });
  }

  async getAllOrganizations() {
    await this.orgSrvc.getAll().then((data) => {
      this.organization = data;
    });
  }

  async AddNewEmployee() {
    if (this.empAddForm.invalid) {
      this.alert.warningAlert('Incomplete Data');
      return;
    }

    if (this.person.password) {
      let useroption: User = {
        name: this.person.username,
        password: this.person.password,
        empEnabled: false,
        instanceName: jwtAuthHelper.SelectedInstance,
        createWithIdentityProvider:"default"
      };
      let user = await this.userService.createUser(useroption).then((data) => {
        return data;
      });
    }

    let createOpt: PersonCreateOption = {
      firstName: this.person.firstName,
      lastName: this.person.lastName,
      middleName: this.person.middleName,
      username: this.person.username,
      image: this.imageInBase64,
    };
    let person = await this.personService.create(createOpt).then((data) => {
      return data;
    });

    if (person) {
      let empCreateOpt: EmployeeCreateOptions = {
        personId: person.id,
        employeeNumber: this.employeeNumber,
        username: person.username,
        City: this.person.city,
        State: this.person.state,
        ZipCode: this.person.zipcode,
        PhoneNumber: this.person.phoneNumber,
        WorkLocation: this.person.workLocation,
        Notes: this.person.notes,
        Password: 'dummypassword',
        TQEqulator: this.person.tqEvaluator,
        Address: this.person.address,
        empEnabled: false,
        websiteUrl: "",
        PublicUser: this.person.publicUser
      };
      let emp = await this.empSrvc.create(empCreateOpt).then((data) => {
        this.employeeID = data.id;
        return data;
      });

      if (this.uploadCheck === true) {
        this.uploadFiles();
      }

      let activityOptions: PersonActivityNotificationOptions_VM = {
        personId: person.id,
        activityNotificationIds: this.empIndex,
      };
      await this.personService
      .createPersonActivityNotification(person.id, activityOptions)
      .then((data) => {
        return data;
      });
      this.alert.successToast(this.translate.instant('L.recAdded'));
      this.empAddForm.resetForm();
      //this.route.navigate(['/implementation/employees', emp.id]);
      //this.route.navigate(['/implementation/employee/edit/', emp.id])
      this.route.navigate(['/implementation/employees']);
    }
  }

  async stepClick(event: Event, stepIndex: number) {
    if (this.currentStepIndex == 0 && this.currentStepIndex !== stepIndex) {
      if(this.step1Form.valid){
        await this.tryPersist()
      }
      if (!this.checkNavigationAllowed()) {
        return;
      }
      this.stepper.next();
    }

    if(this.currentStepIndex !== stepIndex){
      this.currentStepIndex = stepIndex;
    }
  }

  checkNavigationAllowed(){
    return this.step1Form.valid && !this.isSaveUpdateFailed ;
  }

  async UpdateEmployee() {
    let createOpt: PersonUpdateOption = {
      firstName: this.step1Form.get('firstName')?.value,
      lastName: this.step1Form.get('lastName')?.value,
      middleName: this.step1Form.get('middleName')?.value,
      username: this.step1Form.get('emailAddr')?.value,
      image: this.imageInBase64,
    };
    let person = await this.personService
      .update(this.person.id, createOpt)
      .then((data) => {
        return data;
      });
    if (person) {
      let empCreateOpt: EmployeeUpdateOptions = {
        personId: this.person.id,
        employeeNumber: this.step1Form.get('empNum')?.value,
        username: this.step1Form.get('emailAddr')?.value,
        City: this.step1Form.get('city')?.value,
        State: this.step1Form.get('state')?.value,
        ZipCode: this.step1Form.get('zipcode')?.value ?? 0,
        PhoneNumber: this.step1Form.get('phoneNumber')?.value ?? 0,
        WorkLocation: this.step1Form.get('location')?.value,
        Notes: this.step1Form.get('notes')?.value,
        Password: 'dummypassword',
        TQEqulator: this.step1Form.get('tqEvaluator')?.value,
        Address: this.step1Form.get('address')?.value,
        PublicUser: this.step1Form.get('publicUser')?.value
      };
      let emp = await this.empSrvc
        .update(this.id, empCreateOpt)
        .then((data) => {
          return data;
        });

      if (this.uploadCheck) {
        this.uploadFiles();
      }

      let activityOptions: PersonActivityNotificationOptions_VM = {
        personId: person.id,
        activityNotificationIds: this.empIndex,
      };
      await this.personService
      .createPersonActivityNotification(person.id, activityOptions)
        .then((data) => {
          return data;
        });

      this.alert.successToast(await this.transformTitle('Employee') +' Updated Successfully');
      this.empAddForm.resetForm();
      this.route.navigate(['/implementation/employees']);
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async openFlyInPanel(templateRef: any) {
    this.editCertObj = undefined;
    this.modalType = "Add"
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  @ViewChild('fileUpload') fileInput: any;

  removeImage() {
    this.url = null;
    this.imageInBase64 = null;
    this.uploadedImage = null;
    this.fileInput.nativeElement.value = '';
    this.image_name = "No Image Selected";
    if (this.person && this.person.image) {
      this.person.image = null;
    }
  }

  selectFile(event: any) {

    //Angular 11, for stricter type
    if (!event.target.files[0] || event.target.files[0].length == 0) {
      this.img = 'You must select an image';
      return;
    }

    const maxSizeInBytes = 5 * 1024 * 1024; // 1 MB in bytes
    const fileSize = event.target.files[0].size;

    var mimeType = event.target.files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.img = 'Only images are supported';
      this.alert.errorToast(
        'Only images are supported'
      );
      return;
    }

    if (!this.allowedTypes.includes(event.target.files[0].type.toLowerCase())) {
      this.img = 'Inavild Image type selected';
      this.alert.errorToast(
        'Please select valid image type (jpg,jpeg,bnp,png,svg)'
      );
      return;
    }

    if (fileSize > maxSizeInBytes) {
      this.img = 'Image size exceeds 1 MB';
      this.alert.errorToast(
        'Image size exceeds 1 MB'
      );
      return;
    }

    let file = event.target.files[0];
    let reader = new FileReader();
    reader.readAsDataURL(file);
    // Reads and converts image to base64 type string
    reader.onloadend = () => {
      this.imageInBase64 = reader.result?.toString();
      this.uploadedImage = file;
      this.url = this.imageInBase64;
    };
    // Reads the name of image
    reader.onload = (_event) => {
      this.img = '';
      this.image_name = event.target.value.substring(12);
    };
    reader.onerror = function (error) {

    };
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSourceOrg.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.dataSourceOrg.data.forEach((row) => this.selection.select(row));
  }

  async getEmpById() {
    await this.empSrvc.get(this.id).then((data) => {
      this.oldUserName = data.person.username;

      this.step1Form = this.fb.group({
        firstName: data.person.firstName,
        middleName: data.person.middleName,
        lastName: data.person.lastName,
        empNum: new UntypedFormControl(data.employeeNumber, [this.alreadyExistsNumberValidator.bind(this)]),
        emailAddr :new UntypedFormControl(data.person.username, [Validators.required,this.whitespaceOnlyValidator,this.alreadyExistsEmailValidator.bind(this)]),
        password: data.password != null ? data.password : 'Password123',
        tqEvaluator: data.tqEqulator,
        address: data.address,
        city: data.city,
        state: data.state,
        zipcode: data.zipCode,
        phoneNumber: data.phoneNumber,
        location: data.workLocation,
        notes: data.notes,
        publicUser: data.publicUser

      
      })
      this.hasFormValue = false;
      this.url = data.person.image;
      this.person.id = data.personId;
      this.employeeName = data.person.firstName + " " + data.person.lastName;

      if (this.navigation === 'certifications') {
        this.mode === 'edit'
        setTimeout(() => {
          this.stepper.next();
        }, 5);
      }

      //this.empModel = data;
      // this.positions = [];
      // this.empModel.employeePositions.forEach((item) =>
      //   this.positions.push(item.position.name)
      // );
      // this.empOrgs = new Map<any, string>();
      // this.empModel.employeeOrganizations.forEach((item) => {
      //   this.empOrgs.set(item.organization.id, item.organization.name);
    });

    let tempSrc: any[] = [];

    //   this.empModel.employeeCertifications.forEach((c) => {
    //     tempSrc.push({
    //       certBody: c.certification.certifyingBody.name,
    //       certNo: c.certificationNumber,
    //       certArea: c.certificationArea,
    //       issueDate: c.issueDate,
    //       recertDate: '',
    //       expireDate: c.expirationDate,
    //       obj: c,
    //     });
    //   });
    //   this.empCertDataSource = new MatTableDataSource(tempSrc);
    //   tempSrc = [];

    //   this.empModel.employeePositions.forEach((p) => {
    //     tempSrc.push({
    //       posName: p.position.name,
    //       startDate: p.startDate,
    //       Trainee: p.trainee,
    //       qualificationDate: p.qualificationDate,
    //       tp_version: '',
    //       endDate: p.endDate,
    //       obj: p,
    //     });
    //   });
    //   this.empPosDataSource = new MatTableDataSource(tempSrc);
    // });
  }
  async getPositionsByEmployeeId(posFilter: 'current' | 'past' | 'all') {

    this.positionsTableData = await this.empSrvc.getPositions(this.id, posFilter);
    this.DataSourcePositions.data = this.positionsTableData.map((data) => {
      return {
        id:data.id,
        positionId: data.positionId,
        positionTitle: data.position.positionTitle,
        startDate: data.startDate,
        trainee: data.trainee,
        qualificationDate: data.qualificationDate,
        trainingProgramVersion: data.trainingProgramVersion,
        endDate: data.endDate,

      }
    });

    setTimeout(() => {
      this.DataSourcePositions.sort = this.posSort;
      this.DataSourcePositions.paginator = this.posPaginator;
    }, 1)
    // await this.empSrvc.getPositions(this.id).then((data) => {
    //
    //   this.DataSourcePositions = data;
    // });
  }

  onSelect(event: any) {
    if (!this.acceptedTypes.includes(event.addedFiles[0].type)) {
      this.alert.errorAlert('Please Enter A Valid PDF File Type');
      return;
    }
    if (event.addedFiles[0].size < this.MAX_SIZE) {
      this.uploadCheck = true;


      this.files.push(event.addedFiles[0]);
      this.readAndUploadFile(event.addedFiles[0]);
    } else {
      this.alert.errorToast(
        'Please select a valid file with size less then 10MB'
      );
    }
  }

  readAndUploadFile(file: any) {
    this.options.employeeID = this.employeeID;
    let reader = new FileReader();
    this.options.uploadFiles = [];
    reader.onload = () => {
      this.options.uploadFiles.push({
        fileAsBase64: reader.result?.toString() ?? '',
        fileName: file.name,
        fileSize: file.size,
        fileType: file.type,
      });
    };
    reader.readAsDataURL(file);
  }

  uploadFiles() {

    var count = this.files.length;
    this.options.uploadFiles.forEach(async (file: any) => {
      var toUpload = new DocumentCreateOptions();
      toUpload.linkedDataId = this.employeeID;
      toUpload.documentTypeId = this.documentTypeId;
      toUpload.file = file.fileName +';'+file.fileAsBase64;
      if (this.mode === 'add') {
        await this.documentService
          .createAsync(toUpload)
          .then((res: any) => {
            count--;
            if (count === 0) {
              this.files = [];
              this.options.uploadFiles = [];
              this.alert.successToast('Files Uploaded Successfully');
            }
          })
          .catch((err: any) => {
            this.alert.errorToast('Failed to Upload Files');
          });
      } else if (this.mode === 'edit') {
        console.log("upload files edit case")
        await this.empSrvc
          .uploadFile(this.id, toUpload)
          .then((res: any) => {
            count--;
            if (count === 0) {
              this.files = [];
              this.options.uploadFiles = [];
              this.alert.successToast('Files Uploaded Successfully');
            }
          })
          .catch((err: any) => {
            this.alert.errorToast('Failed to Upload Files');
          });
      }
    });
  }

  dataURLtoFile(dataurl: any, filename: any) {
    var arr = dataurl.split(','),
      mime = arr[0].match(/:(.*?);/)[1],
      bstr = atob(arr[1]),
      n = bstr.length,
      u8arr = new Uint8Array(n);

    while (n--) {
      u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, { type: mime });
  }

  onRemove(event: any) {
    this.files.splice(this.files.indexOf(event), 1);

    this.options.uploadFiles.splice(this.options.uploadFiles.indexOf(event), 1);

  }

  // onRemove1(event: any) {
  //    this.uploadedFiles.splice(this.uploadedFiles.indexOf(event), 1);

    /* this.options.uploadFiles.splice(this.options.uploadFiles.indexOf(event), 1);
     */
  // }

  async loadActivityNotificationTypes() : Promise<void>{
    await this.activityNotificationservice
      .getAll()
      .then((res) => {
        res.forEach((i) => {
          this.emp.push({
            id: i.id,
            title: i.title,
            checked: false
          });
        });
      })
      .catch((err) => {
        this.alert.errorToast('Error Fetching Activity Notifications');
      });
  }

  changeActivityNotification(value: string, event: any) {
    if (value === 'All') {
      if (event.checked) {
        this.emp.forEach(res => {
          res.checked = true;
        });
        this.empIndex = this.emp.map(res => res.id);
      } else {
        this.emp.forEach(res => {
          res.checked = false;
        });
        this.empIndex = [];
      }
    } else {
      const empObj = this.emp.find(x => x.id === value);
      if (empObj) {
        empObj.checked = event.checked;
        if (event.checked) {
          this.empIndex.push(value);
        } else {
          const index = this.empIndex.indexOf(value);
          if (index > -1) {
            this.empIndex.splice(index, 1);
          }
        }
      }
    }
    this.empIndex =Array.from(new Set(this.empIndex));
  }


  async getPersonActivityNotifications() {
    let linkedActivity: any = [];
    await this.personService
      .getLinkedActivityNotification(this.person.id)
      .then((res) => {
        res.forEach((i) => {
          linkedActivity.push({
            id: i.id,
            title: i.title,
            checked: true
          });
          this.empIndex.push(i.id);
        });
        this.empIndex =Array.from(new Set(this.empIndex));
      })
      .catch((err) => {
        this.alert.errorToast('Error Fetching Linked Activity Notification');
      });

    this.emp.filter(o => linkedActivity.some(({ id, title }) => {
      if (o.id === id && o.title === title) {
        o.checked = true;
      }
    }));
    //     this.disableCheck = false;

  }

  async getOrganizationsByEmployeeId() {
    this.linkedIds = [];
    this.empSrvc.GetLinkedOrganizationtoEmployee(this.id).then((res) => {

      this.dataSourceOrg.data = res.map((x) => {
        return {
          id: x.id,
          isManager: x.isManager,
          organizationId: x.organizationId,
          name: x.organization.name,
        }
      });
      this.dataSourceOrg.paginator = this.organizationpagintor;
      setTimeout(() => {
        this.dataSourceOrg.sort = this.orgSort;
      })
      res.forEach((o) => {
        this.linkedIds.push(o.organizationId);
      });

    });
  }
  ManagerChecked(option) {
    this.empSrvc.ToggleManagerStatus(this.id, option).then((res) => {
      this.alert.successToast('Manager Status Update Successfully');
    });
  }
  // async geEmployeesLinkedToOrganization()
  // {
  //   this.empSrvc.GetLinkedOrganizationtoEmployee(this.id).then((res) =>
  //   {
  //
  //     this.dataSourceOrg = res;
  //
  //   });
  // }

  async getcertificationsByEmployeeId(certfilter: 'current' | 'past' | 'all') {
    this.certificationsTableData = await this.empSrvc.getEmployeeCertifications(
      this.id, certfilter
    );

    this.dataSourceCert.data = this.certificationsTableData.map((data) => {
      return {
        name: data?.name ?? "",
        certificationNumber: data.certificationNumber,
        issueDate: this.datePipe.transform(data.issueDate, 'MM/dd/yyyy'),
        renewalDate: this.datePipe.transform(data.renewalDate, 'MM/dd/yyyy'),
        expirationDate: this.datePipe.transform(data.expirationDate, 'MM/dd/yyyy'),
        certificateName: data.name,
        certificationId: data.certificationId,
        empCertificationId: data.empCertificationId,
        id: data.empCertificationId,
        isExpired: data.isExpired,
        isFromHistory: data.isFromHistory,
        isNERCCertification:data.isNERCCertification
      }
    });


    setTimeout(() => {
      this.dataSourceCert.sort = this.certSort;
    }, 1)
    // await this.empSrvc.getPositions(this.id).then((data) => {
    //
    //   this.DataSourcePositions = data;
    // });
  }

  async openLinkedFlyInPanel(templateRef: any, id: any,rowId:any) {
    this.empPosId = rowId;
    this.modalType = 'Edit';
    this.posId = id;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  isFromHistory:boolean = false;
  async openLinkedFlyInPanelEditCertificate(templateRef: any, data: EMPCertificationVM) {
    this.modalType = 'Edit';
    this.isFromHistory = data.isFromHistory;
    this.posId = data.empCertificationId;
    this.certId = data.certificationId;
    if(this.isFromHistory){
      await this.empSrvc.getEmployeeCertificationFromHistory(data.empCertificationId).then((res: any) => {
        this.editCertObj = res;
        const portal = new TemplatePortal(templateRef, this.vcf);
        this.flyPanelService.open(portal);
      });
    }
    else{
      await this.empSrvc.getEmployeeCertification(data.empCertificationId).then((res: any) => {
        this.editCertObj = res;
        const portal = new TemplatePortal(templateRef, this.vcf);
        this.flyPanelService.open(portal);
      });
    }
  }

  selectedChanged(event: any) {
    if ((event.selectedIndex === 0 || event.selectedIndex === 1) && this.id != undefined) {
      this.mode = 'edit';
    }
    else {
      this.mode = "add";
    }
    if(event.selectedIndex == 0){
      this.isSaveUpdateFailed=true;
    }
  }

  readyStep1Form() {
    this.step1Form = this.fb.group({
      firstName: new UntypedFormControl('',  [Validators.required,this.whitespaceOnlyValidator]),
      middleName: new UntypedFormControl(''),
      lastName: new UntypedFormControl('',  [Validators.required,this.whitespaceOnlyValidator]),
      empNum: new UntypedFormControl('',[this.alreadyExistsNumberValidator.bind(this)]),
      emailAddr: new UntypedFormControl('',  [Validators.required,this.whitespaceOnlyValidator,this.alreadyExistsEmailValidator.bind(this)]),
      password: new UntypedFormControl(''),
      tqEvaluator: new UntypedFormControl(false),
      address: new UntypedFormControl(''),
      city: new UntypedFormControl(''),
      state: new UntypedFormControl(''),
      zipcode: new UntypedFormControl(''),
      phoneNumber: new UntypedFormControl(''),
      location: new UntypedFormControl(''),
      notes: new UntypedFormControl(''),
      publicUser: new UntypedFormControl(false),
    });
  }
  readyStep2Form() {
    // this.step2Form = this.fb.group({
    //   certRequired:new FormControl(false)
    // });
  }

  async tryPersist(){
    if (this.mode === 'edit') {
      let userUpdateoption: UserUpdateOptions = {
        oldName: this.oldUserName,
        newName: this.step1Form.get('emailAddr')?.value,
      };

      if(this.oldUserName== '' || this.oldUserName== null){
          this.isSaveUpdateFailed = false;
          this.createAspNetUserAsync(jwtAuthHelper.SelectedInstance);
        }else{
          await this.userService.updateUser(userUpdateoption).then((data) => {
            this.isSaveUpdateFailed = false;
            this.oldUserName = userUpdateoption.newName
            return data;
          })
          .catch(async reason=>{
            //var message = reason?.error?.message.replace(/"/g,'');
            var message = reason.headers.get('Application-Error');
            if( message == "Employee Username already in use"){
              this.alreadyExistsUserName = this.step1Form.get('emailAddr')?.value;
              this.step1Form.get('emailAddr')?.updateValueAndValidity();
              this.isSaveUpdateFailed = true;
              this.alert.errorToast(await this.dynamicLabelReplacementPipe.transform(message.replace(/"/g,'')));
            }
            if(reason?.status==404){
              this.isSaveUpdateFailed = false;
              await this.createAspNetUserAsync(jwtAuthHelper.SelectedInstance);
            }
          });;
        }
      if(this.isSaveUpdateFailed) return;
      let createOpt: PersonUpdateOption = {
        firstName: this.step1Form.get('firstName')?.value,
        lastName: this.step1Form.get('lastName')?.value,
        middleName: this.step1Form.get('middleName')?.value,
        username: this.step1Form.get('emailAddr')?.value,
        image: this.imageInBase64,
      };
      this.employeeName = this.step1Form.get('firstName')?.value + ' ' + this.step1Form.get('lastName')?.value;
      await this.personService
        .update(this.person.id, createOpt)
        .then(async (person) => {
          this.isSaveUpdateFailed = false;
          if (person) {
            let empCreateOpt: EmployeeUpdateOptions = {
              personId: this.person.id,
              employeeNumber: this.step1Form.get('empNum')?.value,
              username: this.step1Form.get('emailAddr')?.value,
              City: this.step1Form.get('city')?.value,
              State: this.step1Form.get('state')?.value,
              ZipCode: this.step1Form.get('zipcode')?.value ?? 0,
              PhoneNumber: this.step1Form.get('phoneNumber')?.value ?? 0,
              WorkLocation: this.step1Form.get('location')?.value,
              Notes: this.step1Form.get('notes')?.value,
              Password: 'dummypassword',
              TQEqulator: this.step1Form.get('tqEvaluator')?.value,
              Address: this.step1Form.get('address')?.value,
              PublicUser: this.step1Form.get('publicUser')?.value
            };
            await this.empSrvc
              .update(this.id, empCreateOpt)
              .then( async (data) => {
                this.isSaveUpdateFailed = false;
                if (this.uploadCheck) {
                  this.uploadFiles();
                }

                let activityOptions: PersonActivityNotificationOptions_VM = {
                  personId: person.id,
                  activityNotificationIds: this.empIndex,
                };
                if (this.empIndex) {
                  this.empSpinner = true;
                  await this.personService
                  .createPersonActivityNotification(person.id, activityOptions)
                    .then((data) => {
                      this.isSaveUpdateFailed = false;
                      return data;
                    }).finally(() => {
                      this.empSpinner = false;
                    });
                }

                this.alert.successToast(await this.transformTitle('Employee') +' Updated Successfully');
              })
              .catch(async reason=>{
                var message = reason.replace(/"/g,'');
                if( message == "Employee Number already in use"){
                  this.alreadyExistsEmpNumber = this.step1Form.get('empNum')?.value;
                  this.step1Form.get('empNum')?.updateValueAndValidity();
                  this.isSaveUpdateFailed = true;
                }
                this.alert.errorToast(await this.dynamicLabelReplacementPipe.transform(reason.replace(/"/g,'')));
              });
          }
        })
        .catch(async reason=>{
          var message = reason.replace(/"/g,'');
          if( message == "Employee Username already in use"){
            this.alreadyExistsUserName = this.step1Form.get('emailAddr')?.value;
            this.step1Form.get('emailAddr')?.updateValueAndValidity();
            this.isSaveUpdateFailed = true;
          }
          this.alert.errorToast(await this.dynamicLabelReplacementPipe.transform(reason.replace(/"/g,'')));
        });
    }
    else {
      this.empSpinner = true;
      var url = location.origin;
      this.empLicensedAvailable = false;
      var licenseSettingData = await this.clientSettingService.GetCurrentLicenseAsync().finally(() => { this.empSpinner = false });
      var licenseActiveStatus = licenseSettingData.active
      var licenseexpiration = this.datePipe.transform(licenseSettingData.expiration, "yyyy-MM-dd")
      var totalRecords = licenseSettingData.totalEmployeeRecordsAvailable
      var employeeRecordUsed = licenseSettingData.employeeRecordsUsed
      var productAcronym = licenseSettingData.products[1].productAcronym
      var empPortalenabled = licenseSettingData.products[1].enabled;
      var todaysDate = this.datePipe.transform(Date.now(), "yyyy-MM-dd");
      var availableRecordsLimit = totalRecords - employeeRecordUsed;
      this.licenceNumber = licenseSettingData.activationCode;
      // availableRecordsLimit = 0
      if (licenseActiveStatus === true && licenseexpiration > todaysDate) {
        if (availableRecordsLimit > 0 && productAcronym === 'EMP' && empPortalenabled === true) {
          this.empLicensedAvailable = true;
        }
      }
      if (availableRecordsLimit === 0) {
        const deRef = this.dialog.open(this.limitExceedRef, {
          width: '600px',
          height: 'auto',
          hasBackdrop: true,
          disableClose: true,
        });
        return;
      }

      let useroption: User = {
        name: this.step1Form.get('emailAddr')?.value,
        password: this.step1Form.get('password')?.value ?? 'Password1',
        empEnabled: this.empLicensedAvailable,
        instanceName: jwtAuthHelper.SelectedInstance,
        createWithIdentityProvider:"default"
      };
      this.empSpinner = true;
      let user = await this.userService.createUser(useroption).then(async (data) => {
        await this.createPersonAsync();
        return data;
      }).catch(async (error) => {
        if(error?.status == 409){
          await this.createPersonAsync();
        }
        else{
          this.isSaveUpdateFailed = true;
          this.alert.errorToast("Failed to create User: " + error);
        }
      }).finally(() => {
        this.empSpinner = false;
      });
    }

    if(this.id && !this.isSaveUpdateFailed){
      await this.getEmpById();
      await this.getPositionsByEmployeeId(this.posFilter);
      await this.getPersonActivityNotifications();
      await this.getOrganizationsByEmployeeId();
      await this.getcertificationsByEmployeeId(this.certFilter);
      //this.alert.successAlert(this.translate.instant('L.recAdded'));
    }
  }

  empSpinner = false;
  async SaveEmployeeAndstepNext() {
    await this.tryPersist();
    this.stepper.next();
  }

  async DelPosition(e: any) {
   await this.empSrvc
      .deletempposition(this.id, this.posIdtoDelete,this.empPosId)
      .then(async (res: any) => {
        this.alert.successToast(await this.transformTitle('Employee') +' '+ await this.transformTitle('Position') + ' deleted successfully');
        this.getPositionsByEmployeeId(this.posFilter);
      });
  }

 async deletePosition(templateRef: any, title: any, id: any,empPosId:any) {
    var employeeName = this.step1Form.get('firstName')?.value + "-" + this.step1Form.get('lastName')?.value
    this.posIdtoDelete = id;
    this.empPosId = empPosId;
    this.deleteDescription = `You are selecting to delete ` + await this.transformTitle('Position') +` ${title} from ${employeeName} profile. Deleting the ` + await this.transformTitle('Position') +` from the selected ` + await this.labelPipe.transform('Employee') + ` will remove all date information (e.g., Start Date, Qualification Date, Trainee status, etc.). Completed ` + await this.transformTitle('Task') +` Qualifications and ` + await this.labelPipe.transform('ILA') +` completion records associated with the ` + await this.labelPipe.transform('Employee') + ` will remain active.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  UnlinkOrganization(templateRef: any, title: any, id: any) {
    this.unlinkOrgDescription = `You are selecting to unlink ${title}`;
    this.orgToUnlink = id;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async createPersonAsync(){
    let createOpt: PersonCreateOption = {
      firstName: this.step1Form.get('firstName')?.value,
      lastName: this.step1Form.get('lastName')?.value,
      middleName: this.step1Form.get('middleName')?.value,
      username: this.step1Form.get('emailAddr')?.value,
      image: this.imageInBase64,
    };
    this.employeeName = this.step1Form.get('firstName')?.value + ' ' + this.step1Form.get('lastName')?.value;
    this.empSpinner = true;
    await this.personService.create(createOpt).then(async (person) => {
      this.isSaveUpdateFailed = false;
      if (person) {
        let empCreateOpt: EmployeeCreateOptions = {
          personId: person.id,
          employeeNumber: this.step1Form.get('empNum')?.value,
          username: this.step1Form.get('emailAddr')?.value,
          City: this.step1Form.get('city')?.value,
          State: this.step1Form.get('state')?.value,
          ZipCode: this.step1Form.get('zipcode')?.value ?? 0,
          PhoneNumber: this.step1Form.get('phoneNumber')?.value ?? 0,
          WorkLocation: this.step1Form.get('location')?.value,
          Notes: this.step1Form.get('notes')?.value,
          Password: 'dummypassword',
          TQEqulator: this.step1Form.get('tqEvaluator')?.value,
          Address: this.step1Form.get('address')?.value,
          PublicUser: this.step1Form.get('publicUser')?.value,
          empEnabled: this.empLicensedAvailable,
          websiteUrl: location.origin
        };
        this.empSpinner = true;
        await this.empSrvc.create(empCreateOpt).then(async (data) => {
          this.isSaveUpdateFailed = false;
          this.employeeID = data.id;
          //this.certificationsnotrequired = data.isCertificationRequired
          if (this.uploadCheck === true) {
            this.uploadFiles();
          }

          if (this.empIndex) {
            let activityOptions: PersonActivityNotificationOptions_VM = {
              personId: person.id,
              activityNotificationIds: this.empIndex,
            };
            this.empSpinner = true;
            await this.personService
            .createPersonActivityNotification(person.id, activityOptions)
              .then((data) => {
                return data;
              }).finally(() => {
                this.empSpinner = false;
              });
          }

          this.id = this.employeeID;
          // this.alert.successToast('Employee Added Successfully');
          this.alert.successToast(await this.transformTitle('Employee') +' Added Successfully');
          return data;
        })
        .catch(async reason=>{
          var message = reason.replace(/"/g,'');
          if( message == "Employee Number already in use"){
            this.alreadyExistsEmpNumber = this.step1Form.get('empNum')?.value;
            this.step1Form.get('empNum')?.updateValueAndValidity();
            this.isSaveUpdateFailed = true;
          }
          this.alert.errorToast(await this.dynamicLabelReplacementPipe.transform(reason.replace(/"/g,'')));
          await this.personService.delete(person.id);
        }).finally(() => {
          this.empSpinner = false;
        });
      }
    })
    .catch(async reason=>{
      var message = reason.replace(/"/g,'');
      if( message == "Employee Username already in use"){
        this.alreadyExistsUserName = this.step1Form.get('emailAddr')?.value;
        this.step1Form.get('emailAddr')?.updateValueAndValidity();
        this.isSaveUpdateFailed = true;
      }
      this.alert.errorToast(await this.dynamicLabelReplacementPipe.transform(reason.replace(/"/g,'')));
    })
    .finally(() => {
      this.empSpinner = false;
    });
  }

  unlinkOrganization(e: any) {
    this.empSrvc.deleteOrganization(this.id, this.orgToUnlink).then(async (res: any) => {
      this.alert.successToast(await this.labelPipe.transform('Employee') + ' Organization Unlinked successfully');
      this.getOrganizationsByEmployeeId();
    });
  }

  async deletecerttificate(templateRef: any, row:any) {
    var transformedCertValue = await this.transformTitle("Certification");
    var employeeName = this.step1Form.get('firstName')?.value + " - " + this.step1Form.get('lastName')?.value

    this.isFromHistory = row.isFromHistory;
    if(this.isFromHistory){
      this.certIdtoDelete = row.id;
    }
    else{
      this.certIdtoDelete = row.id;
    }
    this.deletecertDescription = `You are selecting to delete the ${row.name} ${transformedCertValue} data for ${employeeName} this action cannot be undone. `;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async Delcertification(e: any) {
    var transformedCertValue = await this.transformTitle("Certification");
    if(this.isFromHistory){
      this.empSrvc
      .deleteCertificationsFromHist(this.certIdtoDelete)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Employee') + ' '+ transformedCertValue + ' History deleted successfully');
        this.getcertificationsByEmployeeId(this.certFilter);
      });
    }
    else{
      this.empSrvc
      .deleteCertifications(this.certIdtoDelete)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Employee') + ' '+ transformedCertValue+' deleted successfully');
        this.getcertificationsByEmployeeId(this.certFilter);
      });
    }
  }

  PositionsFilter(filter: 'current' | 'past' | 'all') {
    this.posFilter = filter;
    this.getPositionsByEmployeeId(this.posFilter);
  }

  CertificatiomFilter(filter:'current'|'past'|'all') {
    this.certFilter = filter;
    this.getcertificationsByEmployeeId(this.certFilter);
  }

  async openRenewCertificateFlyPanel(templateRef: any, certid: any, empcertId: any) {
    this.modalType = 'Edit';
    this.posId = certid;
    this.empCertId = empcertId;
    await this.empSrvc.getEmployeeCertification(this.empCertId).then((res: any) => {
      this.editCertObj = res;
      const portal = new TemplatePortal(templateRef, this.vcf);
      this.flyPanelService.open(portal);
    });
  }
  async changeEMPCertStatus(event: any) {
    var transformedCertValue = await this.transformTitle("Certification");
    let option = {
      certRequired: event.checked,
    };
    if (event.checked) {
      //this.certificationsnotrequired = true;
    }
    else {
      //this.certificationsnotrequired = false;
    }
    this.empSrvc.setCerificationStatus(this.id, option).then(async (res: any) => {
      this.alert.successToast(await this.labelPipe.transform('Employee') + ' '+transformedCertValue +' status change successfully');
    });
  }

  close() {
    this.flyPanelService.close();
    this.getOrganizationsByEmployeeId()
  }

  async openManagerPopup(templateRef: any, checked: boolean, orgId: any, row: any) {
    this.modalDescription = `You are selecting to make ${this.employeeName} an Organization Manager for ${row}. This will allow this ` + await this.labelPipe.transform('Employee') + ` to view training completion reports for all ` + await this.labelPipe.transform('Employee') + `s linked to the ${row}`;
    this.modalHeader = "Organization Manager ";
    this.optionOrganizationManager = {
      organizationId: orgId,
      isManager: checked,
    };
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async downloadFile(fileId: any) {
    // const linkSource = file.fileAsBase64;
    // const downloadLink = document.createElement("a");
    // const fileName = file.fileName;
    // downloadLink.href = linkSource;
    // downloadLink.download = fileName;
    // downloadLink.click();
    await this.empSrvc.getDownloadData(this.id, fileId).then((res: any) => {
      const linkSource = res.fileAsBase64;
      const downloadLink = document.createElement("a");
      const fileName = res.fileName;
      downloadLink.href = linkSource;
      downloadLink.download = fileName;
      downloadLink.click();
      this.alert.successToast("Document Downloaded Successfully");
    });
  }

  public sendForgotPasswordEmail() {
    this.authService
  .sendResetLink(this.step1Form.get('emailAddr')?.value)
  .subscribe({
    next: (res) => {
      this.alertService.successToast(this.translate.instant('L.' + res));
    },
    error: (err) => {
      this.alertService.errorToast(this.translate.instant('L.' + err.error.message));
    }
  });
  }

  async viewCertificationHistory(templateRef: any, empCertification: any) {
    this.modalType = 'Edit';
    this.empCertification=empCertification;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async createAspNetUserAsync(instanceName : string){
    let user = new User();
    user.name = this.step1Form.get('emailAddr')?.value;
    user.empEnabled = this.empLicensedAvailable;
    user.password = this.step1Form.get('password')?.value ?? 'Password1';
    user.instanceName = instanceName;
    user.createWithIdentityProvider = "default";
    this.userService.createUser(user).then(async (res) => {
    }).catch( (error) => {
        if(error?.status != 409){
            this.alert.errorToast("Failed to create User: " + error);
            throw error;
        }
    });
}

async getDocumentTypeAsync(){
  var documentTypes = await this.apiDocumentTypeService.getAllActiveAsync();
  this.documentTypeId = documentTypes.find(x=>x.linkedDataType=="Employees")?.id;
}

getCertStatus(expirationDateStr: string, isNercCert: boolean): string {
  if (!expirationDateStr || !isNercCert) return 'Expired';

  const expirationDate = new Date(expirationDateStr);
  const currentDate = new Date();

  const suspendedUntil = new Date(expirationDate);
  suspendedUntil.setFullYear(suspendedUntil.getFullYear() + 1);

  if (currentDate <= suspendedUntil) {
    return "Suspended";
  } else {
    return "Expired";
  }
}

viewIDP(){
  this.route.navigate(['/implementation/employees/idp', this.id])
}

}
