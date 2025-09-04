import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkToolsComponent } from './fly-panel-link-tools.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelAddToolModule } from '../../tools/fly-panel-add-tool/fly-panel-add-tool.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    FlyPanelLinkToolsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatTreeModule,
    MatMenuModule,
    FlyPanelAddToolModule,
    FormsModule,
  ],
  exports : [
    FlyPanelLinkToolsComponent
  ]
})
export class FlyPanelLinkToolsModule { }
