import { TestBed } from '@angular/core/testing';

import { RegulatoryRequirementService } from './regulatory-requirement.service';

describe('RegulatoryRequirementService', () => {
  let service: RegulatoryRequirementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegulatoryRequirementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
