import { Injectable } from '@angular/core';
import { ScormUploadAddOptions } from 'src/app/_DtoModels/Scorm/ScormUploadAddOptions';
import { ScormUploadDeleteOptions } from 'src/app/_DtoModels/Scorm/ScormUploadDeleteOptions';

@Injectable({
  providedIn: 'root'
})
export class StubScornService {

  constructor() { }

  getScormUploadsAsync = (ilaId: number, current: Boolean) => {
    return new Promise<any>((resolve, reject) => {
      setTimeout(() => {
        Promise.resolve();
      }, 500);
    });
  }

  addScormUploadAsync = (cbtId: number, options: ScormUploadAddOptions) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        Promise.resolve();
      }, 500);
    });
  }

  disconnectScormUploadAsync = (cbtId: number, options: ScormUploadDeleteOptions) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        Promise.resolve();
      }, 500);
    });
  }

  getScormUploadAsync = (ilaId: number, scormUploadId: number) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        Promise.resolve();
      }, 500);
    });
  }
}
