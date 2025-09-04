import { Injectable } from "@angular/core";
import { ITrainingProgramReview } from "./itrainingProgramReview-service";
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
import * as trainingProgramData from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/trainingProgramReview.json';

@Injectable({
    providedIn: 'root',
})
export class StubTrainingProgramReviewService implements  ITrainingProgramReview{

    getTrainingProgramAsync = () => {
        return new Promise<any>((resolve, reject) => {
            setTimeout(() => {
                resolve(pascalToCamel(trainingProgramData));
            }, 500);
        });
    }
    
}