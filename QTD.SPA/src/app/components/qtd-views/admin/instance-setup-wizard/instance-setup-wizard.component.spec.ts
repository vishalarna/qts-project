import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InstanceSetupWizardComponent } from './instance-setup-wizard.component';

describe('InstanceSetupWizardComponent', () => {
  let component: InstanceSetupWizardComponent;
  let fixture: ComponentFixture<InstanceSetupWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InstanceSetupWizardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InstanceSetupWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
