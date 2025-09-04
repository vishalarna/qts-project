import { Injectable } from '@angular/core';
import { IDocumentsService } from './idocuments-service';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { DocumentViewModel } from 'src/app/_DtoModels/Document/DocumentViewModel';
import { DocumentCreateOptions } from 'src/app/_DtoModels/Document/DocumentCreationOptions';
import { DocumentUpdateOptions } from 'src/app/_DtoModels/Document/DoumentUpdateOptions';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiDocumentsService implements IDocumentsService {
  baseUrl = environment.QTD + 'documents';
  constructor(private http: HttpClient) {}

  getAllActiveAsync() {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/active`)
      .pipe(
        map((res: any) => {
          return res['result'] as DocumentViewModel[];
        })
      )
      );
  }

  getActiveByDocumentTypeAsync(documentTypeId: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/active/documentType/${documentTypeId}`)
      .pipe(
        map((res: any) => {
          return res['result'] as DocumentViewModel[];
        })
      )
      );
  }

  getActiveByLinkedDataAsync(linkedDataType: string, linkedDataId: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/active/${linkedDataType}/${linkedDataId}`)
      .pipe(
        map((res: any) => {
          return res['result'] as DocumentViewModel[];
        })
      )
      );
  }

  getActiveByLinkedDataAndDocumentTypeAsync(
    documentTypeId: string,
    linkedDataType: string,
    linkedDataId: string
  ) {
    return firstValueFrom(this.http
      .get(
        this.baseUrl +
          `/active/${linkedDataType}/${linkedDataId}/documentType/${documentTypeId}`
      )
      .pipe(
        map((res: any) => {
          return res['result'] as DocumentViewModel[];
        })
      )
      );
  }
  createAsync = (options: DocumentCreateOptions) => {
    return firstValueFrom(this.http
      .post(this.baseUrl, options)
      .pipe(
        map((res: any) => {
          return res.result as DocumentViewModel;
        })
      )
      );
  };
  updateActiveAsync = (options: DocumentUpdateOptions, id: string) => {
    return firstValueFrom(this.http
      .put(this.baseUrl + `/active/${id}`, options)
      .pipe(
        map((res: any) => {
          return res.result as DocumentViewModel;
        })
      )
      );
  };
  deleteActiveAsync(id: string) {
    return firstValueFrom(this.http
      .delete(this.baseUrl + `/active/${id}`, { observe: 'response' })
      .pipe(
        map((res: any) => {
          return res.status;
        })
      )
      );
  }
  getActiveFileAsync(id: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/active/${id}/file`)
      .pipe(
        map((res: any) => {
          return res.document;
        })
      )
      );
  }

  getLinkedDataNameAsync(linkedDataType: string, linkedDataId: string) {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/linkedDataName/${linkedDataType}/${linkedDataId}`)
      .pipe(
        map((res: any) => {
          return res.result;
        })
      )
      );
  }
}
