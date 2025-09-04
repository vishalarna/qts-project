import { Injectable } from '@angular/core';
import { IDifSurveyService } from './idifsurvey-service';
import { DIFSurvey_CreateOptions } from '@models/DIFSurvey/DIFSurvey_CreateOptions';

@Injectable({
  providedIn: 'root',
})
export class StubDifSurveyService implements IDifSurveyService {
  constructor() {}

  createAsync = (options: DIFSurvey_CreateOptions) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve('ok');
      }, 500);
    });
  };

}
