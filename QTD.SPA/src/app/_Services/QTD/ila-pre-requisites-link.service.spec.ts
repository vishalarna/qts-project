import { TestBed } from '@angular/core/testing';
import { IlaPreRequisitesLinkService } from './ila-pre-requisites-link.service';

describe('IlaProcedureLinkService', () => {
  let service: IlaPreRequisitesLinkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IlaPreRequisitesLinkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
