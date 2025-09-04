import { SelectionModel } from '@angular/cdk/collections';
import { AfterViewInit, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemShortAnswer } from 'src/app/_DtoModels/TestItemShortAnswer/TestItemShortAnswer';
import { TestItemShortAnswerCreateOptions } from 'src/app/_DtoModels/TestItemShortAnswer/TestItemShortAnswerCreateOptions';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-short-answers',
  templateUrl: './short-answers.component.html',
  styleUrls: ['./short-answers.component.scss']
})
export class ShortAnswersComponent implements OnInit, OnDestroy, AfterViewInit {
  @Input() update:boolean;
  @Input() question_Id:any;
  @Input() showSaveButton = true;
  testItemId:any;
  short_answer: Short_Answer[] = [];
  default_number: number = 1;
  shortAnswerForm: UntypedFormGroup = new UntypedFormGroup({});
  subscriptions = new SubSink();
  saveSpinner = false;
  saveAndAddSpinner = false;

  selection = new SelectionModel<any>(true);

  public Editor = ckcustomBuild;

  constructor(
    private dataBroadcastService: DataBroadcastService,
    private testItemService: TestItemService,
    private alert: SweetAlertService,
    private flyPanelSrv:FlyInPanelService,
  ) { }

  ngOnInit(): void {
    if(this.update){
      this.getQuestionData();
    }
    else{
      this.readyShortAnswerForm();
    }
  }

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.dataBroadcastService.shortAnswerSaved.subscribe((res: any) => {
      if (res !== null) {
        this.saveData(res.id);
      }
      else {
        this.saveSpinner = false;
        this.saveAndAddSpinner = false;
      }
    })
  }

  saveData(id:any){
    var count = 0;
        for (var i = 0; i < this.short_answer.length; i++) {
          var options = new TestItemShortAnswerCreateOptions();
          options.responses = this.shortAnswerForm.get(`acceptedRes${i}`)?.value;
          options.testItemId = id;
          options.isCaseSensitive = this.shortAnswerForm.get(`caseSensitive${i}`)?.value;
          if (this.selection.selected.indexOf(1) > 0) {
            options.acceptableResponses = 1;
          }
          else {
            options.acceptableResponses = this.short_answer[i].id < this.default_number ? 1 : 0;
          }
          
          this.testItemService.createShortAnswer(options).then((res: any) => {
            count++;
            if (count === this.short_answer.length) {
              this.saveSpinner = false;
              this.saveAndAddSpinner = false;
              this.alert.successToast("Short Answer Saved Successfully");
              this.flyPanelSrv.close();
              this.dataBroadcastService.questionSaved.next({isSaved :true,id:id});;
            }
          }).catch((err: any) => {
            this.saveSpinner = false;
            this.saveAndAddSpinner = false;
            this.dataBroadcastService.questionSaved.next({isSaved : false});
            this.alert.errorToast("Error Saving Short Answer Data");
          })
        }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }


  async getQuestionData() {
    this.shortAnswerForm.addControl('question', new UntypedFormControl('', Validators.required));
    await this.testItemService.get(this.question_Id).then((res: TestItem) => {
      this.getAnswers(res.id);
      this.testItemId = res.id;
      this.shortAnswerForm.get('question')?.setValue(res.description);
    }).catch((err: any) => {
      this.alert.errorToast("Error Fetching Question Data");
    })
  }

  async getAnswers(id: any) {
    await this.testItemService.getShortAnswers(id).then((res: TestItemShortAnswer[]) => {
      res.forEach((data:TestItemShortAnswer,index:any)=>{
        this.shortAnswerForm.addControl(`acceptedRes${index}`,new UntypedFormControl(data.responses,Validators.required));
        this.shortAnswerForm.addControl(`caseSensitive${index}`, new UntypedFormControl(data.isCaseSensitive));
        this.short_answer.push({id:index,text:''});
      })
    })
  }

  readyShortAnswerForm() {
    this.shortAnswerForm.addControl('question', new UntypedFormControl('', Validators.required));
    for (let i = 0; i < 3; i++) {
      this.shortAnswerForm.addControl(`acceptedRes${i}`, new UntypedFormControl('', Validators.required));
      this.shortAnswerForm.addControl(`caseSensitive${i}`, new UntypedFormControl(false));
      this.short_answer.push({
        id: i,
        text: ''
      })
    }
  }

  onReady(editor: any) {
    // 
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  OnAddResponses() {
    this.shortAnswerForm.addControl(`acceptedRes${this.short_answer.length}`, new UntypedFormControl('', Validators.required));
    this.shortAnswerForm.addControl(`caseSensitive${this.short_answer.length}`, new UntypedFormControl(false));
    this.short_answer.push({
      id: this.short_answer.length,
      text: '',
    })
  }

  onRemoveResponse() {
    this.short_answer.pop();
    this.shortAnswerForm.removeControl(`acceptedRes${this.short_answer.length}`);
    this.shortAnswerForm.removeControl(`caseSensitive${this.short_answer.length}`);
  }

  IncreaseNumber() {
    if (this.default_number < this.short_answer.length) {
      this.default_number++;
    }
  }

  DecreaseNumber() {
    if (this.default_number > 1) {
      this.default_number--;
    }
  }

  addToSelection(event: any, id: any) {
    if (event.checked) {
      this.selection.select(id);
    }
    else {
      this.selection.deselect(id);
    }
    
  }

  saveTestItem(addToList: boolean) {
    if (addToList) {
      this.saveAndAddSpinner = true;
    }
    else {
      this.saveSpinner = true;
    };
    this.dataBroadcastService.saveShortAnswer.next({ question: this.shortAnswerForm.get('question')?.value, add: addToList });
  }

  updateTestItem(){
    this.testItemService.removeSA(this.testItemId).then((res:any)=>{
      this.saveData(this.testItemId);
      this.dataBroadcastService.updateQuestion.next({ id: this.question_Id, question: this.shortAnswerForm.get('question')?.value });
    }).catch((err:any)=>{
      this.alert.errorToast("Error Deleting Short Answer Item data");
    })
  }

  saveFromEO(options: TestItemCreateOptions){
    options.description = this.shortAnswerForm.get('question')?.value;
    if(this.shortAnswerForm.invalid || this.selection.selected.length < 1){
      this.alert.errorAlert("Invalid Data","Please Fill the Required Fields Correctly");
      this.dataBroadcastService.questionSaved.next({isSaved :false});;
    }
    else{
      this.testItemService.create(options).then((res:TestItem)=>{
        this.saveData(res.id);
      })
    }
  }
}

export class Short_Answer {
  id: any;
  text: string
}
