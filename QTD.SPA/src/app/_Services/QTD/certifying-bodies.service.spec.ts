import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CertifyingBodiesService } from './certifying-bodies.service';

describe('CertifyingBodiesService', () => {
  let service: CertifyingBodiesService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(CertifyingBodiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
