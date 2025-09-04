import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemMcq } from 'src/app/_DtoModels/TestItemMcq/TestItemMcq';
import { TestItemMcqCreateOptions } from 'src/app/_DtoModels/TestItemMcq/TestItemMcqCreateOptions';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-add-multiple-correct-answer',
  templateUrl: './add-multiple-correct-answer.component.html',
  styleUrls: ['./add-multiple-correct-answer.component.scss']
})
export class AddMultipleCorrectAnswerComponent implements OnInit {
  @Input() isCloseClick:boolean = false;
  @Output() closeByValue = new EventEmitter<boolean>();
  mcaForm = new UntypedFormGroup({});
  distractors: Distractors[] = [];
  selection = new SelectionModel<any>(true, []);
  saveAndAddSpinner = false;
  saveSpinner = false;
  close = true;
  AddAnother:any;

  @Input() type!: TestItemType;
  @Input() showSaveButton:boolean = true;
  @Input() levelId = "";
  @Input() eoId: any = null;
  @Input() previousData!: any;
  @Input() mode: 'add' | 'edit' | 'copy' = 'add';
  @Output() itemSaved: EventEmitter<any> = new EventEmitter();
  mcaData: TestItemMcq[] = [];

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
    public dialog : MatDialog,
  ) { }

  ngOnInit(): void {
    this.readyDistractorAndForm();
    if (this.mode !== 'add') {
      this.insertData();
    }
  }

  async insertData() {
    this.mcaForm.patchValue({
      question: this.previousData.question,
    });

    this.mcaData = await this.testItemService.getMCQ(this.previousData.id);
    
    this.mcaData.forEach((data, i) => {
      if (i > 3) {
        this.mcaForm.addControl(`description${i}`, new UntypedFormControl(data.choiceDescription, Validators.required));
        this.mcaForm.addControl(`isCorrect${i}`, new UntypedFormControl(data.isCorrect));
        this.distractors.push({
          id: i,
          text: '',
        })
      }
      else {
        this.mcaForm.patchValue({
          ['description' + i]: data.choiceDescription,
          ['isCorrect' + i]: data.isCorrect,
        });
      }
      if (data.isCorrect) {
        this.selection.select(i);
      }
    })
  }

  readyDistractorAndForm() {
    this.mcaForm.addControl('question', new UntypedFormControl('', Validators.required));
    for (var i = 0; i < 4; i++) {
      this.mcaForm.addControl(`description${i}`, new UntypedFormControl('', Validators.required));
      this.mcaForm.addControl(`isCorrect${i}`, new UntypedFormControl(false));
      this.distractors.push({
        id: i,
        text: '',
      })
    }
  }

  removeDistractor(id: any) {
    this.distractors = this.distractors.filter((data) => {
      return data.id !== id;
    });
    this.mcaForm.removeControl(`description${id}`);
    this.mcaForm.removeControl(`isCorrect${id}`);
    this.selection.deselect(id);
  }

  addDistractor() {
    this.mcaForm.addControl(`description${this.distractors[this.distractors.length - 1].id + 1}`, new UntypedFormControl('', Validators.required));
    this.mcaForm.addControl(`isCorrect${this.distractors[this.distractors.length - 1].id + 1}`, new UntypedFormControl(false));

    this.distractors.push({
      id: this.distractors[this.distractors.length - 1].id + 1,
      text: '',
    })
  }

  selectionChanged(event: any, id: any) {
    if (event.checked) {
      this.selection.select(id);
    }
    else {
      this.selection.deselect(id);
    }
  }

  async saveMCQ(event:any,shouldClose: boolean) {
    shouldClose ? this.saveSpinner = true : this.saveAndAddSpinner = true;
    var options = new TestItemCreateOptions();
    options.description = this.mcaForm.get('question')?.value;
    options.eOId = this.eoId;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    if (this.mode === 'copy' && options.description === this.previousData.question) {
      options.description = options.description + " - Copy";
    }
  /*   var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate']; */
    
    await this.testItemService.create(options).then((res: TestItem) => {
      this.saveMCQData(res.id, shouldClose);
    }).finally(() => {
    });
  }

  saveMCQData(id: any, shouldClose: boolean) {
    var options = new TestItemMcqCreateOptions();
    options.testItemId = id;
    this.distractors.forEach((data, i) => {
      options.isCorrect = this.mcaForm.get(`isCorrect${data.id}`)?.value;
      options.choiceDescription = this.mcaForm.get(`description${data.id}`)?.value;
      options.number = i + 1;
      this.testItemService.createMcq(options).then((_) => {
        if (i === (this.distractors.length - 1)) {
          
          if (this.mode === 'add') {
            this.alert.successToast("Multiple Correct Answers data Saved Successfully");
            if(this.AddAnother){
              this.alert.successToast("Multiple Correct Answers data Saved Successfully");
              shouldClose=false;
            }
          }
          else {
            this.alert.successToast(`Multiple Correct Answers Data ${this.mode === 'copy' ? 'Copied' : 'Updated'} Successfully`);
          }
          this.dataBroadcastService.refreshTestItem.next({close:shouldClose,id:id});
          this.itemSaved.emit(id);
        }
      }).finally(() => {
        if (i === (this.distractors.length - 1)) {
          this.saveSpinner = false;
          this.saveAndAddSpinner = false;
          if(shouldClose){
            if(this.isCloseClick){
              this.closeByValue.emit(false);
            }else{
              this.flyPanelService.close();
            }
          }
          else{
            this.mcaForm.reset();
            this.AddAnother=false;
        }
      }

    })
  })
}

  async updateMCA(event: any) {
    this.saveSpinner = true;
    var options = new TestItemCreateOptions();
    options.description = this.mcaForm.get('question')?.value;
    options.eOId = this.eoId;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    await this.testItemService.update(this.previousData.id, options).then(async (res: TestItem) => {
      await this.testItemService.removeMCQ(this.previousData.id).then(() => {
        this.saveMCQData(res.id, true);
      });
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

  openSaveDialog(templateRef:any){
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
}

export class Distractors {
  id!: any;
  text!: any;
}
