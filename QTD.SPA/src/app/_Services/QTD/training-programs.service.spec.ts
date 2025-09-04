import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { TrainingProgramsService } from './training-programs.service';

describe('TrainingProgramsService', () => {
  let service: TrainingProgramsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(TrainingProgramsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
