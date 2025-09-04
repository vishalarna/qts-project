import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestQuestionBankComponent } from './test-question-bank.component';

describe('TestQuestionBankComponent', () => {
  let component: TestQuestionBankComponent;
  let fixture: ComponentFixture<TestQuestionBankComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestQuestionBankComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TestQuestionBankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
