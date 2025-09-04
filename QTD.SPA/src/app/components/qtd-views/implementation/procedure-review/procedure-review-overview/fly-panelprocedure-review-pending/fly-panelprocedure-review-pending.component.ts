import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';

@Component({
  selector: 'app-fly-panelprocedure-review-pending',
  templateUrl: './fly-panelprocedure-review-pending.component.html',
  styleUrls: ['./fly-panelprocedure-review-pending.component.scss']
})
export class FlyPanelprocedureReviewPendingComponent implements OnInit {

  showSpinner = false;
  @Output() closed = new EventEmitter<any>();
  searchString = "";
  employeeData!: any[];
  employeeDataCopy!: any[];
  ProcedureReviewDataSource = new MatTableDataSource<any>();
  displayedColumns: string[] = [
    'index',
    'procedure',
    'title',
    'name',
  ];
  @ViewChild('eoPaging') eoPaging: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  constructor(
    private procedureService : ProceduresService,
  ) { }

  ngOnInit(): void {
    this.getEmployee();
  }

  closeProvider() {
    this.closed.emit('fp-add-provider-closed');
  }

  async getEmployee() {
    this.showSpinner = true;
    this.procedureService
      .getProcedureReviewPending()
      .then((res: any) => {
        
        this.ProcedureReviewDataSource.data= this.employeeData = res;
      })
      .catch((err) => {
        console.error(err);
      })
      .finally(() => {
        this.showSpinner = false;
      });
      setTimeout(()=>{
        this.ProcedureReviewDataSource.paginator = this.eoPaging; 
      this.ProcedureReviewDataSource.sort = this.sort   },1)
  }
  filterEmployees() {
    
    this.ProcedureReviewDataSource.data = this.employeeData.filter((emp) => {
      return ((emp.procedureReview.procedure.title).trim().toLowerCase().includes(this.searchString.trim().toLowerCase())
          || emp.procedureReview.procedure.number.trim().toLowerCase().includes(this.searchString.trim().toLowerCase()))
          || emp.procedureReview.procedureReviewTitle.trim().toLowerCase().includes(this.searchString.trim().toLowerCase())
    })
  }

}
