import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskListReviewComponent } from './task-list-review.component';

describe('TaskListReviewComponent', () => {
  let component: TaskListReviewComponent;
  let fixture: ComponentFixture<TaskListReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskListReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
