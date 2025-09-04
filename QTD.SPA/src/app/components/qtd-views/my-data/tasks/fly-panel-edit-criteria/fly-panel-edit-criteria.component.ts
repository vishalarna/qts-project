import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TaskSpecificUpdateOptions } from 'src/app/_DtoModels/Task/TaskSpecificUpdateOptions';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-edit-criteria',
  templateUrl: './fly-panel-edit-criteria.component.html',
  styleUrls: ['./fly-panel-edit-criteria.component.scss']
})
export class FlyPanelEditCriteriaComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() taskId = "";
  @Input() data = "";
  editor = ckcustomBuild;
  showSpinner = false;

  criteriaForm = new UntypedFormGroup({});

  constructor(
    private taskService : TasksService,
    private alert : SweetAlertService,
  ) { }

  ngOnInit(): void {
    this.readyFormData();
  }

  readyFormData(){
    this.criteriaForm.addControl('description',new UntypedFormControl(this.data,Validators.required));
  }

  async updateData(){
    this.showSpinner = true;
    var options = new TaskSpecificUpdateOptions();
    options.field = "criteria";
    options.value = this.criteriaForm.get('description')?.value;
    
    this.taskService.updateSpecific(this.taskId,options).then((_)=>{
      this.showSpinner = false;
      this.alert.successToast("Criteria Updated Successfully");
      this.refresh.emit(options.value);
    }).finally(() => {
      this.showSpinner = false;
    });
  }
}
