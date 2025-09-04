import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskRequalificationComponent } from './task-requalification.component';

describe('TaskRequalificationComponent', () => {
  let component: TaskRequalificationComponent;
  let fixture: ComponentFixture<TaskRequalificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskRequalificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskRequalificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
