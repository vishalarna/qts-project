import { DatePipe, formatDate } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewContainerRef,
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Location_CreateOptions } from 'src/app/_DtoModels/Locations/Location_CreateOptions';
import { Location_Category } from 'src/app/_DtoModels/Location_Category/Location_Category';
import { Location_CategoryOptions } from 'src/app/_DtoModels/Location_Category/Location_CategoryOptions';
import { LocationCategoryService } from 'src/app/_Services/QTD/location-category.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-add-location-category',
  templateUrl: './fly-panel-add-location-category.component.html',
  styleUrls: ['./fly-panel-add-location-category.component.scss'],
})
export class FlyPanelAddLocationCategoryComponent implements OnInit {
  @Input() mode: 'Add' | 'Edit' | 'Copy' = 'Add';
  @Input() oldLocationCategory: any;
  @Input() isCopy: any;
  @Input() locationCategoryCheck:boolean;
  @Input() shouldNavigate = false;
  isEdit = false;
  showSpinner = false;
  AddAnotherLocationCategory: boolean = false;
/*   dateError = false; */
  locCategoryTitle: any;
  locationCategoryForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  locCategoryDescription = '';
  locNote = '';
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private locCatService: LocationCategoryService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.readylocationCategoryForm();
    if (this.oldLocationCategory !== undefined) {
      this.insertDataLocForm();
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

  ngAfterViewInit(): void {}

  closeLocationCategory() {
    // this.flyPanelSrvc.close();
    this.closed.emit('Location closed');
  }

  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    } else {
      this.dateError = true;
    }
  } */

  readylocationCategoryForm() {
    this.locationCategoryForm = this.fb.group({
      locCategoryTitle: new UntypedFormControl(this.locCategoryTitle, [
        Validators.required,
      ]),
      locCategoryDescription: new UntypedFormControl(''),
      locCategoryWebsite: new UntypedFormControl(''),
      EffectiveDate: new UntypedFormControl(
        this.datePipe.transform(Date.now(), 'yyyy-MM-dd')
      ),
      Note: new UntypedFormControl('',[
        Validators.required,
      ]),
      AddAnother: new UntypedFormControl(false),
    });
  }

  // async createNewLocCategory() {
  //   
  //   this.showSpinner = true;
  //   var options = {
  //     title: this.locationCategoryForm.get('locCategoryTitle')?.value,
  //     description: this.locationCategoryForm.get('locCategoryDescription')
  //       ?.value,
  //     website: this.locationCategoryForm.get('locCategoryWebsite')?.value,
  //     effectiveDate: this.locationCategoryForm.get('EffectiveDate')?.value,
  //     categoryNotes: this.locationCategoryForm.get('Note')?.value,
  //   };
  //   if (this.isCopy) {
  //     options.title =
  //       this.oldLocationCategory.LocCategoryTitle.trim().toLowerCase() == options.title.trim().toLowerCase()
  //         ? options.title + '-Copy'
  //         : options.title;
  //   }

  //   // options.LocCategoryDesc = this.locationCategoryForm.get(
  //   //   'locCategoryDescription'
  //   // )?.value;
  //   // options.LocCategoryTitle =
  //   //   this.locationCategoryForm.get('locCategoryTitle')?.value;
  //   //
  //   // if (this.isCopy) {
  //   //   options.LocCategoryTitle =
  //   //     this.oldLocationCategory.LocCategoryTitle.trim().toLowerCase() ==
  //   //     options.LocCategoryTitle.trim().toLowerCase()
  //   //       ? options.LocCategoryTitle + '-Copy'
  //   //       : options.LocCategoryTitle;
  //   // }
  //   // options.CreatedDate = this.locationCategoryForm.get('createDate')?.value;
  //   // options.website =
  //   //   this.locationCategoryForm.get('locCategoryWebsite')?.value;
  //   await this.locCatService
  //     .create(options)
  //     .then((res: Location_Category) => {
  //       if (this.locationCategoryForm.get('AddAnother')?.value) {
  //         this.locationCategoryForm.reset();
  //         this.locationCategoryForm
  //           .get('effectiveDate')
  //           ?.setValue(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'));
  //       } else {
  //         this.closed.emit('fp-add-sh-cat-closed');
  //       }
  //       this.refresh.emit();
  //       this.alert.successToast(
  //         `Successfull ${this.isCopy ? 'Copied' : 'Created'} Location Category `
  //       );
  //     })
  //     .finally(() => {
  //       this.showSpinner = false;
  //     });
  // }


  async createNewLocCategory() {
    
    this.showSpinner = true;
    var options = new Location_Category();
    options.description = this.locationCategoryForm.get("locCategoryDescription")?.value;
    options.title = this.locationCategoryForm.get("locCategoryTitle")?.value;
    options.EffectiveDate = this.locationCategoryForm.get('EffectiveDate')?.value;
    options.categoryNotes = this.locationCategoryForm.get('Note')?.value;
    if (this.isCopy) {
      options.title = this.oldLocationCategory.locCategoryTitle.trim().toLowerCase() == options.title.trim().toLowerCase()
        ? options.title.concat("-Copy") : options.title;
    }

    options.website = this.locationCategoryForm.get("locCategoryWebsite")?.value;
    await this.locCatService.create(options).then(async (res: Location_Category) => {
      if(this.isCopy){
        this.alert.successToast(`Successfully Copied ` + await this.labelPipe.transform('Location') +` Category `);
        this.dataBroadcastService.navigateOnChange.next({type:"cat",data:res});
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }else{
        this.alert.successToast(`Successfully Created ` + await this.labelPipe.transform('Location') +` Category `);
      }
      // this.saveSHCatHistory(res.id);
      if (this.locationCategoryForm.get('AddAnother')?.value) {
        this.locationCategoryForm.reset();
        this.locationCategoryForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else
      {
        this.closed.emit('fp-add-sh-cat-closed');
        if(this.locationCategoryCheck){
          this.router.navigate([`/my-data/locations/category-details/${res.id}`]);
        }
      }
      this.refresh.emit();
      if(this.shouldNavigate){
        this.dataBroadcastService.navigateOnChange.next({type:"cat",data:res});
      }
      else{
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }
      //this.alert.successToast(`Successfull ${this.isCopy ? "Copied":"Created"} Location Category `);
    }).finally(()=>{
      this.showSpinner = false;
    })
  }

  insertDataLocForm() {
    this.locationCategoryForm
      .get('locCategoryTitle')
      ?.setValue(this.oldLocationCategory.locCategoryTitle);
    this.locationCategoryForm
      .get('locCategoryDescription')
      ?.setValue(this.oldLocationCategory.locCategoryDesc);
    this.locationCategoryForm
      .get('locCategoryWebsite')
      ?.setValue(this.oldLocationCategory.locCategoryWebsite);
    this.locationCategoryForm
      .get('EffectiveDate')
      ?.setValue(
        this.datePipe.transform(
          Date.now(),
          'yyyy-MM-dd'
        )
      );
    this.locationCategoryForm.get('AddAnother')?.setValue(false);
  }

  updatelocCategory() {
    
    this.showSpinner = true;
    // var options = new Location_Category();
    // options.LocCategoryDesc = this.locationCategoryForm.get(
    //   'locCategoryDescription'
    // )?.value;
    // options.LocCategoryTitle =
    //   this.locationCategoryForm.get('locCategoryTitle')?.value;
    // options.LocCategoryDesc = this.locationCategoryForm.get('Desc')?.value;
    // options.CreatedDate = this.locationCategoryForm.get('effectiveDate')?.value;
    // options.website =
    //   this.locationCategoryForm.get('locCategoryWebsite')?.value;

    var options = {
      title: this.locationCategoryForm.get('locCategoryTitle')?.value,
      description: this.locationCategoryForm.get('locCategoryDescription')
        ?.value,
      website: this.locationCategoryForm.get('locCategoryWebsite')?.value,
      EffectiveDate: this.locationCategoryForm.get('EffectiveDate')?.value,
      categoryNotes: this.locationCategoryForm.get('Note')?.value,
    };

    this.locCatService
      .update(this.oldLocationCategory.id, options)
      .then(async (res: any) => {
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
        this.alert.successToast('Successfully Updated ' + await this.labelPipe.transform('Location') + ' Category');
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }
}
