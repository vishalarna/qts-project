import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnablingObjectiveDetailsComponent } from './enabling-objective-details.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { EoIlasModule } from './eo-ilas/eo-ilas.module';
import { EoProceduresModule } from './eo-procedures/eo-procedures.module';
import { EoRegulatoryRequirementsModule } from './eo-regulatory-requirements/eo-regulatory-requirements.module';
import { EoSafetyHazardsModule } from './eo-safety-hazards/eo-safety-hazards.module';
import { EoTasksModule } from './eo-tasks/eo-tasks.module';
import { EoTestQuestionsModule } from './eo-test-questions/eo-test-questions.module';
import { FlypanelAddEoModule } from '../flypanel-add-eo/flypanel-add-eo.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlypanelMetaEoLinkModule } from '../flypanel-meta-eo-link/flypanel-meta-eo-link.module';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { EoEmployeesModule } from './eo-employees/eo-employees.module';
import { EoPositionsModule } from './eo-positions/eo-positions.module';
import { EoSkillGuideModule } from './eo-skill-guide/eo-skill-guide.module';
import { FlypanelEoHistoryModule } from '../flypanel-eo-history/flypanel-eo-history.module';

const routes : Routes = [
  {
    path: ':id',
    component: EnablingObjectiveDetailsComponent,
  }
]

@NgModule({
  declarations: [
    EnablingObjectiveDetailsComponent
  ],
  imports: [
    BaseModule,
    CommonModule,
    RouterModule.forChild(routes),
    BaseModule,
    MatMenuModule,
    MatTableModule,
    MatTabsModule,
    MatPaginatorModule,
    EoIlasModule,
    EoProceduresModule,
    EoRegulatoryRequirementsModule,
    EoSafetyHazardsModule,
    EoTasksModule,
    EoTestQuestionsModule,
    EoEmployeesModule,
    EoPositionsModule,
    EoSkillGuideModule,
    FlypanelAddEoModule,
    DragDropModule,
    MatCheckboxModule,
    FlypanelMetaEoLinkModule,
    MatIconModule,
    MatTooltipModule,
    FlypanelEoHistoryModule,
  ]
})
export class EnablingObjectiveDetailsModule { }
