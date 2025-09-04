import {Component,Input,OnDestroy,OnInit} from '@angular/core';
import { TrainingProgramReview_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_ViewModel';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-training-department-sign-off',
  templateUrl: './training-department-sign-off.component.html',
  styleUrls: ['./training-department-sign-off.component.scss'],
})
export class TrainingDepartmentSignOffComponent implements OnInit, OnDestroy {
  @Input() inputTPRViewModel: TrainingProgramReview_ViewModel;
  @Input() handleLoad: () => void | undefined;
  @Input() handleTrainerSignOffClick: (e) => void | undefined;
  trainerSignOffForm!: UntypedFormGroup;

  constructor(
    private fb: UntypedFormBuilder,
  ) {}
  
  ngOnInit(): void {
    this.initializTrainerSignOffForm();
    this.load();
  }

  ngOnDestroy(): void {
  }

  _handleLoad() {
    if (this.handleLoad &&typeof this.handleLoad === 'function') {
      this.handleLoad();
    }
  }

  _handleTrainerSignOffClick(e) {
    if (this.handleTrainerSignOffClick &&typeof this.handleTrainerSignOffClick === 'function') {
      this.handleTrainerSignOffClick(e);
    }
  }

  load() {
    this._handleLoad();
  }
  
  trainerSignOffClick(e:any){
    this.inputTPRViewModel.trainerSignOff = this.trainerSignOffForm.get('trainerSignOff').value;
    this._handleTrainerSignOffClick(null);
  }

  initializTrainerSignOffForm() {
    this.trainerSignOffForm = this.fb.group({
      trainerName: new UntypedFormControl(this.inputTPRViewModel?.trainerName),
      trainerTitle: new UntypedFormControl(this.inputTPRViewModel?.title),
      trainerSignOff: new UntypedFormControl(this.inputTPRViewModel?.trainerSignOff,[Validators.required]),
    });
  }

  trainerNameChange() {
    this.inputTPRViewModel.trainerName = this.trainerSignOffForm.get('trainerName').value;
  }
  trainerTitleChange(){
    this.inputTPRViewModel.title = this.trainerSignOffForm.get('trainerTitle').value;
  }

 
}
