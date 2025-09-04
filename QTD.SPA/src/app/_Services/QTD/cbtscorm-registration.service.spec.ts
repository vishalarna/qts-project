import { TestBed } from '@angular/core/testing';

import { CBTScormRegistrationServiceService } from './cbtscorm-registration.service';

describe('CBTScormRegistrationServiceService', () => {
  let service: CBTScormRegistrationServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CBTScormRegistrationServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
