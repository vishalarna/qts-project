import { TestBed } from '@angular/core/testing';

import { QTDService } from './qtd.service';

describe('QTDService', () => {
  let service: QTDService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QTDService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
