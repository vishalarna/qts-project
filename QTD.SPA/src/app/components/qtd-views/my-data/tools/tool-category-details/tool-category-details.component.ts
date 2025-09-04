import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ToolCategory } from 'src/app/_DtoModels/ToolCategory/ToolCategory';
import { ToolCategoryOptions } from 'src/app/_DtoModels/ToolCategory/ToolCategoryOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-tool-category-details',
  templateUrl: './tool-category-details.component.html',
  styleUrls: ['./tool-category-details.component.scss']
})
export class ToolCategoryDetailsComponent implements OnInit, OnDestroy, AfterViewInit{
  subscription = new SubSink();
  isActive:boolean=true;
  toolCategoryId:any;
  toolCategoryData:ToolCategory;
  makeCopy:boolean=false;
  dialogTitle:string;
  dialogDesc:string;
  modalHeader:any;
  modalReason:any;
  modalDescription:any;

  constructor(
    private router:Router,
    private route:ActivatedRoute,
    public flyPanelService:FlyInPanelService,
    private toolService:ToolsService,
    private alert : SweetAlertService,
    public vcf : ViewContainerRef,
    private dataBroadcastService: DataBroadcastService,
    public dialog : MatDialog,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{

      this.toolCategoryId = res.id;
      this.getToolCatData(this.toolCategoryId);
     /*  this.shCatId = res.id;
      this.getSHCatData(res.id); */
    })
  }

  async getToolCatData(id:any){
    await this.toolService.getToolCategoryData(id).then((data)=>{

      this.toolCategoryData = data;
      this.isActive = data.active;
      
    }).catch(async (err)=>{
      this.alert.errorToast("Error Fetching " + await this.labelPipe.transform('Tool') + " Category Data")
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  openEditOrCopy(templateRef:any,isCopy:boolean){
    this.makeCopy = isCopy;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  refresh(){
    this.getToolCatData(this.toolCategoryId);
    this.dataBroadcastService.updateMyDataNavBar.next(null);
  }

  async openDeleteDialog(templateRef:any){
    this.dialogTitle = 'Delete ' + await this.labelPipe.transform('Tool') + ' Category';

    this.dialogDesc = `You are selecting to Delete Category ${this.toolCategoryData.title}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteData(e:any){
    var options = new ToolCategoryOptions();

    var data = JSON.parse(e);
    if(data.effectiveDate != undefined && data.reason != undefined){
      options.changeEffectiveDate = data.effectiveDate;
      options.changeNotes = data.reason;
    }
   /*  else{
      options.changeEffectiveDate = this.date;
      options.changeNotes =this.reason;
    } */
    options.actionType = "delete";

    this.toolService
      .makeActiveInactiveOrDeleteCategory(this.toolCategoryId, options)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Tool') + ' Category Deleted Successfully');
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.toolCategoryId });
        this.dataBroadcastService.updateProcedureInNavBar.next(null);
      });

  }

  MakeActive(e: any, active: boolean) {

    var options = new ToolCategoryOptions();

    var data = JSON.parse(e);
    if(data.effectiveDate != undefined && data.reason != undefined){
      options.changeEffectiveDate = data.effectiveDate;
      options.changeNotes = data.reason;
    }
   /*  else{
      options.changeEffectiveDate = this.date;
      options.changeNotes =this.reason;
    } */
    if (active) {
      options.actionType = 'active';
    } else {
      options.actionType = 'inactive';
    }


    this.toolService
      .makeActiveInactiveOrDeleteCategory(this.toolCategoryId, options)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Tool') + ' Category Made ' + options.actionType);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.toolCategoryId });
        this.dataBroadcastService.updateProcedureInNavBar.next(null);
        this.isActive = options.actionType === 'active' ? true : false;
      });
  }

  changeStatus(templateRef: any, active: boolean) {
    this.modalHeader = active
      ? 'Make Category Active'
      : 'Make Category Inactive';
    if(active === false){
      this.modalDescription = `You are selecting to make Category ${this.toolCategoryData.title} Inactive?.`;
    }
    else{
      this.modalDescription = `You are selecting to make Category ${this.toolCategoryData.title} Active?.`;
    }
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
}
