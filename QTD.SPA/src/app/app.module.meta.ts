import {BrowserModule} from "@angular/platform-browser";
import {CommonModule, APP_BASE_HREF} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from "@angular/common/http";
import {TranslateLoader, TranslateModule, TranslateStore} from "@ngx-translate/core";
import {AppRoutingModule} from "./app-routing.module";
import {SelectDropDownModule} from "ngx-select-dropdown";
import {LocalizeModule} from "./_Shared/modules/localize.module";
import {NgbModule} from "@ng-bootstrap/ng-bootstrap";
import {DataTablesModule} from "angular-datatables";
import {EmpModule} from "./components/qtd-views/implementation/emp/emp.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {BaseModule} from "./components/base/base.module";
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatLegacyMenuModule as MatMenuModule} from "@angular/material/legacy-menu";
import {LayoutModule} from "./components/qtd-views/layout/layout.module";
import {PortalModule} from "@angular/cdk/portal";
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table'
import {MatExpansionModule} from "@angular/material/expansion";
import {MatLegacySlideToggleModule as MatSlideToggleModule} from "@angular/material/legacy-slide-toggle";
import {StoreModule} from "@ngrx/store";
import {
  backdropReducer,
  bgdisableCloseReducer,
  freezeMenuReducer,
  sideBarMenuModeReducer,
  toggleReducer
} from "./_Statemanagement/reducer/state.menureducer";
import {loggedInReducer} from "./_Statemanagement/reducer/state.signInReducer";
import {deleteReducer, evalationInformationReducer, getTestInfoReducer, navigateTQReducer, saveReducer} from "./_Statemanagement/reducer/state.componentcommunicationreducer";
import {StoreDevtoolsModule} from "@ngrx/store-devtools";
import {environment} from "../environments/environment";
import {ErrorInterceptorProvider} from "./_Interceptors/error.interceptor";
import {AppComponent} from "./app.component";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";
import {JwtInterceptor} from "./_Interceptors/jwt.interceptor";
import {ErrorHandler} from "@angular/core";
import {GlobalErrorHandler} from "./_Interceptors/client.errors.interceptor";
import { MatLegacySelectModule as MatSelectModule } from "@angular/material/legacy-select";
import {MatLegacyChipsModule as MatChipsModule} from "@angular/material/legacy-chips";
import {clientSettingsServiceProvider} from "./_Services/QTD/ClientSettings/iclientsettings-service";
import { MatLegacyCheckboxModule as MatCheckboxModule } from "@angular/material/legacy-checkbox";
import { MatLegacyFormFieldModule as MatFormFieldModule } from "@angular/material/legacy-form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatLegacyInputModule as MatInputModule } from "@angular/material/legacy-input";
import { DragDropModule } from "@angular/cdk/drag-drop";
import { clientUserSettingsServiceProvider } from "./_Services/QTD/ClientUserSettings/iclientusersettings-service";
import { reportsServiceProvider } from "./_Services/QTD/Reports/ireports-service";
import { CKEditorModule } from "@ckeditor/ckeditor5-angular";
import { scormServiceProvider } from "./_Services/QTD/Scorm/iscorm.service";
import { LabelReplacementPipe } from "./_Pipes/label-replacement.pipe";
import { TaskSortPipePipe } from "./_Pipes/task-sort-pipe.pipe";

import { documentsServiceProvider } from "./_Services/QTD/Documents/idocuments-service";
import { documentTypesServiceProvider } from "./_Services/QTD/DocumentTypes/idocumentTypes-service";

import { TypedDateValidatorDirective } from "./_Shared/directives/typed-date-validator.directive";
import { trainingProgramReviewServiceProvider } from "./_Services/QTD/TrainingProgramReview/itrainingProgramReview-service";
import { difSurveyServiceProvider } from "./_Services/QTD/DifSurvey/idifsurvey-service";
import { DynamicLabelReplacementPipe } from "./_Pipes/dynamic-label-replacement.pipe";
import { taskListReviewProvider } from "./_Services/QTD/TaskListReview/itasklistreview.service";
import { MatLegacyPaginatorModule as MatPaginatorModule } from "@angular/material/legacy-paginator";


function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/');
}

const AppBaseHrefProvider = {
  provide: APP_BASE_HREF,
  useValue: '/'
};

const JWTInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: JwtInterceptor,
  multi: true,
};
const ClientErrorInterceptor = {
  provide: ErrorHandler,
  useClass: GlobalErrorHandler,
};

export const declarations = [AppComponent,TypedDateValidatorDirective];

export const imports = [
  BrowserModule,
  CommonModule,
  FormsModule,
  MatTableModule,
  CKEditorModule,
  ReactiveFormsModule,
  DragDropModule,
  HttpClientModule,
  MatIconModule,
  MatInputModule ,
  MatFormFieldModule,
  BrowserAnimationsModule ,
  MatChipsModule,
  TranslateModule.forRoot({
    loader: {
      provide: TranslateLoader,
      useFactory: HttpLoaderFactory,
      deps: [HttpClient],
    },
    defaultLanguage: 'en',
    isolate: false,
    extend: true,
    useDefaultLang: true,
  }),
  AppRoutingModule,
  SelectDropDownModule,
  LocalizeModule,
  NgbModule,
  DataTablesModule,
  EmpModule,
  BrowserAnimationsModule,
  BaseModule,
  MatSidenavModule,
  MatMenuModule,
  LayoutModule,
  PortalModule,
  MatExpansionModule,
  MatSlideToggleModule,
  MatSelectModule,
  MatCheckboxModule,
  MatChipsModule,
  MatFormFieldModule,
  MatPaginatorModule,
  StoreModule.forRoot({
    toggle: toggleReducer,
    logIn: loggedInReducer,
    saveIla: saveReducer,
    getTestInfo: getTestInfoReducer,
    deleteIla: deleteReducer,
    menubackdrop: backdropReducer,
    menubgdisableclose: bgdisableCloseReducer,
    menuMode: sideBarMenuModeReducer,
    evalData:evalationInformationReducer,
    navigateTQ:navigateTQReducer,
    freezeMenu:freezeMenuReducer,
  }),
  StoreDevtoolsModule.instrument({
    maxAge: 25,
    logOnly: environment.production,
  }),
];
export const providers = [ErrorInterceptorProvider, JWTInterceptorProvider, TranslateStore, clientSettingsServiceProvider,clientUserSettingsServiceProvider, reportsServiceProvider,scormServiceProvider,documentsServiceProvider,documentTypesServiceProvider, LabelReplacementPipe,TaskSortPipePipe,trainingProgramReviewServiceProvider,difSurveyServiceProvider,DynamicLabelReplacementPipe, taskListReviewProvider];

export const storybookProviders = () => {
  const sbProviders = [...providers];
   // @ts-ignore
  sbProviders.push(AppBaseHrefProvider);
  return sbProviders;
}

export const bootstrap = [AppComponent];
