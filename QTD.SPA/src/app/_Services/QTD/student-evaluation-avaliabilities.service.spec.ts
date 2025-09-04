import { TestBed } from '@angular/core/testing';
import { StudentEvaluationAvaliabilitiesService } from './student-evaluation-avaliabilities.service';


describe('StudentEvaluationAvaliabilitiesService', () => {
  let service: StudentEvaluationAvaliabilitiesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentEvaluationAvaliabilitiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
