import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import {DataExchangeComponent} from "./data-exchange.component";
import {DatabaseSettingsComponent} from "./database-settings/database-settings.component";

const routes: Routes = [
  {
    path: '',
    component: DataExchangeComponent,
    children: [
      {
        path: 'database',
        canActivate: [AuthGuard, RouteGuard],
        component: DatabaseSettingsComponent
      },
      {
        path: 'import',
        canActivate: [AuthGuard, RouteGuard],
        children:
        [
          {
            path: '',
            canActivate: [AuthGuard, RouteGuard],
            loadChildren: () =>
              import('./data-import/data-import.module').then(
                (m) => m.DataImportModule
              ),
          }
        ]
      }
    ],
  },
];

@NgModule({ imports: [RouterModule.forChild(routes)], exports: [RouterModule] })
export class DataExchangeRoutingModule {}
