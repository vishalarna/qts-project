import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit,OnDestroy,AfterViewInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { ToolCategoryOptions } from 'src/app/_DtoModels/ToolCategory/ToolCategoryOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-tool-details',
  templateUrl: './tool-details.component.html',
  styleUrls: ['./tool-details.component.scss']
})
export class ToolDetailsComponent implements OnInit, OnDestroy, AfterViewInit {
  isActive:boolean=true;
  datePipe = new DatePipe('en-us');
  subscription = new SubSink();
  toolId: any;
  toolData: any | undefined;
  makeCopy:boolean;
  modalDescription: string;
  modalHeader: string;

  constructor(
    public flyPanelService:FlyInPanelService,
    private route: ActivatedRoute,
    private toolService: ToolsService,
    private alert : SweetAlertService,
    private vcf:ViewContainerRef,
    private dataBroadcastService: DataBroadcastService,
    public dialog : MatDialog,
    private router: Router,
    private labelPipe: LabelReplacementPipe,

  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      this.toolId = res.id;
      this.getToolData(this.toolId);
     /*  this.shCatId = res.id;
      this.getSHCatData(res.id); */
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  toolNumber:any;
  toolName:any;
  toolDesc:any;
  toolDate:any;
  toolHyperLink:any;
  async getToolData(id:any){
    await this.toolService.get(id).then((data)=>{
      
      this.toolData =  data;
      this.toolName = data.name;
      this.toolDate = data.effectiveDate;
      this.toolDesc = data.description;
      this.toolHyperLink = data.hyperlink;
      this.toolNumber = data.number;
      this.isActive = data.active;
    }).catch(async (err)=>{
      this.alert.errorToast("Error Fetching " + await this.labelPipe.transform('Tool') + " Data");
    });
  }

  refresh(){
    this.getToolData(this.toolId);
    this.dataBroadcastService.updateMyDataNavBar.next(null);
  }

  async openEditOrCopy(templateRef: any,isCopy:boolean) {
    this.makeCopy = isCopy;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  MakeActive(e: any, active: boolean) {
 
    var options = new ToolCategoryOptions();

    var data = JSON.parse(e);
    if(data.effectiveDate != undefined && data.reason != undefined){
      options.changeEffectiveDate = data.effectiveDate;
      options.changeNotes = data.reason;
    }
 
    if (active) {
      options.actionType = 'active';
    } else {
      options.actionType = 'inactive';
    }
    

    this.toolService
      .makeActiveInactiveOrDelete(this.toolId, options)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Tool') + ' Made ' + options.actionType);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.toolId });
        this.dataBroadcastService.updateProcedureInNavBar.next(null);
        this.isActive = options.actionType === 'active' ? true : false;
      });
  }

  async changeStatus(templateRef: any, active: boolean) {
    this.modalHeader = active
      ? 'Make ' + await this.labelPipe.transform('Tool') + ' Active'
      : 'Make ' + await this.labelPipe.transform('Tool') + ' Inactive';
    if(active === false){
      this.modalDescription = `You are selecting to make ` + await this.labelPipe.transform('Tool') + ` ${this.toolNumber} - ${this.toolName} Inactive?.`;
    }
    else{
      this.modalDescription = `You are selecting to make Category  ${this.toolNumber} - ${this.toolName} Active?.`;
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    }); 
  }

  async deleteTool(templateRef: any) {
    this.modalHeader = 'Delete ' + await this.labelPipe.transform('Tool');
    this.modalDescription = `You are selecting to delete ` + await this.labelPipe.transform('Tool') + ` ${this.toolNumber} - ${this.toolName}`;
    //this.modalName = this.procTitle;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getData(event: any, actionType: string) {
    var options = new ToolCategoryOptions();
    options.actionType = actionType;
    var data = JSON.parse(event);
    options.changeEffectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    
    await this.toolService
      .makeActiveInactiveOrDelete(this.toolId, options)
      .then(async (res: any) => {
        this.alert.successToast(
          await this.labelPipe.transform('Tool') + ' ' + actionType + 'd'
        );
        this.router.navigate(['/my-data/tools/overview']);
        this.dataBroadcastService.updateMyDataNavBar.next(null);
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Deleting ' + await this.labelPipe.transform('Tool'));
      });
  }






}
