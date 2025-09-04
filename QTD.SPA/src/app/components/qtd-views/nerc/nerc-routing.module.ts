import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NercComponent } from './nerc.component';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { CehuploadComponent } from './cehupload/cehupload.component';


const routes: Routes = [
  {
    path: '',
    component: NercComponent,
    children: [
      {
        path: 'ceh-upload',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () => import('./cehupload/cehupload.module').then((m) => m.CehuploadModule)
      },
      {
        path: '',
        canActivate: [AuthGuard, RouteGuard],
        loadChildren: () => import('./ceh-upload-export/ceh-upload-export.module').then((m) => m.CehUploadExportModule)
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NercRoutingModule { }
