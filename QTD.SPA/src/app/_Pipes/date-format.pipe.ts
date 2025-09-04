import { Pipe, PipeTransform } from '@angular/core';
import { ApiClientSettingsService } from '../_Services/QTD/ClientSettings/api.clientsettings.service';
import { DatePipe } from '@angular/common';

@Pipe({
  name: 'dateFormat'
})
export class DateFormatPipe implements PipeTransform {
  datePipe = new DatePipe('en-US');

  constructor(public clientSettingsService: ApiClientSettingsService) {
  }


  transform(value: Date): Promise<string> {
    var checkDateFormat =localStorage.getItem('dateFormat'); 
    return new Promise<string>((resolve, reject) => {

      let result = '';
      if(checkDateFormat == null){
        this.clientSettingsService.GetGeneralSettingsAsync().then(settings => {
          let dateFormat = settings.dateFormat;
          if(dateFormat){
            localStorage.setItem('dateFormat', dateFormat);
            result = this.datePipe.transform(value, dateFormat);
          }
          resolve(result);
        });
      }
      else{
        result = this.datePipe.transform(value, checkDateFormat);
        resolve(result);
      }
      
    });
  }

}
