import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import {Component,Input,OnDestroy,OnInit, ViewContainerRef} from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { TrainingProgramReview_Employee_Link_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_Employee_Link_ViewModel';
import { TrainingProgramReview_ViewModel } from 'src/app/_DtoModels/TrainingProgramReview/TrainingProgramReview_ViewModel';
import { TrainingProgram_VersionTitleViewModel } from 'src/app/_DtoModels/TrainingProgram/TrainingProgram_VersionTitleViewModel';
import { TrainingProgramType } from 'src/app/_DtoModels/TrainingProgramType/TrainingProgramType';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { TrainingProgramTypeService } from 'src/app/_Services/QTD/training-program-type.service';
import { TrainingProgramsService } from 'src/app/_Services/QTD/training-programs.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-create-new-program-review',
  templateUrl: './create-new-program-review.component.html',
  styleUrls: ['./create-new-program-review.component.scss'],
})
export class CreateNewProgramReviewComponent implements OnInit, OnDestroy {
  @Input() inputTPRViewModel: TrainingProgramReview_ViewModel ;
  @Input() handleLoad: ()=>void;
  @Input() handlePositionClick: (e)=>void;
  @Input() handleTrainingProgramTypeClick: (e)=>void;
  @Input() handleVersionNumberClick: (e)=>void;
  @Input() handleReviewerNamesClick: (e)=>void;
  @Input() handlerReviewDateClick: (e)=>void;
  @Input() handleStartDateClick: (e)=>void;
  @Input() handleEndDateClick: (e)=>void;
  positions:Position[];
  trainingProgramTypes:TrainingProgramType[];
  trainingPrograms:TrainingProgram_VersionTitleViewModel[];
  createProgramReviewForm: UntypedFormGroup;
  constructor(
    private positionService: PositionsService,
    private trainingProgramTypeService:TrainingProgramTypeService,
    private fb: UntypedFormBuilder,
    private trainingProgramsService:TrainingProgramsService,
    private datePipe:DatePipe,
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
  ) {}
  
  ngOnInit(): void {
    this.initializeCreateReview();
    this.loadAsync();
  }

  ngOnDestroy(): void {
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

  _handleTrainingProgramTypeClick(e) {
    if (this.handleTrainingProgramTypeClick &&typeof this.handleTrainingProgramTypeClick === 'function') {
      this.handleTrainingProgramTypeClick(e);
    }
  }

  _handleVersionNumberClick(e) {
    if (this.handleVersionNumberClick &&typeof this.handleVersionNumberClick === 'function') {
      this.handleVersionNumberClick(e);
    }
  }

  _handleReviewerNamesClick(e) {
    if (this.handleReviewerNamesClick &&typeof this.handleReviewerNamesClick === 'function') {
      this.handleReviewerNamesClick(e);
    }
  }

  _handlerReviewDateClick(e) {
    if (this.handlerReviewDateClick &&typeof this.handlerReviewDateClick === 'function') {
      this.handlerReviewDateClick(e);
    }
  }

  _handleStartDateClick(e) {
    if (this.handleStartDateClick &&typeof this.handleStartDateClick === 'function') {
      this.handleStartDateClick(e);
    }
  }

  _handleEndDateClick(e) {
    if (this.handleEndDateClick &&typeof this.handleEndDateClick === 'function') {
      this.handleEndDateClick(e);
    }
  }

  initializeCreateReview(){
    this.createProgramReviewForm = this.fb.group({
      position: new UntypedFormControl(null, [Validators.required]),
      trainingProgramType: new UntypedFormControl(null, [Validators.required]),
      versionNumber: new UntypedFormControl(null, [Validators.required]),
      reviewDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), [Validators.required]),
      startDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), [Validators.required]),
      endDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), [Validators.required])
    });
  }

  async loadAsync() {
    this.positions = (await this.positionService.getAllWithoutIncludes()).filter(x=>x.active);
    this.trainingProgramTypes = (await this.trainingProgramTypeService.getAll()).filter(x=>x.active);
    if(this.inputTPRViewModel.id){
      this.createProgramReviewForm.get('position').setValue(this.inputTPRViewModel.positionId);
      this.createProgramReviewForm.get('trainingProgramType').setValue(this.inputTPRViewModel.trainingProgramTypeId);
      this.createProgramReviewForm.get('reviewDate').setValue(this.datePipe.transform(this.inputTPRViewModel.reviewDate, "yyyy-MM-dd"));
      this.createProgramReviewForm.get('startDate').setValue(this.datePipe.transform(this.inputTPRViewModel.startDate, "yyyy-MM-dd"));
      this.createProgramReviewForm.get('endDate').setValue(this.datePipe.transform(this.inputTPRViewModel.endDate, "yyyy-MM-dd"));
      this.getTrainingPrograms();
    }else{
      this.reviewDateClick();
      this.startDateClick();this.endDateClick();
    }
    this._handleLoad();
  }

  positionClick(e:any) {
    let positionValue = this.createProgramReviewForm.get('position').value;
    this.inputTPRViewModel.positionId =positionValue;
    this.inputTPRViewModel.positionName =this.positions.find(x=>x.id==positionValue).positionTitle;
    this._handlePositionClick(this.inputTPRViewModel);
    this.getTrainingPrograms();
  }

  trainingProgramTypeClick(e:any) {
    let trainingProgramType = this.createProgramReviewForm.get('trainingProgramType').value;
    this.inputTPRViewModel.trainingProgramTypeId =trainingProgramType;
    this.inputTPRViewModel.trainingProgramType =this.trainingProgramTypes.find(x=>x.id==trainingProgramType).trainingProgramTypeTitle;
    this._handleTrainingProgramTypeClick(this.inputTPRViewModel);
    this.getTrainingPrograms();
  }

  versionNumberClick(e:any) {
    let trainingProgramId = this.createProgramReviewForm.get('versionNumber').value;
    let trainingProgram = this.trainingPrograms.find(x=>x.id == trainingProgramId);
    this.inputTPRViewModel.trainingProgramId =trainingProgramId;
    this.inputTPRViewModel.trainingProgram_ProgramTitle =trainingProgram.programTitle;
    this.inputTPRViewModel.trainingProgram_Version =trainingProgram.version;
    this._handleVersionNumberClick(this.inputTPRViewModel);
  }

  reviewerNamesClick(templateRef:any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
    this._handleReviewerNamesClick(templateRef);
  }

  reviewDateClick() {
    let reviewDate = this.createProgramReviewForm.get('reviewDate').value;
    this.inputTPRViewModel.reviewDate = reviewDate;
    this._handlerReviewDateClick(this.inputTPRViewModel);
  }

  startDateClick() {
    let startDate = this.createProgramReviewForm.get('startDate').value;
    this.inputTPRViewModel.startDate = startDate;
    this._handleStartDateClick(this.inputTPRViewModel);
  }

  endDateClick() {
    let endDate = this.createProgramReviewForm.get('endDate').value;
    this.inputTPRViewModel.endDate = endDate;
    this._handleEndDateClick(this.inputTPRViewModel);
  }

  async getTrainingPrograms(){
    let positionId=this.inputTPRViewModel.positionId;
    let trainingProgramTypeId=this.inputTPRViewModel.trainingProgramTypeId;
    if(positionId && trainingProgramTypeId){
      this.trainingPrograms= await this.trainingProgramsService.GetActiveVersionTitleByPositionAndTrainingProgramTypeAsync(positionId,trainingProgramTypeId);
      if(this.inputTPRViewModel.id){
        this.createProgramReviewForm.get('versionNumber').setValue(this.inputTPRViewModel.trainingProgramId);
      }
      else{
        this.createProgramReviewForm.get('versionNumber').enable();
        this.inputTPRViewModel.trainingProgramId=this.inputTPRViewModel.trainingProgram_ProgramTitle=this.inputTPRViewModel.trainingProgram_Version=undefined;
      }
    }
  }
  closeFlyPanel =()=>{ 
    this.flyPanelService.close()
  }

  setReviewers=(reviewers:TrainingProgramReview_Employee_Link_ViewModel[])=>{
    this.inputTPRViewModel.reviewers=reviewers
  }
 
}
