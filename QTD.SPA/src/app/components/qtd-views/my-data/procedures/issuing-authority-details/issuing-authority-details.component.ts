import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { ProcedureOptions } from 'src/app/_DtoModels/Procedure/ProcedureOptions';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { Procedure_IssuingAuthorityOptions } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthorityOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-issuing-authority-details',
  templateUrl: './issuing-authority-details.component.html',
  styleUrls: ['./issuing-authority-details.component.scss'],
})
export class IssuingAuthorityDetailsComponent implements OnInit, OnDestroy {
  isActive: boolean = true;
  currentIssuingAuthority: Procedure_IssuingAuthority;
  subscription = new SubSink();
  description = '';
  website = '';
  title = '';
  id = '';
  reason = '';
  date : any;
  childernCountCheck : boolean = false;
  isCopy = false;
  isLoading = false;

  modalDescription = '';
  modalHeader = '';
  modalReason = '';
  modalDate : any
  iaInactiveCheck: any;
  constructor(
    private route: ActivatedRoute,
    private vcf: ViewContainerRef,
    private issuAuthService: IssuingAuthoritiesService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    private router: Router,
    public dialog: MatDialog,
    private procService:ProceduresService,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {
    
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.isLoading = true;
      this.id = res.id;
      this.populateData(this.id);
    });

    this.subscription.sink =
      this.dataBroadcastService.updateProcIAData.subscribe((res: any) => {
        this.populateData(this.id);
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  populateData(id: any) {
    this.issuAuthService
      .get(id)
      .then((data: Procedure_IssuingAuthority) => {
        this.currentIssuingAuthority = data;
        this.isLoading = false;
        this.title = data.title;
        this.isActive = data.active;
        this.description = data.description;
        this.reason = data.notes;
        this.date = data.effectiveDate;

        this.website = data.website;
        if(data.procedures.length > 0){
          this.childernCountCheck = true;
        }
        else{
          this.childernCountCheck = false;
        }
      })
      .catch((err: any) => {
        this.isLoading = false;
        this.alert.errorToast('Error Fetching Issuing Authority Detail');
      });

      this.readyProcedureReview(id);
  }

  readyProcedureReview(id:any){
    this.procService.IsIssuingAuthorityReleasedToEmp(id).then((res)=>{
      this.iaInactiveCheck = res;
    })
  }


  MakeActive(e: any, active: boolean) {

    var options = new Procedure_IssuingAuthorityOptions();

    var data = JSON.parse(e);
    if(data.effectiveDate != undefined && data.reason != undefined){
      options.changeEffectiveDate = data.effectiveDate;
      options.changeNotes = data.reason;
    }
    else{
      options.changeEffectiveDate = this.date;
      options.changeNotes =this.reason;
    }
    if (active) {
      options.actionType = 'active';
    } else {
      options.actionType = 'inactive';
    }


    this.issuAuthService
      .makeActiveInactiveOrDelete(this.id, options)
      .then((res: any) => {
        this.alert.successToast('Issuing Authority Made ' + options.actionType);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });
        this.dataBroadcastService.updateProcedureInNavBar.next(null);
        this.isActive = options.actionType === 'active' ? true : false;
      });
  }

  editIA(templateRef: any) {
    this.isCopy = false;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  copyIA(templateRef: any) {
    this.isCopy = true;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async deleteIA(templateRef: any) {
    this.modalHeader = 'Delete Issuing Authority';
    this.modalDescription =`You are selecting to delete ` + await this.transformTitle('Procedure') + ` Issuing Authority with title ${this.title}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteProc_IA(event: any, actionType: string) {
    
    var options = new Procedure_IssuingAuthorityOptions();
    options.actionType = actionType;
    var data = JSON.parse(event);
    if(data.effectiveDate != undefined && data.reason != undefined){
      options.changeEffectiveDate = data.effectiveDate;
      options.changeNotes = data.reason;
    }
    else{
      options.changeEffectiveDate = this.date;
      options.changeNotes =this.reason;
    }

    await this.issuAuthService.delete(this.id, options).then(async (res: any) => {
      this.alert.successToast(
        await this.transformTitle('Procedure') + ' Issuing Authority ' + actionType + 'd'
      );
      this.router.navigate(['/my-data/procedures/overview']);
      this.dataBroadcastService.updateProcedureInNavBar.next(null);
    });
  }

  async changeStatus(templateRef: any, active: boolean) {
    this.modalHeader = active
      ? 'Activate'
      : 'Making Issuing Authority Inactive';
    this.modalReason = 'Please provide Effective Date and Reason (if you want to) for this change';
    if(active === false){
      this.modalDescription = `You are about to change Issuing Authority status with title ${this.title}. If you continue, this Issuing Authority and all associated ` + await this.transformTitle('Procedure') + `s will be made Inactive`;
    }
    else{
      this.modalDescription = `You are about to change Issuing Authority status with title ${this.title}.`;
    }
    //this.modalDescription = `Are you sure you want to make ${this.title} Issuing Authority Inactive?` ;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
