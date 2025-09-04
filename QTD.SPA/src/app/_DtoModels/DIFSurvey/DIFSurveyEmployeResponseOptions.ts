import { DIFSurveyEmployeeResponseModel } from "./DIFSurveyEmployeeResponseModel";

export class DIFSurveyEmployeResponseOptions {
    responses: DIFSurveyEmployeeResponseModel[];
    comments:string;

    AddToResponses = (response: DIFSurveyEmployeeResponseModel) => {

      if (!this.responses) this.responses = [];
  
      if (!this.responses.filter(dd => dd.dIFSurveyTaskId == response.dIFSurveyTaskId)[0]) {
        this.responses.push(response);
      }
    }
  }