import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskSafetyHazardsComponent } from './task-safety-hazards.component';

describe('TaskSafetyHazardsComponent', () => {
  let component: TaskSafetyHazardsComponent;
  let fixture: ComponentFixture<TaskSafetyHazardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskSafetyHazardsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskSafetyHazardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
