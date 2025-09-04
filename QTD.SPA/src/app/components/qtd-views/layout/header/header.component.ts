import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Params, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { AuthService } from 'src/app/_Services/Auth/auth.service';
import { ClientUsersService } from 'src/app/_Services/QTD/client-users.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { CustomClaimTypes } from 'src/app/_Shared/Utils/CustomClaims';
import { UserService } from 'src/app/_Services/Auth/user.service';
import { ModifyTokenModel } from 'src/app/_DtoModels/Auth/ModifyTokenModel';
import { take } from 'rxjs/operators';
import { SubSink } from 'subsink';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';
import { AuthenticationSettingService } from 'src/app/_Services/Auth/authentication-setting.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  username: string = jwtAuthHelper.LoggedInUser;
  toggleState: Observable<string>;
  selectedInstance!: string;
  isAdmin: string = 'false';
  isEmpSideBar: string = 'false';
  defaultText: 'QTD Portal' | 'My Employee Portal' | '' = '';
  showUpdateButton: boolean = false;
  modifyTokenModal: ModifyTokenModel;
  showLeaveInstance: boolean;
  susbcription = new SubSink();
  isBetaInstance: boolean;
  authSettingString:string = '';
  isEmpStateReady: boolean = false;
  constructor(
    private translate: TranslateService,
    private authService: AuthService,
    private DataBroadcastService: DataBroadcastService,
    private router: Router,
    private store: Store<any>,
    private serv: ClientUsersService,
    private empService: EmployeesService,
    private licenseHelper:LicenseHelperService,
    private authsettings: AuthenticationSettingService
  ) {
    this.toggleState = store.select('toggle');
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  @Input() title: string = '';
  @Input() canGoBack: boolean = false;
  @Input() prevUrl: string = '';
  @Input() queryParams!: Params;
  @Input() toggleMenuIcon: boolean = true;
  @Input() breadcrumbs: string = '';
  @Input() crumbOnly: string;
  @Input() showSignOut = true;
  @Output() childToParent = new EventEmitter<boolean>();

  ngOnInit(): void {
  this.loadAsync();
    this.susbcription.sink = this.DataBroadcastService.isUserLoggedIn.subscribe((res) => {
      var data = jwtAuthHelper.unPackJWTToken;
      if (res === true) {
        this.username = jwtAuthHelper.LoggedInUser;

        if (jwtAuthHelper.unPackJWTToken['qtd/claims//hasMultipleInstances'] === "True") {
          this.showLeaveInstance = true;
        } else {
          this.showLeaveInstance = false;
        }
        this.selectedInstance = jwtAuthHelper.SelectedInstance;
        if (this.selectedInstance) {
          this.readyImage();
        }

        this.isBetaInstance = jwtAuthHelper.IsBetaInstance;

        this.isEmpSideBar = sessionStorage.getItem('isEmpSideBar') === null || sessionStorage.getItem('isEmpSideBar') === undefined ? "false" : sessionStorage.getItem('isEmpSideBar');
        this.isAdmin = sessionStorage.getItem('isAdminSliderBar') === null || sessionStorage.getItem('isAdminSliderBar') === undefined ? "false" : sessionStorage.getItem('isAdminSliderBar');;

        if (data[CustomClaimTypes.IsAdmin] === "true" && this.selectedInstance) {
          if(this.isEmpSideBar === "false" && this.isAdmin === "false"){
            this.showUpdateButton = true;
            this.defaultText = "My Employee Portal";
            sessionStorage.setItem('isEmpSideBar', 'false');
            sessionStorage.setItem('isAdminSliderBar', 'true');
            this.router.navigate(['/home/dashboard'])
          }
          else if (this.isEmpSideBar === "true") {
            this.showUpdateButton = true;
            this.defaultText = "QTD Portal";
            // localStorage.setItem('isEmpSideBar', 'true');
            // localStorage.setItem('isAdminSliderBar', 'false');
            // this.router.navigate(['/emp/dashboard'])
          }
          else if (this.isAdmin === "true") {
            this.showUpdateButton = true;
            this.defaultText = "My Employee Portal";
            // localStorage.setItem('isEmpSideBar', 'false');
            // localStorage.setItem('isAdminSliderBar', 'true');
            // this.router.navigate(['/home/dashboard'])
          }
        }
        else if (data[CustomClaimTypes.IsQtdUser] === "True" && this.selectedInstance) {
          if (this.isAdmin === "false" && this.isEmpSideBar === "false" && data[CustomClaimTypes.IsEmployee] === "False") {
            this.showUpdateButton = false;
            sessionStorage.setItem('isEmpSideBar', 'false');
            sessionStorage.setItem('isAdminSliderBar', 'true');
            this.router.navigate(['/home/dashboard']);
          }
          else if (this.isAdmin === "false" && this.isEmpSideBar === "false" && data[CustomClaimTypes.IsEmployee] === "True") {
            this.showUpdateButton = true;
            this.defaultText = "My Employee Portal";
            sessionStorage.setItem('isEmpSideBar', 'false');
            sessionStorage.setItem('isAdminSliderBar', 'true');
            this.router.navigate(['/home/dashboard']);
          }
          else if (this.isAdmin === "true" && data[CustomClaimTypes.IsEmployee] === "True") {
            this.showUpdateButton = true;
            this.defaultText = "My Employee Portal";
            // localStorage.setItem('isEmpSideBar', 'false');
            // localStorage.setItem('isAdminSliderBar', 'true');
            // this.router.navigate(['/home/dashboard']);
          }
          else if (this.isAdmin === "false" && this.isEmpSideBar === "true" && data[CustomClaimTypes.IsEmployee] === "True") {
            this.showUpdateButton = true;
            this.defaultText = "QTD Portal";
          }
        }
        else if (data[CustomClaimTypes.IsEmployee] === "True" && this.selectedInstance) {
          if (this.isEmpSideBar === "false" && this.isAdmin === "false" && data[CustomClaimTypes.IsQtdUser] === "False") {
            this.isEmpStateReady = true;
            this.showUpdateButton = false;
            sessionStorage.setItem('isEmpSideBar', 'true');
            sessionStorage.setItem('isAdminSliderBar', 'false');
            this.router.navigate(['/emp/dashboard']);
          }
          else if (this.isEmpSideBar === "false" && this.isAdmin === "false" && data[CustomClaimTypes.IsQtdUser] === "True") {
            this.showUpdateButton = true;
            this.defaultText = "QTD Portal";
            sessionStorage.setItem('isEmpSideBar', 'false');
            sessionStorage.setItem('isAdminSliderBar', 'true');
            this.router.navigate(['/home/dashboard']);
          }
          else if (this.isEmpSideBar === "true" && data[CustomClaimTypes.IsQtdUser] === "True") {
            this.showUpdateButton = true;
            this.defaultText = "QTD Portal";
            // localStorage.setItem('isEmpSideBar', 'false');
            // localStorage.setItem('isAdminSliderBar', 'true');
            // this.router.navigate(['/home/dashboard']);
          }
          else if (this.isEmpSideBar === "false" && this.isAdmin === "true" && data[CustomClaimTypes.IsQtdUser] === "True") {
            this.showUpdateButton = true;
            this.defaultText = "My Employee Portal";
          }
        }

        this.DataBroadcastService.refreshSideNav.next(null);

        //   if(this.isEmpSideBar === "false" && jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsAdmin] === "true" && this.selectedInstance){
        //     this.showUpdateButton = true;
        //     this.defaultText = "My Employee Portal"
        //   }
        //   else if(this.isEmpSideBar === "true" && jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsAdmin] === "true" && this.selectedInstance){
        //     this.showUpdateButton = true;
        //     this.defaultText = "QTD Portal"
        //   }
        //   else if (this.isEmpSideBar === "true" && (jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsQtdUser] === "True" || jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsAdmin] === "true")) {
        //     this.showUpdateButton = true;
        //     this.defaultText = 'QTD Portal';
        //   }
        //   else if (this.isEmpSideBar === "true" && jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsQtdUser] === "False") {
        //     this.showUpdateButton = false;
        //   }
        //   else if (this.isEmpSideBar === "false" && jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsEmployee] === "False") {
        //     this.showUpdateButton = false;
        //   }
        //   else if (this.isEmpSideBar === "false" && jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsEmployee] === "True" && jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsQtdUser] === "False" && jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsAdmin] === "false") {
        //     this.showUpdateButton = true;
        //     this.isEmpSideBar = "true";
        //     if (this.selectedInstance) {
        //       this.defaultText = 'My Employee Portal';
        //       localStorage.setItem('isEmpSideBar', 'false');
        //       localStorage.setItem('isAdminSliderBar', 'true');
        //       this.router.navigate(['/home/dashboard'])
        //     }
        //     this.defaultText = 'My Employee Portal';
        //   }
        //   else if (this.isEmpSideBar === "false" && jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsQtdUser] === "True") {
        //     this.showUpdateButton = true;
        //     this.isEmpSideBar = "false";
        //     if (this.selectedInstance) {
        //       this.defaultText = 'QTD Portal';
        //       localStorage.setItem('isEmpSideBar', 'true');
        //       localStorage.setItem('isAdminSliderBar', 'true');
        //       this.router.navigate(['/emp/dashboard'])
        //     }
        //     //this.defaultText = 'My Employee Portal';
        //   }
        //   else if (this.isEmpSideBar === "false" && jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsEmployee] === "True") {
        //     this.showUpdateButton = true;
        //     this.isEmpSideBar = "true";
        //     if (this.selectedInstance) {
        //       this.defaultText = 'QTD Portal';
        //       localStorage.setItem('isEmpSideBar', 'true');
        //       this.router.navigate(['/emp/dashboard'])
        //     }
        //     //this.defaultText = 'My Employee Portal';
        //   }
      }
    });
  }

  ngOnDestroy(): void {
    this.susbcription.unsubscribe();
  }
 async loadAsync(){
  var authenticationsetting = await this.authsettings.getAll();
  this.authSettingString = `${authenticationsetting.versionMajor}.${authenticationsetting.versionMinor}.${authenticationsetting.versionPatch}`
 }
  signOut() {
    sessionStorage.removeItem('isAdminSliderBar');
    sessionStorage.removeItem('isEmpSideBar');
    localStorage.removeItem("userImg");
    this.licenseHelper.removeLicenseData();
    this.authService.logout();
  }

  async leaveInstanceAsync() {
    this.modifyTokenModal = new ModifyTokenModel(this.username);
    await this.authService.modifyToken(this.modifyTokenModal);
    localStorage.removeItem("userImg");
    sessionStorage.removeItem('isAdminSliderBar');
    sessionStorage.removeItem('isEmpSideBar');
    this.router.navigate(['home/index'])
  }

  async goBack() {

    await this.router.navigate([this.prevUrl], {
      queryParams: this.queryParams,
    });
  }

  async redirectToEmp() {
    let isEmpSideBar = sessionStorage.getItem('isEmpSideBar');
    if (isEmpSideBar === 'true') {
      this.defaultText = 'My Employee Portal';
      sessionStorage.setItem('isEmpSideBar', 'false');
      sessionStorage.setItem('isAdminSliderBar', 'true');
      this.router.navigate(['/home/dashboard'])
    }
    else {
      this.defaultText = 'QTD Portal';
      sessionStorage.setItem('isEmpSideBar', 'true');
      sessionStorage.setItem('isAdminSliderBar', 'false');
      this.router.navigate(['/emp/dashboard'])
    }
    this.DataBroadcastService.refreshSideNav.next(null);
  }

  toggleMainMenu() {
    //this.DataBroadcastService.ToggleMainMenu.next('');
    this.store.select('freezeMenu').pipe(take(1)).subscribe((data: boolean) => {
      if (!data) {
        this.store.dispatch(sideBarToggle());
      }
    })
  }

  empImage!: string;
  async readyImage() {
    var hasImage = localStorage.getItem("userImg");
    if (hasImage !== undefined && hasImage !== null) {
      this.empImage = hasImage;
    }
    else {
      this.empImage = await this.empService.getImageWithUserName(this.username);
      if (this.empImage !== null && this.empImage !== undefined) {
        localStorage.setItem("userImg", this.empImage);
      }
    }
  }
}
