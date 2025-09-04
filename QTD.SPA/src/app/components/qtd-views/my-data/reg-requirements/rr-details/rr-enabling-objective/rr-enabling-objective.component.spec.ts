import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RREnablingObjectiveComponent } from './rr-enabling-objective.component';

describe('RREnablingObjectiveComponent', () => {
  let component: RREnablingObjectiveComponent;
  let fixture: ComponentFixture<RREnablingObjectiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RREnablingObjectiveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RREnablingObjectiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
