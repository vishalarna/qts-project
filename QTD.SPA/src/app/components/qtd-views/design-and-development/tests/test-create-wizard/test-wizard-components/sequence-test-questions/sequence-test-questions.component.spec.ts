import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SequenceTestQuestionsComponent } from './sequence-test-questions.component';

describe('SequenceTestQuestionsComponent', () => {
  let component: SequenceTestQuestionsComponent;
  let fixture: ComponentFixture<SequenceTestQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SequenceTestQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SequenceTestQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
