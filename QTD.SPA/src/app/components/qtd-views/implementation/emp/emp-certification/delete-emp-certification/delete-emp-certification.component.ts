import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'delete-emp-certification',
  templateUrl: './delete-emp-certification.component.html',
  styleUrls: ['./delete-emp-certification.component.scss'],
})
export class DeleteEmpCertificationComponent implements OnInit {
  empId!: any;
  certificationId!: any;

  cancelText: string ;
  confirmText: string;
  header: string;
  description: string;

  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private empSrvc: EmployeesService,
    private databroadcastSrvc: DataBroadcastService,
    private labelPipe:LabelReplacementPipe,
    private dynamicLabelReplacement:DynamicLabelReplacementPipe
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {
    this.settingDialogue();
  }

  async settingDialogue() {
    
    this.header = await this.dynamicLabelReplacement.transform(this.translate.instant("L.DeleteCertification"));
    this.description = `Are you sure you want to delete this ${await this.transformTitle("Certification")}?`;
    this.cancelText = 'Cancel';
    this.confirmText = 'Delete ' + await this.labelPipe.transform('Employee') + '';
  }

  async deleteEmpCert() {
    await this.empSrvc
      .deleteCertifications(this.empId)
      .then((res) => {
        if (res) {
          this.alert.successToast(this.translate.instant('L.recdelete'));
          this.databroadcastSrvc.refreshTblName.next('cert');
        }
      });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 
}
