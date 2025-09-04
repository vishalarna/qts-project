import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcedureRegRequirementComponent } from './procedure-reg-requirement.component';

describe('ProcedureRegRequirementComponent', () => {
  let component: ProcedureRegRequirementComponent;
  let fixture: ComponentFixture<ProcedureRegRequirementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcedureRegRequirementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcedureRegRequirementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
