import { BreakpointObserver } from '@angular/cdk/layout';
import { StepperOrientation } from '@angular/cdk/stepper';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
    
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-ska',
  templateUrl: './fly-panel-add-ska.component.html',
  styleUrls: ['./fly-panel-add-ska.component.scss']
})
export class FlyPanelAddSkaComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
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
  EONumber: number = 0;
  datePipe = new DatePipe('en-us');

  categories: any[] = [];
  subcategories: any[] = [];

  topics: any[] = [];



  constructor(private fb: UntypedFormBuilder,
    // private positionService: PositionsService,
     public breakpointObserver: BreakpointObserver,
    // public eOCatService:EnablingObjectivesCategoryService,
    // public eoService: EnablingObjectivesService,
    // public eoTopicService: EnablingObjectivesTopicService,
    public alert:SweetAlertService) {
      this.stepperOrientation = breakpointObserver
      .observe('(min-width: 800px)')
      .pipe(map(({ matches }) => (matches ? 'horizontal' : 'vertical')));
    }

  ngOnInit(): void {
    this.readyStep1Form();
    this.readyStep2Form();
    this.readyStep3Form();
    //this.getallDutyAreas();
  }

  ngAfterViewInit(): void {
    this.populateCatData();
    this.readyEONumber();
    // this.subscriptions.sink = this.route.params.subscribe((res:any)=>{
    //   this.procId = res.id;
    // })
  }

  readyEONumber() {
    // this.eoService.getAllEOs().then((res: EnablingObjective[]) => {
    //   
    //   this.EONumber = res.length + 1;
    // })

    this.EONumber = 1;
  }

  async populateCatData(){
    // await this.eOCatService.getAll().then((res:EnablingObjective_Category[])=>{
    //   this.categories = res;
    //   this.refresh.emit();
    // }).catch((err:any)=>{
    //   this.alert.errorToast("Error Fetching Enabling Objective Categories " + err);
    // })
  }

  async populateSubCatData(){
    // 
    // if(this.selectedCatId != "" && this.selectedCatId != null)
    // {
    //   await this.eOCatService.getSubCategories(this.selectedCatId).then((res:EnablingObjective_SubCategory[])=>{
    //     this.subcategories = res;
    //   }).catch((err:any)=>{
    //     this.alert.errorToast("Error Fetching Enabling Objective SubCategories " + err);
    //   })
    // }
  }

  async populateTopicData(){
    // if(this.selectedCatId != "" && this.selectedCatId != null && this.selectedSubCatId != null && this.selectedSubCatId != ""){
    //   await this.eoTopicService.getTopics(this.selectedCatId, this.selectedSubCatId).then((res:EnablingObjective_Topic[])=>{
    //     this.topics = res;
    //     this.refresh.emit();
    //   }).catch((err:any)=>{
    //     this.alert.errorToast("Error Fetching Enabling Objective SubCategories " + err);
    //   })
    // }
  }

  selectedChanged(event: any) {
    if (event.selectedIndex === 1) {

    }
  }

  readyStep1Form() {
    this.step1Form = this.fb.group({
      categoryId: new UntypedFormControl('', [Validators.required]),
      subcategoryId: new UntypedFormControl('', [Validators.required]),
      topicId: new UntypedFormControl('', [Validators.required])
    });
  }

  readyStep2Form() {
    this.step2Form = this.fb.group({
      EONumber: new UntypedFormControl('', [Validators.required]),
      EOStatement: new UntypedFormControl('', [Validators.required]),
      isMetaEO: new UntypedFormControl(false),
    });
  }

  readyStep3Form() {
    this.step3Form = this.fb.group({
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      reason: new UntypedFormControl(''),
    });
  }

  async getCategories(id: any) {
    // 
    // this.selectedCatId = id;
    // 
    // this.eOCatService.getAll().then((res:EnablingObjective_Category[])=>{
    //   this.populateSubCatData()
    // }).catch((err:any)=>{
    //   this.alert.errorToast("Error Fetching Enabling Objective Categories Data " + err.message);
    // })
  }

  async getSubCategories(id: any) {
    // 
    // this.selectedSubCatId = id;
    // 
    // this.eOCatService.getSubCategories(this.selectedCatId).then((res:EnablingObjective_SubCategory[])=>{
    //   //this.cat_Number = res.length + 1;
    //   this.populateTopicData();
    // }).catch((err:any)=>{
    //   this.alert.errorToast("Error Fetching Enabling Objective SubCategories Data " + err.message);
    // })
  }

  async getTopics(id: any){
    // this.selectedTopicId = id;
    // 
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

  async saveEO(){
    // var options = new EnablingObjectiveCreateOptions();
    // options.categoryId = this.step1Form.get("categoryId")?.value;
    // options.subCategoryId = this.step1Form.get("subcategoryId")?.value;
    // options.topicId = this.step1Form.get("topicId")?.value;
    // options.number = this.step2Form.get("EONumber")?.value;
    // options.statement = this.step2Form.get("EOStatement")?.value;
    // options.isMetaEO = this.step2Form.get("isMetaEO")?.value;
    // 
    // await this.eoService.create(options).then((res: EnablingObjective) => {
    //   
    //   this.alert.successToast("Enabling Objective Saved successfully");
    //   this.saveEOHistory(res.id);
    // }).catch((err: any) => {
    //   this.alert.errorToast("Error Saving Enabling Objective Data");
    // });
  }

  async saveEOHistory(id:any){
    // var histOptions = new EnablingObjectiveHistory();
    //   histOptions.changeEffectiveDate = this.step3Form.get("effectiveDate")?.value;
    //   histOptions.changeNotes = this.step3Form.get("reason")?.value;
    //   histOptions.newStatus = true;
    //   histOptions.oldStatus = false;
    //   histOptions.enablingObjectiveId = id;
    //   await this.eoService.saveEOHistory(histOptions).then((res:EnablingObjectiveHistory)=>{
    //     

    //       this.closed.emit('fp-add-sh-cat-closed');
    //       this.refresh.emit();

    //     this.alert.successToast("Enabling Objective Category and History Saved Successfully.");

    //   }).catch((err:any)=>{
    //     this.alert.errorToast("Error Saving Enabling Objective Category History ");
    //   })
  }

}