import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import parse from 'node-html-parser';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { QuestionBankCreateOptions } from 'src/app/_DtoModels/QuestionBank/QuestionBankCreateOptions';
import { QuestionBankService } from 'src/app/_Services/QTD/question-bank.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
@Component({
  selector: 'app-fly-panel-add-question',
  templateUrl: './fly-panel-add-question.component.html',
  styleUrls: ['./fly-panel-add-question.component.scss']
})
export class FlyPanelAddQuestionComponent implements OnInit {
  @Input() mode: 'Add' | 'Edit' | 'Copy' = 'Add';
  @Input() questionId = 0;
  dateError = false;
  data:any;
  questionForm: UntypedFormGroup = new UntypedFormGroup({});
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  ratingScaleList: any[] = [];
  editor = ckcustomBuild;
  showSpinner = false;
  constructor(private fb: UntypedFormBuilder,
    private questionBankService: QuestionBankService,
    private alert: SweetAlertService) { }

  ngOnInit(): void 
  {
    if(this.questionId != 0)
    {
      this.readyQuestionFormWithData();
    }
    this.readyQuestionForm();
  }
  closedefinition() {
    // this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }
  readyQuestionForm() {
    this.questionForm = this.fb.group({
      questionStem: new UntypedFormControl('', Validators.required),
      
    });
  }
  readyQuestionFormWithData()
  {
    this.questionBankService.get(this.questionId).then((res)=>
    {
      
      this.data = res;
      this.questionForm.get('questionStem')?.setValue(res.stem);
    })
  }

  saveQuestion()
  {
    this.showSpinner = true;
    var options = new QuestionBankCreateOptions()
    options.stem = this.questionForm.get('questionStem')?.value;
    options.mode = this.mode;
    if (this.mode === 'Copy') 
    {
      //var p = parse(options.stem).innerText
      options.stem = this.data.stem.trim().toLowerCase() == options.stem.trim().toLowerCase()
        ? parse(options.stem).getElementsByTagName('p')+("-Copy") : options.stem;
    }
    this.questionBankService.create(options).then((res: any) => {
      this.showSpinner = false;
      if(this.mode === 'Add')
      {
        this.alert.successToast('Question Created successfully');
      }
      else
      {
        this.alert.successToast('Question Copied successfully');
      }

    })
    .finally(() => {
      this.showSpinner = false;
        this.closed.emit();
        this.refresh.emit();
    });
  }
  updateQuestion()
    {
      var options = new QuestionBankCreateOptions()
      options.stem = this.questionForm.get('questionStem')?.value;
      options.mode = this.mode;
      this.questionBankService.update(this.questionId,options).then((res: any) => 
      {
          this.alert.successToast('Question Updated successfully');
      })
      .catch(() => {
        this.alert.errorToast('Error updating question');
      })
      .finally(() => {
          this.closed.emit();
          this.refresh.emit();
      });
    }
}
