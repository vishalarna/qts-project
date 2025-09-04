import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimScenariosWizardLinkagesComponent } from './sim-scenarios-wizard-linkages.component';

describe('SimScenariosWizardLinkagesComponent', () => {
  let component: SimScenariosWizardLinkagesComponent;
  let fixture: ComponentFixture<SimScenariosWizardLinkagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimScenariosWizardLinkagesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimScenariosWizardLinkagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
