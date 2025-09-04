import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { CoverSheetCreateOptions } from 'src/app/_DtoModels/CoverSheet/CoverSheetCreateOptions';
import { CoverSheetTypeCreateOptions } from 'src/app/_DtoModels/CoverSheetType/CoverSheetTypeCreateOptions';
import { CoversheetTypeService } from 'src/app/_Services/QTD/coversheet-type.service';
import { CoversheetService } from 'src/app/_Services/QTD/coversheet.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-coversheets',
  templateUrl: './fly-panel-add-coversheets.component.html',
  styleUrls: ['./fly-panel-add-coversheets.component.scss']
})
export class FlyPanelAddCoversheetsComponent implements OnInit {

  @Input() Title: string;
  addCoverSheetForm!: UntypedFormGroup;
  typeList: any[] = [];
  selectedId:any;
  public Editor = ckcustomBuild;
  instructions = "";

  constructor(public flyPanelSrvc: FlyInPanelService,
              private fb: UntypedFormBuilder,
              private coverSheetTypeService:CoversheetTypeService,
              private alert:SweetAlertService,
              private coverSheetService:CoversheetService) { }

  ngOnInit(): void {
    this.readyCoverSheetForm();
    this.getCoversheetTypes();
  }

  AddCoverSheetTemplate() {
    var options = new CoverSheetCreateOptions();
    options.coversheetTitle = this.addCoverSheetForm.get('Title')?.value;
    options.coversheetTypeId = this.selectedId;
    options.coversheetInstructions = this.addCoverSheetForm.get("Instructions")?.value;
    this.coverSheetService.create(options).then((res:any)=>{
      this.alert.successToast("Cover Sheet Type Added to list");
      this.addCoverSheetForm.reset();
      this.getCoversheetTypes();
    }).catch((err)=>{
      this.alert.errorToast("Error Adding Cover Sheet Type");
    })
  }

  selectType(id:any){
    this.selectedId = id.value;
  }

  readyCoverSheetForm()
	{
		this.addCoverSheetForm = this.fb.group({
			Title: new UntypedFormControl(this.Title, [
			  Validators.required,
			]),
      Instructions:new UntypedFormControl("",Validators.required)
	  });
  }

  async getCoversheetTypes() {
    await this.coverSheetTypeService.getAll().then((res:any)=>{
      this.typeList = res;
    }).catch((err:any)=>{
      console.error(err);
    })
  }
}   
