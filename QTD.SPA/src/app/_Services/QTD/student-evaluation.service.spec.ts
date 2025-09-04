import { TestBed } from '@angular/core/testing';

import { StudentEvaluationService } from './student-evaluation.service';

describe('StudentEvaluationService', () => {
  let service: StudentEvaluationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentEvaluationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
