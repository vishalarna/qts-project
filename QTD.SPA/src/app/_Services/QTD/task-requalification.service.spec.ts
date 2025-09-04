import { TestBed } from '@angular/core/testing';

import { TaskRequalificationService } from './task-requalification.service';

describe('TaskRequalificationService', () => {
  let service: TaskRequalificationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TaskRequalificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
