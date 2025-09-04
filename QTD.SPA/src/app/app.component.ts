import {
  AfterContentChecked,
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import {
  ActivatedRoute,
  NavigationEnd,
  NavigationError,
  NavigationStart,
  Router,
} from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DataBroadcastService } from './_Shared/services/DataBroadcast.service';
import { jwtAuthHelper } from './_Shared/Utils/jwtauth.helper';
import { SweetAlertService } from './_Shared/services/sweetalert.service';
import { EmployeeCertification } from './_DtoModels/EmployeeCertification/EmployeeCertification';
import { EmployeePosition } from './_DtoModels/EmployeePosition/EmployeePosition';
import { CustomClaimTypes } from './_Shared/Utils/CustomClaims';
import { MatSidenav } from '@angular/material/sidenav';
import { FlyInPanelService } from './_Shared/services/flyInPanel.service';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { sideBarClose } from './_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink/dist/subsink';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['../assets/css/styles.css'],
})
export class AppComponent
  implements OnInit, AfterViewInit, AfterContentChecked {
  title = 'QTD2SPA';
  defaultLang = 'en';
  lang: string = 'en';
  isLoggedIn: boolean = false;
  showEmpFilters: boolean = false;
  addPositionModalVisible: boolean = false;
  empcertificationAddEditPanelVisible: boolean = false;
  showSideBar: boolean = true;
  isEmpSideBar: string = 'false';
  isAdminSideBar: string = 'false';
  isInitialSideBar:string = 'true';
  jtaSideBarOpen: boolean = false;
  posName: string = '';
  acronymName: string = '';
  empCertEditData!: EmployeeCertification;
  empCertMode: string = 'add';
  editPosId!: any;
  editCertBodyId!: any;
  empPosEditData!: EmployeePosition;
  empPosMode: string = 'add';
  subscription = new SubSink();
  username!: string;
  selectedInstance!: string;
  isTfaRequired:boolean=false;
  ngSelectConfig = {
    displayKey: 'lang', //if objects array passed which key to be displayed defaults to description
    search: true, //true/false for the search functionlity defaults to false,
    height: 'auto', //height of the list so that if there are more no of items it can show a scroll defaults to auto. With auto height scroll will never appear
    placeholder: 'Select Language', // text to be displayed when no item is selected defaults to Select,
    customComparator: () => {
    }, // a custom function using which user wants to sort the items. default is undefined and Array.sort() will be used in that case,
    limitTo: 0, // number thats limits the no of options displayed in the UI (if zero, options will not be limited)
    moreText: 'more', // text to be displayed whenmore than one items are selected like Option 1 + 5 more
    noResultsFound: 'No results found!', // text to be displayed when no items are found while searching
    searchPlaceholder: 'Select Language', // label thats displayed in search input,
    searchOnKey: 'lang', // key on which search should be performed this will be selective search. if undefined this will be extensive search on all keys
  };

  flyInPanelName: string = '';
  toggleMenu: Observable<string>;
  //position id for add training program
  positionIdParent: any;

  @ViewChild('right')
  rightflymenu!: MatSidenav;

  @ViewChild('left')
  leftMainMenu!: MatSidenav;
  disableClose: Observable<any>;
  hasbackdrop: Observable<any>;
  menuMode: Observable<any>;
  freezeMenu: Observable<any>;

  constructor(
    public translate: TranslateService,
    private router: Router,
    private alert: SweetAlertService,
    private DataBroadcastService: DataBroadcastService,
    public flyInPanelSrvc: FlyInPanelService,
    public store: Store<any>,
    private cd: ChangeDetectorRef,
  ) {
    translate.addLangs([this.defaultLang, 'es']);
    translate.setDefaultLang(this.defaultLang);

    if (
      localStorage.getItem('lang') == null ||
      localStorage.getItem('lang') == ''
    )
      localStorage.setItem('lang', this.defaultLang);

    const browserLang = localStorage.getItem('lang') ?? this.defaultLang;
    this.lang = browserLang;
    translate.use(browserLang);

    this.DataBroadcastService.UserLoggedIn();
  }

  ngAfterContentChecked(): void {
    this.disableClose = this.store.select('menubgdisableclose');
    this.hasbackdrop = this.store.select('menubackdrop');
    this.menuMode = this.store.select('menuMode');
    this.freezeMenu = this.store.select('freezeMenu');
    this.cd.detectChanges();
  }

  ngAfterViewInit(): void {
    this.flyInPanelSrvc.panel = this.rightflymenu;
  }
  
  ngOnInit(): void {
    this.selectedInstance = jwtAuthHelper.SelectedInstance
    this.subscription.sink = this.DataBroadcastService.refreshSideNav.subscribe((_) => {
      this.isInitialSideBar = (sessionStorage.getItem('isAdminSliderBar') === null || sessionStorage.getItem('isAdminSliderBar') === undefined ) && (sessionStorage.getItem('isEmpSideBar') === null || sessionStorage.getItem('isEmpSideBar') === undefined) ? "true" :"false";
      this.isAdminSideBar = sessionStorage.getItem('isAdminSliderBar') === null || sessionStorage.getItem('isAdminSliderBar') === undefined ? "false" : sessionStorage.getItem('isAdminSliderBar');
      this.isEmpSideBar = sessionStorage.getItem('isEmpSideBar') === null || sessionStorage.getItem('isEmpSideBar') === undefined ? "false" : sessionStorage.getItem('isEmpSideBar');
    })
    this.DataBroadcastService.isUserLoggedIn.subscribe((res) => {
      this.isLoggedIn = res;
      if (jwtAuthHelper.unPackJWTToken !== null) {
        this.isTfaRequired = jwtAuthHelper.HasTfaRequired;
        // if (localStorage.getItem('isEmpSideBar') === undefined || localStorage.getItem('isEmpSideBar') === null) {
        //   localStorage.setItem('isAdminSliderBar', jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsAdmin] == undefined ? 'false' : 'true');
        //   localStorage.setItem('isEmpSideBar', jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsEmployee] === undefined ? 'false':jwtAuthHelper.unPackJWTToken[CustomClaimTypes.IsEmployee].trim().toLowerCase());
        //   if(localStorage.getItem('isAdminSliderBar') === 'false' && localStorage.getItem('isEmpSideBar') === 'true'){
        //     this.router.navigate(['/emp/dashboard']);
        //   }
        // }
        this.isInitialSideBar = (sessionStorage.getItem('isAdminSliderBar') === null || sessionStorage.getItem('isAdminSliderBar') === undefined ) && (sessionStorage.getItem('isEmpSideBar') === null || sessionStorage.getItem('isEmpSideBar') === undefined) ? "true" :"false";
        this.isAdminSideBar = sessionStorage.getItem('isAdminSliderBar') === null || sessionStorage.getItem('isAdminSliderBar') === undefined ? "false" : sessionStorage.getItem('isAdminSliderBar');
        this.isEmpSideBar = sessionStorage.getItem('isEmpSideBar') === null || sessionStorage.getItem('isEmpSideBar') === undefined ? "false" : sessionStorage.getItem('isEmpSideBar');
      }
    });
    this.DataBroadcastService.isUserLoggedIn.subscribe((res) => {
      this.isLoggedIn = res;
    });
    this.DataBroadcastService.ShowMenuSideBar.subscribe((x) => {

      this.showSideBar = x;
    });

    this.DataBroadcastService.ToggleMainMenu.subscribe((x) => {
      if (x == 'close') this.leftMainMenu.close();
      else this.leftMainMenu.toggle();
    });

    this.toggleMenu = this.store.select('toggle');
    // this.toggleMenu.subscribe((x)=>{
    //
    //   if (x === 'close') this.leftMainMenu.close();
    //   else this.leftMainMenu.toggle();
    // })
  }

  changLocale() {
    if (this.lang.length == 0 || this.lang == '' || undefined)
      localStorage.setItem('lang', this.defaultLang);
    else localStorage.setItem('lang', this.lang);

    const browserLang = String(localStorage.getItem('lang'));
    this.translate.use(browserLang);

    let currentUrl = decodeURIComponent(this.router.url).split('?');
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([currentUrl[0]]);
  }

  menuClosed(event: any) {
    this.store.dispatch(sideBarClose());
  }

  toggleSideBar() {
    let instance = jwtAuthHelper.unPackJWTToken[CustomClaimTypes.InstanceName];
    if (instance) {
      this.showSideBar = !this.showSideBar;
      this.DataBroadcastService.ShowMenuSideBar.next(this.showSideBar);
      this.jtaSideBarOpen = true;
    }
  }

  toggleAddCertificationPanel() {
    this.empcertificationAddEditPanelVisible =
      !this.empcertificationAddEditPanelVisible;

    if (!this.empcertificationAddEditPanelVisible) this.closePanel();
    else this.rightflymenu.open();
  }

  togglePosAddEditPanel() {
    this.addPositionModalVisible = !this.addPositionModalVisible;
    if (!this.addPositionModalVisible) {
      // this.showlistPosition = true;
      // this.getPositionList();
    } else {
      // this.addPositionModalVisible
      //   ? (this.posHeader = 'Add New Position')
      //   : (this.posHeader = 'Edit Position');
    }
  }

  closePanel() {
    this.flyInPanelName = '';
    this.rightflymenu.close();
  }
}
