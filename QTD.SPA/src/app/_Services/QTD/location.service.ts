import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Location } from 'src/app/_DtoModels/Locations/Location';
import { LocationLatestActivityVM } from 'src/app/_DtoModels/Locations/LocationLatestActivityVM';
import { LocationStatsVM } from 'src/app/_DtoModels/Locations/LocationStatsVM';
import { Location_CreateOptions } from 'src/app/_DtoModels/Locations/Location_CreateOptions';
import { LocationCategoryCompactOptions } from 'src/app/_DtoModels/Location_Category/LocationCategoryCompactOptions';
import { Location_HistoryCreateOptions } from 'src/app/_DtoModels/Location_History/Location_HistoryCreateOptions';
import { environment } from 'src/environments/environment';
import { firstValueFrom } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class LocationService {

  baseUrl = environment.QTD + 'locations';
  constructor(private http: HttpClient) {}

  getLocation(){
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{

        return res["locList"] as Location[];
      })
    ));
  }
  getLocCategoryWithLoc()
  {
    return firstValueFrom(this.http.get(this.baseUrl + `/categories/nest`).pipe(
      map((res:any)=>{
        return res["result"] as LocationCategoryCompactOptions[];
      })
    ));
  }
  create(options: Location_CreateOptions)
  {
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['loc'] as Location;
      })
    ));
  }
  getCount(){
    return firstValueFrom(this.http.get(this.baseUrl + `/count`).pipe(
      map((res:any)=>{
        return res['result'] as number;
      })
    ));
  }
  get(id: any)
  {

    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res: any) => {

        return res['loc'] as Location;
      })
    ));
  }
  makeActiveInactiveOrDelete(options:Location_HistoryCreateOptions){
    return firstValueFrom(this.http.delete(this.baseUrl,{body:options}).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  update(id:any,options:Location_CreateOptions)
  {

    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res["result"];
      })
    ));
  }
  getStatsCount()
  {

    return firstValueFrom(this.http
      .get(this.baseUrl + '/stats')
      .pipe(
        map((res: any) => {
          return res['stats'] as LocationStatsVM;
        })
      ));
  }
  getStatusHistory()
  {

    return firstValueFrom(this.http
      .get(this.baseUrl + `/history`)
      .pipe(
        map((res: any) => {
          return res['history'] as LocationLatestActivityVM[];
        })
      ));
  }

  getcatList(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/catlist`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }

  getlocList(notlinkedWith: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${notlinkedWith}/loclist`)
      .pipe(
        map((res: any) => {
          return res['result'] as any;
        })
      )
      );
  }
}


