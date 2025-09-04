import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskReleaseToEmpComponent } from './task-release-to-emp.component';

describe('TaskReleaseToEmpComponent', () => {
  let component: TaskReleaseToEmpComponent;
  let fixture: ComponentFixture<TaskReleaseToEmpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskReleaseToEmpComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskReleaseToEmpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
