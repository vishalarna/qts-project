import { TestBed } from '@angular/core/testing';
import { StudentEvaluationQuestionService } from './student-evaluation-question.service';

describe('StudentEvaluationQuestionService', () => {
  let service: StudentEvaluationQuestionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StudentEvaluationQuestionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
