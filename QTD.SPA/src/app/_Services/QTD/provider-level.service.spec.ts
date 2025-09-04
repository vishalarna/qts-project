import { TestBed } from '@angular/core/testing';

import { ProviderLevelService } from './provider-level.service';

describe('ProviderLevelService', () => {
  let service: ProviderLevelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProviderLevelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
