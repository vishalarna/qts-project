import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoProcedureLinkComponent } from './flypanel-eo-procedure-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelAddProcedureModule } from '../../procedures/fly-panel-add-procedure/fly-panel-add-procedure.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlypanelEoProcedureLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    MatMenuModule,
    FormsModule,
    FlyPanelAddProcedureModule,
  ],
  exports : [FlypanelEoProcedureLinkComponent]
})
export class FlypanelEoProcedureLinkModule { }
