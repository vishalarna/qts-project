import { TestBed } from '@angular/core/testing';

import { CoversheetService } from './coversheet.service';

describe('CoversheetService', () => {
  let service: CoversheetService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CoversheetService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
