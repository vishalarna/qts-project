import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { IDP_ReviewCreateOptions } from 'src/app/_DtoModels/IDP/IDP_ReviewCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { IdpService } from 'src/app/_Services/QTD/idp.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-release-idp',
  templateUrl: './fly-panel-release-idp.component.html',
  styleUrls: ['./fly-panel-release-idp.component.scss']
})
export class FlyPanelReleaseIdpComponent implements OnInit {
  @Input() empId = "";
  @Output() refresh = new EventEmitter<any>();
  employee !: Employee;

  releaseIDPForm = new UntypedFormGroup({});
  spinner = false;

  datePipe = new DatePipe('en-us');

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    public dialog: MatDialog,
    private empService: EmployeesService,
    private idpService: IdpService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) { }

  releaseDescription: string = "";

  ngOnInit(): void {
    this.readyForm();
    this.readyEMP();
  }

  readyForm() {
    this.releaseIDPForm.addControl('title', new UntypedFormControl('', Validators.required));
    this.releaseIDPForm.addControl('release', new UntypedFormControl(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'), Validators.required));
    this.releaseIDPForm.addControl('end', new UntypedFormControl(this.datePipe.transform(Date.now(), 'yyyy-MM-dd'), Validators.required));
    this.releaseIDPForm.addControl('instruction', new UntypedFormControl(''));
  }

  async readyEMP() {
    this.employee = await this.empService.get(this.empId);
  }

  closeFlyPanel() {
    this.flyPanelSrvc.close();
  }

  async releaseIDP(templateRef: any) {
    this.releaseDescription = `You are selecting to release an IDP Review to ` + await this.labelPipe.transform('Employee') + ` ${this.employee.person.firstName + ' ' + this.employee.person.lastName} This review is applicable for only for this ` + await this.labelPipe.transform('Employee') + `.`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async Release(e: any) {
    this.spinner = true;
    var options = new IDP_ReviewCreateOptions();
    options.instructions = this.releaseIDPForm.get('instruction')?.value;
    options.endDate = this.releaseIDPForm.get('end')?.value;
    options.releaseDate = this.releaseIDPForm.get('release')?.value;
    options.title = this.releaseIDPForm.get('title')?.value;
    options.employeeId = this.empId;
    await this.idpService.createIDPReview(options).then((res: any) => {
      this.alert.successToast("IDP Review Released");
      this.refresh.emit();
    }).finally(() => {
      this.spinner = false;
    })
  }
}
