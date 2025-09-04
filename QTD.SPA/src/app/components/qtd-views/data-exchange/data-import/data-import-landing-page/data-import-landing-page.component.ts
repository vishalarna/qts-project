import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-data-import-landing-page',
  templateUrl: './data-import-landing-page.component.html',
  styleUrls: ['./data-import-landing-page.component.scss']
})
export class DataImportLandingPageComponent implements OnInit {
  dataImportForm: UntypedFormGroup;

  constructor(private formBuilder: UntypedFormBuilder, private router : Router ) { }

  ngOnInit(): void {
    this.initializeDataImportForm();
  }

  initializeDataImportForm(){
    this.dataImportForm = this.formBuilder.group({     
      isEmployeeData: [false],
      isILAData: [false],
      isClassRecordData: [false]
    });
  }
  continueClick(){
    this.router.navigate(["/data-exchange/import/class"],{queryParams: { 
      isEmployeeImport : this.dataImportForm.get('isEmployeeData')?.value,
      isILAImport : this.dataImportForm.get('isILAData')?.value,
      isClassImport : this.dataImportForm.get('isClassRecordData')?.value,
    }});
  }

  isSelectedAnyOne(){
    return this.dataImportForm.get("isEmployeeData")?.value == false && this.dataImportForm.get("isILAData")?.value == false && this.dataImportForm.get("isClassRecordData")?.value == false;
  }
}
