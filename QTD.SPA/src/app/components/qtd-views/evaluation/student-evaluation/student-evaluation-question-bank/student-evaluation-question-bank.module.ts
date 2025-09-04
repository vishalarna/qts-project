import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentEvaluationQuestionBankComponent } from './student-evaluation-question-bank.component';
import { RouterModule, Routes } from '@angular/router';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { FlyPanelAddQuestionModule } from './fly-panel-add-question/fly-panel-add-question.module';
import { MatToolbarModule } from '@angular/material/toolbar';
const routes: Routes = [
  {
    path: '',
    pathMatch:'full',
    component: StudentEvaluationQuestionBankComponent,
  }, 
];



@NgModule({
  declarations: [
    StudentEvaluationQuestionBankComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    BaseModule,
    LayoutModule,
    MatTableModule,
    MatMenuModule,
    RouterModule.forChild(routes),
    FlyPanelAddQuestionModule,
    MatToolbarModule
  ]
})
export class StudentEvaluationQuestionBankModule { }
