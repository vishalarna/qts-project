export class ClientSettings_GeneralSettings {
    companyName: string;
    companyLogo: string;
    dateFormat: string;
    classStartEndTimeFormat: string;
    companySpecificCoursePassingScore: number;

    updateCompanyName(name: string) {
      this.companyName = name;
    }

    updateCompanyLogo(logo: string){
      this.companyLogo = logo;
    }

    updateDateFormat(format: string) {
      this.dateFormat = format;
    }
  
    updateClassStartEndTimeFormat(timeFormat: string){
      this.classStartEndTimeFormat = timeFormat;
    }

    updateCompanySpecificCoursePassingScore(score: number){
        this.companySpecificCoursePassingScore = score;
    }
}