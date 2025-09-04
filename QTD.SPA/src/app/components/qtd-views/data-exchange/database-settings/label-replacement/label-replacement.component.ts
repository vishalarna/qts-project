import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormArray, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import {
  ClientSettings_LabelReplacement
} from 'src/app/_DtoModels/ClientSettingsLabelReplacement/ClientSettings_LabelReplacement';
import {
  ClientSettings_LabelReplacement_UpdateOptions
} from "../../../../../_DtoModels/ClientSettingsLabelReplacement/ClientSettings_LabelReplacement_UpdateOptions";
import { Observable, Subscription } from "rxjs";
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-label-replacement',
  templateUrl: './label-replacement.component.html',
  styleUrls: ['./label-replacement.component.scss']
})
export class LabelReplacementComponent implements OnInit {
  @Input()
  ClientSettings_Labels: Array<ClientSettings_LabelReplacement>;
  @Input() completeEvent: Observable<void>;
  @Output()
  OnSaveClickedEvent: EventEmitter<any> = new EventEmitter();
  @Output()
  OnCancelClickEvent: EventEmitter<any> = new EventEmitter();
  @Output()
  OnLabelChangedEvent: EventEmitter<any> = new EventEmitter();
  public mode: string;
  private ClientSettings_LabelsUpdateOption: ClientSettings_LabelReplacement_UpdateOptions = new ClientSettings_LabelReplacement_UpdateOptions();
  public labelReplacementForm: UntypedFormGroup;

  private completeSubscription: Subscription;

  constructor(private fb: UntypedFormBuilder) {
  }

  ngOnInit(): void {
    this.mode = 'read';
    this.getLabelReplacementForm();
    this.labelReplacementForm.disable();
    const self = this;
    this.completeSubscription = this.completeEvent.subscribe((clientSettings_Labels : any) => {
      self.ClientSettings_Labels = clientSettings_Labels;
      self.getLabelReplacementForm();
      self.mode = 'read'
      self.labelReplacementForm.disable();
    })
  }

  getLabelReplacementForm() {
    this.labelReplacementForm = this.fb.group({
      labels: this.fb.array([
        this.fb.group({
          defaultLabel: [null, Validators.required],
          replacementLabel: [null, Validators.required],
        })
      ])
    })
    this.addLabelDataToForm();
  }

  addLabelDataToForm() {
    const control = this.labelReplacementForm.get('labels') as UntypedFormArray;
    if (control.length === 1) {
      control.removeAt(0);
    }
    this.ClientSettings_Labels.forEach((value) => {
      let label = this.fb.group({
        defaultLabel: [value.defaultLabel, Validators.required],
        replacementLabel: [value.labelReplacement, Validators.required]
      });
      (<UntypedFormArray>this.labelReplacementForm.get('labels')).push(label);
    });
  }

  OnSaveButtonClick() {
    this.OnSaveClickedEvent.emit(this.ClientSettings_LabelsUpdateOption);
    // this.OnSaveClickedEvent.emit({
    //   data: this.ClientSettings_LabelsUpdateOption,
    //   func : setTimeout(() => {
    //     // this.getLabelReplacementForm();
    //     // this.mode = 'read';
    //     // this.labelReplacementForm.disable();
    //     this.ngOnInit();
    //    }, 1100)
    // })
  }

  OnCancelButtonClick() {
    const control = this.labelReplacementForm.get('labels') as UntypedFormArray;
    while (control.length != 0) {
      control.removeAt(0);
    }
    this.addLabelDataToForm();
    this.mode = 'read';
    this.labelReplacementForm.disable();
  }

  OnEditButtonClick() {
    this.mode = 'write';
    this.labelReplacementForm.enable();
  }

  onLabelReplacementChange(itemName: string, eventValue: string) {
    this.ClientSettings_LabelsUpdateOption.UpdateLabelReplacement(itemName, eventValue);
  }

}
