import { FlyPanelAddRrModule } from './../../../../../../my-data/reg-requirements/fly-panel-add-rr/fly-panel-add-rr.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { FlyPanelIlaRegulatoryRequirementsComponent } from './fly-panel-ila-reg-requirements.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatTreeModule } from '@angular/material/tree';

@NgModule({
  declarations: [FlyPanelIlaRegulatoryRequirementsComponent],
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
    FlyPanelAddRrModule,
    MatTreeModule
  ],
  exports: [FlyPanelIlaRegulatoryRequirementsComponent],
})
export class FlyPanelIlaRegulatoryRequirementsModule {}
