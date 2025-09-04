import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskEosLinkComponent } from './fly-panel-task-eos-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelAddEoModule } from '../../enabling-objectives/flypanel-add-eo/flypanel-add-eo.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelTaskEosLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FlypanelAddEoModule,
    MatCheckboxModule,
    MatTreeModule,
    MatMenuModule,
    FormsModule,
  ],
  exports : [
    FlyPanelTaskEosLinkComponent
  ]
})
export class FlyPanelTaskEosLinkModule { }
