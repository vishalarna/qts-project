import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentEvaluationQuestionBankComponent } from './student-evaluation-question-bank.component';

describe('StudentEvaluationQuestionBankComponent', () => {
  let component: StudentEvaluationQuestionBankComponent;
  let fixture: ComponentFixture<StudentEvaluationQuestionBankComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentEvaluationQuestionBankComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentEvaluationQuestionBankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
