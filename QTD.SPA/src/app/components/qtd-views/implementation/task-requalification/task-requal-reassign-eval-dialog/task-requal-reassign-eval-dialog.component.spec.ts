import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskRequalReassignEvalDialogComponent } from './task-requal-reassign-eval-dialog.component';

describe('TaskRequalReassignEvalDialogComponent', () => {
  let component: TaskRequalReassignEvalDialogComponent;
  let fixture: ComponentFixture<TaskRequalReassignEvalDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskRequalReassignEvalDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskRequalReassignEvalDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
