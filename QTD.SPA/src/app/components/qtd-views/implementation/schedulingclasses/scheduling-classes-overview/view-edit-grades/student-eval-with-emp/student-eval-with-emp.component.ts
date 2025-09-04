import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ChartConfiguration, ChartData, ChartOptions } from 'chart.js';
import { EvalReleaseOptions } from 'src/app/_DtoModels/StudentEvaluation/EvalReleaseOptions';
import { StudentEvaluationWithEMPVM } from 'src/app/_DtoModels/StudentEvaluation/StudentEvalWithEMPVM';
import { StudentEvaluation } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-student-eval-with-emp',
  templateUrl: './student-eval-with-emp.component.html',
  styleUrls: ['./student-eval-with-emp.component.scss']
})
export class StudentEvalWithEmpComponent implements OnInit, OnDestroy {

  // Pie
  public pieChartOptions: ChartOptions<'pie'> = {
    responsive: false,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: true,
        position: 'right',
      },
    },
  };
  public pieChartLabels = ['Complete', 'Not Completed'];
  public pieChartLegend = true;
  public pieChartPlugins = [];

  public pieChartData: ChartData<'pie'> = {
    labels: this.pieChartLabels,
    datasets: [
      {
        label: 'Title label',
        data: [0, 0],
        backgroundColor: ['rgb(220 245 223)', 'rgb(253 194 194)', '#fff'],
        borderColor: ['rgb(220 245 223)', 'rgb(253 194 194)', '#828886'],
        hoverBackgroundColor: [
          'rgb(220 245 223)',
          'rgb(253 194 194)',
          '#828886',
        ],
        hoverBorderColor: [
          'rgba(0, 160, 0, 1)',
          'rgba(240, 160, 0, 1)',
          'rgba(220, 0, 0, 1)',
        ],
      },
    ],
  };

  // Bar
  public barChartLegend = false;
  public barChartPlugins = [];
  public barChartType = 'horizontalBar';
  public barChartData: ChartConfiguration<'bar'>['data'] = {
    labels: ['4-5 rating', '3-4 rating', '2-3 rating', '0-2 rating', 'Incomplete'],
    datasets: [
      {
        data: [0, 0, 0, 0, 0],
        label: 'Series A',
        backgroundColor: [
          '#5c9b31',
          '#7dac5d',
          '#c1e8a7',
          '#fdc2c2',
          '#7a807e',
        ],
      },
    ],
  };

  public barChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: false,

    indexAxis: 'y',
    scales: {
      x: { display: false },
      y: {
        display: true,
      },
    },
  };

  displayedEvaluationColumns: string[] = [
    'employee',
    'releaseDate',
    'completionDate',
    'action',
  ];
  @Input() classId = "";
  @Input() ilaId = "";
  hideChart = false;
  subscription = new SubSink();
  disableAll = false;
  header = "";
  description = "";
  evalData!:StudentEvaluationWithEMPVM;
  action: 'recalled' | 'released';

  dataSourceEval = new MatTableDataSource<StudentEvaluationWithEMPVM>();
  evalForm = new UntypedFormGroup({});
  evals: StudentEvaluation[] = [];
  rosterData: StudentEvaluationWithEMPVM[] = [];
  selectedEval:StudentEvaluation;
  constructor(
    private trService: TrainingService,
    private dataBroadcastService: DataBroadcastService,
    private alert: SweetAlertService,
    public dialog:MatDialog,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {

    this.subscription.sink = this.dataBroadcastService.refreshRosterData.subscribe((_) => {
      this.emitChange();
    })

    this.evalForm.addControl('eval', new UntypedFormControl(null, Validators.required));
    this.readyEvals();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyEvals() {
    this.evals = await this.trService.getEvaluationsForClass(this.classId);
  }

  async emitChange() {
    var value = this.evalForm.get('eval')?.value;
    this.selectedEval = this.evals.find(x=>x.id==value)
    if (value && value !== '') {
      this.rosterData = await this.trService.getDataForEvaluation(this.classId, value);
      
      this.dataSourceEval.data = this.rosterData;
      this.setChartsData();
    }
  }

  setChartsData() {
    this.hideChart = true;
    var completed = this.rosterData.filter((f) => {
      return f.completedDate !== null;
    }).length;
    var notCompleted = this.rosterData.filter((f) => {
      return f.completedDate == null;
    }).length;

    var data = this.rosterData.map((f) => {
      return f.rating;
    });

    var questions: number[] = [];
    data.forEach((x) => {
      x.forEach((y) => {
        questions.push(y.rating);
      })
    })

    

    var barData: number[] = [0, 0, 0, 0, 0];
    questions?.forEach((data, i) => {
      if (data >= 4 && data <= 5) {
        barData[0] = barData[0] + 1;
      }
      else if (data >= 3 && data < 4) {
        barData[1] = barData[1] + 1
      }
      else if (data >= 2 && data < 3) {
        barData[2] = barData[2] + 1
      }
      else if (data >= 1 && data < 2) {
        barData[3] = barData[3] + 1
      }
      else {
        barData[4] = barData[4] + 1;
      }
    });
    this.pieChartData['datasets'][0]['data'] = Object.assign([completed, notCompleted]);
    this.barChartData['datasets'][0]['data'] = barData;
    this.pieChartData['labels'] = [`${(completed/this.rosterData.length)*100} % Completed`,`${(notCompleted/this.rosterData.length)*100} % Not Completed`]
    this.barChartData['labels'] = ['4-5 rating', '3-4 rating', '2-3 rating', '0-2 rating', `${(notCompleted/this.rosterData.length)*100} % Incomplete`];
    setTimeout(() => {
      this.hideChart = false;
    }, 1)
  }

  async releaseOrRecall() {
    this.disableAll = true;
    var options = new EvalReleaseOptions();
    options.action = this.action;
    options.evalId = this.evalData.evaluationId;
    options.empId = this.evalData.empId;
    options.classId = this.classId;
    await this.trService.releaseOrRecallEvaluation(options).then((_) => {
      this.alert.successToast(`Evaluation ${this.action} Successfully`);
      this.emitChange();
    }).finally(() => {
      this.disableAll = false;
    })
  }

  async openReleaseDialog(templateRef:any,data: StudentEvaluationWithEMPVM){
    this.evalData = data;
    this.action = 'released';
    this.header = "Release Student Evaluation";
    this.description = `You are selecting to release Evaluation for ` + await this.labelPipe.transform('Employee') + ` <b>${this.evalData.empName}</b>.`;
    this.dialog.open(templateRef, {
      width: '680px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    })
  }

  async openRecallDialog(templateRef:any,data: StudentEvaluationWithEMPVM){
    this.evalData = data;
    this.action = 'recalled';
    this.header = "Recall Student Evaluation";
    this.description = `You are selecting to recall Student Eval <b>${this.selectedEval?.title}</b> for ` + await this.labelPipe.transform('Employee') + ` <b>${this.evalData?.empName}</b>.`;
    this.dialog.open(templateRef, {
      width: '680px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    })
  }

}
