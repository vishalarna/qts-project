import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { SubdutyArea } from 'src/app/_DtoModels/SubdutyArea/SubdutyArea';
import { SubdutyAreaOptions } from 'src/app/_DtoModels/SubdutyArea/SubdutyAreaOptions';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-sub-dutyarea-detail',
  templateUrl: './sub-dutyarea-detail.component.html',
  styleUrls: ['./sub-dutyarea-detail.component.scss'],
})
export class SubDutyareaDetailComponent implements OnInit {
  sda: SubdutyArea;
  letterAndNum: any;
  isLoading = false;
  isActive = true;
  subscription = new SubSink();
  sdaId = '';
  dialogDesc = '';
  dialogTitle = '';
  makeCopy = false;
  hasLinks = true;
  dutyAreaDisable:boolean;
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
      this.sdaId = String(res.id).split('-')[0];
      this.letterAndNum = String(res.id).split('-')[1].replace('_', ' ');
      this.getSubDutyArea();
      this.checkSDAforTaskLinks();
    });
  }

  async checkSDAforTaskLinks(){
    this.hasLinks = await this.daSrvc.checkSDAForTaskLinks(this.sdaId);
  }

  async toggleActive(e: any) {

    this.action =  this.isActive ? 'inactive' : 'active';
    var data = JSON.parse(e);
      var options = new SubdutyAreaOptions();
      options.actionType = this.action;
      options.changeNotes = data['reason'];
      options.changeEffectiveDate = data['effectiveDate'];
    this.daSrvc.changeSubdutyAreaStatus(this.sdaId, options).then((res) => {
      this.isActive = !this.isActive;
      this.alert.successToast(`SubDuty Area ` + options.actionType);
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.getSubDutyArea();
    });
  }

  openActiveDialog(templateRef: any) {
    this.dialogTitle =
      (!this.isActive ? 'Activate' : 'Deactivate') + ' SubDuty Area';
    
    this.dialogDesc = `You are about to change SubDuty Area status with title ${this.sda.title}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async deleteData(e: any) {
    this.action =  'delete';
    var data = JSON.parse(e);
      var options = new SubdutyAreaOptions();
      options.actionType = this.action;
      options.changeNotes = data['reason'];
      options.changeEffectiveDate = data['effectiveDate'];

    this.daSrvc.changeSubdutyAreaStatus(this.sdaId, options).then((res) => {
      this.isActive = !this.isActive;
      this.alert.successToast(`SubDuty Area deleted`);
      this.dataBroadcastService.updateMyDataNavBar.next(null);
      this.router.navigate(['/my-data/tasks/overview']);
    });
  }

  openDeleteDialog(templateRef: any) {
    this.dialogTitle = 'Delete SubDuty Area';
    
    this.dialogDesc = `Deleting a Sub-Duty Area will delete all related Tasks, both Active and Inactive, associated with the selected Sub-Duty Area. This action will also remove all Task linkages to other data items, including Courses. Are you sure you want to permanently delete this Sub-Duty Area?`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  openEditOrCopy(templateRef: any, copy: boolean) {
    this.makeCopy = copy;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  getSubDutyArea() {
    this.isLoading = true;
    this.daSrvc
      .getSubDutyArea(this.sdaId)
      .then((res) => {
        this.sda = res;
        this.isActive = this.sda.active;
        this.dutyAreaDisable = this.sda.dutyArea.active;
      })
      .finally(() => (this.isLoading = false));
  }
}
