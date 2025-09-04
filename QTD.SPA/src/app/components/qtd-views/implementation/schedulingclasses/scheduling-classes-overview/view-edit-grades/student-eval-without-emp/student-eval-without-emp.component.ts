import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { ClassSchedules } from 'src/app/_DtoModels/SchedulesClassses/ClassSchedules';
import { ScheduleEvalVM } from 'src/app/_DtoModels/SchedulesClassses/ScheduleEvalVM';
import { StudentEvaluation } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-student-eval-without-emp',
  templateUrl: './student-eval-without-emp.component.html',
  styleUrls: ['./student-eval-without-emp.component.scss']
})
export class StudentEvalWithoutEmpComponent implements OnInit, OnDestroy {
  @Input() ilaId = "";
  @Input() ilaName = "";
  @Input() ilaNumber = "";
  @Input() location = "";
  @Input() classId = "";
  dataSource = new MatTableDataSource<ScheduleEvalVM>();
  displayedColumns = ["ila", "class", "eval", "completed", "action"];
  selectedData!: ScheduleEvalVM;
  studentEvals!:ScheduleEvalVM[];
  subscription = new SubSink();

  constructor(
    public dialog: MatDialog,
    private router : Router,
    private trService : TrainingService,
    private dataBroadcastService : DataBroadcastService,
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshRosterData.subscribe((_)=>{
      this.readyData();
    })
    this.readyData();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyData() {
    this.studentEvals = await this.trService.getLinkedStudentEvalsWithCompletedInfo(this.ilaId,this.classId);
    
    this.dataSource.data = this.studentEvals;
  }

  openEvalDialog(templateRef: any, data: any) {
    this.selectedData = data;
    const dialogRef = this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  description = "";
  event!:any;
  evalSelected(event: any,templateRef:any) {
    this.event = event;
    switch(event.trim().toLowerCase()){
      case 'aggregate':
        if(this.selectedData.completed && !this.selectedData.hasAggregateData){
          this.description = `There is already individual student evaluation data entered for this class. You are selecting to delete the individual data entered from the database and enter aggregate student evaluation data instead. Would you like to continue?`
          const dialogRef = this.dialog.open(templateRef, {
            width: '800px',
            height: 'auto',
            hasBackdrop: true,
            disableClose: true,
          });
        }
        else{
          this.router.navigate([`implementation/sc/eval/${this.selectedData.id}/type/${event}/class/${this.classId}/employee/${this.selectedData.empId}`]);
        }
        break;
      case 'individual':
        
        if(this.selectedData.completed && this.selectedData.hasAggregateData){
          this.description = `There is already aggregate student evaluation data entered for this class. You are selecting to delete the aggregate data entered from the database and enter individual student evaluation data instead. Would you like to continue?`
          const dialogRef = this.dialog.open(templateRef, {
            width: '800px',
            height: 'auto',
            hasBackdrop: true,
            disableClose: true,
          });
        }
        else{
          this.router.navigate([`implementation/sc/eval/${this.selectedData.id}/type/${event}/class/${this.classId}/employee/${this.selectedData.empId}`]);
        }
        break;
    }
    //this.router.navigate([`implementation/sc/eval/${this.selectedData.id}/type/${event}/class/${this.classId}/employee/${this.selectedData.empId}`]);
  }

  redirectToEval(){
    this.router.navigate([`implementation/sc/eval/${this.selectedData.id}/type/${this.event}/class/${this.classId}/employee/${this.selectedData.empId}`]);
  }

}
