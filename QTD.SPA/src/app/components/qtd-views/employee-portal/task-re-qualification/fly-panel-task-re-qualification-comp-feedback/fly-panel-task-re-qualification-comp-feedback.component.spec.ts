import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelTaskReQualificationCompFeedbackComponent } from './fly-panel-task-re-qualification-comp-feedback.component';

describe('FlyPanelTaskReQualificationCompFeedbackComponent', () => {
  let component: FlyPanelTaskReQualificationCompFeedbackComponent;
  let fixture: ComponentFixture<FlyPanelTaskReQualificationCompFeedbackComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelTaskReQualificationCompFeedbackComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelTaskReQualificationCompFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
