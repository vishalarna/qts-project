import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Location_HistoryCreateOptions } from 'src/app/_DtoModels/Location_History/Location_HistoryCreateOptions';
import { LocationService } from 'src/app/_Services/QTD/location.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { Location } from 'src/app/_DtoModels/Locations/Location';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';


@Component({
  selector: 'app-location-details',
  templateUrl: './locations-details.component.html',
  styleUrls: ['./locations-details.component.scss']
})
export class LocationDetailsComponent implements OnInit
{
  modalType:"Add" | "Edit" | "Copy"
  datePipe = new DatePipe('en-us');
  isActive: boolean = true;
  subscription = new SubSink();
  issuAuthTitle = "";
  procTitle = "";
  revisionNumber = "";
  hyperlink = "";
  effectiveDate : string|null = "";
  deleteDescription = "";
  isCopy = false;
  description = '';
  website = '';
  title = '';
  id = '';
  isLoading = false;
  email =''
  IsWorkBookAdmin = ''
  effevtiveDate :any;
  name ='';
  address = '';
  state = '';
  city = '';
  zipcode = '';
  modalHeader = '';
  modalDescription = '';
  locationNumber:any;
  location:Location;
  isLocCatActive:boolean;
  locationPhone:any;
  deleteCheck:number;
  constructor( private route:ActivatedRoute,
    public vcf:ViewContainerRef,

    private alert:SweetAlertService,
    private dataBroadcastService:DataBroadcastService,
    public flyPanelService:FlyInPanelService,
    public dialog:MatDialog,
    private router:Router,
    private locService:LocationService,
    private labelPipe: LabelReplacementPipe,) { }

  ngOnInit(): void {
    
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.isLoading = true;
      this.id = res.id;
      this.populateData();
      this.getLocationData(res.id);
    });
  }
  ngAfterViewInit(): void
  {



  }


  ngOnDestroy(): void {
    //this.subscription.unsubscribe();
  }



 openflyPanel(templateRef:any,mode:any)
 {
    this.modalType = mode;
    if(this.modalType === 'Copy')
    {
      this.isCopy = true;
    }

    if(this.modalType === 'Edit')
    {
      this.locService.get(this.id)
      .then((data: any) => {
        this.isLoading = false;
        this.isActive = data.active;
        this.email = data.email;
        this.effevtiveDate = this.datePipe.transform(data.effectiveDate, 'MM-dd-yyyy');
        this.description = data.locDescription;
        this.address = data.locAddress;
        this.name = data.locName;
        this.state = data.locState;
        this.zipcode = data.locZipCode;
        this.city = data.locCity;
        this.locationNumber = data.locNumber;
        this.locationPhone = data.locPhone;
      })

    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  // editProcedure(templateRef:any){
  //   this.isCopy = false;
  //   const portal = new TemplatePortal(templateRef, this.vcf);
  //   this.flyPanelService.open(portal);
  // }

  async deleteProcedure(templateRef:any){
   this.deleteDescription = `You are selecting to delete ` + await this.labelPipe.transform('Location') +` ${this.name}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  populateData() {
    this.locService.get(this.id)
      .then((data) => {

        this.isLoading = false;
        this.isActive = data.active;
        this.email = data.locAddress;
        this.effevtiveDate = this.datePipe.transform(data.effectiveDate, 'MM-dd-yyyy');
        this.description = data.locDescription;
        this.name = data.locName;
        this.address = data.locAddress;
        this.state = data.locState;
        this.city = data.locCity;
        this.zipcode = data.locZipCode;
        this.locationNumber = data.locNumber;
        this.locationPhone = data.locPhone;
        this.deleteCheck = data.classSchedules.length;
      })
      .catch(async (err: any) => {
        this.isLoading = false;
        this.alert.errorToast('Error Fetching ' + await this.labelPipe.transform('Location') + ' Detail');
      });
  }

  async changeStatus(templateRef: any, active: boolean) {
    this.modalHeader = active
      ? 'Activate ' + await this.labelPipe.transform('Location') 
      : 'Deactivate ' + await this.labelPipe.transform('Location')

      if(active === false){
        this.modalDescription = `You are selecting to make ` + await this.labelPipe.transform('Location') +` ${this.name} Inactive. If you continue, this ` + await this.labelPipe.transform('Location') +` will be made Inactive and can no longer be assigned to classes.`
      }
      else{
        this.modalDescription = `You are selecting to make ` + await this.labelPipe.transform('Location') +`  ${this.name}  Active. If you continue, this ` + await this.labelPipe.transform('Location') +` will be made Active and will be available for class scheduling.`;
      }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  MakeActive(e: any, active: boolean) {
    

    var idarray :any =[];
    idarray.push(this.id);
    var options = new Location_HistoryCreateOptions();

    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.Notes = data['reason'];
    options.locationIds = idarray;
    if (active) {
      options.ActionType = 'active';
    } else {
      options.ActionType = 'inactive';
    }
    // 
    // options.instructorIds?.push(this.id);
    this.locService
      .makeActiveInactiveOrDelete(options)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Location') + ' Made ' + options.ActionType);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });

        this.isActive = options.ActionType === 'active' ? true : false;
      });
  }
  async getLocationData(id:any){
    await this.locService.get(id).then((res:any)=>{

      this.location = res;
      this.isActive = this.location.active;
      this.isLocCatActive = res.location_Category.active;
    }).finally(()=>{

    });
  }

  refresh(){
    this.populateData();
    this.getLocationData(this.id);
    this.dataBroadcastService.updateMyDataNavBar.next(null);
  }

  Delete(e: any)
  {
    
    var options = new Location_HistoryCreateOptions();
    var idarray :any =[];
    idarray.push(this.id);
    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.Notes = data['reason'];
    options.locationIds = idarray;
      options.ActionType = 'delete';


      this.locService
      .makeActiveInactiveOrDelete(options)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Location') +' deleted successfully');
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });
        this.router.navigate(['my-data/locations/overview'])
        this.isActive = options.ActionType === 'active' ? true : false;
      });

  }
}


