import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { EnablingObjective_Question } from 'src/app/_DtoModels/EnablingObjective_Question/EnablingObjective_Question';
import { EnablingObjective_QuestionCreateOptions } from 'src/app/_DtoModels/EnablingObjective_Question/EnablingObjective_QuestionCreateOptions';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-eo-add-questions',
  templateUrl: './flypanel-eo-add-questions.component.html',
  styleUrls: ['./flypanel-eo-add-questions.component.scss']
})
export class FlypanelEoAddQuestionsComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() eoId = "";
  editor = ckcustomBuild;
  quesNum: number = 0;
  spinner = false;
  questionNumber: any;
  @Input() editQuestion: EnablingObjective_Question | undefined;

  questionForm = new UntypedFormGroup({});
  constructor(private eoService: EnablingObjectivesService,
              private alert : SweetAlertService,) { }

  readyForm(){
    this.questionForm.addControl('description',new UntypedFormControl('',Validators.required));
    this.questionForm.addControl('answer',new UntypedFormControl('',Validators.required));
    this.questionForm.addControl('number',new UntypedFormControl({value:0,disabled:true}));
  }

  ngAfterViewInit(): void {
    
  }

  ngOnInit(): void {
    
    if(this.editQuestion === undefined){
      this.readyQuestionForm();
    }
    else {
      this.readyEditData();
    }
  }

  async readyQuestionForm(){
    this.readyForm();

    await this.eoService.getEOQuestionNumber(this.eoId).then((res:number)=>{
      
      this.questionForm.get('number')?.setValue(res);
      this.questionNumber = res;
    });
  }

  async readyStepNumber(){
    this.quesNum = await this.eoService.getEOQuestionNumber(this.eoId);
  }

  readyEditData(){
    this.readyForm();
    this.questionForm.get('description')?.setValue(this.editQuestion?.question);
    this.questionForm.get('answer')?.setValue(this.editQuestion?.answer);
    this.questionNumber = this.editQuestion?.questionNumber;
  }

  async saveQuestion(){
    this.spinner = true;
    var options = new EnablingObjective_QuestionCreateOptions();
    options.answer = this.questionForm.get('answer')?.value;
    options.question = this.questionForm.get('description')?.value;
    options.questionNumber = this.questionForm.get('number')?.value;
    this.eoService.addQuestion(this.eoId,options).then((_)=>{
      this.alert.successToast("Question and Answer Saved Successfully");
      this.refresh.emit();
    }).finally(()=>{
      this.spinner = false;
    });
  }

  async updateQuestion(){
    this.spinner = true;
    var options = new EnablingObjective_QuestionCreateOptions();
    options.question = this.questionForm.get('description')?.value;
    options.answer = this.questionForm.get('answer')?.value;
    this.eoService.updateQuestion(this.eoId,this.editQuestion?.id,options).then((_)=>{
      this.alert.successToast("Question Data Successfully Updated");
      this.refresh.emit();
    }).finally(()=>{
      this.spinner = false;
    });
  }

}
