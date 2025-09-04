import { TestBed } from '@angular/core/testing';
import { MetaILAConfigPublishOptionService } from './meta-ila-config-publish-option.service';


describe('ProcedureIssuingAuthorityService', () => {
  let service: MetaILAConfigPublishOptionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MetaILAConfigPublishOptionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
