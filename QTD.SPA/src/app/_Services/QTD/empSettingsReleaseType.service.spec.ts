import { TestBed } from '@angular/core/testing';

import { EmpSettingsReleaseTypeService } from './empSettingsReleaseType.service';

describe('EmpSettingsReleaseTypeService', () => {
  let service: EmpSettingsReleaseTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpSettingsReleaseTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
