import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoEditToolComponent } from './flypanel-eo-edit-tool.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelEoToolLinkModule } from '../flypanel-eo-tool-link/flypanel-eo-tool-link.module';
import { BaseModule } from 'src/app/components/base/base.module';


@NgModule({
  declarations: [
    FlypanelEoEditToolComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatCheckboxModule,
    MatMenuModule,
    FlypanelEoToolLinkModule,
  ],
  exports : [
    FlypanelEoEditToolComponent,
  ]
})
export class FlypanelEoEditToolModule { }
