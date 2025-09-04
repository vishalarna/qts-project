import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColloboratorSimulatorScenarioModalComponent } from './colloborator-simulator-scenario-modal.component';

describe('ColloboratorSimulatorScenarioModalComponent', () => {
  let component: ColloboratorSimulatorScenarioModalComponent;
  let fixture: ComponentFixture<ColloboratorSimulatorScenarioModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ColloboratorSimulatorScenarioModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ColloboratorSimulatorScenarioModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
