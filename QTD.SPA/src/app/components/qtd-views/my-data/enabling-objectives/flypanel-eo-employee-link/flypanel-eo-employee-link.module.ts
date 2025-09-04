import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoEmployeeLinkComponent } from './flypanel-eo-employee-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelEoEmployeeLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    MatMenuModule,
    FormsModule,
  ],
  exports : [
    FlypanelEoEmployeeLinkComponent,
  ]
})
export class FlypanelEoEmployeeLinkModule { }
