import { TestBed } from '@angular/core/testing';
import {ErrorLogging} from './error-logging.service'

describe('ErrorLogging.Service.TsService', () => {
  let service: ErrorLogging;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ErrorLogging);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
