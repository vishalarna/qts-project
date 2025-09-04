import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportTestQuestionsComponent } from './import-test-questions.component';

describe('ImportTestQuestionsComponent', () => {
  let component: ImportTestQuestionsComponent;
  let fixture: ComponentFixture<ImportTestQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportTestQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportTestQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
