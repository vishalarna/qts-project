import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormArray, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { TaxonomyLevel } from 'src/app/_DtoModels/TaxonomyLevel/TaxonomyLevel';
import { TestItem } from 'src/app/_DtoModels/TestItem/TestItem';
import { TestItemCreateOptions } from 'src/app/_DtoModels/TestItem/TestItemCreateOptions';
import { TestItemType } from 'src/app/_DtoModels/TestItemType/TestItemType';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { TaxonomyLevelService } from 'src/app/_Services/QTD/taxonomy-level.service';
import { TestItemTypeService } from 'src/app/_Services/QTD/test-item-type.service';
import { TestItemService } from 'src/app/_Services/QTD/test-item.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';


@Component({
  selector: 'app-fly-panel-new-test',
  templateUrl: './fly-panel-new-test.component.html',
  styleUrls: ['./fly-panel-new-test.component.scss'],
})
export class FlyPanelNewTestComponent implements OnInit, AfterViewInit, OnDestroy {
  @Output() array_create = new EventEmitter<any>();
  @Input() view_data: boolean;
  @Input() question_Id: any;
  @Input() update: boolean;
  @Input() editIlaCheck:any;

  viewToDisplay = "";

  masterArray: Push_Items[] = [];
  remove_array_length: boolean = false;

  question_type: any[] = [];
  taxonomy: any[] = [];
  obj_topics: Objectives_Topics[] = [];
  isDropdownVisible: boolean;
  QuestionTypeForm: UntypedFormGroup = new UntypedFormGroup({});
  TaxonomyLevelForm!: UntypedFormGroup;
  EOForm!: UntypedFormGroup;
  tax: any;
  defaultType: any = "";
  defaultTaxonomy: any = "";

  questionStatement = "";
  trueFalseOptions: any[] = [];
  mcqOptions: any[] = [];

  questionType = "";
  taxonomyLevel = "";

  @Output() sendQuestion: EventEmitter<any> = new EventEmitter();

  obj_length: number;
  obj_id: any;
  qType: any;
  ques_ans: any;
  linked_objective: any;
  subscriptions = new SubSink();
  ILAId: any;
  editable = false;



  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private fb: UntypedFormBuilder,
    private testItemTypeService: TestItemTypeService,
    private router: Router,
    private alert: SweetAlertService,
    private taxonomyLevelService: TaxonomyLevelService,
    private ILAService: IlaService,
    private saveStore: Store<{ saveIla: any }>,
    private dataBroadcastService: DataBroadcastService,
    private testItemService: TestItemService,
    private enablingObjectiveService: EnablingObjectivesService,
    private labelPipe: LabelReplacementPipe,
  ) {
    // this.QuestionTypeForm = fb.group({
    //   questionType: [{disabled=true},Validators.compose([
    //     Validators.required,
    //   ])]
    // });


    this.TaxonomyLevelForm = fb.group({
      taxonomyLevel: ['', Validators.compose([
        Validators.required,
      ])],
    });

    this.EOForm = fb.group({
      EO_id: ['', Validators.compose([
        Validators.required,
      ])],
    });
  }

  ngOnInit(): void {
    this.QuestionTypeForm.addControl('questionType', new UntypedFormControl(null, Validators.required))
    if (this.view_data) {
      //this.getLinkedObjective();
      this.getQuestion();
    }
    else if (this.update) {

      this.getQuestionById();
      this.editable = true;
    }
    else {
      this.readyQuestionTypes();
      this.readyTaxonomyLevels();
      this.readyStore(null);
    }
  }

  ngAfterViewInit(): void {
    this.readyMCQSubscription();
    this.readyMatchColSubscription();
    this.readyTrueFalseSubscription();
    this.readyShortAnswerSubscription();
    this.readyFillBlankSubscription();
    this.readyUpdateSubscription();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }


  readyUpdateSubscription() {
    this.subscriptions.sink = this.dataBroadcastService.updateQuestion.subscribe((res: any) => {
      var options = new TestItemCreateOptions();
      options.description = res.question;
      options.eOId = this.EOForm.get('EO_id')?.value;
      this.testItemService.updateDescription(res.id, options).then((res: any) => {
        this.dataBroadcastService.updateAnswerList.next(res);
      }).catch((err: any) => {
        this.alert.errorToast("Error Updating Question Data");
      })
    })
  }

  async getLinkedObjective(id: any) {
    await this.enablingObjectiveService.get(id).then((res: any) => {
      this.linked_objective = res.description;
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching Linked "  + await this.transformTitle('Enabling Objective'));
    })
  }

  async getQuestion() {
    await this.testItemService.get(this.question_Id).then((res: TestItem) => {
      this.getQuestionType(res.testItemTypeId);
      this.getTaxonomyLevel(res.taxonomyId);
      this.questionStatement = res.description.replace("<p>", '');
      this.questionStatement = this.questionStatement.replace("</p>", '');
      this.getLinkedObjective(res.eoId);
    }).catch((err: any) => {
      this.alert.errorToast("Error Fetching Question Data");
    })
  }

  async getQuestionType(typeId: any) {
    await this.testItemTypeService.get(typeId).then((res: any) => {
      this.questionType = res.description;
      this.getQuestionAnswers(this.questionType);
    }).catch((err: any) => {
      this.alert.errorToast("Failed to Fetch Question Type Data");
    })
  }

  async getQuestionById() {
    this.testItemService.get(this.question_Id).then((res: TestItem) => {
      this.readyStore(res.eoId);
      this.getQuestionTypeById(res.testItemTypeId);
      this.getTaxonomyLevelById(res.taxonomyId);
    }).catch((err: any) => {
      this.alert.errorToast("Error Fetching Question Data");
    })
  }

  async getTaxonomyLevelById(id: any) {
    this.taxonomyLevelService.get(id).then((res: TaxonomyLevel) => {
      this.defaultTaxonomy = res.description;
    }).catch((err: any) => {
      this.alert.errorToast("Error fetching Taxonomy Level");
    })
  }

  async getQuestionTypeById(id: any) {
    this.testItemTypeService.get(id).then((res: TestItemType) => {
      this.defaultType = res.description;
      this.qType = this.defaultType;
    }).catch((err: any) => {
      this.alert.errorToast("Error Fetching Type Data");
    })
  }

  getQuestionAnswers(type: any) {
    switch (type) {
      case "True / False":
        this.viewToDisplay = "TF";
        this.getTrueFalseData();
        break;
      case "Multiple Choice Questions":
        this.viewToDisplay = "MCQ";
        this.getMCQData();
        break;
    }
  }

  async getTrueFalseData() {
    await this.testItemService.getTrueFalse(this.question_Id).then((res: any) => {
      this.trueFalseOptions = res;

    }).catch((err: any) => {
      this.alert.errorToast("Failed to fetch True False Data");
    });
  }

  async getMCQData() {
    await this.testItemService.getMCQ(this.question_Id).then((res: any) => {
      this.mcqOptions = res;

    }).catch((err: any) => {
      this.alert.errorToast("Failed to Fetch MCQ Data");
    })
  }

  async getTaxonomyLevel(levelId: any) {
    await this.taxonomyLevelService.get(levelId).then((res: any) => {
      this.taxonomyLevel = res.description;
    }).catch((err: any) => {
      this.alert.errorToast("Failed To Fetch Taxonomy Level Data");
    })
  }

  readyShortAnswerSubscription() {
    this.subscriptions.sink = this.dataBroadcastService.saveShortAnswer.subscribe((res: any) => {
      this.saveTestItem(res, "SQ");
    })
  }

  readyTrueFalseSubscription() {
    this.subscriptions.sink = this.dataBroadcastService.saveTrueFalse.subscribe((res: any) => {
      this.saveTestItem(res, "TF");
    })
  }

  readyMatchColSubscription() {
    this.subscriptions.sink = this.dataBroadcastService.saveMatchColumn.subscribe((res: any) => {
      this.saveTestItem(res, "MATCH");
    })
  }

  readyMCQSubscription() {

    this.subscriptions.sink = this.dataBroadcastService.saveTestItem.subscribe((res: any) => {
      this.saveTestItem(res, 'MCQ');
    })
  }

  readyFillBlankSubscription() {
    this.subscriptions.sink = this.dataBroadcastService.saveFillBlank.subscribe((res: any) => {
      this.saveTestItem(res, 'FB');
    })
  }


  async saveTestItem(ques: any, type: string) {

    if (this.QuestionTypeForm.valid && this.TaxonomyLevelForm.valid && this.EOForm.valid) {

      var options = new TestItemCreateOptions();
      options.testItemTypeId = (this.QuestionTypeForm.get('questionType')?.value).id;
      options.taxonomyId = (this.TaxonomyLevelForm.get('taxonomyLevel')?.value).id;
      options.description = ques.question;
      options.isActive = true;
      options.eOId = this.EOForm.get('EO_id')?.value;
      this.testItemService.create(options).then((res: any) => {
        switch (type) {
          case "MCQ":

            this.dataBroadcastService.testItemSaved.next(res);
            break;
          case "MATCH":
            this.dataBroadcastService.matchColumnSaved.next(res);
            break;
          case "TF":
            this.dataBroadcastService.trueFalseSaved.next(res);
            break;
          case "SQ":
            this.dataBroadcastService.shortAnswerSaved.next(res);
            break;
          case "FB":
            this.dataBroadcastService.fillBlankSaved.next(res);
        }
        if (ques.add) {
          var data = {
            id: res.id,
            description: res.description,
            taxType: (this.QuestionTypeForm.get('questionType')?.value).description,
            quesType: (this.TaxonomyLevelForm.get('taxonomyLevel')?.value).description,
          }
          this.sendQuestion.emit(data);
        }
      }).catch((err: any) => {
        this.emitFailure(type);
        this.alert.errorToast("Error Creating Test Item Question Already Exists");
      })
    }
    else {
      this.emitFailure(type);
      this.alert.errorToast("Taxonomy level and " + await this.transformTitle('Enabling Objective'));
    }
  }

  emitFailure(type: any) {

    switch (type) {
      case "MCQ":
        this.dataBroadcastService.testItemSaved.next(null);
        break;
      case "MATCH":
        this.dataBroadcastService.matchColumnSaved.next(null);
        break;
      case "TF":
        this.dataBroadcastService.trueFalseSaved.next(null);
        break;
      case "SQ":
        this.dataBroadcastService.shortAnswerSaved.next(null);
        break;
      case "FB":
        this.dataBroadcastService.fillBlankSaved.next(null);

    }
  }

  readyStore(id: any) {
    this.subscriptions.sink = this.saveStore.pipe(select('saveIla')).subscribe((res: any) => {

      if ((res?.saveData?.result !== undefined)) {
        this.ILAId = res['saveData']?.result?.id;
        this.readyEO(id);
      }
    })


  }

  async readyEO(id: any) {
    await this.ILAService.getLinkedEnablingObjectives(this.ILAId).then((res: any) => {
      if (id !== null) {
        this.EOForm.get('EO_id')?.setValue(id);
      }
      this.obj_topics = res;
      this.obj_length = this.obj_topics.length;
      this.isDropdownVisible = true;

    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching " + await this.transformTitle('Enabling Objective') + "s");
    })
  }

  async readyQuestionTypes() {
    await this.testItemTypeService.getAll().then((res: any) => {

      this.question_type = res;
    }).catch((err: any) => {
      this.alert.errorToast("Error Fetching Question Types");
    })
  }

  async readyTaxonomyLevels() {
    await this.taxonomyLevelService.getAll().then((res: any) => {

      this.taxonomy = res;
    }).catch((err: any) => {
      this.alert.errorToast("Error Fetching Taxonomy Levels");
    })
  }

  ShowDropDown() {
    this.isDropdownVisible = !this.isDropdownVisible;
  }

  checkEnablingObjectives(e: any) {

    this.obj_id = e;
    this.remove_array_length = true;
  }

  OnSelectionChangeQuestion(d: any, e: any) {
    if (e.isUserInput) {
      this.qType = d;
    }
  }

  recieveAnswer(d: any) {

    this.ques_ans = d;
    let tax_1 = this.TaxonomyLevelForm.get('taxonomyLevel')?.value;
    let type_1 = this.QuestionTypeForm.get('questionType')?.value;
    this.masterArray.push({
      id: this.masterArray.length + 1,
      EO_id: this.obj_id,
      tax: tax_1,
      type: type_1,
      ans: this.ques_ans
    })

    this.array_create.emit(this.masterArray);
  }

  recievePanel(d: any) {
    if (d) {
      this.flyPanelSrvc.close();
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}

export class Objectives_Topics {
  id: any;
  description: string;
  type: string;
  checked?: boolean;
}

export class Push_Items {
  id: any;
  EO_id: any;
  tax: any;
  type: any;
  ans: any;
}









