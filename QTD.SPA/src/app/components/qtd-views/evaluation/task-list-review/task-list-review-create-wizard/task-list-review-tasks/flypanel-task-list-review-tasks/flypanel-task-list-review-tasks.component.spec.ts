import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelTaskListReviewTasksComponent } from './flypanel-task-list-review-tasks.component';

describe('FlypanelTaskListReviewTasksComponent', () => {
  let component: FlypanelTaskListReviewTasksComponent;
  let fixture: ComponentFixture<FlypanelTaskListReviewTasksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelTaskListReviewTasksComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelTaskListReviewTasksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
