import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMultipleCorrectAnswerComponent } from './add-multiple-correct-answer.component';

describe('AddMultipleCorrectAnswerComponent', () => {
  let component: AddMultipleCorrectAnswerComponent;
  let fixture: ComponentFixture<AddMultipleCorrectAnswerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddMultipleCorrectAnswerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMultipleCorrectAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
