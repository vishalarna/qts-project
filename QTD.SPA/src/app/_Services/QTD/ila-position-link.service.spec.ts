import { TestBed } from '@angular/core/testing';

import { IlaPositionLinkService } from './ila-position-link.service';

describe('IlaPositionLinkService', () => {
  let service: IlaPositionLinkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IlaPositionLinkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
