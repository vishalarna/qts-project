import { TestBed } from '@angular/core/testing';

import { ProcedureIssuingAuthorityService } from './procedure-issuing-authority.service';

describe('ProcedureIssuingAuthorityService', () => {
  let service: ProcedureIssuingAuthorityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProcedureIssuingAuthorityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
