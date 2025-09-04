import { Component, ElementRef, OnInit, Renderer2, TemplateRef, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatStepper } from '@angular/material/stepper';
import { ActivatedRoute, Router } from '@angular/router';
import { ImportData_ClassResponse_VM } from '@models/ExternalClassImport/ImportData_ClassResponse_VM';
import { ImportData_EmployeeResponse_VM } from '@models/ExternalClassImport/ImportData_EmployeeResponse_VM';
import { ImportData_ILAResponse_VM } from '@models/ExternalClassImport/ImportData_ILAResponse_VM';
import { ImportDatum_Class_VM } from '@models/ExternalClassImport/ImportDatum_Class_VM';
import { ImportDatum_Employee_VM } from '@models/ExternalClassImport/ImportDatum_Employee_VM';
import { ImportDatum_ILA_VM } from '@models/ExternalClassImport/ImportDatum_ILA_VM';
import { ValidateCSV_Class_Results_VM } from '@models/ExternalClassImport/ValidateCSV_Class_Results_VM';
import { ValidateCSV_Employee_Results_VM } from '@models/ExternalClassImport/ValidateCSV_Employee_Results_VM';
import { ValidateCSV_ILA_Results_VM } from '@models/ExternalClassImport/ValidateCSV_ILA_Results_VM';
import saveAs from 'file-saver';
import { DateFormatPipe } from 'src/app/_Pipes/date-format.pipe';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import { ImportService } from 'src/app/_Services/QTD/import.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { ImportCsvWizardComponent } from 'src/app/components/shared/import-csv-wizard/import-csv-wizard.component';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-external-class-import',
  templateUrl: './external-class-import.component.html',
  styleUrls: ['./external-class-import.component.scss']
})
export class ExternalClassImportComponent implements OnInit {
  @ViewChild('stepper') stepper: MatStepper;
  @ViewChild('importWizardILA') importWizardILA!:ImportCsvWizardComponent;
  @ViewChild('importWizardEmployee') importWizardEmployee!:ImportCsvWizardComponent;
  @ViewChild('importWizardClass') importWizardClass!:ImportCsvWizardComponent;
  @ViewChild('exitClassImport') exitClassImport: TemplateRef<any>;
  ilaValidationResult: ValidateCSV_ILA_Results_VM = new ValidateCSV_ILA_Results_VM();
  classValidationResult: ValidateCSV_Class_Results_VM = new ValidateCSV_Class_Results_VM();
  employeeValidationResult: ValidateCSV_Employee_Results_VM = new ValidateCSV_Employee_Results_VM();
  ilaConfirmResult:ImportDatum_ILA_VM[]=[];
  employeeConfirmResult:ImportDatum_Employee_VM[]=[];
  classConfirmResult:ImportDatum_Class_VM[]=[];
  uploadILAColumns:string[]=[];
  uploadEmployeeColumns:string[]=[];
  uploadClassColumns:string[]=[];
  dateTimeColumns :string[]=[];
  currentIndex: number = 0;
  ilaTemplate:any;
  employeeTemplate:any;
  classTemplate:any;
  type:string;
  isILAValidate: boolean = false;
  isEmployeeValidate: boolean = false;
  isClassValidate: boolean = false;
  isILAConfirm: boolean = false;
  isEmployeeConfirm: boolean = false;
  isClassConfirm: boolean = false;
  indexToGo: number =0;
  isExitConfirmation:boolean=false;
  subscription = new SubSink();
  isEmployeeImport:string="true";
  isILAImport:string = "true";
  isClassImport:string = "true";
  employeeImportIndex:number = -1;
  iLAImportIndex:number = -1;
  classImportIndex:number = -1;
  lastIndex:number = 0;
  dateFormatPipe=new DateFormatPipe(this.clientSettingsService);
  constructor( private router: Router,
    public dialog: MatDialog,
    private importService: ImportService,
    private _alertService: SweetAlertService,
    private elRef : ElementRef,
    private renderer :Renderer2,
    private route: ActivatedRoute,
    private labelPipe: LabelReplacementPipe,
    private dynamiclabelPipe: DynamicLabelReplacementPipe,
    public clientSettingsService: ApiClientSettingsService
    ) { }

  ngOnInit(): void {
    this.subscription.sink = this.route.queryParams.subscribe((res)=>{
      this.isEmployeeImport = res.isEmployeeImport != null ? res.isEmployeeImport : "true";
      this.isILAImport = res.isILAImport != null ? res.isILAImport : "true";
      this.isClassImport = res.isClassImport != null ? res.isClassImport :"true";
      this.setClassImportIndex();
    })
    this.uploadILAColumns = ["ILA Name", "ILA Num", "ILA Desc", "Self Paced", "Total Hours", "Provider Name","Delivery Method Name","Effective Date", "NercIsIncludeSimulation", "NercEmergencyOperatingTraining",
    "NercIsPartialCred", "NercTotalCEH", "NercStandards", "NercSimulation", "Reg", "Reg2"];
    this.uploadEmployeeColumns = ["Last Name", "First Name", "Middle", "Emp Num", "Email", "Phone", "Cert Name", "Cert Num", "Issue Date", "Recert Date", "Cert Exp Date", "Position Num","Position Start Date",
    "Pos Abbrev", "Organization Name"];
    this.uploadClassColumns = ["Class ILA Num", "Start Date", "Class End Date", "Instructor Name", "Location", "Emp Num", "Comp Grade"];
    this.dateTimeColumns=["StartDate", "ClassEndDate"];
    this.getTemplateAsync();
  }

  ngAfterViewInit() {
    const stepHeaders = this.elRef.nativeElement.querySelectorAll('.mat-step-header');
    stepHeaders.forEach((stepHeader, index) => {
      this.renderer.listen(stepHeader, 'click', () => {
        this.stepClick(index);
      });
    });
    this.lastIndex = this.stepper.steps.length - 1 ;
  }

  stepClick(stepIndex: number) {
    if(this.currentIndex != stepIndex){
      if(!this.checkNavigationAllowed()){
        this.isExitConfirmation=false;
        this.continueOrExitClassImport(this.exitClassImport);
        this.indexToGo = stepIndex;
      }
    }
  }


  goBack(templateRef: any) {
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  closeClassImport(){
    this.router.navigate(['data-exchange/import']);
  }

  continueOrExitClassImport(templateRef: any) {
      const dialogRef = this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
  }

  exitImport(templateRef: any){
    if((this.lastIndex == this.iLAImportIndex && (this.isILAValidate && !this.isILAConfirm)) || (this.lastIndex == this.employeeImportIndex && (this.isEmployeeValidate && !this.isEmployeeConfirm)) || (this.lastIndex == this.classImportIndex && (this.isClassValidate && !this.isClassConfirm))){
      this.isExitConfirmation=true;
      this.continueOrExitClassImport(templateRef);
    }
    else{
      this.router.navigate(['data-exchange/import']);
    }
  }

  
  nextOrExitImport(){
    switch(this.currentIndex){
      case this.iLAImportIndex :
        this.importWizardILA.goToUpload();
        break;
      case this.employeeImportIndex :
        this.importWizardEmployee.goToUpload();
        break;
      case this.classImportIndex:
        this.importWizardClass.goToUpload();
        break;
    }
    if(this.isExitConfirmation){
      this.router.navigate(['data-exchange/import']);
    }
    else if (this.currentIndex != this.indexToGo){
      this.navigateStepper();
    }
  }

  navigateStepper() {
    const difference = this.currentIndex - this.indexToGo;
    if (difference < 0) {
      for (let i = 0; i < Math.abs(difference); i++) {
        setTimeout(() => {
          this.stepper.next();
        }, i);
      }
    } else if (difference > 0) {
      for (let i = 0; i < difference; i++) {
        setTimeout(() => {
          this.stepper.previous();
        }, i);
      }
    }
  }


   goNext() {
    this.currentIndex =  this.stepper.selectedIndex;
    if(!this.checkNavigationAllowed()){
      this.isExitConfirmation=false;
      this.continueOrExitClassImport(this.exitClassImport);
      this.indexToGo=this.currentIndex + 1;
    }else{
      this.stepper.next();
    }
  }


  onStepSelectionChange(event : any){
    this.currentIndex=event.selectedIndex;
  }

  async getTemplateAsync(){
    await this.importService.getTemplateAsync('ILA Data').then((res:any) => {
      this.ilaTemplate=res.body;
    });
    await this.importService.getTemplateAsync('Employee Data').then((res:any) => {
      this.employeeTemplate=res.body;
    });
    await this.importService.getTemplateAsync('Class Data').then((res:any) => {
      this.classTemplate=res.body;
    });
  }

  async getILAUploadedFile(event:any){
    
    const formData = new FormData();
  
    formData.append('file', event);
    formData.append('returnFile', "true");
    await this.importService.validateCSVILAAsync(formData).then((res) => {
      this.importWizardILA.validationResult = res;
      this.ilaValidationResult=res;
      if(res.fileValidationErrors?.length>0){
        this._alertService.errorAlert("Invalid File Data",res.fileValidationErrors.map(x=>x.error).join("\n"));
        this.importWizardILA.isImportVisible=true;
      }
      else{
        this.importWizardILA.isImportVisible=false;
        this.isILAValidate = true;
      }
    })
  }

  async getEmployeeUploadedFile(event:any){
   
    const formData = new FormData();
    formData.append('file', event);
    formData.append('returnFile', "true");
    await this.importService.validateCSVEmployeeAsync(formData).then((res) => {
      this.importWizardEmployee.validationResult = res;
      this.employeeValidationResult=res;
      if(res.fileValidationErrors?.length>0){
        this._alertService.errorAlert("Invalid File Data",res.fileValidationErrors.map(x=>x.error).join("\n"));
        this.importWizardEmployee.isImportVisible=true;
      }
      else{
        this.isEmployeeValidate = true;
        this.importWizardEmployee.isImportVisible=false;
      }
    })
  }

  async getClassUploadedFile(event:any){
    
    const formData = new FormData();
    formData.append('file', event);
    formData.append('returnFile', "true");
    await this.importService.validateCSVClassAsync(formData).then(async (res) => {
      if(res && res.data) {
        res.data = await this.dateTimeColumnConversion(res.data, true);
      }
      this.importWizardClass.validationResult = res.data;
      this.classValidationResult=res;
      if(res.fileValidationErrors?.length>0){
        this._alertService.errorAlert("Invalid File Data",res.fileValidationErrors.map(x=>x.error).join("\n"));
        this.importWizardClass.isImportVisible=true;
      }
      else{
        this.isClassValidate = true;
        this.importWizardClass.isImportVisible=false;
      }
    })
  }
  convertUtcToLocal(utcDate: Date): Date {
    const utcStartDateTime = new Date(utcDate.toString() + ' UTC');
    return new Date(utcStartDateTime.toLocaleString());
  }
  
  async getTransformedDateAsync(date: Date): Promise<string> {
    const formattedDate = await this.dateFormatPipe.transform(date);
    const timeComponent = date.toLocaleTimeString('en-US', { hour12: false });
    return `${formattedDate} ${timeComponent}`;
  }

  async OnConfirmILAUpload(isReturnFile: boolean){
    var importILAOptions = new ImportData_ILAResponse_VM();
    importILAOptions.data= this.ilaValidationResult.data;
    importILAOptions.returnFile =isReturnFile;
    await this.importService.importILAResponseAsync(importILAOptions).then(async (res) => {
      this.importWizardILA.isConfirmed=true;  
      this.ilaConfirmResult=res.body?.result?.data;
       this.isILAConfirm = true;
      if(isReturnFile){
        this.createAndDownloadCSVFile(res,'ILAResponseData');
      }    
      this._alertService.successToast(await this.labelPipe.transform('ILA') + " Data Imported Successfully");
    })
  }

  async OnConfirmEmployeeUpload(isReturnFile: boolean){
    var importEmployeeOptions = new ImportData_EmployeeResponse_VM();
    importEmployeeOptions.data= this.employeeValidationResult.data;
    importEmployeeOptions.returnFile =isReturnFile;
    await this.importService.importEmployeeResponseAsync(importEmployeeOptions).then(async (res) => {
      this.importWizardEmployee.isConfirmed=true;  
      this.employeeConfirmResult=res.body?.result?.data;
      this.isEmployeeConfirm = true;
      if(isReturnFile){
        this.createAndDownloadCSVFile(res,'EmployeeResponseData');
      }    
      this._alertService.successToast(await this.labelPipe.transform('Employee') + " Data Imported Successfully");
    })
  }

  async OnConfirmClassUpload(isReturnFile: boolean){
    var importClassOptions = new ImportData_ClassResponse_VM();
    this.classValidationResult.data = await this.dateTimeColumnConversion(this.classValidationResult.data, false);
    importClassOptions.data= this.classValidationResult.data;
    importClassOptions.returnFile =isReturnFile;
    await this.importService.importClassResponseAsync(importClassOptions).then(async (res) => {
      this.importWizardClass.isConfirmed=true;  
      let response = res.body?.result?.data ?? [];
      response = await this.dateTimeColumnConversion(response, true);
      this.classConfirmResult = response;
      this.isClassConfirm = true;
      if(isReturnFile){
        this.createAndDownloadCSVFile(res,'ClassResponseData');
      }    
      this._alertService.successToast("Class Data Imported Successfully");
    })
  }

  async dateTimeColumnConversion(data : ImportDatum_Class_VM[], isConvertToUTC :  boolean){
    for (const row of data) {
      for (const key in row) {
        if (this.dateTimeColumns.some(x=>x.toLocaleLowerCase() == key.toLocaleLowerCase())) {
          const date = new Date(row[key]);
          if (!isNaN(date.getTime())) { 
          const convertedDate = isConvertToUTC ? this.convertUtcToLocal(date) : this.convertLocalToUtc(date);
          row[key] = await this.getTransformedDateAsync(convertedDate);
          }
        }
      }
    }
    return data;
  }

  convertLocalToUtc(localDate: Date): Date {
    const utcDate = new Date(
        localDate.getUTCFullYear(),
        localDate.getUTCMonth(),
        localDate.getUTCDate(),
        localDate.getUTCHours(),
        localDate.getUTCMinutes(),
        localDate.getUTCSeconds(),
        localDate.getUTCMilliseconds()
    );
    return utcDate;
  }
  
  checkNavigationAllowed(){
    switch(this.currentIndex){
      case this.iLAImportIndex :
        return this.isNavFromILAAllowed();
      case this.employeeImportIndex :
        return this.isNavFromEMPAllowed();
      case this.classImportIndex :
        return this.isNavFromClassAllowed();
    }
    return false;
  }

  isNavFromEMPAllowed(){
    return (!this.isEmployeeValidate || this.isEmployeeConfirm);
  }
  isNavFromILAAllowed(){
    return (!this.isILAValidate || this.isILAConfirm);
  }
  isNavFromClassAllowed(){
    return (!this.isClassValidate || this.isClassConfirm);
  }

  public async createAndDownloadCSVFile(response: any, dataFileName: string) {
    const contentDispositionHeader = response.headers.get(
      'content-disposition'
    );
    const defaultFileName = dataFileName;
    let fileName = defaultFileName;

    if (contentDispositionHeader) {
      const match = contentDispositionHeader.match(
        /filename=['"]?([^'";]+)['"]?/
      );
      fileName = match ? match[1] : defaultFileName;
    }
    var file= response.body?.result?.returnedFile;
      if (file) {
        const contentType = 'text/csv'; // Specify the content type of the file
        const byteCharacters = atob(file);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
          byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: contentType });
        fileName = await this.dynamiclabelPipe.transform(fileName);
        saveAs(blob, fileName);
      }
  }

  setClassImportIndex() {
    if(this.isILAImport == 'true') { this.iLAImportIndex++;}
    if(this.isEmployeeImport == 'true') { this.employeeImportIndex = this.iLAImportIndex + 1;}
    if(this.isClassImport == 'true') { this.classImportIndex = this.isEmployeeImport != 'true' ? this.iLAImportIndex + 1 : this.employeeImportIndex + 1;}
  }

}
