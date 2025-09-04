import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { EnablingObjectiveHistory } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveHistory';
import { EnablingObjectiveHistoryCreateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveHistoryCreateOptions';
import { TaxonomyLevel } from 'src/app/_DtoModels/TaxonomyLevel/TaxonomyLevel';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { TestItem_HistoryCreateOptions } from 'src/app/_DtoModels/TestItem_History/TestItem_HistoryCreateOptions';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { TaxonomyLevelService } from 'src/app/_Services/QTD/taxonomy-level.service';
import { TestItemTypeService } from 'src/app/_Services/QTD/test-item-type.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { TestTypeService } from 'src/app/_Services/QTD/test-type.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SubSink } from 'subsink';
import { AddTrueFalseComponent } from '../../../design-and-development/tests/test-question-bank/add-true-false/add-true-false.component';
import { AddShortQuestionsComponent } from '../../../design-and-development/tests/test-question-bank/add-short-questions/add-short-questions.component';
import { AddMcqComponent } from '../../../design-and-development/tests/test-question-bank/add-mcq/add-mcq.component';
import { AddMultipleCorrectAnswerComponent } from '../../../design-and-development/tests/test-question-bank/add-multiple-correct-answer/add-multiple-correct-answer.component';
import { AddMatchTheColumnComponent } from '../../../design-and-development/tests/test-question-bank/add-match-the-column/add-match-the-column.component';
import { AddFillInTheBlankComponent } from '../../../design-and-development/tests/test-question-bank/add-fill-in-the-blank/add-fill-in-the-blank.component';

@Component({
  selector: 'app-flypanel-eo-test-question-link',
  templateUrl: './flypanel-eo-test-question-link.component.html',
  styleUrls: ['./flypanel-eo-test-question-link.component.scss']
})
export class FlypanelEoTestQuestionLinkComponent implements OnInit, AfterViewInit,OnDestroy {
  @Input() hasSpace = false;
  showSpinner = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  stepperOrientation: Observable<StepperOrientation>;

  toolTipString =
    `Select the appropriate level of complexity for the test question:\n

  \nRecall - Remember facts and concepts

  \nApplication - Use information and concepts in different situations

  \nAnalyze - Establish relationships between presented information`

  subscription = new SubSink();
  datePipe = new DatePipe('en-us');

  testTypes: TestItemType[] = [];
  taxonomyLevels: TaxonomyLevel[] = [];
  selectedType = "";
  isQuestionValid = false;
  questionForm: UntypedFormGroup = new UntypedFormGroup({});
  histForm: UntypedFormGroup = new UntypedFormGroup({});
  eoId = "";
  testQuestionNumber: Number=null;
  continue_enable:any;

  continueForm = new UntypedFormGroup({});

  @ViewChild('mcq') mcq!:AddMcqComponent;
  @ViewChild('mca') mca!:AddMultipleCorrectAnswerComponent;
  @ViewChild('trueFalse') trueFalse!:AddTrueFalseComponent;
  @ViewChild('blanks') blanks!:AddFillInTheBlankComponent;
  @ViewChild('matchCol') matchCol!:AddMatchTheColumnComponent;
  @ViewChild('short') short!:AddShortQuestionsComponent;

  constructor(
    public breakpointObserver: BreakpointObserver,
    private testTypeService: TestItemTypeService,
    private taxonomyService: TaxonomyLevelService,
    private testItemService: TestItemService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private alert : SweetAlertService
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.readyForm();
    this.readyData();
    this.readyTestItemData();
  }

  readyTestItemData(){
    this.testItemService.getTestItemNumber().then((res)=>{
      this.testQuestionNumber = res;

    });
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.eoId = String(res.id).split('-')[1];
    });

    this.subscription.sink = this.dataBroadcastService.questionSaved.subscribe((res) => {
      if (res.isSaved) {
        var options = new TestItem_HistoryCreateOptions();
        options.newStatus = true;
        options.oldStatus = false;
        options.changeNotes = this.histForm.get('reason')?.value;
        options.effectiveDate = this.histForm.get('date')?.value;
        options.testItemId = res.id;
        this.testItemService.createHistory(options).then((_) => {
          this.refresh.emit();
          this.closed.emit();
          this.dataBroadcastService.refreshStats.next(null);
        }).finally(() => {
          this.showSpinner = false;
        });
      }
      else{
        this.showSpinner = false;
      }
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  TrueFalseCheck(e:any){
    
    this.continue_enable = e.value;
  }

  readyForm() {
    this.questionForm.addControl('type', new UntypedFormControl('', Validators.required));
    this.questionForm.addControl('taxonomy', new UntypedFormControl('', Validators.required));

    this.histForm.addControl('reason', new UntypedFormControl(''));
    this.histForm.addControl('date', new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")));

    this.subscription.sink = this.questionForm.statusChanges.subscribe((status)=>{
      if(status === "VALID"){

      }
    })
  }

  async readyData() {
    this.testTypes = await this.testTypeService.getAll();
    this.taxonomyLevels = await this.taxonomyService.getAll();
    [...this.taxonomyLevels.splice(3)];
  }

  selectedChanged(event: any) {
    if (event.selectedIndex === 1) {
      switch (this.selectedType) {
        case 'Multiple Choice Questions':
          this.continueForm = this.mcq.mcqForm;
          break;
        case 'True / False':
          this.continueForm = this.trueFalse.tfForm;
          break;
        case 'Fill in the Blank':
          // this.blanks.saveFromEO(options);
          break;
        case 'Match the Column':
          // this.matchCol.saveFromEO(options);
          break;
        case 'Short Answers':
          // this.continue_enable = this.short.isSaveValid();
          break;
        case "Multiple Correct Answers":

          break;
      }
    }
  }

  selectQuesType(type: TestItemType) {

    this.selectedType = type.description;

  }

  async saveTestItem() {
  }

  isFormValid(e: boolean) {
    

    this.isQuestionValid = e;
  }

  setOptions(type: string) {
    this.showSpinner = true;
    var options = new TestItemCreateOptions();
    options.taxonomyId = this.questionForm.get('taxonomy')?.value;
    options.testItemTypeId = this.questionForm.get('type')?.value.id;
    options.eOId = this.eoId;
    const jsonOBJ = JSON.stringify({reason:this.histForm.get('reason')?.value,effectiveDate:this.histForm.get('date')?.value});

    switch (type) {
      case 'Multiple Choice Questions':
        this.mcq.saveMCQ(jsonOBJ,true);
        break;
      case 'True / False':
        this.trueFalse.saveTF(jsonOBJ,true);
        break;
      case 'Fill in the Blank':
        this.blanks.saveFIB(jsonOBJ,true);
        break;
      case 'Match the Column':
        this.matchCol.saveTestItem(jsonOBJ,true);
        break;
      case 'Short Answers':
        this.short.saveTestItem(jsonOBJ,true);
        break;
      case "Multiple Correct Answers":
        this.mca.saveMCQ(jsonOBJ,true);
        break;
    }
  }

  getToolTip(): string {
    return `Select the appropriate level of complexity for the test question:
    Recall - Remember facts and concepts
    Application - Use information and concepts in different situations
    Analyze - Establish relationships between presented information`;
  }

}
