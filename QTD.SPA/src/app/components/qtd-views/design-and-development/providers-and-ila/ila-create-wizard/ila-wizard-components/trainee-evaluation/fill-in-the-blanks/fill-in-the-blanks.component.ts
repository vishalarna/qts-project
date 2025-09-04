import { TestItemFillBlankCreateOptions } from './../../../../../../../../_DtoModels/TestItemFillBlank/TestItemFillBlankCreateOptions';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { UploadAdapter } from 'src/app/_Shared/Utils/CKEditor_UploadAdapter';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SubSink } from 'subsink';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import parse from 'node-html-parser';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemOptions } from 'src/app/_DtoModels/TestItem/TestItemOptions';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';

@Component({
  selector: 'app-fill-in-the-blanks',
  templateUrl: './fill-in-the-blanks.component.html',
  styleUrls: ['./fill-in-the-blanks.component.scss']
})
export class FillInTheBlanksComponent implements OnInit {
  @Input() update: boolean;
  @Input() question_Id: any;
/*   @ViewChild('editor') editor:any; */
  @Output() close_flypanel = new EventEmitter<any>();
  editor_string:any;
 /*  editor_string_array :any=[];
  temp:any=[]; */
  subscriptions = new SubSink();
  public Editor = ckcustomBuild;
 /*  markCheck: boolean = false; */
  answers: string[] = [];
  previewString: string = '';
  saveSpinner = false;
  saveandAddSpinner = false;
  updateSpinner = false;
  testItemId: any;

  @Input() showSaveButton = true;

  EditorForm: UntypedFormGroup = new UntypedFormGroup({
    htmlContent: new UntypedFormControl(),
  });

  FillBlank: UntypedFormGroup = new UntypedFormGroup({
    fillBlank: new UntypedFormControl('', Validators.compose([
      Validators.required,
    ])),
  });


  constructor( private dataBroadcastService:DataBroadcastService,
    private alert: SweetAlertService,
    private testItemService:TestItemService) { }

  ngOnInit(): void {
    if (this.update) {
      this.getFillBlank();
    }
  }

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.dataBroadcastService.fillBlankSaved.subscribe((res)=>{
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

  onReady(editor: any) {
    // 
    editor.plugins.get('FileRepository').createUploadAdapter = function (
      loader: any
    ) {
      return new UploadAdapter(loader);
    };
  }

  async getFillBlank() {
    await this.testItemService.get(this.question_Id).then((res: TestItem) => {
      this.testItemId = res.id;
      this.FillBlank.get('fillBlank')?.setValue(res.description);
    }).catch((err: any) => {
      this.alert.errorToast("Error Fetching Fill In the Blank Question Data");
    })
  }

  generateBlanks() {
    
    this.answers = [];
    this.editor_string = this.FillBlank.get('fillBlank')?.value;
    let root = parse(this.editor_string);
    let elements = root.getElementsByTagName('u');
    if (elements.length == 0) {
      this.alert.errorAlert('Please underline at least one word as correct answer');
      return;
    }
    for (const el of elements) {
      this.answers.push(el.innerText);
    }
    
    this.generatePreview();
  }

  generatePreview() {
    
    this.previewString = this.editor_string;
    for (const ans of this.answers) {
      this.previewString = this.previewString.replace(
        ans,
        '&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp'
      );
    }
    
  }

  saveTestItem(id: any) {
    var index = 0;
    var i = 0;
    this.answers.forEach(async (element: any) => {
      var options = new TestItemFillBlankCreateOptions();
      options.testItemId = id;
      options.correctIndex = i+1;
      options.correct = element;
      await this.testItemService.createFillInBlank(options).then((res: TestItem) => {
        index++;
        if (index === this.answers.length) {
          this.alert.successAlert("Fill In The Blank Test Item Saved");
          this.saveSpinner = false;
          this.saveandAddSpinner = false;
          this.close_flypanel.emit(true);
          this.dataBroadcastService.questionSaved.next({isSaved :true,id:id});
        }
      }).catch((err: any) => {
        this.saveSpinner = false;
        this.saveandAddSpinner = false;
        this.alert.errorToast("Error Creating Fill In The Blank Test Item");
        this.dataBroadcastService.questionSaved.next({isSaved:false});
      })
    })
  }

  updateData() {
    if(this.previewString === ''){
      this.alert.errorAlert('Please Generate Preview');
    }
    else{
      this.testItemService.removeFillInBlank(this.testItemId).then((res: any) => {
        this.saveTestItem(this.testItemId);
        this.dataBroadcastService.updateQuestion.next({ id: this.question_Id, question: this.FillBlank.get('fillBlank')?.value });
      }).catch((err: any) => {
        this.alert.errorToast("Error Removing Fill In The Blank items");
      })
    }
  }

  /* onMarkAsCorrect(){
    this.editor_string = this.editor_string.replace("<p>",'').replace("</p>",'');
    this.editor_string_array = this.editor_string;
    
    let re = /<u>(.*?)<\/u>/g;
    let match :any;
    while(match = re.exec(this.editor_string)){
        this.temp.push(match?.[1]);
    }
    if(this.temp.length === 0){
      this.alert.errorAlert('Please Underline Atleast 1 Word');
    }
    else {
      //distinct array
      this.temp = this.temp.filter((item:any, index:any, self:any) => self.indexOf(item) === index);
      
      this.markCheck=true;
      this.OnReadyPreviewTextBox();
    }
  } */

 /*  OnReadyPreviewTextBox(){
    if(this.markCheck===true){
      this.temp.forEach((i:any) => {
        
        this.editor_string = this.editor_string.replace(i,'___').replace(/<[^>]+>/g, '');
      });

      
      
      this.FillBlankGroup.patchValue({
        fillblank: this.editor_string
      });
    }
    this.temp=[];
  }
 */
  /* onUnmark(){
    
    let re = /<u>(.*?)<\/u>/g;
    let match :any;
    let temp1:any=[];
    while(match = re.exec(this.new_string)){
      temp1.push(match?.[1]);
    }

    temp1.forEach((i:any) => {
    
    this.editor_string = this.editor_string.replace(i,'___').replace(/<[^>]+>/g, '');
    });
    this.FillBlankGroup.patchValue({
      fillblank: this.editor_string.replace('<p>','').replace('</p>','')
    });

  } */

 /*  onChange({ editor }: any ) {
    this.editor_string = editor.getData();
    this.new_string = editor.getData();
    

  } */

  OnSave(addToList: boolean) {
    if (addToList) {
      if(this.previewString === ''){
        this.alert.errorAlert('Please Generate Preview');
      }
      else{
        this.saveandAddSpinner = true;
        this.dataBroadcastService.saveFillBlank.next({ question: this.FillBlank.get('fillBlank')?.value, add: addToList });
      }
    }
    else {
      if(this.previewString === ''){
        this.alert.errorAlert('Please Generate Preview');
      }
      else{
        this.saveSpinner = true;
        this.dataBroadcastService.saveFillBlank.next({ question: this.FillBlank.get('fillBlank')?.value, add: addToList });
      }
    }
  }

  saveFromEO(options : TestItemCreateOptions){
    options.description = this.FillBlank.get('fillBlank')?.value;
    if(this.FillBlank.valid && this.previewString !== '' && this.answers.length > 0){
      this.testItemService.create(options).then((res:TestItem)=>{
        this.saveTestItem(res.id);
      });
    }
    else{
      this.dataBroadcastService.questionSaved.next({isSaved : false});
      this.alert.errorAlert("Invalid Data","Please Generate a blank with 1 correct answer marked");
    }
  }

}
