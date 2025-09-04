import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatStep, MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { Client } from '@models/Client/ClientViewModel';
import { ClientService } from 'src/app/_Services/Auth/client.service';
import { Store } from '@ngrx/store';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';
import { Instance } from '@models/Instance/Instance';
import { SubSink } from 'subsink';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ClientSettings_License } from '@models/ClientSettingsLicense/ClientSettings_License';
import { InstanceSetting } from '@models/Instance/InstanceSetting';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { PersonWithUserDataVm } from '@models/Person/PersonWithUserDataVm';
import { InstanceSetupService } from 'src/app/_Services/QTD/Instance/instance-setup.service';
import { UserService } from 'src/app/_Services/Auth/user.service';

@Component({
  selector: 'app-instance-setup-wizard',
  templateUrl: './instance-setup-wizard.component.html',
  styleUrls: ['./instance-setup-wizard.component.scss']
})
export class InstanceSetupWizardComponent implements OnInit {
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('selectClient') selectClient: MatSelect;
  @ViewChild('selectInstance') selectInstance: MatSelect;
  subscription = new SubSink();
  licenseDataSource = new MatTableDataSource<any>();
  productDetailDataSource = new MatTableDataSource<any>();
  personsWithUserDataSource = new MatTableDataSource<PersonWithUserDataVm>();
  tempPersonsWithUserDataSource: any[] = [];
  clientForm: UntypedFormGroup;
  instanceForm: UntypedFormGroup;
  licenseForm: UntypedFormGroup;
  usersForm: UntypedFormGroup;
  currentIndex: number = 0;
  previousIndex: number = 0;
  clientList: Client[];
  tempClientList: Client[];
  instanceList: Instance[];
  tempInstanceList: Instance[];
  licenseHistoryList: ClientSettings_License[];
  licenseDetail: any;
  selectedClient: Client;
  selectedInstance: Instance;
  selectedInstanceDetail: InstanceSetting;
  personWithUserDataVm: PersonWithUserDataVm = new PersonWithUserDataVm();
  loader: boolean = false;
  expandLicenseTable: boolean = false;
  licenseEditMode: string;
  instanceFlyInMode: string;
  instanceSearchString: string = "";
  clientSearchString: string = "";
  userSearchString: string = "";
  actionMode: string;
  expandedData: any | null;
  readding: boolean = false;
  @ViewChild('licenseDataSort') set licenseDataSort(sorting: MatSort) {
    if (sorting) this.licenseDataSource.sort = sorting;
  }
  @ViewChild('productDataSort') productDataSort: MatSort;
  @ViewChild('personDataSort') set personDataSort(sorting: MatSort) {
    if (sorting) this.personsWithUserDataSource.sort = sorting;
  }
  displayedLicenseColumns: string[] = [
    'activationCode',
    'createdDate',
    'active'
  ];
  displayLicenseExpandColumns: string[] = [
    'productName',
    'productAcronym',
    'releaseDate',
    'companyProduct'
  ];
  displayedPersonWithUserDataColumns: string[] = [
    'username',
    'firstName',
    'lastName',
    'isEmployee',
    'employeeNumber',
    'isTQEvaluator',
    'isInstructor',
    'instructorCategoryTitle',
    'instructorNumber',
    'isQTDUser',
    'status',
    'options'
  ];
  constructor(private router: Router,
    public dialog: MatDialog,
    private formBuilder: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private _client: ClientService,
    private store: Store<{ toggle: string }>,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    private instanceSetupService: InstanceSetupService,
    private userService: UserService,
  ) { }

  async ngOnInit(): Promise<void> {
    this.store.dispatch(sideBarClose());
    this.initializeClientForm();
    this.initializeInstanceForm();
    await this.getAllClients();

    this.subscription.sink = this.route.queryParams.subscribe(async (res) => {
      if (res.clientId != null) {
        this.clientForm.patchValue({
          clientId: res.clientId
        });
        const clientDetail = this.clientList?.find(x => x.id == res.clientId)
        this.selectedClient = clientDetail;
        this.initializeClientForm();
        if (res.instanceId != null) {
          this.instanceForm.patchValue({
            instanceId: res.instanceId
          });
          const clientDetail = this.clientList?.find(x => x.id == res.clientId)
          this.selectedClient = clientDetail;
          await this.getInstancesAsync()
          const instanceDetail = this.instanceList?.find(x => x.id == res.instanceId)
          this.selectedInstance = instanceDetail;
          this.selectedInstanceDetail = instanceDetail?.instanceSetting;
          this.stepper?.next();
        }
      }
    })
    this.handleSpaceForSearch();
  }

  initializeClientForm() {
    this.clientForm = this.formBuilder.group({
      clientId: [this.selectedClient?.id, Validators.required],
      searchClient: [""]
    });
  }

  initializeInstanceForm() {
    this.instanceForm = this.formBuilder.group({
      instanceId: [this.selectedInstance?.id, Validators.required],
      searchInstance: [""],
    });
  }

  handleSpaceForSearch() {
    this.selectClient._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE')
        return
    };
    this.selectInstance._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE')
        return
    };
  }

  async onStepSelectionChange(event: any) {
    this.currentIndex = event.selectedIndex;
    this.previousIndex = event.previouslySelectedIndex;
    let stepDifference = event.selectedIndex - event.previouslySelectedIndex;
    if (this.currentIndex == 1) {
      await this.getInstancesAsync();
    }
    if (this.currentIndex == 2) {
      await this.getLicenseHistoryAsync();
      this.expandLicenseTable = false;
    }
    if (this.currentIndex == 3) {
      await this.getPersonsWithUserDataAsync();
      this.licenseDetail = this.licenseHistoryList.find(x => x.active);
      this.userSearchString = "";
    }
    let matSteps: MatStep[] = this.stepper.steps.toArray();
    if (stepDifference < 1) {
      for (let i = event.previouslySelectedIndex; i > event.selectedIndex; i--) {
        matSteps[i].interacted = false;
        matSteps[i].completed = false;
      }
    }
    else if (stepDifference > 0) {
      matSteps[event.previouslySelectedIndex].interacted = true;
      matSteps[event.previouslySelectedIndex].completed = true;
    }
  }

  exitWizard(templateRef: any) {
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  backOrExitWizard() {
    this.router.navigate(['home/index']);
  }

  nextStep() {
    this.currentIndex = this.stepper.selectedIndex;
    this.stepper.next();
  }

  async openClientFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async openUserFlyInPanel(templateRef: any, detail: PersonWithUserDataVm, action: string, isReadd: boolean = false) {
    this.actionMode = action;
    this.readding = isReadd;
    if (action == "edit") {
      this.personWithUserDataVm = detail;

    }
    else {
      this.personWithUserDataVm = new PersonWithUserDataVm();
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async openLicenseFlyInPanel(templateRef: any, mode: string) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.licenseEditMode = mode;
    if (mode == 'Update') {
      this.licenseDetail = this.licenseHistoryList.find(x => x.active);
    }
    this.flyPanelService.open(portal);
  }

  async openInstanceFlyInPanel(templateRef: any, mode: string) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.licenseEditMode = mode;
    this.instanceFlyInMode = mode == 'create' ? 'create' : 'edit';
    this.flyPanelService.open(portal);
  }

  async getAllClients() {
    await this._client.getAllClients().then((res) => {
      this.clientList = res;
      this.clientList = this.clientList.sort((a, b) => a?.name.localeCompare(b?.name));
      this.tempClientList = res;
    });
  }


  async getInstancesAsync() {
    await this._client.getClientInstances(this.selectedClient?.name).then((res) => {
      this.instanceList = res;
      let tempSrc: any[] = [];
      this.instanceList?.forEach((i) => {
        tempSrc.push({
          name: i.name,
          db_v: 'v 1.0',
          active: i.active,
          obj: i,
        });
      });
      this.instanceList = this.instanceList.sort((a, b) => a?.name.localeCompare(b?.name));
      this.tempInstanceList = this.instanceList;
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Instance Data Not Fetched");
    });
  }

  onClientSelected(event: any) {
    const clientDetail = this.clientList.find(x => x.id == event.value)
    this.selectedClient = clientDetail;
    this.selectedInstance = null;
    this.selectedInstanceDetail = null;
    this.instanceForm.reset();
  }

  onInstanceSelected(event: any) {
    const instanceDetail = this.instanceList.find(x => x.id == event.value)
    this.selectedInstance = instanceDetail;
    this.selectedInstanceDetail = instanceDetail?.instanceSetting;
  }

  getNewClient(client: any) {
    this.clientList.push(client);
    this.clientList = this.clientList.sort((a, b) => a?.name.localeCompare(b?.name));
  }

  getNewInstance(instance: any) {
    this.loader = true;
    this.instanceList.push(instance);
    this.instanceList = this.instanceList.sort((a, b) => a?.name.localeCompare(b?.name));
    this.loader = false;
  }

  updateInstanceDetail(instance: any) {
    this.loader = true;
    const updateInstanceDataIndex = this.instanceList.findIndex(x => x.name === instance?.name);
    if (updateInstanceDataIndex !== -1) {
      this.instanceList[updateInstanceDataIndex] = instance;
      this.selectedInstance = instance;
      this.selectedInstanceDetail = instance?.instanceSetting;
    }
    this.loader = false;
  }


  getNewLicense(license: any) {
    this.loader = true;
    for (let record of this.licenseHistoryList) {
      record.active = false;
    }
    this.licenseHistoryList.push(license);
    this.licenseHistoryList = this.licenseHistoryList.sort(this.customSortWithNullHandling);
    this.licenseDetail = this.licenseHistoryList.find(x => x.active);
    this.licenseDataSource = new MatTableDataSource(this.licenseHistoryList);
    this.loader = false;
  }

  async getNewUser(event) {
    this.loader = true;
    this.setNewUserDetails(event);
    this.loader = false;
  }

  setNewUserDetails(newUserData: PersonWithUserDataVm) {
    if (newUserData?.isEmployee == true) {
      this.licenseDetail.employeeRecordsUsed = this.licenseDetail.employeeRecordsUsed + 1;
    }
    let existingUserIndex = this.tempPersonsWithUserDataSource.findIndex(x => x.username == newUserData.username);
    if (existingUserIndex != -1) {
      this.tempPersonsWithUserDataSource[existingUserIndex] = newUserData;
    }
    else {
      this.tempPersonsWithUserDataSource.push(newUserData);
    }
    this.tempPersonsWithUserDataSource = [...this.tempPersonsWithUserDataSource];
    this.filterUserData();
  }

  async getUpdateUser(event) {
    this.loader = true;
    this.setUpdateUserDetails(event);
    this.loader = false;
  }

  setUpdateUserDetails(updateUserData: PersonWithUserDataVm) {
    const index = this.tempPersonsWithUserDataSource.findIndex(record => record.personId === updateUserData.personId);
    if (index !== -1) {
      this.tempPersonsWithUserDataSource[index] = updateUserData;
    } else {
      this.tempPersonsWithUserDataSource.push(updateUserData);
    }
    if (updateUserData.isEmployee) {
      this.licenseDetail.employeeRecordsUsed += 1;
    }
    else {
      this.licenseDetail.employeeRecordsUsed -= 1;
    }
    this.tempPersonsWithUserDataSource = [...this.tempPersonsWithUserDataSource];
    this.filterUserData();
  }

  async getLicenseHistoryAsync() {
    this.loader = true;
    await this.instanceSetupService.getLicenseHistoryAsync(this.selectedInstance?.name).then((res) => {
      this.licenseHistoryList = res;
      this.licenseDetail = this.licenseHistoryList.find(x => x.active);
      this.licenseHistoryList = this.licenseHistoryList.sort(this.customSortWithNullHandling);
      this.licenseDetail = this.licenseHistoryList.find(x => x.active);
      this.licenseDataSource = new MatTableDataSource(this.licenseHistoryList);
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Persons Data Not Fetched");
    });
  }

  getLicenseProductDetails(id: string) {
    this.loader = true;
    this.licenseDetail = this.licenseHistoryList.find(x => x.id == id);
    const productDetailList = this.licenseDetail.products;
    this.productDetailDataSource = new MatTableDataSource(productDetailList);
    this.expandLicenseTable = !this.expandLicenseTable
    this.loader = false;
  }

  customSortWithNullHandling(a: any, b: any): number {
    if (a.active === b.active) {
      return new Date(a.createdDate).getTime() - new Date(b.createdDate).getTime();
    }
    return a.active ? -1 : 1;
  }

  async getPersonsWithUserDataAsync() {
    this.loader = true
    await this.instanceSetupService.getPersonsWithUserDataAsync(this.selectedInstance?.name).then((res) => {
      this.personsWithUserDataSource = new MatTableDataSource(res)
      this.tempPersonsWithUserDataSource = this.personsWithUserDataSource.data;
      this.loader = false;
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Persons Data Not Fetched");
    });
  }

  isDisableContinueButton() {
    if (this.currentIndex == 0) {
      if (this.clientForm.invalid) {
        return true;
      }
    }
    if (this.currentIndex == 1) {
      if (this.instanceForm.invalid) {
        return true;
      }
    }

    return false;
  }

  sortPersonsWithUserData(matSort: MatSort) {
    this.personsWithUserDataSource.sort = matSort;
  }

  sortProductDetailDataSource(matSort: MatSort) {
    this.productDetailDataSource.sort = matSort;
  }

  sortLicenseDataSource(sort: Sort) {
    if (sort.active == 'createdDate') {
      this.licenseDataSource.data.sort((a, b) => {
        if (a.createdDate instanceof Date && b.createdDate instanceof Date) {
          return a.createdDate.getTime() - b.createdDate.getTime();
        } else {
          if (!(a.createdDate instanceof Date)) return 1;
          if (!(b.createdDate instanceof Date)) return -1;
          return 0;
        }
      });
    }
  }
  getCompletion(index: number) {
    if (this.currentIndex == 2) {
      return true;
    }
    if (this.stepper) {
      let matSteps: MatStep[] = this.stepper.steps.toArray();
      return matSteps[index].completed;
    }
    return false;
  }

  inputSearchClient(event: any) {
    this.clientSearchString = event.target.value;
    this.filterClientData();
  }

  filterClientData() {
    const searchTerm = this.clientSearchString.trim().toLowerCase();
    this.clientList = this.tempClientList.filter(item => {
      return (
        (item?.name?.trim().toLowerCase().includes(searchTerm))
      );
    });
  }

  inputSearchInstance(event: any) {
    this.instanceSearchString = event.target.value;
    this.filterInstanceData();
  }

  filterInstanceData() {
    const searchTerm = this.instanceSearchString.trim().toLowerCase();
    this.instanceList = this.tempInstanceList.filter(item => {
      return (
        (item?.name?.trim().toLowerCase().includes(searchTerm))
      );
    });
  }

  inputSearchUser(event: any) {
    this.userSearchString = event.target.value;
    this.filterUserData();
  }

  filterUserData() {
    const searchTerm = this.userSearchString.trim().toLowerCase();
    this.personsWithUserDataSource.data = this.tempPersonsWithUserDataSource.filter(item => {
      return (
        (item?.username?.trim().toLowerCase().includes(searchTerm)) ||
        (item?.firstName?.trim().toLowerCase().includes(searchTerm)) ||
        (item?.lastName?.trim().toLowerCase().includes(searchTerm)) ||
        (item?.isEmployee && item?.employeeNumber?.trim().toLowerCase().includes(searchTerm)) ||
        (item?.isInstructor && item?.instructorCategoryTitle?.trim().toLowerCase().includes(searchTerm)) ||
        (item?.isInstructor && item?.instructorNumber.toString().trim().toLowerCase().includes(searchTerm))
      );
    });
  }

  clearUserSearch() {
    this.userSearchString = "";
    this.filterUserData();
  }


  async removeUserByInstanceAsync(row: PersonWithUserDataVm) {
    var instanceName = this.selectedInstance.name;
    await this.userService.removeUserByInstanceAsync(row.username, instanceName).catch(async (error) => {
      if (error?.status != 404) {
        this.alert.errorToast("Failed to remove User: " + error?.message);
        throw error;
      }
    });
    await this.instanceSetupService.deactivatePersonAsync(instanceName, row.personId).then(res => {
      row.username = res.person?.username;
      row.firstName = res.person?.firstName;
      row.lastName = res.person?.lastName;
      row.active = false;
    });
    await this.instanceSetupService.deactivateClientUserAsync(instanceName, row.personId);
    if (row.isQTDUser) {
      await this.instanceSetupService.deactivateQTDUserAsync(instanceName, row.qtdUserId).then(res => {
        row.isQTDUser = row.isQTDUserActive = false;

      });
    }
    if (row.isEmployee) {
      await this.instanceSetupService.deactivateEmployeeAsync(instanceName, row.employeeId).then(res => {
        row.isEmployee = row.isEmployeeActive = false;
      });
    }
    if (row.isInstructor) {
      await this.instanceSetupService.deactivateInstructorAsync(instanceName, row.instructorId).then(res => {
        row.isInstructor = row.isInstructorActive = false;
      });
    }
    this.alert.successToast("User Removed Successfully");
  }
}


