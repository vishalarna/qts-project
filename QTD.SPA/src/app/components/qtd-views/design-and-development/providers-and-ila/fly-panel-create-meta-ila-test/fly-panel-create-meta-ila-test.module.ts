import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelCreateMetaILATestComponent } from './fly-panel-create-meta-ila-test.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { DragDropModule } from '@angular/cdk/drag-drop';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AddTestInformationModule } from './fly-panel-create-meta-ila-test-components/add-test-information/add-test-information.module';
import { AddAndSequenceTestQuestionsModule } from './fly-panel-create-meta-ila-test-components/add-and-sequence-test-questions/add-and-sequence-test-questions.module';
import { PreviewAndPublishMetaILATestModule } from './fly-panel-create-meta-ila-test-components/preview-and-publish-meta-ila-test/preview-and-publish-meta-ila-test.module';
import { FlypanelAddTestItemModule } from '../../tests/test-question-bank/flypanel-add-test-item/flypanel-add-test-item.module';
import { ImportMetaILATestQuestionsModule } from './fly-panel-create-meta-ila-test-components/import-meta-ila-test-questions/import-meta-ila-test-questions.module';

const routes: Routes = [
];

@NgModule({
  declarations: [FlyPanelCreateMetaILATestComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    BaseModule,
    LayoutModule,
    MatDialogModule,
    MatStepperModule,
    DragDropModule,
    MatTooltipModule,
    CKEditorModule,
    FormsModule,
    FormsModule,
    MatToolbarModule,
    AddTestInformationModule,
    AddAndSequenceTestQuestionsModule,
    PreviewAndPublishMetaILATestModule,
    FlypanelAddTestItemModule,
    ImportMetaILATestQuestionsModule
  ],
  exports:[FlyPanelCreateMetaILATestComponent]
})
export class FlyPanelCreateMetaILATestModule {}
