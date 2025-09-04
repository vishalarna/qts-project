import { TestBed } from '@angular/core/testing';
import { DataBroadcastService } from './DataBroadcast.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('DataBroadcastService', () => {
  let service: DataBroadcastService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(DataBroadcastService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
