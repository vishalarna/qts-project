import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Position } from '@models/Position/Position';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { DIFSurveyVM } from '@models/DIFSurvey/DIFSurveyVM';

@Component({
  selector: 'app-dif-create-survey',
  templateUrl: './dif-create-survey.component.html',
  styleUrls: ['./dif-create-survey.component.scss'],
})
export class DifCreateSurveyComponent implements OnInit {
  createDifSurveyForm: UntypedFormGroup;
  Editor = ckcustomBuild;
  positions:Position[];
  @Input() inputDifSurveyVM: DIFSurveyVM;
  @Input() handleLoad: ()=>void;
  @Input() handlePositionClick: (e)=>void;
  @Input() handleStartDateClick: (e)=>void;
  @Input() handleDueDateClick: (e)=>void;
  @Output() formStatus = new EventEmitter<boolean>();

  constructor(private fb: UntypedFormBuilder,
     private posService:PositionsService,
    private datepipe: DatePipe,
    ) {}

  ngOnInit(): void {
    this.initializeCreateDifForm();
    this.getAllActivePositions();
  }

  initializeCreateDifForm() {
    this.createDifSurveyForm = this.fb.group({
      surveyTitle: new UntypedFormControl(null, [Validators.required]),
      position: new UntypedFormControl(null, [Validators.required]),
      startDate: new UntypedFormControl(null, [Validators.required]),
      dueDate: new UntypedFormControl(null, [Validators.required]),
      instructions: new UntypedFormControl(null),
    });
  }
  setFormValues(){
    this.createDifSurveyForm.patchValue({
      surveyTitle: this.inputDifSurveyVM?.surveyTitle,
      position: this.inputDifSurveyVM?.positionId,
      startDate:this.inputDifSurveyVM?.startDate != null?this.datepipe.transform(this.inputDifSurveyVM?.startDate,'yyyy-MM-dd') :null,
      dueDate:this.inputDifSurveyVM?.dueDate != null ? this.datepipe.transform(this.inputDifSurveyVM?.dueDate,'yyyy-MM-dd') :null,
      instructions:this.inputDifSurveyVM?.instructions ??''
    });
    this.formStatus.emit(this.createDifSurveyForm.valid);
  }
  

  _handleLoad() {
    if (this.handleLoad &&typeof this.handleLoad === 'function') {
      this.handleLoad();
    }
  }

  _handlePositionClick(e) {
    if (this.handlePositionClick &&typeof this.handlePositionClick === 'function') {
      this.handlePositionClick(e);
    }
  }

  _handleStartDateClick(e) {
    if (this.handleStartDateClick &&typeof this.handleStartDateClick === 'function') {
      this.handleStartDateClick(e);
    }
  }

  _handleDueDateClick(e) {
    if (this.handleDueDateClick &&typeof this.handleDueDateClick === 'function') {
      this.handleDueDateClick(e);
    }
  }
  async getAllActivePositions(){
    this.positions = (await this.posService.getAllWithoutIncludes()).filter(x=>x.active);
  }

  positionClick(e:any) {
    let positionValue = this.createDifSurveyForm.get('position').value;
    this.inputDifSurveyVM.positionId =positionValue;
    this._handlePositionClick(this.inputDifSurveyVM);
    this.changeForm();
  }

  startDateClick() {
    let startDate = this.createDifSurveyForm.get('startDate').value;
    this.inputDifSurveyVM.startDate = startDate;
    this._handleStartDateClick(this.inputDifSurveyVM);
    this.changeForm();
  }

  dueDateClick() {
    let dueDate = this.createDifSurveyForm.get('dueDate').value;
    this.inputDifSurveyVM.dueDate = dueDate;
    this._handleDueDateClick(this.inputDifSurveyVM);
    this.changeForm();
  }

  titleChange(){
    let title = this.createDifSurveyForm.get('surveyTitle').value;
    this.inputDifSurveyVM.surveyTitle = title;
    this.changeForm();
  }

  instructionChange(){
    let instruction = this.createDifSurveyForm.get('instructions').value;
    this.inputDifSurveyVM.instructions = instruction;
  }

  changeForm(){
    this.formStatus.emit(this.createDifSurveyForm.valid);
  }

}
