import { Component,Input,OnInit, ViewChild, ViewContainerRef, TemplateRef } from "@angular/core";
import { MatSort } from "@angular/material/sort";
import { MatLegacyTableDataSource as MatTableDataSource } from "@angular/material/legacy-table";
import { EmployeeCertificationHistory } from "@models/EmployeeCertificationHistory/EmployeeCertificationHistory";
import { EmployeesService } from "src/app/_Services/QTD/employees.service";
import { FlyInPanelService } from "src/app/_Shared/services/flyInPanel.service";
import { TemplatePortal } from "@angular/cdk/portal";
import { SweetAlertService } from "src/app/_Shared/services/sweetalert.service";
import { EmployeeCertificationHistoryDeleteOptions } from "@models/EmployeeCertificationHistory/EmployeeCertificationHistoryDeleteOptions";
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { LabelReplacementPipe } from "src/app/_Pipes/label-replacement.pipe";

@Component({
  selector: 'app-fly-panel-view-emp-certification-history',
  templateUrl: './fly-panel-view-emp-certification-history.component.html',
  styleUrls: ['./fly-panel-view-emp-certification-history.component.scss']
})
export class FlyPanelViewEmpCertificationHistoryComponent implements OnInit{
  @Input() employeeId : string;
  @Input() empCertification : any;
  empCertificationHistory:EmployeeCertificationHistory[];
  displayColumns: string[] = ['checkbox','issueDate', 'expirationDate', 'action'];
  empCertHistoryData:MatTableDataSource<EmployeeCertificationHistory> = new MatTableDataSource<EmployeeCertificationHistory>();
  @ViewChild('certificationSort') certificationSort: MatSort;
  spinner:boolean=true;
  employeecertificationHistoryId:any;
  selectedIds: string[] = [];
  deleteDescription:string;
  certificationHistoryIdToDelete:string;
  mode:string = '';
  showHistoryPanel:boolean =false;

  constructor(private employeeService:EmployeesService,
              public flyPanelSrvc: FlyInPanelService, 
              private vcf: ViewContainerRef , 
              public alert: SweetAlertService, 
              public dialog: MatDialog, private labelPipe: LabelReplacementPipe) {
  }


  ngOnInit(): void {
    this.loadAsync();
    this.selectedIds = []; 
  }

  async loadAsync(){
    this.empCertificationHistory = await this.employeeService.getEmpCertificationHistory(this.empCertification.id);
    this.empCertHistoryData.data=this.empCertificationHistory;
    this.spinner=false;
    this.empCertHistoryData.sort=this.certificationSort;
  }

  openDeleteCertificationHistoryDialog(templateRef: any, row: any) {
      this.deleteDescription = `Are you sure you want to delete this Certification History entry?`;
      this.certificationHistoryIdToDelete= row.id;
      const dialogRef = this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
  }
  
  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  openFlyInPanel( mode:string, row:any) {
      this.showHistoryPanel = true
      this.mode = mode;
      this.employeecertificationHistoryId = row.id;
  }

  closeFlypanel(){
    this.loadAsync();
    this.showHistoryPanel=false;
  }

  async deleteCertificationHistory(id: string) {
     await this.employeeService.deleteCertificationsFromHist(id).then(async (res: any) => {
       this.selectedIds = [];
       this.alert.successToast(await this.transformTitle('Employee') + await this.transformTitle('Certification') +' History Deleted Successfully');
        this.loadAsync();
      });
  }

  async bulkDelete() {
    const options: EmployeeCertificationHistoryDeleteOptions = { employeeCertificationHistoryIds: this.selectedIds};
    await this.employeeService.bulkdeleteCertificationsFromHistoryAsync(options).then(async (res: any) => {
      this.selectedIds = [];
      this.alert.successToast(await this.transformTitle('Employee') + await this.transformTitle('Certification') + ' History Deleted Successfully');
      await this.loadAsync()
    });
 }

  toggleSelection(id: string, event: any) {
    if (event.checked) {
      this.selectedIds.push(id);
    } else {
      this.selectedIds = this.selectedIds.filter(x => x !== id);
    }
  }

  toggleSelectAll(event: any) {
    if (event.checked) {
      this.selectedIds = this.empCertHistoryData.data.map(row => row.id);
    } else {
      this.selectedIds = [];
    }
  }
}
