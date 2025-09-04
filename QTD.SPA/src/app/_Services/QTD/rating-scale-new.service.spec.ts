import { TestBed } from '@angular/core/testing';

import { RatingScaleNewService } from './rating-scale-new.service';

describe('RatingScaleNewService', () => {
  let service: RatingScaleNewService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RatingScaleNewService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
