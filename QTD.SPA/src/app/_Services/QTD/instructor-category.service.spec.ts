import { TestBed } from '@angular/core/testing';

import { InstructorCategoryService } from './instructor-category.service';

describe('InstructorCategoryService', () => {
  let service: InstructorCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InstructorCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
