import { TestBed } from '@angular/core/testing';

import { TrainingProgramTypeService } from './training-program-type.service';

describe('TrainingProgramTypeService', () => {
  let service: TrainingProgramTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrainingProgramTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
