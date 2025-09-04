import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { LayoutModule } from '@angular/cdk/layout';
import { FormsModule } from '@angular/forms';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BaseModule } from 'src/app/components/base/base.module';
import { SimulatorScenariosComponent } from './simulator-scenarios.component';
import { SimulatorScenariosWizardComponent } from './simulator-scenarios-wizard/simulator-scenarios-wizard.component';

 const routes: Routes = [
  {
    path: '',
    component: SimulatorScenariosComponent,
    children:
      [
        {
          path: '',
          redirectTo: 'overview',
          pathMatch: 'full',
        },
        {
          path: 'overview',
          loadChildren: () =>
            import('./simulator-scenarios-overview/simulator-scenarios-overview.module').then((m) => m.SimulatorScenariosOverviewModule),
        },
        {
          path: 'create',
          canActivate: [AuthGuard, RouteGuard],
          component: SimulatorScenariosWizardComponent,
          loadChildren: () =>
              import('./simulator-scenarios-wizard/simulator-scenarios-wizard.module').then(
                  (m) => m.SimulatorScenariosWizardModule
              )
      },
      {
          path: 'edit/:id',
          canActivate: [AuthGuard, RouteGuard],
          component: SimulatorScenariosWizardComponent,
          loadChildren: () =>
              import('./simulator-scenarios-wizard/simulator-scenarios-wizard.module').then(
                  (m) => m.SimulatorScenariosWizardModule
              )
      },
      {
          path: 'view/:id',
          canActivate: [AuthGuard, RouteGuard],
          component: SimulatorScenariosWizardComponent,
          loadChildren: () =>
              import('./simulator-scenarios-wizard/simulator-scenarios-wizard.module').then(
                  (m) => m.SimulatorScenariosWizardModule
              )
      },
      ]
  }
 ]

@NgModule({
  declarations: [SimulatorScenariosComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatSidenavModule,
    BaseModule,
    HttpClientModule,
    LayoutModule,
    FormsModule,
    MatMenuModule,
  ],
  
})
export class SimulatorScenariosModule { }
