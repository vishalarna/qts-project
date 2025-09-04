import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjective_SubCategoryHistory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategoryHistory';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-eo-sub-category',
  templateUrl: './flypanel-eo-sub-category.component.html',
  styleUrls: ['./flypanel-eo-sub-category.component.scss']
})
export class FlypanelEOSubCategoryComponent implements OnInit {
  @Input() hasSpace = false;
  @Input() shouldNavigate = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refreshSubCategories = new EventEmitter<any>();
  @Output() refreshSubCatData = new EventEmitter<EnablingObjective_SubCategory>();

  @Input() subCategory: EnablingObjective_SubCategory;

  showSpinner: boolean = false;
  subCategoryForm: UntypedFormGroup;
  @Input() subCat_Number: any = 0;
  CatNumber: number = 0;
  selectedCatId = "";
  datePipe = new DatePipe('en-us');
  categories: any[] = [];
  constructor(private fb: UntypedFormBuilder,
    public eOService: EnablingObjectivesCategoryService,
    public alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,) { }

  ngOnInit(): void {
    this.readyCategoryForm();
  }

  ngAfterViewInit(): void {
    this.populateCatData();
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


  async populateCatData() {
    await this.eOService.getAllSimplifiedCategories().then((res: EnablingObjective_Category[]) => {
      this.categories = res;
      this.subCategory ? this.populateDataForEdit() : '';
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching "+ await this.transformTitle('Enabling Objective') + " Categories " + err);
    })
  }

  readySubCatNumber() {
    this.eOService.getSubCategoryNumber(this.selectedCatId).then((res: number) => {
      this.subCategoryForm.patchValue({
        number: res,
      })
    })
  }

  async getCategories(id: any) {
    this.selectedCatId = id;
    this.readySubCatNumber();
  }


  populateDataForEdit() {
    this.subCategoryForm.patchValue({
      title: this.subCategory.title,
      desc: this.subCategory.description,
      category: this.subCategory.categoryId,
      number: this.subCategory.number,
      effectiveDate: this.datePipe.transform(this.subCategory.effectiveDate, "yyyy-MM-dd")
    });
  }

  readyCategoryForm() {
    this.subCategoryForm = this.fb.group({
      /*   number: new FormControl({ value: 0, disabled: (this.subCategory) }, [Validators.required]), */
      number: new UntypedFormControl('', Validators.required),
      title: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      desc: new UntypedFormControl(''),
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      reason: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      category: new UntypedFormControl({ value: '', disabled: this.subCategory }, Validators.required),
      AddAnother: new UntypedFormControl(false),
    });
  }

  async saveSubCategory() {
    this.showSpinner = true;
    var options = new EnablingObjective_SubCategory();
    options.title = this.subCategoryForm.get("title")?.value;
    options.description = this.subCategoryForm.get("desc")?.value;
    options.number = this.subCategoryForm.get("number")?.value;
    options.categoryId = this.subCategoryForm.get('category')?.value;
    options.effectiveDate = this.subCategoryForm.get('effectiveDate')?.value;
    options.reason = this.subCategoryForm.get("reason")?.value;
    await this.eOService.createSubCategory(this.selectedCatId, options).then(async (res: EnablingObjective_SubCategory) => {
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.dataBroadcastService.refreshStats.next(null);
      if (this.shouldNavigate) {
        this.dataBroadcastService.navigateOnChange.next({ type: "SUB", data: res })
      }
      // this.saveEOSubCatHistory(res.id);
      if (this.subCategoryForm.get('AddAnother')?.value) {
        this.subCategoryForm.reset();
        this.readySubCatNumber();
        this.subCategoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.subCategoryForm.get('desc')?.setValue(null);
        this.subCategoryForm.get('reason')?.setValue(null);
      }
      else {
        this.closed.emit('fp-add-sh-cat-closed');
      }
      this.refreshSubCategories.emit();

      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Sub Category and History Saved Successfully.");
    }).finally(() => {
      this.showSpinner = false;
    })
  }

  async updateSubCategory() {
    this.showSpinner = true;
    var options = new EnablingObjective_SubCategory();
    options.title = this.subCategoryForm.get("title")?.value;
    options.description = this.subCategoryForm.get("desc")?.value;
    options.number = this.subCategoryForm.get("number")?.value;
    options.categoryId = this.subCategoryForm.get('category')?.value;
    options.effectiveDate = this.subCategoryForm.get('effectiveDate')?.value;
    options.reason = this.subCategoryForm.get("reason")?.value;
    await this.eOService.updateSubCategory(this.subCategory.id, options).then(async (res: EnablingObjective_SubCategory) => {
      this.showSpinner = false;
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      // this.saveEOSubCatHistory(res.id);
      if (this.subCategoryForm.get('AddAnother')?.value) {
        this.subCategoryForm.reset();
        this.readySubCatNumber();
        this.subCategoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        this.subCategoryForm.get('desc')?.setValue(null);
        this.subCategoryForm.get('reason')?.setValue(null);
      }
      else {
        this.closed.emit('fp-add-sh-cat-closed');
      }
      this.refreshSubCategories.emit();
      this.refreshSubCatData.emit(res);

      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Sub Category and History Saved Successfully.");
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  // async saveEOSubCatHistory(id: any) {
  //   var histOptions = new EnablingObjective_SubCategoryHistory();
  //   histOptions.changeEffectiveDate = this.subCategoryForm.get("effectiveDate")?.value;
  //   histOptions.changeNotes = this.subCategoryForm.get("reason")?.value;
  //   histOptions.newStatus = true;
  //   histOptions.oldStatus = false;
  //   histOptions.enablingObjectiveSubCategoryId = id;
  //   await this.eOService.saveEOSubCatHistory(histOptions).then((res: EnablingObjective_SubCategoryHistory) => {

  //     if (this.subCategoryForm.get('AddAnother')?.value) {
  //       this.subCategoryForm.reset();
  //       this.readySubCatNumber();
  //       this.subCategoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
  //       this.subCategoryForm.get('desc')?.setValue(null);
  //       this.subCategoryForm.get('reason')?.setValue(null);
  //     }
  //     else {
  //       this.closed.emit('fp-add-sh-cat-closed');
  //     }
  //     this.refreshSubCategories.emit();

  //     this.alert.successToast("Enabling Objective Sub Category and History Saved Successfully.");

  //   })
  // }

}
