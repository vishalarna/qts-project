
import { AfterViewInit, Component, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemOptions } from 'src/app/_DtoModels/TestItem/TestItemOptions';
import { TestItemMatch } from 'src/app/_DtoModels/TestItemMatch/TestItemMatch';
import { TestItemMatchCreateOPtions } from 'src/app/_DtoModels/TestItemMatch/TestItemMatchCreateOptions';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { SubSink } from 'subsink';
@Component({
  selector: 'app-match-the-column',
  templateUrl: './match-the-column.component.html',
  styleUrls: ['./match-the-column.component.scss']
})
export class MatchTheColumnComponent implements OnInit, OnDestroy, AfterViewInit {
  @Input() update: boolean;
  @Input() question_Id: any;
  @Input() showSaveButton = true;
  option_array: any[] = [];
  choice_array: any[] = [];
  option_text: string;
  choiceForm: UntypedFormGroup = new UntypedFormGroup({});
  matchesForm: UntypedFormGroup = new UntypedFormGroup({});
  MAX_CORRECT_ANS = 'D';
  correctOptions: any[] = [];
  subsctiptions = new SubSink();

  testItemId: any;

  saveSpinner = false;

  questionForm: UntypedFormGroup = new UntypedFormGroup({});


  public Editor = ckcustomBuild;


  constructor(
    private dataBroadcastService: DataBroadcastService,
    private alert: SweetAlertService,
    private testItemService: TestItemService,
  ) { }

  ngOnInit(): void {
    this.questionForm.addControl('question', new UntypedFormControl('', Validators.required));
    if (this.update) {
      this.getQuestionData();
    }
    else {
      this.onReadyMatches();
    }
  }

  ngAfterViewInit(): void {
    this.subsctiptions.sink = this.dataBroadcastService.matchColumnSaved.subscribe((res: any) => {
      if (res !== null) {
        this.saveData(res.id);
      }
      else {
        this.saveSpinner = false;
      }
    })
  }

  ngOnDestroy(): void {
    this.subsctiptions.unsubscribe();
  }

  saveData(id: any) {
    var count = 0;
    this.option_array.forEach((i, index) => {
      var options = new TestItemMatchCreateOPtions();
      options.choiceDescription = this.choiceForm.get(`choiceDescription${i.id}`)?.value === undefined ? "" : this.choiceForm.get(`choiceDescription${i.id}`)?.value;
      options.correctValue = this.correctOptions[i.id - 1] === undefined ? '' : this.correctOptions[i.id - 1];
      options.matchDescription = this.matchesForm.get(`matchDescription${i.id}`)?.value;
      options.matchValue = i.text;
      options.testItemId = id;
      
      this.testItemService.createMatchTheColumn(options).then((res) => {
        count++;
        if (count === this.option_array.length) {
          this.alert.successToast("Match the column Question Saved");
          this.questionForm.reset();
          this.matchesForm.reset();
          this.choiceForm.reset();
          this.saveSpinner = false;
          this.dataBroadcastService.questionSaved.next({isSaved :true,id:id});
        }
      }).catch((err: any) => {
        this.alert.errorAlert("Error Saving Match the column data");
        this.saveSpinner = false;
        this.dataBroadcastService.questionSaved.next({isSaved :false});;
      })
    })
  }

  async getQuestionData() {
    await this.testItemService.get(this.question_Id).then((res: TestItem) => {
      this.getAnswers(res.id);
      this.testItemId = res.id;
      this.questionForm.get('question')?.setValue(res.description);
    }).catch((err: any) => {
      this.alert.errorToast("Error Fetching Question Data");
    })
  }

  async getAnswers(id: any) {
    await this.testItemService.getMatchTheColumn(id).then((res: TestItemMatch[]) => {
      
      res.forEach((data: TestItemMatch, i: any) => {
        this.matchesForm.addControl(`matchDescription${i + 1}`, new UntypedFormControl(data.matchDescription, Validators.required));
        this.option_array.push({
          id: i + 1,
          text: data.matchValue,
        });

        if (data.choiceDescription !== '') {
          this.correctOptions.push(data.correctValue);
          this.choiceForm.addControl(`choiceDescription${i + 1}`, new UntypedFormControl(data.choiceDescription, Validators.required));
          this.choice_array.push({
            id: i + 1,
            text: data.matchValue,
          });
        }
      })
    })
  }

  onReady(editor: any) {
    // 
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  onReadyMatches() {
    for (let i = 0; i < 4; i++) {
      this.matchesForm.addControl(`matchDescription${i + 1}`, new UntypedFormControl('', Validators.required));
      this.option_array.push({
        id: i + 1,
        text: String.fromCharCode(65 + i)
      });
    }

    console.dir(this.matchesForm);
    for (let i = 0; i < 4; i++) {
      this.correctOptions.push(String.fromCharCode(65));
      this.choiceForm.addControl(`choiceDescription${i + 1}`, new UntypedFormControl('', Validators.required));
      this.choice_array.push({
        id: i + 1,
        text: String.fromCharCode(65)
      });
    }
    
  }

  OnAddOption() {
    this.matchesForm.addControl(`matchDescription${this.option_array.length + 1}`, new UntypedFormControl('', Validators.required));
    console.dir(this.matchesForm);
    this.option_array.push({
      id: this.option_array.length + 1,
      text: String.fromCharCode(65 + this.option_array.length)
    });
    this.MAX_CORRECT_ANS = this.option_array[this.option_array.length - 1].text
  }

  onRemoveOptions() {
    this.matchesForm.removeControl(`matchDescription${this.option_array.length}`);
    this.option_array.pop();
    this.MAX_CORRECT_ANS = this.option_array[this.option_array.length - 1].text;
    this.checkAndResetCorrectAnswers();
    this.alert.warningToast("The correct value for choice associated with removed option is reset.");
  }

  checkAndResetCorrectAnswers() {
    this.correctOptions.forEach((i, index) => {
      if (i > this.MAX_CORRECT_ANS) {
        //this.correctOptions[index] = String.fromCharCode(this.correctOptions[index].charCodeAt(0) - 1);
        this.correctOptions[index] = 'A';
      }
    })
  }

  onIncreaseAlphabet(i: number) {
    var option = this.correctOptions[i];
    if (option < this.MAX_CORRECT_ANS) {
      this.correctOptions[i] = String.fromCharCode(option.charCodeAt(0) + 1);
    }
  }

  onDecreaseAlphabet(i: number) {
    var option = this.correctOptions[i];
    if (option > 'A') {
      this.correctOptions[i] = String.fromCharCode(option.charCodeAt(0) - 1);
    }
  }

  OnAddChoice() {
    this.choiceForm.addControl(`choiceDescription${this.choice_array.length + 1}`, new UntypedFormControl('', Validators.required));
    this.correctOptions.push('A');
    this.choice_array.push({
      id: this.choice_array.length + 1,
      text: String.fromCharCode(65)
    });
  }
  onRemoveChoice() {
    this.choiceForm.removeControl(`choiceDescription${this.choice_array.length}`);
    this.choice_array.pop();
    this.correctOptions.pop();
    
  }

  saveQuestion(addToList: boolean) {
    this.saveSpinner = true;
    this.dataBroadcastService.saveMatchColumn.next({ question: this.questionForm.get('question')?.value, add: addToList });
  }

  updateData() {
    this.testItemService.deleteMatchColumnItems(this.testItemId).then((res: any) => {
      this.saveData(this.testItemId);
      this.dataBroadcastService.updateQuestion.next({ id: this.question_Id, question: this.questionForm.get('question')?.value });
    }).catch((err: any) => {
      this.alert.errorToast("Error Removing MCQ items");
    })
  }

  saveFromEO(options : TestItemCreateOptions){
    options.description = this.questionForm.get('question')?.value;
    if(this.questionForm.invalid || this.choiceForm.invalid || this.matchesForm.invalid || this.choice_array.length > this.option_array.length){
      this.alert.errorAlert("Invalid Data","Please Enter Valid Match column Data");
      this.dataBroadcastService.questionSaved.next({isSaved :false});
    }
    else{
      this.testItemService.create(options).then((res:TestItem)=>{
        this.saveData(res.id);
      })
    }
  }
}

