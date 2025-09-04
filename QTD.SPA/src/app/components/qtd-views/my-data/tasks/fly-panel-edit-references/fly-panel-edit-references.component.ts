import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TaskSpecificUpdateOptions } from 'src/app/_DtoModels/Task/TaskSpecificUpdateOptions';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-edit-references',
  templateUrl: './fly-panel-edit-references.component.html',
  styleUrls: ['./fly-panel-edit-references.component.scss']
})
export class FlyPanelEditReferencesComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() taskId = "";
  @Input() data = "";
  editor = ckcustomBuild;

  referenceForm = new UntypedFormGroup({});

  constructor(
    private taskService : TasksService,
    private alert : SweetAlertService,
  ) { }

  ngOnInit(): void {
    this.readyFormData();
  }

  readyFormData(){
    this.referenceForm.addControl('description',new UntypedFormControl(this.data,Validators.required));
  }

  async updateData(){
    var options = new TaskSpecificUpdateOptions();
    options.field = "references";
    options.value = this.referenceForm.get('description')?.value;
    this.taskService.updateSpecific(this.taskId,options).then((_)=>{
      this.alert.successToast("Reference Updated Successfully");
      this.refresh.emit(options.value);
    });
  }

}
