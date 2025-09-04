import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlyPanelFilterTaskListReviewComponent } from './fly-panel-filter-task-list-review.component';
import { BaseModule } from 'src/app/components/base/base.module';
import { LayoutModule } from '../../../layout/layout.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyCheckboxModule as MatCheckboxModule } from '@angular/material/legacy-checkbox';


@NgModule({
  declarations: [FlyPanelFilterTaskListReviewComponent],
  imports: [
    CommonModule,
    BaseModule,
    LayoutModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatIconModule,
    MatCheckboxModule
  ],
  exports:[FlyPanelFilterTaskListReviewComponent]
})
export class FlyPanelFilterTaskListReviewModule { }
