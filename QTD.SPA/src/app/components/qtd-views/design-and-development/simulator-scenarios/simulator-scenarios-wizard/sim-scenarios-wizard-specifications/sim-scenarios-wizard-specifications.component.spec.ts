import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimScenariosWizardSpecificationsComponent } from './sim-scenarios-wizard-specifications.component';

describe('SimScenariosWizardSpecificationsComponent', () => {
  let component: SimScenariosWizardSpecificationsComponent;
  let fixture: ComponentFixture<SimScenariosWizardSpecificationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimScenariosWizardSpecificationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimScenariosWizardSpecificationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
