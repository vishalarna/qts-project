import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { EnablingObjective_Step } from 'src/app/_DtoModels/EnablingObjective_Step/EnablingObjective_Step';
import { EnablingObjective_StepCreateOptions } from 'src/app/_DtoModels/EnablingObjective_Step/EnablingObjective_StepCreateOptions';
import { EnablingObjective_StepUpdateOptions } from 'src/app/_DtoModels/EnablingObjective_Step/EnablingObjective_StepUpdateOptions';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-flypanel-eo-add-step',
  templateUrl: './flypanel-eo-add-step.component.html',
  styleUrls: ['./flypanel-eo-add-step.component.scss']
})
export class FlypanelEoAddStepComponent implements OnInit {

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  editor = ckcustomBuild;
  @Input() eoId: any;
  stepNum:number = 0;
  description = "";
  showSpinner = false;
  @Input() editStep:EnablingObjective_Step | undefined;

  constructor(private eoService: EnablingObjectivesService,
    private alert : SweetAlertService, private labelPipe: LabelReplacementPipe) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    

    if(this.editStep?.number === undefined)
    {
      this.readyStepNumber();
    }
    else {
      this.readyEditData();
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async readyStepNumber(){
    this.stepNum = await this.eoService.getStepNumber(this.eoId);
  }

  readyEditData(){
    this.stepNum = this.editStep?.number ?? 0;
    this.description = this.editStep?.description ?? "";
  }

  async saveStep(){
    var options = new EnablingObjective_StepCreateOptions();
    options.description = this.description;
    options.number = this.stepNum;
    await this.eoService.createSteps(this.eoId,options).then(async (_)=>{
      this.alert.successToast(await this.transformTitle('Enabling Objective') + "Step Created Successfully");
      this.refresh.emit();
      this.closed.emit();
    });

  }

  async updateStepData(){
    this.showSpinner = true;
    var options = new EnablingObjective_StepUpdateOptions();
    options.description = this.description;

    await this.eoService.updateSteps(this.eoId,this.editStep?.id,options).then(async (_)=>{
      this.alert.successToast(await this.transformTitle('Task') + " Step Updated successfully");
      this.refresh.emit();
    }).finally(()=>{
      this.showSpinner = false;
    })
  }

}
