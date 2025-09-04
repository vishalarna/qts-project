import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, Provider } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { ActivityNotification } from 'src/app/_DtoModels/ActivityNotification/ActivityNotification';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ActivityNotificationService {
  baseUrl = environment.QTD + 'activitynotification';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {
          return res['result'] as ActivityNotification[];
        })
      ));
  }
}
