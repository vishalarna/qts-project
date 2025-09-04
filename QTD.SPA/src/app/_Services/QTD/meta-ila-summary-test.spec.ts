import { TestBed } from '@angular/core/testing';
import { MetaILASummaryTestService } from './meta-ila-summary-test.service';



describe('MetaILASummaryTestService', () => {
  let service: MetaILASummaryTestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MetaILASummaryTestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
