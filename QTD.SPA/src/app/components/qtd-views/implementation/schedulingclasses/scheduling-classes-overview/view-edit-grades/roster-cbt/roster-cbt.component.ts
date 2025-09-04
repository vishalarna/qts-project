import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { ClassRoasterUpdateOptions } from '@models/ClassRoasterUpdateOption/ClassRoasterUpdateOptions';
import { ChartConfiguration, ChartData, ChartOptions } from 'chart.js';
import { RosterFetchOptions } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RosterFetchOptions';
import { RoastersModel } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RostersModel';
import { CBTScormRegistrationServiceService } from 'src/app/_Services/QTD/cbtscorm-registration.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { RostersService } from 'src/app/_Services/QTD/rosters.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-roster-cbt',
  templateUrl: './roster-cbt.component.html',
  styleUrls: ['./roster-cbt.component.scss']
})
export class RosterCbtComponent implements OnInit, OnDestroy {
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
  passCount:number=0;
  failCount:number=0;
  notCompCount:number=0;
  public pieChartLabels = [[this.passCount+'% Pass'], [this.failCount+'% Fail'],this.notCompCount+'% Not Completed'];
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
    'cbtScore',
    'cbtGrade',
    'completedDate',
  ];

  @Input() ilaId = "";
  @Output() selectedTestId = new EventEmitter<any>();
  subscription = new SubSink();
  cbts!: any[];
  scormForms!: any[];
  cbtForm = new UntypedFormGroup({});
  @Input() classId = "";
  @Input() testType = "";
  rosterData: RoastersModel[] = [];

  editType: 'score' | 'grade' | 'none'| 'compDate' = 'none';
  editId: any = "";
  compDate!: any;
  tblGradeError = true;

  spinner = false;
  gradeValue = "";

  valueToSave!: any;
  hideChart = false;
  isSaving = false;
  datePipe = new DatePipe('en-us');
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private router: ActivatedRoute,
    private rosterService: RostersService,
    private dataBroadcastService: DataBroadcastService,
    private alert: SweetAlertService,
    private ilaService:IlaService,
    private cbtScormRegistrationservice:CBTScormRegistrationServiceService
  ) { }

  ngOnInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshRosterData.subscribe((_) => {
      this.emitChange();
    })
    this.cbtForm.addControl('test', new UntypedFormControl('', Validators.required));
    this.getCBTs();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  openFlyInPanelEditGrades(templateRef: any, row: any) {
    const portal = new TemplatePortal(templateRef, row);
    this.flyPanelSrvc.open(portal);
  }

  async getCBTs() {
    this.editId = '';
    this.editType = 'none';
    this.valueToSave = "";
    this.tblGradeError = true;
    this.gradeValue = "";
    this.cbts = await this.ilaService.GetCBTScormFormsForILAAsync(this.ilaId);
    if (this.cbts && Array.isArray(this.cbts)) {
      const scormUploadsArray = [].concat(...this.cbts.map(x => x.scormUploads));
      this.scormForms = Array.from(new Set(scormUploadsArray));
    } else {
      this.scormForms = [];
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

  setChartsData() {
    this.hideChart = true;
    var pass = this.rosterData.filter((rost: RoastersModel) => {
      return rost.completedDate !== null && (rost.score && rost.score > 50);
    }).length;
    this.passCount=pass;
    var fail = this.rosterData.filter((rost: RoastersModel) => {
      return rost.completedDate !== null && (rost.score !== null && rost.score <= 50);
    }).length;
    this.failCount=fail;
    var notCompleted = this.rosterData.filter((rost: RoastersModel) => {
      return rost.completedDate === null;
    }).length;
    this.notCompCount=notCompleted;
    var percentages = this.rosterData.map((rost) => {
      return (rost.score === null ? null : rost.score)
    })
    var barData: number[] = [0, 0, 0, 0, 0];
    percentages.forEach((data,i) => {
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
    setTimeout(() => {
      this.hideChart = false;
    }, 1)
  }

  validGrades = ["P", "F", "W"];
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
    var value = this.cbtForm.get('test')?.value;
    if (value && value !== '') {
      var options = new RosterFetchOptions();
      options.classId = this.classId;
      options.testId = value;
      options.testType = 'CBT';
      this.rosterData = await this.rosterService.getRosterData(options);
      this.rosterData = this.rosterData.map((item=>{
        var compDate = ""
        if(item.completedDate == null){
          compDate = null;
        }else{
          var updatedDate = new Date(item.completedDate + "Z");
          compDate = new Date(updatedDate).toLocaleString();
        } 
        return{...item,completedDate:compDate}
      }));
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

  async updateValue(empId:any) {
    this.isSaving = true;
    var options = new ClassRoasterUpdateOptions();

    switch (this.editType) {
      case 'grade':
        options.grade = this.valueToSave;
        options.testType = "CBT";
        options.testId = this.cbtForm.get('test')?.value;
        options.classId = this.classId;
        await this.cbtScormRegistrationservice.updateCbtRegistrationAsync(this.editId, options).then((_) => {
          this.alert.successToast("Grade Updated Successfully");
          this.getCBTs();
          this.emitChange();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
      case 'score':
        options.score = this.valueToSave;
        options.testType = "CBT";
        options.testId = this.cbtForm.get('test')?.value;
        options.classId = this.classId;
        await this.cbtScormRegistrationservice.updateCbtRegistrationAsync(this.editId, options).then((_) => {
          this.alert.successToast("Score Updated Successfully");
          this.getCBTs();
          this.emitChange();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
        case 'compDate':
        this.compDate = new Date(this.compDate +'T00:00:00')
        options.compDate = this.compDate;
        options.testType = "CBT";
        options.testId = this.cbtForm.get('test')?.value;
        options.classId = this.classId;
        await this.cbtScormRegistrationservice.updateCbtRegistrationAsync(this.editId, options).then((_) => {
          this.alert.successToast("Completion Date Updated Successfully");
          this.getCBTs();
          this.emitChange();
        }).finally(() => {
          this.isSaving = false;
        })
        break;
    }
  }

  async bulkUpdate() {
    this.spinner = true;
    var options = new ClassRoasterUpdateOptions();
    options.testType = "CBT";
    options.grade = this.gradeValue;
    options.classId = this.classId;
    options.testId = this.cbtForm.get('test')?.value;
    await this.cbtScormRegistrationservice.bulkUpdateCbtRegistrationsAsync(this.classId, options).then((_) => {
      this.getCBTs();
      this.emitChange();
      this.alert.successToast("Grades Updated in Bulk");
    }).finally(() => {
      this.spinner = false;
      this.gradeError = true;
    })
  }

}
