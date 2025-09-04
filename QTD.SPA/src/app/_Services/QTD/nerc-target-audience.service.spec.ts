import { TestBed } from '@angular/core/testing';
import { NercTargetAudienceService } from './nerc-target-audience.service';

describe('NercTargetAudienceService', () => {
  let service: NercTargetAudienceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NercTargetAudienceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
