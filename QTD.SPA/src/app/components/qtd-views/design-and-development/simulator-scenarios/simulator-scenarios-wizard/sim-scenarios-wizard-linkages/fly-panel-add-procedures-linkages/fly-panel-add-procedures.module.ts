import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox'; 
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatTreeModule } from '@angular/material/tree';
import { FlyPanelAddProceduresLinkagesComponent } from './fly-panel-add-procedures-linkages.component';

@NgModule({
  declarations: [FlyPanelAddProceduresLinkagesComponent],
  imports: [
    CommonModule,
    MatCardModule,
    DragDropModule,
    LocalizeModule,
    BaseModule,
    MatSelectModule,
    MatSidenavModule,
    FormsModule,
    ReactiveFormsModule,
    MatExpansionModule,
    MatCheckboxModule,
    MatTreeModule,
    MatMenuModule
  ],
  exports: [FlyPanelAddProceduresLinkagesComponent],
})
export class FlyPanelAddProceduresLinkagesModule {}

