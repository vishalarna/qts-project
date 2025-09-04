import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkTaskComponent } from './fly-panel-link-task.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { ReactiveFormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';



@NgModule({
  declarations: [FlyPanelLinkTaskComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    MatMenuModule,
    MatTreeModule
  ],
  exports: [FlyPanelLinkTaskComponent]
})
export class FlyPanelLinkTaskModule { }
