import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-ska-sub-category',
  templateUrl: './fly-panel-add-ska-sub-category.component.html',
  styleUrls: ['./fly-panel-add-ska-sub-category.component.scss']
})
export class FlyPanelAddSkaSubCategoryComponent implements OnInit {

  @Output() closed = new EventEmitter<any>();
  @Output() refreshSubCategories = new EventEmitter<any>();
  showSpinner: boolean = false;
  subCategoryForm: UntypedFormGroup;
  @Input() subCat_Number: any = '';
  CatNumber: number = 0;
  selectedCatId = "";
  datePipe = new DatePipe('en-us');
  categories: any[] = [];
  constructor(private fb: UntypedFormBuilder,
    //public eOService:EnablingObjectivesCategoryService,
    public alert:SweetAlertService) {}

  ngOnInit(): void {
    this.readyCategoryForm();
   
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

  readySubCatNumber() {
    // this.eOService.getSubCategories(this.selectedCatId).then((res: EnablingObjective_SubCategory[]) => {
    //   this.CatNumber = res.length + 1;
    // })
    this.CatNumber = 1;
  }

  async getCategories(id: any) {
    // this.selectedCatId = id;
    // 
    // this.eOService.getAll().then((res:EnablingObjective_Category[])=>{
    //   this.readySubCatNumber();
    // }).catch((err:any)=>{
    //   this.alert.errorToast("Error Fetching Enabling Objective Category Number Data " + err.message);
    // })
  }

  readyCategoryForm() {
    this.subCategoryForm = this.fb.group({
      number: new UntypedFormControl(this.subCat_Number, [Validators.required]),
      title: new UntypedFormControl('', [Validators.required]),
      desc: new UntypedFormControl('', [Validators.required]),
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      reason: new UntypedFormControl(''),
      category: new UntypedFormControl('', Validators.required),
      AddAnother: new UntypedFormControl(false),
    });
  }

  async saveSubCategory(){
    // var options = new EnablingObjective_SubCategory();
    // options.title = this.subCategoryForm.get("title")?.value;
    // options.description = this.subCategoryForm.get("desc")?.value;
    // options.number = this.subCategoryForm.get("number")?.value;
    // options.categoryId = this.subCategoryForm.get('category')?.value;
    // var id = options.number;;
    // await this.eOService.createSubCategory(this.selectedCatId, options).then((res: EnablingObjective_SubCategory) => {
    //   
    //   this.saveEOSubCatHistory(res.id);
    //   this.alert.successToast("Sub category Data saved successfully");
    // }).catch((err: any) => {
    //   this.alert.errorToast("Error Saving Category Data");
    // });
  }

  async saveEOSubCatHistory(id:any){
    
    // var histOptions = new EnablingObjective_SubCategoryHistory();
    //   histOptions.changeEffectiveDate = this.subCategoryForm.get("effectiveDate")?.value;
    //   histOptions.changeNotes = this.subCategoryForm.get("reason")?.value;
    //   histOptions.newStatus = true;
    //   histOptions.oldStatus = false;
    //   histOptions.enablingObjectiveSubCategoryId = id;
    //   await this.eOService.saveEOSubCatHistory(histOptions).then((res:EnablingObjective_SubCategoryHistory)=>{
    //     
    //     if(this.subCategoryForm.get('AddAnother')?.value){
    //       this.subCategoryForm.reset();
    //       this.readySubCatNumber();
    //       this.subCategoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
    //       this.subCategoryForm.get('desc')?.setValue(null);
    //       this.subCategoryForm.get('reason')?.setValue(null);
    //     }
    //     else{
    //       this.closed.emit('fp-add-sh-cat-closed');
    //     }
    //     this.refreshSubCategories.emit();
        
    //     this.alert.successToast("Enabling Objective Category and History Saved Successfully.");
        
    //   }).catch((err:any)=>{
    //     this.alert.errorToast("Error Saving Enabling Objective Category History ");
    //   })
  }

}
