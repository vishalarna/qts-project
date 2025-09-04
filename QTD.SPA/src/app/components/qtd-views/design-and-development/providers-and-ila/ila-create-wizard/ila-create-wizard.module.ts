import { FormsModule } from '@angular/forms';
import { NewSimulationModule } from './ila-wizard-components/new-simulation/new-simulation.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IlaCreateWizardComponent } from './ila-create-wizard.component';
import { CreateIlaModule } from './ila-wizard-components/create-ila/create-ila.module';
import { IlaDetailsComponent } from './ila-wizard-components/ila-details/ila-details.component';
import { IlaDetailsModule } from './ila-wizard-components/ila-details/ila-details.module';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatStepperModule } from '@angular/material/stepper';
import { TrainingPlanModule } from './ila-wizard-components/training-plan/training-plan.module';
import {IlaApplicationModule} from './ila-wizard-components/ila-application/ila-application.module'
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { PreviewTestModule } from './ila-wizard-components/trainee-evaluation/preview-test/preview-test.module';
import {PreviewIlaModule} from './ila-wizard-components/preview-ila/preview-ila.module'
import { TraineeEvaluationModule } from './ila-wizard-components/trainee-evaluation/trainee-evaluation.module';
import { IlaEvaluationModule } from './ila-wizard-components/ila-evaluation/ila-evaluation.module';
import { FlyPanelNewTestModule } from './ila-wizard-components/trainee-evaluation/fly-panel-new-test/fly-panel-new-test.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import {MatLegacyTooltipModule as MatTooltipModule} from '@angular/material/legacy-tooltip';
import { SeqAndScheduleModule } from './ila-wizard-components/seq-and-schedule/seq-and-schedule.module';
import { CreateGuideEditorModule } from './ila-wizard-components/training-material/create-guide-editor/create-guide-editor.module';
import { TrainingMaterialModule } from './ila-wizard-components/training-material/training-material.module';
import { MatIconModule } from '@angular/material/icon';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { PublishIlaModalModule } from './ila-wizard-components/publish-ila-modal/publish-ila-modal.module';


const routes: Routes = [
  {
    path: '',
    component: IlaCreateWizardComponent,
  },
];

@NgModule({
  declarations: [IlaCreateWizardComponent],
  imports: [
    CommonModule,
    CreateIlaModule,
    IlaDetailsModule,
    TrainingPlanModule,
    IlaApplicationModule,
    PreviewTestModule,
    PreviewIlaModule,
    TraineeEvaluationModule,
    TrainingMaterialModule,
    SeqAndScheduleModule,
    HttpClientModule,
    CreateGuideEditorModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    BaseModule,
    LayoutModule,
    MatDialogModule,
    MatToolbarModule,
    MatStepperModule,
    IlaEvaluationModule,
    FlyPanelNewTestModule,
    DragDropModule,
    MatTooltipModule,
    NewSimulationModule,
    CKEditorModule,
    FormsModule,
    MatIconModule,
    FormsModule,
    PublishIlaModalModule,
  ],
})
export class IlaCreateWizardModule {}
