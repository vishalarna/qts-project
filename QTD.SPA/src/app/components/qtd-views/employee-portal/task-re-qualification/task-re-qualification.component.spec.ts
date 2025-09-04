import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskReQualificationComponent } from './task-re-qualification.component';

describe('TaskReQualificationComponent', () => {
  let component: TaskReQualificationComponent;
  let fixture: ComponentFixture<TaskReQualificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskReQualificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskReQualificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
