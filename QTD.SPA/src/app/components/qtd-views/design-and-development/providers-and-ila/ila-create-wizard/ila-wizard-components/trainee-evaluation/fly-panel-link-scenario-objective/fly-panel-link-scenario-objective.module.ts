import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelLinkScenarioObjectiveComponent } from './fly-panel-link-scenario-objective.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckbox as MatCheckbox, MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatTreeModule } from '@angular/material/tree';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatToolbarModule } from '@angular/material/toolbar';



@NgModule({
  declarations: [FlyPanelLinkScenarioObjectiveComponent],
  imports: [
    CommonModule,
    BaseModule,
    MatCheckboxModule,
    MatTreeModule,
    ReactiveFormsModule,
    MatMenuModule,
    MatTabsModule,
    FormsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatToolbarModule
  ],
  exports: [FlyPanelLinkScenarioObjectiveComponent]
})
export class FlyPanelLinkScenarioObjectiveModule { }
