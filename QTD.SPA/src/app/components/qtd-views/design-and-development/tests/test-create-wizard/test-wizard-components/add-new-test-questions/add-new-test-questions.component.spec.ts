import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewTestQuestionsComponent } from './add-new-test-questions.component';

describe('AddNewTestQuestionsComponent', () => {
  let component: AddNewTestQuestionsComponent;
  let fixture: ComponentFixture<AddNewTestQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddNewTestQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNewTestQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
