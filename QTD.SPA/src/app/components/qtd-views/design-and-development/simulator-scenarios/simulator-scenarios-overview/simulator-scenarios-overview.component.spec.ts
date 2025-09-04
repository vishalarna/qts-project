import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimulatorScenariosOverviewComponent } from './simulator-scenarios-overview.component';

describe('SimulatorScenariosOverviewComponent', () => {
  let component: SimulatorScenariosOverviewComponent;
  let fixture: ComponentFixture<SimulatorScenariosOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SimulatorScenariosOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SimulatorScenariosOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
