import {
  Component,
  EventEmitter,
  Inject,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  UntypedFormGroup,
  UntypedFormBuilder,
  UntypedFormControl,
  Validators,
} from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { finalize } from 'rxjs/operators';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { CreateClientOption } from 'src/app/_DtoModels/Client/CreateClientOption';
import { ClientService } from 'src/app/_Services/Auth/client.service';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';

@Component({
  selector: 'app-add-edit-client',
  templateUrl: './add-edit-client.component.html',
  styleUrls: ['./add-edit-client.component.scss'],
})
export class AddEditClientComponent implements OnInit {
  clientForm!: UntypedFormGroup;
  model: CreateClientOption = new CreateClientOption();
  processing = false;
  mode: string = 'add';
  editID: any;

  constructor(
    private translate: TranslateService,
    private fb: UntypedFormBuilder,
    private alert: SweetAlertService,
    private client: ClientService,
    private dialog: MatDialog
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
    this.clientForm = this.fb.group({
      name: new UntypedFormControl('', [Validators.required]),
      createDatabase: new UntypedFormControl(false),
    });
  }

  async addUpdateClient() {
    if (!this.clientForm.valid) {
      this.alert.errorAlert('Error', this.translate.instant('L.validErr'));
      return;
    }

    this.processing = true;
    Object.assign(this.model, this.clientForm.value);

    // adding the client
    if (this.mode == 'add') {
      await this.client
        .createClient(this.model)
        .then((res) =>
          this.alert.successToast(this.translate.instant('L.' + res))
        )
        .finally(() => {
          this.clientForm.reset;
          this.dialog.closeAll();
          this.processing = false;
        });

      //updating the client
    } else {
      await this.client
        .updateClient(this.editID, this.model.name)

        .then((res) =>
          this.alert.successToast(this.translate.instant('L.' + res))
        )
        .finally(() => {
          this.clientForm.reset;
          this.dialog.closeAll();
          this.processing = false;
        });
    }
  }

  getClientByName(name: string) {
    this.client.getClient(name).then((res) => {
      this.clientForm.patchValue(res);
    });
  }
}
