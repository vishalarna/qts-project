import { Component, Input, OnInit } from '@angular/core';
import { TestSettingService } from 'src/app/_Services/QTD/test-setting.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-test-setting',
  templateUrl: './fly-panel-test-setting.component.html',
  styleUrls: ['./fly-panel-test-setting.component.scss']
})
export class FlyPanelTestSettingComponent implements OnInit {
  @Input() WrittenStatus: boolean;
  @Input() DiscussStatus: boolean;
  testSettings: any[] = [];

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private testSettingservice: TestSettingService,
    private alert: SweetAlertService,
  ) { }

  ngOnInit(): void {
    this.readyTestSettings();
  }

  async readyTestSettings() {
    await this.testSettingservice.getAll().then((res: any) => {
      
      this.testSettings = res;
    }).catch((err: any) => {
      this.alert.errorToast("Error Fetching test settings");
    })
  }

  OnSave() {
    this.flyPanelSrvc.close();
  }

}
