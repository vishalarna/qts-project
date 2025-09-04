import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelShRrLinkComponent } from './fly-panel-sh-rr-link.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddRrModule } from '../../reg-requirements/fly-panel-add-rr/fly-panel-add-rr.module';

@NgModule({
  declarations: [FlyPanelShRrLinkComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    FlyPanelAddRrModule,
  ],
  exports: [FlyPanelShRrLinkComponent],
})
export class FlyPanelShRrLinkModule {}
