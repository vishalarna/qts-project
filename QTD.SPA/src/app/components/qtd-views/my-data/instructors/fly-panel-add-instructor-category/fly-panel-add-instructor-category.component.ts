 import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Instructor_Category } from 'src/app/_DtoModels/Instructor_Category/Instructor_Category';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { InstructorCategoryService } from 'src/app/_Services/QTD/instructor-category.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-instructor-category',
  templateUrl: './fly-panel-add-instructor-category.component.html',
  styleUrls: ['./fly-panel-add-instructor-category.component.scss']
})
export class FlyPanelAddInstructorCategoryComponent implements OnInit {
  @Input() mode : "Add" | "Edit" | "Copy" = "Add";
  @Input() oldInstructorCategory: any;
  @Input() isCopy:any;
  @Input() instructorCategoryCheck:boolean;
  isEdit = false;
  showSpinner = false;
  AddAnotherInstructorCategory: boolean = false;
/*   dateError = false; */
  insCategoryTitle: any;
  instructorCategoryForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  iADescription = "";
  iaNote = "";
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() shouldNavigate = false;
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private insCatService: InstructorCategoryService,
    private alert: SweetAlertService,
    private router: Router,
    //private proc_issueAuthService: IssuingAuthoritiesService,
    //private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.readyinstructorCategoryForm();
    if (this.oldInstructorCategory !== undefined) 
    {
      this.insertDataInForm();
    }
    // if (this.oldIssuingAuthority !== undefined && !this.isCopy) {
    //   this.isEdit = true;
    //   this.readyIssuingAuthorityFormWithData();
    // }
    // else if (this.oldIssuingAuthority && this.isCopy) {
    //   this.isEdit = false;
    //   this.readyIssuingAuthorityFormWithData();
    // }
    // else {
      this.isEdit = false;
      //this.readyinstructorCategoryForm();
    // }
  }

  ngAfterViewInit(): void {

  }

  closeInstructorCategory() {
    // this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }

  
  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    }
    else {
      this.dateError = true;
    }
  }
 */
  readyinstructorCategoryForm() {
    this.instructorCategoryForm = this.fb.group({
      insCategoryTitle: new UntypedFormControl(this.insCategoryTitle, [
        Validators.required,
      ]),
      insCategoryDescription: new UntypedFormControl(''),
      insCategoryWebsite: new UntypedFormControl(''),
      EffectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      Note: new UntypedFormControl(''),
      AddAnother: new UntypedFormControl(false),
    });
  }
  async createNewInsCategory() {
    this.showSpinner = true;
    var options = new Instructor_Category();
    options.description = this.instructorCategoryForm.get("insCategoryDescription")?.value;
    options.title = this.instructorCategoryForm.get("insCategoryTitle")?.value;
    if (this.isCopy) {
      options.title = this.oldInstructorCategory.iCategoryTitle.trim().toLowerCase() == options.title.trim().toLowerCase()
        ? options.title + ("-Copy") : options.title;
    }
    options.categoryNotes = this.instructorCategoryForm.get("Note")?.value;
    options.effectiveDate = this.instructorCategoryForm.get("EffectiveDate")?.value;
    
    options.website = this.instructorCategoryForm.get("insCategoryWebsite")?.value;
    await this.insCatService.create(options).then(async (res: Instructor_Category) => {
      if(this.isCopy){
        this.alert.successToast(`Successfully Copied ` + await this.labelPipe.transform('Instructor') + ` Category `);
        this.dataBroadcastService.navigateOnChange.next({type:"cat",data:res});
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }
      else{
        this.alert.successToast(`Successfully Created ` + await this.labelPipe.transform('Instructor') + ` Category `);
      }
      // this.saveSHCatHistory(res.id);
      if (this.instructorCategoryForm.get('AddAnother')?.value) {
        this.instructorCategoryForm.reset();
        this.instructorCategoryForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else 
      {
        this.closed.emit('fp-add-sh-cat-closed');
        if(this.instructorCategoryCheck){
          this.router.navigate([`/my-data/instructors/category-details/${res.id}`]);
        }
      }
     
      if(this.shouldNavigate){
        
        this.dataBroadcastService.navigateOnChange.next({type:"cat",data:res});
      }
      else{
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }
      this.refresh.emit();
      //this.alert.successToast(`Successfull ${this.isCopy ? "Copied":"Created"} Instructor Category `);
    }).finally(()=>{
      this.showSpinner = false;
    })
  }
  insertDataInForm() 
  {
    //this.CatNumber = this.oldInstructorCategory.ICategoryId;
    this.instructorCategoryForm.get('insCategoryTitle')?.setValue(this.oldInstructorCategory.iCategoryTitle);
    this.instructorCategoryForm.get('insCategoryDescription')?.setValue(this.oldInstructorCategory.iCategoryDescription);
    this.instructorCategoryForm.get('insCategoryWebsite')?.setValue(this.oldInstructorCategory.iCategoryUrl);
    this.instructorCategoryForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(),'yyyy-MM-dd'));
    this.instructorCategoryForm.get('AddAnother')?.setValue(false);
  }

  updateinsCategory()
  {
      
      this.showSpinner = true;
      var options = new Instructor_Category();
      options.description = this.instructorCategoryForm.get("insCategoryDescription")?.value;
      options.title = this.instructorCategoryForm.get("insCategoryTitle")?.value;
      options.categoryNotes = this.instructorCategoryForm.get("Note")?.value;
      options.effectiveDate = this.instructorCategoryForm.get("EffectiveDate")?.value;
      options.website = this.instructorCategoryForm.get("insCategoryWebsite")?.value;
    
      this.insCatService.update(this.oldInstructorCategory.id,options).then(async (res: any) => {
        // this.saveSHCatHistory(res.id);
        // if (this.instructorForm.get('AddAnother')?.value) {
        //   this.instructorForm.reset();
        //   this.instructorForm.get('effectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
        // }
        // else 
        // {
        //   this.closed.emit('fp-add-ins-cat-closed');
        // }
        this.closed.emit('fp-add-ins-cat-closed');
        this.refresh.emit();
        this.alert.successToast("Successfully Updated " + await this.labelPipe.transform('Instructor') + " Category");
      }).finally(()=>{
        this.showSpinner = false;
      })
  }
}
