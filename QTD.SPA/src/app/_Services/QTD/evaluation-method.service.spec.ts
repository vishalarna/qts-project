import { TestBed } from '@angular/core/testing';

import { EvaluationMethodService } from './evaluation-method.service';

describe('EvaluationMethodService', () => {
  let service: EvaluationMethodService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EvaluationMethodService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
