import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SchedulingclassesComponent } from './schedulingclasses.component';
import { RouterModule, Routes } from '@angular/router';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { AuthGuard } from 'src/app/_Guards/auth.guard';


const routes: Routes = [
  {
    path: '',
    component: SchedulingclassesComponent,
    children: [
      {

        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/scheduling-classes-overview.module').then(
            (m) => m.SchedulingClassesOverviewModule
          ),
      },
      {

        path: '',
        redirectTo:'overview',
        pathMatch:'full'
      },
      {

        path: 'addTraining',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/add-new-training/add-new-training.module').then(
            (m) => m.AddNewTrainingModule
          ),
      },
      {

        path: 'editTraining',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/add-new-training/add-new-training.module').then(
            (m) => m.AddNewTrainingModule
          ),
      },

      {

        path: 'addTraining',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/add-new-training/add-new-training.module').then(
            (m) => m.AddNewTrainingModule
          ),
      },

      {

        path: 'grades/:instructorId',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/view-edit-grades/view-edit-grades.module').then(
            (m) => m.ViewEditGradesModule
          ),
      },

      {

        path: 'self-registration',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/fly-panel-view-to-dos/fly-panel-view-to-dos.module').then(
            (m) => m.FlyPanelViewToDosModule
          ),
      },
      {

        path: 're-release',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/fly-panel-view-to-dos/fly-panel-view-to-dos.module').then(
            (m) => m.FlyPanelViewToDosModule
          ),
      },
      {

        path: 'retake',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/fly-panel-view-to-dos/fly-panel-view-to-dos.module').then(
            (m) => m.FlyPanelViewToDosModule
          ),
      },
      {

        path: 'ila-classes',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/fly-panel-view-to-dos/fly-panel-view-to-dos.module').then(
            (m) => m.FlyPanelViewToDosModule
          ),
      },
      {

        path: 'waitlist',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/fly-panel-view-to-dos/fly-panel-view-to-dos.module').then(
            (m) => m.FlyPanelViewToDosModule
          ),
      },
      {
        path: 'eval/:id/type/:type/class/:classId/employee/:empId',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./scheduling-classes-overview/view-edit-grades/enter-eval-data/enter-eval-data.module').then(
            (m) => m.EnterEvalDataModule
          ),
      },
    ],
  },
];

@NgModule({
  declarations: [
    SchedulingclassesComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class SchedulingclassesModule { }
