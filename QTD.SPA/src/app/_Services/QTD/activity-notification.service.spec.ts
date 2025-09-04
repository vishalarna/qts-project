import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ActivityNotificationService } from './activity-notification.service';

describe('ActivityNotificationService', () => {
  let service: ActivityNotificationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(ActivityNotificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
