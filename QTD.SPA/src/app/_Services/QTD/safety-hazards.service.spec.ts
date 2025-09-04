import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { SafetyHazardsService } from './safety-hazards.service';

describe('SafetyHazardsService', () => {
  let service: SafetyHazardsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(SafetyHazardsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
