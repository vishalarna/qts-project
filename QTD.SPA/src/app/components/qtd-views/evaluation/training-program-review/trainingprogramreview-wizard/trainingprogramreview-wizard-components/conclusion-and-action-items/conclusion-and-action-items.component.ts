import {Component,Input,OnDestroy,OnInit} from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TrainingProgramReview_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_ViewModel';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';

@Component({
  selector: 'app-conclusion-and-action-items',
  templateUrl: './conclusion-and-action-items.component.html',
  styleUrls: ['./conclusion-and-action-items.component.scss'],
})
export class ConclusionAndActionItemsComponent implements OnInit, OnDestroy {

  @Input() inputTPRViewModel: TrainingProgramReview_ViewModel;
  @Input() handleLoad: () => void | undefined;
  concAndActionItemsForm!: UntypedFormGroup;
  public Editor = ckcustomBuild;
  public configCKEditor = {
    toolbar: ["|","heading","bold","italic","strikethrough","underline","link","|","outdent","indent","bulletedList","numberedList","|","insertTable","undo","redo"],
  };
  
  constructor(
    private fb: UntypedFormBuilder,
  ) {}
  
  ngOnInit(): void {
    this.initializConcAndActionItemsForm();
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

  initializConcAndActionItemsForm() {
    this.concAndActionItemsForm = this.fb.group({
      conclusion: new UntypedFormControl(this.inputTPRViewModel?.conclusion),
      summary: new UntypedFormControl(this.inputTPRViewModel?.summary),
    });
  }
 
  onReady(editor: any) {
    editor.setData('<p></p><p></p><p></p><p></p>');
  }
  conclusionChange() {
    this.inputTPRViewModel.conclusion = this.concAndActionItemsForm.get('conclusion').value;
  }
  summaryChange(){
    this.inputTPRViewModel.summary = this.concAndActionItemsForm.get('summary').value;
  }

}
