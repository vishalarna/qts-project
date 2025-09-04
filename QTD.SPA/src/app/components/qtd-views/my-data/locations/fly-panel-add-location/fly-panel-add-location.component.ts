import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Location } from 'src/app/_DtoModels/Locations/Location';
import { Location_CreateOptions } from 'src/app/_DtoModels/Locations/Location_CreateOptions';
import { Location_Category } from 'src/app/_DtoModels/Location_Category/Location_Category';
import { LocationCategoryService } from 'src/app/_Services/QTD/location-category.service';
import { LocationService } from 'src/app/_Services/QTD/location.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-add-location',
  templateUrl: './fly-panel-add-location.component.html',
  styleUrls: ['./fly-panel-add-location.component.scss']
})

export class FlyPanelAddLocationComponent implements OnInit {
  @Input() oldLocation: any; 
  @Input() isCopy: any;
  @Input() locationCheck:boolean;
  @Input() mode : "Add" | "Edit" | "Copy" = "Add";
  @Input() shouldNavigate = false;
  isEdit = false;
  showSpinner = false;
  locCategory: any;
  locationForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  locDescription = "";
  locNote = "";
  locationCategoryList : any[] = [];
  CatNumber: number = 0;
  locationCategoryCheck:boolean=false;

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private locService:LocationService,
    private locCatService:LocationCategoryService,
    private alert: SweetAlertService,
    private router: Router,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void 
  {
    this.getCategoriesList();
    this.readylocationForm();

    if (this.oldLocation !== undefined) 
    {
      this.insertDataLocForm();
    }

    else 
    {
      this.isEdit = false;
    }
  }

  ngAfterViewInit(): void {

  }

  closeLocation() {
    this.closed.emit('IA_Proc closed');
  }
  
  /* dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    }
    else {
      this.dateError = true;
    }
  } */

  /* readylocationCategoryForm() {
    this.locationForm = this.fb.group({
      locCategoryTitle: new FormControl(this.locCategory, [
        Validators.required,
      ]),
      locNumber: new FormControl('', Validators.required),
      locName: new FormControl('', Validators.required),
      locAddress: new FormControl(''),
      locCategory : new FormControl('', Validators.required),
      locDescription: new FormControl(''),
      EffectiveDate: new FormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      Note:  new FormControl(''),
      AddAnother : new FormControl(false),
    });
  } */

  async createNewLocation() 
  {
    this.showSpinner = true;
    var options = new Location_CreateOptions();
    options.LocName = this.locationForm.get("locName")?.value;
    options.LocDescription = this.locationForm.get("locDescription")?.value;
    options.LocCategoryId = this.locationForm.get("locCategory")?.value;
    options.LocNumber = this.locationForm.get("locNumber")?.value;
    options.LocState = this.locationForm.get("locState")?.value;
    options.LocCity = this.locationForm.get("locCity")?.value;
    options.LocZipCode = this.locationForm.get("locZipCode")?.value;
    options.LocAddress = this.locationForm.get("locAddress")?.value;
    options.LocPhone = this.locationForm.get("locPhone")?.value;
    options.EffectiveDate =  this.locationForm.get('EffectiveDate')?.value;
    if (this.isCopy) 
    {
      options.LocName = this.oldLocation.locName.trim().toLowerCase() == options.LocName.trim().toLowerCase()
        ? options.LocName + ("-Copy") : options.LocName;
    }

    await this.locService.create(options).then(async (res: Location) => {
      if(this.isCopy){
        this.alert.successToast(`Successfully Copied ` + await this.labelPipe.transform('Location'));
        this.dataBroadcastService.navigateOnChange.next({type:"loc",data:res});
        this.dataBroadcastService.updateMyDataNavBar.next(null);
        this.router.navigate([`/my-data/locations/details/${res.id}`]);
      }else{
        this.alert.successToast(`Successfully Created ` + await this.labelPipe.transform('Location'));
      }

      if (this.locationForm.get('AddAnother')?.value) {
        this.locationForm.reset();
        this.locationForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
      }
      else {
        this.closed.emit('fp-add-sh-cat-closed');
        if(this.locationCheck){
          this.router.navigate([`/my-data/locations/details/${res.id}`]);
        }
      }
      this.refresh.emit();
      if(this.shouldNavigate){
        this.dataBroadcastService.navigateOnChange.next({type:"loc",data:res});
      }
      else{
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      }
      this.alert.successToast(`Successfully ${this.isCopy ? "Copied":"Created"} ` + await this.labelPipe.transform('Location'));
    }).finally(()=>{
      this.showSpinner = false;
    })
  }


  updateLocation()
  {
      
      this.showSpinner = true;
      var options = new Location_CreateOptions();
      options.LocCategoryId = this.locationForm.get("locCategory")?.value;
      options.LocName = this.locationForm.get("locName")?.value;
      options.LocDescription = this.locationForm.get("locDescription")?.value;
      options.LocAddress = this.locationForm.get("locAddress")?.value;
      options.LocNumber = this.locationForm.get("locNumber")?.value;

      options.LocState = this.locationForm.get("locState")?.value;
      options.LocCity = this.locationForm.get("locCity")?.value;
      options.LocZipCode = this.locationForm.get("locZipCode")?.value;
      options.LocPhone = this.locationForm.get("locPhone")?.value;
      options.Notes = this.locationForm.get("Notes")?.value;
      options.EffectiveDate = this.locationForm.get('EffectiveDate')?.value,

      this.locService.update(this.oldLocation.id,options).then(async (res: any) => {
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
        this.alert.successToast(await this.labelPipe.transform('Location') + " Successfully Updated");
      }).finally(()=>{
        this.showSpinner = false;
      })
  }


  readylocationForm() {
    this.locationForm = this.fb.group({

      locNumber: new UntypedFormControl('', Validators.required),
      locName: new UntypedFormControl('', Validators.required),
      locEmail: new UntypedFormControl(''),
      locCategory : new UntypedFormControl('', Validators.required),
      locDescription: new UntypedFormControl(''),
      EffectiveDate: new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd")),
      locAddress: new UntypedFormControl(''),
      locCity: new UntypedFormControl(''),
      locState: new UntypedFormControl(''),
      locZipCode: new UntypedFormControl(''),
      locPhone: new UntypedFormControl(''),
      Notes: new UntypedFormControl('', Validators.required),
      AddAnother : new UntypedFormControl(false),

    });
  }



  async getCategoriesList() 
  {
    
    await this.locCatService
      .getAll()
      .then((res: any) => {
        if (res != null) {
          this.locationCategoryList = res;
        }
      })
      .catch(async (err: any) => {
        this.alert.errorToast(await this.labelPipe.transform('Location') + ' category error');
      });
  }

  readyCatNumber() {
    this.locService.getCount().then((res: number) => {
      this.CatNumber = res + 1;
      this.locationForm.get('number')?.setValue(this.CatNumber);
    })
  }

  insertDataLocForm() 
  {
    this.locationForm.get('locCategory')?.setValue(this.oldLocation.locCategoryID);
    this.locationForm.get('locNumber')?.setValue(this.oldLocation.locNumber);
    this.locationForm.get('locName')?.setValue(this.oldLocation.locName);
    this.locationForm.get('locPhone')?.setValue(this.oldLocation.locPhone);
    this.locationForm.get('locAddress')?.setValue(this.oldLocation.locAddress);
    this.locationForm.get('locCity')?.setValue(this.oldLocation.locCity);
    this.locationForm.get('locState')?.setValue(this.oldLocation.locState);
    this.locationForm.get('locZipCode')?.setValue(this.oldLocation.locZipCode);    
    this.locationForm.get('locDescription')?.setValue(this.oldLocation.locDescription);
    this.locationForm.get('EffectiveDate')?.setValue(this.datePipe.transform(Date.now(), "yyyy-MM-dd"));
  }

  openAddNewLocationCategory(){
    this.locationCategoryCheck = true;
  }
}


