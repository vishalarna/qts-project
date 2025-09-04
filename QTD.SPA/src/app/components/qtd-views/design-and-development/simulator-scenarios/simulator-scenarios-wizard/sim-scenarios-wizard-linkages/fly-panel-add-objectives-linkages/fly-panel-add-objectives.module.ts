import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelAddObjectivesLinkagesComponent } from './fly-panel-add-objectives-linkages.component';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyProgressSpinnerModule as MatProgressSpinnerModule } from '@angular/material/legacy-progress-spinner';

@NgModule({
  declarations: [FlyPanelAddObjectivesLinkagesComponent],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    MatTreeModule,
    MatCheckboxModule,
    MatTabsModule,
    MatMenuModule,
    MatSelectModule,
    MatProgressSpinnerModule,
    FormsModule,
  ],
  exports: [FlyPanelAddObjectivesLinkagesComponent],
})
export class FlyPanelAddObjectivesLinkagesModule {}
