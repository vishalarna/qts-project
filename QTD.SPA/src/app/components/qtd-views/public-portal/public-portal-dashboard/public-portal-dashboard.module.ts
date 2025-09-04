import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicPortalDashboardComponent } from './public-portal-dashboard.component';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from '../../layout/layout.module';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { DisclaimerDialogModule } from '../../employee-portal/evaluation/disclaimer-dialog/disclaimer-dialog.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelPublicClassDetailViewModule } from '../fly-panel-public-class-detail-view/fly-panel-public-class-detail-view.component.module';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlyPanelRegistrationPageModule } from '../fly-panel-registration-page/fly-panel-registration-page.module';

const routes: Routes = [
  {
    path:'',
    component:PublicPortalDashboardComponent,
  },
]


@NgModule({
  declarations: [
    PublicPortalDashboardComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    LayoutModule,
    MatSidenavModule,
    MatTabsModule,
    MatSelectModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    BaseModule,
    MatTooltipModule,
    MatMenuModule,
    MatToolbarModule,
    MatTableModule,
    DisclaimerDialogModule,
    FlyPanelPublicClassDetailViewModule, 
    MatDialogModule,
    FormsModule,
    ReactiveFormsModule,
    FlyPanelRegistrationPageModule
  ],
  exports:[PublicPortalDashboardComponent]
})
export class PublicPortalDashboardComponentModule { }
