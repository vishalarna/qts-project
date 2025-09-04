import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ImportTestQuestionsComponent } from './import-test-questions.component';
import { FormsModule } from '@angular/forms';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { FlypanelRandomlySelectAllEosModule } from './flypanel-randomly-select-all-eos/flypanel-randomly-select-all-eos.module';
import { FlypanelRandomlySelectByIndividualEoModule } from './flypanel-randomly-select-by-individual-eo/flypanel-randomly-select-by-individual-eo.module';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [ImportTestQuestionsComponent],
  imports: [
    CommonModule,
    FormsModule,
    BaseModule,
    MatTableModule,
    MatPaginatorModule,
    FlypanelRandomlySelectAllEosModule,
    FlypanelRandomlySelectByIndividualEoModule,
    MatSortModule,
  ],
  exports: [ImportTestQuestionsComponent]
})
export class ImportTestQuestionsModule { }
