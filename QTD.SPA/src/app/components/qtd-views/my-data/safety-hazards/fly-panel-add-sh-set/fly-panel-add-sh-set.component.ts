import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { SafetyHazard_Set } from 'src/app/_DtoModels/SaftyHazard_Set/SafetyHazard_Set';
import { SaftyHazard_SetCreateOptions } from 'src/app/_DtoModels/SaftyHazard_Set/SaftyHazard_SetCreateOptions';
import { SafetyHazardSetService } from 'src/app/_Services/QTD/safety-hazard-set.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-add-sh-set',
  templateUrl: './fly-panel-add-sh-set.component.html',
  styleUrls: ['./fly-panel-add-sh-set.component.scss']
})
export class FlyPanelAddShSetComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() shSet: SafetyHazard_Set | undefined;
  @Output() refresh = new EventEmitter();
  Editor = ckcustomBuild;
  subscription = new SubSink();
  setForm: UntypedFormGroup = new UntypedFormGroup({});
  shId = "";
  showSpinner = false;

  constructor(
    public flyPanelService: FlyInPanelService,
    private route: ActivatedRoute,
    private shSetService: SafetyHazardSetService,
    private alert : SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.readySetForm();
    if (this.shSet !== undefined) {
      this.insertSetData();
    }
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  readySetForm() {
    this.setForm.addControl(`Abatement`, new UntypedFormControl(''));
    this.setForm.addControl(`Control`, new UntypedFormControl(''));
    this.setForm.addControl(`addAnother`, new UntypedFormControl(false));
  }

  insertSetData() {
    this.setForm.get('Abatement')?.setValue(this.shSet?.safetyHzAbatementText);
    this.setForm.get('Control')?.setValue(this.shSet?.safetyHzControlsText);
  }

  async saveSet() {
    this.showSpinner = true;
    var options = new SaftyHazard_SetCreateOptions();
    options.safetyHzControlsText = this.setForm.get('Control')?.value;
    options.safetyHzAbatementText = this.setForm.get('Abatement')?.value;
    this.shSetService.createAndLink(this.shId, options).then(async (res: any) => {
      if (this.setForm.get('addAnother')?.value) {
        this.resetFormValues();
      }
      else {
        this.flyPanelService.close();
      }
      this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard")} Set Saved Successfully`);
      this.refresh.emit();
    }).finally(() => {
      this.showSpinner = false;
    });
  }

  resetFormValues() {
    this.setForm.get('Abatement')?.setValue("");
    this.setForm.get('Control')?.setValue('');
    this.setForm.get('addAnother')?.setValue(false);
  }

  async updateData() {
    this.showSpinner = true;
    var options = new SaftyHazard_SetCreateOptions();
    options.safetyHzControlsText = this.setForm.get('Control')?.value;
    options.safetyHzAbatementText = this.setForm.get('Abatement')?.value;
    this.shSetService.update(this.shSet?.id, options).then(async (res: any) => {
      this.flyPanelService.close();
      this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard")} Set Updated Successfully`);
      this.refresh.emit();
    }).finally(() => {
      this.showSpinner = false;
    })
  }

}
