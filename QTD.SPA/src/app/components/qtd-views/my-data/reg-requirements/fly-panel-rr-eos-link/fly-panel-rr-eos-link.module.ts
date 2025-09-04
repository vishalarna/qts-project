import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelRREOsLinkComponent } from './fly-panel-rr-eos-link.component';
import { FormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelAddEoModule } from '../../enabling-objectives/flypanel-add-eo/flypanel-add-eo.module';

@NgModule({
  declarations: [FlyPanelRREOsLinkComponent],
  imports: [
    CommonModule,

    BaseModule,
    MatMenuModule,
    FormsModule,
    MatCheckboxModule,
    MatTreeModule,
    FlypanelAddEoModule,
  ],
  exports: [FlyPanelRREOsLinkComponent],
})
export class FlyPanelRREOsLinkModule {}
