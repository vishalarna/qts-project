import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  UntypedFormGroup,
  UntypedFormBuilder,
  UntypedFormControl,
  Validators,
} from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { InstanceUpdateOptions } from '@models/Instance/InstanceUpdateOptions';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { finalize } from 'rxjs/operators';
import { InstanceCreateOptions } from 'src/app/_DtoModels/Instance/InstanceCreateOptions';
import { InstanceService } from 'src/app/_Services/Auth/instance.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-add-edit-instance',
  templateUrl: './add-edit-instance.component.html',
  styleUrls: ['./add-edit-instance.component.scss'],
})
export class AddEditInstanceComponent implements OnInit {
  Form!: UntypedFormGroup;
  model: InstanceCreateOptions = new InstanceCreateOptions();
  instanceUpdateOptions: InstanceUpdateOptions = new InstanceUpdateOptions();
  processing = false;
  mode: string = 'add';
  editID: any;
  clientName: string = '';

  constructor(
    private translate: TranslateService,
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService,
    private instanceSrvc: InstanceService,
    public dialog: MatDialog
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {
    this.readyForm();

    if (this.editID) {
      this.mode = 'edit';
      this.getClientByName(this.editID);
    }
  }

  readyForm() {
    this.Form = this.fb.group({
      name: new UntypedFormControl('', [Validators.required]),
      createDatabase: new UntypedFormControl(false),
    });
  }

  async addUpdateInstance() {
    if (!this.Form.valid) {
      this.alert.errorAlert('Error', this.translate.instant('L.validErr'));
      return;
    }

    this.processing = true;
    Object.assign(this.model, this.Form.value);
    this.model.clientName = this.clientName;
    if (this.mode == 'add') {
      await this.instanceSrvc
        .create(this.model)
        .then((res) => {
          this.alert.successToast(this.translate.instant('L.recAdded')),
            this.dialog.closeAll();
        })
        .finally(() => {
          this.Form.reset;
          this.processing = false;
        });
    } else {
      this.instanceUpdateOptions = new InstanceUpdateOptions();
      this.instanceUpdateOptions.name = this.model.name;
      this.instanceUpdateOptions.isInBeta  =this.model.isInBeta;
      await this.instanceSrvc
        .update(this.editID, this.instanceUpdateOptions)

        .then((res) => {
          this.alert.successToast(this.translate.instant('L.recUpdated')),
            this.dialog.closeAll();
        })
        .finally(() => {
          this.Form.reset();
          this.processing = false;
        });
    }
  }

  async getClientByName(name: string) {
    await this.instanceSrvc.get(name).then((res) => {
      this.Form.patchValue(res);
    });
  }
}
