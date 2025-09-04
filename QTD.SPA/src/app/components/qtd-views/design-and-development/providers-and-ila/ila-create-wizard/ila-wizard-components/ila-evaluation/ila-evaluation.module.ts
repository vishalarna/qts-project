import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseModule } from 'src/app/components/base/base.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { IlaEvaluationComponent } from './ila-evaluation.component';
import { FlyPanelSettingsModule } from './fly-panel-settings/fly-panel-settings.module';
import { FlyPanelCreateModule } from './fly-panel-create/fly-panel-create.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';

@NgModule({
  declarations: [ IlaEvaluationComponent],
  imports: [
    CommonModule,
    BaseModule,
    CKEditorModule,
    MatCheckboxModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatSelectModule,
    FlyPanelSettingsModule,
    FlyPanelCreateModule,
    FormsModule,
    ReactiveFormsModule,
    MatChipsModule
  ],
  exports: [ IlaEvaluationComponent],
})
export class  IlaEvaluationModule {}
