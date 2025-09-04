import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { ChartConfiguration, ChartData, ChartOptions } from 'chart.js';
import { ClassRoasterUpdateOptions } from 'src/app/_DtoModels/SchedulesClassses/Rosters/ClassRosterUpdateOptions';
import { RosterFetchOptions } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RosterFetchOptions';
import { RosterTestVM } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RosterTestVM';
import { RoastersModel } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RostersModel';
import { Test } from 'src/app/_DtoModels/Test/Test';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import { RostersService } from 'src/app/_Services/QTD/rosters.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-roster-pretest',
  templateUrl: './roster-pretest.component.html',
  styleUrls: ['./roster-pretest.component.scss']
})
export class RosterPretestComponent implements OnInit, OnDestroy {

  option: ClassRoasterUpdateOptions;
  recallDescription: string;
  releaseDescription: string;
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
  passCount: number = 0;
  failCount: number = 0;
  notCompCount: number = 0;


  public pieChartLabels = [[this.passCount + '% Pass'], [this.failCount + '% Fail'], this.notCompCount + '% Not Completed'];
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

  dataSourcePreTest = new MatTableDataSource<any>();

  displayedAllColumns: string[] = [
    'employee',
    'releaseDate',
    'pretestScore',
    'pretestGrade',
    'completionDate',
    'disclaimer',
    'interrupted',
    'restarted',
    'actions'
  ];

  displayedNonEMPColumns:string[] = [
    'employee',
    'releaseDate',
    'pretestScore',
    'pretestGrade',
    'completionDate',
  ]

  preTestForm = new UntypedFormGroup({});

  openFlyInPanelEditGrades(templateRef: any, row: any) {
    const portal = new TemplatePortal(templateRef, row);
    this.flyPanelSrvc.open(portal);
  }

  subscription = new SubSink();

  @Input() ilaId = "";
  @Input() classId = "";
  @Input() testType = "";

  @Output() selectedTestId = new EventEmitter<any>();

  datePipe = new DatePipe('en-us');

  pretests!: RosterTestVM[];
  hideChart = false;
  editType: 'score' | 'grade' | 'compDate' | 'none' = 'none';
  compDate!:any;
  editId: any = "";
  spinner = false;
  gradeValue = "";

  tblGradeError = true;

  valueToSave!: any;

  rosterData: RoastersModel[] = [];
  isSaving = false;
  license!:any;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private testService: TestsService,
    private router: ActivatedRoute,
    private rosterService: RostersService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private dataBroadcastService: DataBroadcastService,
    private clientSettingService : ApiClientSettingsService,
    private labelPipe:LabelReplacementPipe
  ) {
    this.preTestForm.addControl('test', new UntypedFormControl('', Validators.required));
  }

  ngOnInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshRosterData.subscribe((_) => {
      this.emitChange();
    })
    this.readyPretests();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyPretests() {
    
    this.editId = '';
    this.editType = 'none';
    this.valueToSave = "";
    this.gradeValue = "";
    this.tblGradeError = true;
    this.pretests = await this.testService.getSpecificTests(this.classId, 'Pretest');
    
    this.license = await this.clientSettingService.GetCurrentLicenseAsync();
    
  }

  setChartsData() {
    this.hideChart = true;
    var pass = this.rosterData.filter((rost: RoastersModel) => {
      return rost.completedDate !== null && (rost.score && rost.score > 50);
    }).length;
    this.passCount = pass;
    var fail = this.rosterData.filter((rost: RoastersModel) => {
      return rost.completedDate !== null && (rost.score !== null && rost.score <= 50);
    }).length;
    this.failCount = fail;
    var notCompleted = this.rosterData.filter((rost: RoastersModel) => {
      return rost.completedDate === null;
    }).length;
    this.notCompCount = notCompleted;
    var percentages = this.rosterData.map((rost) => {
      return (rost.score === null ? null : rost.score)
    })
    var barData: number[] = [0, 0, 0, 0, 0];
    percentages.forEach((data, i) => {
      if (data === null || this.rosterData[i].completedDate === null) {
        barData[4] = barData[4] + 1
      }
      else if (data >= 90 && data <= 100) {
        barData[0] = barData[0] + 1;
      }
      else if (data >= 80 && data < 90) {
        barData[1] = barData[1] + 1
      }
      else if (data >= 70 && data < 80) {
        barData[2] = barData[2] + 1
      }
      else if (data >= 0 && data < 70) {
        barData[3] = barData[3] + 1
      }
      else {
        barData[4] = barData[4] + 1
      }
    });
    this.pieChartData['datasets'][0]['data'] = Object.assign([pass, fail, notCompleted]);
    this.barChartData['datasets'][0]['data'] = barData;
    this.passCount = (pass/this.rosterData.length)*100;
    this.failCount = (fail/this.rosterData.length)*100;
    this.notCompCount = (notCompleted/this.rosterData.length)*100;
    this.pieChartData['labels'] = [[this.passCount + '% Pass'], [this.failCount + '% Fail'], this.notCompCount + '% Not Completed']
    setTimeout(() => {
      this.hideChart = false;
    }, 1)
  }

  validGrades = ["P", "F", "W","O"];
  gradeError = true;
  checkInput(event: any) {
    if (event.data) {
      if (this.validGrades.includes(String(event.data).trim().toUpperCase())) {
        this.gradeError = false;
      }
      else {
        this.gradeError = true
      }
    }
    else {
      this.gradeError = true;
    }
  }

  async emitChange() {
    var value = this.preTestForm.get('test')?.value;
    if (value && value !== '') {
      var options = new RosterFetchOptions();
      options.classId = this.classId;
      options.testId = value;
      options.testType = 'Pretest';
      this.rosterData = await this.rosterService.getRosterData(options);
      
      this.dataSourcePreTest.data = this.rosterData;
      this.setChartsData();
      this.selectedTestId.emit(value);
    }
  }

  checkInputTbl(event: any) {
    if (event.data) {
      if (this.validGrades.includes(String(event.data).trim().toUpperCase())) {
        this.tblGradeError = false;
      }
      else {
        this.tblGradeError = true
      }
    }
    else {
      this.tblGradeError = true;
    }
  }

  keyPressNumbers(event: any) {
    var charCode = (event.which) ? event.which : event.keyCode;
    // Only Numbers 0-9
    if ((charCode < 48 || charCode > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }

  async updateValue() {
    this.isSaving = true;
    var options = new ClassRoasterUpdateOptions();
    switch (this.editType) {
      case 'grade':
        options.grade = this.valueToSave;
        options.testItemType = "Pretest";
        options.testId = this.preTestForm.get('test')?.value;
        options.classId = this.classId;
        await this.rosterService.updateGrade(this.editId, options).then((_) => {
          this.alert.successToast("Grade Updated Successfully");
          this.readyPretests();
          this.emitChange();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
      case 'score':
        options.score = this.valueToSave;
        options.testItemType = "Pretest";
        options.testId = this.preTestForm.get('test')?.value;
        options.classId = this.classId;
        await this.rosterService.updateScore(this.editId, options).then((_) => {
          this.alert.successToast("Score Updated Successfully");
          this.readyPretests();
          this.emitChange();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
      case 'compDate':
        this.compDate = new Date(this.compDate);
        options.compDate = this.compDate.toUTCString();
        options.testItemType = "Pretest";
        options.testId = this.preTestForm.get('test')?.value;
        options.classId = this.classId;
        await this.rosterService.updateCompDate(this.editId, options).then((_) => {
          this.alert.successToast("Completion Date Updated Successfully");
          this.readyPretests();
          this.emitChange();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
    }
  }

  async bulkUpdate() {
    this.spinner = true;
    this.isSaving = true;
    var options = new ClassRoasterUpdateOptions();
    options.testItemType = "Pretest";
    options.classId = this.classId,
    options.testId = this.preTestForm.get('test')?.value;
    options.bulkGrade = this.gradeValue;
    var testId = this.preTestForm.get('test')?.value;
    await this.rosterService.bulkUpdateGrade(this.classId, testId, options).then((_) => {
      this.readyPretests();
      this.emitChange();
      this.alert.successToast("Grades Updated in Bulk");
    }).finally(() => {
      this.spinner = false;
      this.gradeError = true;
      this.isSaving = false;
    })
  }


  async reCallTestDiaLog(templateRef: any, option: any) {
    this.option = option;
    var testId = this.preTestForm.get('test')?.value;
    var pretest = this.pretests.find(x => x.id === testId);
    this.recallDescription = `You are selecting to Recall Pretest ${pretest.testTitle} for ` + await this.labelPipe.transform('Employee') + ` ${option.employeeName}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  async releaseTestDiaLog(templateRef: any, option: any) {
    this.option = option;
    var testId = this.preTestForm.get('test')?.value;
    var pretest = this.pretests.find(x => x.id === testId);
    this.releaseDescription = `You are selecting to Release Pretest ${pretest.testTitle} for ` + await this.labelPipe.transform('Employee') + ` ${option.employeeName}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  async recallTest(option: ClassRoasterUpdateOptions) {
    if (option.grade === null) {
      option.testId = this.preTestForm.get('test')?.value;
      option.classId = this.classId;
      option.testType = this.testType;
      if (option.score == null) {
        option.score = 0;
      }
      await this.rosterService.reCallTest(option.empId, option).then((_) => {
        this.readyPretests();
        this.emitChange();
        ///this.alert.successToast("Grades Updated in Bulk");
      }).finally(() => {
        this.spinner = false;
        this.gradeError = true;
        this.isSaving = false;
      })
    }
    else{
      this.alert.errorAlert("Cannot Recall Pretest","The Pretest you are trying to recall has a grade assigned to it. Unable to recall this pretest from " + await this.labelPipe.transform('Employee') + " Portal.");
    }
  }
  async releaseTest(option: ClassRoasterUpdateOptions) {
    
    option.testId = this.preTestForm.get('test')?.value;
    option.classId = this.classId;
    option.testType = this.testType;
    var releaseDate = new Date();
    option.releaseDate = releaseDate.toUTCString();
    if (option.score == null) {
      option.score = 0;
    }
    await this.rosterService.releaseTest(option.empId, option).then((_) => {
      
      this.readyPretests();
      this.emitChange();
      //this.alert.successToast("Grades Updated in Bulk");
    }).finally(() => {
      this.spinner = false;
      this.gradeError = true;
      this.isSaving = false;
    })
  }

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
  //
  return newdatetime;
}

}
