import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { StubDocumentTypesService } from "./stub.documentTypes";
import { ApiDocumentTypesService } from "./api.documentTypes.Service";
export interface IDocumentTypesService {
  getAllActiveAsync: () => Promise<any>;
  // updateActiveAsync: (id:string, ) => Promise<any>;
  // DeleteActiveAsync: (id:string) => Promise<any>;
  // getActiveFileAsync: (id:string) => Promise<any>;
  getActiveLinkToDataOptionsAsync:(documentTypeId:string)=>Promise<any>;
  // getActiveByDocumentTypeAsync: (documentTypeId: string) => Promise<any>;
}

function documentTypesServiceFactory(http: HttpClient) {
    //here you can either inject params in to determine whic service to use OR detect an env var
    if (environment.Storybook_UseStub) {
      return new StubDocumentTypesService();
    }
    else {
      return new ApiDocumentTypesService(http);
    }
  }
  
  export const documentTypesServiceProvider = {
    provide: ApiDocumentTypesService,
    useFactory: documentTypesServiceFactory,
    deps: [HttpClient]
  };