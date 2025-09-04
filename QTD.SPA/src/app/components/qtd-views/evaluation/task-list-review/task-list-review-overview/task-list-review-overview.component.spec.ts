import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskListReviewOverviewComponent } from './task-list-review-overview.component';

describe('TaskListReviewOverviewComponent', () => {
  let component: TaskListReviewOverviewComponent;
  let fixture: ComponentFixture<TaskListReviewOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskListReviewOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListReviewOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
