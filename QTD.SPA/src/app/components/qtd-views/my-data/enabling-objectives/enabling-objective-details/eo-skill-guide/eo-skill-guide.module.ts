import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EoSkillGuideComponent } from './eo-skill-guide.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlypanelEoAddQuestionsModule } from '../../flypanel-eo-add-questions/flypanel-eo-add-questions.module';
import { FlypanelEoAddStepModule } from '../../flypanel-eo-add-step/flypanel-eo-add-step.module';
import { FlypanelEoAddSuggestionModule } from '../../flypanel-eo-add-suggestion/flypanel-eo-add-suggestion.module';
import { FlypanelEoEditConditionsModule } from '../../flypanel-eo-edit-conditions/flypanel-eo-edit-conditions.module';
import { FlypanelEoEditCriteriaModule } from '../../flypanel-eo-edit-criteria/flypanel-eo-edit-criteria.module';
import { FlypanelEoEditReferencesModule } from '../../flypanel-eo-edit-references/flypanel-eo-edit-references.module';
import { FlypanelEoToolLinkModule } from '../../flypanel-eo-tool-link/flypanel-eo-tool-link.module';
import { FlypanelEoEditToolModule } from '../../flypanel-eo-edit-tool/flypanel-eo-edit-tool.module';
import { FlyPanelAddStepModule } from '../../../tasks/fly-panel-add-step/fly-panel-add-step.module';



@NgModule({
  declarations: [
    EoSkillGuideComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    DragDropModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    FlypanelEoAddQuestionsModule,
    FlypanelEoAddStepModule,
    FlypanelEoAddSuggestionModule,
    FlypanelEoEditConditionsModule,
    FlypanelEoEditCriteriaModule,
    FlypanelEoEditReferencesModule,
    FlypanelEoEditToolModule
  ],
  exports : [
    EoSkillGuideComponent,
  ]
})
export class EoSkillGuideModule { }
