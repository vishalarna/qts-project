import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_CategoryOptions } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_CategoryOptions';
import { EnablingObjective_SubCategoryOptions } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategoryOptions';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjective_CategoryHistory } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_CategoryHistory';
import { EnablingObjective_SubCategoryHistory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategoryHistory';
import { EnablingObjectiveHistory } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveHistory';
import { EnablingObjective_Topic } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic';
import { EO_CategoryDeleteOptions } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_CategoryDeleteOptions';
import { firstValueFrom } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class EnablingObjectivesCategoryService {
  baseUrl = environment.QTD + 'enablingObjectives_categories';
  constructor(private http: HttpClient) {}

  getAll() {
    return firstValueFrom(this.http
      .get(this.baseUrl)
      .pipe(
        map((res: any) => {

          return res['eo_cats'] as EnablingObjective_Category[];
        })
      )
      );
  }
  getAllSimplifiedCategories(): Promise<any> {
    return firstValueFrom(this.http
      .get(this.baseUrl+'/simplifiedlist')
      .pipe(
        map((res: any) => {

          return res['eo_cats'] as any;
        })
      )
      );
  }

  get(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}`)
      .pipe(
        map((res: any) => {

          return res['eo_cat'] as EnablingObjective_Category;
        })
      )
      );
  }

  create(option: EnablingObjective_CategoryOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl, option)
      .pipe(
        map((res: any) => {

          return res['eo_cat'] as EnablingObjective_Category;
        })
      )
      );
  }

  update(id:any,options : EnablingObjective_CategoryOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/${id}`,options).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective_Category
      })
    ));
  }

  deleteCat(id:any,options : EO_CategoryDeleteOptions){
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/${id}`, { body : options })
      .pipe(
        map((res:any)=>{
          return res['message'] as string;
        })
      ));
  }

  deleteSubCat(id:any, options : EO_CategoryDeleteOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/subcategories/${id}`, { body : options }).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  deleteTopic(id:any,options : EO_CategoryDeleteOptions){
    return firstValueFrom(this.http.delete(this.baseUrl + `/enablingObjectives_topics/topics/${id}`, { body : options }).pipe(
      map((res:any)=>{
        return res['message'] as string;
      })
    ));
  }

  createSubCategory(id: any, option: EnablingObjective_SubCategoryOptions) {
    return firstValueFrom(this.http
      .post(this.baseUrl + `/${id}/subcategories`, option)
      .pipe(
        map((res: any) => {

          return res['eo_subCat'] as EnablingObjective_SubCategory;
        })
      )
      );
  }

  updateSubCategory(id:any,option : EnablingObjective_SubCategoryOptions){
    return firstValueFrom(this.http.put(this.baseUrl + `/subcategories/${id}`,option).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective_SubCategory
      })
    ));
  }

  getSubCategories(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/subcategories`)
      .pipe(
        map((res: any) => {

          return res['eo_subCats'] as EnablingObjective_SubCategory[];
        })
      )
      );
  }
  getAllSimplifiedSubCategories(id: any) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${id}/subcategories/simplifiedlist`)
      .pipe(
        map((res: any) => {

          return res['eo_subCats'] as EnablingObjective_SubCategory[];
        })
      )
      );
  }

  getSubCategoryNumber(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/subcategories/number`).pipe(
      map((res:any)=>{
        return res['result'] as number
      })
    ));
  }

  getAllWithSubCategories(){
    return firstValueFrom(this.http.get(this.baseUrl + '/subCategories').pipe(
      map((res:any)=>{
        return res['eo_cat'] as EnablingObjective_Category[];
      })
    ));
  }

  saveEOCatHistory(options:EnablingObjective_CategoryHistory){
    return firstValueFrom(this.http.post(this.baseUrl + '/history',options).pipe(
      map((res:any)=>{
        return res["result"] as EnablingObjective_CategoryHistory;
      })
    ));
  }

  saveEOSubCatHistory(options:EnablingObjective_SubCategoryHistory){
    
    return firstValueFrom(this.http.post(this.baseUrl + '/subcategory/history',options).pipe(
      map((res:any)=>{
        return res["result"] as EnablingObjective_SubCategoryHistory;
      })
    ));
  }

  getWithEOCategoryId(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/cat/${id}`).pipe(
      map((res:any)=>{
        return res["result"] as EnablingObjective_Category[];
      })
    ));
  }

  getSubCategory(subCatId:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/subcategories/${subCatId}`).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective_SubCategory;
      })
    ));
  }

  getTopic(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/enablingObjectives_topics/topics/${id}`).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective_Topic;
      })
    ));
  }

  getCatNumber(){
    return firstValueFrom(this.http.get(this.baseUrl + `/number`).pipe(
      map((res:any)=>{
        return res['result'] as number;
      })
    ));
  }

  checkCatForLinks(id : any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/haslinks`).pipe(
      map((res:any)=>{
        return res['result'] as boolean;
      })
    ));
  }

  checkSubCatForLinks(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/subcategories/${id}/haslinks`).pipe(
      map((res:any)=>{
        return res['result'] as boolean;
      })
    ));
  }

  checkTopicForLinks(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/enablingObjectives_topics/${id}/haslinks`).pipe(
      map((res:any)=>{
        return res['result'] as boolean;
      })
    ));
  }

  getAllSubCategories(){
    return firstValueFrom(this.http.get(this.baseUrl + `/subcategories/all`).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective_SubCategory[];
      })
    ));
  }

  getAllTopics(){
    return firstValueFrom(this.http.get(this.baseUrl + '/subcategories/topics').pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective_Topic[];
      })
    ))
  }

  getSubCategoriesWithNumber(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/${id}/subCat/number`).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective_SubCategory[];
      })
    ));
  }

  getTopicWithNumber(id:any){
    return firstValueFrom(this.http.get(this.baseUrl + `/subCat/${id}/topic/number`).pipe(
      map((res:any)=>{
        return res['result'] as EnablingObjective_Topic[];
      })
    ));
  }
}
