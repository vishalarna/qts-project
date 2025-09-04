import { TestBed } from '@angular/core/testing';
import { SafetyHazardCategoryService } from './safety-hazard-category.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('SaftyHazardCategoryService', () => {
  let service: SafetyHazardCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(SafetyHazardCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
