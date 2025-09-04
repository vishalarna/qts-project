import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { StartPrcedureReviewDialogComponent } from '../start-prcedure-review-dialog/start-prcedure-review-dialog.component';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { MatLegacyTabGroup as MatTabGroup } from '@angular/material/legacy-tabs';
import { LicenseHelperService } from 'src/app/_Shared/services/licenseHelper.service';
import { Router } from '@angular/router';

const ELEMENT_DATA_COMPLETED: any[] = [


  { procedureNo: '1.1', procedureCode: 'Task', procedureName: 'Task Description 1', procedureTitle: 'Test Title', procedureDueDate: '20-24-2023', status: 'Not Started' },
  { procedureNo: '1.1', procedureCode: 'Task', procedureName: 'Task Description 1', procedureTitle: 'Test Title', procedureDueDate: '20-24-2023', status: 'Not Started' },

];
const ELEMENT_DATA_PENDING: any[] = [
  { procedureNo: '1.1', procedureCode: 'Task', procedureName: 'Task Description 1', procedureTitle: 'Test Title', procedureDueDate: '20-24-2023', status: 'Not Started' },
  { procedureNo: '1.1', procedureCode: 'Task', procedureName: 'Task Description 1', procedureTitle: 'Test Title', procedureDueDate: '20-24-2023', status: 'Not Started' },


];
@Component({
  selector: 'app-procedure-review-overview',
  templateUrl: './procedure-review-overview.component.html',
  styleUrls: ['./procedure-review-overview.component.scss']
})
export class ProcedureReviewOverviewComponent implements OnInit {

  url: string = 'Dashboard / Procedure Review';
  completedDataSource = new MatTableDataSource<any>();
  pendingDataSource = new MatTableDataSource<any>();
  isLoading: boolean = false;
  selectedIndex:number = 1;

  CompleteCount = 0;
  PendingCount = 0;
  datePipe = new DatePipe('en-us');
  today=new Date();
  employeeTestList: any[] = [];
  isLicenseValid:boolean=true;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTabGroup) tabGroup!: MatTabGroup;
  @ViewChild('compPage') completedDataPaginator: MatPaginator;
  @ViewChild('pendPage') pendingDataPaginator: MatPaginator;
  displayedCompletedColumns: string[] = [
    'procedureNo',
    'procedureCode',
    'procedureName',
    'procedureTitle',
    'procedureDueDate',
    'action',
  ];
  displayedPendingColumns: string[] = [
    'procedureNo',
    'procedureCode',
    'procedureName',
    'procedureTitle',
    'procedureDueDate',
    'status',
    'action',
  ];
  constructor(public dialog: MatDialog, private empService: EmployeesService,
    private procedureService:ProceduresService,
    private licenseHelper:LicenseHelperService,
    private readonly router: Router,
    ) { }

  ngOnInit(): void {
     this.checkLicense();
     if(this.isLicenseValid){
    this.getTestsByEmployee();
   }
   else{
    this.router.navigate(['emp/dashboard']);
   }
    //this.completedDataSource.data = ELEMENT_DATA_COMPLETED;
    //this.pendingDataSource.data = ELEMENT_DATA_PENDING;

  }
  checkLicense(){
    var license = this.licenseHelper.getLicenseData();
    if(!license?.deluxe){
      this.isLicenseValid = false;
    }
  }
  convertUtcToLocalDate(val : Date) : Date {
    var d = new Date(val); // val is in UTC
    var localOffset = d.getTimezoneOffset() * 60000;
    var localTime = d.getTime() - localOffset;

    d.setTime(localTime);
    return d;
}
  getlocalDateTime(startDate: any) {
    let dateTime = new Date(startDate);
    let localDateTime = new Date(dateTime.getTime() + (dateTime.getTimezoneOffset() * 60 * 1000));
    return localDateTime;
  }
  filterCompletedTest(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.completedDataSource.filter = filter.trim().toLowerCase();
  }
  sortData(sort: Sort) {
    //this.completedDataSource.sort = this.sort;
    this.completedDataSource.sort = this.sort;
    const data = this.completedDataSource.data;
    if (!sort.active || sort.direction === '') {
      this.completedDataSource.data = data;
      return;
    }

    this.completedDataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'procedureNo':
          return this.compare(a.procedureNo, b.procedureNo, isAsc);
        case 'procedureCode':
          return this.compare(a.procedureCode, b.procedureCode, isAsc);
        case 'procedureName':
          return this.compare(a.procedureName, b.procedureName, isAsc);
          case 'procedureTitle':
            return this.compare(a.procedureTitle, b.procedureTitle, isAsc);
            case 'procedureDueDate':
              return this.compare(a.procedureDueDate, b.procedureDueDate, isAsc);

        default:
          return 0;
      }
    });
  }

  sortpendingProcedureReviewsData(sort: Sort)
  {
    this.pendingDataSource.sort = this.sort;
    const data = this.pendingDataSource.data;
    if (!sort.active || sort.direction === '') {
      this.pendingDataSource.data = data;
      return;
    }

    this.pendingDataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'procedureNo':
          return this.compare(a.procedureNo, b.procedureNo, isAsc);
        case 'procedureCode':
          return this.compare(a.procedureCode, b.procedureCode, isAsc);
        case 'procedureName':
          return this.compare(a.procedureName, b.procedureName, isAsc);
          case 'procedureTitle':
            return this.compare(a.procedureTitle, b.procedureTitle, isAsc);
            case 'procedureDueDate':
              return this.compare(a.procedureDueDate, b.procedureDueDate, isAsc);
              case 'status':
                return this.compare(a.status, b.status, isAsc);
        default:
          return 0;
      }
    });
  }
  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  filterPendingTest(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.pendingDataSource.filter = filter.trim().toLowerCase();
  }


  startTest(event) {

    const dialogRef = this.dialog.open(StartPrcedureReviewDialogComponent, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
      data: {
        object: event
      },
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }

  goToLink(link:any){

    window.open(link,'_blank');
  }
    getTestsByEmployee() {

      this.procedureService.getProcedureReviewEmpSide().then((res) => {

        this.isLoading = false;
        this.employeeTestList = res;
        this.pendingDataSource.data = this.employeeTestList.filter(x => x.status !== 'Completed' && this.today >= this.convertUtcToLocalDate(x.startDateTime) && this.today <=this.convertUtcToLocalDate(x.endDateTime));
        this.completedDataSource.data = this.employeeTestList.filter(x => x.status === 'Completed');
        this.CompleteCount=this.completedDataSource.data.length;
        this.PendingCount=this.pendingDataSource.data.length;

        
        setTimeout(() => {
          this.completedDataSource.paginator = this.completedDataPaginator;
          this.pendingDataSource.paginator = this.pendingDataPaginator;
          this.tabGroup.selectedIndex = 1;
        }, 1)
      }).catch((res: any) => {


      })
    }
    showPdf(event) {

      const linkSource = 'data:application/pdf;base64,' + event.file;
      const downloadLink = document.createElement("a");
      const fileName = event.fileName;

      downloadLink.href = linkSource;
      downloadLink.download = fileName;
      downloadLink.click();
    }
}
