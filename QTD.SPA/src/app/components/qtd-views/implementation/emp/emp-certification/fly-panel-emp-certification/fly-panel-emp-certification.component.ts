import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Certification } from 'src/app/_DtoModels/Certification/Certification';
import { CertifyingBody } from 'src/app/_DtoModels/CertifyingBody/CertifyingBody';
import { EmployeeCertificateCreateOptions } from 'src/app/_DtoModels/EmployeeCertification/EmployeeCertificateCreateOptions';
import { EmployeeCertificateUpdateOptions } from 'src/app/_DtoModels/EmployeeCertification/EmployeeCertificateUpdateOptions';
import { EmployeeCertification } from 'src/app/_DtoModels/EmployeeCertification/EmployeeCertification';
import { CertifyingBodiesService } from 'src/app/_Services/QTD/certifying-bodies.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-emp-certification',
  templateUrl: './fly-panel-emp-certification.component.html',
  styleUrls: ['./fly-panel-emp-certification.component.scss'],
})
export class FlyPanelEmpCertificationComponent implements OnInit {
  processing = false;
  @Input() mode: string = 'add';
  empId!: any;
  certBodies: CertifyingBody[] = [];
  empCertOpt: EmployeeCertificateCreateOptions =
    new EmployeeCertificateCreateOptions();
  certifications: Certification[] = [];
  @Input() empCertEditData!: EmployeeCertification;
  empCertUpdateOpt: EmployeeCertificateUpdateOptions =
    new EmployeeCertificateUpdateOptions();
  showCertBodyPanel = false;
  form: UntypedFormGroup;

  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private route: Router,
    private certBodyService: CertifyingBodiesService,
    private databroadcastService: DataBroadcastService,
    private empService: EmployeesService,
    private datepipe: DatePipe,
    public flyPanelSrvc: FlyInPanelService
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);

    this.empId = this.route.url.substring(this.route.url.lastIndexOf('/') + 1);
  }

  ConstructForm() {
    this.form = new UntypedFormGroup({
      certBodyId: new UntypedFormControl(
        { value: '', disabled: this.mode == 'edit' },
        Validators.required
      ),
      certNumber: new UntypedFormControl('', Validators.required),
      certificationId: new UntypedFormControl(
        { value: '', disabled: this.mode == 'edit' },
        Validators.required
      ),
      recertDate: new UntypedFormControl(''),
      expireDate: new UntypedFormControl('', Validators.required),
      issueDate: new UntypedFormControl({ value: '', disabled: true }),
    });
  }

  ngOnInit(): void {
    this.ConstructForm();

    this.getCertBodies().then(async (_) => {
      if (this.empCertEditData && this.mode == 'edit') {
        this.form
          .get('certBodyId')
          ?.setValue(this.empCertEditData.certification.certifyingBody.id);

        await this.getCertTypes(
          this.empCertEditData.certification.certifyingBody.id
        );
        this.form
          .get('certificationId')
          ?.setValue(this.empCertEditData.certification.id);

        this.form
          .get('issueDate')
          ?.setValue(
            String(
              this.datepipe.transform(
                this.empCertEditData.issueDate,
                'yyyy-MM-dd'
              )
            )
          );

        this.form
          .get('certNumber')
          ?.setValue(this.empCertEditData.certificationNumber);

        this.form
          .get('expireDate')
          ?.setValue(
            String(
              this.datepipe.transform(
                this.empCertEditData.expirationDate,
                'yyyy-MM-dd'
              )
            )
          );
      }
    });

    this.databroadcastService.refreshListName.subscribe((res) => {
      if (res == 'certBody') this.getCertBodies();
    });
  }

  async getCertBodies() {
    await this.certBodyService.getAll().then((data) => {
      this.certBodies = data;
    });
  }

  async getCertTypes(certBodyId: any) {
    await this.certBodyService.getAllCertifications(certBodyId).then((data) => {
      this.certifications = data;
    });
  }

  async AddEditEmployeeCertification() {
    

    if (this.form.invalid) {
      this.alert.errorAlert(
        'Validations are being bypassed. Please provide valid data'
      );
      return;
    }

    if (this.mode == 'add') {
      this.empCertOpt = {
        certificationId: this.form.get('certificationId')?.value,
        certifyingBodyId: this.form.get('certBodyId')?.value,
        certificationNumber: this.form.get('certNumber')?.value,
        expirationDate: this.form.get('expireDate')?.value,
        issueDate: new Date().toISOString(),
      };

      await this.empService
        .createCertifications(this.empId, this.empCertOpt)
        .then((data) => {
          if (data) {
            this.toggleCertPanel();
            this.alert.successToast(this.translate.instant('L.recAdded'));
          }
        })
        .finally(() => this.databroadcastService.refreshTblName.next('cert'));
    } else {
      this.empCertUpdateOpt = {
        certificationId: this.form.get('certificationId')?.value,

        certificationNumber: this.form.get('certNumber')?.value,
        expirationDate: this.form.get('expireDate')?.value,
      };
      await this.empService
        .updateCertifications(
          this.empCertEditData.id,
          this.empCertUpdateOpt
        )
        .then((data) => {
          if (data) {
            this.toggleCertPanel();
            this.alert.successToast(this.translate.instant('L.recUpdated'));
          }
        })
        .finally(() => this.databroadcastService.refreshTblName.next('cert'));
    }
  }

  toggleCertPanel() {
    this.flyPanelSrvc.close();
  }

  toggleCertBodyPanel() {
    this.showCertBodyPanel = !this.showCertBodyPanel;
  }
}
