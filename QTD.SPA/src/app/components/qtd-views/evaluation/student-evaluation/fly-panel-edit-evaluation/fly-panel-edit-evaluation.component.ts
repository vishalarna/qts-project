import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { StudentEvaluationCreateOptions } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationCreateOptions';
import { RatingScaleNewService } from 'src/app/_Services/QTD/rating-scale-new.service';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { StudentEvaluation_Question_LinkCreateOptions } from 'src/app/_DtoModels/StudentEvaluationQuestions/StudentEvaluation_Question_LinkCreateOptions';
import { AnyFn } from '@ngrx/store/src/selector';
export interface EvaluationQuestions {
  id: string;
  question: string;
}
const ELEMENT_DATA: EvaluationQuestions[] = [
  {id: 'QTD_036', question: 'Here is a question for you'},
  {id: 'QTD_038', question: 'Here is another question for you'}
];
@Component({
  selector: 'app-fly-panel-edit-evaluation',
  templateUrl: './fly-panel-edit-evaluation.component.html',
  styleUrls: ['./fly-panel-edit-evaluation.component.scss']
})
export class FlyPanelEditEvaluationComponent implements OnInit {
  @Input() mode: 'Add' | 'Edit' | 'Copy' = 'Edit';
  @Input() studentEvalId :any;
  dateError = false;
  editCopyEvaluationForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  ratingScaleList: any[] = [];
  editor = ckcustomBuild;
  dataSource :any;
  displayedColumns: string[] = ['id','question','unlink'];
  displayedEditColumns: string[] = ['id','question'];

  importCheck:boolean = false;
  addCheck:boolean = false;
  editCheck:boolean = true;
  alreadyLinkedId:any[]=[];
  ids:any[] = [];


  constructor(  private fb: UntypedFormBuilder,
    private ratingScaleService:RatingScaleNewService,
    private studentEvalService:StudentEvaluationService,
    private alert: SweetAlertService) { }

  ngOnInit(): void {
    //this.ratingScaleList = ["Ratining Scale 1","Ratining Scale 2","Ratining Scale 3","Ratining Scale 4","Ratining Scale 5","Ratining Scale 6"]
   this.ratingScaleService.getAll().then((res)=>
   {
    this.ratingScaleList = res;
   })
    this.readydefinitionCategoryForm();
    this.readyStudentEvaluationFormWithData();
  }
  dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  }
  readydefinitionCategoryForm() {
    this.editCopyEvaluationForm = this.fb.group({
      title: new UntypedFormControl('', Validators.required),
      ratingScale: new UntypedFormControl('', Validators.required),
      instructions: new UntypedFormControl(''),
      notes: new UntypedFormControl(''),
      EffectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
    });
  }

  closedefinition() {
    this.closed.emit('IA_Proc closed');
  }
  readyStudentEvaluationFormWithData()
  {
    this.studentEvalService.get(this.studentEvalId).then((res)=>
    {
      
      this.editCopyEvaluationForm.get('title')?.patchValue(res.title);
      this.editCopyEvaluationForm.get('ratingScale')?.patchValue(res.ratingScaleN.id);
      this.editCopyEvaluationForm.get('instructions')?.patchValue(res.instructions);

    });
    this.studentEvalService.getLinkedQuestions(this.studentEvalId).then((res)=>
    {
      this.dataSource = new MatTableDataSource(res);
    })
  }
  updateEvaluation(publish:any)
  {
    let anotherMode1:any;
    if(publish === false){
      anotherMode1 = "edit";
    }else if(publish === true){
      anotherMode1 = "editPublish";
    }


    let createOpt: StudentEvaluationCreateOptions = {
    title:  this.editCopyEvaluationForm.get('title')?.value,
    ratingScaleId: this.editCopyEvaluationForm.get('ratingScale')?.value,
    instructions: this.editCopyEvaluationForm.get('instructions')?.value,
    mode:'Edit',
    anotherMode:anotherMode1,
    effectiveDate: this.editCopyEvaluationForm.get('EffectiveDate')?.value,
    studentEvaluationNotes :  this.editCopyEvaluationForm.get('notes')?.value,
    notes :  this.editCopyEvaluationForm.get('notes')?.value
  };

    this.studentEvalService.update(this.studentEvalId,createOpt).then((res: any) =>{
        this.alert.successToast('Student Evaluation Updated successfully');
        this.closed.emit();
        this.refresh.emit();
    });
  }

  unlinkStudentEvaluationQuestions(questionId:any){
    this.ids = [];
    var options = new StudentEvaluation_Question_LinkCreateOptions();
    this.ids.push(questionId);
    options.questionIds = this.ids;
    options.studentEvaluationId = this.studentEvalId

    this.studentEvalService.UnLinkQuestions(this.studentEvalId,options).then((res: any) =>{
      this.alert.successToast('Student Evaluation Question Unlinked successfully');
      this.GetLinkedQuestions();
  });

  }

  copyEvaluation() {
    let createOpt: StudentEvaluationCreateOptions = {
    title:  this.editCopyEvaluationForm.get('title')?.value + ' - Copy',
    ratingScaleId: this.editCopyEvaluationForm.get('ratingScale')?.value,
    instructions: this.editCopyEvaluationForm.get('instructions')?.value,
    mode:'Copy',
    effectiveDate: this.editCopyEvaluationForm.get('EffectiveDate')?.value,
    studentEvaluationNotes :  this.editCopyEvaluationForm.get('notes')?.value,
    stdEvalId:this.studentEvalId,
    notes:this.editCopyEvaluationForm.get('notes')?.value
  };
    this.studentEvalService.create(createOpt).then((res: any) =>
    {
        this.alert.successToast('Student Evaluation Copied successfully');
        this.closed.emit();
        this.refresh.emit();
        
    });
  }

  GetLinkedQuestions(){
    this.studentEvalService.getLinkedQuestions(this.studentEvalId).then((res)=>
    {
      this.dataSource = new MatTableDataSource(res);
      
    })
  }
}
