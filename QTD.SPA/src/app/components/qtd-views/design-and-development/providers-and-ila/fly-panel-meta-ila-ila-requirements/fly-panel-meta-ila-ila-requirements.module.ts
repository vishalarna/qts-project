import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelMetaIlaIlaRequirementsComponent } from './fly-panel-meta-ila-ila-requirements.component';
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
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';

@NgModule({
  declarations: [FlyPanelMetaIlaIlaRequirementsComponent],
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
    MatTableModule     
  ],
  exports: [FlyPanelMetaIlaIlaRequirementsComponent],
})
export class FlyPanelMetaIlaIlaRequirementsModule {}
