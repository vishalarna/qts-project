import { TestBed } from '@angular/core/testing';
import { SimulatorScenarioService } from './simulator-scenario.service';


describe('SimulatorScenarioService', () => {
  let service: SimulatorScenarioService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SimulatorScenarioService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
