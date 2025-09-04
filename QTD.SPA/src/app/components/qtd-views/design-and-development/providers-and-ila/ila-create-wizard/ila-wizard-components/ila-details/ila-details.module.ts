import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IlaDetailsComponent } from './ila-details.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelIlaPrerequisitesModule } from './fly-panel-ila-prerequisites/fly-panel-ila-prerequisites.module';
import { FlyPanelIlaProceduresModule } from './fly-panel-ila-procedures/fly-panel-ila.procedures.module';
import { FlyaPanelILASafetyHazardModule } from 'src/app/components/qtd-views/design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/ila-details/fly-panel-ila-safety-hazard/fly-panel-ila-safety-hazard.module';
import { FlyPanelIlaRegulatoryRequirementsModule } from './fly-panel-ila-reg-requirements/fly-panel-ila-reg-requirements.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonToggleModule } from '@angular/material/button-toggle';


@NgModule({
  declarations: [IlaDetailsComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    FormsModule,
    MatSelectModule,
    FlyPanelIlaPrerequisitesModule,
    FlyPanelIlaProceduresModule,
    FlyaPanelILASafetyHazardModule,
    FlyPanelIlaRegulatoryRequirementsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    CKEditorModule,
    MatTableModule,
    MatPaginatorModule,
    MatMenuModule,
    MatTooltipModule,
    MatSortModule,
    MatButtonToggleModule
  ],

  exports: [IlaDetailsComponent],
})
export class IlaDetailsModule {}
