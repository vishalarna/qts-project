import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StubTaskListReviewService {

  constructor() { }
  getAsync = () => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        Promise.resolve();
      }, 3000);
    });
  }
}


