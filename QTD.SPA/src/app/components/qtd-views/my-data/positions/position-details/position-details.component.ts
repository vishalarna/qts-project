import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { PositionOption } from 'src/app/_DtoModels/Position/PositionOption';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-position-details',
  templateUrl: './position-details.component.html',
  styleUrls: ['./position-details.component.scss']
})
export class PositionDetailsComponent implements OnInit {
  modalType:"Add" | "Edit" | "Copy"
  datePipe = new DatePipe('en-us');
  isActive: boolean = true;
  subscription = new SubSink();
  currentPosition:Position;
  isLoading: boolean = false;

  posTitle = '';
  positionNumber = '';
  positionDescription = '';
  positionAbbreviation = '';
  revisionNumber = '';
  hyperlink = '';
  effectiveDate: any;
  modalDescription = '';
  modalHeader = '';
  modalName = '';
  positionTitle = '';
  isCopy = false;
  id = '';
  taskCount = 0;
  sqCount = 0;
  employeeCount = 0;
  fileName:any;

  deleteDescription = '';
  positionDeleteCheck : boolean;
  constructor( private route:ActivatedRoute,
    public vcf:ViewContainerRef,
    private alert:SweetAlertService,
    private dataBroadcastService:DataBroadcastService,
    public flyPanelService:FlyInPanelService,
    public dialog:MatDialog,
    private router:Router,
    private positionService: PositionsService,
    private labelPipe: LabelReplacementPipe) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.id = res.id;
      this.readyPositionData(res.id);
    });

    this.subscription.sink =
      this.dataBroadcastService.refreshPositionData.subscribe((res: any) => {
        this.readyPositionData(this.id);
      });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  readyPositionData(id:any){
    this.isLoading = true;
    this.positionService.get(id).then((res:Position)=>{


      this.currentPosition = res;
      this.posTitle = res.positionTitle;
      this.positionNumber = res.positionNumber;
      this.positionAbbreviation = res.positionAbbreviation;
      this.positionTitle = res.positionNumber + ' ' + res.positionAbbreviation + ' - ' + res.positionTitle;
      this.positionDescription = res.positionDescription;
      this.isActive = res.active;
      this.hyperlink = res.hyperLink;
      this.effectiveDate =this.datePipe.transform(res.effectiveDate,"MM-dd-yyyy");
      this.revisionNumber = res.revisionNumber;
      this.taskCount = res.position_Tasks.length;
      this.sqCount = res.position_SQs.length;
      this.employeeCount = res.employeePositions.length;
      this.fileName = res.fileName;
      //this.effectiveDate = this.datePipe.transform(res.effectiveDate,"yyyy-MM-dd");
    })
    .catch(async () => {
      this.alert.errorAlert("Error fetching"+  await this.transformTitle('Position') + "data");
    })
    .finally(() => {
      this.isLoading = false;
    })
  }

  copyPosition(templateRef: any, mode:any) {
    this.isCopy = true;
    this.modalType = mode;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  editPosition(templateRef: any, mode:any) {
    this.isCopy = false;
    this.modalType = mode;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  ngOnDestroy(): void {
    //this.subscription.unsubscribe();
  }

 openflyPanel(templateRef:any,mode:any)
 {
    this.modalType = mode;
    this.isCopy = true;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async changeStatus(templateRef: any, active: boolean) {
    this.modalHeader = active
      ? 'Make'+  await this.transformTitle('Position') + 'Active'
      : 'Make' +  await this.transformTitle('Position') + 'Inactive';
    var isActive  = active ? 'Active' : 'Inactive';
    this.modalDescription = `You are selecting to make ` + await this.transformTitle('Position') +`  ${this.positionAbbreviation} . ${this.posTitle} ${isActive}.` ;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async changeActiveStatus(e: any, activate: any) {
    
    var options = new PositionOption();
    options.positionIds = [];
    options.actionType = activate ? 'active' : 'inactive';
    options.positionIds.push(this.id);
    var data = JSON.parse(e);
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    await this.positionService
      .delete(options)
      .then(async (res: any) => {
        this.isActive = activate;
        this.alert.successToast(`${await this.transformTitle('Position')} made ${options.actionType} Successfully`);
        // this.getData(e, options.actionType).then(() => {
        //   this.isActive = activate;
        // });
      })
      .catch(async (err: any) => {
        this.alert.errorToast( `Error Making ` + await this.transformTitle('Position') +` ${options.actionType} ${err}`);
      })
      .finally(() => {
        this.dataBroadcastService.updatePositionInNavBar.next(null);
        this.dataBroadcastService.updatePosition.next(null);
      });
  }

  async deleteProcedure(templateRef: any) {
    this.modalHeader = 'Delete'+ await this.transformTitle('Position');
    this.modalDescription = `You are about to delete ` + await this.transformTitle('Position') +` with title ${this.posTitle}`;
    //this.modalName = this.procTitle;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getData(event: any) {
    var options = new PositionOption();
    options.actionType = "delete";
    var data = JSON.parse(event);
    options.effectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];

    options.positionIds = [];
    options.positionIds.push(this.id);
    await this.positionService
      .delete(options)
      .then(async (res: any) => {
        this.alert.successToast(
           await this.transformTitle('Position') + ' deleted Successfully');
        this.router.navigate(['/my-data/positions/overview']);
        this.dataBroadcastService.updatePositionInNavBar.next(null);
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Deleting ' +  await this.transformTitle('Position'));
      });
  }
}
