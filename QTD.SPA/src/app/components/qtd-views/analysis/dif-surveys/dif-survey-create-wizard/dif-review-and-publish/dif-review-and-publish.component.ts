import { Component, Input, OnInit } from '@angular/core';
import { DIFSurveyVM } from '@models/DIFSurvey/DIFSurveyVM';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';

@Component({
  selector: 'app-dif-review-and-publish',
  templateUrl: './dif-review-and-publish.component.html',
  styleUrls: ['./dif-review-and-publish.component.scss']
})
export class DifReviewAndPublishComponent implements OnInit {
  @Input() inputDifSurveyVM: DIFSurveyVM;
  isLicenseValid:boolean=false;
  isNotificationEnabled:boolean=false;
  isReleaseToEMp:boolean=false;

  constructor(
    private licenseHelper:LicenseHelperService,
    private clientSettingsService: ApiClientSettingsService,

  ) { }

  ngOnInit(): void {
    this.checkLicense();
    this.getEmailNotificationDetailsAsync();
    this.isReleaseToEMp= this.inputDifSurveyVM?.releasedToEMP ?? false;
  }

  checkLicense(){
    var license = this.licenseHelper.getLicenseData();
    if(license?.deluxe ){
      this.isLicenseValid = true;
    }
  }
  async getEmailNotificationDetailsAsync(){
    await this.clientSettingsService.getNotificationByName('EMP DIF Survey').then(r => {
      this.isNotificationEnabled = r.enabled;
    });
  }
  releasedToEMPChange(event:any){
    this.isReleaseToEMp = event.checked;
    this.inputDifSurveyVM.releasedToEMP = this.isReleaseToEMp;
  }
}
