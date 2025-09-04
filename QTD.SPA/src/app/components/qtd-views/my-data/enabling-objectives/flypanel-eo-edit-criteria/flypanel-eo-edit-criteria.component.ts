import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { EnablingObjectiveSpecificUpdateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveSpecificUpdateOptions';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-eo-edit-criteria',
  templateUrl: './flypanel-eo-edit-criteria.component.html',
  styleUrls: ['./flypanel-eo-edit-criteria.component.scss']
})
export class FlypanelEoEditCriteriaComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() data = "";
  @Input() eoId = "";
  editor = ckcustomBuild;

  criteriaForm = new UntypedFormGroup({});

  constructor(private eoService : EnablingObjectivesService,
    private alert : SweetAlertService,) { }

  ngOnInit(): void {
    this.readyFormData();
  }

  readyFormData(){
    this.criteriaForm.addControl('description',new UntypedFormControl(this.data,Validators.required));
  }

  async updateData(){
    var options = new EnablingObjectiveSpecificUpdateOptions();
    options.field = "criteria";
    options.value = this.criteriaForm.get('description')?.value;
    this.eoService.updateSpecific(this.eoId,options).then((_)=>{
      this.alert.successToast("Criteria Updated Successfully");
      this.refresh.emit(options.value);
      this.closed.emit();
    });
  }

}
