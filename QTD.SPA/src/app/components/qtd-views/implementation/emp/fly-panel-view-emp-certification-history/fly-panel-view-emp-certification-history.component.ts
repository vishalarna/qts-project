import { Component,Input,OnInit, ViewChild } from "@angular/core";
import { MatSort } from "@angular/material/sort";
import { MatLegacyTableDataSource as MatTableDataSource } from "@angular/material/legacy-table";
import { EmployeeCertificationHistory } from "@models/EmployeeCertificationHistory/EmployeeCertificationHistory";
import { EmployeesService } from "src/app/_Services/QTD/employees.service";
import { FlyInPanelService } from "src/app/_Shared/services/flyInPanel.service";


@Component({
  selector: 'app-fly-panel-view-emp-certification-history',
  templateUrl: './fly-panel-view-emp-certification-history.component.html',
  styleUrls: ['./fly-panel-view-emp-certification-history.component.scss']
})
export class FlyPanelViewEmpCertificationHistoryComponent implements OnInit{
  @Input() employeeId : string;
  @Input() empCertification : any;
  empCertificationHistory:EmployeeCertificationHistory[];
  displayColumns: string[] = ['issueDate', 'expirationDate'];
  empCertHistoryData:MatTableDataSource<EmployeeCertificationHistory> = new MatTableDataSource<EmployeeCertificationHistory>();
  @ViewChild('certificationSort') certificationSort: MatSort;
  spinner:boolean=true;
  constructor( private employeeService:EmployeesService,public flyPanelSrvc: FlyInPanelService ) {
  }

  ngOnInit(): void {
    this.loadAsync()
  }

  async loadAsync(){
    this.empCertificationHistory = await this.employeeService.getEmpCertificationHistory(this.empCertification.id);
    this.empCertHistoryData.data=this.empCertificationHistory;
    this.spinner=false;
    this.empCertHistoryData.sort=this.certificationSort;
  }

}
