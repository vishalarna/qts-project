import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemTrueFalse } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalse';
import { TestItemTrueFalseCreateOptions } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalseCreateOptions';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-true-false',
  templateUrl: './true-false.component.html',
  styleUrls: ['./true-false.component.scss']
})
export class TrueFalseComponent implements OnInit, OnDestroy, AfterViewInit {
  @Input() update:boolean;
  @Input() question_Id:any;
  @Output() continue_enable = new EventEmitter<any>();
  true_false_array: any[] = [];
  checked: boolean;
  thirdOption = false;
  trueFalseForm = new UntypedFormGroup({});
  subscriptions = new SubSink();
  saveSpinner = false;
  thirdVal = "";

  @Input() showSaveButton = true;

  testItemId:any;

  choices = ['True', 'False'];

  public Editor = ckcustomBuild;
  public configCKEditor = {
    toolbar: [
      'bold',
      'italic',
      'underline',
      '|',
      'link',
      '|',
      'bulletedList',
      'numberedList',
    ],
  };

  constructor(
    private dataBroadcastService: DataBroadcastService,
    private testItemService: TestItemService,
    private alert: SweetAlertService,
    private flyPanelSrvc:FlyInPanelService,
  ) { }

  ngOnInit(): void {
    this.true_false_array = [
      { id: 1, text: 'True', check: false },
      { id: 2, text: 'False', check: false },
    ];

    this.trueFalseForm.addControl('correctAnswer', new UntypedFormControl('', Validators.required));
    this.trueFalseForm.addControl('question', new UntypedFormControl('', Validators.required));

    if(this.update){
      this.getQuestionData();
    }
  }

  async getQuestionData(){
    await this.testItemService.get(this.question_Id).then((res:TestItem)=>{
      this.getAnswers(res.id);
      this.testItemId = res.id;
      this.trueFalseForm.get('question')?.setValue(res.description);
    }).catch((err:any)=>{
      this.alert.errorToast("Error Fetching Question Data");
    })
  }

  async getAnswers(id:any){
    await this.testItemService.getTrueFalse(id).then((res:TestItemTrueFalse[])=>{
      var trueVal = res.find((x:TestItemTrueFalse)=> x.isCorrect === true);
      if(res.length>2){
        var thirdVal =(res.find((x:TestItemTrueFalse)=> (x.choices!== "True") && (x.choices !== "False")))?.choices;
        this.thirdVal = thirdVal === undefined ? "":thirdVal;
        this.addThirdOption(thirdVal);
        //this.trueFalseForm.get('description')?.setValue(thirdVal);
      }
      var setVal = trueVal?.choices === "True" ? 1:(trueVal?.choices === "False" ? 2:3);
      this.trueFalseForm.get('correctAnswer')?.setValue(setVal);
    })
  }

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.dataBroadcastService.trueFalseSaved.subscribe((res: any) => {
      if (res === null) {
        this.saveSpinner = false;
      }
      else {
        this.saveData(res.id);
      }
    })
  }

  saveData(id:any){
    for (var i = 0; i < 2; i++) {
      var options = new TestItemTrueFalseCreateOptions();
      options.choices = this.choices[i];
      options.isCorrect = this.trueFalseForm.get('correctAnswer')?.value === (i + 1);
      options.testItemId = id;
      this.saveDefaultTestItems(options, i);
    }
    if (this.thirdOption) {
      this.saveThird(id);
    }
  }

  async saveThird(id: any) {
    var options = new TestItemTrueFalseCreateOptions();
    options.choices = this.trueFalseForm.get('description')?.value;
    options.isCorrect = this.trueFalseForm.get('correctAnswer')?.value === 3;
    options.testItemId = id;
    await this.testItemService.createTrueFalse(options).then((res: any) => {
      this.alert.successToast("Saved True / False Test type");
      this.saveSpinner = false;
      this.dataBroadcastService.questionSaved.next({isSaved :true,id:id});;
    }).catch((err: any) => {
      this.dataBroadcastService.questionSaved.next({isSaved :true});;
      this.alert.errorToast("Failed to save true / false test item");
      this.saveSpinner = false;
    })
  }

  async saveDefaultTestItems(options: TestItemTrueFalseCreateOptions, i: any) {
    await this.testItemService.createTrueFalse(options).then((res: any) => {
      if ((i + 1) === 2 && !this.thirdOption) {
        this.alert.successToast("Saved True / False Test type");
        this.saveSpinner = false;
        this.flyPanelSrvc.close();
        this.dataBroadcastService.questionSaved.next({isSaved :true,id:options.testItemId});;
      }
    }).catch((err: any) => {
      this.alert.errorToast("Failed to save true / false test item");
      this.saveSpinner = false;
    })
  }

  updateData(){
    this.testItemService.removeTrueFalseItems(this.testItemId).then((res:any)=>{
      this.saveData(this.question_Id);
      this.dataBroadcastService.updateQuestion.next({id:this.question_Id,question:this.trueFalseForm.get('question')?.value});
    }).catch((err:any)=>{
      this.alert.errorAlert("Failed To Remove True False Data");
    })
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  onReady(editor: any) {
    // 
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  OnAddChoice() {
    
    this.true_false_array.push({
      id: this.true_false_array.length + 1,
      text: '',
      check: false
    })
  }

  OnDelete(id: any) {
    
    
    for (let i = 0; i < this.true_false_array.length; i++) {
      if (this.true_false_array[i].id == id) {
        this.true_false_array.splice(i, 1);
      }
    }
  }

  OnSelectionChange(id: any) {
    for (let i = 0; i < this.true_false_array.length; i++) {
      if (this.true_false_array[i].id == id) {
        this.true_false_array[i].check = true;
      }
      else {
        this.true_false_array[i].check = false;

      }
    }

  }

  saveTrueFalse(addToList:boolean) {
    this.saveSpinner = true;
    this.dataBroadcastService.saveTrueFalse.next({question:this.trueFalseForm.get('question')?.value,add:addToList});
  }

  addThirdOption(data:any = "") {
    
    this.trueFalseForm.addControl('description', new UntypedFormControl(data, Validators.required));
    this.thirdOption = true;
  }

  deleteThirdOption() {
    if (this.trueFalseForm.get('correctAnswer')?.value === 3) {
      this.trueFalseForm.get('correctAnswer')?.reset();
    }
    this.trueFalseForm.removeControl('description');
    this.thirdOption = false;
  }

  updateTestItem(){

  }

  saveFromEO(options : TestItemCreateOptions){
    options.description = this.trueFalseForm.get('question')?.value;
    if(this.trueFalseForm.valid){
      this.testItemService.create(options).then((res:TestItem)=>{
        this.saveData(res.id);
      });
    }
    else{
      this.dataBroadcastService.questionSaved.next({isSaved : false});
      this.alert.errorAlert("Invalid Data","Question Description and At least 1 correct option is required");
    }
  }

}
