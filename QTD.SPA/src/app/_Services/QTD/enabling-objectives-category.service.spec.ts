import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { EnablingObjectivesCategoryService } from './enabling-objectives-category.service';

describe('EnablingObjectivesCategoryService', () => {
  let service: EnablingObjectivesCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(EnablingObjectivesCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
