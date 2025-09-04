import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalendarBaseComponent } from './calendar-base.component';
import { BaseModule } from '../base.module';
import { LayoutModule } from '../../qtd-views/layout/layout.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';


@NgModule({
  declarations: [
    CalendarBaseComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,

    MatCheckboxModule,
    MatSelectModule,
    MatButtonToggleModule,
    MatDialogModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    MatTableModule
  ],
  exports :[CalendarBaseComponent]
})
export class CalendarBaseModule { }
