import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimScenariosWizardIlaComponent } from './sim-scenarios-wizard-ila.component';

describe('SimScenariosWizardIlaComponent', () => {
  let component: SimScenariosWizardIlaComponent;
  let fixture: ComponentFixture<SimScenariosWizardIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimScenariosWizardIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimScenariosWizardIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
