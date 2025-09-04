import { BaseModule } from 'src/app/components/base/base.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelStartProcedureReviewComponent } from './fly-panel-start-procedure-review.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule, Routes } from '@angular/router';
import { LayoutModule } from '../../../layout/layout.module';


const routes: Routes = [
  {
    path: '',
    component: FlyPanelStartProcedureReviewComponent,
  },
  {
    path: ':procedureReviewId',
    component: FlyPanelStartProcedureReviewComponent,
  },
];
@NgModule({
  declarations: [
    FlyPanelStartProcedureReviewComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CommonModule,
    BaseModule,
    LayoutModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatIconModule,
    MatRadioModule,
    MatChipsModule,
  ]
})
export class FlyPanelStartProcedureReviewModule { }
