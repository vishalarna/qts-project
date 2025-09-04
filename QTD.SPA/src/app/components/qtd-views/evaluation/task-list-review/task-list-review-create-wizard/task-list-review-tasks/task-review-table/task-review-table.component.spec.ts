import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskReviewTableComponent } from './task-review-table.component';

describe('TaskReviewTableComponent', () => {
  let component: TaskReviewTableComponent;
  let fixture: ComponentFixture<TaskReviewTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskReviewTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskReviewTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
