import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { User } from '@models/User/User';
import { UserService } from 'src/app/_Services/Auth/user.service';
import { Instance } from '@models/Instance/Instance';
import { ClientSettings_License } from '@models/ClientSettingsLicense/ClientSettings_License';
import { PersonWithUserDataVm } from '@models/Person/PersonWithUserDataVm';
import { PersonCreateOption } from '@models/Person/PersonCreateOption';
import { ClientUserCreateOptions } from '@models/ClientUser/ClientUserCreateOptions';
import { Person } from '@models/Person/Person';
import { QtdUserVM } from '@models/QtdUser/QtdUserVM';
import { EmployeeCreateOptions } from '@models/Employee/EmployeeCreateOptions';
import { Instructor_CreateOptions } from '@models/Instructors/Instructor_CreateOptions';
import { Instructor } from '@models/Instructors/Instructor';
import { Employee } from '@models/Employee/Employee';
import { Instructor_Category } from '@models/Instructor_Category/Instructor_Category';
import { InstanceSetupService } from 'src/app/_Services/QTD/Instance/instance-setup.service';
import { UserUpdateOptions } from '@models/User/UserUpdateOptions';
import { PersonUpdateOption } from '@models/Person/PersonUpdateOption';
import { EmployeeUpdateOptions } from '@models/Employee/EmployeeUpdateOptions';
import { ClientService } from 'src/app/_Services/Auth/client.service';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Instructor_UpdateByEmailOptions } from '@models/Instructors/Instructor_UpdateByEmailOptions';
import { AuthService } from 'src/app/_Services/Auth/auth.service';
import { IdentityProviderVM } from '@models/IdentityProvider/IdentityProviderVM';
import { InstanceService } from 'src/app/_Services/Auth/instance.service';
import { IdentityProviderUpdateModel } from '@models/IdentityProvider/IdentityProviderUpdateModel';

@Component({
  selector: 'app-fly-panel-add-user',
  templateUrl: './fly-panel-add-user.component.html',
  styleUrls: ['./fly-panel-add-user.component.scss']
})
export class FlyPanelAddUserComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() newUserDetail = new EventEmitter<PersonWithUserDataVm>();
  @Output() updateUserDetail = new EventEmitter<PersonWithUserDataVm>();
  @Input() selectedInstance: Instance;
  @Input() licenseDetail: ClientSettings_License;
  @Input() mode: string;
  @Input() inputPersonWithUserDataVm: PersonWithUserDataVm = new PersonWithUserDataVm();
  @Input() readding: boolean = false;
  @ViewChild('updateInstances') updateInstances: TemplateRef<any>;
  userForm: UntypedFormGroup;
  personDetail: Person;
  instructorDetail: Instructor;
  employeeDetail: Employee;
  qtdUserDetail: QtdUserVM;
  instructorCategoryList: Instructor_Category[] = [];
  userOtherInstancesList: string[] = [];
  oldUserName: string;
  updateInstanceHeader: string;
  updateInstanceDescription: string;
  loader: boolean = false;
  providerChecked:boolean = false;
  providerEverChecked:boolean = false;
  instanceIdentityProviders:IdentityProviderVM[];
  get isQTDUser() {
    return this.userForm.get('isQTDUser')?.value ?? false;
  }
  get isEmployee() {
    return this.userForm.get('isEmployee')?.value ?? false;
  }
  get isInstructor() {
    return this.userForm.get('isInstructor')?.value ?? false;
  }

  newUserName : string
  oldUserNameInstanceClaims : string[] = []
  newUserNameInstanceClaims : string[] = []
  separateUsers : boolean = false;
  userNameAlreadyExists : boolean = false;
  employeeNumberAlreadyExists : boolean = false;
  isExternalProvider:boolean = false;
  isLoading:boolean = false;

  constructor(private formBuilder: UntypedFormBuilder,
    private alert: SweetAlertService,
    private userService: UserService,
    private instanceSetupService: InstanceSetupService,
    private clientService: ClientService,
    public dialog: MatDialog,
    private authService:AuthService,
    private instanceService:InstanceService) { }

  async ngOnInit(): Promise<void> {
    this.isLoading = true;
    this.initializeUserForm();
    await this.getCategoriesList();
    this.oldUserName = this.inputPersonWithUserDataVm?.username;
    await this.getAllIdentityProviders();
    await this.getUserIdentityProvider();
    this.userForm.get('username').valueChanges.subscribe((value)=>{
      this.userForm.get('identityProvider')?.setValue(null)
      this.providerChecked = false;
    })
    this.isLoading = false;
  }

  // async loadIdentityProviderAsync(){
  //   if(this.mode=='edit'){
  //     this.isProviderCheckDisable = true;
  //     this.providerChecked = true;
  //     var idp = await this.authService.getUserIdentityProviderByUsername(this.inputPersonWithUserDataVm?.username);
  //     this.setIdentityProviderValue(idp);
  //   }
  // }

  initializeUserForm() {
    this.userForm = this.formBuilder.group({
      username: [this.mode == 'edit' ? this.inputPersonWithUserDataVm?.username : null, Validators.required],
      firstName: [this.mode == 'edit' ? this.inputPersonWithUserDataVm?.firstName : null, Validators.required],
      lastName: [this.mode == 'edit' ? this.inputPersonWithUserDataVm?.lastName : null, Validators.required],
      isEmployee: [this.mode == 'edit' ? this.inputPersonWithUserDataVm?.isEmployee : false],
      employeeNumber: [this.mode == 'edit' ? this.inputPersonWithUserDataVm?.employeeNumber : null, Validators.required],
      isTQEvaluator: [this.mode == 'edit' ? this.inputPersonWithUserDataVm?.isTQEvaluator : false],
      isInstructor: [this.mode == 'edit' ? this.inputPersonWithUserDataVm?.isInstructor : false],
      instructorCategoryId: [this.mode == 'edit' ? this.inputPersonWithUserDataVm?.instructorCategoryId : null, Validators.required],
      instructorNumber: [this.mode == 'edit' ? this.inputPersonWithUserDataVm?.instructorNumber : null, Validators.required],
      isQTDUser: [this.mode == 'edit' ? this.inputPersonWithUserDataVm?.isQTDUser : false],
      identityProvider: [null, Validators.required]
    });
  }

  async getCategoriesList() {
    await this.instanceSetupService.getCategoriesByDatabaseAsync(this.selectedInstance?.name)
      .then((res: any) => {
        if (res != null) {
          this.instructorCategoryList = res;
        }
      })
      .catch((err: any) => {
        this.alert.errorToast('Categories not found');
      });
  }

  /////////////////////////////////////////////////////////// New Implementation Code Starts //////////////////////////////////////////////////////////////////////////////////////////////////////

  async persistUserAsync(){
    this.loader = true;
    this.userForm.disable({ emitEvent: false });
    this.newUserName = this.userForm.get('username')?.value
    let preValidations = await this.preValidateAsync();
    if(preValidations){
      await this.getClaimsForUserNames()
      await this.setupUserForCurrentInstanceAsync()
    }
    this.userForm.enable();
    this.loader = false;
  }

  async preValidateAsync() : Promise<Boolean>{
    // Prevalidate to make sure the operation is legal
    this.employeeNumberAlreadyExists = false;
    this.userNameAlreadyExists = false;
    var employeeNumber = this.userForm.get('employeeNumber')?.value ?? "";
    if(this.isEmployee && employeeNumber != ""){
      var employeeIdWithMatchingEmployeeNumber = (await this.instanceSetupService.getEmployeeByNumberAsync(employeeNumber , this.selectedInstance?.name))?.id;
      if ((this.mode == "edit" && employeeIdWithMatchingEmployeeNumber != null && employeeIdWithMatchingEmployeeNumber != this.inputPersonWithUserDataVm.employeeId) || (this.mode == "create" && employeeIdWithMatchingEmployeeNumber != null))
      {
        this.alert.errorToast("Failed to create User: Employee Number already in use");
        this.employeeNumberAlreadyExists = true;
        return false;
        //High light the employee number field in red outline
      }
    }

    if(this.mode == "edit" && this.oldUserName != this.newUserName){
      var personWithMatchingUserName = await this.instanceSetupService.getUserDetailByUserNameAsync(this.selectedInstance?.name, this.newUserName);
      if(personWithMatchingUserName != null){
        this.alert.errorToast("Failed to create User: Username already exists in Instance");
        this.userNameAlreadyExists = true;
        return false;
        //High light the user name field in red outline
      }
    }
    return true;
  }

  async getClaimsForUserNames(){
    // Get the lists of Instances where the oldUserName and newUserName have claims, excluding the current instance
    // (that is managed later in setupUserForCurrentInstance())
    if(this.mode == "edit" && this.oldUserName != this.newUserName && this.oldUserName.trim() !== ''){
      await this.userService.getUserIsAdminClaim(this.oldUserName).then((res) => {
        this.separateUsers = res ? true : this.separateUsers;
      }).catch((error) => {
        if (error?.status != 404) {
          this.loader = false;
          this.alert.errorToast("Failed to get User: " + error);
          this.userForm.enable();
          throw error;
        }
      });

      this.oldUserNameInstanceClaims = (await this.clientService.getUserClientInstances(this.oldUserName)).filter(x=> x.name != this.selectedInstance?.name).map(x=> x.name);

      this.separateUsers = this.oldUserNameInstanceClaims.length > 0 ? true : this.separateUsers;
    }

    await this.userService.getUserIsAdminClaim(this.newUserName).then((res) => {
      this.separateUsers = res ? true : this.separateUsers;
    }).catch((error) => {
      if (error?.status != 404) {
        this.loader = false;
        this.alert.errorToast("Failed to get User: " + error);
        this.userForm.enable();
        throw error;
      }
    });

    this.newUserNameInstanceClaims = (await this.clientService.getUserClientInstances(this.newUserName)).filter(x=> x.name != this.selectedInstance?.name).map(x=> x.name);

    this.separateUsers = this.newUserNameInstanceClaims.length > 0 ? true : this.separateUsers;

    // At this point, we have the completely accurate lists of old and new username claims for other instances
  }

  async setupUserForCurrentInstanceAsync(){
    if(!this.separateUsers && this.mode == 'edit' && this.oldUserName != this.newUserName && this.oldUserName.trim() !== ''){
      // Remove the newUserName AspNetUser if we aren't keeping as separate users and no instance claims exist, preventing issues when renaming the existing oldUserName AspNetUser if another already exists with newUserName
      await this.userService.removeUserAsync(this.newUserName).then(async (res) => {
      }).catch(async (error) => {
        if (error?.status != 404) {
          this.loader = false;
          this.alert.errorToast("Failed to remove User: " + error);
          this.userForm.enable();
          throw error;
        }
      });
    }

    switch (this.mode) {
      case "create":
        await this.createAspNetUserAsync(this.selectedInstance?.name)
        break;
      case "edit":
        if(this.separateUsers){
          await this.createAspNetUserAsync(this.selectedInstance?.name)
        }else{
          await this.updateAspNetUserAsync();
        }
        break;
      default:
        throw new Error("Invalid Mode: " + this.mode);
    }

    await this.updateUserIdentityProviderClaim(this.newUserName);

    // Reactivate necessary records if readding
    if(this.readding == true){
      await this.activatePersonAsync();
      await this.activateUserAsync();
    }

    // Add/Update Current Instance's Entities
    switch (this.mode) {
      case "create":
        await this.createPersonAsync()
        break;
      case "edit":
        await this.updateAndPatchDetailsAsync()
        break;
      default:
        throw new Error("Invalid Mode: " + this.mode);
    }

    // Remove oldUserName instance claim from current instance, this will also clean up the AspNetUser for the oldUserName if the currentInstance was it's only instance
    // ORDER MATTERS, we do this last so we don't delete the AspNetUser if currentInstance was it's last instance and we had wanted to keep the AspNetUser
    if(this.separateUsers && this.mode == "edit" && this.oldUserName != this.newUserName && this.oldUserName.trim() !== ''){
      await this.userService.removeUserByInstanceAsync(this.oldUserName, this.selectedInstance?.name);
    }
  }

  async createAspNetUserAsync(instanceName : string){
    // Make AspNetUser for newUserName if it doesn't exist
      // This also adds a claim to the current instance for the newUserName as well
      let user = new User();
      user.name = this.newUserName;
      user.empEnabled = this.userForm.get('isEmployee')?.value;
      user.password = "";
      user.instanceName = instanceName;
      await this.userService.createUser(user).then(async (res) => {
      }).catch( (error) => {
        if(error?.status != 409){
          console.log(error);
          this.alert.errorToast("Failed to create User: " + error);
          this.userForm.enable();
          this.loader = false;
          throw error;
        }
      }); 
  }

  async updateAspNetUserAsync() {
    let userUpdateOptions = new UserUpdateOptions();
    userUpdateOptions.oldName = this.oldUserName;
    userUpdateOptions.newName = this.newUserName;
    try{
      await this.userService.updateUser(userUpdateOptions);
      await this.createAspNetUserAsync(this.selectedInstance?.name);
    }catch(error){
      console.log(error)
      if (error?.status == 404) {
        await this.createAspNetUserAsync(this.selectedInstance?.name);
      }
      else {
        console.log(error)
        this.loader = false;
        this.alert.errorToast("Failed to update User: " + error);
        this.userForm.enable();
        throw error;
      }
    }
  }

  async updatePersonInstanceAsync(personDetail: any, instanceName: string) {
    let personUpdateOption = new PersonUpdateOption();
    personUpdateOption.firstName = this.userForm.get('firstName')?.value;
    personUpdateOption.middleName = personDetail?.middleName;
    personUpdateOption.lastName = this.userForm.get('lastName')?.value;
    personUpdateOption.username = this.userForm.get('username')?.value;
    personUpdateOption.image = personDetail?.image;
    await this.instanceSetupService.updatePersonAsync(personUpdateOption, instanceName, personDetail?.personId).then(async (res) => {
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to update Person Instance Detail : " + error);
      this.userForm.enable();
      throw error;
    });
  }

  async updateInstructorInstanceAsync(instructorDetail: any, instanceName: string) {
    let instructorCreateOptions = new Instructor_CreateOptions();
    instructorCreateOptions.ICategoryId = instructorDetail?.instructorCategoryId;
    instructorCreateOptions.Num = instructorDetail?.instructorNumber;
    instructorCreateOptions.Email = this.userForm.get('username')?.value;
    instructorCreateOptions.Name = this.userForm.get('firstName')?.value + " " + this.userForm.get('lastName')?.value;
    instructorCreateOptions.Description = instructorDetail?.instructorDescription;
    instructorCreateOptions.Isworkbookadmin = instructorDetail?.instructorIsworkbookadmin;
    instructorCreateOptions.EffectiveDate = instructorDetail?.instructorEffectiveDate;
    await this.instanceSetupService.updateInstructorAsync(instructorCreateOptions, instanceName, instructorDetail?.instructorId).then(async (res) => {
    }).catch(async (error) => {
      if (error?.status == 409) {
        let duplicateInstructor : Instructor = error?.error?.conflictValue;
        instructorDetail.instructorId = duplicateInstructor.id;
        instructorDetail.instructorCategoryId = duplicateInstructor.iCategoryId;
        instructorDetail.instructorNumber = duplicateInstructor.instructorNumber;
        instructorDetail.instructorDescription = duplicateInstructor.instructorDescription;
        instructorDetail.isworkbookadmin = duplicateInstructor.isWorkBookAdmin;
        instructorDetail.instructorEffectiveDate = duplicateInstructor.effectiveDate;
        await this.updateInstructorInstanceAsync(instructorDetail,instanceName);
      }
      else {
        this.loader = false;
        this.alert.errorToast("Failed to update Instructor Instance Detail : " + error);
        this.userForm.enable();
        throw error;
      }
    });
  }

  async tryUpdateInstructorNameByEmailInstanceAsync(instanceName: string){
    let instructorUpdateByEmailOptions = new Instructor_UpdateByEmailOptions();
    instructorUpdateByEmailOptions.Email = this.userForm.get('username')?.value;
    instructorUpdateByEmailOptions.Name = this.userForm.get('firstName')?.value + " " + this.userForm.get('lastName')?.value;
    await this.instanceSetupService.updateInstructorByEmailAsync(instructorUpdateByEmailOptions, instanceName).then(async (res) => {
    }).catch(async (error) => {
      if (error?.status != 404) {
        this.loader = false;
        this.alert.errorToast("Failed to update Instructor Instance Detail by New UserName: " + error);
        this.userForm.enable();
        throw error;
      }
    });
  }

  async getUserDetailAsync() {
    await this.instanceSetupService.getUserDetailAsync(this.inputPersonWithUserDataVm?.personId, this.selectedInstance?.name).then(async (res) => {
      this.inputPersonWithUserDataVm = res;
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to get User Detail: " + error);
      this.userForm.enable()
      throw error;
    });
  }

  async updateAndPatchDetailsAsync() {
    await this.getUserDetailAsync();
    await this.updatePersonAsync();
    await this.checkQTDUserStatusAndUpdate();
    await this.checkEmployeeStatusAndUpdate();
    await this.checkInstructorStatusAndUpdate();
    this.setDetailsForupdatedUser();
  }

  async checkQTDUserStatusAndUpdate() {
    let isQTDUserExist = this.inputPersonWithUserDataVm.qtdUserId ?? false;
    let isQTDUserActive = this.inputPersonWithUserDataVm.isQTDUserActive ?? false;

    if (this.isQTDUser) {
      if (isQTDUserExist) {
        await this.updateQTDUserAsync();
      }
      else {
        await this.createQTDUserAsync();
      }
    }
    else {
      if (isQTDUserExist) {
        if (isQTDUserActive) {
          await this.deactivateQTDUserAsync();
        }
      }
    }
    this.inputPersonWithUserDataVm.isQTDUser = this.isQTDUser;
  }

  async checkEmployeeStatusAndUpdate() {
    let isEmployeeExist = this.inputPersonWithUserDataVm?.employeeId ?? false;
    let isEmployeeActive = this.inputPersonWithUserDataVm?.isEmployeeActive ?? false;

    if (this.isEmployee) {
      if (isEmployeeExist) {
        await this.updateEmployeeAsync();
      }
      else {
        await this.createEmployeeAsync();
      }
    }
    else {
      if (isEmployeeExist) {
        if (isEmployeeActive) {
          await this.deactivateEmployeeAsync();
        }
      }
    }
    this.inputPersonWithUserDataVm.isEmployee = this.isEmployee;
  }

  async checkInstructorStatusAndUpdate() {
    let isInstructorExist = this.inputPersonWithUserDataVm?.instructorId ?? false;

    if (this.isInstructor) {
      if (isInstructorExist) {
        await this.updateInstructorAsync(false);
      }
      else {
        await this.createInstructorAsync();
      }
    }
    else {
      if (isInstructorExist) {
        await this.updateInstructorAsync(true); //Keeps Inactive Instructors in sync when renaming Person data

        if (this.inputPersonWithUserDataVm?.isInstructorActive ?? false) {
          await this.deactivateInstructorAsync();
        }
      }else{
        await this.tryUpdateInstructorNameByEmailAsync(); // Do this in case there is an existing Instructor with the New UserName as the Email that we wouldn't have the id for yet

        if((this.inputPersonWithUserDataVm.instructorId ?? false) && (this.inputPersonWithUserDataVm?.isInstructorActive ?? false)){
          await this.deactivateInstructorAsync();
        }
      }
    }
    this.inputPersonWithUserDataVm.isInstructor = this.isInstructor;
  }

  async createPersonAsync() {
    this.loader = true;
    let personCreateOption = new PersonCreateOption();
    personCreateOption.firstName = this.userForm.get('firstName')?.value;
    personCreateOption.lastName = this.userForm.get('lastName')?.value;
    personCreateOption.username = this.userForm.get('username')?.value;
    await this.instanceSetupService.createPersonAsync(personCreateOption, this.selectedInstance?.name).then((res) => {
      this.personDetail = res;
      this.inputPersonWithUserDataVm.personId = this.personDetail?.id;
    }).catch(async (error) => {
      if (error?.status == 409) {
        this.personDetail = error?.error?.conflictValue;
        this.inputPersonWithUserDataVm.personId = this.personDetail?.id;
        await this.getUserDetailAsync();
        await this.updatePersonAsync();
      }
      else {
        this.loader = false;
        this.alert.errorToast("Failed to create Person: " + error);
        this.userForm.enable()
        throw error;
      }
    });
    if (this.personDetail) {
      this.inputPersonWithUserDataVm.username = this.personDetail?.username;
      this.inputPersonWithUserDataVm.firstName = this.personDetail?.firstName;
      this.inputPersonWithUserDataVm.lastName = this.personDetail?.lastName;
      this.inputPersonWithUserDataVm.active = this.personDetail?.active;
      await this.createClientUserAsync();
      await this.checkQTDUserStatusAndUpdate();
      await this.checkEmployeeStatusAndUpdate();
      await this.checkInstructorStatusAndUpdate();
      this.setDetailsForUser();
    }
  }

  async updatePersonAsync() {
    this.loader = true;
    let personUpdateOption = new PersonUpdateOption();
    personUpdateOption.firstName = this.userForm.get('firstName')?.value;
    personUpdateOption.middleName = this.inputPersonWithUserDataVm?.middleName;
    personUpdateOption.lastName = this.userForm.get('lastName')?.value;
    personUpdateOption.username = this.userForm.get('username')?.value;
    personUpdateOption.image = this.inputPersonWithUserDataVm?.image;
    await this.instanceSetupService.updatePersonAsync(personUpdateOption, this.selectedInstance?.name, this.inputPersonWithUserDataVm?.personId).then(async (res) => {
      this.inputPersonWithUserDataVm.username = res?.username;
      this.inputPersonWithUserDataVm.firstName = res?.firstName;
      this.inputPersonWithUserDataVm.lastName = res?.lastName;
      this.personDetail = res;
    }).catch((error) => {
      console.log(error)
      var message = error.headers.get("Application-Error");
      this.loader = false;
      this.alert.errorToast("Failed to update Person: " + message);
      this.userForm.enable()
      throw error;
    });
  }

  async activatePersonAsync() {
    await this.instanceSetupService.activatePersonAsync(this.selectedInstance?.name, this.inputPersonWithUserDataVm?.personId).then((res) => {
      this.inputPersonWithUserDataVm.active = true;
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to activate Person : " + error);
      this.userForm.enable()
      throw error;
    });
  }

  async createClientUserAsync() {
    this.loader = true;
    var clientUser = null;
    let clientUserCreateOptions = new ClientUserCreateOptions();
    clientUserCreateOptions.personId = this.personDetail?.id;
    await this.instanceSetupService.createClientUserAsync(clientUserCreateOptions, this.selectedInstance?.name).then((res) => {
      clientUser = res;
    }).catch((error) => {
      if (error?.status == 409) {
        clientUser = error?.error?.conflictValue;
      }
      else {
        this.loader = false;
        this.alert.errorToast("Failed to create Client User: " + error);
        this.userForm.enable();
        throw error;
      }
    });
  }

  async activateUserAsync() {
    await this.instanceSetupService.activateClientUserAsync(this.selectedInstance?.name, this.inputPersonWithUserDataVm?.personId).then((res) => {
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to activate Client User : " + error);
      this.userForm.enable()
      throw error;
    });
  }

  async createQTDUserAsync() {
    this.loader = true;
    let qtdUserVM = new QtdUserVM();
    qtdUserVM.person = this.personDetail;
    await this.instanceSetupService.createQTDUserAsync(qtdUserVM, this.selectedInstance?.name).then(async (res) => {
      this.qtdUserDetail = res;
    }).catch((error) => {
      if (error?.status == 409) {
        this.qtdUserDetail = error?.error?.conflictValue;
      }
      else {
        this.loader = false;
        this.alert.errorToast("Failed to create QTDUser: " + error);
        this.userForm.enable();
        throw error;
      }
    });
    if (this.qtdUserDetail) {
      this.inputPersonWithUserDataVm.qtdUserId = this.qtdUserDetail?.id;
      this.inputPersonWithUserDataVm.isQTDUser = true;
      this.loader = false;
    }
  }

  async updateQTDUserAsync() {
    let qtdUserVM = new QtdUserVM();
    qtdUserVM.id = this.inputPersonWithUserDataVm?.qtdUserId;
    qtdUserVM.person = this.personDetail;
    let isQTDUserActive = this.inputPersonWithUserDataVm?.isQTDUserActive;
    await this.instanceSetupService.updateQTDUserAsync(qtdUserVM, this.selectedInstance?.name, qtdUserVM?.id).then(async (res) => {
      this.qtdUserDetail = res;
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to update QTDUser Detail: " + error);
      this.userForm.enable();
      throw error;
    });
    if (!isQTDUserActive) {
      await this.activateQTDUserAsync();
    }
  }

  async activateQTDUserAsync() {
    await this.instanceSetupService.activateQTDUserAsync(this.selectedInstance?.name, this.inputPersonWithUserDataVm?.qtdUserId).then((res) => {
      this.qtdUserDetail = res;
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to activate QTDUser : " + error);
      this.userForm.enable()
      throw error;
    });
  }

  async deactivateQTDUserAsync() {
    await this.instanceSetupService.deactivateQTDUserAsync(this.selectedInstance?.name, this.inputPersonWithUserDataVm?.qtdUserId).then((res) => {
      this.inputPersonWithUserDataVm.isQTDUser = false;
      this.qtdUserDetail = res;
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to deactivate QTDUser : " + error);
      this.userForm.enable();
      throw error;
    });
  }

  async createEmployeeAsync() {
    this.loader = true;
    let employeeCreateOptions = new EmployeeCreateOptions();
    employeeCreateOptions.employeeNumber = this.userForm.get('employeeNumber')?.value ?? null;
    employeeCreateOptions.TQEqulator = this.userForm.get('isTQEvaluator')?.value ?? false;
    employeeCreateOptions.personId = this.personDetail?.id;
    await this.instanceSetupService.createEmployeeAsync(employeeCreateOptions, this.selectedInstance?.name).then(async (res) => {
      this.employeeDetail = res;
    }).catch((error) => {
      if (error?.status == 409) {
        this.employeeDetail = error?.error?.conflictValue;
      }
      else {
        this.loader = false;
        this.alert.errorToast("Failed to create Employee: " + error);
        this.userForm.enable()
        throw error;
      }
    });

    if (this.employeeDetail) {
      this.inputPersonWithUserDataVm.employeeId = this.employeeDetail?.id;
      this.inputPersonWithUserDataVm.employeeNumber = this.employeeDetail?.employeeNumber;
      this.inputPersonWithUserDataVm.isTQEvaluator = this.employeeDetail?.tqEqulator;
      this.inputPersonWithUserDataVm.isEmployee = true;
    }
  }

  async updateEmployeeAsync() {
    let employeeUpdateOptions = new EmployeeUpdateOptions();
    employeeUpdateOptions.employeeNumber = this.userForm.get('employeeNumber')?.value;
    employeeUpdateOptions.Address = this.inputPersonWithUserDataVm?.employeeAddress;
    employeeUpdateOptions.City = this.inputPersonWithUserDataVm?.employeeCity;
    employeeUpdateOptions.State = this.inputPersonWithUserDataVm?.employeeState;
    employeeUpdateOptions.ZipCode = this.inputPersonWithUserDataVm?.employeeZipCode;
    employeeUpdateOptions.PhoneNumber = this.inputPersonWithUserDataVm?.employeePhoneNumber;
    employeeUpdateOptions.WorkLocation = this.inputPersonWithUserDataVm?.employeeWorkLocation;
    employeeUpdateOptions.Notes = this.inputPersonWithUserDataVm?.employeeNotes;
    employeeUpdateOptions.TQEqulator = this.userForm.get('isTQEvaluator')?.value;
    let isEmployeeActive = this.inputPersonWithUserDataVm?.isEmployeeActive;
    await this.instanceSetupService.updateEmployeeAsync(employeeUpdateOptions, this.selectedInstance?.name, this.inputPersonWithUserDataVm?.employeeId).then(async (res) => {
      this.inputPersonWithUserDataVm.employeeNumber = res?.employeeNumber;
      this.inputPersonWithUserDataVm.isTQEvaluator = res?.tqEqulator;
      this.employeeDetail = res;
      if (!isEmployeeActive) {
        await this.activateEmployeeAsync();
      }
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to update Employee Detail: " + error);
      this.userForm.enable()
      throw error;
    });
  }

  async activateEmployeeAsync() {
    await this.instanceSetupService.activateEmployeeAsync(this.selectedInstance?.name, this.inputPersonWithUserDataVm?.employeeId).then((res) => {
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to activate Employee : " + error);
      this.userForm.enable();
      throw error;
    });
  }

  async deactivateEmployeeAsync() {
    await this.instanceSetupService.deactivateEmployeeAsync(this.selectedInstance?.name, this.inputPersonWithUserDataVm?.employeeId).then((res) => {
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to deactivate Employee : " + error);
      this.userForm.enable();
      throw error;
    });
  }

  async createInstructorAsync() {
    this.loader = true;
    let instructorCreateOptions = new Instructor_CreateOptions();
    instructorCreateOptions.Email = this.userForm.get('username')?.value;
    instructorCreateOptions.Name = this.userForm.get('firstName')?.value + " " + this.userForm.get('lastName')?.value;
    instructorCreateOptions.ICategoryId = this.userForm.get('instructorCategoryId')?.value ?? null;
    instructorCreateOptions.Num = this.userForm.get('instructorNumber')?.value ?? null;
    await this.instanceSetupService.createInstructorAsync(instructorCreateOptions, this.selectedInstance?.name).then(async (res) => {
      this.instructorDetail = res;
    }).catch(async (error) => {
      if (error?.status == 409) {
        this.instructorDetail = error?.error?.conflictValue;
        this.patchInputPersonWithUserDataVmInstructor();
        await this.updateInstructorAsync(false);
        if (!this.inputPersonWithUserDataVm.isInstructorActive) {
          await this.activateInstructorAsync();
        }
      }
      else {
        this.loader = false;
        this.alert.errorToast("Failed to create Instructor : " + error);
        this.userForm.enable();
        throw error;
      }
    });
    if (this.instructorDetail) {
      this.patchInputPersonWithUserDataVmInstructor();
      this.loader = false;
    }
  }

  patchInputPersonWithUserDataVmInstructor(){
    this.inputPersonWithUserDataVm.instructorId = this.instructorDetail?.id;
    this.inputPersonWithUserDataVm.instructorNumber = this.instructorDetail?.instructorNumber;
    this.inputPersonWithUserDataVm.instructorCategoryId = this.instructorDetail?.iCategoryId;
    this.inputPersonWithUserDataVm.instructorCategoryTitle = this.instructorCategoryList.find(x => x.id == (this.instructorDetail?.iCategoryId ?? 0))?.iCategoryTitle;
    this.inputPersonWithUserDataVm.instructorDescription = this.instructorDetail.instructorDescription;
    this.inputPersonWithUserDataVm.instructorEffectiveDate = this.instructorDetail.effectiveDate;
    this.inputPersonWithUserDataVm.instructorIsworkbookadmin = this.instructorDetail.isWorkBookAdmin;
    this.inputPersonWithUserDataVm.isInstructorActive = this.instructorDetail.active;
  }

  async updateInstructorAsync(onlyPersonInfo : boolean) {
    let instructorCreateOptions = new Instructor_CreateOptions();
    instructorCreateOptions.Email = this.userForm.get('username')?.value;
    instructorCreateOptions.Name = this.userForm.get('firstName')?.value + " " + this.userForm.get('lastName')?.value;
    instructorCreateOptions.ICategoryId = onlyPersonInfo ? this.inputPersonWithUserDataVm?.instructorCategoryId : this.userForm.get('instructorCategoryId')?.value;
    instructorCreateOptions.Num = onlyPersonInfo ? this.inputPersonWithUserDataVm?.instructorNumber : this.userForm.get('instructorNumber')?.value;
    instructorCreateOptions.Description = this.inputPersonWithUserDataVm?.instructorDescription;
    instructorCreateOptions.Isworkbookadmin = this.inputPersonWithUserDataVm?.instructorIsworkbookadmin;
    instructorCreateOptions.EffectiveDate = this.inputPersonWithUserDataVm?.instructorEffectiveDate;
    await this.instanceSetupService.updateInstructorAsync(instructorCreateOptions, this.selectedInstance?.name, this.inputPersonWithUserDataVm?.instructorId).then(async (res) => {
      this.instructorDetail = res;
      this.patchInputPersonWithUserDataVmInstructor();
      if (!this.inputPersonWithUserDataVm.isInstructorActive && !onlyPersonInfo) {
        await this.activateInstructorAsync();
      }
    }).catch(async (error) => {
      if (error?.status == 409) {
        this.instructorDetail = error?.error?.conflictValue;
        this.patchInputPersonWithUserDataVmInstructor();
        await this.updateInstructorAsync(onlyPersonInfo);
      }
      else{
        this.loader = false;
        this.alert.errorToast("Failed to update Instructor Detail: " + error);
        this.userForm.enable();
        throw error;
      }
    });
  }

  async tryUpdateInstructorNameByEmailAsync(){
    let instructorUpdateByEmailOptions = new Instructor_UpdateByEmailOptions();
    instructorUpdateByEmailOptions.Email = this.userForm.get('username')?.value;
    instructorUpdateByEmailOptions.Name = this.userForm.get('firstName')?.value + " " + this.userForm.get('lastName')?.value;
    await this.instanceSetupService.updateInstructorByEmailAsync(instructorUpdateByEmailOptions, this.selectedInstance?.name).then(async (res) => {
      this.instructorDetail = res;
      this.patchInputPersonWithUserDataVmInstructor();
    }).catch(async (error) => {
      if (error?.status != 404) {
        this.loader = false;
        this.alert.errorToast("Failed to update Instructor Instance Detail by New UserName: " + error);
        this.userForm.enable();
        throw error;
      }
    });
  }

  async activateInstructorAsync() {
    await this.instanceSetupService.activateInstructorAsync(this.selectedInstance?.name, this.inputPersonWithUserDataVm?.instructorId).then((res) => {
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to activate Instructor : " + error);
      this.userForm.enable();
      throw error;
    });
  }

  async deactivateInstructorAsync() {
    await this.instanceSetupService.deactivateInstructorAsync(this.selectedInstance?.name, this.inputPersonWithUserDataVm?.instructorId).then((res) => {
    }).catch((error) => {
      this.loader = false;
      this.alert.errorToast("Failed to deactivate Instructor : " + error);
      this.userForm.enable();
      throw error;
    });
  }

  isDisableSaveButton() {
    const form = this.userForm;
    if (!form.get('username').valid || !form.get('firstName').valid || !form.get('lastName').valid) {
      return true;
    }
    if (form.get('isInstructor').value === true) {
      if (!form.get('instructorCategoryId').valid || !form.get('instructorNumber').valid) {
        return true;
      }
    }
    if(!form.get('identityProvider').valid){
      return true;
    }
    return false;
  }

  setDetailsForUser() {
    this.alert.successToast("User Created successfully");
    this.userForm.enable();
    this.closed.emit();
    this.newUserDetail.emit(this.inputPersonWithUserDataVm);
  }

  setDetailsForupdatedUser() {
    this.alert.successToast("User Updated successfully");
    this.userForm.enable();
    this.closed.emit();
    this.updateUserDetail.emit(this.inputPersonWithUserDataVm);
  }

  async getUserIdentityProvider(){
    var username = this.userForm.get('username').value;
    if(username != null){
      var identityProvider =  await this.authService.getUserIdentityProviderByUsername(username);
      this.setIdentityProviderValue(identityProvider);
      this.providerChecked = true;
      this.providerEverChecked = true;
    }
  }

  setIdentityProviderValue(identityProvider:any){
    this.isExternalProvider = false;
    if(identityProvider==null){
      this.userForm.get('identityProvider')?.setValue(this.selectedInstance?.instanceSetting?.defaultIdentityProviderId);
    }else if(!this.instanceIdentityProviders.map(item=>item.id).includes(identityProvider.id)){
      this.userForm.get('identityProvider')?.setValue("external")
      this.isExternalProvider = true;
    }else{
      this.userForm.get('identityProvider')?.setValue(identityProvider?.id);
    }
  }

  async getAllIdentityProviders(){
    this.instanceIdentityProviders = await this.instanceService.getIdentityProvidersByInstanceName(this.selectedInstance.name);
  }

  async updateUserIdentityProviderClaim(name:string){
    if(!this.isExternalProvider){
      var identityProviderUpdateValue = new IdentityProviderUpdateModel();
          var id = this.userForm.get('identityProvider')?.value;
          var idp = this.instanceIdentityProviders.find(x=>x.id==id);
          identityProviderUpdateValue.identityProviderValue = idp.name;
          await this.authService.updateUserIdentityProviderClaimByUsername(name,identityProviderUpdateValue);
    }
  }
}
