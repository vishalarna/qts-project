import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DifSurveyCreateWizardComponent } from './dif-survey-create-wizard.component';

describe('DifSurveyCreateWizardComponent', () => {
  let component: DifSurveyCreateWizardComponent;
  let fixture: ComponentFixture<DifSurveyCreateWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DifSurveyCreateWizardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DifSurveyCreateWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
