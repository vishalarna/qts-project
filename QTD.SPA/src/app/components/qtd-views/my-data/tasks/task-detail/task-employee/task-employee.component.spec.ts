import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskEmployeeComponent } from './task-employee.component';

describe('TaskEmployeeComponent', () => {
  let component: TaskEmployeeComponent;
  let fixture: ComponentFixture<TaskEmployeeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskEmployeeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
