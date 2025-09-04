import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { PersonsService } from './persons.service';

describe('PersonsService', () => {
  let service: PersonsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(PersonsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
