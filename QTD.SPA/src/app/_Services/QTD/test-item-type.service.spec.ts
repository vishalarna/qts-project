import { TestBed } from '@angular/core/testing';

import { TestItemTypeService } from './test-item-type.service';

describe('TestItemTypeService', () => {
  let service: TestItemTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestItemTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
