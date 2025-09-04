import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ClientUsersService } from './client-users.service';

describe('ClientUsersService', () => {
  let service: ClientUsersService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(ClientUsersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
