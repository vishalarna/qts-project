import {Component,Input,OnDestroy,OnInit} from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TrainingProgramReview_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_ViewModel';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';

@Component({
  selector: 'app-review-evaluation',
  templateUrl: './review-evaluation.component.html',
  styleUrls: ['./review-evaluation.component.scss'],
})
export class ReviewEvaluationComponent implements OnInit, OnDestroy {

  @Input() inputTPRViewModel: TrainingProgramReview_ViewModel;
  @Input() handleLoad: () => void | undefined;
  reviewEvaluationForm!: UntypedFormGroup;
  public Editor = ckcustomBuild;
  public configCKEditor = {
    toolbar: ["|","heading","bold","italic","strikethrough","underline","link","|","outdent","indent","bulletedList","numberedList","|","insertTable","undo","redo"],
  };
  
  constructor(
    private fb: UntypedFormBuilder,
  ) {}
  
  ngOnInit(): void {
    this.initializeReviewEvaluationForm();
    this.load();
  }
  ngOnDestroy(): void {
  }

  _handleLoad() {
    if (this.handleLoad &&typeof this.handleLoad === 'function') {
      this.handleLoad();
    }
  }

  load() {
    this._handleLoad();
  }

  initializeReviewEvaluationForm() {
    this.reviewEvaluationForm = this.fb.group({
      evaluationOfTraineeLearning: new UntypedFormControl(this.inputTPRViewModel?.evaluationOfTraineeLearning),
      studentEvaluationResults: new UntypedFormControl(this.inputTPRViewModel?.studentEvaluationResults),
    });
  }

  evalOfTraineeLearningChange() {
    this.inputTPRViewModel.evaluationOfTraineeLearning = this.reviewEvaluationForm.get('evaluationOfTraineeLearning').value;
  }
  
  studentEvalResultsChange(){
    this.inputTPRViewModel.studentEvaluationResults = this.reviewEvaluationForm.get('studentEvaluationResults').value;
  }
 
}
