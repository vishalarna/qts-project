import { TestBed } from '@angular/core/testing';

import { TaxonomyLevelService } from './taxonomy-level.service';

describe('TaxonomyLevelService', () => {
  let service: TaxonomyLevelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TaxonomyLevelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
