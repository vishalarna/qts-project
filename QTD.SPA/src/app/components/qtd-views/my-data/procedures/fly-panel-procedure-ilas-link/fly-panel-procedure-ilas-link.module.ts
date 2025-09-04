import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelProcedureIlasLinkComponent } from './fly-panel-procedure-ilas-link.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddIlaModule } from '../../../design-and-development/providers-and-ila/fly-panel-add-ila/fly-panel-add-ila.module';

@NgModule({
  declarations: [FlyPanelProcedureIlasLinkComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    FlyPanelAddIlaModule,
  ],
  exports: [FlyPanelProcedureIlasLinkComponent],
})
export class FlyPanelProcedureIlasLinkModule {}
