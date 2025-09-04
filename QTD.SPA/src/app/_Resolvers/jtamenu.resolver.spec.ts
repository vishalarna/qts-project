import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { JTAMenuResolver } from './jtamenu.resolver';

describe('JTAMenuResolver', () => {
  let service: JTAMenuResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(JTAMenuResolver);
  });

  it('should create resolver', () => {
    expect(service).toBeTruthy();
  });
});
