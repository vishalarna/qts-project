import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-emp-list',
  templateUrl: './fly-panel-emp-list.component.html',
  styleUrls: ['./fly-panel-emp-list.component.scss']
})
export class FlyPanelEmpListComponent implements OnInit {
  @Input() ListName: any;
  displayedColumns: string[] = [
    'id',
    'image',
    'fullName',
    'employeeNumber'
  ];
  empDataSource: MatTableDataSource<any>;
  spinner: boolean;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    public flyInService: FlyInPanelService,
    private employeesService: EmployeesService,
    private alertService: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.getNames();
    this.getData();
  }

  flyPanelHeading: any;
  async getNames() {
    switch (this.ListName) {
      case 'All':
        this.flyPanelHeading = "Max " + await this.labelPipe.transform('Employee') + " for License Type"
        break;

      case 'Active':
        this.flyPanelHeading = "Active " + await this.labelPipe.transform('Employee') + "s";
        break;

      case 'Inactive':
        this.flyPanelHeading = "Inactive " + await this.labelPipe.transform('Employee') + "";
        break;

      case 'Trainee':
        this.flyPanelHeading = "" + await this.labelPipe.transform('Employee') + "s Marked as Trainees"
        break;

      case 'ExpiredCertificates':
        this.flyPanelHeading = "" + await this.labelPipe.transform('Employee') + "s With Expired certificates"
        break;

      case 'NoCertificates':
        this.flyPanelHeading = "" + await this.labelPipe.transform('Employee') + "s With No certificates"
        break;
    }
  }

  async getData() {

    var list: any;
    this.spinner = true;
    await this.employeesService.getAllEmployeeList(this.ListName).then((data) => {

      this.empDataSource = new MatTableDataSource(data);
    }).catch(async (error) => {
      this.alertService.errorToast('Error getting ' + await this.labelPipe.transform('Employee') + ' List');
    }).finally(() => {
      this.spinner = false;
    });
    setTimeout(() => {
      this.empDataSource.paginator = this.paginator;
      this.empDataSource.sort = this.sort;
    }, 1)

    

  }

}
