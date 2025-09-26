import { StudentEvaluationFormService } from './../../../../../../../../_Services/QTD/student-evaluation-form.service';
import { StudentEvaluationFormsCreateOptions } from './../../../../../../../../_DtoModels/StudentEvaluationForms/StudentEvaluationFormsCreateOptions';
import { RatingScaleService } from './../../../../../../../../_Services/QTD/rating-scale.service';
import { RatingScaleCreateOptions } from './../../../../../../../../_DtoModels/RatingScale/RatingScaleCreateOptions';
import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewContainerRef,
  AfterViewInit,
} from '@angular/core';
import {
  UntypedFormArray,
  UntypedFormBuilder,
  UntypedFormGroup,
  UntypedFormControl,
  Validators,
} from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { StudentEvaluationQuestionCreateOptions } from 'src/app/_DtoModels/StudentEvaluationQuestion/StudentEvaluationQuestionCreateOptions';
import { StudentEvaluationQuestionService } from 'src/app/_Services/QTD/student-evaluation-question.service';
import { RatingScaleNewService } from 'src/app/_Services/QTD/rating-scale-new.service';
import { StudentEvaluationCreateOptions } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluationCreateOptions';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { QuestionBankCreateOptions } from 'src/app/_DtoModels/QuestionBank/QuestionBankCreateOptions';
import { QuestionBankService } from 'src/app/_Services/QTD/question-bank.service';
import { StudentEvaluation } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation';
import { StudentEvaluationQuestion } from 'src/app/_DtoModels/StudentEvaluationQuestion/StudentEvaluationQuestion';
import { QuestionBank } from 'src/app/_DtoModels/QuestionBank/QuestionBank';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { StudentEvaluationHistoryCreateOptions } from '@models/StudentEvaluationHistory/StudentEvaluationHistoryCreateOptions';

@Component({
  selector: 'app-fly-panel-create',
  templateUrl: './fly-panel-create.component.html',
  styleUrls: ['./fly-panel-create.component.scss'],
})
export class FlyPanelCreateComponent implements OnInit, AfterViewInit {
  @Input() preview_evaluation?: any;
  @Input() updateResult: any;
  @Input() mode: string;
  @Output() refreshEvals = new EventEmitter<any>();

  show_panel: boolean = false;
  show_create_new_rating_scale: boolean = false;
  text_area: string;
  options: RatingScaleCreateOptions = new RatingScaleCreateOptions();
  totalQuestions = 5;

  public Editor = ckcustomBuild;

  title: string;
  checked_options: Checkboxes_Options[] = [];
  add_boxes = new UntypedFormArray([new UntypedFormControl('', Validators.required)]);
  rating_scale: any[] = [];
  new_rating_scale: any[] = [];
  RatingControl = new UntypedFormControl([]);
  ratingScaleId = new UntypedFormControl(null,Validators.required);
  isAvailableForAllILAs = new UntypedFormControl();
  isAvailableForSelectedILAs = new UntypedFormControl();
  isIncludeCommentSections = new UntypedFormControl();
  isAllowNAOption = new UntypedFormControl();
  QuestionAnswerform: UntypedFormGroup = new UntypedFormGroup({});
  rating_scale_list: any[] = [];
  rating_scale_index: any;
  questionCheck: boolean = false;
  ratingScale: any;
  additionalOptionsForm: UntypedFormGroup = new UntypedFormGroup({});
  studentEval!: StudentEvaluation;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private ratingScaleService: RatingScaleNewService,
    private alert: SweetAlertService,
    private studentEvaluationForm: StudentEvaluationFormService,
    private studentEvaluationQuestion: StudentEvaluationQuestionService,
    private studentEvalService: StudentEvaluationService,
    private fb: UntypedFormBuilder,
    private questionBankService: QuestionBankService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  async ngOnInit(): Promise<void> {
    this.checked_options = [
      {
        id: '1',
        description: 'Make student Evaluation for all ' + await this.labelPipe.transform('ILA') + 's',
        checked: true,
      },
      {
        id: '2',
        description:
          'Make Student Evaluation Available for Selected '  + await this.labelPipe.transform('ILA') + 's (ability to select ' + await this.labelPipe.transform('ILA') + 's to link the Evaluation to)',
      },
      { id: '3', description: 'Allow N/A option' },
      {
        id: '4',
        description: 'Include Comments Section following each section',
      },
    ];
    if (this.mode === 'view') {
      this.ratingScaleId.disable();
    }

    if (this.preview_evaluation) {
      this.readyadditionaloptionsForm();
      this.readyEvalData();
    }
    else {
      this.readyQuestionForm();
      this.readyadditionaloptionsForm();
    }
    /*    this.rating_scale = [
      {id:1,text:'Strongly Agree'},
      {id:2,text:'Strongly Disagree'},
      {id:3,text:'Neutral'},
      {id:4,text:'Agree'},
      {id:5,text:'Disagree'},

    ];  */

    this.new_rating_scale = [
      { id: 1, text: null },
      { id: 2, text: null },
      { id: 3, text: null },
      { id: 4, text: null },
      { id: 5, text: null },
    ];
  }

  ngAfterViewInit(): void {
    this.readyRatingScale();
  }

  studentEvalQuestions: StudentEvaluationQuestion[] = []

  async readyEvalData() {
    this.studentEval = await this./* The above code is written in TypeScript and defines a variable
    named "studentEvalService". The three hash symbols " */
    studentEvalService.get(this.preview_evaluation?.id);
    if (this.studentEval) {
      
      this.additionalOptionsForm.get('availableForAllIlas').setValue(this.studentEval.isAvailableForAllILAs);
      this.additionalOptionsForm.get('availableForSelectedIlas').setValue(this.studentEval.isAvailableForSelectedILAs);
      this.additionalOptionsForm.get('allowNAoption').setValue(this.studentEval.isAllowNAOption);
      this.additionalOptionsForm.get('includeComments').setValue(this.studentEval.isIncludeCommentSections);
      this.ratingScaleId.setValue(this.studentEval.ratingScaleId);
      this.title = this.studentEval.title;
      this.readyEvaluationQuestion(this.studentEval.id);
    }
  }

  readyadditionaloptionsForm() {
    this.additionalOptionsForm = this.fb.group({
      availableForAllIlas: new UntypedFormControl(false),
      availableForSelectedIlas: new UntypedFormControl(false),
      allowNAoption: new UntypedFormControl(false),
      includeComments: new UntypedFormControl(false),
    });
    if (this.mode === 'view') {
      this.additionalOptionsForm.disable();
    }
  }
  async readyEvaluationQuestion(id: any) {
    let tempSrc: QuestionBank[] = [];
    await this.studentEvaluationQuestion
      .getStudentEvalQuestionsForEval(id)
      .then((res) => {
        res.forEach((data)=>{
            tempSrc.push(data);
        })
        this.totalQuestions = tempSrc.length;
        
        this.readyQuestionForm();
        tempSrc.forEach((element: QuestionBank,index) => {
            
            this.quesAns.controls[index].patchValue({ questionText: element.stem });
        });
      })
      .catch((err) => {
        
      });

    
    /*  tempSrc.forEach((element:any) => {
      this.quesAns.controls.forEach((c)=>c.setValue(element.questionText))
    });*/

    
  }

  readyRatingScale() {
    this.ratingScaleService
      .getAll()
      .then((res) => {
        this.rating_scale = res;
        // if(res.length === 0){
        //   this.alert.errorAlert("Please Create A Rating Scale Initally");
        // }
        // else{
        //  res.forEach((i:any)=>{

        //     this. rating_scale_list = res;
        //     this.rating_scale.push(i.position1Text,i.position2Text,i.position3Text,i.position4Text,i.position5Text);
        //  })
        // }
      })
      .catch((err) => {
        
      });
    
  }

  readyQuestionForm() {
    this.QuestionAnswerform = this.fb.group({
      quesAns: this.fb.array([]),
    });
    for (let i = 0; i < this.totalQuestions; i++) {
      this.addArray();
    }
    if (this.mode === 'view') {
      this.QuestionAnswerform.disable();
    }
  }

  addArray() {
    this.quesAns.push(this.buildArray());
  }

  buildArray(): UntypedFormGroup {
    return this.fb.group({
      questionText: '',
    });
  }

  get quesAns(): UntypedFormArray {
    return this.QuestionAnswerform.get('quesAns') as UntypedFormArray;
  }

  AddInput() {
    this.addArray();
  }

  //code for create new evaluation student starts

  // SelectCheckBox(e:any)
  // {
  //   
  //   if(e == '1'){
  //     this.checked_options[1].checked = false;
  //     this.checked_options[2].checked = false;
  //   }
  //   else if(e == '2'){
  //     this.checked_options[0].checked = false;
  //     this.checked_options[2].checked = false;
  //   }
  //   else if(e == '3'){
  //     this.checked_options[0].checked = false;
  //     this.checked_options[1].checked = false;
  //   }
  // }

  onReady(editor: any) {
    // 
  }

  async OnSaveEvaluation() {
    
    let evalformID: any;
    // 
    // 
    // 

    // var options : StudentEvaluationFormsCreateOptions = new StudentEvaluationFormsCreateOptions();
    // options.name = this.title;
    // options.ratingScaleId = this.rating_scale_index;

    // switch(this.checked_options[0].id){
    //   case '1':
    //     options.availableForAllIlas = true;
    //     break;

    //     case '2':
    //     options.availableForSelectedIlas = true;
    //     break;

    //   case '3':
    //     options.allowNAoption = true;
    //     break;

    //   case '4':
    //     options.includeComments = true;
    //     break;

    //   default:
    //     options.availableForAllIlas = true;
    // }
    // await this.studentEvaluationForm.create(options).then((res)=>{
    //   
    //   evalformID = res['result'].id
    //   this.alert.successToast("Student Evaluation Form Saved Successfully");
    // }).catch((err)=>{
    //   this.alert.errorToast("Title Already Exists");
    //   
    // })
    //New implementation Student Evaluation
    let createOpt: StudentEvaluationCreateOptions = {
      title: this.title,
      ratingScaleId: this.ratingScaleId.value,
      isAvailableForAllILAs: this.additionalOptionsForm.get(
        'availableForAllIlas'
      )?.value,
      isAvailableForSelectedILAs: this.additionalOptionsForm.get(
        'availableForSelectedIlas'
      )?.value,
      isIncludeCommentSections:
        this.additionalOptionsForm.get('includeComments')?.value,
      isAllowNAOption: this.additionalOptionsForm.get('allowNAoption')?.value,
    };

    await this.studentEvalService.create(createOpt).then(async (res: any) => {
      var options = new StudentEvaluationHistoryCreateOptions();
      options.effectiveDate = new Date();
      await this.studentEvalService.publishEvaluation(res.id, options);
      this.alert.successToast('Student Evaluation Created successfully');
      this.refreshEvals.emit();
      evalformID = res.id;
      
    });
    if (evalformID) {
      for (let i of this.quesAns.value) {
        if (i.questionText == '') {
          this.questionCheck = true;
        } else {
          this.questionCheck = false;
        }
        
        if (this.questionCheck === false) {
          var questionsArray: any = [];
          //it means there are questions
          for (let i of this.quesAns.value) {
            if (i.questionText != '') {
              questionsArray.push(i.questionText);
            }
          }

        }
      }
      if (questionsArray.length > 0) {
        var options = new QuestionBankCreateOptions();
        options.stemArray = questionsArray;
        options.studentEvaluationId = evalformID;
        options.mode = 'From ILA';

        this.questionBankService
          .createAndLinkQuestion(options)
          .then((res: any) => {
            this.flyPanelSrvc.close();
          })
          .finally(() => {
            this.flyPanelSrvc.close();
          });
        //this.saveQuestion(evalformID);
      }
    }
    else {
      this.flyPanelSrvc.close();
    }
  }

  async saveQuestion(id: any) {
    var options = new StudentEvaluationQuestionCreateOptions();

    this.quesAns.controls.forEach(async (element, index) => {
      
      options.evalFormID = id;
      options.questionNumber = index + 1;
      options.questionText = element.value.questionText;

      await this.studentEvaluationQuestion
        .create(options)
        .then((res) => {
          this.alert.successToast('Evaluation Questions Saved Successfully');
        })
        .catch((err) => {
          this.alert.errorToast('Error Saving Evaluation Questions');
        });
    });
  }

  OnRatingScaleClick() {
    this.show_create_new_rating_scale = true;
  }
  //code for create new evaluation student ends

  //code for create new rating scale starts
  OnChange(entry: any) {
    

    

    let index = this.new_rating_scale.findIndex((o) => o.id == entry);
    
  }

  OnDelete(id: any) {
    
    
    for (let i = 0; i < this.new_rating_scale.length; i++) {
      if (this.new_rating_scale[i].id == id) {
        this.new_rating_scale.splice(i, 1);
      }
    }
  }

  // OnSave(){
  //   
  //   
  //   let index = 0;
  //  this.options.Position1Text = this.new_rating_scale[index].text;
  //  this.options.Position2Text = this.new_rating_scale[index+1].text;
  //  this.options.Position3Text = this.new_rating_scale[index+2]?.text;
  //  this.options.Position4Text = this.new_rating_scale[index+3]?.text;
  //  this.options.Position5Text = this.new_rating_scale[index+4]?.text;

  //  if(this.options.Position1Text === '' && this.options.Position2Text === ''){
  //   this.alert.errorAlert('First 2 Options Are Required');
  //  }
  //  else{

  //   
  //   this.ratingScaleService.create(this.options).then((res)=>{
  //     this.alert.successToast('Rating Scale Saved Successfully');
  //     this.readyRatingScale();
  //    /* for(let i of this.new_rating_scale){
  //       if(i.text !== ''){
  //         this.rating_scale.push({
  //           id:i.id,
  //           text:i.text
  //         });
  //       }
  //     }  */
  //     for(let i of this.new_rating_scale){
  //       i.text = '';
  //     }
  //     
  //   }).catch((err)=>{
  //     this.alert.errorToast('Error Saving Rating Scale');
  //     
  //   });

  //   this.show_create_new_rating_scale=false;
  //  }
  // }

  removeRating(i: any) {
    
    const pos = this.RatingControl.value as Rating[];
    this.removeFirst(pos, i);
    this.RatingControl.setValue(pos);
  }

  private removeFirst(array: Rating[], toRemove: Rating): void {
    
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }

  OnClick(selected: any) {
    
    this.rating_scale_list.forEach((i: any) => {
      if (
        selected === i.position1Text ||
        selected === i.position2Text ||
        selected === i.position3Text ||
        selected === i.position4Text ||
        selected === i.position5Text
      ) {
        this.rating_scale_index = i.id;
      }
    });
    
    /*  if (selected) {
      this.rating_scale_list.push(this.RatingControl);
    }
     */
  }
}

export class Rating {
  id: any;
  text: string;
}

export class Checkboxes_Options {
  id: any;
  description: string;
  checked?: boolean;
}
