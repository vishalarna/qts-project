import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { EnablingObjective_Topic } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { EnablingObjectivesTopicService } from 'src/app/_Services/QTD/enabling-objectives-topic.service';
import { DatePipe } from '@angular/common';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EnablingObjectiveCreateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveCreateOptions';
import { EnablingObjectiveHistory } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveHistory';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { MatStepper } from '@angular/material/stepper';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-flypanel-add-eo',
  templateUrl: './flypanel-add-eo.component.html',
  styleUrls: ['./flypanel-add-eo.component.scss']
})
export class FlypanelAddEoComponent implements OnInit {
  @Input() hasSpace = false;
  @Input() isMetaEoCheck: boolean;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Output() refresEO = new EventEmitter<any>();
  @Output() refreshOverview = new EventEmitter<any>();
  @Input() eo: EnablingObjective;
  @Input() makeCopy = false;
  @Input() changeCatSubTopic = false;
  @Input() shouldNavigate = false;
  @Input() sqPosition:boolean;

  showSpinner: boolean = false;
  stepperOrientation: Observable<StepperOrientation>;
  step1Form: UntypedFormGroup;
  step2Form: UntypedFormGroup;
  step3Form: UntypedFormGroup;

  addCategory: boolean = false;
  addSubCategory: boolean = false;
  addTopic: boolean = false;
  addEO: boolean = true;
  selectedCatId = "";
  selectedSubCatId = "";
  selectedTopicId = "";
  EONumber = "";
  datePipe = new DatePipe('en-us');

  categories: any[] = [];
  subcategories: any[] = [];

  topics: any[] = [];
  changeNumber = "";
  informationLabel='EO Information';
  numberLabel='EO Number';
  statmentLabel='EO Statement';
  categoryLoader = false;
  subCategoryLoader = false;
  topicLoader = false;

  @ViewChild('stepper') stepper !: MatStepper;


  constructor(private fb: UntypedFormBuilder,
    private positionService: PositionsService,
    public breakpointObserver: BreakpointObserver,
    public eOCatService: EnablingObjectivesCategoryService,
    public eoService: EnablingObjectivesService,
    public eoTopicService: EnablingObjectivesTopicService,
    public alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,
  ) {
    this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
  }

  ngOnInit(): void {
    this.readyStep1Form();
    this.readyStep2Form();
    this.readyStep3Form();
    if(this.sqPosition){
      this.informationLabel = 'SQ Information';
      this.numberLabel = 'SQ Number';
      this.statmentLabel = 'SQ Statement';
    }
    //this.getallDutyAreas();
  }

  ngAfterViewInit(): void {
    this.populateCatData();
    (this.eo !== undefined || this.makeCopy) ? this.readyPreviousData() : this.readyEONumber();
    // this.subscriptions.sink = this.route.params.subscribe((res:any)=>{
    //   this.procId = res.id;
    // })
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only
  
    if (pattern.test(control.value)) {
      return null; 
    } else {
      return { whitespaceOnly: true }; 
    }
  }

  readyPreviousData() {
    //// Step 1 form data
    this.step1Form.patchValue({
      categoryId: this.eo.categoryId,
      subcategoryId: this.eo.subCategoryId,
      topicId: this.eo.topicId,
    });

    /// Step 2 Form
    var num = [];
    var changednum = "";
    if (this.eo !== undefined && !this.makeCopy) {
      if (this.eo.fullNumber.includes('.')) {
        num = this.eo.fullNumber.split('.');
        changednum = this.eo.number;
      }
      else {
        changednum = this.eo.number;
      }
    }
    this.step2Form.patchValue({
      EONumber: num.length > 0 ? `${num[0]}.${num[1]}.${num[2]}`:``,
      ChangedNumber: changednum,
      EOStatement: this.eo.description,
      isMetaEO: this.eo.isMetaEO,
      isSkill: this.eo.isSkillQualification,
    });

    this.step3Form.patchValue({
      effectiveDate: this.datePipe.transform(this.eo.effectiveDate, 'MM-dd-yyyy')
    })
    this.selectedCatId = this.eo.categoryId;
    this.selectedSubCatId = this.eo.subCategoryId;
    this.selectedTopicId = this.eo.topicId;
    this.populateCatData();
    this.populateSubCatData();
    this.populateTopicData();
    if (this.selectedTopicId == null && this.makeCopy) {
      this.getEONumber();
    }
    else if (this.makeCopy) {
      this.getEONumberWithTopic();
    }
  }

  readyEONumber() {
    // this.eoService.getAllEOs().then((res: EnablingObjective[]) => {
    //   this.EONumber = String(res.length + 1);
    // })
  }

  async populateCatData() {
    await this.eOCatService.getAllSimplifiedCategories().then((res: any[]) => {
      this.categories = res;
      this.refresh.emit();
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching " + await this.transformTitle('Enabling Objective') + " Categories " + err);
    })
  }

  async populateSubCatData() {
    if (this.selectedCatId != "" && this.selectedCatId != null) {
      await this.eOCatService.getAllSimplifiedSubCategories(this.selectedCatId).then((res: EnablingObjective_SubCategory[]) => {
        this.subcategories = res;
      }).catch(async (err: any) => {
        this.alert.errorToast("Error Fetching " + await this.transformTitle('Enabling Objective') + "SubCategories " + err);
      })
    }
  }

  async populateTopicData() {
    if (this.selectedCatId != "" && this.selectedCatId != null && this.selectedSubCatId != null && this.selectedSubCatId != "") {
      this.topicLoader = true;
      await this.eoTopicService.getAllSimplifiedTopics(this.selectedCatId, this.selectedSubCatId).then((res: EnablingObjective_Topic[]) => {
        this.topics = res;
        this.refresh.emit();
      }).finally(()=>{
        this.topicLoader = false;
      })
    }
  }

  selectedChanged(event: any) {
    if (event.selectedIndex === 1) {

    }
  }

  readyStep1Form() {
    this.step1Form = this.fb.group({
      categoryId: new UntypedFormControl({ value: '', disabled: !this.changeCatSubTopic && this.eo !== undefined }, [Validators.required]),
      subcategoryId: new UntypedFormControl({ value: '', disabled: !this.changeCatSubTopic && this.eo !== undefined }, [Validators.required]),
      topicId: new UntypedFormControl({ value: '', disabled: !this.changeCatSubTopic && this.eo !== undefined }),
    });
  }

  readyStep2Form() {
    this.step2Form = this.fb.group({
      EONumber: new UntypedFormControl(''),
      EOStatement: new UntypedFormControl({ value: '', disabled: this.changeCatSubTopic }, [Validators.required,this.whitespaceOnlyValidator]),
      isMetaEO: new UntypedFormControl({ value: false, disabled: false }),
      isSkill: new UntypedFormControl({ value: false, disabled: false }),
      ChangedNumber: new UntypedFormControl('', Validators.required),
    });
  }

  readyStep3Form() {
    this.step3Form = this.fb.group({
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      reason: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      addNew: new UntypedFormControl({ value: false, disabled: this.eo !== undefined }),
    });
  }

  async getCategories(id: any) {
    this.categoryLoader = true;
    this.selectedCatId = id;
    this.eOCatService.getAllSimplifiedCategories().then((res: any[]) => {
      this.populateSubCatData()
    }).finally(()=>{
      this.categoryLoader = false;
    })
  }

  refreshCatSelection(){
    this.step1Form.get('subcategoryId')?.setValue('');
    this.step1Form.get('topicId')?.setValue('');
  }

  refreshSubCatSelection(){
    this.step1Form.get('topicId')?.setValue('');
  }

  async getSubCategories(id: any) {
    this.subCategoryLoader = true;
    this.selectedSubCatId = id;
    this.eOCatService.getAllSimplifiedSubCategories(this.selectedCatId).then((res: EnablingObjective_SubCategory[]) => {
      //this.cat_Number = res.length + 1;
      this.getEONumber();
      this.populateTopicData();
    }).finally(()=>{
      this.subCategoryLoader = false;
    })
  }

  async getTopics(id: any) {
    this.selectedTopicId = id;
    if (this.selectedTopicId !== '') {
      this.getEONumberWithTopic();
    }
    else {
      this.getEONumber();
    }
  }

  openCategoryPanel() {
    this.addCategory = true;
    this.addSubCategory = false;
    this.addTopic = false;
    this.addEO = false;
  }

  openSubCategoryPanel() {
    this.addCategory = false;
    this.addSubCategory = true;
    this.addTopic = false;
    this.addEO = false;
  }

  openTopicPanel() {
    this.addCategory = false;
    this.addSubCategory = false;
    this.addTopic = true;
    this.addEO = false;
  }

  closePanel() {
    this.addCategory = false;
    this.addSubCategory = false;
    this.addTopic = false;
    this.addEO = true;
    this.refresh.emit();
    this.populateCatData();
    this.populateSubCatData();
    this.populateTopicData();
  }

  async saveEO() {
    this.showSpinner = true;
    var options = new EnablingObjectiveCreateOptions();
    options.categoryId = this.step1Form.get("categoryId")?.value;
    options.subCategoryId = this.step1Form.get("subcategoryId")?.value;
    if (this.step1Form.get("topicId")?.value === '' || this.step1Form.get("topicId")?.value === null) {
    }
    else {
      options.topicId = this.step1Form.get("topicId")?.value;
    }
    options.number =  this.changeNumber;
    options.statement = this.step2Form.get("EOStatement")?.value;
    options.isMetaEO = this.step2Form.get("isMetaEO")?.value ?? false;
    options.isSkill = this.step2Form.get('isSkill')?.value ?? false;
    options.effectiveDate = this.step3Form.get('effectiveDate')?.value;
    await this.eoService.create(options).then(async (res: EnablingObjective) => {
      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Saved successfully");
      //this.dataBroadcastService.updateMyDataNavBar.next();
      this.dataBroadcastService.refreshStats.next(null);
      if (this.shouldNavigate) {
        this.dataBroadcastService.navigateOnChange.next({ type: "EO", data: res });
      }
      this.saveEOHistory(res.id);
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  async saveEOHistory(id: any) {
    var histOptions = new EnablingObjectiveHistory();
    histOptions.changeEffectiveDate = this.step3Form.get("effectiveDate")?.value;
    histOptions.changeNotes = this.step3Form.get("reason")?.value;
    histOptions.newStatus = true;
    histOptions.oldStatus = false;
    histOptions.enablingObjectiveId = id;
    await this.eoService.saveEOHistory(histOptions).then(async (res: EnablingObjectiveHistory) => {
      if (this.step3Form.get('addNew')?.value) {
        //this.step1Form.reset();
        // this.step2Form.reset();
        this.step2Form.patchValue({
          isMetaEO: false,
          isSkill: false,
          EOStatement: '',
        })
        this.step1Form.patchValue({
          categoryId: '',
          subcategoryId: '',
          topicId: '',
        }, { onlySelf: true });
        this.step3Form.reset();
        this.stepper.reset();
        this.step3Form.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else {
        this.closed.emit('fp-add-sh-cat-closed');
      }
      this.refresh.emit();
      this.refreshOverview.emit();
      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Category and History Saved Successfully.");
      this.dataBroadcastService.refreshStats.next(null);
    }).finally(() => {
      this.showSpinner = false;
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  async getEONumberWithTopic() {
    if (this.selectedCatId != "" && this.selectedCatId != null && this.selectedSubCatId != null && this.selectedSubCatId != "") {
      await this.eoService.
        getEONumberWithTopic(this.selectedCatId, this.selectedSubCatId, this.selectedTopicId).
        then((res: any) => {

        var num = String(res).split('.');
        this.EONumber = num[0] + '.' + num[1] + '.' + num[2];
        this.changeNumber = num[3];
          //this.topics = res;
          //this.refresh.emit();
        }).catch(async (err: any) => {
          this.alert.errorToast("Error Fetching " + await this.transformTitle('Enabling Objective'));
        })
    }
  }

  async getEONumber() {
    if (this.selectedCatId != "" && this.selectedCatId != null && this.selectedSubCatId != null && this.selectedSubCatId != "") {
      await this.eoService.
        getEONumber(this.selectedCatId, this.selectedSubCatId).
        then((res: any) => {



        var num = String(res).split('.');
        this.EONumber = num[0] + '.' + num[1] + '.' + num[2];
        this.changeNumber = num[3];
          //this.topics = res;
          //this.refresh.emit();
        });
    }
  }

  async copyEO() {
    this.showSpinner = true;
    var options = new EnablingObjectiveCreateOptions();
    options.categoryId = this.step1Form.get("categoryId")?.value;
    options.subCategoryId = this.step1Form.get("subcategoryId")?.value;
    if (this.step1Form.get("topicId")?.value == "") {
    }
    else {
      options.topicId = this.step1Form.get("topicId")?.value;
    }

    options.number =this.changeNumber;
    options.statement = this.step2Form.get("EOStatement")?.value;
    if (String(options.statement).trim().toLowerCase() === this.eo.description.trim().toLowerCase()) {
      options.statement = String(options.statement).concat("-Copy");
    }
    options.isMetaEO = this.step2Form.get("isMetaEO")?.value;
    options.isSkill = this.step2Form.get('isSkill')?.value;
    options.effectiveDate = this.step3Form.get('effectiveDate')?.value;
    await this.eoService.copy(this.eo.id, options).then(async (res: any) => {
      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Copied successfully");
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.dataBroadcastService.navigateOnChange.next({ type: "EO", data: res });
      this.saveEOHistory(res.id);
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  async updateEO() {
    this.showSpinner = true;
    var options = new EnablingObjectiveCreateOptions();
    options.categoryId = this.step1Form.get("categoryId")?.value;
    options.subCategoryId = this.step1Form.get("subcategoryId")?.value;

    if (this.step1Form.get("topicId")?.value === null) {
      options.topicId = null;
    }
    else {
      options.topicId = this.step1Form.get("topicId")?.value;
    }
    options.number = this.changeNumber;
    options.statement = this.step2Form.get("EOStatement")?.value;
    options.isMetaEO = this.step2Form.get("isMetaEO")?.value;
    options.isSkill = this.step2Form.get('isSkill')?.value;
    options.hasChanges = this.changeCatSubTopic;
    options.effectiveDate = this.step3Form.get('effectiveDate')?.value;
    await this.eoService.update(this.eo.id, options).then(async (res: EnablingObjective) => {
      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Updated successfully");
      if(this.eo.categoryId !== options.categoryId || this.eo.subCategoryId !== options.subCategoryId || this.eo.topicId !== options.topicId){
        this.dataBroadcastService.navigateOnChange.next({ type: "EO", data: res });
      }
      // this.changeCatSubTopic ? (this.shouldNavigate ? this.dataBroadcastService.navigateOnChange.next({ type: "EO", data: res }):'') : this.dataBroadcastService.updateMyDataNavBar.next();;
      this.refresEO.emit();
      this.saveEOHistory(res.id);
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  checkboxTesting(event: any) {

  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

}
