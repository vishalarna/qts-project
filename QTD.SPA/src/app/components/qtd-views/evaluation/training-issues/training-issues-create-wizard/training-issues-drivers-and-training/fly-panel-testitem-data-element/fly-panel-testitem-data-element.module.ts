import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { FlyPanelTestItemDataElementComponent } from './fly-panel-testitem-data-element.component';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlypanelPreviewTestQuestionsModule } from 'src/app/components/qtd-views/design-and-development/tests/flypanel-preview-test-questions/flypanel-preview-test-questions.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';

@NgModule({
  declarations: [
    FlyPanelTestItemDataElementComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatRadioModule,
    MatMenuModule,
    MatRadioModule,
    MatTableModule,
    FlypanelPreviewTestQuestionsModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatIconModule,
    MatSelectModule
  ],
  exports: [FlyPanelTestItemDataElementComponent]
})
export class FlyPanelTestItemDataElementModule { }
