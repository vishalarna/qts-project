import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { FlyPanelScenarioModule } from './fly-panel-scenario/fly-panel-scenario.module';
import { FlyPanelPowerDataModule } from './fly-panel-power-data/fly-panel-power-data.module';
import { FlyPanelTestSettingModule } from './fly-panel-test-setting/fly-panel-test-setting.module';
import { FlyPanelQuesBankModule } from './fly-panel-ques-bank/fly-panel-ques-bank.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TraineeEvaluationComponent } from './trainee-evaluation.component';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { FlyPanelPositionsModule } from 'src/app/components/qtd-views/implementation/positions/fly-panel-positions/fly-panel-positions.module';
import { FlyPanelProviderModule } from '../../../fly-panel-provider/fly-panel-provider.module';
import { FlyPanelTopicModule } from '../../../fly-panel-topic/fly-panel-topic.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { FlyPanelCoversheetsModule } from '../../../fly-panel-coversheets/fly-panel-coversheets.module';
import { FlyPanelAddCoversheetsModule } from '../../../fly-panel-add-coversheets/fly-panel-add-coversheets.module';
import { RouterModule } from '@angular/router';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { FlyPanelNewTestModule } from './fly-panel-new-test/fly-panel-new-test.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlyPanelLinkScenarioObjectiveComponent } from './fly-panel-link-scenario-objective/fly-panel-link-scenario-objective.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { FlypanelAddTestItemModule } from '../../../../tests/test-question-bank/flypanel-add-test-item/flypanel-add-test-item.module';
import { FlypanelPreviewTestQuestionsModule } from '../../../../tests/flypanel-preview-test-questions/flypanel-preview-test-questions.module';

@NgModule({
  declarations: [TraineeEvaluationComponent, DeleteDialogComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    LayoutModule,
    FlyPanelPositionsModule,
    FlyPanelProviderModule,
    FlyPanelTopicModule,
    FlyPanelCoversheetsModule,
    FlyPanelAddCoversheetsModule,
    MatSelectModule,
    MatCheckboxModule,
    MatChipsModule,
    MatTooltipModule,
    MatTabsModule,
    MatRadioModule,
    CKEditorModule,
    FlyPanelNewTestModule,
    DragDropModule,
    RouterModule,
    DragDropModule,
    FlyPanelQuesBankModule,
    FlyPanelTestSettingModule,
    FlyPanelPowerDataModule,
    FlyPanelScenarioModule,
    MatMenuModule,
    MatDialogModule,
    MatTableModule,
    MatRadioModule,
    MatPaginatorModule,
    MatSortModule,
    FlypanelAddTestItemModule,
    FlypanelPreviewTestQuestionsModule,
  ],
  exports: [TraineeEvaluationComponent],
})
export class TraineeEvaluationModule {}
