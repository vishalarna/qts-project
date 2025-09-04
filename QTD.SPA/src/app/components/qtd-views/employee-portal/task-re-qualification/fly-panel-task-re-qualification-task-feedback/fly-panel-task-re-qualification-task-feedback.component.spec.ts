import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskReQualificationTaskFeedbackComponent } from './fly-panel-task-re-qualification-task-feedback.component';

describe('FlyPanelTaskReQualificationTaskFeedbackComponent', () => {
  let component: FlyPanelTaskReQualificationTaskFeedbackComponent;
  let fixture: ComponentFixture<FlyPanelTaskReQualificationTaskFeedbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskReQualificationTaskFeedbackComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskReQualificationTaskFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
