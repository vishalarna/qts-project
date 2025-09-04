import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { Task_Suggestion } from 'src/app/_DtoModels/Task_Suggestion/Task_Suggestion';
import { TaskSuggestionOptions } from 'src/app/_DtoModels/Task_Suggestion/Task_SuggestionOptions';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-add-suggestion',
  templateUrl: './fly-panel-add-suggestion.component.html',
  styleUrls: ['./fly-panel-add-suggestion.component.scss']
})
export class FlyPanelAddSuggestionComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() taskId = "";
  @Input() editSuggestion : Task_Suggestion | undefined;
  editor = ckcustomBuild;
  anotherCheck:boolean=false;

  description = "";
  number : number = 0;
  spinner = false;

  constructor(
    private taskService : TasksService,
    private alert :SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.editSuggestion === undefined ? this.readyNumber():this.readyEditData();
  }

  async readyNumber(){
    this.number = await this.taskService.getSuggestionNumber(this.taskId);
    
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  readyEditData(){
    this.number = this.editSuggestion?.number ?? 0;
    this.description = this.editSuggestion?.description ?? "";
  }

  async saveSuggestion(){
    this.spinner = true;
    var options = new TaskSuggestionOptions();
    options.description = this.description;
    options.taskId = this.taskId;
    await this.taskService.createSuggestion(this.taskId,options).then(async (res)=>{
      if(res !== undefined && this.anotherCheck === true){
        this.alert.successToast(await this.transformTitle('Task') + " Specific Suggestion Created");
        this.anotherCheck=false;
        this.readyNumber();
        this.description = "";
      }
      else{
        this.alert.successToast(await this.transformTitle('Task') + " Specific Suggestion Created");
        this.closed.emit();
      }this.refresh.emit();
    }).finally(()=>{
      this.spinner = false;
    })
  }

  async updateSuggestion(){
    this.spinner = true;
    var options = new TaskSuggestionOptions();
    options.description = this.description;
    options.taskId = this.taskId
    await this.taskService.updateSuggestion(this.taskId, this.editSuggestion?.id,options).then((_)=>{
      this.alert.successToast("Suggestion Updated");
      this.refresh.emit();
      this.closed.emit();
    }).finally(()=>{
      this.spinner = false;
    })
  }

}
