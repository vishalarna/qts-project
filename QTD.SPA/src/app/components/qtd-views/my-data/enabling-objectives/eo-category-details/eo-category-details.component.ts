import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnDestroy, OnInit, Output, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EO_CategoryDeleteOptions } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_CategoryDeleteOptions';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-eo-category-details',
  templateUrl: './eo-category-details.component.html',
  styleUrls: ['./eo-category-details.component.scss']
})
export class EoCategoryDetailsComponent implements OnInit,OnDestroy {
  isActive = true;
  isLoading = false;
  subscriptions = new SubSink();
  catId = "";
  action = "";
  category : EnablingObjective_Category;
  header = "";
  description = "";
  showReason = false;
  hasLinks = false;

  constructor(
    private route : ActivatedRoute,
    private eoCatService : EnablingObjectivesCategoryService,
    private router : Router,
    private alert : SweetAlertService,
    public dialog : MatDialog,
    private dataBroadcastService : DataBroadcastService,
    public flyPanelService : FlyInPanelService,
    private vcf : ViewContainerRef,
  ) { }

  ngOnInit(): void {
    this.subscriptions.sink  = this.route.params.subscribe((res:any)=>{
      this.catId = res.id;
      
      this.getCatData();
    })
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  async getCatData(){
    this.category  = await this.eoCatService.get(this.catId);
    this.hasLinks = await this.eoCatService.checkCatForLinks(this.category.id);
  }

  activeModal(templateRef:any,makeActive : boolean){
    this.header = `Make ${makeActive ? "Active" : "Inactive"}`;
    this.action = `${makeActive ? "Active" : "Inactive"}`;
    this.showReason = true;
    this.description = `You are selecting to make category "${this.category.number} - ${this.category.title}" ${makeActive ? "Active" : "Inactive"}.`
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  deleteModal(templateRef:any){
    this.header = "Delete Category";
    this.description = `You are selecting to delete category, "${this.category.number} - ${this.category.title}".`;
    this.showReason = false;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async getDataWithReason(e:any){
    var data = JSON.parse(e);
    var options = new EO_CategoryDeleteOptions();
    options.actionType = this.action;
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    await this.eoCatService.deleteCat(this.catId,options).then((_)=>{
      this.alert.successToast(`Category Successfully Made ${this.action}`);
      this.getCatData();
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    }).finally(()=>{
    });
  }

  async getData(){
    var options = new EO_CategoryDeleteOptions();
    options.actionType = "delete";
    await this.eoCatService.deleteCat(this.catId,options).then((_)=>{
      this.alert.successToast("Category Successfully Deleted");
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.router.navigate(['my-data/enabling-objectives/overview']);
    }).finally(()=>{

    });
  }

  openFlyPanel(templateRef:any){
    const portal = new TemplatePortal(templateRef,this.vcf);
    this.flyPanelService.open(portal);
  }

}
