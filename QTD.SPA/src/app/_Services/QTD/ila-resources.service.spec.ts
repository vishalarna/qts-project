import { TestBed } from '@angular/core/testing';

import { IlaResourcesService } from './ila-resources.service';

describe('IlaResourcesService', () => {
  let service: IlaResourcesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IlaResourcesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
