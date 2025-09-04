import { TestBed } from '@angular/core/testing';

import { AuthenticationSettingService } from './authentication-setting.service';

describe('AuthenticationSettingService', () => {
  let service: AuthenticationSettingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthenticationSettingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
