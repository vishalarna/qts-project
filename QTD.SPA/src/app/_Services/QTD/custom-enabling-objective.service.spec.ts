import { TestBed } from '@angular/core/testing';

import { CustomEnablingObjectiveService } from './custom-enabling-objective.service';

describe('CustomEnablingObjectiveService', () => {
  let service: CustomEnablingObjectiveService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomEnablingObjectiveService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
