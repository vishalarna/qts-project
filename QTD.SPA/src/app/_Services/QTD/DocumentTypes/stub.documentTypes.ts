import { Injectable } from '@angular/core';
import { IDocumentTypesService } from './idocumentTypes-service';
import { pascalToCamel } from 'src/app/_Shared/Utils/PascalToCamel';
import * as documentTypesData from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/documentType.json';
import * as positions from '../../../../../../QTD2.Data/Initialization/QTDContext/Data/Development/positions.json';

@Injectable({
  providedIn: 'root',
})
export class StubDocumentTypesService implements IDocumentTypesService {
  constructor() {}
  getAllActiveAsync = () => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        resolve(pascalToCamel(documentTypesData));
      }, 500);
    });
  };
  getActiveLinkToDataOptionsAsync = (documentTypeId: string) => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        let linkToDataOptions = pascalToCamel(positions).map((item) => {
          return {
            dataId: item.positionNumber,
            optionText: item.positionTitle,
          };
        });
        resolve(linkToDataOptions);
      }, 500);
    });
  };
}
