import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AuthGuard } from './auth.guard';
import { LocalizeModule } from '../_Shared/modules/localize.module';
import { RouterTestingModule } from '@angular/router/testing';

describe('AuthGuard', () => {
  let guard: AuthGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule, LocalizeModule, HttpClientTestingModule],
    });
    guard = TestBed.inject(AuthGuard);
  });

  it('should create guard', () => {
    expect(guard).toBeTruthy();
  });
});
