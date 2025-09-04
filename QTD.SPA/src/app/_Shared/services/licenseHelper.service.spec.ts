import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { LicenseHelperService } from './licenseHelper.service';

describe('LicenseHelperService', () => {
  let service: LicenseHelperService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(LicenseHelperService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
