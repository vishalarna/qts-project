import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { FormsModule } from '@angular/forms';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatLegacyPaginatorModule as MatPaginatorModule } from '@angular/material/legacy-paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { DifSurveyCreateWizardComponent } from './dif-survey-create-wizard.component';
import { DifCreateSurveyModule } from './dif-create-survey/dif-create-survey.module';
import { MatStepperModule } from '@angular/material/stepper';
import { DifAssignTaskModule } from './dif-assign-task/dif-assign-task.module';
import { DifAssignEmployeesModule } from './dif-assign-employees/dif-assign-employees.module';
import { DifReviewAndPublishModule } from './dif-review-and-publish/dif-review-and-publish.module';
import { MatToolbarModule } from '@angular/material/toolbar';


const routes: Routes = [
  {
    path: '',
    component: DifSurveyCreateWizardComponent,
  },
];

@NgModule({
  declarations: [
    DifSurveyCreateWizardComponent,   
  ],
  imports: [
    CommonModule,
    LayoutModule,
    BaseModule,
    RouterModule.forChild(routes),
    LayoutModule,
    FormsModule,    
    BaseModule,
    LayoutModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatMenuModule,
    MatCheckboxModule,
    DifCreateSurveyModule,
    MatStepperModule,
    DifAssignTaskModule,
    DifAssignEmployeesModule,
    DifReviewAndPublishModule,
    MatToolbarModule
  ],
  exports:[DifSurveyCreateWizardComponent]
})
export class DifSurveyCreateWizardModule { }
