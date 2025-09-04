import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'app-delete-emp',
  templateUrl: './delete-emp.component.html',
  styleUrls: ['./delete-emp.component.scss'],
})
export class DeleteEmpComponent implements OnInit {
  empId!: any;
  cancelText: string ;
  confirmText: string;
  header: string;
  description: string;
  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private empService: EmployeesService,
    private labelPipe:LabelReplacementPipe,
    private dynamicLabelPipe:DynamicLabelReplacementPipe
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }
  ngOnInit(): void {
    this.settingDialogue();
  }

  async settingDialogue() {
    this.header = await this.dynamicLabelPipe.transform(this.translate.instant("L.DeleteEmployee")) ;
    this.description = await this.dynamicLabelPipe.transform(this.translate.instant("L.DeleteEmpMsg"));
    this.cancelText = 'Cancel';
    this.confirmText = 'Delete ' + await this.labelPipe.transform('Employee') + '';
  }

  async deleteEmp() {
    
    await this.empService.delete(this.empId, 'delete').then(async (res) => {
      this.alert.successToast(await this.labelPipe.transform('Employee') + " Deleted Successfully");
    });
  }
}
