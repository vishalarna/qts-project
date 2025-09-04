import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { EmpComponent } from './emp.component';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { LocalizeModule } from 'src/app/_Shared/modules/localize.module';
import { DeleteEmpComponent } from './delete-emp/delete-emp.component';
import { AddEmpComponent } from './add-emp/add-emp.component';
import { ApiEmpsComponent } from './api-emps/api-emps.component';
import { FormsModule } from '@angular/forms';
import { FlyPanelEmpPositionComponent } from './emp-position/fly-panel-emp-position/fly-panel-emp-position.component';
import { FlyPanelEmpCertificationComponent } from './emp-certification/fly-panel-emp-certification/fly-panel-emp-certification.component';

import { MatLegacyDialogModule as MatDialogModule } from '@angular/material/legacy-dialog';
import { BaseModule } from 'src/app/components/base/base.module';
import { DeleteEmpPositionComponent } from './emp-position/delete-emp-position/delete-emp-position.component';
import { DeleteEmpCertificationComponent } from './emp-certification/delete-emp-certification/delete-emp-certification.component';
import { DeleteEmpDialogueComponent } from './delete-emp-dialogue/delete-emp-dialogue.component';
import { FlyPanelRenewCertificationComponent } from './fly-panel-renew-certification/fly-panel-renew-certification.component';
import { ActiveInactiveDialogueComponent } from './active-inactive-dialogue/active-inactive-dialogue.component';
const routes: Routes = [
  {
    path: '',
    component: EmpComponent,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./list-emps/list-emps.module').then((m) => m.ListEmpsModule),
      },
      {
        path: 'edit',
        loadChildren: () =>
          import('./edit-view-emp/edit-view-emp.module').then(
            (m) => m.EditViewEmpModule
          ),
      },
      {
        path: 'view',
        loadChildren: () =>
          import('./edit-view-emp/edit-view-emp.module').then(
            (m) => m.EditViewEmpModule
          ),
      },
      {
        path: 'upload-csv',
        loadChildren: () =>
          import('./upload-emps/upload-emps.module').then(
            (m) => m.UploadEmpsModule
          ),
      },
      {
        path: 'api',
        loadChildren: () =>
          import('./api-emps/api-emps.module').then((m) => m.ApiEmpsModule),
      },
      {
        path: 'idp',
        loadChildren: () =>
          import('./idp/idp.module').then((m) => m.IDPModule),
      },
    ],
  },
];

@NgModule({
  declarations: [
    EmpComponent,
    DeleteEmpComponent,
    DeleteEmpPositionComponent,
    DeleteEmpCertificationComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forChild(routes),
    LocalizeModule,
    MatDialogModule,
    BaseModule,
  ],
  providers: [DatePipe],
})
export class EmpModule {}
