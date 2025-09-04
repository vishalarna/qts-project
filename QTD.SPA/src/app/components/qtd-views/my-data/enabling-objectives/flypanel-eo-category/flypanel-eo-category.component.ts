import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_CategoryHistory } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_CategoryHistory';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-eo-category',
  templateUrl: './flypanel-eo-category.component.html',
  styleUrls: ['./flypanel-eo-category.component.scss']
})
export class FlypanelEOCategoryComponent implements OnInit {

  @Output() closed = new EventEmitter<any>();
  @Output() refreshCategories = new EventEmitter<any>();
  @Output() refreshCateforyData = new EventEmitter<EnablingObjective_Category>();
  showSpinner: boolean = false;
  categoryForm: UntypedFormGroup;
  @Input() cat_Number: any = 0;
  @Input() hasSpace: boolean = false;

  @Input() category: EnablingObjective_Category;
  @Input() shouldNavigate = false;

  CatNumber: number = 0;
  datePipe = new DatePipe('en-us');
  selectedCatId = "";
  constructor(private fb: UntypedFormBuilder,
    public eOService: EnablingObjectivesCategoryService,
    public alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,) { }

  ngOnInit(): void {
    this.readyCategoryForm();
    this.category === undefined ? this.readyCatNumber() : this.readyPreviousData();
  }

  readyPreviousData() {

    this.categoryForm.patchValue({
      number: this.category.number,
      title: this.category.title,
      desc: this.category.description,
      effectiveDate: this.datePipe.transform(this.category.effectiveDate, "yyyy-MM-dd")
    });
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only
  
    if (pattern.test(control.value)) {
      return null; 
    } else {
      return { whitespaceOnly: true }; 
    }
  }

  readyCategoryForm() {
    this.categoryForm = this.fb.group({
      number: new UntypedFormControl({ value: this.cat_Number, disabled: this.category }, [Validators.required]),
      title: new UntypedFormControl('',[Validators.required,this.whitespaceOnlyValidator]),
      desc: new UntypedFormControl(''),
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      reason: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      AddAnother: new UntypedFormControl(false),
    });
  }

  readyCatNumber() {
    this.eOService.getCatNumber().then((res: number) => {
      this.categoryForm.patchValue({
        number: res,
      })
    })
  }

  async saveCategory() {
    this.showSpinner = true;
    var options = new EnablingObjective_Category();
    options.title = this.categoryForm.get("title")?.value;
    options.description = this.categoryForm.get("desc")?.value;
    options.number = this.categoryForm.get("number")?.value;
    options.effectiveDate = this.categoryForm.get("effectiveDate")?.value;
    options.reason = this.categoryForm.get("reason")?.value;
    await this.eOService.create(options).then(async (res: EnablingObjective_Category) => {
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.dataBroadcastService.refreshStats.next(null);
      // this.saveEOCatHistory(res);
      if (this.categoryForm.get('AddAnother')?.value) {
        this.readyCatNumber();
        this.categoryForm.reset();
        this.categoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else {
        this.closed.emit('fp-add-sh-cat-closed');
      }
      this.refreshCategories.emit();

      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Category and History Saved Successfully.");
      if (this.shouldNavigate) {
        this.dataBroadcastService.navigateOnChange.next({ type: "CAT", data: res })
      }
    }).finally(() => {
      this.showSpinner = false;
    });
  }

  async saveEOCatHistory(cat: any) {
    var histOptions = new EnablingObjective_CategoryHistory();
    histOptions.changeEffectiveDate = this.categoryForm.get("effectiveDate")?.value;
    histOptions.changeNotes = this.categoryForm.get("reason")?.value;
    histOptions.newStatus = true;
    histOptions.oldStatus = false;
    histOptions.enablingObjectiveCategoryId = cat.id;
    await this.eOService.saveEOCatHistory(histOptions).then(async (res: EnablingObjective_CategoryHistory) => {
      if (this.categoryForm.get('AddAnother')?.value) {
        this.readyCatNumber();
        this.categoryForm.reset();
        this.categoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else {
        this.closed.emit('fp-add-sh-cat-closed');
      }
      this.refreshCategories.emit();

      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Category and History Saved Successfully.");
      if (this.shouldNavigate) {
        this.dataBroadcastService.navigateOnChange.next({ type: "CAT", data: cat })
      }
    });
  }

  async getCategories(id: any) {
    this.selectedCatId = id;
    this.eOService.getWithEOCategoryId(this.selectedCatId).then((res: EnablingObjective_Category[]) => {
      this.cat_Number = res.length + 1;
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching "+ await this.labelPipe.transform("Safety Hazard") + " Number Data " + err.message);
    })
  }



  async updateCategory() {
    this.showSpinner = true;
    var options = new EnablingObjective_Category();
    options.title = this.categoryForm.get("title")?.value;
    options.description = this.categoryForm.get("desc")?.value;
    options.number = this.categoryForm.get("number")?.value;
    options.effectiveDate = this.categoryForm.get("effectiveDate")?.value;
    options.reason = this.categoryForm.get("reason")?.value;
    await this.eOService.update(this.category.id, options).then(async (res: EnablingObjective_Category) => {
      this.showSpinner = false;
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.dataBroadcastService.refreshStats.next(null);
      this.refreshCateforyData.emit(res);
      if (this.categoryForm.get('AddAnother')?.value) {
        this.readyCatNumber();
        this.categoryForm.reset();
        this.categoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else {
        this.closed.emit('fp-add-sh-cat-closed');
      }
      // this.refreshCategories.emit();

      this.alert.successToast(await this.transformTitle('Enabling Objective') + " Category and History Saved Successfully.");
      if (this.shouldNavigate) {
        this.dataBroadcastService.navigateOnChange.next({ type: "CAT", data: res })
      }
      // this.saveEOCatHistory(res.id);
    }).finally(()=>{
      this.showSpinner = false;
    })
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
