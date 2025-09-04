import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { CoverSheet } from 'src/app/_DtoModels/CoverSheet/CoverSheet';
import { CoversheetService } from 'src/app/_Services/QTD/coversheet.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-coversheets',
  templateUrl: './fly-panel-coversheets.component.html',
  styleUrls: ['./fly-panel-coversheets.component.scss']
})
export class FlyPanelCoversheetsComponent implements OnInit {

  coversheetsList : CoverSheet[] = [];
  coverForm = new UntypedFormGroup({
    cover:new UntypedFormControl(null,Validators.required)
  });

  @Output() coverSelected = new EventEmitter<CoverSheet>();

  constructor(
              public flyPanelSrvc: FlyInPanelService,
              private coverSheetService:CoversheetService,
              private alert:SweetAlertService,
              ) { }

  ngOnInit(): void {
    this.getCoversheets();
  }

  async getCoversheets() {
    // this.coversheetsList = [
    //   {id: 1, coversheetName: 'Generic OJT'},
    //   {id: 2, coversheetName: 'Generic TQ'},
    //   {id: 3, coversheetName: 'System Operator Initial Training TQ'},
    //   {id: 4, coversheetName: 'Lead System Operator Initial Training TQ'},

    // ];

    await this.coverSheetService.getAll().then((res:any)=>{
      this.coversheetsList = res;
    }).catch((err)=>{
      this.alert.errorToast("Error Fetching Cover Sheet");
    })
  }

  Save(){
    this.flyPanelSrvc.close();
    var sheet = this.coverForm.get('cover')?.value as CoverSheet;
    this.coverSelected.emit(sheet);
  }

}
