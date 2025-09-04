import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelEoTestQuestionLinkComponent } from './flypanel-eo-test-question-link.component';

describe('FlypanelEoTestQuestionLinkComponent', () => {
  let component: FlypanelEoTestQuestionLinkComponent;
  let fixture: ComponentFixture<FlypanelEoTestQuestionLinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelEoTestQuestionLinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelEoTestQuestionLinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
