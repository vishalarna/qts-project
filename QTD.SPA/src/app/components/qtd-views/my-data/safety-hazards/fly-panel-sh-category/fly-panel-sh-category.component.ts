import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  UntypedFormGroup,
  UntypedFormBuilder,
  UntypedFormControl,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { SaftyHazard_Category } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_Category';
import { SaftyHazard_CategoryHistory } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryHistory';
import { SafetyHazardCategoryService } from 'src/app/_Services/QTD/safety-hazard-category.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SafetyHazard_Categories } from '../../../design-and-development/providers-and-ila/ila-create-wizard/ila-wizard-components/ila-details/fly-panel-ila-safety-hazard/fly-panel-ila-safety-hazard.component';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-sh-category',
  templateUrl: './fly-panel-sh-category.component.html',
  styleUrls: ['./fly-panel-sh-category.component.scss'],
})
export class FlyPanelShCategoryComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refreshCategories = new EventEmitter<any>();
  showSpinner: boolean = false;
  categoryForm: UntypedFormGroup;
  @Input() cat_Number: any = '';
  @Input() oldSHCat: SaftyHazard_Category;
  @Input() isCopy: boolean = false;
  @Input() safetyHazardCategoryCheck:boolean;
  @Input() shouldNavigate = false;
/*   dateError = false; */
  CatNumber: number = 0;
  datePipe = new DatePipe('en-us');
  constructor(
    private fb: UntypedFormBuilder,
    private shCatService: SafetyHazardCategoryService,
    private alert: SweetAlertService,
    private router: Router,
    private dataBroadcastService : DataBroadcastService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.readyCategoryForm();
    if (this.oldSHCat !== undefined) {
      this.insertDataInForm();
    }
    else {
      this.readyCatNumber();
    }
  }

  whitespaceOnlyValidator(control) {
    let pattern = /\S/; // Regular expression to match whitespace-only
  
    if (pattern.test(control.value)) {
      return null; 
    } else {
      return { whitespaceOnly: true }; 
    }
  }

  insertDataInForm() {
    
    this.CatNumber = this.oldSHCat.number;
    this.categoryForm.get('number')?.setValue(this.CatNumber);
    this.categoryForm.get('title')?.setValue(this.oldSHCat.title);
    this.categoryForm.get('desc')?.setValue(this.oldSHCat.description);
    this.categoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(this.oldSHCat.effectiveDate,'yyyy-MM-dd'));
   // this.categoryForm.get('reason')?.setValue(this.oldSHCat.notes);
    this.categoryForm.get('AddAnother')?.setValue(false);
  }

  readyCatNumber() {
    this.shCatService.getCount().then((res: number) => {
      this.CatNumber = res + 1;
      this.categoryForm.get('number')?.setValue(this.CatNumber);
    })
  }

  readyCategoryForm() {
    this.categoryForm = this.fb.group({
      number: new UntypedFormControl(0, [Validators.required,this.whitespaceOnlyValidator]),
      title: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      desc: new UntypedFormControl(''),
      effectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      reason: new UntypedFormControl('', [Validators.required,this.whitespaceOnlyValidator]),
      AddAnother: new UntypedFormControl(false),
    });
  }

  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    }
    else {
      this.dateError = true;
    }
  } */

  async createNewSHCategory() {
    this.showSpinner = true;
    var options = new SaftyHazard_Category();
    options.description = this.categoryForm.get("desc")?.value;
    options.title = this.categoryForm.get("title")?.value;
    if (this.isCopy) {
      options.title = this.oldSHCat.title.trim().toLowerCase() == options.title.trim().toLowerCase()
        ? options.title + ("-Copy") : options.title;
    }
    options.notes = this.categoryForm.get("reason")?.value;
    options.effectiveDate = this.categoryForm.get("effectiveDate")?.value;
    options.number = this.CatNumber;
    await this.shCatService.create(options).then(async (res: SaftyHazard_Category) => {
      this.showSpinner = false;
      if(this.isCopy){
        this.alert.successToast(`Successfully Copied ${await this.labelPipe.transform("Safety Hazard")} Category `);
        this.dataBroadcastService.navigateOnChange.next({type:"cat",data:res});
       // this.dataBroadcastService.updateMyDataNavBar.next();
      }else{
        this.alert.successToast(`Successfully Created ${await this.labelPipe.transform("Safety Hazard")} Category `);
      }
      // this.saveSHCatHistory(res.id);
      if (this.categoryForm.get('AddAnother')?.value) {
        this.categoryForm.reset();
        this.categoryForm.get('desc')?.setValue('');
        this.readyCatNumber();
        this.categoryForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else {
        this.closed.emit('fp-add-sh-cat-closed');
        if(this.safetyHazardCategoryCheck){
          this.router.navigate([`/my-data/safety-hazards/cat/${res.id}`]);
        }
      }
      this.refreshCategories.emit();
      if(this.shouldNavigate){
        this.dataBroadcastService.navigateOnChange.next({type:"cat",data:res});
      }
      else{
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }
      //this.alert.successToast(`Successfull ${this.isCopy ? "Copied":"Created"} Safety Hazard Category `);
    }).finally(()=>{
      this.showSpinner = false;
    })
  }


  editSHCategory(){
    
    this.showSpinner = true;
    var options = new SaftyHazard_Category();
    options.description = this.categoryForm.get("desc")?.value;
    options.title = this.categoryForm.get("title")?.value;
    options.notes = this.categoryForm.get("reason")?.value;
    options.effectiveDate = this.categoryForm.get("effectiveDate")?.value;
    options.number = this.CatNumber;

    this.shCatService.update(this.oldSHCat.id,options).then(async (res:any)=>{
      this.showSpinner = false;
      this.closed.emit('fp-add-sh-cat-closed');
      this.refreshCategories.emit();
      this.alert.successToast(`Successfull Edited ${await this.labelPipe.transform("Safety Hazard")} Category `);
    }).finally(()=>{
      this.showSpinner = false;
    })
  }
}
