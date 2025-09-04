import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { StubDocumentsService } from "./stub.documents";
import { ApiDocumentsService } from "./api.document.Service";
export interface IDocumentsService {
  getAllActiveAsync: () => Promise<any>;
}

function documentsServiceFactory(http: HttpClient) {
    //here you can either inject params in to determine whic service to use OR detect an env var
    
    if (environment.Storybook_UseStub) {
      return new StubDocumentsService();
    }
    else {
      return new ApiDocumentsService(http);
    }
  }
  
  export const documentsServiceProvider = {
    provide: ApiDocumentsService,
    useFactory: documentsServiceFactory,
    deps: [HttpClient]
  };