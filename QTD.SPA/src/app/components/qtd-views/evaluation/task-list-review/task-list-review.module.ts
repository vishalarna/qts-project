import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListReviewRoutingModule } from './task-list-review-routing.module';
import { TaskListReviewComponent } from './task-list-review.component';

@NgModule({
  declarations: [TaskListReviewComponent],
  imports: [
    CommonModule,
    TaskListReviewRoutingModule
  ]
})
export class TaskListReviewModule { }
