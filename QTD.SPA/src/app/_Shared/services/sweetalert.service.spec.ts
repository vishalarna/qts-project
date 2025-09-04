import { TestBed } from '@angular/core/testing';
import { SweetAlertService } from './sweetalert.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
describe('SweetalertService', () => {
  let service: SweetAlertService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(SweetAlertService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
