import { TestBed } from '@angular/core/testing';

import { CoversheetTypeService } from './coversheet-type.service';

describe('CoversheetTypeService', () => {
  let service: CoversheetTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CoversheetTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
