import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimulatorScenariosComponent } from './simulator-scenarios.component';

describe('SimulatorScenariosComponent', () => {
  let component: SimulatorScenariosComponent;
  let fixture: ComponentFixture<SimulatorScenariosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimulatorScenariosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimulatorScenariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
