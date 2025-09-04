import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskQualificationComponent } from './task-qualification.component';

describe('TaskQualificationComponent', () => {
  let component: TaskQualificationComponent;
  let fixture: ComponentFixture<TaskQualificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskQualificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskQualificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
