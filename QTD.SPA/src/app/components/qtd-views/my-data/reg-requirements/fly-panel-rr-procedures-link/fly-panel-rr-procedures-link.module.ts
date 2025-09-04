import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelRRProceduresLinkComponent } from './fly-panel-rr-procedures-link.component';
import { FlyPanelAddProcedureModule } from '../../procedures/fly-panel-add-procedure/fly-panel-add-procedure.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';

@NgModule({
  declarations: [FlyPanelRRProceduresLinkComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    FlyPanelAddProcedureModule,
  ],
  exports: [FlyPanelRRProceduresLinkComponent],
})
export class FlyPanelRRProceduresLinkModule {}
