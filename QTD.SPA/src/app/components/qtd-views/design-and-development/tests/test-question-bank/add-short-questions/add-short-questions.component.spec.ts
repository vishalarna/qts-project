import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddShortQuestionsComponent } from './add-short-questions.component';

describe('AddShortQuestionsComponent', () => {
  let component: AddShortQuestionsComponent;
  let fixture: ComponentFixture<AddShortQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddShortQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddShortQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
