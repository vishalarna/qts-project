import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelEnablingObjectiveComponent } from './fly-panel-enabling-objective.component';

describe('FlyPanelEnablingObjectiveComponent', () => {
  let component: FlyPanelEnablingObjectiveComponent;
  let fixture: ComponentFixture<FlyPanelEnablingObjectiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelEnablingObjectiveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelEnablingObjectiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
