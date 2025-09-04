import { Injectable } from '@angular/core';
import { IEmployeesService } from './iemployees-service'
import { employeesTestData } from './testData';
import {map} from "rxjs/operators";
import {Employee} from "../../../_DtoModels/Employee/Employee";
import {environment} from "../../../../environments/environment";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root',
})

export class ApiEmployeesService implements IEmployeesService {

  baseUrl = environment.QTD + 'employees';

  constructor(private http: HttpClient) {}

  getAll() {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(employeesTestData);
      }, 3000);
    });
  }

}
