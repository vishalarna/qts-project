import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ToolGroupsService } from './tool-groups.service';

describe('ToolGroupsService', () => {
  let service: ToolGroupsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(ToolGroupsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
