import { TestBed } from '@angular/core/testing';
import { StudentEvaluationAudiencesService } from './student-evaluation-audiences.service';


describe('StudentEvaluationAudiencesService', () => {
  let service: StudentEvaluationAudiencesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentEvaluationAudiencesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
