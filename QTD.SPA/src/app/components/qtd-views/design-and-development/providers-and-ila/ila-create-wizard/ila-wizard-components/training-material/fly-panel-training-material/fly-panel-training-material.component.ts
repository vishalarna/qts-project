import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ILAResourceCreateOptions } from 'src/app/_DtoModels/ILA/ILAResourcesCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IlaResourcesService } from 'src/app/_Services/QTD/ila-resources.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-training-material',
  templateUrl: './fly-panel-training-material.component.html',
  styleUrls: ['./fly-panel-training-material.component.scss']
})
export class FlyPanelTrainingMaterialComponent implements OnInit {
  public trainingMaterialForm: UntypedFormGroup;
  @Output() saveComplete: EventEmitter<void> = new EventEmitter<void>();
  @Output() close = new EventEmitter<any>();
  @Input() ilaId;
  @Input() editILAResourceData;
  @Input() isEditILAResource;
  constructor( private fb: UntypedFormBuilder,  
    public flyPanelSrvc: FlyInPanelService,
    private ilaResourceService:IlaResourcesService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe) { }

  ngOnInit(): void {
    this.initializeTrainingMaterialForm();
    if(this.isEditILAResource){
      this.setILAResourceValues()
    }
    }

  initializeTrainingMaterialForm() {
    this.trainingMaterialForm = this.fb.group({
      title: new UntypedFormControl(null,Validators.required),
      comments: new UntypedFormControl(null),
    });
  }
  
  setILAResourceValues(){
    this.trainingMaterialForm.get('comments').setValue(this.editILAResourceData?.comments);
    this.trainingMaterialForm.get('title').setValue(this.editILAResourceData?.title);
  }


 async saveClick(event:any){
    const ilaResourcesCreateOption = new ILAResourceCreateOptions(this.trainingMaterialForm.get('title').value,
    this.trainingMaterialForm.get('comments').value);
     await this.ilaResourceService.createAsync(this.ilaId,ilaResourcesCreateOption).then(async (res: any) => {
      this.flyPanelSrvc.close();
      this.alert.successToast( await this.labelPipe.transform('ILA') + " Resource Added Successfully");
      this.notifyTrainingMaterial()
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Adding " +  await this.labelPipe.transform('ILA') + " Resource");
    });
  }

  async updateClick(event:any){
    const ilaResourcesCreateOption = new ILAResourceCreateOptions(this.trainingMaterialForm.get('title').value,
    this.trainingMaterialForm.get('comments').value);
     await this.ilaResourceService.updateAsync(this.ilaId,this.editILAResourceData?.id,ilaResourcesCreateOption).then(async (res: any) => {
      this.flyPanelSrvc.close();
      this.alert.successToast( await this.labelPipe.transform('ILA') + " Resource Updated Successfully");
      this.notifyTrainingMaterial()
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Updating " +  await this.labelPipe.transform('ILA') + " Resource");
    });
  }

  notifyTrainingMaterial() {
    this.saveComplete.emit(); 
  }
}