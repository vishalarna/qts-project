import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimScenariosWizardDetailsComponent } from './sim-scenarios-wizard-details.component';

describe('SimScenariosWizardDetailsComponent', () => {
  let component: SimScenariosWizardDetailsComponent;
  let fixture: ComponentFixture<SimScenariosWizardDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimScenariosWizardDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimScenariosWizardDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
