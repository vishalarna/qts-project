import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShEnablingObjectiveComponent } from './sh-enabling-objective.component';

describe('ShEnablingObjectiveComponent', () => {
  let component: ShEnablingObjectiveComponent;
  let fixture: ComponentFixture<ShEnablingObjectiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShEnablingObjectiveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShEnablingObjectiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
