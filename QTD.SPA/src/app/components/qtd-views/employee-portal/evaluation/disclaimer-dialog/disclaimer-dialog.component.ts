import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog, MatLegacyDialogRef as MatDialogRef } from '@angular/material/legacy-dialog';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { EmpEvaluationVM } from 'src/app/_DtoModels/EmpEvaluation/EmpEvaluationVM';
import { EmpEvaluationService } from 'src/app/_Services/QTD/Employees/emp-evaluation.service';
import { evalationInformation } from 'src/app/_Statemanagement/action/state.componentcommunication';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-disclaimer-dialog',
  templateUrl: './disclaimer-dialog.component.html',
  styleUrls: ['./disclaimer-dialog.component.scss']
})
export class DisclaimerDialogComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Input() selectedRowData:EmpEvaluationVM;
  dialogRef!: MatDialogRef<unknown, any>;
  disclaimerForm = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');

  constructor(
    public innerDialog: MatDialog,
    private router:Router,
    private evaluationService: EmpEvaluationService,
    private alert: SweetAlertService,
    private store : Store<any>,
  ) { }

  ngOnInit(): void {
    this.disclaimerForm.addControl('tos', new UntypedFormControl(false, Validators.required));
  }

  closeDialog() {
    this.closed.emit();
  }

  async openDialog(templateRef: any) {
    if (this.disclaimerForm.get('tos')?.value === true) {
      this.closeDialog();
      this.store.dispatch(evalationInformation({evalData:this.selectedRowData}));

      await this.evaluationService.startEvaluation(this.selectedRowData.classSchedule_Evaluation_RosterId);
      //have to soft code the id in route url
      this.router.navigate([`/emp/evaluation/${this.selectedRowData.evaluationId}`]);
    }
    else {
      this.dialogRef = this.innerDialog.open(templateRef, {
        width: '680px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }
  }

}
