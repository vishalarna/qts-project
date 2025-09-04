import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { EMPSettingCBTCreationOptions } from 'src/app/_DtoModels/SchedulesClassses/training-creation-options';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';


@Component({
  selector: 'app-ila-emp-cbt-settings',
  templateUrl: './ila-emp-cbt-settings.component.html',
  styleUrls: ['./ila-emp-cbt-settings.component.scss']
})
export class IlaEmpCbtSettingsComponent implements OnInit {
  @Input() ilaId = "";
  constructor(
    private ilaService: IlaService,
    private alert: SweetAlertService,
    private saveStore: Store<{ saveIla: any }>,

  ) { }

  ngOnInit(): void {

    this.saveStore.select('saveIla').pipe().subscribe((res) => {
      if (res['saveData']['result'] !== undefined && res['tabIndex'] === 1) {
        this.ilaId = res['saveData']['result']['id'];
      }
    });
    this.getCBTReleaseByILAId();

  }
  public Editor = ckcustomBuild;

  cbtReleaseCheckNUll: boolean = false;
  numberOfDays:any[] = [1,2,3,4,5,6,7,8,9,10];

  trainingForm: UntypedFormGroup = new UntypedFormGroup({
    // EMP CBT Release
    cbtPK: new UntypedFormControl(null),
    cbtRequiredForCource: new UntypedFormControl(false),
    releaseCBTLearningContract: new UntypedFormControl(false),
    cbtLearningContractInstructions: new UntypedFormControl(''),
    makeAvailableOnClassStartDate: new UntypedFormControl(false),
    makeAvailableOnClassEndDate: new UntypedFormControl(false),
    makeAvailableAfterPretestCompleted: new UntypedFormControl(false),
    cbtDueDate: new UntypedFormControl(0),
  });


  onReady(editor: any) {
    // 
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  keyPressNumbers(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }


  getCBTReleaseByILAId() {
    this.ilaService.getIlaCBT(this.ilaId).then((res: any) => {
      if (res !== null) {
        this.patchCBTRelease(res);
      } else {
        this.cbtReleaseCheckNUll = true;
      }

    });
  }
  async patchCBTRelease(values) {
    this.trainingForm.patchValue({
      cbtPK: values?.id,
      cbtRequiredForCource: values.cbtRequiredForCource,
      releaseCBTLearningContract: values.releaseCBTLearningContract,
      cbtLearningContractInstructions: values.cbtLearningContractInstructions,
      makeAvailableOnClassStartDate: values.makeAvailableOnClassStartDate,
      makeAvailableOnClassEndDate: values.makeAvailableOnClassEndDate,
      makeAvailableAfterPretestCompleted: values.makeAvailableAfterPretestCompleted,
      cbtDueDate: values.cbtDueDate,
    });
  }
  async saveInfoCBTRelease() {
    let createOpt: EMPSettingCBTCreationOptions = {
      ilaId: this.ilaId,
      cbtDueDate: this.trainingForm.get('cbtDueDate')?.value,
      cbtLearningContractInstructions: this.trainingForm.get('cbtLearningContractInstructions')?.value,
      cbtRequiredForCource: this.trainingForm.get('cbtRequiredForCource')?.value,
      makeAvailableAfterPretestCompleted: this.trainingForm.get('makeAvailableAfterPretestCompleted')?.value,
      makeAvailableOnClassEndDate: this.trainingForm.get('makeAvailableOnClassEndDate')?.value,
      makeAvailableOnClassStartDate: this.trainingForm.get('makeAvailableOnClassStartDate')?.value,
      releaseCBTLearningContract: this.trainingForm.get('releaseCBTLearningContract')?.value

    };
    if (!this.cbtReleaseCheckNUll) {
      this.ilaService.updateIlaCBT(this.trainingForm.get('cbtPK')?.value, createOpt).then((res) => {
        this.patchCBTRelease(res);
        this.alert.successToast(`cbt has been updated `);
      }).catch((res: any) => {
        
        
        this.alert.errorToast(res);
      })
    }
    else {
      this.ilaService.createIlaCBT(createOpt).then((res) => {
        this.patchCBTRelease(res);
        this.alert.successToast(`Cbt has been created `);
      }).catch((res: any) => {
        
        
        this.alert.errorToast(res);
      })
    }

  }



}
