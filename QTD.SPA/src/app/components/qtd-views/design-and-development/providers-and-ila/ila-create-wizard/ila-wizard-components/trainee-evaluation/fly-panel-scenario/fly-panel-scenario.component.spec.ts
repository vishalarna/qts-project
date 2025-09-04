import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelScenarioComponent } from './fly-panel-scenario.component';

describe('FlyPanelScenarioComponent', () => {
  let component: FlyPanelScenarioComponent;
  let fixture: ComponentFixture<FlyPanelScenarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelScenarioComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelScenarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
