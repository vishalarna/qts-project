import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskProceduresComponent } from './task-procedures.component';

describe('TaskProceduresComponent', () => {
  let component: TaskProceduresComponent;
  let fixture: ComponentFixture<TaskProceduresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskProceduresComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskProceduresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
