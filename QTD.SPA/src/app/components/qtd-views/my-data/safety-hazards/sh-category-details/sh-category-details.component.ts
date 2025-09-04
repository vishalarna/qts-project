import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnDestroy, OnInit,AfterViewInit, ViewContainerRef, Output } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { SaftyHazard_Category } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_Category';
import { SaftyHazard_CategoryCompactOptions } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryCompactOptions';
import { SaftyHazard_CategoryOptions } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SafetyHazardCategoryService } from 'src/app/_Services/QTD/safety-hazard-category.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-sh-category-details',
  templateUrl: './sh-category-details.component.html',
  styleUrls: ['./sh-category-details.component.scss']
})
export class ShCategoryDetailsComponent implements OnInit,OnDestroy,AfterViewInit {

  isLoading = false;
  isActive = true;
  shCat:SaftyHazard_Category;
  subscription = new SubSink();
  shCatId = "";
  dialogDesc = "";
  dialogTitle = "";
  makeCopy = false;
  childernCountCheck : boolean = false;

  constructor(
    private SHCatService:SafetyHazardCategoryService,
    private route:ActivatedRoute,
    public dialog : MatDialog,
    public vcf : ViewContainerRef,
    private alert : SweetAlertService,
    private router:Router,
    private dataBroadcastService:DataBroadcastService,
    public flyPanelService:FlyInPanelService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      this.shCatId = res.id;
      this.getSHCatData(res.id);
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getSHCatData(id:any){
    await this.SHCatService.get(id).then((res:SaftyHazard_Category)=>{
      
      this.shCat = res;
      this.isActive = res.active;
      if(res.saftyHazards.length > 0){
        this.childernCountCheck = true;
      }
      else{
        this.childernCountCheck = false;
      }
    }).finally(()=>{

    });
  }

  async toggleActive(e:any){
    var options = new SaftyHazard_CategoryOptions();
    options.saftyHazardCategoryIds = [];
    options.saftyHazardCategoryIds.push(this.shCatId);
    options.actionType = this.isActive ? "inactive":"active";
    var data = JSON.parse(e);
    options.changeNotes = data["reason"];
    options.effectiveDate = data["effectiveDate"];
    await this.SHCatService.delete(options).then(async (res:any)=>{
      this.isActive = !this.isActive;
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.alert.successToast(`Successfully made ${await this.labelPipe.transform("Safety Hazard")} Category ${this.isActive ? "Active":"Inactive"}`);
    }).finally(()=>{

    });
  }

  async openActiveDialog(templateRef:any){
    this.dialogTitle = (!this.isActive
      ? 'Activate'
      : 'Deactivate' ) + await this.labelPipe.transform("Safety Hazard")+' Category';
      if(!this.isActive === false){
        this.dialogDesc = `You are about to change Issuing Authority status with title ${this.shCat.title} Inactive. If you continue, this Issuing Authority and all associated ${await this.labelPipe.transform("Safety Hazard")}s will be made Inactive`;
      }
      else{
        this.dialogDesc = `You are about to change Issuing Authority status with title ${this.shCat.title} Active.`;
      }

  //  this.dialogDesc = `You are selecting to make Safety Hazard Category with title '${this.shCat.title}' ${(!this.isActive? 'Active': 'Inactive' )}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteData(e:any){
    var options = new SaftyHazard_CategoryOptions();
    options.saftyHazardCategoryIds = [];
    options.saftyHazardCategoryIds.push(this.shCatId);
    options.actionType = 'delete';
     var data = JSON.parse(e);
    options.changeNotes = data["reason"];
    options.effectiveDate = data["effectiveDate"];
    await this.SHCatService.delete(options).then(async (res:any)=>{
      this.isActive = !this.isActive;
      this.alert.successToast(`Successfully Deleted ${await this.labelPipe.transform("Safety Hazard")} Category`);
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.router.navigate(['/my-data/safety-hazards/overview']);
    }).finally(()=>{

    });
  }

  async openDeleteDialog(templateRef:any){
    this.dialogTitle = `Delete ${await this.labelPipe.transform("Safety Hazard")} Category`;
      
    this.dialogDesc = `You are selecting to Delete Category ${this.shCat.title}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  refresh(){
    this.getSHCatData(this.shCatId);
    this.dataBroadcastService.updateMyDataNavBar.next(null);
  }

  openEditOrCopy(templateRef:any,copy:boolean){
    this.makeCopy = copy;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

}
