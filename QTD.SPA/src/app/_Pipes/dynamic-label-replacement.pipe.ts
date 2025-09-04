import { Pipe, PipeTransform } from '@angular/core';
import { ApiClientSettingsService } from '../_Services/QTD/ClientSettings/api.clientsettings.service';

@Pipe({
  name: 'dynamicLabelReplacement',
})
export class DynamicLabelReplacementPipe implements PipeTransform {
  constructor(
    private readonly clientSettingsService: ApiClientSettingsService
  ) {}
  transform(value: string): Promise<string> {    
    return this.clientSettingsService.GetLabelReplacementsAsync()
      .then((replacements) => {
        for (let replacement of replacements) {
          if(replacement.labelReplacement?.trim() == ''|| replacement.labelReplacement == null){
            replacement.labelReplacement = replacement.defaultLabel;
          }
          const regex = new RegExp("\\b" + replacement.defaultLabel, 'gi');
          value = value.replace(regex, replacement.labelReplacement);
        }
        return value; 
      });
  }
}
