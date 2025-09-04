import {Component,Input,OnDestroy,OnInit} from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { TrainingProgramReview_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_ViewModel';

@Component({
  selector: 'app-purpose-of-review',
  templateUrl: './purpose-of-review.component.html',
  styleUrls: ['./purpose-of-review.component.scss'],
})
export class PurposeOfReviewComponent implements OnInit, OnDestroy {
  @Input() inputTPRViewModel: TrainingProgramReview_ViewModel;
  @Input() handleLoad: () => void | undefined;
  purposeReviewForm!: UntypedFormGroup;
  public Editor = ckcustomBuild;
  public configCKEditor = {
    toolbar: ["|","heading","bold","italic","strikethrough","underline","link","|","outdent","indent","bulletedList","numberedList","|","insertTable","undo","redo"],
  };
  
  constructor(
    private fb: UntypedFormBuilder,
  ) {}
  
  ngOnInit(): void {
    this.initializePurposeReviewForm();
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

  initializePurposeReviewForm() {
    this.purposeReviewForm = this.fb.group({
      purpose: new UntypedFormControl(this.inputTPRViewModel?.purpose),
      method: new UntypedFormControl(this.inputTPRViewModel?.method),
      historicalBackground: new UntypedFormControl(this.inputTPRViewModel?.historicalBackground),
    });
  }

  purposeChange() {
    this.inputTPRViewModel.purpose = this.purposeReviewForm.get('purpose').value;
  }
  methodChange(){
    this.inputTPRViewModel.method = this.purposeReviewForm.get('method').value;
  }
  historicalBackChange(){
    this.inputTPRViewModel.historicalBackground = this.purposeReviewForm.get('historicalBackground').value;
  }
 
}
