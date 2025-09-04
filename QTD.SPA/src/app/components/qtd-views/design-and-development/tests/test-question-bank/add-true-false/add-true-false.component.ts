import { Component, OnInit, AfterViewInit, OnDestroy, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemTrueFalse } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalse';
import { TestItemTrueFalseCreateOptions } from 'src/app/_DtoModels/TestItemTrueFalse/TestItemTrueFalseCreateOptions';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';

@Component({
  selector: 'app-add-true-false',
  templateUrl: './add-true-false.component.html',
  styleUrls: ['./add-true-false.component.scss']
})
export class AddTrueFalseComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() isCloseClick:boolean = false;
  @Output() closeByValue = new EventEmitter<boolean>();
  choiceList = ["True", "False"];
  tfForm = new UntypedFormGroup({});
  subscription = new SubSink();
  saveSpinner = false;
  saveAndAddSpinner = false;
  close = true;
  AddAnother:boolean=false;

  @Input() type!: TestItemType;
  @Input() levelId = "";
  @Input() eoId: any = null;

  @Input() previousData!: any;
  @Input() mode: 'add' | 'edit' | 'copy' = 'add';
  @Input() showSaveButton = true;
  @Output() itemSaved: EventEmitter<any> = new EventEmitter();
  tfData: TestItemTrueFalse[] = [];

  @ViewChild("ckeditor") ckeditor: any;
  onEventOrRequest(event: any) {
    this.ckeditor.instance.setData('');
  }
  Editor = ckcustomBuild;

  constructor(
    private TestItemService: TestItemService,
    private flyPanelService: FlyInPanelService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    public dialog: MatDialog,
  ) { }

  ngOnInit(): void {
    this.readyTFForm();
    if (this.mode !== 'add') {
      this.readyData();
    }
  }

  ngAfterViewInit(): void {
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyData() {
    this.tfData = await this.TestItemService.getTrueFalse(this.previousData.id);
    var trueData = this.tfData.find((data) => {
      return data.isCorrect;
    });
    if (trueData) {
      this.tfForm.patchValue({
        question: this.previousData.question,
        correct: trueData.choices.trim().toLowerCase() === 'true' ? 1 : 2,
      })
    }
  }

  readyTFForm() {
    this.tfForm.addControl('question', new UntypedFormControl('', Validators.required));
    this.tfForm.addControl('correct', new UntypedFormControl('', Validators.required));
  }

  testFunction() {
    
  }

  async saveTF(event: any, shouldClose: boolean) {
    shouldClose ? this.saveSpinner = true : this.saveAndAddSpinner = true;
    var options = new TestItemCreateOptions();
    options.description = this.tfForm.get('question')?.value;
    options.eOId = this.eoId;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    if (this.mode === 'copy' && options.description.trim().toLowerCase() === this.previousData.question.trim().toLowerCase()) {
      options.description = options.description + ' - Copy';
    }
 /*    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate']; */
    
    await this.TestItemService.create(options).then((res: TestItem) => {
      
      this.saveTrueFalseData(res.id, shouldClose);
    }).finally(() => {
      this.saveSpinner = false;
      this.saveAndAddSpinner = false;
    });
  }

  async saveTrueFalseData(id: any, shouldClose: boolean) {
    
    var options = new TestItemTrueFalseCreateOptions();
    options.testItemId = id;
    options.choices = "True";
    options.isCorrect = this.tfForm.get('correct')?.value === 1 ? true : false;
    await this.TestItemService.createTrueFalse(options).then(async (_) => {
      options.choices = "False";
      options.isCorrect = this.tfForm.get('correct')?.value === 2 ? true : false;
      await this.TestItemService.createTrueFalse(options).then((_) => {
        
        if (this.mode === 'add') {
          this.alert.successToast("True False Data Saved Successfully");
          if(this.AddAnother){
            this.alert.successToast("True False Data Saved Successfully");
            shouldClose=false;
          }
        }
        else {
          this.alert.successToast(`True False Data ${this.mode === 'copy' ? "Copied" : "Updated"} Successfully`);
        }

        if (shouldClose) {
          if(this.isCloseClick){
            this.closeByValue.emit(false);
          }else{
            this.flyPanelService.close();
          }
        }
        else {
          this.tfForm.reset();
          this.AddAnother=false;
        }

        this.dataBroadcastService.refreshTestItem.next({ close: shouldClose,id:id });
        this.itemSaved.emit(id);
      }).finally(() => {
        this.saveSpinner = false;
        this.saveAndAddSpinner = false;
      });
    });
  }

  updateTFData(event: any) {
    this.saveSpinner = true;
    var options = new TestItemCreateOptions();
    options.description = this.tfForm.get('question')?.value;
    options.eOId = this.eoId;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    this.TestItemService.update(this.previousData.id, options).then((res: TestItem) => {
      this.TestItemService.removeTrueFalseItems(res.id).then((_) => {
        this.saveTrueFalseData(res.id, true);
      })
    });
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
