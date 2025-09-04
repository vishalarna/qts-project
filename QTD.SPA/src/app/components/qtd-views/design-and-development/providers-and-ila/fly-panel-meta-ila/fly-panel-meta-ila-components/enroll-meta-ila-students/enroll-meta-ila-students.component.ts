import { SelectionModel } from '@angular/cdk/collections';
import {Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MetaILAEmployeesLinkOptions } from 'src/app/_DtoModels/MetaILAEmployeesLink/MetaILAEmployeesLinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { MetaILAService } from 'src/app/_Services/QTD/meta-ila.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import {MatStepper} from "@angular/material/stepper";
import { MetaILAVM } from '@models/MetaILA/MetaILAVM';
import { MetaILAEmployeeVM } from '@models/MetaILAEmployeesLink/MetaILAEmployeeVM';

@Component({
  selector: 'app-enroll-meta-ila-students',
  templateUrl: './enroll-meta-ila-students.component.html',
  styleUrls: ['./enroll-meta-ila-students.component.scss'],
})
export class EnrollMetaILAStudentsComponent implements OnInit, OnDestroy {
  @Input() metaILA: MetaILAVM ;
  @Input() metaILAId: string ;
  @Input() mode: string;
  @Output() openAddEmpFlyPanel = new EventEmitter<any>();
  enrolledEmployees:MatTableDataSource<MetaILAEmployeeVM>=new MatTableDataSource();
  selection = new SelectionModel<MetaILAEmployeeVM>(true, []);
  displayedColumnsEmployees: string[] = ['select','name','position','organization','action'];
  description = "";

  @ViewChild('stepper') stepper: MatStepper;

  constructor( public dialog: MatDialog,
    private metaILAService: MetaILAService,
      private alert: SweetAlertService,
      private labelPipe:LabelReplacementPipe) {}

  ngOnInit(): void {
  this.loadAsync();
  }

  ngOnDestroy(): void {
  }

  async continue(){
    this.stepper.next();
  }

  async loadAsync(){
    if(this.mode=='edit' || this.mode == 'copy'){
      this.metaILAId = this.metaILA.id !== undefined ? this.metaILA.id:this.metaILAId;
      let linkedEmployees = await this.metaILAService.getMetaILAEmployees(this.metaILAId);
      this.metaILA.metaILA_EmployeeVM = linkedEmployees;
    }
    this.enrolledEmployees = new MatTableDataSource(this.metaILA.metaILA_EmployeeVM)
  }

  async removeEMPDialog(templateRef: any) {
    let employee =this.selection.selected[0];
    this.description = `You are selecting to remove ${employee?.fullName} from the Meta  ` + await this.labelPipe.transform('ILA') + `.`
   this.openDialog(templateRef)
  }

  openDialog(templateRef: any){
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async removeMultipleEMPDialogue(templateRef: any) {
    this.description = 'You are selecting to remove the following ' + await this.labelPipe.transform('Employee') + 's from the Meta ' + await this.labelPipe.transform('ILA') + ':';
   this.openDialog(templateRef)
  }

  async removeEMP() {
    var options = new MetaILAEmployeesLinkOptions();
    options.metaILAIDs.push(this.metaILA.id);
    options.employeeIDs=this.selection.selected.map(data=>data.employeeId);
    await this.metaILAService.unlinkMetaILAEmployees(options)
    this.metaILA.metaILA_EmployeeVM = await this.metaILAService.getMetaILAEmployees(this.metaILA.id)
    this.enrolledEmployees = new MatTableDataSource(this.metaILA.metaILA_EmployeeVM)
    this.alert.successToast("Student Enrollement and Related record deleted");
    this.selection.clear();
  }

}
