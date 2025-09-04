import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { RRIssuingAuthority } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthority';
import { RR_IssuingAuthorityOptions } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-rr-issuing-authority-details',
  templateUrl: './rr-issuing-authority-details.component.html',
  styleUrls: ['./rr-issuing-authority-details.component.scss']
})
export class RRIssuingAuthorityDetailsComponent implements OnInit, OnDestroy, AfterViewInit {
  isActive: boolean = true;
  rrIAId = "";
  rrIA = new RRIssuingAuthority();
  subscription = new SubSink();
  dialogDescription = "";
  dialogHeader = "";
  makeCopy = false;
  childernCountCheck : boolean = false;
  constructor(
    private router: ActivatedRoute,
    private rrIAService: RRIssuingAuthorityService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private route: Router,
    private dataBroadCastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    public vcf: ViewContainerRef,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.router.params.subscribe((res: any) => {
      this.rrIAId = res.id;
      this.fetchRRIAData();
      this.fetchRRwithIAData();
    });

    this.subscription.sink = this.dataBroadCastService.updateRRIA.subscribe((res: RRIssuingAuthority) => {
      if (res !== undefined) {
        this.rrIA = res;
        this.isActive = res.active;
      }
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async fetchRRIAData() {
    await this.rrIAService.get(this.rrIAId).then((res: RRIssuingAuthority) => {
      this.rrIA = res;
      this.isActive = res.active;
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching " + await this.labelPipe.transform('Regulatory Requirement') + " Issuing Authority Data " + err?.message);
    });
  }

  async fetchRRwithIAData(){
    await this.rrIAService.GetRRWithIA().then((res)=>{
      res.forEach((i)=>{
        if(i.id === this.rrIAId){
          if(i.regulatoryRequirementCompacts.length > 0){
            this.childernCountCheck = true;
          }
          else{
            this.childernCountCheck=false;
          }

        }
      })
    }).catch(async (err)=>{
      this.alert.errorAlert("Error fetch " + await this.labelPipe.transform('Regulatory Requirement') + " data" + err?.message);
    });
  }

  async toggleActive(templateRef: any, active: boolean) {
    this.dialogHeader = `Make ` + await this.labelPipe.transform('Regulatory Requirement') + ` Issuing Authority ${active ? "Active" : "Inactive"}`;
    if(active === false){
      this.dialogDescription = `You are about to change Issuing Authority status with title ${this.rrIA.title} Inactive. If you continue, this Issuing Authority and all associated Regulations will be made Inactive`;
    }
    else{
      this.dialogDescription = `You are about to change Issuing Authority status with title ${this.rrIA.title} Active.`;
    }
    //this.dialogDescription = `You are selecting to make Regulatory Requirement Issuing Authority with title '${this.rrIA.title}' ${this.isActive ? "Inactive" : "Active"}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  getData(e: any) {
    var options = new RR_IssuingAuthorityOptions();
    options.actionType = this.isActive ? "inactive" : "active";
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    this.rrIAService.delete(this.rrIAId, options).then(async (res: any) => {
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + ` Issuing Authority Made ${this.isActive ? "Inactive" : "Active"}`);
      this.dataBroadCastService.updateMyDataNavBar.next(null);
      this.isActive = !this.isActive;
    }).catch(async (err: any) => {
      this.alert.errorToast(`Error Making ` + await this.labelPipe.transform('Regulatory Requirement') + ` Issuing Authority ${this.isActive ? "Inactive" : "Active"} ${err}`)
    })
  }

  async deleteRRIA(templateRef: any) {
    this.dialogHeader = "Deleting " + await this.labelPipe.transform('Regulatory Requirement') + " Issuing Authority."
    this.dialogDescription = `You are selecting to delete Issuing Authority, ${this.rrIA.title}.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  getDeleteHistory(e: any) {
    var options = new RR_IssuingAuthorityOptions();
    options.actionType = "delete";
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    this.rrIAService.delete(this.rrIAId, options).then(async (res: any) => {
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + ` Issuing Authority Deleted`);
      this.isActive = !this.isActive;
      this.dataBroadCastService.updateMyDataNavBar.next(null);
      this.route.navigate(['my-data/reg-requirements/overview']);
    })
  }

  editRRIA(templateRef: any) {
    this.makeCopy = false;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  copyRRIA(templateRef: any) {
    this.makeCopy = true;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
}
