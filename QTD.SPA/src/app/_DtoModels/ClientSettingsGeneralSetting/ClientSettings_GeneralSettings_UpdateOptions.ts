export class ClientSettings_GeneralSettings_UpdateOptions {

  companyName: string;
  companyLogo: string;
  dateFormat: string;
  ClassStartAndEndTimeFormat: string;
  CompanySpecificCoursePassingScore: number;
  defaultTimeZone:string;

  UpdateSetting = (name, value) => {
    this[name] = value;
  }
}
