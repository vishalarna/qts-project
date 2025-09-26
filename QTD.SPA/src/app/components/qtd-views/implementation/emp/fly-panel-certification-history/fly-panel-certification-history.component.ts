import { DatePipe } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewContainerRef,
} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { EmployeeLinkCertification } from '@models/Employee/EmployeeLinkCertification';
import { EmployeeCertificationHistory } from '@models/EmployeeCertificationHistory/EmployeeCertificationHistory';
import { EmployeeCertificationHistoryCreateOptions } from '@models/EmployeeCertificationHistory/EmployeeCertificationHistoryCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-certification-history',
  templateUrl: './fly-panel-certification-history.component.html',
  styleUrls: ['./fly-panel-certification-history.component.scss'],
})
export class FlyPanelCertificationHistoryComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() employeeId: string;
  @Input() employeeCertificationHistory: EmployeeCertificationHistory[];
  @Input() employeeCertification: any;
  @Input() mode: any;
  @Input() employeeCertificationHistoryId: any;
  certificationHistoryForm: UntypedFormGroup = new UntypedFormGroup({});
  showSpinner = false;
  datePipe = new DatePipe('en-us');

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private fb: UntypedFormBuilder,
    private employeeService: EmployeesService,
    public alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.intializeCertificationForm();
  }

  intializeCertificationForm() {
    this.certificationHistoryForm = this.fb.group({
      renewalDate: new UntypedFormControl('', Validators.required),
      expirationDate: new UntypedFormControl('', Validators.required),
      addAnother: new UntypedFormControl(false),
    });

    if (this.mode === 'edit' && this.employeeCertificationHistory?.length) {
     const record = this.employeeCertificationHistory.find((x: any) => x.id === this.employeeCertificationHistoryId);
      this.certificationHistoryForm.patchValue({
        renewalDate: this.datePipe.transform(record.issueDate, 'yyyy-MM-dd'),
        expirationDate: this.datePipe.transform( record.expirationDate, 'yyyy-MM-dd'),
      });
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async addCertificationHistory() {
    const options = new EmployeeCertificationHistoryCreateOptions();
    options.renewalDate = this.certificationHistoryForm.get('renewalDate')?.value;
    options.expirationDate = this.certificationHistoryForm.get('expirationDate')?.value;
    options.employeeCertificationId = this.employeeCertification.id;
    options.certificationnumber = this.employeeCertification?.certificationNumber;

   this.employeeService.createCertificationsHistory(options).then(async (res: any) => {
        this.alert.successToast(await this.transformTitle('Employee') + await this.transformTitle('Certification') +' Certification History Created Successfully');
        if (this.certificationHistoryForm.get('addAnother')?.value) {
          this.certificationHistoryForm.reset();
        } else {
          this.closed.emit();
        }
      });
  }

  async UpdateCertification() {
    this.showSpinner = true;
    var options = new EmployeeLinkCertification();
    options.certificationId = this.employeeCertification.certificationId;
    options.issueDate = this.certificationHistoryForm.get('renewalDate')?.value;
    options.renewalDate = this.certificationHistoryForm.get('renewalDate')?.value;
    options.expirationDate = this.certificationHistoryForm.get('expirationDate')?.value;
    options.certificationNumber = this.employeeCertification.certificationNumber;

    await this.employeeService.updateCertificationInHistory(this.employeeCertificationHistoryId, options).then(async (res: any) => {
        this.alert.successToast(await this.transformTitle('Employee') + await this.transformTitle('Certification') +` History Updated Successfully`);
        this.closed.emit();
      })
      .finally(() => {
        this.showSpinner = false;
      });
  }
}
