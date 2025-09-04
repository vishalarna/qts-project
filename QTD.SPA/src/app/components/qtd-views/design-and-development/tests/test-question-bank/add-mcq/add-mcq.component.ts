import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemMcq } from 'src/app/_DtoModels/TestItemMcq/TestItemMcq';
import { TestItemMcqCreateOptions } from 'src/app/_DtoModels/TestItemMcq/TestItemMcqCreateOptions';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import {CdkDragDrop, CdkDropList, CdkDrag, moveItemInArray} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-add-mcq',
  templateUrl: './add-mcq.component.html',
  styleUrls: ['./add-mcq.component.scss']
})
export class AddMcqComponent implements OnInit {
  @Input() isCloseClick:boolean = false;
  @Output() closeByValue = new EventEmitter<boolean>();
  mcqForm = new UntypedFormGroup({});
  distractors: Distractors[] = [];
  saveAndAddSpinner = false;
  saveSpinner = false;
  close = true;

  @Input() type!: TestItemType;
  @Input() showSaveButton = true;
  @Input() levelId = "";
  @Input() eoId: any = null;
  @Input() previousData!: any;
  @Input() mode: 'add' | 'edit' | 'copy' = 'add';

  @Output() itemSaved: EventEmitter<any> = new EventEmitter();
  mcqData: TestItemMcq[] = [];
  AddAnother:any;

  @ViewChild("ckeditor") ckeditor: any;
  onEventOrRequest(event: any) {
    this.ckeditor.instance.setData('');
  }
  Editor = ckcustomBuild;

  constructor(
    private testItemService: TestItemService,
    private flyPanelService: FlyInPanelService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    public dialog: MatDialog,
  ) {
  }

  ngOnInit(): void {
    this.readyDistractorAndForm();
    if (this.mode !== 'add') {
      this.insertData();
    }
  }

  async insertData() {
    this.mcqData = await this.testItemService.getMCQ(this.previousData.id);
    this.mcqForm.patchValue({
      question: this.previousData.question,
    });
    this.mcqData.forEach((data, i) => {
      if (i > 3) {
        this.mcqForm.addControl(`description${i}`, new UntypedFormControl(data.choiceDescription, Validators.required));
        this.distractors.push({
          id: i,
          text: '',
        })
      }
      else {
        this.mcqForm.patchValue({
          ['description' + i]: data.choiceDescription,
        })
      }

      if (data.isCorrect) {
        this.mcqForm.patchValue({
          correct: i,
        })
      }
    })
  }

  readyDistractorAndForm() {
    this.mcqForm.addControl('question', new UntypedFormControl('', Validators.required));
    this.mcqForm.addControl('correct', new UntypedFormControl('', Validators.required));
    for (var i = 0; i < 4; i++) {
      this.mcqForm.addControl(`description${i}`, new UntypedFormControl('', Validators.required));
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
    this.mcqForm.removeControl(`description${id}`);
    if (this.mcqForm.get('correct')?.value === id) {
      this.mcqForm.get('correct')?.reset;
      this.mcqForm.get('correct')?.setErrors({
        require: false,
      });
    }

    
  }

  addDistractor() {
    this.mcqForm.addControl(`description${this.distractors[this.distractors.length - 1].id + 1}`, new UntypedFormControl('', Validators.required));
    this.distractors.push({
      id: this.distractors[this.distractors.length - 1].id + 1,
      text: '',
    })
  }

  async saveMCQ(event:any,shouldClose: boolean) {
    shouldClose ? this.saveSpinner = true : this.saveAndAddSpinner = true;
    var options = new TestItemCreateOptions();
    options.description = this.mcqForm.get('question')?.value;
    options.eOId = this.eoId;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    if (this.mode === 'copy' && options.description === this.previousData.question) {
      options.description = options.description + " - Copy";
    }
   /*  var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate']; */
    
    await this.testItemService.create(options).then((res: TestItem) => {
      this.saveMCQData(res.id, shouldClose);
    }).finally(() => {
      this.saveSpinner = false;
      this.saveAndAddSpinner = false;
    });
  }

  saveMCQData(id: any, shouldClose: boolean) {
    var options = new TestItemMcqCreateOptions();
    options.testItemId = id;
    this.distractors.forEach((data, i) => {
      options.isCorrect = this.mcqForm.get('correct')?.value === data.id;
      options.choiceDescription = this.mcqForm.get(`description${data.id}`)?.value;
      options.number = i + 1;
      this.testItemService.createMcq(options).then((_) => {
        if (i === (this.distractors.length - 1)) {
          if (this.mode !== 'add') {
            this.alert.successToast(`MCQ Data ${this.mode === 'copy' ? "Copied" : "Updated"} Successfully`);
          }
          else {
            this.alert.successToast("MCQ Data Saved Successfully");
            if(this.AddAnother){
              this.alert.successToast("MCQ Data Saved Successfully");
              shouldClose = false;
              this.AddAnother = false;
            }
          }
          this.dataBroadcastService.refreshTestItem.next({ close: shouldClose,id:id });
          this.itemSaved.emit(id);
        }
      }).finally(() => {
        if (i === (this.distractors.length - 1)) {
          this.saveSpinner = false;
          this.saveAndAddSpinner = false;
          shouldClose ? (this.isCloseClick?this.closeByValue.emit(false):this.flyPanelService.close()): this.mcqForm.reset();
        }
      })
    })
  }

  async updateMCQ(event: any) {
    var options = new TestItemCreateOptions();
    options.description = this.mcqForm.get('question')?.value;
    options.eOId = this.eoId;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
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

  openSaveDialog(templateRef: any) {
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  drop(event: CdkDragDrop<any[]>) {
    moveItemInArray(this.distractors, event.previousIndex, event.currentIndex);
  }
}

export class Distractors {
  id!: any;
  text!: any;
}
