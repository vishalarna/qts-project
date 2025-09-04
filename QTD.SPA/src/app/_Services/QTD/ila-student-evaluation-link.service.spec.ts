import { TestBed } from '@angular/core/testing';
import { IlaStudentEvaluationLinkService } from './ila-student-evaluation-link.service';

describe('IlaStudentEvaluationLinkService', () => {
  let service: IlaStudentEvaluationLinkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IlaStudentEvaluationLinkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
