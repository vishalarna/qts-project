import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GeneralDatabaseSettingsComponent } from './general-database-settings.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatExpansionModule } from '@angular/material/expansion';
import {  MatIconModule } from '@angular/material/icon';
import {BaseModule} from "../../../../base/base.module";
import { CustomizeDashboardModule } from '../customize-dashboard/customize-dashboard.module';

@NgModule({
  declarations: [
    GeneralDatabaseSettingsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatIconModule,
    BaseModule,
    CustomizeDashboardModule
  ],
  exports: [
    GeneralDatabaseSettingsComponent
  ]
})
export class GeneralDatabaseSettingsModule { }
