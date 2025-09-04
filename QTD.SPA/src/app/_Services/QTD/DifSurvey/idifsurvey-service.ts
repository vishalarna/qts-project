import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ApiDifSurveyService } from './api.difsurvey.service';
import { StubDifSurveyService } from './stub.difsurvey.service';
import { DIFSurvey_CreateOptions } from '@models/DIFSurvey/DIFSurvey_CreateOptions';
import { DIFSurveyOverview_VM } from '@models/DIFSurvey/DifSurveyOverview_VM';

export interface IDifSurveyService {
  createAsync: (options: DIFSurvey_CreateOptions) => Promise<any>;
}

function clientSettingsServiceFactory(http: HttpClient) {
  if (environment.Storybook_UseStub) {
    return new StubDifSurveyService();
  } else {
    return new ApiDifSurveyService(http);
  }
}

export const difSurveyServiceProvider = {
  provide: ApiDifSurveyService,
  useFactory: clientSettingsServiceFactory,
  deps: [HttpClient],
};
