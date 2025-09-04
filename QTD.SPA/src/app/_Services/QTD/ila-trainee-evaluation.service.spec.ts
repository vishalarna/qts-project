import { TestBed } from '@angular/core/testing';

import { IlaTraineeEvaluationService } from './ila-trainee-evaluation.service';

describe('IlaTraineeEvaluationService', () => {
  let service: IlaTraineeEvaluationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IlaTraineeEvaluationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
