import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SelfRegistrationOverviewComponent } from './self-registration-overview.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { DisclaimerDialogModule } from '../../evaluation/disclaimer-dialog/disclaimer-dialog.module';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';

const routes:Routes = [
  {
    path:'',
    component:SelfRegistrationOverviewComponent,
  }
]

@NgModule({
  declarations: [
    SelfRegistrationOverviewComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    RouterModule.forChild(routes),
    MatSidenavModule,
    MatTabsModule,
    MatTableModule,
    DisclaimerDialogModule,
    MatSortModule,
    MatPaginatorModule,
    MatTooltipModule,
    MatChipsModule
  ]
})
export class SelfRegistrationOverviewModule { }
