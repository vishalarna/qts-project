import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAndSequenceTestQuestionsComponent } from './add-and-sequence-test-questions.component';

describe('AddAndSequenceTestQuestionsComponent', () => {
  let component: AddAndSequenceTestQuestionsComponent;
  let fixture: ComponentFixture<AddAndSequenceTestQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddAndSequenceTestQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddAndSequenceTestQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
