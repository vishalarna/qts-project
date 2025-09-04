import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {EmployeePickerComponent} from "./employee-picker.component";
import {DataTablesModule} from "angular-datatables";
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [
    EmployeePickerComponent
  ],
  imports: [
    BaseModule,
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    DataTablesModule,
    BaseModule
  ],
  exports: [
    EmployeePickerComponent
  ]
})
export class EmployeePickerModule {
}
