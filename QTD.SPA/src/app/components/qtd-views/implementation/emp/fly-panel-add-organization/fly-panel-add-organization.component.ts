import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-add-organization',
  templateUrl: './fly-panel-add-organization.component.html',
  styleUrls: ['./fly-panel-add-organization.component.scss']
})
export class FlyPanelAddOrganizationComponent implements OnInit
 {
  @Input() oldOrg: any;
  @Input() isCopy: any;
  @Input() mode : "Add" | "Edit" | "Copy" = "Add"
  isEdit = false;
  showSpinner = false;
  AddAnotherposition: boolean = false;
  dateError = false;
  defCategory: any;
  addOrgForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  psoitionList : any;
  modalHeader:any;
  modalDescription:any;
  isChecked:boolean;

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Output() confirmed = new EventEmitter<any>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService,
    private orgService: OrganizationsService,
    public dialog: MatDialog,
    //private proc_issueAuthService: IssuingAuthoritiesService,
    //private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
  ) { }

  ngOnInit(): void {
    // if (this.oldIssuingAuthority !== undefined && !this.isCopy) {
    //   this.isEdit = true;
    //   this.readyIssuingAuthorityFormWithData();
    // }
    // else if (this.oldIssuingAuthority && this.isCopy) {
    //   this.isEdit = false;
    //   this.readyIssuingAuthorityFormWithData();
    // }
    // else {

    if(this.mode == "Add")
    {
      this.isEdit = false;
      this.readyorgForm();
    }
    else
    {
      if(this.oldOrg !== undefined )
      {
        this.readyorgFormWithData();
      }
      //this.orgService.get()
    }


    // }
    this.dataBroadcastService.publicClassEnabled.subscribe((x) => {
      this.isChecked = x;
    })
  }

  ngAfterViewInit(): void {

  }

  closedefinition() {
    // this.flyPanelSrvc.close();
    this.closed.emit('IA_Proc closed');
  }


  dateChanged(event: any) {
    if (event >= formatDate(Date.now(), 'yyyy-MM-dd', 'en')) {
      this.dateError = false;
    }
    else {
      this.dateError = true;
    }
  }

  readyorgForm() {
    this.addOrgForm = this.fb.group({
      orgName : new UntypedFormControl('', Validators.required),
      publicOrganisation : new UntypedFormControl(false)


    });
  }
  async createNewOrganization()
  {

    let orgName = this.addOrgForm.get("orgName")?.value;
    let publicOrg = this.addOrgForm.get('publicOrganisation')?.value;
    await this.orgService.create({ name: orgName, publicOrganization: publicOrg}).then((res) => {
      if (res)
      {
        this.closed.emit('fp-add-org-closed');
        this.alert.successToast(`Successfull Add Organization`);
      }
    });
  }
  readyorgFormWithData() {
    this.addOrgForm = this.fb.group({
      orgName : this.oldOrg.name,
      publicOrganisation: this.oldOrg.publicOrganization
    });
  }
  updateOrganization()
  {
    let orgName = this.addOrgForm.get("orgName")?.value;
    let publicOrg = this.addOrgForm.get("publicOrganisation")?.value;
     this.orgService.update(this.oldOrg.id,{ name: orgName, publicOrganization:publicOrg}).then((res) => {

      this.closed.emit('fp-add-org-closed');
      this.refresh.emit();
      this.alert.successToast(`Successfully Update Organization`);
  });}

  EditModal(templateRef:any){
    this.modalHeader = 'Update Organization';
    this.modalDescription = `You are selecting to update the Organization ${this.oldOrg.name}, doing this will update the Organization throughout the database`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
}

