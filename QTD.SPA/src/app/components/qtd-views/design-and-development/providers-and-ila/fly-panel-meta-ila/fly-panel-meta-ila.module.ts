import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelMetaIlaComponent } from './fly-panel-meta-ila.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { MatStepperModule } from '@angular/material/stepper';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlyPanelCreateMetaILATestModule } from '../fly-panel-create-meta-ila-test/fly-panel-create-meta-ila-test.module';
import { FlyPanelMetaIlaIlaRequirementsModule } from '../fly-panel-meta-ila-ila-requirements/fly-panel-meta-ila-ila-requirements.module';
import { FlyPanelCreateMetaILAStudentEvaluationModule } from '../fly-panel-create-meta-ila-student-evaluation/fly-panel-create-meta-ila-student-evaluation.module';
import { MatLegacyOptionModule as MatOptionModule } from '@angular/material/legacy-core';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FlyPanelEditEvaluationModule } from '../../../evaluation/student-evaluation/fly-panel-edit-evaluation/fly-panel-edit-evaluation.module';
import { EnrollMetaILAStudentsModule } from './fly-panel-meta-ila-components/enroll-meta-ila-students/enroll-meta-ila-students.module';
import { FlyPanelAddMetaILAEmployeesModule } from './fly-panel-meta-ila-components/fly-panel-add-meta-ila-employees/fly-panel-add-meta-ila-employees.module';
import { MatIconModule } from '@angular/material/icon';
import { FlyPanelLinkILAsToMetaILAModule } from './fly-panel-meta-ila-components/fly-panel-link-ilas-to-meta-ila/fly-panel-link-ilas-to-meta-ila.module';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';

@NgModule({
  declarations: [FlyPanelMetaIlaComponent],
  imports: [
    CommonModule,
    BaseModule,
    ReactiveFormsModule,
    CKEditorModule,
    MatRadioModule,
    DragDropModule,
    MatMenuModule,
    MatDialogModule,
    MatStepperModule,
    MatFormFieldModule,
    MatTooltipModule,
    MatCheckboxModule,
    FormsModule,
    FlyPanelCreateMetaILATestModule,
    FlyPanelMetaIlaIlaRequirementsModule,
    FlyPanelCreateMetaILAStudentEvaluationModule,
    MatOptionModule,
    MatSelectModule,
    FlyPanelEditEvaluationModule,
    EnrollMetaILAStudentsModule,
    FlyPanelAddMetaILAEmployeesModule,
    MatIconModule,
    FlyPanelLinkILAsToMetaILAModule,
    MatInputModule
  ],
  exports: [FlyPanelMetaIlaComponent],
})
export class FlyPanelMetaIlaModule { }
