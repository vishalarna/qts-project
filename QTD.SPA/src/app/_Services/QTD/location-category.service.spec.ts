import { TestBed } from '@angular/core/testing';

import { LocationCategoryService } from './location-category.service';

describe('LocationCategoryService', () => {
  let service: LocationCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocationCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
