import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelQuestionAndAnswerOperationComponent } from './flypanel-question-and-answer-operation.component';

describe('FlypanelQuestionAndAnswerOperationComponent', () => {
  let component: FlypanelQuestionAndAnswerOperationComponent;
  let fixture: ComponentFixture<FlypanelQuestionAndAnswerOperationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelQuestionAndAnswerOperationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelQuestionAndAnswerOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
