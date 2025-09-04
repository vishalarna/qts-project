import { TestBed } from '@angular/core/testing';
import { IlaRegRequirementsLinkService } from './ila-reg-requirements-link.service';


describe('IlaProcedureLinkService', () => {
  let service: IlaRegRequirementsLinkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IlaRegRequirementsLinkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
