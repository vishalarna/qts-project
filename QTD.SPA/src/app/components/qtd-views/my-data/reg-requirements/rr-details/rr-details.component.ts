import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { RegulatoryRequirement } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirement';
import { RegulatoryRequirementOptions } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementOptions';
import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { RR_IssuingAuthorityOptions } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-rr-details',
  templateUrl: './rr-details.component.html',
  styleUrls: ['./rr-details.component.scss']
})
export class RRDetailsComponent implements OnInit,AfterViewInit,OnDestroy {
  isActive: boolean = true;
  datePipe = new DatePipe('en-us');
  rrId = "";
  subscription = new SubSink();
  dialogDescription = "";
  dialogHeader = "";
  regulationDeleteCheck : boolean;
  rrTaskTitle:any;
  rr = new RegulatoryRequirementsCompact();
  issuingAuthorityCheck:boolean;

  makeCopy = false;

  constructor(
    private router:ActivatedRoute,
    private alert:SweetAlertService,
    private rrService:RegulatoryRequirementService,
    public dialog: MatDialog,
    private dataBroadCastService:DataBroadcastService,
    public route:Router,
    public flyPanelService:FlyInPanelService,
    public vcf:ViewContainerRef,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.router.params.subscribe((res:any)=>{
      this.rrId = res.id;
      this.fetchCompactRRData();
    });

    this.subscription.sink = this.dataBroadCastService.updateRR.subscribe((res:any)=>{
      if(res !== undefined){
        this.fetchCompactRRData();
      }
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async fetchCompactRRData(){
    
    await this.rrService.getCompactData(this.rrId).then((res:RegulatoryRequirementsCompact)=>{
      this.rr = res;
      this.rrTaskTitle = res.title;
      
      this.isActive = res.active;
      if(res.issuingAuthorityCompact?.active === false){
        this.issuingAuthorityCheck = true;
      }
      else{
        this.issuingAuthorityCheck = false;
      }
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching " + await this.labelPipe.transform('Regulatory Requirement') + "s Data " + err?.message);
    });
  }

  async toggleActive(templateRef:any,active:boolean){
    this.dialogHeader = `Make ` + await this.labelPipe.transform('Regulatory Requirement') + ` ${active ? "Active":"Inactive"}`;
    this.dialogDescription = `You are about to make Regulation ${this.rr.title} ${this.isActive ? "Inactive":"Active"}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteData(templateRef:any){
    this.dialogHeader = `Deleting ` + await this.labelPipe.transform('Regulatory Requirement');
    this.dialogDescription = `You are selecting to delete Regulation ${this.rr.title}.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  getDeleteReason(e:any){
    var options = new RegulatoryRequirementOptions();
    options.actionType = 'delete';
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    options.regulatoryRequirementIds =[];
    options.regulatoryRequirementIds.push(this.rrId);
    this.rrService.delete(this.rrId,options).then(async (res:any)=>{
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + ` Deleted`);
      this.isActive = !this.isActive;
      this.dataBroadCastService.updateMyDataNavBar.next(null);
      this.route.navigate(['my-data/reg-requirements/overview']);
    });
  }

  getData(e:any){
    var options = new RegulatoryRequirementOptions();
    options.actionType = this.isActive ? "inactive":"active";
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    options.regulatoryRequirementIds =[];
    options.regulatoryRequirementIds.push(this.rrId);
    this.rrService.delete(this.rrId,options).then(async (res:any)=>{
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + ` is now ${this.isActive ? "Inactive":"Active"}`);
      this.isActive = !this.isActive;
      this.dataBroadCastService.updateMyDataNavBar.next(null);
      this.fetchCompactRRData();
      //this.route.navigate([`/my-data/reg-requirements/rr/${this.rrId}`]);
    });
  }

  copyRR(templateRef:any){
    this.makeCopy = true;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  editRR(templateRef:any){
    this.makeCopy = false;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

}
