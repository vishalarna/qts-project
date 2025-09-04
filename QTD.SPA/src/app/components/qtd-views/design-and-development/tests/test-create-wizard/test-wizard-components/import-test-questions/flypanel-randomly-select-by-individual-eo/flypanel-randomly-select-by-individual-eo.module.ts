import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlypanelRandomlySelectByIndividualEoComponent } from './flypanel-randomly-select-by-individual-eo.component';
import { LayoutModule } from 'src/app/components/qtd-views/layout/layout.module';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { FlypanelPreviewTestQuestionsModule } from '../../../../flypanel-preview-test-questions/flypanel-preview-test-questions.module';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FormsModule } from '@angular/forms';
import { MatTreeModule } from '@angular/material/tree';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';



@NgModule({
  declarations: [FlypanelRandomlySelectByIndividualEoComponent],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    MatCheckboxModule,
    FlypanelPreviewTestQuestionsModule,
    MatPaginatorModule,
    MatTableModule,
    FormsModule,
    MatTreeModule,
    MatSelectModule,
    MatFormFieldModule,

    MatInputModule,
    MatIconModule,
    MatMenuModule,
    
  ],
  exports: [FlypanelRandomlySelectByIndividualEoComponent]
})
export class FlypanelRandomlySelectByIndividualEoModule { }
