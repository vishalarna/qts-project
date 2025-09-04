import { SelectionModel } from '@angular/cdk/collections';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TranslateService } from '@ngx-translate/core';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemMcq } from 'src/app/_DtoModels/TestItemMcq/TestItemMcq';
import { TestItemMcqCreateOptions } from 'src/app/_DtoModels/TestItemMcq/TestItemMcqCreateOptions';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { SubSink } from 'subsink';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';

@Component({
  selector: 'app-mcqs-test',
  templateUrl: './mcqs-test.component.html',
  styleUrls: ['./mcqs-test.component.scss']
})
export class McqsTestComponent implements OnInit, OnDestroy, AfterViewInit {
  @Output() answer = new EventEmitter<any>();
  @Output() close_flypanel = new EventEmitter<any>();
  @Input() update: boolean;
  @Input() question_Id: any;
  @Input() showSaveButton = true;
  @Output() isDistractorValid = new EventEmitter<boolean>();

  QuestionAnswerForm!: UntypedFormGroup;
  testItemId: any;

  updateSpinner = false;

  obj_length: number;
  distractorsForm: UntypedFormGroup = new UntypedFormGroup({});
  distractors: any[] = [];
  checked: boolean = false;
  description: any;
  text_area: any;
  obj_id: any;
  subscriptions = new SubSink();
  selection = new SelectionModel<any>(true);

  saveSpinner = false;
  saveandAddSpinner = false;

  /*  enabling_obj : Enabling_Objectives[]=[]; */

  public Editor = ckcustomBuild;


  constructor(
    private alert: SweetAlertService,
    private translate: TranslateService,
    private fb: UntypedFormBuilder,
    private dataBroadcastService: DataBroadcastService,
    private testItemService: TestItemService,) {
    this.QuestionAnswerForm = fb.group({
      quesAns: ['', Validators.compose([
        Validators.required,
      ])],
    });
  }

  ngOnInit(): void {
    //putting 4 options by default for distractors
    if (this.update) {
      this.getQuestionData();
    }
    else {
      this.DistractorsByDefault();
    }

    this.subscriptions.sink = this.distractorsForm.statusChanges.subscribe((_)=>{
      
      if(this.selection.selected.length > 0){
        this.isDistractorValid.emit(this.distractorsForm.invalid);
      }
      else{
        this.isDistractorValid.emit(false);
      }
    })

  }

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.dataBroadcastService.testItemSaved.subscribe((res) => {
      if (res !== null) {
        this.saveTestItem(res.id);
      }
      else {
        this.saveSpinner = false;
        this.saveandAddSpinner = false;
      }
    })
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  saveTestItem(id: any) {
    var index = 0;
    this.distractors.forEach(async (element: any) => {
      
      var options = new TestItemMcqCreateOptions();
      options.testItemId = id;
      options.isCorrect = this.selection.selected.indexOf(element.id) > -1;
      options.choiceDescription = this.distractorsForm.get('quesAns' + element.id)?.value;
      await this.testItemService.createMcq(options).then((res: any) => {
        index++;
        if (index === this.distractors.length) {
          this.alert.successAlert("MCQ Test Item Saved");
          this.saveSpinner = false;
          this.saveandAddSpinner = false;
          this.close_flypanel.emit(true);
          this.dataBroadcastService.questionSaved.next({isSaved :true,id:id});;
        }
      }).catch((err: any) => {
        this.saveSpinner = false;
        this.saveandAddSpinner = false;
        this.alert.errorToast("Error Creating MCQ Test Item");
        this.dataBroadcastService.questionSaved.next({isSaved :false});;
      })
      console.dir(options);
    })
  }

  async getQuestionData() {
    await this.testItemService.get(this.question_Id).then((res: TestItem) => {
      this.getAnswers(res.id);
      this.testItemId = res.id;
      this.QuestionAnswerForm.get('quesAns')?.setValue(res.description);
    }).catch((err: any) => {
      this.alert.errorToast("Error Fetching Question Data");
    })
  }

  async getAnswers(id: any) {
    await this.testItemService.getMCQ(id).then((res: TestItemMcq[]) => {
      this.distractorsForm = new UntypedFormGroup({});
      res.forEach((data: TestItemMcq, index: any) => {
        this.distractorsForm.addControl(`quesAns${index}`, new UntypedFormControl(data.choiceDescription, Validators.required));
        
        this.distractors.push({id:index,choiceDescription:data.choiceDescription,isCorrect:data.isCorrect});
        if (data.isCorrect) {
          this.selection.select(index);
        }
      })
    })
  }

  //putting 4 options by default for distractors function
  DistractorsByDefault() {
    this.distractorsForm = new UntypedFormGroup({});
    for (let i = 0; i < 4; i++) {
      this.distractors.push({
        id: i,
        choiceDescription: '',
        correctAnswer: false,
      })
      this.distractorsForm.addControl(`quesAns${i}`, new UntypedFormControl('', Validators.required));
    }
    this.distractorsForm.addControl(`correctAnswer`, new UntypedFormControl());
  }

  OnSelectionChange() {
    
    for (let i = 0; i < this.distractors.length; i++) {
      if (this.distractors[i].id == this.description) {
        this.distractors[i].correctAnswer = true;
      }
      else {
        this.distractors[i].correctAnswer = false;

      }
    }
    
    
  }

  OnChange(entry: any) {
    

    let index = this.distractors.findIndex(o => o.id == entry);
    

    //take array from this if all distractors options are needed instead of after add new
    //or in add new save the array in another variable and then call ngOnInit()
    
  }

  OnSave(addToList: boolean) {
    
    if (addToList) {
      this.saveandAddSpinner = true;
    }
    else {
      this.saveSpinner = true;
    }
    this.dataBroadcastService.saveTestItem.next({ question: this.QuestionAnswerForm.get('quesAns')?.value, add: addToList });
    //console.dir(this.distractorsForm);
  }

  onReady(editor: any) {
    // 
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  AddInput() {
    this.distractorsForm.addControl(`quesAns${(this.distractors[this.distractors.length - 1]?.id === undefined ? 0 : this.distractors[this.distractors.length - 1]?.id) + 1}`, new UntypedFormControl('', Validators.required));
    this.distractors.push({
      id: (this.distractors[this.distractors.length - 1]?.id === undefined ? 0 : this.distractors[this.distractors.length - 1]?.id) + 1,
      choiceDescription: '',
      correctAnswer: false,
    })
  }

  OnDelete(id: any) {
    
    this.distractors = this.distractors.filter((data: any) => {
      if (data.id === id) {
        this.distractorsForm.removeControl(`quesAns${data.id}`);
        this.distractorsForm.get('correctAnswer')?.setValue(this.distractorsForm.get('correctAnswer')?.value === id ? null : this.distractorsForm.get('correctAnswer')?.value)
      }
      return data.id !== id;
    })
    this.selection.deselect(id);
  }

  rebuildDistractorsForm() {
    this.distractorsForm = new UntypedFormGroup({});
    for (var i = 0; i < this.distractors.length; i++) {
      this.distractorsForm.addControl(`quesAns${i + 1}`, new UntypedFormControl('', Validators.required));
    }

    this.distractorsForm.addControl(`correctAnswer`, new UntypedFormControl());
  }


  AddNewQuestion() {
    // 
    let ans_1 = this.QuestionAnswerForm.get('quesAns')?.value.replace(/<[^>]+>/g, '');
    this.answer.emit(ans_1);
    /*  if(this.answer){*/
    this.QuestionAnswerForm.patchValue({ quesAns: "" });
    this.distractors.length = 0;
    this.DistractorsByDefault()
    //this.ngOnInit();
    //}

    

  }

  changeSelection(event: any, dis: any) {
    if (event.checked) {
      this.selection.select(dis.id);
    }
    else {
      this.selection.deselect(dis.id);
    }
    if(this.selection.selected.length > 0){
      this.distractorsForm.valid ? this.isDistractorValid.emit(true): this.isDistractorValid.emit(false);
    }
    
    
    
  }

  updateData() {
    this.testItemService.removeMCQ(this.testItemId).then((res: any) => {
      this.saveTestItem(this.testItemId);
      this.dataBroadcastService.updateQuestion.next({ id: this.question_Id, question: this.QuestionAnswerForm.get('quesAns')?.value });
    }).catch((err: any) => {
      this.alert.errorToast("Error Removing MCQ items");
    })
  }

  saveFromEO(options : TestItemCreateOptions){
    options.description = this.QuestionAnswerForm.get('quesAns')?.value;
    if(this.QuestionAnswerForm.valid && this.distractorsForm.valid && (this.distractors.length > 0) && this.selection.selected.length > 0){
      this.testItemService.create(options).then((res:TestItem)=>{
        this.saveTestItem(res.id);
      })
    }
    else{
      this.dataBroadcastService.questionSaved.next({isSaved :false});
      this.alert.errorToast("All distractors should have a description and at least 1 should be marked as correct");
    }
  }

}

/* export class Enabling_Objectives{
  id:any;
  question:any;
} */

export class Distractors {
  id: any;
  text: any;
  correctAnswer: boolean;
}

