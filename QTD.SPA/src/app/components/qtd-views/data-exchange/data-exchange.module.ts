import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {LocalizeModule} from 'src/app/_Shared/modules/localize.module';
import {DataExchangeRoutingModule} from "./data-exchange-routing.module";
import {DataExchangeComponent} from "./data-exchange.component";
import { DatabaseSettingsModule } from './database-settings/database-settings.module';
import {BaseModule} from "../../base/base.module";
import { DataImportModule } from './data-import/data-import.module';


@NgModule({
  declarations: [
    DataExchangeComponent,
  ],
  imports: [
    BaseModule,
    CommonModule,
    HttpClientModule,
    LocalizeModule,
    DatabaseSettingsModule,
    DataExchangeRoutingModule,
    DataImportModule
  ],
})
export class DataExchangeModule {
}
