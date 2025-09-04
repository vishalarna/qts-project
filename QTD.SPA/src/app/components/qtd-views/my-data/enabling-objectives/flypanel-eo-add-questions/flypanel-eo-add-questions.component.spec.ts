import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoAddQuestionsComponent } from './flypanel-eo-add-questions.component';

describe('FlypanelEoAddQuestionsComponent', () => {
  let component: FlypanelEoAddQuestionsComponent;
  let fixture: ComponentFixture<FlypanelEoAddQuestionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoAddQuestionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoAddQuestionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
