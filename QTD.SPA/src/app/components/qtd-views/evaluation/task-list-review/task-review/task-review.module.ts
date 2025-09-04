import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LayoutModule } from '../../../layout/layout.module';
import { RouterModule, Routes } from '@angular/router';
import { BaseModule } from 'src/app/components/base/base.module';
import { MatLegacyMenuModule as MatMenuModule } from '@angular/material/legacy-menu';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';
import { TaskReviewComponent } from './task-review.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyRadioModule as MatRadioModule } from '@angular/material/legacy-radio';
import { FlypanelActionItemModule } from './flypanel-action-item/flypanel-action-item.module';
import { MatLegacyChipsModule as MatChipsModule } from '@angular/material/legacy-chips';

const routes: Routes = [
  {
    path: '',
    component: TaskReviewComponent,
  }
];

@NgModule({
  declarations: [TaskReviewComponent],
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    LayoutModule,
    RouterModule.forChild(routes),
    BaseModule,
    MatMenuModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatSelectModule,
    MatRadioModule,
    FlypanelActionItemModule,
    ReactiveFormsModule,
    MatChipsModule
  ],
  exports:[TaskReviewComponent]
})
export class TaskReviewModule { }
