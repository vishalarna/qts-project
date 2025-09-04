import { TestBed } from '@angular/core/testing';

import { NercStandardService } from './nerc-standard.service';

describe('NercStandardService', () => {
  let service: NercStandardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NercStandardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
