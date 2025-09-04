import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { ProcedureOptions } from 'src/app/_DtoModels/Procedure/ProcedureOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-procedure-details',
  templateUrl: './procedure-details.component.html',
  styleUrls: ['./procedure-details.component.scss'],
})
export class ProcedureDetailsComponent
  implements OnInit, OnDestroy, AfterViewInit
{
  datePipe = new DatePipe('en-us');
  isActive: boolean = true;
  subscription = new SubSink();
  currentProcedure: Procedure;
  issuAuthTitle = '';
  procTitle = '';
  revisionNumber = '';
  hyperlink = '';
  effectiveDate: string | null = '';
  modalDescription = '';
  modalHeader = '';
  modalName = '';
  isCopy = false;
  id = '';
  procedureDeleteCheck : boolean;
  procedureFileUpload:any;
  catInactive:any;
  procedureInactiveCheck:boolean=false;

  constructor(
    private route: ActivatedRoute,
    public vcf: ViewContainerRef,
    private procService: ProceduresService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    public dialog: MatDialog,
    private router: Router,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.id = res.id;
      this.readyProcedureData(res.id);
    });

    this.subscription.sink =
      this.dataBroadcastService.refreshProcedureData.subscribe((res: any) => {
        this.readyProcedureData(this.id);
      });
  }

  readyProcedureData(id: any) {
    this.procService.get(id).then((res: Procedure) => {

      this.currentProcedure = res;
      this.issuAuthTitle = res.procedure_IssuingAuthority.title;
      this.procTitle = res.number + '- ' + res.title;
      this.isActive = res.active;
      this.revisionNumber = res.revisionNumber;
      this.hyperlink = res.hyperlink;
      this.catInactive = res.procedure_IssuingAuthority.active;
      this.effectiveDate = this.datePipe.transform(
        res.effectiveDate,
        'MM-dd-yyyy'
      );
      this.procedureFileUpload = res.fileName;
    });

    this.readyProcedureReview(id);
  }

  readyProcedureReview(id:any){
    this.procService.IsProcedureReleasedToEmp(id).then((res)=>{

      this.procedureInactiveCheck = res;
    })
  }

  async changeActiveStatus(e: any, activate: boolean) {
    
    var options = new ProcedureOptions();
    options.procedureIds = [];
    options.actionType = activate ? 'active' : 'inactive';
    options.procedureIds.push(this.id);
    var data = JSON.parse(e);
    options.changeEffectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    /* await this.procService
      .delete(this.id, options)
      .then((res: any) => { */
        this.getData(e, options.actionType).then(() => {
          this.isActive = activate;
        });
        // this.alert.successToast('Procedure Is now ' + options.actionType);

        // this.dataBroadcastService.updateProcedureInNavBar.next();
    /*   }) */
    /*   .catch((err: any) => {
        this.alert.errorToast(
          `Error Making Procedure ${options.actionType} ${err}`
        );
      }); */
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getData(event: any, actionType: string) {
    var options = new ProcedureOptions();
    options.actionType = actionType;
    var data = JSON.parse(event);
    options.changeEffectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];

    options.procedureIds = [];
    options.procedureIds.push(this.id);
    await this.procService
      .delete(this.id, options)
      .then(async (res: any) => {
        this.alert.successToast(
          await this.transformTitle('Procedure') + ' ' + actionType + 'd'
        );
        this.router.navigate(['/my-data/procedures/overview']);
        this.dataBroadcastService.updateProcedureInNavBar.next(null);
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Deleting ' + await this.transformTitle('Procedure') + ' Issuing Authority');
      });
  }

  copyProcedure(templateRef: any) {
    this.isCopy = true;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  editProcedure(templateRef: any) {
    this.isCopy = false;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async deleteProcedure(templateRef: any) {
    this.modalHeader = 'Delete ' + await this.transformTitle('Procedure');
    this.modalDescription = `You are about to delete ` + await this.transformTitle('Procedure') + ` with title ${this.procTitle}`;
    //this.modalName = this.procTitle;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async changeStatus(templateRef: any, active: boolean) {
    this.modalHeader = active ? 'Activate' : 'Deactivate' +  await this.transformTitle('Procedure');
    if (active) {
    this.modalDescription = `Are you sure you want to make ` + await this.transformTitle('Procedure') + ` ${this.procTitle} Active`;
    }else{
    this.modalDescription = `Are you sure you want to make ` + await this.transformTitle('Procedure') + ` ${this.procTitle} Inactive`;
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  HyperlinkClick(link:any){
    this.router.navigate([link]);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
