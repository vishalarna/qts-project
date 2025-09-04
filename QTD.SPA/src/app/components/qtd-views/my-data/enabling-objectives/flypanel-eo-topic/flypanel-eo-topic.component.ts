import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjective_Topic } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic';
import { EnablingObjective_TopicHistory } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_TopicHistory';
import { EnablingObjective_TopicOptions } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_TopicOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { EnablingObjectivesTopicService } from 'src/app/_Services/QTD/enabling-objectives-topic.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-eo-topic',
  templateUrl: './flypanel-eo-topic.component.html',
  styleUrls: ['./flypanel-eo-topic.component.scss']
})
export class FlypanelEOTopicComponent implements OnInit {
  @Input() hasSpace = false;
  @Input() shouldNavigate = false;
  @Output() closed = new EventEmitter<any>();
  showSpinner: boolean = false;
  topicForm: UntypedFormGroup;
  @Input() topic_Number: any = '';
  @Input() topic: EnablingObjective_Topic;
  @Output() refreshTopicData = new EventEmitter<EnablingObjective_Topic>();

  TopicNumber: number = 0;
  selectedCatId = "";
  selectedSubCatId = "";
  datePipe = new DatePipe('en-us');
  @Output() refreshTopic = new EventEmitter<any>();

  categories: EnablingObjective_Category[] = [];
  categoryLoader = false;
  subCategoryLoader = false;

  subcategories: any[] = [];
  constructor(private fb: UntypedFormBuilder,
    public eOService: EnablingObjectivesCategoryService,
    public eoTopicService: EnablingObjectivesTopicService,
    public alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,) { }

  ngOnInit(): void {
    this.readyTopicForm();
    //this.readyTopicNumber();
  }

  readyTopicNumber() {
    this.eoTopicService.getTopicNumber(this.selectedCatId, this.selectedSubCatId).then((res: number) => {
      this.topicForm.patchValue({
        number: res,
      })
    })
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only
  
    if (pattern.test(control.value)) {
      return null; 
    } else {
      return { whitespaceOnly: true }; 
    }
  }

  ngAfterViewInit(): void {
    this.populateCatData();
    // this.subscriptions.sink = this.route.params.subscribe((res:any)=>{
    //   this.procId = res.id;
    // })
  }

  async populateCatData() {
    this.categoryLoader =true;
    await this.eOService.getAllSimplifiedCategories().then((res: EnablingObjective_Category[]) => {
      this.categories = res;

      this.topic ? this.readyPreviousData() : null;
    }).finally(()=>{
      this.categoryLoader = false;
    })
  }

  async readyPreviousData() {

    var catId = await this.eoTopicService.getCategoryIdForTopic(this.topic.subCategoryId);
    this.selectedCatId = catId;
    this.selectedSubCatId = this.topic.subCategoryId;

    await this.getSubCategories(this.selectedSubCatId);
    this.topicForm.patchValue({
      number: this.topic.number,
      title: this.topic.title,
      desc: this.topic.description,
      category: this.selectedCatId,
      subcategory: this.topic.subCategoryId,
      effectiveDate: this.datePipe.transform(this.topic.effectiveDate, "yyyy-MM-dd")
    });
    this.topicForm.updateValueAndValidity();
  }

  async populateSubCatData() {
    this.subCategoryLoader = true;
    await this.eOService.getAllSimplifiedSubCategories(this.selectedCatId).then((res: EnablingObjective_SubCategory[]) => {
      this.subcategories = res;
    }).finally(()=>{
      this.subCategoryLoader = false;
    })
  }

  readyTopicForm() {
    this.topicForm = this.fb.group({
      /*       number: new FormControl({value : this.topic_Number, disabled : this.topic}, [Validators.required]), */
      number: new UntypedFormControl(this.topic_Number, [Validators.required]),
      title: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      desc: new UntypedFormControl(''),
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      reason: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      category: new UntypedFormControl('', [Validators.required]),
      subcategory: new UntypedFormControl('', [Validators.required]),
      AddAnother: new UntypedFormControl(false),
    });
  }

  async getCategories(id: any) {
    this.selectedCatId = id;
    this.eOService.getAllSimplifiedCategories().then((res: EnablingObjective_Category[]) => {
      this.populateSubCatData()
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching " + await this.transformTitle('Enabling Objective') + " Categories Data " + err.message);
    })
  }

  async getSubCategories(id: any) {
    this.selectedSubCatId = id;
    this.eOService.getAllSimplifiedSubCategories(this.selectedCatId).then((res: EnablingObjective_SubCategory[]) => {
      this.topic ? null : this.readyTopicNumber();
      this.subcategories = res;

      //this.cat_Number = res.length + 1;
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching " + await this.transformTitle('Enabling Objective') + " SubCategories Data " + err.message);
    })
  }

  async saveTopic() {
    this.showSpinner = true;
    var options = new EnablingObjective_TopicOptions();
    options.title = this.topicForm.get("title")?.value;
    options.description = this.topicForm.get("desc")?.value;
    options.number = this.topicForm.get("number")?.value;
    options.subCategoryId = this.topicForm.get('subcategory')?.value;
    options.effectiveDate = this.topicForm.get('effectiveDate')?.value;
    options.reason = this.topicForm.get("reason")?.value;
    await this.eoTopicService.createTopic(this.selectedCatId, this.selectedSubCatId, options).then(async (res: EnablingObjective_Topic) => {
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.dataBroadcastService.refreshStats.next(null);
      if (this.shouldNavigate) {
        this.dataBroadcastService.navigateOnChange.next({ type: "TOPIC", data: res });
      }
      if (this.topicForm.get('AddAnother')?.value === true) {
        this.topicForm.reset();
        this.topic_Number = 0;
        this.readyTopicNumber();
        this.topicForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.topicForm.get('desc')?.reset();
        this.topicForm.get('reason')?.reset();
      }
      else {
        this.closed.emit('fp-add-sh-cat-closed');
      }
      this.refreshTopic.emit();

      this.topic ? this.alert.successToast(await this.transformTitle('Enabling Objective') + " Category and History Saved Successfully.") :
        this.alert.successToast(await this.transformTitle('Enabling Objective') + " Category Updated and History Saved.");
    }).finally(() => {
      this.showSpinner = false;
    });
  }

  async updateTopic() {
    this.showSpinner = true;
    var options = new EnablingObjective_TopicOptions();
    options.title = this.topicForm.get("title")?.value;
    options.description = this.topicForm.get("desc")?.value;
    options.number = this.topicForm.get("number")?.value;
    options.subCategoryId = this.topicForm.get('subcategory')?.value;
    options.effectiveDate = this.topicForm.get('effectiveDate')?.value;
    options.reason = this.topicForm.get("reason")?.value;
    await this.eoTopicService.updateTopic(this.topic.id, options).then(async (res: EnablingObjective_Topic) => {
      this.showSpinner = false;
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.dataBroadcastService.refreshStats.next(null);
      this.refreshTopicData.emit(res);
      if (this.topicForm.get('AddAnother')?.value === true) {
        this.topicForm.reset();
        this.topic_Number = 0;
        this.readyTopicNumber();
        this.topicForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.topicForm.get('desc')?.reset();
        this.topicForm.get('reason')?.reset();
      }
      else {
        this.closed.emit('fp-add-sh-cat-closed');
      }
      this.refreshTopic.emit();

      this.topic ? this.alert.successToast(await this.transformTitle('Enabling Objective') + " Category and History Saved Successfully.") :
        this.alert.successToast(await this.transformTitle('Enabling Objective') + " Category Updated and History Saved.");
      // this.saveEOTopicHistory(res.id);
    }).finally(()=>{
      this.showSpinner = false;
    })
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  // async saveEOTopicHistory(id: any) {
  //   this.showSpinner = true;
  //   var histOptions = new EnablingObjective_TopicHistory();
  //   histOptions.changeEffectiveDate = this.topicForm.get("effectiveDate")?.value;
  //   histOptions.changeNotes = this.topicForm.get("reason")?.value;
  //   histOptions.newStatus = true;
  //   histOptions.oldStatus = false;
  //   histOptions.enablingObjectiveTopicId = id;
  //   await this.eoTopicService.saveEOTopicHistory(histOptions).then((res: EnablingObjective_TopicHistory) => {

  //     if (this.topicForm.get('AddAnother')?.value === true) {
  //       this.topicForm.reset();
  //       this.topic_Number = 0;
  //       this.readyTopicNumber();
  //       this.topicForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
  //       this.topicForm.get('desc')?.reset();
  //       this.topicForm.get('reason')?.reset();
  //     }
  //     else {
  //       this.closed.emit('fp-add-sh-cat-closed');
  //     }
  //     this.refreshTopic.emit();

  //     this.topic ? this.alert.successToast("Enabling Objective Category and History Saved Successfully.") :
  //       this.alert.successToast("Enabling Objective Category Updated and History Saved.");

  //   }).finally(() => {
  //     this.showSpinner = false;
  //   });
  // }

}
