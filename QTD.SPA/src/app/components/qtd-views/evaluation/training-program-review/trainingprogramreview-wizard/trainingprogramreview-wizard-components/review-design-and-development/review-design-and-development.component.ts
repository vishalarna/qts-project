import {Component,Input,OnDestroy,OnInit} from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TrainingProgramReview_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_ViewModel';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';

@Component({
  selector: 'app-review-design-and-development',
  templateUrl: './review-design-and-development.component.html',
  styleUrls: ['./review-design-and-development.component.scss'],
})
export class ReviewDesignAndDevelopmentComponent implements OnInit, OnDestroy {

  @Input() inputTPRViewModel: TrainingProgramReview_ViewModel;
  @Input() handleLoad: () => void | undefined;
  reviewDndForm!: UntypedFormGroup;
  public Editor = ckcustomBuild;
  public configCKEditor = {
    toolbar: ["|","heading","bold","italic","strikethrough","underline","link","|","outdent","indent","bulletedList","numberedList","|","insertTable","undo","redo"],
  };
  
  constructor(
    private fb: UntypedFormBuilder,
  ) {}
  
  ngOnInit(): void {
    this.initializeReviewDndForm();
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

  initializeReviewDndForm() {
    this.reviewDndForm = this.fb.group({
      programDesign: new UntypedFormControl(this.inputTPRViewModel?.programDesign),
      programMaterials: new UntypedFormControl(this.inputTPRViewModel?.programMaterials),
      programImplementation: new UntypedFormControl(this.inputTPRViewModel?.programImplementation),
    });
  }

  programDesignChange() {
    this.inputTPRViewModel.programDesign = this.reviewDndForm.get('programDesign').value;
  }
  programMaterialsChange(){
    this.inputTPRViewModel.programMaterials = this.reviewDndForm.get('programMaterials').value;
  }
  programImplementationChange(){
    this.inputTPRViewModel.programImplementation = this.reviewDndForm.get('programImplementation').value;
  }
 
}
