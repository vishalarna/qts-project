import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelLinkScenarioObjectiveComponent } from './fly-panel-link-scenario-objective.component';

describe('FlyPanelLinkScenarioObjectiveComponent', () => {
  let component: FlyPanelLinkScenarioObjectiveComponent;
  let fixture: ComponentFixture<FlyPanelLinkScenarioObjectiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelLinkScenarioObjectiveComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelLinkScenarioObjectiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
