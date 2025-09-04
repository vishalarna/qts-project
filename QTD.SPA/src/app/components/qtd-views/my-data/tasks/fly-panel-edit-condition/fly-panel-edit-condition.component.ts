import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TaskSpecificUpdateOptions } from 'src/app/_DtoModels/Task/TaskSpecificUpdateOptions';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-edit-condition',
  templateUrl: './fly-panel-edit-condition.component.html',
  styleUrls: ['./fly-panel-edit-condition.component.scss']
})
export class FlyPanelEditConditionComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  editor = ckcustomBuild;
  @Input() taskId = "";
  @Input() data = "";
  @Output() refresh = new EventEmitter<any>();
  showSpinner: boolean = false;

  conditionForm = new UntypedFormGroup({});
  constructor(
    private taskService : TasksService,
    private alert : SweetAlertService,
  ) { }

  ngOnInit(): void {
    this.readyFormData();
  }

  readyFormData(){
    this.conditionForm.addControl('description',new UntypedFormControl(this.data,Validators.required));
  }

  async updateData(){
    var options = new TaskSpecificUpdateOptions();
    this.showSpinner = true;
    options.field = "conditions";
    options.value = this.conditionForm.get('description')?.value;
    this.taskService.updateSpecific(this.taskId,options).then((_)=>{
      this.alert.successToast("Conditions Updated Successfully");
      this.refresh.emit(options.value);
    }).finally(() => {
      this.showSpinner = false;
    });;
  }
}
