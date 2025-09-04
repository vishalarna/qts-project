import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import parse from 'node-html-parser';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemFillBlankCreateOptions } from 'src/app/_DtoModels/TestItemFillBlank/TestItemFillBlankCreateOptions';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TestItemFillBlank } from 'src/app/_DtoModels/TestItemFillBlank/TestItemFillBlank';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { CKEditorComponent, CKEditorModule } from '@ckeditor/ckeditor5-angular';

@Component({
  selector: 'app-add-fill-in-the-blank',
  templateUrl: './add-fill-in-the-blank.component.html',
  styleUrls: ['./add-fill-in-the-blank.component.scss']
})
export class AddFillInTheBlankComponent implements OnInit, AfterViewInit {
  @Input() isCloseClick:boolean = false;
  @Output() closeByValue = new EventEmitter<boolean>();
  public Editor = ckcustomBuild;
  answers: string[] = [];
  editor_string: any;
  previewString = "";
  saveSpinner = false;
  saveandAddSpinner = false;

  @Input() type!: TestItemType;
  @Input() levelId = "";
  @Input() eoId: any = null;
  @Input() showSaveButton: boolean = true;
  FIBCheckbox:any;

  @Input() previousData!: any;
  @Input() mode: 'add' | 'edit' | 'copy' = 'add';

  @Output() itemSaved: EventEmitter<any> = new EventEmitter();

  fibData: TestItemFillBlank[] = [];
  header = "";
  close = true;

  EditorForm: UntypedFormGroup = new UntypedFormGroup({
    htmlContent: new UntypedFormControl(),
  });

  FillBlank: UntypedFormGroup = new UntypedFormGroup({
    fillBlank: new UntypedFormControl('', Validators.compose([
      Validators.required,
    ])),
  });

  constructor(
    private alert: SweetAlertService,
    private testItemService: TestItemService,
    private flyPanelService: FlyInPanelService,
    private dataBroadcastService: DataBroadcastService,
    public dialog: MatDialog,
  ) { }

  ngAfterViewInit(): void {
  }

  ngOnInit(): void {
    if (this.mode !== 'add') {
      this.readyData();
    }
  }

  async readyData() {
    this.FillBlank.get('fillBlank')?.setValue(this.previousData.question);
    this.generateBlanks();
  }

  generateBlanks() {
    this.answers = [];
    this.editor_string = this.FillBlank.get('fillBlank')?.value;
    let root = parse(this.editor_string);
    let elements = root.getElementsByTagName('u');
    // if (elements.length == 0) {
    //   this.alert.errorAlert('Please underline at least one word as correct answer');
    //   return;
    // }
    for (const el of elements) {
      this.answers.push(el.toString());
    }
    
    this.generatePreview();
  }

  unmarkBlank() {
    this.answers = [];
    this.previewString = String(this.editor_string).replace(/<u>/g, "");
    this.previewString = this.previewString.replace(/<\/u>/g, "");

    this.FillBlank.get('fillBlank')?.setValue(this.previewString);
  }

  previewOnly() {
    this.editor_string = this.FillBlank.get('fillBlank')?.value;
    this.previewString = this.editor_string;
    let root = parse(this.editor_string);
    let elements = root.getElementsByTagName('u');
    for (const el of elements) {
      this.previewString = this.previewString.replace(
        (el.toString()),
        '<u>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</u>'
      )
    }
  }

  generatePreview() {
    this.previewString = this.editor_string;
    for (const ans of this.answers) {
      this.previewString = this.previewString.replace(
        ans,
        '<u>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</u>'
      );
    }
    
  }

  async saveFIB(event: any, shouldClose: boolean) {
    shouldClose ? this.saveSpinner = true : this.saveandAddSpinner = true;
    var options = new TestItemCreateOptions();
    options.description = this.FillBlank.get('fillBlank')?.value;
    options.eOId = this.eoId;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    if (this.mode === 'copy' && options.description.trim().toLowerCase() === this.previousData.question.trim().toLowerCase()) {
      options.description = options.description + ' - Copy';
    }
/*     var data = JSON.parse(event);
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason']; */
    await this.testItemService.create(options).then((res: TestItem) => {
      this.saveFIBData(res.id, shouldClose);
    }).finally(() => {
      this.saveSpinner = false;
      this.saveandAddSpinner = false;
    })

  }

  async saveFIBData(id: any, shouldClose: boolean) {
    var index = 0;
    var options = new TestItemFillBlankCreateOptions();
    options.testItemId = id;
    this.answers.forEach(async (element: any, i) => {
      shouldClose ? this.saveSpinner = true : this.saveandAddSpinner = true;
      options.correctIndex = i + 1;
      options.correct = parse(element).innerText;
      
      await this.testItemService.createFillInBlank(options).then((res: TestItem) => {
        index++;
        if (index === this.answers.length) {
          
          if (this.mode === 'add') {
            this.alert.successToast("Fill in The Blank Data Saved Successfully");
            if(this.FIBCheckbox){
              this.alert.successToast("Fill in The Blank Data Saved Successfully");
              shouldClose = false;
              this.answers = [];
              this.previewString = '';
              this.FillBlank.reset();
            }
          }
          else {
            this.alert.successToast(`Fill in The Blank Data ${this.mode === 'copy' ? "Copied" : "Updated"} Successfully`);
          }
          if (shouldClose) {
            if(this.isCloseClick){
              this.closeByValue.emit(false);
            }else{
              this.flyPanelService.close();
            }
          }
          else {
            this.answers = [];
            this.previewString = '';
            this.FillBlank.reset();
            this.FIBCheckbox = false;
          }
          this.dataBroadcastService.refreshTestItem.next({ close: shouldClose, id: id });
          this.itemSaved.emit(id);
        }
      }).finally(() => {
        if (index === this.answers.length) {
          this.saveSpinner = false;
          this.saveandAddSpinner = false;
        }
      })
    })
  }

  async updateFIB(event: any) {
    this.saveSpinner = true;
    var options = new TestItemCreateOptions();
    options.description = this.FillBlank.get('fillBlank')?.value;
    options.eOId = this.eoId;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    var data = JSON.parse(event);
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    await this.testItemService.update(this.previousData.id, options).then(async (res: TestItem) => {
      await this.testItemService.removeFillInBlank(res.id).then(() => {
        this.saveFIBData(res.id, true);
      });
    }).finally(() => {
      this.saveSpinner = false;
    })
  }

  openDialog(templateRef: any) {
    this.header = "Update Fill In The Blank";
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openSaveDialog(templateRef: any) {
    this.header = "Save Fill In The Blank";
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
}
