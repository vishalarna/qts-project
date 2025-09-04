import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { DIFSurveyEmployeResponseOptions } from '@models/DIFSurvey/DIFSurveyEmployeResponseOptions';
import { DIFSurveyEmployeeResponseModel } from '@models/DIFSurvey/DIFSurveyEmployeeResponseModel';
import { DIFSurveyResponseVm } from '@models/DIFSurvey/DIFSurveyResponseVM';
import { DIFSurveyViewResponseVm } from '@models/DIFSurvey/DifSurveyViewResponseVm';
import { Store } from '@ngrx/store';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { ApiDifSurveyService } from 'src/app/_Services/QTD/DifSurvey/api.difsurvey.service';
import { sideBarClose } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-dif-survey-page',
  templateUrl: './dif-survey-page.component.html',
  styleUrls: ['./dif-survey-page.component.scss'],
})
export class DifSurveyPageComponent implements OnInit {
  createDifSurveyPageForm: UntypedFormGroup;
  Editor = ckcustomBuild;
  dataSource: MatTableDataSource<DIFSurveyResponseVm>;
  displayedColumns: string[];
  difSurveyViewResponse: DIFSurveyViewResponseVm;
  difSurveyId: string;
  surveyResponses: any[];
  isSubmitDisable: boolean;
  isComplete: boolean;
  todayDate: Date;
  empResponseOptions: DIFSurveyEmployeResponseOptions;

  constructor(
    public dialog: MatDialog,
    private router: Router,
    private route: ActivatedRoute,
    private store: Store<{ toggle: string }>,
    private difSurveyService: ApiDifSurveyService,
    private fb: UntypedFormBuilder
  ) {}

  async ngOnInit() {
    this.initializeCreateDifForm();
    this.empResponseOptions = new DIFSurveyEmployeResponseOptions();
    this.isSubmitDisable = true;
    this.displayedColumns = [
      'index',
      'taskDescription',
      'difficulty',
      'importance',
      'frequency',
      'n/A',
      'comments',
      'textBox'
    ];
    this.surveyResponses = [];
    this.isComplete = false;
    this.todayDate = new Date();
    this.route.params.subscribe((params) => {
      this.difSurveyId = params['id'];
    });
    this.store.dispatch(sideBarClose());
    await this.loadAsync();
  }

  async loadAsync() {
    this.difSurveyViewResponse = await this.difSurveyService.getAllEmployeeResponses(
      this.difSurveyId
    );
    this.dataSource = new MatTableDataSource<DIFSurveyResponseVm>(this.difSurveyViewResponse.difSurveyResponseVM);
    this.createDummySurveysArray();
  }

  initializeCreateDifForm() {
    this.createDifSurveyPageForm = this.fb.group({
      additionalComment: new UntypedFormControl(null),
    });
  }

  toggleComments(element: any): void {
    element.showComments = !element.showComments;
  }

  openDialog(templateRef: any) {
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  createDummySurveysArray(){
    this.difSurveyViewResponse.difSurveyResponseVM.forEach((item, index) =>{
       const response = {
         idx: index,
         taskId: item?.difSurveyTaskId,
         difficulty: item?.difficulty,
         importance: item?.importance,
         frequency: item?.frequency,
         na: item?.na,
         comments:item?.comments
       };
       this.createDifSurveyPageForm.get('additionalComment').setValue(this.difSurveyViewResponse?.additionalComments);
      this.surveyResponses.push(response);
    });
    this.checkSubmitDisable();
  }

  onDifficultyChange(event:any, index:number){
    const res = this.surveyResponses.filter(r => r.idx === index)[0];
    res.difficulty = event.value;
    res.na = false;
    this.checkSubmitDisable();
  }

  getDifficultyValue(index:number){;
    const res = this.surveyResponses.filter(r => r.idx === index)[0];
    return res.difficulty?.toString();
  }

  onImportanceChange(event:any, index:number){
    const res = this.surveyResponses.filter(r => r.idx === index)[0];
    res.importance = event.value;
    res.na = false;
    this.checkSubmitDisable();
  }

  getImportanceValue(index:number){
    const res = this.surveyResponses.filter(r => r.idx === index)[0];
    return res.importance?.toString();
  }

  onFrequencyChange(event:any, index:number){
    const res = this.surveyResponses.filter(r => r.idx === index)[0];
    res.frequency = event.value;
    res.na = false;
    this.checkSubmitDisable();
  }

  getFrequencyValue(index:number){
    const res = this.surveyResponses.filter(r => r.idx === index)[0];
    return res.frequency?.toString();
  }

  onNAChange(index:number,row:any){
    let res = this.surveyResponses.filter(r => r.idx === index)[0];
    res.na = true;
    if (res?.na) {
      res.importance = null;
      res.frequency = null;
      res.difficulty = null;
    }
    this.checkSubmitDisable();
  }

  getNAValue(index:number){
    const res = this.surveyResponses.filter(r => r.idx === index)[0];
    return res.na?.toString();
  }

  onCommentsChange(event:any, index:number){
    let res = this.surveyResponses.filter(r => r.idx === index)[0];
    res.comments = event.target.value;
  }

  checkSubmitDisable(){
    let count = 0;
    this.surveyResponses.forEach((el) => {
      if(el.na){
        count ++ ;
      } else if(el.importance && el.frequency && el.difficulty){
        count ++;
      }
    });
    if(this.surveyResponses.length === count){
      this.isSubmitDisable = false;
    } else {
      this.isSubmitDisable = true;
    }
  }

  handleCommentChange(){
  let comments=  this.createDifSurveyPageForm.get('additionalComment')?.value;
  this.empResponseOptions.comments= comments;
  }

  getEmployeeResponseOptions(){
    this.surveyResponses.forEach((res) => {
      if(res.na){
        let response = new DIFSurveyEmployeeResponseModel(res.taskId, res.difficulty, res.importance, res.frequency, res.na, res.comments);
        this.empResponseOptions.AddToResponses(response);
      } else if(res.importance || res.frequency || res.difficulty){
        let response = new DIFSurveyEmployeeResponseModel(res.taskId, res.difficulty, res.importance, res.frequency, res.na, res.comments);
        this.empResponseOptions.AddToResponses(response);
      }
    })
  }

  async onBackExitClick(){
    this.getEmployeeResponseOptions();
    await this.difSurveyService.updateDIFEmployeeResponsesAsync(this.difSurveyId, this.empResponseOptions).then((res)=> {
      this.goBack();
    })
  }

 async onSubmitClick(){
    this.getEmployeeResponseOptions();
    await this.difSurveyService.completeDIFEmployeeResponsesAsync(this.difSurveyId, this.empResponseOptions).then(() => {
      this.isComplete = true;
    });
  }

  goBack(){
    this.router.navigate(['/emp/dif-survey/overview']);
  }

}
