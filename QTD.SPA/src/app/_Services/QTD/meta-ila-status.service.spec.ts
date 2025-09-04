import { TestBed } from '@angular/core/testing';
import { MetaILAStatusService } from './meta-ila-status.service';



describe('MetaILAService', () => {
  let service: MetaILAStatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MetaILAStatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
