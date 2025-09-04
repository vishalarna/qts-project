import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlyPanelFilterSimulatorScenariosComponent } from './fly-panel-filter-simulator-scenarios.component';

describe('FlyPanelFilterSimulatorScenariosComponent', () => {
  let component: FlyPanelFilterSimulatorScenariosComponent;
  let fixture: ComponentFixture<FlyPanelFilterSimulatorScenariosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlyPanelFilterSimulatorScenariosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlyPanelFilterSimulatorScenariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
