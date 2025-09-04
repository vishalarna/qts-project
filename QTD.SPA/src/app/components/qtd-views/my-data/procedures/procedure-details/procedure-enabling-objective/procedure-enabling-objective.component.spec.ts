import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcedureEnablingObjectiveComponent } from './procedure-enabling-objective.component';

describe('ProcedureEnablingObjectiveComponent', () => {
  let component: ProcedureEnablingObjectiveComponent;
  let fixture: ComponentFixture<ProcedureEnablingObjectiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcedureEnablingObjectiveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcedureEnablingObjectiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
