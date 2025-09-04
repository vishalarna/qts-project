import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskEnablingObjectivesComponent } from './task-enabling-objectives.component';

describe('TaskEnablingObjectivesComponent', () => {
  let component: TaskEnablingObjectivesComponent;
  let fixture: ComponentFixture<TaskEnablingObjectivesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskEnablingObjectivesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskEnablingObjectivesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
