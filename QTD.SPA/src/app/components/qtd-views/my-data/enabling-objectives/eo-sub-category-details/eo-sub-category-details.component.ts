import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnDestroy, OnInit,ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { EO_CategoryDeleteOptions } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_CategoryDeleteOptions';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-eo-sub-category-details',
  templateUrl: './eo-sub-category-details.component.html',
  styleUrls: ['./eo-sub-category-details.component.scss']
})
export class EoSubCategoryDetailsComponent implements OnInit, OnDestroy {
  isActive = true;
  isLoading = false;
  subscription = new SubSink();
  subCatId = "";
  prevNumbers = "";

  header = "";
  description = "";
  showReason = false;
  action = "";
  hasLinks = false;

  subCategory: EnablingObjective_SubCategory;
  constructor(
    private route: ActivatedRoute,
    private eoCatService: EnablingObjectivesCategoryService,
    public dialog : MatDialog,
    private dataBroadcastService : DataBroadcastService,
    private router : Router,
    private alert : SweetAlertService,
    public flyPanelService : FlyInPanelService,
    private vcf : ViewContainerRef,
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      var myRoute = String(res.id).split('-');
      this.subCatId = myRoute[1];
      this.prevNumbers = myRoute[0];
      this.getSubCatData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getSubCatData(){
    this.subCategory = await this.eoCatService.getSubCategory(this.subCatId);
    this.hasLinks = await this.eoCatService.checkSubCatForLinks(this.subCategory.id);
  }

  deleteModal(templateRef:any){
    this.header = "Delete Sub Category";
    this.description = `You are selecting to delete Sub Category, "${this.prevNumbers}${this.subCategory.number} - ${this.subCategory.title}".`;
    this.showReason = false;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  activeModal(templateRef:any,makeActive : boolean){
    this.header = `Make ${makeActive ? "Active" : "Inactive"}`;
    this.action = `${makeActive ? "Active" : "Inactive"}`;
    this.showReason = true;
    this.description = `You are selecting to make Sub Category "${this.prevNumbers}${this.subCategory.number} - ${this.subCategory.title}" ${makeActive ? "Active" : "Inactive"}.`
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
    await this.eoCatService.deleteSubCat(this.subCatId,options).then((_)=>{
      this.alert.successToast(`Sub Category Successfully Made ${this.action}`);
      this.getSubCatData();
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    }).finally(()=>{
    });
  }

  async getData(){
    var options = new EO_CategoryDeleteOptions();
    options.actionType = "delete";
    await this.eoCatService.deleteSubCat(this.subCatId,options).then((_)=>{
      this.alert.successToast("Sub Category Successfully Deleted");
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
