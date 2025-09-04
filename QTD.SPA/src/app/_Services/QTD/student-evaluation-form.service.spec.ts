import { TestBed } from '@angular/core/testing';
import { StudentEvaluationFormService } from './student-evaluation-form.service';


describe('StudentEvaluationFormService', () => {
  let service: StudentEvaluationFormService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentEvaluationFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
