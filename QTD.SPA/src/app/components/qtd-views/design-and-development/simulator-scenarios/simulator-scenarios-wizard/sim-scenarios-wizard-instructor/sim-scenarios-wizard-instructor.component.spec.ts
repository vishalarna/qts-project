import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimScenariosWizardInstructorComponent } from './sim-scenarios-wizard-instructor.component';

describe('SimScenariosWizardInstructorComponent', () => {
  let component: SimScenariosWizardInstructorComponent;
  let fixture: ComponentFixture<SimScenariosWizardInstructorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimScenariosWizardInstructorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimScenariosWizardInstructorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
