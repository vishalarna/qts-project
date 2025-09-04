import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PreviewAndPublishComponent } from './preview-and-publish.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { RouterModule, Routes } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';

const routes: Routes = [
  {
    path: ':id',
    component: PreviewAndPublishComponent,
  },
  {
    path: ':id/:publish',
    component: PreviewAndPublishComponent,
  },
];

@NgModule({
  declarations: [
    PreviewAndPublishComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatIconModule,
    MatRadioModule,
    MatToolbarModule,
    MatCheckboxModule,
  ],
  exports : [
    PreviewAndPublishComponent,
  ]
})
export class PreviewAndPublishModule { }
