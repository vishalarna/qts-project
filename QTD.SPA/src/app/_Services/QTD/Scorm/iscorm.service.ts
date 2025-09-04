import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ScormUploadAddOptions } from 'src/app/_DtoModels/Scorm/ScormUploadAddOptions';
import { ScormUploadDeleteOptions } from 'src/app/_DtoModels/Scorm/ScormUploadDeleteOptions';
import { environment } from 'src/environments/environment';
import { ApiScormService } from './api.scorm.service';
import { StubScornService } from './stub.scorm.service';


export interface IscormService {
  getScormUploadsAsync: (ilaId: number, current: Boolean) => Promise<any>;
  addScormUploadAsync: (cbtId: number, options: ScormUploadAddOptions) => Promise<any>;
  disconnectScormUploadAsync: (cbtId: number, options: ScormUploadDeleteOptions) => Promise<any>;
  getScormUploadAsync: (ilaId: number, scormUploadId: number) => Promise<any>;
}

function scornServiceFactory(http: HttpClient) {
  //here you can either inject params in to determine whic service to use OR detect an env var
  
  if (environment.Storybook_UseStub) {
    return new StubScornService();
  }
  else {
    return new ApiScormService(http);
  }
}

export const scormServiceProvider = {
  provide: ApiScormService,
  useFactory: scornServiceFactory,
  deps: [HttpClient]
};