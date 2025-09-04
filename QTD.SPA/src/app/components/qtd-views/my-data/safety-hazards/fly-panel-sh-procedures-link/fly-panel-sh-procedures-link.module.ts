import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelShProceduresLinkComponent } from './fly-panel-sh-procedures-link.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddProcedureModule } from '../../procedures/fly-panel-add-procedure/fly-panel-add-procedure.module';

@NgModule({
  declarations: [FlyPanelShProceduresLinkComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    FlyPanelAddProcedureModule,
  ],
  exports: [FlyPanelShProceduresLinkComponent],
})
export class FlyPanelShProceduresLinkModule {}
