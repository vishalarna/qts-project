import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimScenariosWizardCriteriaComponent } from './sim-scenarios-wizard-criteria.component';

describe('SimScenariosWizardCriteriaComponent', () => {
  let component: SimScenariosWizardCriteriaComponent;
  let fixture: ComponentFixture<SimScenariosWizardCriteriaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimScenariosWizardCriteriaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimScenariosWizardCriteriaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
