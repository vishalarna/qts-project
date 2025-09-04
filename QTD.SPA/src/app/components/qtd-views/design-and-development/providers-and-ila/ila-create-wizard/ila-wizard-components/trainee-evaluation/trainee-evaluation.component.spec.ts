import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TraineeEvaluationComponent } from './trainee-evaluation.component';

describe('TraineeEvaluationComponent', () => {
  let component: TraineeEvaluationComponent;
  let fixture: ComponentFixture<TraineeEvaluationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TraineeEvaluationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TraineeEvaluationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
