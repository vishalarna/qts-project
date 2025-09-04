import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RosterTestComponent } from './roster-test.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { NgChartsModule } from 'ng2-charts';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';



@NgModule({
  declarations: [
    RosterTestComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    NgChartsModule,
    MatSelectModule,
    MatMenuModule,
    ReactiveFormsModule,
    MatIconModule,
    FormsModule,
    MatTooltipModule,
  ],
  exports : [
    RosterTestComponent,
  ]
})
export class RosterTestModule { }
