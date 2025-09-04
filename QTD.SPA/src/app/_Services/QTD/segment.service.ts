import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UpdateSegmentObjectiveOrderListVM } from '@models/Segment_ObjectiveLink/UpdateSegmentObjectiveOrderListVM';
import { firstValueFrom } from 'rxjs';
import { map } from 'rxjs/operators';
import { Segment } from 'src/app/_DtoModels/Segment/Segment';
import { SegmentCreateOptions } from 'src/app/_DtoModels/Segment/SegmentCreateOptions';
import { SegmentOptions } from 'src/app/_DtoModels/Segment/SegmentOptions';
import { Segment_ObjectiveLink } from 'src/app/_DtoModels/Segment_ObjectiveLink/Segment_ObjectiveLink';
import { Segment_ObjectiveLinkOptions } from 'src/app/_DtoModels/Segment_ObjectiveLink/Segment_ObjectiveLinkOptions';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SegmentService {
  baseUrl = environment.QTD + 'segments';
  constructor(private http:HttpClient) { }

  get(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as Segment;
      })
    ));
  }

  getAll(){
    return firstValueFrom(this.http.get(this.baseUrl).pipe(
      map((res:any)=>{
        return res['result'] as Segment[];
      })
    ));
  }

  create(options:SegmentCreateOptions){
    return firstValueFrom(this.http.post(this.baseUrl,options).pipe(
      map((res:any)=>{
        return res['result'];
      })
    ));
  }

  delete(id:any,options:SegmentOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/${id}`,{body:options}).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  update(id:any,options:SegmentCreateOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  linkObjectives(id:any,options: UpdateSegmentObjectiveOrderListVM){
    return firstValueFrom(this.http.post(this.baseUrl + '/' + id + '/objective',options).pipe(
      map((res:any)=>{
        return res;
      })
    ));
  }

  unlinkObjectives(id:any,options:Segment_ObjectiveLinkOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + '/' + id + "/objective",{ body:options }).pipe(
      map((res:any)=>{
        return res['message'];
      })
    ));
  }

  getLinkedObjectives(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/objective`).pipe(
      map((res:any)=>{
        return res['result'] as Segment_ObjectiveLink[];
      })
    ));
  }

  getWithObjectives(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/links`).pipe(
      map((res:any)=>{
        return res['result'] as Segment[];
      })
    ));
  }

}
