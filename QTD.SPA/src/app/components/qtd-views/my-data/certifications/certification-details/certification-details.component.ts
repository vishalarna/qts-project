import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Certification_HistoryCreateOptions } from 'src/app/_DtoModels/Certification_History/Certification_HistoryCreateOptions';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';
import { Certification } from 'src/app/_DtoModels/Certification/Certification';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';



@Component({
  selector: 'app-certification-details',
  templateUrl: './certification-details.component.html',
  styleUrls: ['./certification-details.component.scss']
})
export class CertificationDetailsComponent implements OnInit {
  modalType:"Add" | "Edit" | "Copy"
  datePipe = new DatePipe('en-us');
  isActive: boolean = true;
  subscription = new SubSink();
  issuAuthTitle = "";
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
 EffectiveDate :any;
 name ='';
 modalHeader = '';
 modalDescription = '';

 acronym = '';
 renewaltimeframe = true;
 renewalinterval = '';

creditHrsReq = '';
creditHrs = '';
certSubReq = '';
certSubReqName = '';
certSubReqHours = '';
certMemberNum = '';
certifiedDate = '';
renewalDate = '';
expirationDate = '';
allowRolloverHours = '';
additionalHours = '';
deleteCheck:number;
totalAdditionalHours:number = 0;





certification:Certification;
  constructor( private route:ActivatedRoute,
    public vcf:ViewContainerRef,

    private alert:SweetAlertService,
    private dataBroadcastService:DataBroadcastService,
    public flyPanelService:FlyInPanelService,
    public dialog:MatDialog,
    private router:Router,
    private certService:CertificationService,
    private labelPipe: LabelReplacementPipe) { }

  ngOnInit(): void {
    
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.isLoading = true;
      this.id = res.id;
      this.populateData();

      this.getCertificationData(res.id);
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
       this.certService.get(this.id)
       .then((data: any) => {
         this.isLoading = false;
         this.isActive = data.active;
         this.EffectiveDate = this.datePipe.transform(data.createdDate, 'MM-dd-yyyy');
         this.description = data.certDesc;
         this.name = data.name;


       })

     }
     const portal = new TemplatePortal(templateRef, this.vcf);
     this.flyPanelService.open(portal);
   }

    async deleteProcedure(templateRef:any){
    var transformedValue =await this.transformTitle("Certification");
    this.deleteDescription = `You are selecting to delete ${transformedValue}:  ${this.name}`;
     const dialogRef = this.dialog.open(templateRef, {
       width: '600px',
       height: 'auto',
       hasBackdrop: true,
       disableClose: true,
     });
   }


  async populateData() {
    this.certService.get(this.id)
      .then((data) => {
        this.isLoading = false;
        this.isActive = data.active;
        this.EffectiveDate = this.datePipe.transform(data.effectiveDate, 'MM-dd-yyyy');
        this.name = data.name;

        this.website = data.certDesc;
        this.description = data.certDesc;
        this.acronym = data.certAcronym;
        this.certSubReqName = data.certSubReqName;
        this.certifiedDate = data.certifiedDate;
        this.renewalDate = data.renewalDate;
        this.expirationDate = data.expirationDate;
        this.allowRolloverHours = data.allowRolloverHours;
        this.additionalHours = data.additionalHours;
        this.renewalinterval = data.renewalInterval;
        this.creditHrs = data.creditHrs;
        this.deleteCheck = data.employeeCertifications.length;


      })
      .catch((err: any) => {
        this.isLoading = false;
        var transformedValue =this.transformTitle("Certification");
        this.alert.errorToast(`Error Fetching ${transformedValue} Detail`);
      });

      this.readySubRequirmentData();
  }

  readySubRequirmentData(){
    this.totalAdditionalHours = 0;
   this.certService.getSubRequirement(this.id).then((response=>{
   response.forEach(element => {
    this.totalAdditionalHours += element.reqHour
   });
   }))
  }


  async changeStatus(templateRef: any, active: boolean) {
    var transformedValue = await this.transformTitle("Certification");
    this.modalHeader = active
      ? 'Activate ' + transformedValue
      : 'Deactivate ' + transformedValue;

      if(active === false){
        this.modalDescription = `You are selecting to make ${transformedValue} ${this.name} Inactive. If you continue, this ${transformedValue} will be made Inactive and can not be assigned to ` + await this.labelPipe.transform('Employee') + `s.`;
      }
      else if(active === true){
        this.modalDescription = `You are selecting to make ${transformedValue} ${this.name} Active. If you continue, this ${transformedValue} will be made Active and can be assigned to ` + await this.labelPipe.transform('Employee') + `s.`;
      }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }


  async MakeActive(e: any, active: boolean) {
    

    var idarray :any =[];
    idarray.push(this.id);
    var options = new Certification_HistoryCreateOptions();

    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.Notes = data['reason'];
    options.certIds = idarray;
    if (active) {
      options.ActionType = 'active';
    } else {
      options.ActionType = 'inactive';
    }
    var transformedValue =await this.transformTitle("Certification");
    this.certService
      .makeActiveInactiveOrDelete(options)
      .then((res: any) => {
        this.alert.successToast(transformedValue +' Made ' + options.ActionType);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });

        this.isActive = options.ActionType === 'active' ? true : false;
      });
  }

  async getCertificationData(id:any){
    await this.certService.get(id).then((res:any)=>{

      this.certification = res;
      this.isActive = this.certification.active;
    }).finally(()=>{

    });
  }
  
  getUpdatedCertification(value:any){
    this.certification = value;
  }

  refresh(){
    this.populateData();
    this.dataBroadcastService.updateMyDataNavBar.next(null);
  }


  async Delete(e: any)
  {
    var transformedValue =await this.transformTitle("Certification");
    var options = new Certification_HistoryCreateOptions();
    var idarray :any =[];
    idarray.push(this.id);
    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.Notes = data['reason'];
    options.certIds = idarray;
      options.ActionType = 'delete';


      this.certService
      .makeActiveInactiveOrDelete(options)
      .then((res: any) => {
        this.alert.successToast(transformedValue+' deleted successfully');
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });
        this.router.navigate(['my-data/certifications/overview'])
        this.isActive = options.ActionType === 'active' ? true : false;
      });

  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }  


}
