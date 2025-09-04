import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, Input, OnInit, Optional, Output } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MatLegacyDialog as MatDialog, MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';
import { QTDDialogueComponent } from 'src/app/components/base/qtd-dialogue/qtd-dialogue.component';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { OrganizationsService } from 'src/app/_Services/QTD/organizations.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-remove-employees-dialogue',
  templateUrl: './remove-employees-dialogue.component.html',
  styleUrls: ['./remove-employees-dialogue.component.scss']
})
export class RemoveEmployeesDialogueComponent implements OnInit {
  constructor(
    @Optional() public mdDialogRef: MatDialogRef<QTDDialogueComponent>,
    public dialog: MatDialog ,
    private alert: SweetAlertService,
    private organizationService : OrganizationsService,
    private employeeService : EmployeesService
  ) {
  }

  datePipe = new DatePipe('en-us');
  @Input() deleteSelectedEmp: any[] = [];
  @Input() header: string;
  @Input() deleteDescription: string;
  @Input() cancelText: string;
  @Input() confirmText: string;
  @Input() defaultValue: string;
  @Input() reasonDescription: string;
  @Input() reasonName: string;
  @Input() oldOrg: any;
  @Input() showEffectiveDateAndReason: boolean = false;
  @Input() filteredList:any[] = [];
  @Input() isEmployeeFullName:boolean = false;
  employeeId:any;
  canceled = new EventEmitter<any>();
 orgamizationId : any;
 empOrgs :any[];
 tempsrc:any[]=[];
  @Output()
  confirmed = new EventEmitter<any>();

  showReasonModal = false;

  effectiveDate =this.datePipe.transform(Date.now(), "yyyy-MM-dd");
  reason!: string;

  ngOnInit(): void {
    
    
    

    this.tempsrc.push('Prabhu SampathKumar')
    this.tempsrc.push('Daniela Petrovic')

    // this.tempsrc.push('Stephniee Laude')
    // this.tempsrc.push('Prabhu SampathKumar')
    // this.tempsrc.push('Prabhu SampathKumar')
    // this.tempsrc.push('Daniela Petrovic')

    // this.tempsrc.push('Stephniee Laude')
    // this.tempsrc.push('Prabhu SampathKumar')
    // if(this.oldOrg != undefined)
    // {
    //   this.filteredList.forEach((element:any) => {
    //     if(element.id === this.oldOrg.id){
    //       this.tempsrc = element.employeeOrganizations;
    //     }
    //   });
    // }
  }

  onConfirm(event: any) {
    if (!this.showEffectiveDateAndReason){
      this.confirmed.emit(true);

    }
    else {
      this.showReasonModal = true;
    }
  }

  onSaveReason(e: any) {
    if (!this.effectiveDate || !this.reason) {
      //this.alert.errorToast('Please provide Effective date and reason');
      //return;
    }
    let data = {
      effectiveDate: this.effectiveDate,
      reason: this.reason,
    };
    this.confirmed.emit(JSON.stringify(data));
    this.dialog.closeAll();
  }

  closeDialog() {
    this.canceled.emit('cancelled clicked');
    this.dialog.closeAll();
  }
}

