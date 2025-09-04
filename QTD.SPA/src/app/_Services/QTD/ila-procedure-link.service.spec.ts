import { TestBed } from '@angular/core/testing';
import { IlaProcedureLinkService } from './ila-procedure-link.service';

describe('IlaProcedureLinkService', () => {
  let service: IlaProcedureLinkService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IlaProcedureLinkService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
