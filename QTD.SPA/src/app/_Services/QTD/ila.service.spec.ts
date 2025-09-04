import { TestBed } from '@angular/core/testing';

import { IlaService } from './ila.service';

describe('IlaService', () => {
  let service: IlaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IlaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
