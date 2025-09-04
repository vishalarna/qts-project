import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { SaftyHazardOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-sh-details',
  templateUrl: './sh-details.component.html',
  styleUrls: ['./sh-details.component.scss']
})
export class ShDetailsComponent implements OnInit,AfterViewInit,OnDestroy {
  isActive: boolean = true;
  subscription = new SubSink();
  shId = "";
  sh : SaftyHazard | undefined;
  dialogTitle = "";
  dialogDesc = "";
  makeCopy = false;
  shTitle='';
  inActive:boolean;
  datePipe = new DatePipe('en-us');
  taskDeleteCheck:boolean;

  constructor(
    private shService:SafetyHazardsService,
    private route : ActivatedRoute,
    public dialog : MatDialog,
    private alert : SweetAlertService,
    private dataBroadcastService : DataBroadcastService,
    private router:Router,
    public flyPanelService:FlyInPanelService,
    private vcf:ViewContainerRef,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      this.shId = res.id;
      this.getSHData();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getSHData(){
    
    await this.shService.get(this.shId).then((res:SaftyHazard)=>{

      this.sh = res;
      this.shTitle =res.number + ' - ' + res.title ;
      this.isActive = this.sh.active;
    }).finally(()=>{

    })
  }

  async toggleActive(e:any){
    
    var options = new SaftyHazardOptions();
    options.saftyHazardIds = [];
    options.saftyHazardIds.push(this.shId);
    options.actionType = this.isActive ? "inactive":"active";
    var data = JSON.parse(e);
    options.changeNotes = data["reason"];
    options.effectiveDate = data["effectiveDate"];
    await this.shService.delete(this.shId,options).then(async (res:any)=>{
      this.isActive = options.actionType === 'active' ? true : false;
      this.alert.successToast(`Successfully made ${await this.labelPipe.transform("Safety Hazard")} ${this.isActive ? "Active":"Inactive"}`);
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    }).finally(()=>{

    });
  }

  async openActiveDialog(templateRef:any){
    this.dialogTitle = (!this.isActive
      ? 'Activate'
      : 'Deactivate' ) + await this.labelPipe.transform("Safety Hazard");

    this.dialogDesc = `You are selecting to make ${await this.labelPipe.transform("Safety Hazard")} "${this.sh?.title}" ${!this.isActive ? "Active": "Inactive"}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async openDeleteDialog(templateRef:any){
    this.dialogTitle = `Delete ${await this.labelPipe.transform("Safety Hazard")}`;

    this.dialogDesc = `You are selecting to delete ${await this.labelPipe.transform("Safety Hazard")} "${this.sh?.title}"`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getDeleteData(e:any){
    var options = new SaftyHazardOptions();
    options.saftyHazardIds = [];
    options.saftyHazardIds.push(this.shId);
    options.actionType = "delete";
    var data = JSON.parse(e);
    options.changeNotes = data["reason"];
    options.effectiveDate = data["effectiveDate"];
    await this.shService.delete(this.shId,options).then(async (res:any)=>{
      this.isActive = !this.isActive;
      this.alert.successToast(`` + await this.labelPipe.transform('Safety Hazard') + ` Deleted Successfully`);
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.router.navigate(['/my-data/safety-hazards/overview']);
    }).finally(()=>{

    });
  }

  async openEditOrCopy(templateRef: any,isCopy:boolean) {
    this.makeCopy = isCopy;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  refreshData(){
    this.sh = undefined;
    this.getSHData();
  }

}
