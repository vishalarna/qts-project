import { AfterViewInit, ViewChild, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CKEditorComponent } from '@ckeditor/ckeditor5-angular';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { Task_Step } from 'src/app/_DtoModels/Task_Step/Task_Step';
import { Task_StepCreateOptions } from 'src/app/_DtoModels/Task_Step/Task_StepCreateOptions';
import { Task_StepUpdateOptions } from 'src/app/_DtoModels/Task_Step/Task_StepUpdateOptions';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-add-step',
  templateUrl: './fly-panel-add-step.component.html',
  styleUrls: ['./fly-panel-add-step.component.scss']
})
export class FlyPanelAddStepComponent implements OnInit,AfterViewInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  editor = ckcustomBuild;
  @Input() taskId = "";
  stepNum:number = 0;
  description = "";
  showSpinner = false;
  @Input() editStep:Task_Step | undefined;
  anotherCheck:boolean=false;

  constructor(
    private taskService: TasksService,
    private alert : SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {

  }

  @ViewChild(CKEditorComponent) ckeditorComponent?: CKEditorComponent;

  onEditorReady(editorInstance: any) {
    console.log("CKEditor instance ready:", editorInstance);

    editorInstance.editing.view.document.on('keydown', (event: any, data: any) => {
      if (data.keyCode === 9) { // Tab key
        data.preventDefault(); // Prevent focus shift

        // Insert a tab character at the cursor position
        editorInstance.model.change((writer: any) => {
          const insertPosition = editorInstance.model.document.selection.getFirstPosition();
          writer.insertText('    ', insertPosition);
        });
      }
    });
  }

  ngAfterViewInit(): void {
    this.editStep?.number === undefined ? this.readyStepNumber() : this.readyEditData();
  }

  async readyStepNumber(){
    this.stepNum = await this.taskService.getStepNumber(this.taskId);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  readyEditData(){
    this.stepNum = this.editStep?.number ?? 0;
    this.description = this.editStep?.description ?? "";
  }

  async saveStep(){
    this.showSpinner = true;
    var options = new Task_StepCreateOptions();
    options.description = this.description;
    options.number = this.stepNum;
    await this.taskService.createSteps(this.taskId,options).then(async (res)=>{
      if (res !== undefined && this.anotherCheck === true){
        this.anotherCheck = false;
        this.alert.successToast(await this.transformTitle('Task') + " Step Created Successfully");
        this.readyStepNumber();
        this.description = "";
      }else if(this.anotherCheck === false){
        this.alert.successToast(await this.transformTitle('Task') +" Step Created Successfully");
        this.closed.emit();
      }this.refresh.emit();

    }).finally(()=>{
      this.showSpinner = false;
    });

  }

  async updateStepData(){
    this.showSpinner = true;
    var options = new Task_StepUpdateOptions();
    options.description = this.description;

    await this.taskService.updateSteps(this.taskId,this.editStep?.id,options).then(async (_)=>{
      this.alert.successToast(await this.transformTitle('Task') + " Step Updated successfully");
      this.refresh.emit();
      this.closed.emit();
    }).finally(()=>{
      this.showSpinner = false;
    })
  }

}
