import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EoTestQuestionsComponent } from './eo-test-questions.component';

describe('EoTestQuestionsComponent', () => {
  let component: EoTestQuestionsComponent;
  let fixture: ComponentFixture<EoTestQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EoTestQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EoTestQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
