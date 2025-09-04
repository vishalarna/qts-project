import { Injectable } from '@angular/core';
import { IDocumentTypesService } from './idocumentTypes-service';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { DocumentTypeViewModel } from 'src/app/_DtoModels/DocumentType/DocumentTypeViewModel';
import { TreeItemViewModel } from 'src/app/_DtoModels/TreeVMs/TreeItemViewModel';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiDocumentTypesService implements IDocumentTypesService {
  baseUrl = environment.QTD + 'documentTypes';
  constructor(private http: HttpClient) {}
  getAllActiveAsync = () => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/active`)
      .pipe(
        map((res: any) => {
          return res.result as DocumentTypeViewModel[];
        })
      )
      );
  };

  getActiveLinkToDataOptionsAsync = (documentTypeId: string) => {
    return firstValueFrom(this.http
      .get(this.baseUrl + `/${documentTypeId}/linkToDataOptions/active`)
      .pipe(
        map((res: any) => {
          return res.result as TreeItemViewModel;
        })
      )
      );
  };
}
