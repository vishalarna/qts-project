import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskRequalificationOverviewComponent } from './task-requalification-overview.component';

describe('TaskRequalificationOverviewComponent', () => {
  let component: TaskRequalificationOverviewComponent;
  let fixture: ComponentFixture<TaskRequalificationOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskRequalificationOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskRequalificationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
