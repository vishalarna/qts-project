import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-definition-category-details',
  templateUrl: './definition-category-details.component.html',
  styleUrls: ['./definition-category-details.component.scss']
})
export class DefinitionCategoryDetailsComponent implements OnInit {
  modalType: "Add" | "Edit" | "Copy"
  datePipe = new DatePipe('en-us');
  isActive: boolean = true;
  subscription = new SubSink();
  currentProcedure: Procedure;
  issuAuthTitle = "";
  procTitle = "";
  revisionNumber = "";
  hyperlink = "";
  effectiveDate: string | null = "";
  deleteDescription = "";
  isCopy = false;
  id = "";
  constructor(private route: ActivatedRoute,
    public vcf: ViewContainerRef,
    private procService: ProceduresService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    public dialog: MatDialog,
    private router: Router,
    private labelPipe: LabelReplacementPipe) { }

  ngOnInit(): void {
  }
  ngAfterViewInit(): void {
    // this.subscription.sink = this.route.params.subscribe((res:any)=>{
    //   this.id = res.id;
    //   this.readyProcedureData(res.id);
    // });

    // this.subscription.sink = this.dataBroadcastService.refreshProcedureData.subscribe((res:any)=>{
    //   this.readyProcedureData(this.id);
    // });
  }

  // readyProcedureData(id:any){
  //   this.procService.get(id).then((res:Procedure)=>{
  //     this.currentProcedure = res;
  //     this.issuAuthTitle = res.procedure_IssuingAuthority.title;
  //     this.procTitle = res.title;
  //     this.isActive = res.active;
  //     this.revisionNumber = res.revisionNumber;
  //     this.hyperlink = res.hyperlink;
  //     this.effectiveDate = this.datePipe.transform(res.effectiveDate,"yyyy-MM-dd");
  //   })
  // }

  // async changeActiveStatus(activate:boolean){
  //   var options = new ProcedureOptions();
  //   options.procedureIds = [];
  //   options.actionType = activate ? "active" : "inactive";
  //   options.procedureIds.push(this.id);

  //   await this.procService.delete(this.id,options).then((res:any)=>{
  //     this.alert.successToast("Procedure Is now " + options.actionType);
  //     this.isActive = activate;
  //     this.dataBroadcastService.updateProcedureInNavBar.next();
  //   }).catch((err:any)=>{
  //     this.alert.errorToast(`Error Making Procedure ${options.actionType} ${err}`);
  //   })

  // }

  ngOnDestroy(): void {
    //this.subscription.unsubscribe();
  }

  // getData(event:any){
  //   var options = new ProcedureOptions();
  //   options.actionType = "delete";
  //   var data = JSON.parse(event);
  //   options.changeEffectiveDate = data['effectiveDate'];
  //   options.changeNotes = data['reason'];
  //   options.procedureIds = [];
  //   options.procedureIds.push(this.id);
  //   this.procService.delete(this.id, options).then((res: any) => {
  //     this.alert.successToast("Procedure Issuing Authority Deleted");
  //     this.router.navigate(['/my-data/procedures/overview']);
  //     this.dataBroadcastService.updateProcedureInNavBar.next();
  //   }).catch((err: any) => {
  //     this.alert.errorToast("Error Deleting Procedure Issuing Authority");
  //   })
  // }

  openFlyPanel(templateRef: any, mode: any) {
    this.isCopy = true;
    this.modalType = mode
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  // editProcedure(templateRef:any){
  //   this.isCopy = false;
  //   const portal = new TemplatePortal(templateRef, this.vcf);
  //   this.flyPanelService.open(portal);
  // }

  async deleteProcedure(templateRef: any) {
    this.deleteDescription = `You are selecting to delete ` + await this.labelPipe.transform('Instructor') + ` Category 1. Technical Training`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

}

