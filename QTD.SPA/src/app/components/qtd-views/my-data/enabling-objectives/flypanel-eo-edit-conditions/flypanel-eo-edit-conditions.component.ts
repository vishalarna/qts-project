import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { EnablingObjectiveSpecificUpdateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveSpecificUpdateOptions';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-eo-edit-conditions',
  templateUrl: './flypanel-eo-edit-conditions.component.html',
  styleUrls: ['./flypanel-eo-edit-conditions.component.scss']
})
export class FlypanelEoEditConditionsComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  editor = ckcustomBuild;
  @Input() data = "";
  @Input() eoId = "";
  @Output() refresh = new EventEmitter<any>();

  conditionForm = new UntypedFormGroup({});

  constructor(private eoService : EnablingObjectivesService,
    private alert : SweetAlertService,) { }

  ngOnInit(): void {
    this.readyFormData();
  }

  readyFormData() {
    this.conditionForm.addControl('description', new UntypedFormControl(this.data, Validators.required));
  }

  async updateData(){
    var options = new EnablingObjectiveSpecificUpdateOptions();
    options.field = "conditions";
    options.value = this.conditionForm.get('description')?.value;
    this.eoService.updateSpecific(this.eoId,options).then((_)=>{
      this.alert.successToast("Criteria Updated Successfully");
      this.refresh.emit(options.value);
      this.closed.emit();

    });
  }

}
