import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskRecentActivityComponent } from './task-recent-activity.component';

describe('TaskRecentActivityComponent', () => {
  let component: TaskRecentActivityComponent;
  let fixture: ComponentFixture<TaskRecentActivityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskRecentActivityComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskRecentActivityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
