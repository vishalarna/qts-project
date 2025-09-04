import { Component, OnInit, Output,EventEmitter, Input } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { QuestionCreateOptions } from 'src/app/_DtoModels/Task_Question/QuestionCreateOptions';
import { Task_Question } from 'src/app/_DtoModels/Task_Question/Task_Question';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-question',
  templateUrl: './fly-panel-add-question.component.html',
  styleUrls: ['./fly-panel-add-question.component.scss']
})
export class FlyPanelAddQuestionComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() taskId = "";
  @Input() editQuestion: Task_Question | undefined;
  editor = ckcustomBuild;
  quesNum:number = 0;
  spinner = false;
  anotherCheck:boolean=false;


  questionForm = new UntypedFormGroup({});
  constructor(
    private taskService : TasksService,
    private alert : SweetAlertService,
  ) { }

  ngOnInit(): void {
    this.editQuestion === undefined ? this.readyQuestionForm() : this.readyEditData();
  }

  async readyQuestionForm(){
    this.readyForm();

    await this.taskService.getTaskQuestionNumber(this.taskId).then((res:number)=>{
      this.questionForm.get('num')?.setValue(res);
    });
  }

  readyForm(){
    this.questionForm.addControl('description',new UntypedFormControl('',Validators.required));
    this.questionForm.addControl('answer',new UntypedFormControl('',Validators.required));
    this.questionForm.addControl('num',new UntypedFormControl({value:0,disabled:true}));
    this.questionForm.addControl('anotherCheck',new UntypedFormControl(false));
  }

  readyEditData(){
    this.readyForm();
    this.questionForm.get('description')?.setValue(this.editQuestion?.question);
    this.questionForm.get('answer')?.setValue(this.editQuestion?.answer);
    this.questionForm.get('num')?.setValue(this.editQuestion?.questionNumber);
  }

  async saveQuestion(){
    this.spinner = true;
    var options = new QuestionCreateOptions();
    options.answer = this.questionForm.get('answer')?.value;
    options.question = this.questionForm.get('description')?.value;
    options.questionNumber = this.questionForm.get('num')?.value;
    this.taskService.addQuestion(this.taskId,options).then((res)=>{
      
      if (this.questionForm.get('anotherCheck')?.value === true){
        this.alert.successToast("Question and Answer Added Successfully");
        this.readyQuestionForm();
        this.questionForm.reset();
      }else{
        this.alert.successToast("Question and Answer Added Successfully");
        this.closed.emit();
      }
      this.refresh.emit();
    }).finally(()=>{
      this.spinner=false;
    })
  }

  async updateQuestion(){
    this.spinner = true;
    var options = new QuestionCreateOptions();
    options.question = this.questionForm.get('description')?.value;
    options.answer = this.questionForm.get('answer')?.value;

    this.taskService.updateQuestion(this.taskId,this.editQuestion?.id,options).then((_)=>{
      this.alert.successToast("Question Data Successfully Updated");
      this.refresh.emit();
      this.closed.emit();
    }).finally(()=>{
      this.spinner = false;
    });
  }

}
