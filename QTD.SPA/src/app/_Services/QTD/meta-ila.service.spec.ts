import { TestBed } from '@angular/core/testing';
import { MetaILAService } from './meta-ila.service';



describe('MetaILAService', () => {
  let service: MetaILAService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MetaILAService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
