import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { Employee } from './../../../../../_DtoModels/Employee/Employee';
import { OrganizationsService } from './../../../../../_Services/QTD/organizations.service';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Optional, Output } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { QTDDialogueComponent } from 'src/app/components/base/qtd-dialogue/qtd-dialogue.component';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-delete-emp-dialogue',
  templateUrl: './delete-emp-dialogue.component.html',
  styleUrls: ['./delete-emp-dialogue.component.scss']
})
export class DeleteEmpDialogueComponent implements OnInit {
  constructor(
    @Optional() public mdDialogRef: MatDialogRef<QTDDialogueComponent>,
    public dialog: MatDialog /*   @Inject(MAT_DIALOG_DATA) public dialogData: any */,
    private alert: SweetAlertService,
    private organizationService : OrganizationsService,
    private employeeService : EmployeesService
  ) {}

  datePipe = new DatePipe('en-us');
  @Input() header: string;
  @Input() description: string;
  @Input() cancelText: string;
  @Input() confirmText: string;
  @Input() defaultValue: string;
  @Input() reasonDescription: string;
  @Input() reasonName: string;
  @Input() oldOrg: any;
  @Input() showEffectiveDateAndReason: boolean = false;
  @Input() filteredList:any;
  employeeId:any;
  canceled = new EventEmitter<any>();
 orgamizationId : any;
 empOrgs :any[];
 tempsrc:any[]=[];
 orgManagerArray : any[]=[];
  @Output()
  confirmed = new EventEmitter<any>();

  showReasonModal = false;

  effectiveDate =this.datePipe.transform(Date.now(), "yyyy-MM-dd");
  reason!: string;

  ngOnInit(): void {
    
    
    

    if(this.oldOrg != undefined)
    {
      this.oldOrg.employeeOrganizations.forEach((element:any) => {
        var fullName =  element.employee.person.lastName + "-"+ element.employee.person.firstName;
        this.tempsrc.push(fullName);
        if(element.isManager === true)
        {
           this.orgManagerArray.push(fullName);
        }

      });


    }
  }

  onConfirm(event: any) {
    if (!this.showEffectiveDateAndReason){
      this.organizationService.delete(this.oldOrg.id).then((res)=>{
        this.alert.successToast('Organization Deleted Successfully');

       // this.employeeService.deleteOrganization()
      }).catch((err)=>{
        this.alert.errorToast('Error Deleting oOrganization');
      })
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

