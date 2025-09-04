import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { DutyAreaOptions } from 'src/app/_DtoModels/DutyArea/DutyAreaOptions';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-dutyarea-detail',
  templateUrl: './dutyarea-detail.component.html',
  styleUrls: ['./dutyarea-detail.component.scss'],
})
export class DutyareaDetailComponent implements OnInit {
  da: DutyArea;
  hasLinks = true;
  isLoading = false;
  isActive = true;
  subscription = new SubSink();
  daId = '';
  dialogDesc = '';
  dialogTitle = '';
  makeCopy = false;
  action:any;
  constructor(
    private route: ActivatedRoute,
    public dialog: MatDialog,
    public vcf: ViewContainerRef,
    private alert: SweetAlertService,
    private router: Router,
    public dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    private daSrvc: DutyAreaService
  ) {}

  ngOnInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.daId = res.id;
      this.getDutyArea();
      this.checkForTaskLinks();
    });
  }

  async toggleActive(e: any) {

      this.action = this.isActive ? 'inactive' : 'active';
      var data = JSON.parse(e);
      var options = new DutyAreaOptions();
      options.actionType = this.action;
      options.changeNotes = data['reason'];
      options.effectiveDate = data['effectiveDate'];
      this.daSrvc.changeStatus(this.daId, options).then((res) => {
        this.isActive = !this.isActive;
        this.alert.successToast(`Duty Area ` + options.actionType);
        this.dataBroadcastService.updateMyDataNavBar.next(null);
        this.getDutyArea();
      });

   /*  this.daSrvc.changeStatus(this.daId, options).then((res) => {
      this.isActive = !this.isActive;
      this.alert.successToast(`Duty Area ` + options.actionType);
      this.dataBroadcastService.updateMyDataNavBar.next();
      this.getDutyArea();
    }); */
  }

  async checkForTaskLinks(){
    this.hasLinks = await this.daSrvc.checkDAForTaskLinks(this.daId);
  }

  openActiveDialog(templateRef: any) {
    this.dialogTitle =
      (!this.isActive ? 'Activate' : 'Deactivate') + ' Duty Area';
    this.dialogDesc = `You are making Duty Area "${this.da.letter} ${this.da.number}. ${this.da.title}"  ${!this.isActive ? 'Active' : 'Inactive'}.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteData(e: any) {
    this.action = 'delete';
    var data = JSON.parse(e);
      var options = new DutyAreaOptions();
      options.actionType = this.action;
      options.changeNotes = data['reason'];
      options.effectiveDate = data['effectiveDate'];
      this.daSrvc.changeStatus(this.daId, options).then((res) => {
        this.isActive = !this.isActive;
        this.alert.successToast(`Duty Area ` + options.actionType);
        this.dataBroadcastService.updateMyDataNavBar.next(null);
        this.router.navigate(['/my-data/tasks/overview']);
      });
    /* this.daSrvc.changeStatus(this.daId, options).then((res) => {
      this.isActive = !this.isActive;
      this.alert.successToast(`Duty Area deleted`);
      this.dataBroadcastService.updateMyDataNavBar.next();
      this.router.navigate(['/my-data/tasks/overview']);
    }); */
  }

  openDeleteDialog(templateRef: any) { 
    this.dialogTitle = 'Delete Duty Area';
    this.dialogDesc = `Deleting a Duty Area will delete all related Sub-Duty Areas and Tasks, both Active and Inactive, associated with the selected Duty Area. This action will also remove all Task linkages to other data items, including Courses. Are you sure you want to permanently delete this Duty Area?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

 /*  async getData(){
    var options = new EO_CategoryDeleteOptions();
    options.actionType = "delete";
    await this.eoCatService.deleteCat(this.catId,options).then((_)=>{
      this.alert.successToast("Category Successfully Deleted");
      this.dataBroadcastService.updateMyDataNavBar.next();
      this.router.navigate(['my-data/enabling-objectives/overview']);
    }).finally(()=>{

    });
  } */


  openEditOrCopy(templateRef: any, copy: boolean) {
    this.makeCopy = copy;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  getDutyArea() {
    this.isLoading = true;
    this.daSrvc
      .get(this.daId)
      .then((res) => {
        this.da = res;
        this.isActive = this.da.active;
      })
      .finally(() => (this.isLoading = false));
  }
}
