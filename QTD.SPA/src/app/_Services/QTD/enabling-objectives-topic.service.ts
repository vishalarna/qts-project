import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EnablingObjective_TopicOptions } from "src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_TopicOptions";
import { environment } from "src/environments/environment";
import { map } from 'rxjs/operators';
import { EnablingObjective_Topic } from "src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic";
import { EnablingObjective_TopicHistory } from "src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_TopicHistory";
import { firstValueFrom } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class EnablingObjectivesTopicService {
  baseUrl = environment.QTD + 'enablingObjectives_topics';
  constructor(private http: HttpClient) {}

  createTopic(
    categoryid: any,
    subcategoryid: any,
    options: EnablingObjective_TopicOptions
  ) {
    return firstValueFrom(this.http
      .post(
        this.baseUrl + `/${categoryid}/subcategories/${subcategoryid}/topics`,
        options
      )
      .pipe(
        map((res: any) => {

          return res['eo_topic'] as EnablingObjective_Topic;
        })
      )
      );
  }

  updateTopic(topicId:any, options: EnablingObjective_TopicOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${topicId}`,options).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective_Topic;
      })
    ));
  }

  getTopics(categoryid: any, subcategoryid: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${categoryid}/subcategories/${subcategoryid}/topics`)
      .pipe(
        map((res: any) => {

          return res['eoTopics'] as EnablingObjective_Topic[];
        })
      )
      );
  }
  getAllSimplifiedTopics(categoryid: any, subcategoryid: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${categoryid}/subcategories/${subcategoryid}/topics/simplifiedlist`)
      .pipe(
        map((res: any) => {

          return res['eoTopics'] as EnablingObjective_Topic[];
        })
      )
      );
  }

  getAllTopics() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/topics`)
      .pipe(
        map((res: any) => {
          return res['eoTopics'] as EnablingObjective_Topic[];
        })
      )
      );
  }

  saveEOTopicHistory(options:EnablingObjective_TopicHistory){
    return firstValueFrom(this.http.post(this.baseUrl + '/history',options).pipe(
      map((res:any)=>{
        return res["result"] as EnablingObjective_TopicHistory;
      })
    ));
  }

  getTopicNumber(catId:any,subCatId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${catId}/${subCatId}/number`).pipe(
      map((res:any)=>{
        return res['result'] as number;
      })
    ));
  }

  getCategoryIdForTopic(subCatId : any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${subCatId}/catId`).pipe(
      map((res:any)=>{
        return res['result'] as string;
      })
    ));
  }
}
