import { Injectable } from "@angular/core";
import { IDocumentsService } from "./idocuments-service";
import { pascalToCamel } from "src/app/_Shared/Utils/PascalToCamel";
import * as documentsList from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/documentStorage.json';

@Injectable({
    providedIn: 'root',
})
export class StubDocumentsService implements  IDocumentsService{
    
    getAllActiveAsync = () => {
        return new Promise<any>((resolve, reject) => {
            setTimeout(() => {
                resolve(pascalToCamel(documentsList));
            }, 500);
        });
    }
}