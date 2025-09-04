import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelPreviewTestQuestionsComponent } from './flypanel-preview-test-questions.component';

describe('FlypanelPreviewTestQuestionsComponent', () => {
  let component: FlypanelPreviewTestQuestionsComponent;
  let fixture: ComponentFixture<FlypanelPreviewTestQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelPreviewTestQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelPreviewTestQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
