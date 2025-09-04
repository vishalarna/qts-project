import { TestBed } from '@angular/core/testing';

import { RRIssuingAuthorityService } from './rr-issuing-authority.service';

describe('RRIssuingAuthorityService', () => {
  let service: RRIssuingAuthorityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RRIssuingAuthorityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
