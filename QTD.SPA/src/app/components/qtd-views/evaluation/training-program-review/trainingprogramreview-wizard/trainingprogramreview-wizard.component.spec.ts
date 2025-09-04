import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingProgramReviewWizardComponent } from './trainingprogramreview-wizard.component';

describe('TrainingProgramReviewWizardComponent', () => {
  let component: TrainingProgramReviewWizardComponent;
  let fixture: ComponentFixture<TrainingProgramReviewWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingProgramReviewWizardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingProgramReviewWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
