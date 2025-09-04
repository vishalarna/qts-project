import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { IssuingAuthoritiesService } from './issuing-authorities.service';

describe('IssuingAuthoritiesService', () => {
  let service: IssuingAuthoritiesService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(IssuingAuthoritiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
