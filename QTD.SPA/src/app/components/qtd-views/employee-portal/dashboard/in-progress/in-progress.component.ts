import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { DetailsEventVM } from 'src/app/_DtoModels/Dashboard/DetailsEventVM';
import { GetDueTrainingOptions } from 'src/app/_DtoModels/Dashboard/GetDueTrainingOptions';
import { EmpEvaluationVM } from 'src/app/_DtoModels/EmpEvaluation/EmpEvaluationVM';
import { StartTestDialogComponent } from '../../../implementation/test/start-test-dialog/start-test-dialog.component';
import { StartPrcedureReviewDialogComponent } from '../../procedure-review/start-prcedure-review-dialog/start-prcedure-review-dialog.component';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { DashboardService } from 'src/app/_Services/QTD/Dashboard/dashboard.service';
import { EmpEvaluationService } from 'src/app/_Services/QTD/Employees/emp-evaluation.service';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-in-progress',
  templateUrl: './in-progress.component.html',
  styleUrls: ['./in-progress.component.scss']
})
export class InProgressComponent implements OnInit {

  datePipe = new DatePipe('en-us');
  currentDate = new Date();
  endDate = new Date();
  events!:DetailsEventVM;
  hasILATrainings:boolean = false;
  selectedTemplateRef!:TemplateRef<any>;
  selectedRow!:EmpEvaluationVM;
  ILAId!:any;
  classId!:any;
  constructor(
    private dashboardService : DashboardService,
    public dialog : MatDialog,
    private empEvalService : EmpEvaluationService,
    private empTestService : EmployeesService,
    private procedureService : ProceduresService,
    public flypanelService : FlyInPanelService,
    private vcf : ViewContainerRef,
  ) { }

  ngOnInit(): void {
    this.getEvents();
  }

  async getEvents(){
    this.endDate.setDate(this.currentDate.getDate() + 7);
    var options = new GetDueTrainingOptions();
    options.startDate = this.currentDate;
    options.endDate = this.endDate;
    this.events = await this.dashboardService.getInProgressEvents(this.currentDate.toUTCString(),this.endDate.toUTCString());
    this.hasILATrainings = this.events.ilaList.map((x) =>{
      return x.trainings;
    }).length > 0;
  }

  async start(data:any|null){
    if(data && data.type === 'Student Evaluation'){
      var evals = await this.empEvalService.getAllEvaluations();
      this.selectedRow = evals.filter(x => x.evaluationId === data.id && x.classScheduleId == data.parentId)[0]
      const dialogRef = this.dialog.open(this.selectedTemplateRef, {
        width: '900px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      });
    }
    if(data && data.type === 'Test'){
      var tests:any = await this.empTestService.getTestEmployees();
      var selectedTest = tests.filter((x) => {
        return x.classScheduleId === data.parentId && x.testId === data.id;
      })[0];
      const dialogRef = this.dialog.open(StartTestDialogComponent, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
        data: {
          object: selectedTest
        },
      });
    }
    if(data && data.type === 'Procedure Review'){
      var procs = await this.procedureService.getProcedureReviewEmpSide();
      var selectedProc = procs.filter((x) => {
        return x.procedureReviewId === data.id && x.procedureId === data.parentId;
      })[0];
      const dialogRef = this.dialog.open(StartPrcedureReviewDialogComponent, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
        data: {
          object: selectedProc
        },
      });
    }
  }

  openViewCoursePanel(ilaId:any,classId:any,templateRef:any){
    this.ILAId = ilaId;
    this.classId = classId;
    const portal = new TemplatePortal(templateRef,this.vcf);
    this.flypanelService.open(portal);
  }

}
