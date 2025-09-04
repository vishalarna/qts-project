import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingIssuesCreateWizardComponent } from './training-issues-create-wizard.component';

describe('TrainingIssuesCreateWizardComponent', () => {
  let component: TrainingIssuesCreateWizardComponent;
  let fixture: ComponentFixture<TrainingIssuesCreateWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingIssuesCreateWizardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingIssuesCreateWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
