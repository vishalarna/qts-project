import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelEoToolLinkComponent } from './flypanel-eo-tool-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelAddToolModule } from '../../tools/fly-panel-add-tool/fly-panel-add-tool.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';



@NgModule({
  declarations: [
    FlypanelEoToolLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    FlyPanelAddToolModule,
    FormsModule,
    MatMenuModule,
  ],
  exports : [
    FlypanelEoToolLinkComponent,
  ]
})
export class FlypanelEoToolLinkModule { }
