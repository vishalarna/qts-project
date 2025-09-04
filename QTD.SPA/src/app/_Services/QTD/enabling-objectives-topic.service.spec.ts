import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { EnablingObjectivesTopicService } from './enabling-objectives-topic.service';

describe('EnablingObjectivesTopicService', () => {
  let service: EnablingObjectivesTopicService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(EnablingObjectivesTopicService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
