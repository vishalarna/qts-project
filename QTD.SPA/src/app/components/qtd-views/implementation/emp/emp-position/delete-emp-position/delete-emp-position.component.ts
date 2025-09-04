import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'delete-emp-position',
  templateUrl: './delete-emp-position.component.html',
  styleUrls: ['./delete-emp-position.component.scss'],
})
export class DeleteEmpPositionComponent implements OnInit {
  empId!: any;
  positionId!: any;

  header: string;
  description: string;
  cancelText: string;
  confirmText: string;
  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private databroadcastSrvc: DataBroadcastService,
    private empSrvc: EmployeesService,
    private labelPipe: LabelReplacementPipe,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {
     this.settingDialogue();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async settingDialogue() {
    this.header = await this.dynamicLabelReplacementPipe.transform(this.translate.instant("L.DeletePosition"));
    this.description = 'Are you sure you want to delete this '+ await this.transformTitle('Position')+ '?';
    this.cancelText = 'Cancel';
    this.confirmText = 'Delete' + await this.transformTitle('Employee');
  }

  async deleteEmpPos() {
    
    await this.empSrvc
      .deletePosition(this.empId, this.positionId)
      .then(async (res) => {
        if (res) {
          
          this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.recdelete')));
          this.databroadcastSrvc.refreshTblName.next('pos');
        }
      });
  }
}
