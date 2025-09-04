import { TestBed } from '@angular/core/testing';

import { TestSettingService } from './test-setting.service';

describe('TestSettingService', () => {
  let service: TestSettingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestSettingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
