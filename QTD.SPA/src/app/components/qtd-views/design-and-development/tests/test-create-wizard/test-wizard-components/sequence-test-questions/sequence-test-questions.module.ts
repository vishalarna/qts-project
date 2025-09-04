import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SequenceTestQuestionsComponent } from './sequence-test-questions.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyTabsModule as MatTabsModule } from '@angular/material/legacy-tabs';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlypanelPreviewTestQuestionsModule } from '../../../flypanel-preview-test-questions/flypanel-preview-test-questions.module';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [SequenceTestQuestionsComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MatMenuModule,
    MatTableModule,
    MatTabsModule,
    MatPaginatorModule,
    FlypanelPreviewTestQuestionsModule,
    DragDropModule,
    MatCheckboxModule,
    MatIconModule,
    MatTooltipModule,
    MatSortModule,
    MatSelectModule
  ],
  exports: [SequenceTestQuestionsComponent]
})
export class SequenceTestQuestionsModule { }
