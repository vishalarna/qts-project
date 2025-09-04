import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimScenariosWizardEventsAndScriptsComponent } from './sim-scenarios-wizard-events-and-scripts.component';

describe('SimScenariosWizardEventsAndScriptsComponent', () => {
  let component: SimScenariosWizardEventsAndScriptsComponent;
  let fixture: ComponentFixture<SimScenariosWizardEventsAndScriptsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimScenariosWizardEventsAndScriptsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimScenariosWizardEventsAndScriptsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
