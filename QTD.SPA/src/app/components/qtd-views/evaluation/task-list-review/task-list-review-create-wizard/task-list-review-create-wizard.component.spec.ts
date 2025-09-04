import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskListReviewCreateWizardComponent } from './task-list-review-create-wizard.component';

describe('TaskListReviewCreateWizardComponent', () => {
  let component: TaskListReviewCreateWizardComponent;
  let fixture: ComponentFixture<TaskListReviewCreateWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskListReviewCreateWizardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskListReviewCreateWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
