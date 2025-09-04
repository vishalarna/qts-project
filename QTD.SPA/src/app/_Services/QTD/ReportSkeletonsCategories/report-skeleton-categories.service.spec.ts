import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReportSkeletonCategoriesService } from './report-skeleton-categories.service';

describe('ReportSkeletonCategoriesService', () => {
  let service: ReportSkeletonCategoriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(ReportSkeletonCategoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
