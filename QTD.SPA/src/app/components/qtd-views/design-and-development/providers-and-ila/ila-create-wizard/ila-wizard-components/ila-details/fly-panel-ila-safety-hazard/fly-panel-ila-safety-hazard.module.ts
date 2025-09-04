import { FlypanelAddSafetyHazardsModule } from './../../../../../../my-data/safety-hazards/flypanel-add-safety-hazards/flypanel-add-safety-hazards.module';
import { NgModule } from '@angular/core';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FormsModule } from '@angular/forms';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyaPanelILASafetyHazardComponent } from './fly-panel-ila-safety-hazard.component';
import { MatTreeModule } from '@angular/material/tree';


@NgModule({
  declarations: [FlyaPanelILASafetyHazardComponent],
  imports: [
    CommonModule,
    MatCardModule,
    DragDropModule,
    LocalizeModule,
    BaseModule,
    MatSelectModule,
    MatSidenavModule,
    FormsModule,
    MatExpansionModule,
    MatMenuModule,
    MatCheckboxModule,
    FlypanelAddSafetyHazardsModule,
    MatTreeModule
  ],
  exports: [FlyaPanelILASafetyHazardComponent],
})
export class FlyaPanelILASafetyHazardModule {}
