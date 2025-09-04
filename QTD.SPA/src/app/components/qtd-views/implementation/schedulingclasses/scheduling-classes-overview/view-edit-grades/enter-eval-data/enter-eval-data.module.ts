import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnterEvalDataComponent } from './enter-eval-data.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { RouterModule, Routes } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    pathMatch:'full',
    component: EnterEvalDataComponent,
  },
];

@NgModule({
  declarations: [
    EnterEvalDataComponent
  ],
  imports: [
    CommonModule,
    BaseModule,
    RouterModule.forChild(routes),
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  exports: [
    EnterEvalDataComponent,
  ]
})
export class EnterEvalDataModule { }
