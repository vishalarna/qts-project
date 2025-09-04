import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelTestResultComponent } from './fly-panel-test-result.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { NgCircleProgressModule } from 'ng-circle-progress';
import { MatLegacyTooltipModule as MatTooltipModule } from '@angular/material/legacy-tooltip';

const routes: Routes = [
  {
    path: '',
    component: FlyPanelTestResultComponent,
  },
  {
    path: ':classId/:testId',
    component: FlyPanelTestResultComponent,
  },
];
@NgModule({
  declarations: [
    FlyPanelTestResultComponent
  ],
  imports: [
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
    MatToolbarModule,
    MatCheckboxModule,
    MatChipsModule,
    MatCardModule,
    MatTooltipModule,
    NgCircleProgressModule.forRoot({})
  ]
})
export class FlyPanelTestResultModule { }
