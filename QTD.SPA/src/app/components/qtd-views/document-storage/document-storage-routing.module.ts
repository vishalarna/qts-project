import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/_Guards/auth.guard';
import { RouteGuard } from 'src/app/_Guards/route.guard';
import { DocumentStorageViewComponent } from './document-storage-view/document-storage-view.component';
import { DocumentStorageComponent } from './document-storage.component';
const routes: Routes = [
  {
    path: '',
    component: DocumentStorageComponent,
    children: [
      {
        path: '',
        canActivate: [AuthGuard, RouteGuard],
        component: DocumentStorageViewComponent
      },
    ],
  },
];

@NgModule({ imports: [RouterModule.forChild(routes)], exports: [RouterModule] })
export class DocumentStorageRoutingModule {}
