import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatIconModule } from '@angular/material/icon';
import { FlyPanelEditPerformanceCriteriaComponent } from './fly-panel-edit-performance-criteria.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { EditCriteriaModalModule } from '../edit-criteria-modal/edit-criteria-modal.module';

@NgModule({
  declarations: [FlyPanelEditPerformanceCriteriaComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatSelectModule,
    MatTableModule,
    MatIconModule,
    CKEditorModule,
    EditCriteriaModalModule
  ],
  exports :[FlyPanelEditPerformanceCriteriaComponent]
})
export class FlyPanelEditPerformanceCriteriaModule {}
