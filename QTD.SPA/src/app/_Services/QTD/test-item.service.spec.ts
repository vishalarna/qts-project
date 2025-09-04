import { TestBed } from '@angular/core/testing';

import { TestItemService } from './test-item.service';

describe('TestItemService', () => {
  let service: TestItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestItemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
