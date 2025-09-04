import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { StubTaskListReviewService } from './stub.tasklistreview.service';
import { ApiTaskListReviewService } from './api.tasklistreview.service';

@Injectable({
  providedIn: 'root'
})
export class ITaskListReviewService {
  getOverviewAsync: () => Promise<any>;
  getAsync: (id : string) => Promise<any>;
  constructor() { }
}
function clientSettingsServiceFactory(http: HttpClient) {
  if (environment.Storybook_UseStub) {
    return new StubTaskListReviewService();
  } else {
    return new ApiTaskListReviewService(http);
  }
}

export const taskListReviewProvider = {
  provide: ApiTaskListReviewService,
  useFactory: clientSettingsServiceFactory,
  deps: [HttpClient],
};
