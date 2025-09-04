import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskRequalReassignTaskRequalComponent } from './task-requal-reassign-task-requal.component';

describe('TaskRequalReassignTaskRequalComponent', () => {
  let component: TaskRequalReassignTaskRequalComponent;
  let fixture: ComponentFixture<TaskRequalReassignTaskRequalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskRequalReassignTaskRequalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskRequalReassignTaskRequalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
