import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingMapLandingComponent } from './training-map-landing.component';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatGridListModule } from '@angular/material/grid-list';
import { LayoutModule } from '../../../layout/layout.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatDividerModule } from '@angular/material/divider';
const routes: Routes = [
  {
    path: '',
    component: TrainingMapLandingComponent,
  },
];

@NgModule({
  declarations: [TrainingMapLandingComponent],
  imports: [
    CommonModule,
    LocalizeModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatCardModule,
    MatGridListModule,
    LayoutModule,
    MatMenuModule,
    MatDividerModule,
  ],
})
export class TrainingMapLandingModule {}
