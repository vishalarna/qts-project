import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskListReviewTasksComponent } from './task-list-review-tasks.component';

describe('TaskListReviewTasksComponent', () => {
  let component: TaskListReviewTasksComponent;
  let fixture: ComponentFixture<TaskListReviewTasksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskListReviewTasksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListReviewTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
