import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTaskIlaLinkComponent } from './fly-panel-task-ila-link.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FormsModule } from '@angular/forms';
import { FlyPanelAddIlaModule } from '../../../design-and-development/providers-and-ila/fly-panel-add-ila/fly-panel-add-ila.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';



@NgModule({
  declarations: [
    FlyPanelTaskIlaLinkComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    MatTreeModule,
    MatCheckboxModule,
    FormsModule,
    FlyPanelAddIlaModule,
    MatMenuModule,
  ],
  exports : [
    FlyPanelTaskIlaLinkComponent
  ]
})
export class FlyPanelTaskIlaLinkModule { }
