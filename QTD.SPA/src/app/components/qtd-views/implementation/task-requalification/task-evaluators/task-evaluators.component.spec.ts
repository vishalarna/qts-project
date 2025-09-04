import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskEvaluatorsComponent } from './task-evaluators.component';

describe('TaskEvaluatorsComponent', () => {
  let component: TaskEvaluatorsComponent;
  let fixture: ComponentFixture<TaskEvaluatorsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskEvaluatorsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskEvaluatorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
