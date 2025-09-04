import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-ska-category',
  templateUrl: './fly-panel-add-ska-category.component.html',
  styleUrls: ['./fly-panel-add-ska-category.component.scss']
})
export class FlyPanelAddSkaCategoryComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refreshCategories = new EventEmitter<any>();
  showSpinner: boolean = false;
  categoryForm: UntypedFormGroup;
  @Input() cat_Number: any = '';
  CatNumber: number = 0;
  EoSymbol:number=0;
  datePipe = new DatePipe('en-us');
  selectedCatId = "";
  constructor(private fb: UntypedFormBuilder,
        //public eOService:EnablingObjectivesCategoryService,
        public alert:SweetAlertService) {}

  ngOnInit(): void {
    this.readyCategoryForm();
    this.readyCatNumber();
  }

  readyCategoryForm() {
    this.categoryForm = this.fb.group({
      number: new UntypedFormControl(this.cat_Number, [Validators.required]),
      eosymbol : new UntypedFormControl(this.EoSymbol),
      title: new UntypedFormControl('', [Validators.required]),
      desc: new UntypedFormControl('', [Validators.required]),
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      reason: new UntypedFormControl(''),
      AddAnother: new UntypedFormControl(false),
    });
  }

  readyCatNumber() {
    // this.eOService.getAll().then((res: EnablingObjective_Category[]) => {
    //   if(res.length == 0)
    //   {
    //     this.CatNumber = res.length + 1;
    //   }
    //   else {
    //     this.CatNumber = Math.max(...res.map((x) => x.number)) + 1;
    //   }
    // })

    this.CatNumber = 1;
  }

  async saveCategory(){
    // var options = new EnablingObjective_Category();
    // options.title = this.categoryForm.get("title")?.value;
    // options.description = this.categoryForm.get("desc")?.value;
    // options.number = this.categoryForm.get("number")?.value;
    // await this.eOService.create(options).then((res: EnablingObjective_Category) => {
    //   this.saveEOCatHistory(res.id);
    // }).catch((err: any) => {
    //   this.alert.errorToast("Error Saving Category Data");
    // });
  }

  async saveEOCatHistory(id:any){
    // var histOptions = new EnablingObjective_CategoryHistory();
    //   histOptions.changeEffectiveDate = this.categoryForm.get("effectiveDate")?.value;
    //   histOptions.changeNotes = this.categoryForm.get("reason")?.value;
    //   histOptions.newStatus = true;
    //   histOptions.oldStatus = false;
    //   histOptions.enablingObjectiveCategoryId = id;
    //   await this.eOService.saveEOCatHistory(histOptions).then((res:EnablingObjective_CategoryHistory)=>{
    //     
    //     if(this.categoryForm.get('AddAnother')?.value){
    //       this.categoryForm.reset();
    //       this.readyCatNumber();
    //       this.categoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
    //       this.categoryForm.get('desc')?.setValue(null);
    //       this.categoryForm.get('reason')?.setValue(null);
    //     }
    //     else{
    //       this.closed.emit('fp-add-sh-cat-closed');
    //     }
    //     this.refreshCategories.emit();
        
    //     this.alert.successToast("Enabling Objective Category and History Saved Successfully.");
        
    //   }).catch((err:any)=>{
    //     this.alert.errorToast("Error Saving Enabling Objective Category History ");
    //   })
  }

  async getCategories(id: any) {
    // this.selectedCatId = id;
    // 
    // this.eOService.getWithEOCategoryId(this.selectedCatId).then((res:EnablingObjective_Category[])=>{
    //   this.cat_Number = res.length + 1;
    // }).catch((err:any)=>{
    //   this.alert.errorToast("Error Fetching Safety Hazard Number Data " + err.message);
    // })
  }
}
