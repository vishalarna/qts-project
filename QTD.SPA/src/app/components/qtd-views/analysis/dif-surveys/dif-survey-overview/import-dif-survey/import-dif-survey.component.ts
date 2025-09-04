import { Component, OnInit, ViewChild } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { ImportService } from 'src/app/_Services/QTD/import.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { ActivatedRoute } from '@angular/router';
import { ValidateCSV_DIFSurveyEmployeeResponse_Results_VM } from '@models/DIFSurveyImport/ValidateCSV_DIFSurveyEmployeeResponse_Results_VM';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { ImportCsvWizardComponent } from 'src/app/components/shared/import-csv-wizard/import-csv-wizard.component';
import { ImportData_DIFSurveyEmployeeResponse_VM } from '@models/DIFSurveyImport/ImportData_DIFSurveyEmployeeResponse_VM';
import saveAs from 'file-saver';
import { ImportDatum_DIFSurveyEmployeeResponse_VM } from '@models/DIFSurveyImport/ImportDatum_DIFSurveyEmployeeResponse_VM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-import-dif-survey',
  templateUrl: './import-dif-survey.component.html',
  styleUrls: ['./import-dif-survey.component.scss']
})
export class ImportDifSurveyComponent implements OnInit {
  difSurveyId: string;
  validationResult: ValidateCSV_DIFSurveyEmployeeResponse_Results_VM = new ValidateCSV_DIFSurveyEmployeeResponse_Results_VM();
  confirmResult:ImportDatum_DIFSurveyEmployeeResponse_VM[]=[];
  uploadColumns:string[]=[];
  type:string;
  template:any;
  @ViewChild('importWizard') importWizard!:ImportCsvWizardComponent;
 
  constructor(
    private translate: TranslateService,
    private databroadcastService: DataBroadcastService,
    private store: Store<{ toggle: string }>,
    private route: ActivatedRoute,
    private importService: ImportService,
    private _alertService: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
    this.databroadcastService.ShowMenuSideBar.next(false);
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.difSurveyId = params['id'];
    });
    this.type='DIF Survey Employee Response Data';
    this.uploadColumns = ["EmployeeNumber", "TaskNumber", "Difficulty", "Importance", "Frequency", "NA", "Comments"];
    this.getTemplateAsync();
  }
  
  async getTemplateAsync(){
    await this.importService.getTemplateAsync(this.type).then((res:any) => {
      this.template=res.body;
    });
  }
  async getUploadedFile(event:any){
    const formData = new FormData();
    formData.append('difSurveyId',`${this.difSurveyId}`);
    formData.append('file', event);
    formData.append('returnFile', "true");
    await this.importService.validateCSVAsync(formData).then((res) => {
      this.importWizard.validationResult = res;
      this.validationResult=res;
      if(res.fileValidationErrors?.length>0){
        this._alertService.errorAlert("Invalid File Data",res.fileValidationErrors.map(x=>x.error).join("\n"));
        this.importWizard.isImportVisible=true;
      }
      else{
        this.importWizard.isImportVisible=false;
      }
    })
  }

  async OnConfirmUpload(isReturnFile: boolean){
    var importOptions = new ImportData_DIFSurveyEmployeeResponse_VM();
    importOptions.difSurveyId=this.difSurveyId;
    importOptions.data= this.validationResult.data;
    importOptions.returnFile =isReturnFile;
    await this.importService.importDIFSurveyEmployeeResponseAsync(importOptions).then(async (res) => {
      this.importWizard.isConfirmed=true;  
      this.confirmResult=res.body?.result?.data;
      if(isReturnFile){
        this.createAndDownloadCSVFile(res);
      }    
      this._alertService.successToast("Dif Survey " + await this.labelPipe.transform('Employee') + " Response Data Imported Successfully");
    })
  }

  public createAndDownloadCSVFile(response: any) {
    const contentDispositionHeader = response.headers.get(
      'content-disposition'
    );
    const defaultFileName = 'DIFSurveyEmployeeResponseData';
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
        saveAs(blob, fileName);
      }
  }

}
