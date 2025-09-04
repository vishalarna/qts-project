import { TestBed } from '@angular/core/testing';

import { PublicRequestGuard } from './public-request.guard';

describe('PublicRequestGuard', () => {
  let guard: PublicRequestGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(PublicRequestGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
