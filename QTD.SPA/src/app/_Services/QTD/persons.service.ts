import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivityNotification } from '@models/ActivityNotification/ActivityNotification';
import { PersonActivityNotificationOptions_VM } from '@models/Person/PersonActivityNotificationOptions_VM';
import { PersonWithUserDataVm } from '@models/Person/PersonWithUserDataVm';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { Person } from 'src/app/_DtoModels/Person/Person';
import { PersonCreateOption } from 'src/app/_DtoModels/Person/PersonCreateOption';
import { PersonUpdateOption } from 'src/app/_DtoModels/Person/PersonUpdateOption';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PersonsService {
  baseUrl = environment.QTD + 'persons';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          
          return res['persons'] as Person[];
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          
          return res['person'] as Person;
        })
      )
      );
  }

  create(options: PersonCreateOption) {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          
          return res['person'] as Person;
        })
      )
      );
  }

  update(id: any, options: PersonUpdateOption) {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/${id}`, options)
      .pipe(
        map((res: any) => {
          
          return res['person'] as Person;
        })
      )
      );
  }

  delete(id: any) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {
          
          return res.message;
        })
      )
      );
  }

  getPersonsWithoutQtdUser(){
    return firstValueFrom(this.http
      .get(this.baseUrl + '/withoutQtdUsers')
      .pipe(
        map((res: any) => {
          
          return res['persons'] as Person[];
        })
      )
      );
  }

  createPersonActivityNotification(id: any, options: PersonActivityNotificationOptions_VM) {
    return firstValueFrom(this.http.post(this.baseUrl + `/${id}/activitynotification`, options).pipe(
      map((res: any) => {
        return res
      })

    ))
  }

  unlinkPersonActivityNotification(id: any, options: PersonActivityNotificationOptions_VM) {
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}/activitynotification`, { body: options }).pipe(
      map((res: any) => {
        return res;
      })
    ));
  }

  getLinkedActivityNotification(personId: any) {
    return firstValueFrom(this.http.get(this.baseUrl + `/${personId}/activitynotification`,).pipe(
      map((res: any) => {
        return res['result'] as ActivityNotification[];
      })
    ));
  }

  getPersonByUserNameAsync(username: string){
    return firstValueFrom(this.http.get(this.baseUrl + `/getByUserName/${username}`).pipe(
      map((res: any) => {
        return res['person'] as Person;
      })
    ));
  }
}
