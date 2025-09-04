import { Pipe, PipeTransform } from '@angular/core';
import { ApiClientSettingsService } from '../_Services/QTD/ClientSettings/api.clientsettings.service';
/*
 * Raise the value exponentially
 * Takes an exponent argument that defaults to 1.
 * Usage:
 *   value | exponentialStrength:exponent
 * Example:
 *   {{ 2 | exponentialStrength:10 }}
 *   formats to: 1024
*/
@Pipe({name: 'labelReplacement'})
export class LabelReplacementPipe implements PipeTransform {
  constructor(private readonly clientSettingsService: ApiClientSettingsService) {
  }

  transform(value: string): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      let result = value;

      this.clientSettingsService.GetLabelReplacementsAsync().then(labelReplacements => {
        const replacement = labelReplacements.filter(r => r.defaultLabel.toUpperCase() === value.toUpperCase())[0];
        if (replacement && replacement.labelReplacement) {
          result = replacement.labelReplacement.trim() === '' ? value : replacement.labelReplacement;
        }
        resolve(result)
      });
    });
  }
}
