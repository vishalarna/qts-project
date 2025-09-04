import { TestBed } from '@angular/core/testing';

import { OnlineCoursesService } from './online-courses.service';
import { NercService } from './nerc.service';

describe('OnlineCoursesService', () => {
  let service: NercService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NercService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
