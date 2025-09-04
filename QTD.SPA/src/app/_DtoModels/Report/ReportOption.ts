export class ReportOptions {
   value: string;
   display: boolean;

   constructor(filterName: string, filterValue: boolean ){
      this.value = filterName;
      this.display = filterValue;
   }
}