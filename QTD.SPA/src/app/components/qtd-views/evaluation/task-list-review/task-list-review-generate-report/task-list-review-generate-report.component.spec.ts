import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskListReviewGenerateReportComponent } from './TaskListReviewGenerateReportComponent';

describe('TaskListReviewGenerateReportComponent', () => {
  let component: TaskListReviewGenerateReportComponent;
  let fixture: ComponentFixture<TaskListReviewGenerateReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskListReviewGenerateReportComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListReviewGenerateReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
