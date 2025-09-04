import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { FlypanelRandomlySelectAllEosComponent } from './flypanel-randomly-select-all-eos.component';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlypanelPreviewTestQuestionsModule } from '../../../../flypanel-preview-test-questions/flypanel-preview-test-questions.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FormsModule } from '@angular/forms';




@NgModule({
  declarations: [FlypanelRandomlySelectAllEosComponent],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    MatCheckboxModule,
    FlypanelPreviewTestQuestionsModule,
    MatPaginatorModule,
    MatTableModule,
    FormsModule,

  ],
  exports: [FlypanelRandomlySelectAllEosComponent]
})
export class FlypanelRandomlySelectAllEosModule { }
