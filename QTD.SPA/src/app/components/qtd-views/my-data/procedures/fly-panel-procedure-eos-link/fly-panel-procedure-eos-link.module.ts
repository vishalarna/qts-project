import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelProcedureEosLinkComponent } from './fly-panel-procedure-eos-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { FlypanelAddEoModule } from '../../enabling-objectives/flypanel-add-eo/flypanel-add-eo.module';



@NgModule({
  declarations: [FlyPanelProcedureEosLinkComponent],
  imports: [
    CommonModule,

    BaseModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    FlypanelAddEoModule
  ],
  exports: [FlyPanelProcedureEosLinkComponent]
})
export class FlyPanelProcedureEosLinkModule { }
