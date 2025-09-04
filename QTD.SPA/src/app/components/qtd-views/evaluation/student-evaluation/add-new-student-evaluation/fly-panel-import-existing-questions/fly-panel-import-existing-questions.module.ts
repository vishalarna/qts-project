import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FlyPanelImportExistingQuestionsComponent } from './fly-panel-import-existing-questions.component';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatSortModule } from '@angular/material/sort';



@NgModule({
  declarations: [
    FlyPanelImportExistingQuestionsComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatTableModule,
    MatCheckboxModule,
    MatMenuModule,
    MatSortModule
  ],
  exports : [
    FlyPanelImportExistingQuestionsComponent,
  ]
})
export class FlyPanelImportExistingQuestionsModule { }
