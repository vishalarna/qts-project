import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TrainingMapComponent } from './training-map.component';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';

const routes: Routes = [
  {
    path: '',
    component: TrainingMapComponent,
    children: [
      {
        path: '',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./training-map-landing/training-map-landing.module').then(
            (m) => m.TrainingMapLandingModule
          ),
      },
      {
        path: 'create',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./create-training-map/create-training-map.module').then(
            (m) => m.CreateTrainingMapModule
          ),
      },
      {
        path: 'design',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./training-map-design/training-map-design.module').then(
            (m) => m.TrainingMapDesignModule
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [TrainingMapComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
  ],
})
export class TrainingMapModule {}
