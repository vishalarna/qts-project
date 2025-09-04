import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskProcedureLinkComponent } from './fly-panel-task-procedure-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatTreeModule } from '@angular/material/tree';
import { FlyPanelAddProcedureModule } from '../../procedures/fly-panel-add-procedure/fly-panel-add-procedure.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelTaskProcedureLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTableModule,
    MatCheckboxModule,
    MatTreeModule,
    FlyPanelAddProcedureModule,
    MatMenuModule,
    FormsModule,
  ],
  exports : [
    FlyPanelTaskProcedureLinkComponent
  ]
})
export class FlyPanelTaskProcedureLinkModule { }
