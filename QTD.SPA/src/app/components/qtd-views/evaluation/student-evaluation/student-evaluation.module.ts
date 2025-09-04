import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentEvaluationComponent } from './student-evaluation.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { LayoutModule } from '../../layout/layout.module';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';

const routes: Routes = [
  {
    path: '',
    component: StudentEvaluationComponent,
    children: [
      {
        
        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./student-evaluation-overview/student-evaluation-overview.module').then(
            (m) => m.StudentEvaluationOverviewModule
          ),
      },
      {
        
        path: '',
        redirectTo:'overview',
        pathMatch:'full'
      },
      {
        path: 'questionBank',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./student-evaluation-question-bank/student-evaluation-question-bank.module').then(
            (m) => m.StudentEvaluationQuestionBankModule
          ),
      },
      {
        path: 'create',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./add-new-student-evaluation/add-new-student-evaluation.module').then(
            (m) => m.AddNewStudentEvaluationModule
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [
    StudentEvaluationComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    DragDropModule,
    LocalizeModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatSelectModule,
    CommonModule,
    MatSidenavModule,
    LayoutModule

  ]
})
export class StudentEvaluationModule { }
