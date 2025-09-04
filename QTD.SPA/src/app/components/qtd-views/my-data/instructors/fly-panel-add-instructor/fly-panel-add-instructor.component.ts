import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Instructor } from 'src/app/_DtoModels/Instructors/Instructor';
import { Instructor_CreateOptions } from 'src/app/_DtoModels/Instructors/Instructor_CreateOptions';
import { Instructor_Category } from 'src/app/_DtoModels/Instructor_Category/Instructor_Category';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { InstructorCategoryService } from 'src/app/_Services/QTD/instructor-category.service';
import { InstructorService } from 'src/app/_Services/QTD/instructor.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-add-instructor',
  templateUrl: './fly-panel-add-instructor.component.html',
  styleUrls: ['./fly-panel-add-instructor.component.scss']
})

export class FlyPanelAddInstructorComponent implements OnInit {
  @Input() oldInstructor: any; //this should be instructor class whwn we make it dynamic
  @Input() isCopy: any;
  @Input() mode : "Add" | "Edit" | "Copy" = "Add";
  @Input() instructorCheck:boolean;
  isEdit = false;
  showSpinner = false;
  AddAnotherInstructorCategory: boolean = false;
/*   dateError = false; */
  insCategory: any;
  instructorForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  iADescription = "";
  iaNote = "";
  instructorCategoryList : any[] = [];
  CatNumber: number = 0;
  instructorCategoryCheck:boolean=false;

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Output() routerLinkActive = new EventEmitter<any>();
  @Input() shouldNavigate = false;
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private insService:InstructorService,
    private insCatService:InstructorCategoryService,
    private alert: SweetAlertService,
    //private proc_issueAuthService: IssuingAuthoritiesService,
    //private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void 
  {
    this.getCategoriesList();
    this.readyinstructorForm();

    if (this.oldInstructor !== undefined) 
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
    else 
    {
      this.isEdit = false;
      
      //this.getCategoriesList();
    
      //this.instructorCategoryList = ["Category 1","Category 2","Category 3","Category 4","Category 5","Category 6"]

      //this.readyCatNumber();
    }
  }

  ngAfterViewInit(): void {

  }

  closeInstructor() {
    // this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }

  openAddNewInstructorCategory(){
    this.instructorCategoryCheck = true;
  }

  
/*   dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    }
    else {
      this.dateError = true;
    }
  } */

  readyinstructorForm() {
    this.instructorForm = this.fb.group({
      // insCategoryTitle: new FormControl(this.insCategory, [
      //   Validators.required,
      // ]),
      insNumber: new UntypedFormControl('', Validators.required),
      insName: new UntypedFormControl('', Validators.required),
      insEmail: new UntypedFormControl(''),
      insCategory : new UntypedFormControl('', Validators.required),
      insDescription: new UntypedFormControl(''),
      insNotes: new UntypedFormControl('', Validators.required),
      EffectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      isWorkBookAdmin: new UntypedFormControl(false),
      AddAnother: new UntypedFormControl(false),
    });
  }

  async createNewInstructor() {
    this.showSpinner = true;
    var options = new Instructor_CreateOptions();
    options.Name = this.instructorForm.get("insName")?.value;
    options.Description = this.instructorForm.get("insDescription")?.value;
    options.Email = this.instructorForm.get("insEmail")?.value;
    options.Isworkbookadmin = this.instructorForm.get("isWorkBookAdmin")?.value;
    options.ICategoryId = this.instructorForm.get("insCategory")?.value;
    options.Num = this.instructorForm.get("insNumber")?.value;
    options.InstructorNotes = this.instructorForm.get("insNotes")?.value;
    if (this.isCopy) 
    {
      options.Name = this.oldInstructor.instructorName.trim().toLowerCase() == options.Name.trim().toLowerCase()
        ? options.Name + ("-Copy") : options.Name;
    }
    //options.InstructorNotes = this.instructorForm.get("Note")?.value;
    options.EffectiveDate = this.instructorForm.get("EffectiveDate")?.value;
    await this.insService.create(options).then(async (res: Instructor) => {
      
      if(this.isCopy){
        this.alert.successToast(`Successfully Copied ` + await this.labelPipe.transform('Instructor'));
        this.dataBroadcastService.navigateOnChange.next({type:"ins",data:res});
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }
      else{
        this.alert.successToast(`Successfully Created ` + await this.labelPipe.transform('Instructor'));
      }
      // this.saveSHCatHistory(res.id);
      if (this.instructorForm.get('AddAnother')?.value) {
        this.instructorForm.reset();
        this.instructorForm.get('insEmail')?.setValue('');
        this.instructorForm.get('insDescription')?.setValue('');
        this.instructorForm.get('isWorkBookAdmin')?.setValue(false);
        this.instructorForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else 
      {
        this.closed.emit('fp-add-sh-cat-closed');
        
        if(this.instructorCheck){
          this.router.navigate([`/my-data/instructors/details/${res.id}`]);
        }
      }
      this.refresh.emit();
     // this.dataBroadcastService.updateMyDataNavBar.next('');
     if(this.shouldNavigate){
 
      this.dataBroadcastService.navigateOnChange.next({type:"ins",data:res});
    }
    else{
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    }
      this.alert.successToast(`Successfull ${this.isCopy ? "Copied":"Created"} ` + await this.labelPipe.transform('Instructor'));
    }).finally(()=>{
      this.showSpinner = false;
    })
  }

  updateInstructor()
  {
      
      this.showSpinner = true;
      var options = new Instructor_CreateOptions();
      options.ICategoryId = this.instructorForm.get("insCategory")?.value;
      options.Name = this.instructorForm.get("insName")?.value;
      options.Description = this.instructorForm.get("insDescription")?.value;
      options.Email = this.instructorForm.get("insEmail")?.value;
      options.Isworkbookadmin = this.instructorForm.get("isWorkBookAdmin")?.value;
      options.Num = this.instructorForm.get("insNumber")?.value;
      options.EffectiveDate = this.instructorForm.get("EffectiveDate")?.value;
      options.InstructorNotes = this.instructorForm.get("insNotes")?.value;
    
      this.insService.update(this.oldInstructor.id,options).then(async (res: any) => {
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
        this.alert.successToast(await this.labelPipe.transform('Instructor') + " Successfully Updated");
      }).finally(()=>{
        this.showSpinner = false;
      })
  }
  async getCategoriesList() 
  {
    
    await this.insCatService
      .getAll()
      .then((res: any) => {
        if (res != null) {
          this.instructorCategoryList = res;
        }
      })
      .catch((err: any) => {
        this.alert.errorToast('Issuing Authority error');
      });
  }
  readyCatNumber() {
    this.insService.getCount().then((res: number) => {
      this.CatNumber = res + 1;
      this.instructorForm.get('number')?.setValue(this.CatNumber);
    })
  }
  insertDataInForm() 
  {
    //this.CatNumber = this.oldInstructor.ICategoryId;
    this.instructorForm.get('insCategory')?.setValue(this.oldInstructor.iCategoryId);
    this.instructorForm.get('insNumber')?.setValue(this.oldInstructor.instructorNumber);
    this.instructorForm.get('insName')?.setValue(this.oldInstructor.instructorName);
    this.instructorForm.get('insEmail')?.setValue(this.oldInstructor.instructorEmail);
    this.instructorForm.get('isWorkBookAdmin')?.setValue(this.oldInstructor.isWorkBookAdmin);
    this.instructorForm.get('insDescription')?.setValue(this.oldInstructor.instructorDescription);
    this.instructorForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
    this.instructorForm.get('AddAnother')?.setValue(false);
  }
}
