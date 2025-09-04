import { TestBed } from '@angular/core/testing';

import { DeliveryMethodeService } from './delivery-methode.service';

describe('DeliveryMethodeService', () => {
  let service: DeliveryMethodeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeliveryMethodeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
