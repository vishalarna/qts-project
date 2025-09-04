import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit,TemplateRef,ViewContainerRef} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { DIFSurveyEmployeeLinkUnlinkOptions } from '@models/DIFSurvey/DIFSurveyEmployeeLinkUnlinkOptions';
import { DIFSurveyVM } from '@models/DIFSurvey/DIFSurveyVM';
import { DIFSurvey_EmployeeVM } from '@models/DIFSurvey/DIFSurvey_EmployeeVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiDifSurveyEmployeeService } from 'src/app/_Services/QTD/DifSurveyEmployee/api.difsurvey-employee.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-dif-assign-employees',
  templateUrl: './dif-assign-employees.component.html',
  styleUrls: ['./dif-assign-employees.component.scss'],
})
export class DifAssignEmployeesComponent implements OnInit {
  @Input() inputDifSurveyVM: DIFSurveyVM;
  displayedColumns: string[];
  employeeDataSource: MatTableDataSource<any>;
  selection = new SelectionModel<DIFSurvey_EmployeeVM>(true, []);
  unlinkDescription:string;
  unlinkHeader:string;
  unlinkEmpId:string;
  isSingleUnlink:boolean;

  constructor(
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    public dialog: MatDialog,
    private difSurveyEmpService :ApiDifSurveyEmployeeService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.selection.clear();
    this.displayedColumns = ['checkBox', 'employees', 'position', 'organization','action'];
    this.employeeDataSource= new MatTableDataSource(this.inputDifSurveyVM.employees);
  }
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.employeeDataSource.data.length;
    return numSelected === numRows;
  }
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.employeeDataSource.data.forEach((row) => {
           this.selection.select(row);
        });
  }
  async unlinkItemsModal(templateRef: any, row?:DIFSurvey_EmployeeVM) {
    if(row == null){
      this.isSingleUnlink=false;
      var selectedEmpNames= this.selection.selected.map(s=> s.empFirstName +  " " + s.empLastName);
      this.unlinkHeader= "Unlink " + await this.labelPipe.transform('Employee') + "s"
      this.unlinkDescription = `Are you sure you want to unlink ` + await this.labelPipe.transform('Employee') + `(s) <b>${selectedEmpNames.join(", ")}</b> from this DIF Survey?`;
    }
    else{
      this.isSingleUnlink=true;
      this.unlinkEmpId=row.employeeId;
      this.unlinkHeader="Unlink " + await this.labelPipe.transform('Employee') + "";
      this.unlinkDescription = `Are you sure you want to unlink ` + await this.labelPipe.transform('Employee') + ` <b>${row.empFirstName + " " + row.empLastName}</b> from this DIF Survey?`;
    }
      const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  openFlyInPanelAddEmployees(templateRef: TemplateRef<any>) {
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);
  }
  updateEmployeesList(event:DIFSurvey_EmployeeVM[]){
    this.inputDifSurveyVM.employees=event;
    this.employeeDataSource= new MatTableDataSource(this.inputDifSurveyVM.employees);
    this.selection.clear();
    this.flyPanelService.close();
  }
  async unlinkemployeesAsync(){
    var empIdsToUnlink = this.isSingleUnlink ? [this.unlinkEmpId] : Array.from(new Set(this.selection.selected.map(z=>z.employeeId)));
    var unlinkOptions= new DIFSurveyEmployeeLinkUnlinkOptions();
    unlinkOptions.difSurveyId = this.inputDifSurveyVM?.id;
    unlinkOptions.employeeIds =empIdsToUnlink;
    await this.difSurveyEmpService.unlinkEmployeesAsync(unlinkOptions).then(async res=>{
      if(res?.status == 200){
        this.inputDifSurveyVM.employees = this.inputDifSurveyVM.employees.filter(x=> !empIdsToUnlink.includes(x.employeeId));
        this.employeeDataSource= new MatTableDataSource(this.inputDifSurveyVM.employees);
        this.selection.clear();
        if(this.isSingleUnlink){
          this.alert.successToast(await this.labelPipe.transform('Employee') + " Successfully Removed");
        }else{
          this.alert.successToast(await this.labelPipe.transform('Employee') + "(s) Successfully Removed");
        }
      }
    })
  }
}
