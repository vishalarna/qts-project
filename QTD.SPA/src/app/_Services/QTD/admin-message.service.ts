import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AdminMessageCreateOptions } from "@models/AdminMessage/AdminMessageCreateOptions";
import { AdminMessageQTDUserUpdateOptions } from "@models/AdminMessage/AdminMessageQTDUserUpdateOptions";
import { AdminMessageVM } from "@models/AdminMessage/AdminMessageVM";
import { firstValueFrom } from "rxjs";
import { map } from "rxjs/operators";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
  })
  export class AdminMessageService {
    baseUrl = environment.QTD + 'adminMessages'
    constructor(private http: HttpClient) {}

    createAdminMessageAsync(options: AdminMessageCreateOptions){
        return firstValueFrom(this.http.post(this.baseUrl ,options).pipe(
                map((res:any)=>{
                  return;
                })
              ));
    }

    getAdminMessagesAsync() {
      return firstValueFrom(this.http.get(this.baseUrl).pipe(
        map((res: any)=> {
          return res['result'] as AdminMessageVM[];
        })
      ));
    }

    updateAdminMessageQTDUserAsync(options: AdminMessageQTDUserUpdateOptions){
      return firstValueFrom(this.http.put(this.baseUrl, options).pipe(
        map((res:any) => {
          return res['result'] as any;
        })
      ));
    }
  }