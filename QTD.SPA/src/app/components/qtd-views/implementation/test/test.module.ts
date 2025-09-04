import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestComponent } from './test.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { StartTestDialogModule } from './start-test-dialog/start-test-dialog.module';


const routes: Routes = [
  {
    path: '',
    component: TestComponent,
    children: [
      {

        path: 'overview',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./test-overview/test-overview.module').then(
            (m) => m.TestOverviewModule
          ),
      },
      {

        path: 'start-test',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./fly-panel-start-test/fly-panel-start-test.module').then(
            (m) => m.FlyPanelStartTestModule
          ),
      },
      {

        path: 'test-result',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./fly-panel-test-result/fly-panel-test-result.module').then(
            (m) => m.FlyPanelTestResultModule
          ),
      },
      {

        path: '',
        redirectTo:'overview',
        pathMatch:'full'
      },

    ],
  },
];
@NgModule({
  declarations: [
    TestComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    StartTestDialogModule
  ]
})
export class TestModule { }
