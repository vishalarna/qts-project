import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimulatorScenariosWizardComponent } from './simulator-scenarios-wizard.component';

describe('SimulatorScenariosWizardComponent', () => {
  let component: SimulatorScenariosWizardComponent;
  let fixture: ComponentFixture<SimulatorScenariosWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimulatorScenariosWizardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimulatorScenariosWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
