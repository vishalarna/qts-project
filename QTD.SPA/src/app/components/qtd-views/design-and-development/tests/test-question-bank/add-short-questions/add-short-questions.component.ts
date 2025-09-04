import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemShortAnswer } from 'src/app/_DtoModels/TestItemShortAnswer/TestItemShortAnswer';
import { TestItemShortAnswerCreateOptions } from 'src/app/_DtoModels/TestItemShortAnswer/TestItemShortAnswerCreateOptions';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';


@Component({
  selector: 'app-add-short-questions',
  templateUrl: './add-short-questions.component.html',
  styleUrls: ['./add-short-questions.component.scss']
})
export class AddShortQuestionsComponent implements OnInit {
  @Input() isCloseClick:boolean = false;
  @Output() closeByValue = new EventEmitter<boolean>();
  acceptableResponses: Short_Answer[] = [];
  default_number = 1;
  shortAnswerForm = new UntypedFormGroup({});
  acceptableResponseForm = new UntypedFormGroup({});
  selection = new SelectionModel<any>(false);
  indexes: number[] = [];
  correctIndexesControl = new UntypedFormControl([]);
  saveSpinner = false;
  saveAndAddSpinner = false;
  close = true;
  AddAnother:any;

  @Input() type!: TestItemType;
  @Input() showSaveButton = true;
  @Input() levelId = "";
  @Input() eoId: any = null;
  @Input() previousData!: any;
  @Input() mode: 'add' | 'edit' | 'copy' = 'add';
  @Output() itemSaved: EventEmitter<any> = new EventEmitter();
  sqData: TestItemShortAnswer[] = [];

  @ViewChild("ckeditor") ckeditor: any;
  onEventOrRequest(event: any) {
    this.ckeditor.instance.setData('');
  }
  Editor = ckcustomBuild;

  constructor(
    private testItemService: TestItemService,
    private alert: SweetAlertService,
    private flyPanelService: FlyInPanelService,
    private dataBroadcastService: DataBroadcastService,
    public dialog: MatDialog,
  ) { }

  ngOnInit(): void {
    
    this.readySQForm();
    this.readyAcceptableResponseForm();
    if (this.mode === 'edit' || this.mode === 'copy' || this.mode === undefined) {
      this.insertPreviousData();
    }
  }

  async insertPreviousData() {
    this.shortAnswerForm.patchValue({
      question: this.previousData.question,
    });
    this.sqData = await this.testItemService.getShortAnswers(this.previousData.id);
    
    this.sqData.forEach((data, i) => {
      if (i > 0) {
        this.shortAnswerForm.addControl(`acceptedRes${i}`, new UntypedFormControl(data.responses, Validators.required));
        this.shortAnswerForm.addControl(`caseSensitive${i}`, new UntypedFormControl(data.isCaseSensitive));
        this.acceptableResponses.push({
          id: i,
          text: ''
        })
        this.indexes.push(i + 1);
      }
      else {
        this.shortAnswerForm.patchValue({
          ['acceptedRes' + i]: data.responses,
          ['caseSensitive' + i]: data.isCaseSensitive,
        });
      }
    });

    var correctResponses = this.sqData.map((data) => {
      return data.acceptableResponses;
    });
    if (correctResponses.every((data) => { return data === 1 })) {
      this.acceptableResponseForm.get('all')?.setValue(true);
      this.selection.select(1);
    }
    else {
      this.acceptableResponseForm.get('selected')?.setValue(true);
      this.selection.select(2);
      var correctValues: number[] = [];
      correctResponses.forEach((data, i) => {
        if (data === 1) {
          correctValues.push(i + 1);
        }
      });
      this.correctIndexesControl.setValue(correctValues);
    }
  }

  IncreaseNumber() {
    if (this.default_number < this.acceptableResponses.length) {
      this.default_number++;
    }
  }

  DecreaseNumber() {
    if (this.default_number > 1) {
      this.default_number--;
    }
  }

  readySQForm() {
    this.shortAnswerForm.addControl('question', new UntypedFormControl('', Validators.required));
    for (let i = 0; i < 1; i++) {
      this.shortAnswerForm.addControl(`acceptedRes${i}`, new UntypedFormControl('', Validators.required));
      this.shortAnswerForm.addControl(`caseSensitive${i}`, new UntypedFormControl(false));
      this.acceptableResponses.push({
        id: i,
        text: ''
      })
      this.indexes.push(i + 1);
    }
  }

  readyAcceptableResponseForm() {
    this.acceptableResponseForm.addControl('all', new UntypedFormControl(false));
    this.acceptableResponseForm.addControl('selected', new UntypedFormControl(false));

  }

  OnAddResponses() {
    this.shortAnswerForm.addControl(`acceptedRes${this.acceptableResponses.length}`, new UntypedFormControl('', Validators.required));
    this.shortAnswerForm.addControl(`caseSensitive${this.acceptableResponses.length}`, new UntypedFormControl(false));
    this.acceptableResponses.push({
      id: this.acceptableResponses.length,
      text: '',
    })
    this.indexes.push(this.indexes.length + 1);
  }

  onRemoveResponse() {
    this.acceptableResponses.pop();
    this.shortAnswerForm.removeControl(`acceptedRes${this.acceptableResponses.length}`);
    this.shortAnswerForm.removeControl(`caseSensitive${this.acceptableResponses.length}`);
    var value = this.indexes.pop();
    if (value) {
      this.removeIndex(value);
    }
  }

  selectionChange(event: any, id: any) {
    if (event.checked) {
      this.selection.select(id);
    }
    else {
      this.selection.deselect(id);
    }
  }

  removeIndex(index: number) {
    const corrects = this.correctIndexesControl.value as number[];
    this.removeFirst(corrects, index);
    this.correctIndexesControl.setValue(corrects);
  }

  OnClick(selected: boolean, index: any) {
    if (selected) {
      var values = this.correctIndexesControl.value as number[];
      values.push(index);
      this.correctIndexesControl.setValue(values);
    }
    else {
      this.removeIndex(index);
    }

    
  }

  removeFirst(array: number[], toRemove: number) {
    const index = array.indexOf(toRemove);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }

  compareObjects(object1: any, object2: any) {
    return object1 && object2 && object1 == object2;
  }

  async saveTestItem(event: any, shouldClose: boolean) {
    shouldClose ? this.saveSpinner = true : this.saveAndAddSpinner = true;
    var options = new TestItemCreateOptions();
    options.eOId = this.eoId;
    options.description = this.shortAnswerForm.get('question')?.value;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    if (this.mode === 'copy' && options.description.trim().toLowerCase() === this.previousData.question.trim().toLowerCase()) {
      options.description = options.description + ' - Copy';
    }
   /*  var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate']; */
    await this.testItemService.create(options).then((res: TestItem) => {
      this.saveSQData(res.id, shouldClose);
    }).finally(() => {
      this.saveSpinner = false;
      this.saveAndAddSpinner = false;
    })
  }

  saveSQData(id: any, shouldClose: boolean) {
    var options = new TestItemShortAnswerCreateOptions();
    options.testItemId = id;
    this.acceptableResponses.forEach(async (_, i) => {
      this.saveSpinner = true;
      this.saveAndAddSpinner = true;
      options.responses = this.shortAnswerForm.get(`acceptedRes${i}`)?.value;
      options.isCaseSensitive = this.shortAnswerForm.get(`caseSensitive${i}`)?.value ?? false;
      options.number = (i + 1);
      if (this.selection.selected[0] === 1) {
        options.acceptableResponses = 1;
      }
      else {
        options.acceptableResponses = this.correctIndexesControl.value.includes((i + 1)) ? 1 : 0;
      }
      
      await this.testItemService.createShortAnswer(options).then((_) => {
        if ((this.acceptableResponses.length - 1) === i) {
          if (this.mode === 'add') {
            this.alert.successToast("Short Answer Saved Successfully");
            if(this.AddAnother){
              this.alert.successToast("Short Answer Saved Successfully");
              shouldClose = false;
            }
          }
          else {
            this.alert.successToast(`Short Answer ${this.mode === 'copy' ? "Copied" : "Updated"} Successfully`);
          }
          if (shouldClose) {
            if(this.isCloseClick){
              this.closeByValue.emit(false);
            }else{
              this.flyPanelService.close();
            }
          }
          else {
            this.shortAnswerForm.reset();
            this.acceptableResponseForm.reset();
            this.correctIndexesControl.setValue([]);
            this.selection.clear();
            this.AddAnother=false;
          }
          this.dataBroadcastService.refreshTestItem.next({ close: shouldClose,id:id });
          this.itemSaved.emit(id);
        }
      }).finally(() => {
        if (this.acceptableResponses.length - 1 === i) {
          this.saveSpinner = false;
          this.saveAndAddSpinner = false;
        }
      })
    })
  }

  async editTestItem(event: any) {
    this.saveSpinner = true;
    var options = new TestItemCreateOptions();
    options.eOId = this.eoId;
    options.description = this.shortAnswerForm.get('question')?.value;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    
    await this.testItemService.update(this.previousData.id, options).then((res: TestItem) => {
      this.testItemService.removeSA(this.previousData.id).then((_) => {
        this.saveSQData(res.id, true);
      })
    }).finally(() => {

    })
  }

  openDialog(templateRef: any) {
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openSaveDialog(templateRef: any) {
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
}

export class Short_Answer {
  id: any;
  text: string
}
