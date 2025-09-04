import { DatePipe } from '@angular/common';
import { Component, Inject, Input, TemplateRef, ViewChild } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import {
  MatLegacyDialog as MatDialog,
  MatLegacyDialogRef as MatDialogRef,
  MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA,
} from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';
import { ILAPublishOptions } from 'src/app/_DtoModels/ILA/ILAPublishOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-publish-ila-modal',
  templateUrl: './publish-ila-modal.component.html',
  styleUrls: ['./publish-ila-modal.component.scss'],
})
export class PublishIlaModalComponent {
  publishToAll: boolean = true;
  @Input() ilaId!: any;
  @ViewChild('publishILA') publishILA: TemplateRef<any>;
  datePipe = new DatePipe('en-us');
  publishForm = new UntypedFormGroup({});
  header: string;
  description: string;
  cancelText: string;
  confirmText: string;

  constructor(
    private router: Router,
    private alert: SweetAlertService,
    private dialogSrvc: MatDialog,
    private IlaService: IlaService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.publishForm.addControl("date",new UntypedFormControl(this.datePipe.transform(Date.now(), "yyyy-MM-dd"), Validators.required))
    this.publishForm.addControl("reason",new UntypedFormControl(''));
    this.settingDialogue();
  }

  async settingDialogue() {

    this.header = 'Publish ' + await this.labelPipe.transform('ILA') ;
    this.description = 'You are selecting to publish the ' + await this.labelPipe.transform('ILA') + ' and make it available for ' + await this.labelPipe.transform('Employee') + 's to Complete. When ' + await this.labelPipe.transform('Employee') + 's ' +
      'begin to complete the ' + await this.labelPipe.transform('ILA') + ' you will not be able to make edits to the sequence or Trainee Evaluations. Are you sure ' +
      'you want to continue?';
    this.cancelText = '';
    this.confirmText = 'Yes';
  }

  publishSpinner = false;
  async goToProvidersAndIla() {
    this.publishSpinner = true;
    var options = new ILAPublishOptions();
    options.effectiveDate = this.publishForm.get('date')?.value;
    options.reason = this.publishForm.get('reason')?.value;
    await this.IlaService.publisILA(this.ilaId,options).then(async (_) => {
      this.alert.successToast( await this.labelPipe.transform('ILA') +" Published Successfully");
      this.router.navigate(['/dnd/ila/list']);
      this.dialogSrvc.closeAll();
    }).finally(()=>{
      this.publishSpinner = false;
    })
  }

  togglePublishToAll() {
    this.publishToAll = !this.publishToAll;
  }

  // publish(publishILA: TemplateRef<any>, action: string) {
  //   if (action === 'cancel') {
  //     this.publishILARef.close();
  //   } else {
  //     this.publishILARef.close();
  //     this.dialogSrvc.closeAll();
  //     this.router.navigate(['/dnd/ila/list']);
  //   }
  // }
}
