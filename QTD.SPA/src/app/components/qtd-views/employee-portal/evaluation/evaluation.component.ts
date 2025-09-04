import { DatePipe } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatLegacyTab as MatTab, MatLegacyTabGroup as MatTabGroup } from '@angular/material/legacy-tabs';
import { Store } from '@ngrx/store';
import { EmpEvaluationVM } from 'src/app/_DtoModels/EmpEvaluation/EmpEvaluationVM';
import { EmpEvaluationService } from 'src/app/_Services/QTD/Employees/emp-evaluation.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-evaluation',
  templateUrl: './evaluation.component.html',
  styleUrls: ['./evaluation.component.scss']
})
export class EvaluationComponent implements OnInit {

  url = "Dashboard / Evaluation";
  selected: 'C'|'P' = 'C';
  displayedColumns = ["number","ilaTitle","instructorName","locationName","classStartDate","dueDate","status","action"];
  displayedColumnsCompleted = ["number","title","instructor","location","classtime","completionDate","status"];
  pendingSource = new MatTableDataSource<any>();
  completedSource = new MatTableDataSource<any>();
  datePipe = new DatePipe('en-us');
  ilaId = "";
  pendingCount = 0;
  completedCount = 0;
  selectedRow:EmpEvaluationVM;
  selectedIndex = 1;

  @ViewChild('pendingSort',{static:false}) pendingSort!:MatSort;
  @ViewChild('completedSort',{static:false}) completedSort!:MatSort;
  @ViewChild('pendingPaging',{static:false}) pendingPaging!:MatPaginator;
  @ViewChild('completedPaging',{static:false}) completedPaging!:MatPaginator;

  constructor(
    public dialog : MatDialog,
    private store: Store,
    private evaluationService: EmpEvaluationService

  ) { }

  ngOnInit(): void {
    this.store.dispatch(sideBarOpen());
    this.readyData();
  }

  async readyData(){
    var result=await this.evaluationService.getAllEvaluations();

    this.pendingSource.data =result.filter((x)=>x.isCompleted===false && new Date(x.dueDate) > new Date());
    this.completedSource.data =result.filter((x)=>x.isCompleted===true);

    this.pendingCount=this.pendingSource.data.length;
    this.completedCount=this.completedSource.data.length;

    this.pendingSource.sort = this.pendingSort;
    this.pendingSource.paginator = this.pendingPaging;

    this.completedSource.sort = this.completedSort;
    this.completedSource.paginator = this.completedPaging;
  }

  openDialog(templateRef:any,ilaId:any){
    this.ilaId = ilaId;
    const dialogRef = this.dialog.open(templateRef, {
      width: '900px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  filterCompleted(event:any){
    this.completedSource.filter = String(event.target.value).trim().toLowerCase();
  }

  filterPending(event:any){
    this.pendingSource.filter = String(event.target.value).trim().toLowerCase();
  }

   //to convert ISO 8601 to local time
//    changeStartSateForEdit(startDate:any){
//     let dateTime = new Date(startDate);
//     let localDateTime = new Date(dateTime.getTime() + (dateTime.getTimezoneOffset() * 60 * 1000));
//     return localDateTime;
// }

// changeEndSateForEdit(startDate:any){
//   let dateTime = new Date(startDate);
//   let localDateTime = new Date(dateTime.getTime() + (dateTime.getTimezoneOffset() * 60 * 1000));
//   return localDateTime;
// }
covertUtcToLocalTime(datetime:any) : Date
{

  var startDateString = this.datePipe.transform(
    datetime,
    'yyyy-MM-dd hh:mm a'
  );
  const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
  // Convert UTC date and time to local time
  const localstartDateTimeString = utcStartDateTime.toLocaleString();
  var newdatetime = new Date(Date.parse(localstartDateTimeString));
  return newdatetime;
}

}
