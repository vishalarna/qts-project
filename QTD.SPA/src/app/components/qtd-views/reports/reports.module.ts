import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import { ReportsComponent } from './reports.component';
import {FormsModule} from '@angular/forms';
import { ReportsViewModule } from './reports-view/reports-view.module';
import { ReportsRoutingModule } from './reports-routing.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';


@NgModule({
  declarations: [
    ReportsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReportsViewModule,
    ReportsRoutingModule,
    MatPaginatorModule
  ]
})
export class ReportsModule {
}