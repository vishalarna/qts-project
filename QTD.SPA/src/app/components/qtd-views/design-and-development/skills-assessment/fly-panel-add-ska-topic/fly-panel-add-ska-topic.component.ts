import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-ska-topic',
  templateUrl: './fly-panel-add-ska-topic.component.html',
  styleUrls: ['./fly-panel-add-ska-topic.component.scss']
})
export class FlyPanelAddSkaTopicComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  showSpinner: boolean = false;
  topicForm: UntypedFormGroup;
  @Input() topic_Number: any = '';
  TopicNumber: number = 0;
  selectedCatId = "";
  selectedSubCatId = "";
  datePipe = new DatePipe('en-us');
  @Output() refreshTopic = new EventEmitter<any>();

  categories: any[] = [];

  subcategories: any[] = [];
  constructor(private fb: UntypedFormBuilder,
    // public eOService:EnablingObjectivesCategoryService,
    // public eoTopicService: EnablingObjectivesTopicService,
    public alert:SweetAlertService) {}

  ngOnInit(): void {
    this.readyTopicForm();
    this.readyTopicNumber();
  }

  readyTopicNumber() {
    
    // this.eoTopicService.getAllTopics().then((res: EnablingObjective_Topic[]) => {
    //   
    //   this.TopicNumber = res.length + 1;
    // })
    this.TopicNumber = 1
  }
  
  ngAfterViewInit(): void {
    this.populateCatData();
    // this.subscriptions.sink = this.route.params.subscribe((res:any)=>{
    //   this.procId = res.id;
    // })
  }

  async populateCatData(){
    // await this.eOService.getAll().then((res:EnablingObjective_Category[])=>{
    //   this.categories = res;
    // }).catch((err:any)=>{
    //   this.alert.errorToast("Error Fetching Enabling Objective Categories " + err);
    // })
  }

  async populateSubCatData(){

    // await this.eOService.getSubCategories(this.selectedCatId).then((res:EnablingObjective_SubCategory[])=>{
    //   this.subcategories = res;
    // }).catch((err:any)=>{
    //   this.alert.errorToast("Error Fetching Enabling Objective SubCategories " + err);
    // })
  }

  readyTopicForm() {
    this.topicForm = this.fb.group({
      number: new UntypedFormControl(this.topic_Number, [Validators.required]),
      title: new UntypedFormControl('', [Validators.required]),
      desc: new UntypedFormControl('', [Validators.required]),
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      reason: new UntypedFormControl(''),
      category: new UntypedFormControl('', [Validators.required]),
      subcategory: new UntypedFormControl('', [Validators.required]),
      AddAnother: new UntypedFormControl(false),
    });
  }

  async getCategories(id: any) {
    // this.selectedCatId = id;
    // 
    // this.eOService.getAll().then((res:EnablingObjective_Category[])=>{
    //   this.populateSubCatData()
    // }).catch((err:any)=>{
    //   this.alert.errorToast("Error Fetching Enabling Objective Categories Data " + err.message);
    // })
  }

  async getSubCategories(id: any) {
    // this.selectedSubCatId = id;
    // 
    // this.eOService.getSubCategories(this.selectedCatId).then((res:EnablingObjective_SubCategory[])=>{
    //   //this.cat_Number = res.length + 1;
    // }).catch((err:any)=>{
    //   this.alert.errorToast("Error Fetching Enabling Objective SubCategories Data " + err.message);
    // })
  }

  async saveTopic(){
    // var options = new EnablingObjective_TopicOptions();
    // options.title = this.topicForm.get("title")?.value;
    // options.description = this.topicForm.get("desc")?.value;
    // options.number = this.topicForm.get("number")?.value;
    // options.subCategoryId = this.topicForm.get('subcategory')?.value;
    // await this.eoTopicService.createTopic(this.selectedCatId, this.selectedSubCatId, options).then((res: EnablingObjective_TopicOptions) => {
    //   this.saveEOTopicHistory(res.id);
    // }).catch((err: any) => {
    //   this.alert.errorToast("Error Saving Topic Data");
    // });
  }

  async saveEOTopicHistory(id:any){
    
    // var histOptions = new EnablingObjective_TopicHistory();
    //   histOptions.changeEffectiveDate = this.topicForm.get("effectiveDate")?.value;
    //   histOptions.changeNotes = this.topicForm.get("reason")?.value;
    //   histOptions.newStatus = true;
    //   histOptions.oldStatus = false;
    //   histOptions.enablingObjectiveTopicId = id;
    //   await this.eoTopicService.saveEOTopicHistory(histOptions).then((res:EnablingObjective_TopicHistory)=>{
    //     
    //     if(this.topicForm.get('AddAnother')?.value){
    //       this.topicForm.reset();
    //       this.readyTopicNumber();
    //       this.topicForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
    //       this.topicForm.get('desc')?.setValue(null);
    //       this.topicForm.get('reason')?.setValue(null);
    //     }
    //     else{
    //       this.closed.emit('fp-add-sh-cat-closed');
    //     }
    //     this.refreshTopic.emit();
        
    //     this.alert.successToast("Enabling Objective Category and History Saved Successfully.");
        
    //   }).catch((err:any)=>{
    //     this.alert.errorToast("Error Saving Enabling Objective Category History ");
    //   })
  }

}

