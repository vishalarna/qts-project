import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DatabaseSettingsComponent} from './database-settings.component';
import {FormsModule} from '@angular/forms';
import {CustomizeDashboardModule} from './customize-dashboard/customize-dashboard.module';
import {ProductOverviewModule} from './product-overview/product-overview.module';
import {UserRoleAndPermissionsModule} from './user-role-and-permissions/user-role-and-permissions.module';
import {LabelReplacementModule} from './label-replacement/label-replacement.module';
import {MatLegacyTabsModule as MatTabsModule} from "@angular/material/legacy-tabs";
import {GeneralDatabaseSettingsModule} from "./general-database-settings/general-database-settings.module";
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../layout/layout.module';
import { FeaturesModule } from './features/features.module';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    DatabaseSettingsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    CustomizeDashboardModule,
    ProductOverviewModule,
    UserRoleAndPermissionsModule,
    LabelReplacementModule,
    MatTabsModule,
    GeneralDatabaseSettingsModule,
    BaseModule,
    LayoutModule,
    RouterModule,
    FeaturesModule
  ],
  exports: [
    DatabaseSettingsComponent
  ]
})
export class DatabaseSettingsModule {
}
