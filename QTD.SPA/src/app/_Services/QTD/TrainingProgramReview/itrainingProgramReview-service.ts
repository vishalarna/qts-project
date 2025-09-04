import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { StubTrainingProgramReviewService } from "./stub.trainingProgramReview";
import { ApiTrainingProgramReviewService } from "./api.trainingProgramReview.Service";
export interface ITrainingProgramReview {
}

function trainingProgramReviewServiceFactory(http: HttpClient) {
    //here you can either inject params in to determine whic service to use OR detect an env var
    
    if (environment.Storybook_UseStub) {
      return new StubTrainingProgramReviewService();
    }
    else {
      return new ApiTrainingProgramReviewService(http);
    }
  }
  
  export const trainingProgramReviewServiceProvider = {
    provide: ApiTrainingProgramReviewService,
    useFactory: trainingProgramReviewServiceFactory,
    deps: [HttpClient]
  };