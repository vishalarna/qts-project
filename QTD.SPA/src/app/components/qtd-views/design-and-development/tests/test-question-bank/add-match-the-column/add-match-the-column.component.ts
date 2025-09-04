import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { add } from 'date-fns';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemMatch } from 'src/app/_DtoModels/TestItemMatch/TestItemMatch';
import { TestItemMatchCreateOPtions } from 'src/app/_DtoModels/TestItemMatch/TestItemMatchCreateOptions';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';


@Component({
  selector: 'app-add-match-the-column',
  templateUrl: './add-match-the-column.component.html',
  styleUrls: ['./add-match-the-column.component.scss']
})
export class AddMatchTheColumnComponent implements OnInit {
  @Input() isCloseClick:boolean = false;
  @Output() closeByValue = new EventEmitter<boolean>();
  matchQuestions: MatchQuestions[] = [];
  totalQuestions = 0;
  totalChoices = 0;
  saveSpinner = false;
  saveAndAddSpinner = false;
  invalidCorrect = false;

  correctAnswers = ['A', 'B', 'C', 'D'];

  @Input() type!: TestItemType;
  @Input() levelId = "";
  @Input() eoId: any = null;
  @Input() showSaveButton:boolean = true;

  @Output() itemSaved: EventEmitter<any> = new EventEmitter();

  @Input() previousData!: any;
  @Input() mode: 'add' | 'edit' | 'copy' = 'add';
  matchData: TestItemMatch[] = [];
  close = true;

  matchForm = new UntypedFormGroup({});
  questionForm = new UntypedFormGroup({});
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
  ) { }

  ngOnInit(): void {
    this.readyQuestions();
    if (this.mode !== 'add') {
      this.readyData();
    }
  }

  async readyData() {
    this.matchData = await this.testItemService.getMatchTheColumn(this.previousData.id);
    this.questionForm.get('question')?.setValue(this.previousData.question);

    this.matchData.forEach((data, i) => {
      if (i < 4) {
        this.matchForm.patchValue({
          ['question' + i]: data.choiceDescription,
          ['choice' + i]: data.matchDescription,
          ['correct' + i]: data.correctValue,
        });
      }
      else {
        if (data.choiceDescription === '') {
          this.addChoice(data);
        }
        else if (data.matchDescription === '') {
          this.addQuestion();
        }
        else {
          this.addChoice(data);
          this.addQuestion();
        }
        this.matchForm.patchValue({
          ['question' + i]: data.choiceDescription,
          ['choice' + i]: data.matchDescription,
          ['correct' + i]: data.correctValue,
        });
      }
    })
  }

  readyQuestions() {
    this.questionForm.addControl('question', new UntypedFormControl("", Validators.required));
    for (var i = 0; i < 4; i++) {
      this.matchQuestions.push({
        id: i,
        question: "Here is a Question for You?",
        choice: "",
        correct: "",
        colCharacter: String.fromCharCode(65 + i),
        hasChoice: true,
        hasQuestion: true,
      });
      this.matchForm.addControl(`question${i}`, new UntypedFormControl("", Validators.required));
      this.matchForm.addControl(`choice${i}`, new UntypedFormControl("", Validators.required));
      this.matchForm.addControl(`correct${i}`, new UntypedFormControl("", Validators.required));
    }

    this.totalChoices = this.matchQuestions.filter((data) => {
      return data.hasChoice
    }).length;

    this.totalQuestions = this.matchQuestions.filter((data) => {
      return data.hasQuestion;
    }).length;
  }

  addQuestion() {
    if (this.matchQuestions[this.matchQuestions.length - 1].hasQuestion) {
      this.matchQuestions.push({
        id: this.matchQuestions.length - 1,
        question: "Here is a Question for You?",
        choice: "",
        correct: "",
        colCharacter: String.fromCharCode(65 + (this.matchQuestions.length)),
        hasChoice: false,
        hasQuestion: true,
      });
      this.matchForm.addControl(`question${this.matchQuestions.length - 1}`, new UntypedFormControl("", Validators.required));
      this.matchForm.addControl(`correct${this.matchQuestions.length - 1}`, new UntypedFormControl("", Validators.required));
    }
    else {
      var data = this.matchQuestions.find((data) => {
        return !data.hasQuestion;
      });
      if (data) {
        var index = this.matchQuestions.indexOf(data);
        this.matchQuestions[index].hasQuestion = true;
        this.matchForm.get(`question${index}`)?.addValidators(Validators.required);
      }
    }

    this.totalQuestions = this.matchQuestions.filter((data) => {
      return data.hasQuestion
    }).length;
  }

  removeQuestion() {
    this.matchQuestions[this.totalQuestions - 1].hasQuestion = false;
    this.matchForm.get(`question${this.totalQuestions - 1}`)?.setValue('');
    this.matchForm.get(`question${this.totalQuestions - 1}`)?.removeValidators(Validators.required);
    this.matchForm.get(`question${this.totalQuestions - 1}`)?.updateValueAndValidity();

    this.totalQuestions = this.matchQuestions.filter((data) => {
      return data.hasQuestion
    }).length;

  }

  addChoice(matchData = null) {
    if (this.matchQuestions[this.matchQuestions.length - 1].hasChoice) {
      this.matchQuestions.push({
        id: this.matchQuestions.length - 1,
        question: "",
        choice: "",
        correct: "",
        colCharacter: String.fromCharCode(65 + (this.matchQuestions.length)),
        hasChoice: true,
        hasQuestion: false,
      });
      this.matchForm.addControl(`question${this.matchQuestions.length - 1}`, new UntypedFormControl(""));
      this.matchForm.addControl(`choice${this.matchQuestions.length - 1}`, new UntypedFormControl("", Validators.required));
      this.matchForm.addControl(`correct${this.matchQuestions.length - 1}`, new UntypedFormControl(""))
    }
    else {
      var data = this.matchQuestions.find((data) => {
        return !data.hasChoice;
      });
      if (data) {
        var index = this.matchQuestions.indexOf(data);
        this.matchQuestions[index].hasChoice = true;
        this.matchForm.get(`question${index}`)?.addValidators(Validators.required);
      }
    }

    if(matchData)
    {
      this.correctAnswers.push(matchData.matchValue);
    }
    else
    {
      var c = this.correctAnswers[this.correctAnswers.length -1];
      var nextChar = c == 'z' ? 'a' : c == 'Z' ? 'A' : String.fromCharCode(c.charCodeAt(0) + 1);

      console.log(nextChar);
      this.correctAnswers.push(nextChar);
    }
    this.totalChoices = this.matchQuestions.filter((data) => {
      return data.hasChoice
    }).length;
  }

  removeChoice() {
    this.matchQuestions.pop();
    this.matchForm.removeControl(`question${this.totalChoices - 1}`);
    this.matchForm.removeControl(`correct${this.totalChoices - 1}`);
    this.matchForm.removeControl(`choice${this.totalChoices - 1}`);
    this.totalQuestions = this.matchQuestions.filter((data) => {
      return data.hasQuestion;
    }).length;

    this.totalChoices = this.matchQuestions.filter((data) => {
      return data.hasChoice
    }).length;
    this.correctAnswers.pop();
  }

  async saveTestItem(event:any,shouldClose: boolean) {
    shouldClose ? this.saveSpinner = true : this.saveAndAddSpinner = true;
    var options = new TestItemCreateOptions();
    options.description = this.questionForm.get('question')?.value;
    options.eOId = this.eoId;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    if (this.mode === 'copy' && this.previousData.question.trim().toLowerCase() === options.description.trim().toLowerCase()) {
      options.description = options.description + ' - Copy';
    }
  /*   var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate']; */
    await this.testItemService.create(options).then((res: TestItem) => {
      this.saveMatchColData(res.id, shouldClose);
    }).finally(() => {
      this.saveAndAddSpinner = false;
      this.saveSpinner = false;
    })
  }

  async saveMatchColData(id: any, shouldClose: boolean) {
    var options = new TestItemMatchCreateOPtions();
    options.testItemId = id;
    this.matchQuestions.forEach((data, i) => {
      shouldClose ? this.saveSpinner = true : this.saveAndAddSpinner = true;
      options.choiceDescription = this.matchForm.get(`question${i}`)?.value;
      options.correctValue = this.matchForm.get(`correct${i}`)?.value;
      options.matchValue = data.colCharacter ?? "";
      options.matchDescription = this.matchForm.get(`choice${i}`)?.value;
      options.number = i + 1;

      this.testItemService.createMatchTheColumn(options).then((_) => {
        if (this.matchQuestions.length - 1 === i) {

          if (this.mode === 'add') {
            this.alert.successToast("Match The Column Saved");
            if(this.AddAnother){
              this.alert.successToast("Match The Column Saved");
              shouldClose = false;
              this.questionForm.reset();
              this.AddAnother=false;
            }
          }

          else {
            this.alert.successToast(`Match The Column ${this.mode === 'copy' ? "Copied" : "Updated"}`);
          }
          this.dataBroadcastService.refreshTestItem.next({ close: shouldClose,id:id });
          this.itemSaved.emit(id);
          shouldClose ? (this.isCloseClick?this.closeByValue.emit(false):this.flyPanelService.close()): this.matchForm.reset();
        }
      }).finally(() => {
        if (this.matchQuestions.length - 1 === i) {
          this.saveSpinner = false;
          this.saveAndAddSpinner = false;
        }
      })
    })
  }

  async updateMatchColData(event: any) {
    this.saveSpinner = true;
    var options = new TestItemCreateOptions();
    options.description = this.questionForm.get('question')?.value;
    options.eOId = this.eoId;
    options.taxonomyId = this.levelId;
    options.testItemTypeId = this.type.id;
    var data = JSON.parse(event);
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    await this.testItemService.update(this.previousData.id, options).then(async (res: TestItem) => {
      await this.testItemService.deleteMatchColumnItems(res.id).then((_) => {
        this.saveMatchColData(res.id, true);
      })
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

export class MatchQuestions {
  id?: any;
  question?: string;
  choice?: string;
  correct?: string;
  colCharacter?: string;
  hasChoice!: boolean;
  hasQuestion !: boolean;
}
