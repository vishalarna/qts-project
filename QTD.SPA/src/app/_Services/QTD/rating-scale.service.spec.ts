import { TestBed } from '@angular/core/testing';
import { RatingScaleService } from './rating-scale.service';

describe('RatingScaleService', () => {
  let service: RatingScaleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RatingScaleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
