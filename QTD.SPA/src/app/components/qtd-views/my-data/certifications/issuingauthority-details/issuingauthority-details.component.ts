import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { CertifyingBody } from 'src/app/_DtoModels/CertifyingBody/CertifyingBody';
import { CertifyingBody_HistoryCreateOptions } from 'src/app/_DtoModels/CertifyingBody/CertifyingBody_HistoryCreateOptions';
import { CertifyingBodiesService } from 'src/app/_Services/QTD/certifying-bodies.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-issuingauthority-details',
  templateUrl: './issuingauthority-details.component.html',
  styleUrls: ['./issuingauthority-details.component.scss']
})
export class IssuingauthorityDetailsComponent implements OnInit {
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
  certifyingbody: CertifyingBody;
  certificationCheck:boolean=false;
  disableEmployeeCertificationCheck:boolean=false;
  constructor(
    private route: ActivatedRoute,
    public vcf: ViewContainerRef,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    public dialog: MatDialog,
    private router: Router,
    private certBodyService: CertifyingBodiesService
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


  deleteProcedure(templateRef: any) {
    this.deleteDescription = `You are selecting to delete Issuing Authority:  ${this.title}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  populateData() {
    
    this.certBodyService
      .get(this.id)

      .then((data) => {

        this.isLoading = false;
        this.title = data.name;
        this.description = data.desc;

        this.website = data.website;
      })
      .catch((err: any) => {
        this.isLoading = false;
        this.alert.errorToast('Error Fetching IA Detail');
      });
  }

  async getCategoriesData() {
    await this.certBodyService
      .get(this.id)
      .then((data) => {

        this.certifyingbody = data;
         this.isActive = this.certifyingbody.active;


        if(data.certifications.length > 0){
          this.certificationCheck = true;
        }else{
          this.certificationCheck = false;
        }
      })
      .finally(() => {});

      this.isEmployeeCertification(this.id);
  }

  isEmployeeCertification(id:any){
    this.certBodyService.isEmployeeCertification(id).then((res) => {

      this.disableEmployeeCertificationCheck = res;
    })
  }




  refresh() {
    this.populateData();
    this.getCategoriesData();
    this.dataBroadcastService.updateMyDataNavBar.next(null);
  }

  changeStatus(templateRef: any, active: boolean) {
    this.modalHeader = active
      ? 'Activate'
      : 'Deactivate' + ' Issuing Authority';

    this.modalDescription =active ? `Are you sure you want to make Issuing Authority  ${this.title} Active` : `Are you sure you want to make Issuing Authority  ${this.title} Inactive`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  MakeActive(e: any, active: boolean) {
    

    var options = new CertifyingBody_HistoryCreateOptions();

    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.Notes = data['reason'];
    if (active) {
      options.ActionType = 'active';
    } else {
      options.ActionType = 'inactive';
    }

    this.certBodyService
      .makeActiveInactiveOrDelete(this.id, options)
      .then((res: any) => {
        this.alert.successToast('Issuing Authority Made ' + options.ActionType);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });

        this.isActive = options.ActionType === 'active' ? true : false;
      });
  }

  Delete(e: any) {
    
    var options = new CertifyingBody_HistoryCreateOptions();

    var data = JSON.parse(e);
    options.EffectiveDate = data['effectiveDate'];
    options.Notes = data['reason'];

    options.ActionType = 'delete';

    this.certBodyService
      .makeActiveInactiveOrDelete(this.id, options)
      .then((res: any) => {
        
        if (options.ActionType==='delete') {
        this.alert.successToast("Successfully deleted Issuing Authority.");
        }else{
          this.alert.successToast('Issuing Authority ' + options.ActionType + ' Success.');
        }
        this.router.navigate(['my-data/certifications/overview']);
        this.dataBroadcastService.updateMyDataNavBar.next({ id: this.id });

        this.isActive = options.ActionType === 'active' ? true : false;
      });
  }


}
