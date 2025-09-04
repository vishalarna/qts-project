import { TestBed } from '@angular/core/testing';
import { IlaSafteyHazardLinkService } from './ila-safety-hazard-link.service';

describe('IlaProcedureLinkService', () => {
  let service: IlaSafteyHazardLinkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IlaSafteyHazardLinkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
