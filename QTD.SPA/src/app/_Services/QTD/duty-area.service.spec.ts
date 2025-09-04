import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { DutyAreaService } from './duty-area.service';

describe('DutyAreaService', () => {
  let service: DutyAreaService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(DutyAreaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
