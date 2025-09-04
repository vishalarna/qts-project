import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelFilterTaskListReviewTasksComponent } from './flypanel-filter-task-list-review-tasks.component';

describe('FlypanelFilterTaskListReviewTasksComponent', () => {
  let component: FlypanelFilterTaskListReviewTasksComponent;
  let fixture: ComponentFixture<FlypanelFilterTaskListReviewTasksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelFilterTaskListReviewTasksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelFilterTaskListReviewTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
