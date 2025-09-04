import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyDataComponent } from './my-data.component';
import { RouterModule, Routes } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';

const routes: Routes = [
  {
    path: '',
    component: MyDataComponent,
    children: [
      {
        path: 'procedures',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./procedures/procedures.module').then(
            (m) => m.ProceduresModule
          ),
      },
      {
        path: 'reg-requirements',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./reg-requirements/reg-requirements.module').then(
            (m) => m.RegRequirementsModule
          ),
      },
      {
        path: 'safety-hazards',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./safety-hazards/safety-hazards.module').then(
            (m) => m.SafetyHazardsModule
          ),
      },
      {
        path: 'instructors',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./instructors/instructors.module').then(
            (m) => m.InstructorsModule
          ),
      },
      {
        path: 'locations',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./locations/locations.module').then(
            (m) => m.LocationsModule
          ),
      },
      {
        path: 'certifications',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./certifications/certifications.module').then(
            (m) => m.CertificationsModule
            
          ),
      },
      {
        path: 'definitions',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./definitions/definitions.module').then(
            (m) => m.DefinitionsModule
          ),
      },
      {
        path: 'tools',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./tools/tools.module').then((m) => m.ToolsModule),
      },
      {
        path: 'tasks',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () =>
          import('./tasks/tasks.module').then((m) => m.TasksModule),
      },
      {
        path:'enabling-objectives',
        canActivate:[AuthGuard,RouteGuard],
        loadChildren: ()=>
          import('./enabling-objectives/enabling-objectives.module').then((m)=>m.EnablingObjectivesModule),
      }
    ],
  },
  {
    path: 'positions',
    canActivate: [AuthGuard, RouteGuard],
    loadChildren: () =>
      import('./positions/positions.module').then(
        (m) => m.PositionsModule
      ),
  },
  {
    path: 'bulkedit',
    canActivate: [AuthGuard, RouteGuard],
    loadChildren: () =>
      import('./bulk-edit/bulk-edit.module').then((m) => m.BulkEditModule),
  },
  {
    path: 'position-bulkedit',
    canActivate: [AuthGuard, RouteGuard],
    loadChildren: () =>
      import('./position-bulk-edit/position-bulk-edit.module').then(
        (m) => m.PositionBulkEditModule
      ),
  },

  {
    path: 'tool-bulkedit',
    canActivate: [AuthGuard, RouteGuard],
    loadChildren: () =>
      import('./tools/bulkedit-tool/bulkedit-tool.module').then(
        (m) => m.BulkeditToolModule
      ),
  },
];

@NgModule({
  declarations: [MyDataComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    LocalizeModule,
  ],
})
export class MyDataModule {}
