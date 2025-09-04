import { TestBed } from '@angular/core/testing';

import { SafetyHazardSetService } from './safety-hazard-set.service';

describe('SafetyHazardSetService', () => {
  let service: SafetyHazardSetService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SafetyHazardSetService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
