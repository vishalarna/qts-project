import { Injectable } from '@angular/core';
import { IEmployeesService } from './iemployees-service';
import {employeesTestData} from './testData';

@Injectable({
  providedIn: 'root',
})

export class StubEmployeesService implements IEmployeesService {
  constructor() {}

  getAll = () => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(employeesTestData);
      }, 3000);
    });
  }
}
