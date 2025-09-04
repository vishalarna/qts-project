import { TestBed } from '@angular/core/testing';

import { TraineeEvaluationService } from './trainee-evaluation.service';

describe('TraineeEvaluationService', () => {
  let service: TraineeEvaluationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TraineeEvaluationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
