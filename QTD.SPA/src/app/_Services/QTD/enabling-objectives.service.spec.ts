import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { EnablingObjectivesService } from './enabling-objectives.service';

describe('EnablingObjectivesService', () => {
  let service: EnablingObjectivesService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(EnablingObjectivesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
