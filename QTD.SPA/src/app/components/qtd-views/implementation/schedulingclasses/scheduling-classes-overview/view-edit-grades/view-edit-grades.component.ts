import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { ChartConfiguration, ChartData, ChartOptions } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';
import { DatePipe } from '@angular/common';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ClassScheduleDetailVM } from '@models/SchedulesClassses/ClassScheduleDetailVM';
export interface Grades {
  id: string;
  employee: string;
  releaseDate: string;
  pretestScore: string;
  pretestGrade: string;
  completionDate: string;
  description: string;
  disclaimer: string;
  interrupted: string;
  restarted: string;
  cbtScore: string;
  cbtGrade: string;
}
export interface OverView {
  id: string;
  employee: string;
  preTestStatus: boolean;
  cbtStatus: boolean;
  testStatus: boolean;
  reTakeStatus: boolean;
  finalScore: string;
  finalGrade: string;
  gradeNotes: string;

}
const OVERVIEW_DATA: OverView[] = [
  { cbtStatus: true, employee: 'zeeshan', finalGrade: 'A', finalScore: '100', gradeNotes: 'test', preTestStatus: false, reTakeStatus: false, testStatus: false, id: '1' },
  { cbtStatus: true, employee: 'ALI', finalGrade: 'A', finalScore: '100', gradeNotes: 'test', preTestStatus: false, reTakeStatus: false, testStatus: false, id: '1' }

]
const ELEMENT_DATA: Grades[] = [
  {
    id: '1',
    employee: 'zeeshan',
    cbtGrade: 'A',
    cbtScore: '200',
    completionDate: '04-02-22',
    description: 'test',
    disclaimer: 'yes',
    interrupted: '',
    pretestGrade: 'A',
    pretestScore: '200',
    releaseDate: '07-02-22',
    restarted: 'X',
  },
  {
    id: '2',
    employee: 'Ali',
    cbtGrade: 'A',
    cbtScore: '200',
    completionDate: '04-02-22',
    description: 'test',
    disclaimer: 'yes',
    interrupted: '',
    pretestGrade: 'A',
    pretestScore: '200',
    releaseDate: '07-02-22',
    restarted: 'X',
  },
];

@Component({
  selector: 'app-view-edit-grades',
  templateUrl: './view-edit-grades.component.html',
  styleUrls: ['./view-edit-grades.component.scss'],
})
export class ViewEditGradesComponent implements OnInit, OnDestroy {
  url: string = 'Implementation / Scheduling Classes and Roster';
  dataSourcePreTest = new MatTableDataSource<any>();
  dataSourceOverview = new MatTableDataSource<any>();
  recallDescription: string;
  releaseDescription: string;

  displayOverviewColumns: string[] = [
    'image',
    'employee',
    'preTestStatus',
    'cBTStatus',
    'testStatus',
    'retakeStatus',
    'finalScore',
    'finalGrade',
    'gradeNotes',
    'action',
  ];
  displayedAllColumns: string[] = [
    'image',
    'employee',
    'releaseDate',
    'pretestScore',
    'pretestGrade',
    'completionDate',
    'description',
    'disclaimer',
    'interrupted',
    'restarted',
    'action',
  ];
  displayedPreTestColumns: string[] = [
    'image',
    'employee',
    'releaseDate',
    'pretestScore',
    'pretestGrade',
    'completionDate',
    'description',
    'disclaimer',
    'interrupted',
    'restarted',
    'action',
  ];

  displayedCbtColumns: string[] = [
    'image',
    'employee',
    'releaseDate',
    'cbtScore',
    'cbtGrade',
    'completionDate',
    'action',
  ];
  displayedTestColumns: string[] = [
    'image',
    'employee',
    'releaseDate',
    'testScore',
    'testGrade',
    'completionDate',
    'description',
    'disclaimer',
    'interrupted',
    'restarted',
    'action',
  ];
  displayedReTakeColumns: string[] = [
    'image',
    'employee',
    'releaseDate',
    'retakeScore',
    'retakeGrade',
    'completionDate',
    'description',
    'disclaimer',
    'interrupted',
    'restarted',
    'action',
  ];

  displayedEvaluationColumns: string[] = [
    'image',
    'employee',
    'releaseDate',
    'completionDate',
    'action',
  ];

  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSourcePreTest.paginator = paginator;
  }
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSourcePreTest.sort = sort;
  }

  @ViewChild(MatPaginator) set tblPagingOverView(paginator: MatPaginator) {
    if (paginator) this.dataSourceOverview.paginator = paginator;
  }
  @ViewChild(MatSort) set tblSortOverView(sort: MatSort) {
    if (sort) this.dataSourceOverview.sort = sort;
  }

  classId: any = "";
  selectedTestId: any = "";
  selectedTestType: 'Pretest' | 'Test' | 'Retake' | 'CBT' | 'none' = 'none';
  class!: ClassScheduleDetailVM;
  subscription = new SubSink();
  datePipe = new DatePipe('en-us')

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    public dialog: MatDialog,
    private vcr: ViewContainerRef,
    private router: ActivatedRoute,
    private trService: TrainingService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.dataSourcePreTest.data = ELEMENT_DATA;
    this.dataSourceOverview.data = OVERVIEW_DATA;
    this.subscription.sink = this.router.params.subscribe((res: any) => {
      this.classId = res.instructorId;
      this.readyData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyData() {
    this.class = await this.trService.get(this.classId);
    // this.class.startDateTime = this.changeStartSateForEdit(this.class.startDateTime);
    // this.class.endDateTime = this.changeStartSateForEdit(this.class.endDateTime);


    var startDateString = this.datePipe.transform(
      this.class.startDateTime,
      'yyyy-MM-dd hh:mm a'
    );

    const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
    // Convert UTC date and time to local time
    const localstartDateTimeString = utcStartDateTime.toLocaleString();
    this.class.startDateTime = new Date(Date.parse(localstartDateTimeString));
    var endDateString = this.datePipe.transform(
      this.class.endDateTime,
      'yyyy-MM-dd hh:mm a'
    );
    //const utcendtDateTimeString = res.startDateTime.toDateString();
    const utcendtDateTime = new Date(endDateString.toString() + ' UTC');
    // Convert UTC date and time to local time
    const localendDateTimeString = utcendtDateTime.toLocaleString();
    this.class.endDateTime = new Date(Date.parse(localendDateTimeString));
    
  }

  // changeStartSateForEdit(startDate: any) {
  //   let dateTime = new Date(startDate);
  //   let localDateTime = new Date(dateTime.getTime() + (dateTime.getTimezoneOffset() * 60 * 1000));
  //   return localDateTime;
  // }

  openFlyInPanelEditGrades(templateRef: any, row: any) {
    const portal = new TemplatePortal(templateRef, row);
    this.flyPanelSrvc.open(portal);
  }

  openFlyPanelEditScore(templateRef: any, row: any) {
    const portal = new TemplatePortal(templateRef, row);
    this.flyPanelSrvc.open(portal);
  }

  openFlyInPanelEditRoaster(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  toggleTab(index: any) {
    switch (index) {
      case 0:
        // this.displayedAllColumns = this.displayedEvaluationColumns;
        this.selectedTestType = 'none';
        break;
      case 1:
        this.displayedAllColumns = this.displayedPreTestColumns;
        this.selectedTestType = 'Pretest';
        // this.selectedTestId = this.pretest.preTestForm.get('test')?.value ?? '';
        break;
      case 2:
        this.displayedAllColumns = this.displayedCbtColumns;
        this.selectedTestType = 'CBT';
        // this.selectedTestId = this.cbt.cbtForm.get('test')?.value ?? '';
        break;
      case 3:
        this.displayedAllColumns = this.displayedTestColumns;
        this.selectedTestType = 'Test';
        // this.selectedTestId = this.test.testForm.get('test')?.value ?? '';
        break;
      case 4:
        this.displayedAllColumns = this.displayedReTakeColumns;
        this.selectedTestType = 'Retake';
        // this.selectedTestId = this.retake.retakeForm.get('test')?.value ?? '';
        break;
      case 5:
        this.displayedAllColumns = this.displayedEvaluationColumns;
        this.selectedTestType = 'none';
        break;
    }
  }

  // Pie
  public pieChartOptions: ChartOptions<'pie'> = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: true,
        position: 'right',
      },
    },
  };
  public pieChartLabels = [['Pass'], ['Fail'], 'Not Completed'];
  public pieChartLegend = true;
  public pieChartPlugins = [];

  public pieChartData: ChartData<'pie'> = {
    labels: this.pieChartLabels,
    datasets: [
      {
        label: 'Title label',
        data: [300, 500, 100],
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
    labels: ['90-100%', '80-89%', '70-79%', '0-69%', 'Incomplete'],
    datasets: [
      {
        data: [65, 59, 80, 81, 56],
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

  async recallTest(templateRef: any) {
    this.recallDescription = `You are selecting to Recall Pretest for ` + await this.labelPipe.transform('Employee');
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  async releaseTest(templateRef: any) {
    this.releaseDescription = `You are selecting to Recall Pretest for ` + await this.labelPipe.transform('Employee') ;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  Success(event) { }
  public barChartOptions: ChartConfiguration<'bar'>['options'] = {
    responsive: true,

    indexAxis: 'y',
    scales: {
      x: { display: false },
      y: {
        display: true,
      },
    },
  };

  goBack(){
    history.back();
  }
}
