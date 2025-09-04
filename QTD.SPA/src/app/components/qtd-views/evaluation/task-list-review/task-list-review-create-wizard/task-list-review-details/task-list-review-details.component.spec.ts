import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskListReviewDetailsComponent } from './task-list-review-details.component';

describe('TaskListReviewDetailsComponent', () => {
  let component: TaskListReviewDetailsComponent;
  let fixture: ComponentFixture<TaskListReviewDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskListReviewDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListReviewDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
