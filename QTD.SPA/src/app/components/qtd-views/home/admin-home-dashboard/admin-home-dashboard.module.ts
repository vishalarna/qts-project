import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminHomeDashboardComponent } from './admin-home-dashboard.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from "../../layout/layout.module";
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CalendarBaseModule } from 'src/app/components/base/calendar-base/calendar-base.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { AdminMessagesComponentModule } from 'src/app/components/shared/admin-messages/admin-messages.module';
import { FlypanelCertificationExpirationTableFilterModule } from './flypanel-certification-expiration-table-filter/flypanel-certification-expiration-table-filter.module';
import { MatIconModule } from '@angular/material/icon';

const routes: Routes = [{
  path: '',
  component: AdminHomeDashboardComponent,
  children: [
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full',
    },

  ]
},
];


@NgModule({
    declarations: [
        AdminHomeDashboardComponent
    ],
    imports: [
        CommonModule,
        BaseModule,
        MatTableModule,
        LayoutModule,
        MatCheckboxModule,
        RouterModule.forChild(routes),
        CalendarBaseModule,
        MatPaginatorModule,
        MatSortModule,
        AdminMessagesComponentModule,
        MatIconModule,
        FlypanelCertificationExpirationTableFilterModule
    ]
})
export class AdminHomeDashboardModule { }
