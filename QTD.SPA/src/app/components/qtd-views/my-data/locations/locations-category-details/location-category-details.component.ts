import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Location_Category } from 'src/app/_DtoModels/Location_Category/Location_Category';
import { Location_CategoryHistoryCreateOptions } from 'src/app/_DtoModels/Location_Category/Location_CategoryHistoryCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { LocationCategoryService } from 'src/app/_Services/QTD/location-category.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-location-category-details',
  templateUrl: './location-category-details.component.html',
  styleUrls: ['./location-category-details.component.scss'],
})
export class LocationCategoryDetailsComponent implements OnInit {
  modalType: 'Add' | 'Edit' | 'Copy';
  datePipe = new DatePipe('en-us');
  isActive: boolean = true;
  subscription = new SubSink();
  issuAuthTitle = '';
  revisionNumber = '';
  hyperlink = '';
  effectiveDate: string | null = '';
  deleteDescription = '';
  isCopy = false;
  description = '';
  website = '';
  title = '';
  id = '';
  isLoading = false;
  modalHeader = '';
  modalDescription = '';
  name = '';
  childernCount:boolean;
  location_Category: Location_Category;
  constructor(
    private route: ActivatedRoute,
    public vcf: ViewContainerRef,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    public dialog: MatDialog,
    private router: Router,
    private locCatService: LocationCategoryService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {}
  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.isLoading = true;
      this.id = res.id;
      this.populateData();
      this.getCategoriesData();
    });
  }

  ngOnDestroy(): void {
    //this.subscription.unsubscribe();
  }

  openFlyPanel(templateRef: any, mode: any) {
    this.modalType = mode;
    if (this.modalType === 'Copy') {
      this.isCopy = true;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async deleteProcedure(templateRef: any) {
    this.deleteDescription = `You are selecting to delete ` + await this.labelPipe.transform('Location') + ` Category ${this.title}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  populateData() {
    
    this.locCatService
      .get(this.id)
      .then((data) => {
        this.isLoading = false;
        this.title = data.locCategoryTitle;
        this.isActive = data.active;
        this.description = data.locCategoryDesc;

        this.website = data.locCategoryWebsite;

        this.childernCount = data.locations.length > 0 ? true : false;
      })
      .catch(async (err: any) => {
        this.isLoading = false;
        this.alert.errorToast('Error Fetching ' + await this.labelPipe.transform('Location') +' Category Detail');
      });
  }

  async getCategoriesData() {

    await this.locCatService
      .get(this.id)
      .then((res: any) => {

        this.location_Category = res;
        this.isActive = this.location_Category.active;
      })
      .finally(() => {});
  }
  refresh() {
    this.populateData();
    this.getCategoriesData();
    this.dataBroadcastService.updateMyDataNavBar.next(null);
  }
  async changeStatus(templateRef: any, active: boolean) {
    this.modalHeader = active
      ? 'Activate ' + await this.labelPipe.transform('Location') + ' Category'
      : 'Deactivate ' + await this.labelPipe.transform('Location') + ' Category';
    if(active === false){
      this.modalDescription = `You are selecting to make Category ${this.title} Inactive. If you continue, this Category and all associated ` + await this.labelPipe.transform('Location') +`s will be made Inactive.`;
    }
    else{
      this.modalDescription = `You are selecting to make Category ${this.title} Active. If you continue, this category and all associated ` + await this.labelPipe.transform('Location') +`s will be make Active.`;
    }

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  MakeActive(e: any, active: boolean) {
    

    var options = new Location_CategoryHistoryCreateOptions();

    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.CategoryNotes = data['reason'];
    if (active) {
      options.ActionType = 'active';
    } else {
      options.ActionType = 'inactive';
    }

    this.locCatService
      .makeActiveInactiveOrDelete(this.id, options)
      .then(async (res: any) => {
        this.alert.successToast( await this.labelPipe.transform('Location') + ' Category Made ' + options.ActionType);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });
        this.dataBroadcastService.updateMyDataNavBar.next(null);
        this.isActive = options.ActionType === 'active' ? true : false;
      });
  }

  Delete(e: any) {
    
    var options = new Location_CategoryHistoryCreateOptions();

    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.CategoryNotes = data['reason'];

    options.ActionType = 'delete';

    this.locCatService
      .makeActiveInactiveOrDelete(this.id, options)
      .then(async (res: any) => {
        this.alert.successToast(await this.labelPipe.transform('Location') +' Category ' + options.ActionType);
        this.router.navigate(['my-data/locations/overview']);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });

        this.isActive = options.ActionType === 'active' ? true : false;
      });
  }
}
