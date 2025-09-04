import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
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
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-roster-retake',
  templateUrl: './roster-retake.component.html',
  styleUrls: ['./roster-retake.component.scss']
})
export class RosterRetakeComponent implements OnInit, OnDestroy {
  @Input() ilaId = "";
  @Output() selectedTestId = new EventEmitter<any>();
  retakes!: RosterTestVM[];
  retakeForm = new UntypedFormGroup({});
  recallDescription: string = "";
  releaseDescription: string = "";
  option: ClassRoasterUpdateOptions;

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
    'retakeScore',
    'retakeGrade',
    'completionDate',
    'disclaimer',
    'interrupted',
    'restarted',
    'actions'
  ];

  displayedNonEMPColumns: string[] = [
    'employee',
    'releaseDate',
    'retakeScore',
    'retakeGrade',
    'completionDate',
  ]

  rosterData: RoastersModel[] = [];
  @Input() classId = "";
  @Input() testType = "";

  editType: 'score' | 'grade' | 'none' | 'compDate' = 'none';
  editId: any = "";
  compDate!: any;
  datePipe = new DatePipe('en-us');

  tblGradeError = true;

  valueToSave!: any;
  hideChart = false;

  subscription = new SubSink();

  spinner = false;

  gradeValue = "";
  isSaving = false;
  license!: any;
  retakeOrder?: any;

  constructor(
    public dialog: MatDialog,
    private testService: TestsService,
    private rosterService: RostersService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private clientSettingService: ApiClientSettingsService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.retakeForm.addControl('test', new UntypedFormControl('', Validators.required));
    this.subscription.sink = this.dataBroadcastService.refreshRosterData.subscribe((_) => {
      this.emitChange();
    })
    this.readyData();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async readyData() {
    this.editId = '';
    this.editType = 'none';
    this.valueToSave = "";
    this.tblGradeError = true;
    this.gradeValue = "";
    this.retakes = await this.testService.getSpecificTests(this.classId, 'Retake');
    this.license = await this.clientSettingService.GetCurrentLicenseAsync();
    var id = this.retakeForm.get('test')?.value.id;
    if(id !== null && id !== undefined){
      var test = this.retakes.find(x => x.id === id && x.retakeOrder === this.retakeForm.get('test')?.value.retakeOrder);
      this.retakeForm.get('test').setValue(test);
      this.retakeForm.updateValueAndValidity();
    }
    // this.setChartsData();
    
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
    this.passCount = (pass / this.rosterData.length) * 100;
    this.failCount = (fail / this.rosterData.length) * 100;
    this.notCompCount = (notCompleted / this.rosterData.length) * 100;
    this.pieChartData['labels'] = [[this.passCount + '% Pass'], [this.failCount + '% Fail'], this.notCompCount + '% Not Completed'];
    setTimeout(() => {
      this.hideChart = false;
    }, 1)
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

  async emitChange() {
    var value = this.retakeForm.get('test')?.value.id;
    if (value && value !== '') {
      var options = new RosterFetchOptions();
      options.classId = this.classId;
      options.testId = value;
      options.testType = 'Retake';
      var test = this.retakes.find((x) => x.id === value && this.retakeForm.get('test')?.value.retakeOrder === x.retakeOrder)
      options.retakeOrder = test.retakeOrder;
      this.retakeOrder = options.retakeOrder;
      this.rosterData = await this.rosterService.getRosterData(options);
      this.dataSourcePreTest.data = this.rosterData;
      this.setChartsData();
      this.selectedTestId.emit(value);
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
        options.testType = "Retake";
        options.testId = this.retakeForm.get('test')?.value.id;
        options.classId = this.classId;
        options.retakeOrder = this.rosterData.find(x=>x.empId==this.editId)?.retakeOrder;
        await this.rosterService.updateGrade(this.editId, options).then(async(_) => {
          this.alert.successToast("Grade Updated Successfully");
          await this.readyData();
          await this.emitChange();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
      case 'score':
        options.score = this.valueToSave;
        options.testType = "Retake";
        options.testId = this.retakeForm.get('test')?.value.id;
        options.classId = this.classId;
        options.retakeOrder =  this.rosterData.find(x=>x.empId==this.editId)?.retakeOrder;
        await this.rosterService.updateScore(this.editId, options).then(async(_) => {
          this.alert.successToast("Score Updated Successfully");
          await this.readyData();
          await this.emitChange();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
      case 'compDate':
        this.compDate = new Date(this.compDate);
        options.compDate = this.compDate.toUTCString();
        options.testItemType = "Retake";
        options.testId = this.retakeForm.get('test')?.value.id;
        options.classId = this.classId;
        options.retakeOrder = this.rosterData.find(x=>x.empId==this.editId)?.retakeOrder;
        await this.rosterService.updateCompDate(this.editId, options).then(async(_) => {
          this.alert.successToast("Completion Date Updated Successfully");
          await this.readyData();
          await this.emitChange();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
    }
  }

  validGrades = ["P", "F", "W", "O"];
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

  async bulkUpdate() {
    this.spinner = true;
    this.isSaving = true;
    var options = new ClassRoasterUpdateOptions();
    options.testItemType = "Retake";
    options.classId = this.classId;
    options.testId = this.retakeForm.get('test')?.value.id;
    options.retakeOrder = this.retakeOrder;
    options.bulkGrade = this.gradeValue;
    var testId = this.retakeForm.get('test')?.value.id;
    await this.rosterService.bulkUpdateGrade(this.classId, testId, options).then(async(_) => {
      await this.readyData();
      await this.emitChange();
      this.alert.successToast("Grades Updated in Bulk");
    }).finally(() => {
      this.spinner = false;
      this.gradeError = true;
      this.isSaving = false;
    })
  }

  async reCallTestDiaLog(templateRef: any, option: any) {
    this.option = option;
    var testId = this.retakeForm.get('test')?.value.id;
    var pretest = this.retakes.find(x => x.id === testId && this.retakeForm.get('test')?.value.retakeOrder === x.retakeOrder);
    this.recallDescription = `You are selecting to Recall Test ${pretest.testTitle} for ` + await this.labelPipe.transform('Employee') + ` ${option.employeeName}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async releaseTestDiaLog(templateRef: any, option: any) {
    this.option = option;
    var testId = this.retakeForm.get('test')?.value.id;
    var pretest = this.retakes.find(x => x.id === testId && this.retakeForm.get('test')?.value.retakeOrder === x.retakeOrder);
    this.releaseDescription = `You are selecting to Release Test ${pretest.testTitle} for ` + await this.labelPipe.transform('Employee') + ` ${option.employeeName}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async recallTest(option: ClassRoasterUpdateOptions) {
    
    option.testId = this.retakeForm.get('test')?.value.id;
    option.classId = this.classId;
    option.testType = this.testType;
    option.retakeOrder = this.retakeOrder;
    if (option.score == null) {
      option.score = 0;
    }
    await this.rosterService.reCallTest(option.empId, option).then((_) => {
      this.readyData();
      this.emitChange();
      ///this.alert.successToast("Grades Updated in Bulk");
    }).finally(() => {
      this.spinner = false;
      this.gradeError = true;
      this.isSaving = false;
    })
  }
  async releaseTest(option: ClassRoasterUpdateOptions) {
    
    option.testId = this.retakeForm.get('test')?.value.id;
    option.classId = this.classId;
    option.testType = this.testType;
    option.retakeOrder = this.retakeOrder;
    var releaseDate = new Date();
    option.releaseDate = releaseDate.toUTCString();
    if (option.score == null) {
      option.score = 0;
    }
    await this.rosterService.releaseTest(option.empId, option).then((_) => {
      this.readyData();
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
