import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatExpansionPanel } from '@angular/material/expansion';
import { Observable, Subscription } from 'rxjs';
import { CustomDashboardSettingOption, CustomizeDashboardUpdateOptions } from 'src/app/_DtoModels/ClientUserSettingsDashboard/CustomizeDashboardUpdateOptions';

@Component({
  selector: 'app-customize-dashboard',
  templateUrl: './customize-dashboard.component.html',
  styleUrls: ['./customize-dashboard.component.scss'],
  viewProviders: [MatExpansionPanel]
})
export class CustomizeDashboardComponent implements OnInit {

  @Input()
  ClientUserSettings_Dashboard;
  @Input() completeEvent: Observable<void>;
  @Output()
  OnSaveClickedEvent: EventEmitter<any> = new EventEmitter();
  combinedSettingsList: any;
  uniqueGroupNames: any;
  CombinedUniqueName: any;
  enableParentToggle: boolean;
  enableChildToggle: boolean;
  accordinIndex: number;
  customizeDashboardUpdateOptions: CustomizeDashboardUpdateOptions;

  private completeSubscription: Subscription;

  constructor() { }

  ngOnInit(): void {
    const self = this;
    this.getcategorydetails();
    this.initializeCustomizeDashboardOptions();
    self.completeSubscription = this.completeEvent.subscribe((dashboardSettings) => {
      self.ClientUserSettings_Dashboard = dashboardSettings;
    })
  }
  initializeCustomizeDashboardOptions(): void {
    this.customizeDashboardUpdateOptions = new CustomizeDashboardUpdateOptions();
  }

  public getcategorydetails() {
    this.CombinedUniqueName = [];
    this.CombinedUniqueName = new Set(this.ClientUserSettings_Dashboard.map(item => item.dashboardSetting.categoryName));
  }

  getCategoryValuesList(categoryName: string) {
    return this.ClientUserSettings_Dashboard.filter(r => r.dashboardSetting.categoryName == categoryName);
  }

  getCategoryGroupList(groupName: string) {
    return this.ClientUserSettings_Dashboard.filter(r => r.dashboardSetting.groupName == groupName);
  }

  onParentToggleChange(event, elementName: string) {
    if (event.target.checked) {
      this.ClientUserSettings_Dashboard.forEach(element => {
        if (element.dashboardSetting.groupName === elementName) {
          element.enabled = 'true';
          let settingOption = new CustomDashboardSettingOption(element.dashboardSetting.name, true, false);
          this.customizeDashboardUpdateOptions.UpdateSettingOption(settingOption);
        }
      });
    }
    else {
      this.ClientUserSettings_Dashboard.forEach(element => {
        if (element.dashboardSetting.groupName === elementName) {
          element.enabled = 'false';
          let settingOption = new CustomDashboardSettingOption(element.dashboardSetting.name, false, true);
          this.customizeDashboardUpdateOptions.UpdateSettingOption(settingOption);
        }
      });
    }
  }

  onToggleChange(event: any, element: any){
    if(event.target.checked){
      let settingOption = new CustomDashboardSettingOption(element.dashboardSetting.name, true, false);
      this.customizeDashboardUpdateOptions.UpdateSettingOption(settingOption);
    }
    else{
      let settingOption = new CustomDashboardSettingOption(element.dashboardSetting.name, false, true);
      this.customizeDashboardUpdateOptions.UpdateSettingOption(settingOption);
    }
  }

  onChildToggleChange(event, groupName: string, index: number, id: number) {
    if (!event.target.checked) {
      let element = <HTMLInputElement>document.getElementById("groupClass_" + index);
      element.checked = false;
      this.ClientUserSettings_Dashboard.filter(r => r.dashboardSetting.id == id)[0].enabled = "false";
      let settingOption = new CustomDashboardSettingOption(this.ClientUserSettings_Dashboard.filter(r => r.dashboardSetting.id == id)[0].dashboardSetting.name, element.checked, !element.checked);
      this.customizeDashboardUpdateOptions.UpdateSettingOption(settingOption);
    }
    else {
      this.ClientUserSettings_Dashboard.filter(r => r.dashboardSetting.id == id)[0].enabled = "true";
      let totalChildLength = this.ClientUserSettings_Dashboard.filter(r => r.dashboardSetting.groupName == groupName).length;
      let totalIsEnabledChildLength = this.ClientUserSettings_Dashboard.filter(r => r.dashboardSetting.groupName == groupName && r.enabled == 'true').length;
      if (totalChildLength === totalIsEnabledChildLength) {
        let element = <HTMLInputElement>document.getElementById("groupClass_" + index);
        element.checked = true;
        let settingOption = new CustomDashboardSettingOption(this.ClientUserSettings_Dashboard.filter(r => r.dashboardSetting.id == id)[0].dashboardSetting.name, element.checked, !element.checked);
        this.customizeDashboardUpdateOptions.UpdateSettingOption(settingOption);
      }
    }
  }

  getgroupcategoryList(categoryName: string) {
    const tempList = this.ClientUserSettings_Dashboard.filter(r => r.dashboardSetting.categoryName == categoryName && r.dashboardSetting.groupName !== null);
    if (tempList.length > 0) {
      this.uniqueGroupNames = new Set(tempList.map(r => r.dashboardSetting.groupName))
      return true;
    } else {
      return false;
    }
  }

  public SaveButtonClick() {
    this.OnSaveClickedEvent.emit(this.customizeDashboardUpdateOptions);
  }

}
