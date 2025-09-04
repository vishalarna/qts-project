import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ClassInfoVM } from 'src/app/_DtoModels/Dashboard/ClassInfoVM';
import { ILA_UploadOptions } from 'src/app/_DtoModels/ILA/ILA_UploadOptions';
import { DashboardService } from 'src/app/_Services/QTD/Dashboard/dashboard.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';

@Component({
  selector: 'app-flypanel-view-course-info',
  templateUrl: './flypanel-view-course-info.component.html',
  styleUrls: ['./flypanel-view-course-info.component.scss']
})
export class FlypanelViewCourseInfoComponent implements OnInit {
  @Input() ilaId!: any;
  @Input() classId!: any;
  @Output() closed = new EventEmitter<any>();
  classInfo!: ClassInfoVM;
  classSpinner = false;
  courseSpinner = false;
  courseInfo!: any;
  ilaFiles:any[] = [];
  eoSource: MatTableDataSource<any> = new MatTableDataSource();
  displayColumns = ["type", "num", "statement"];
  datePipe = new DatePipe('en-us');
  downloadSpinner:any[] = [];
  resourceLoader = false;
  constructor(
    private dashboardService: DashboardService,
    private ilaService: IlaService,
  ) { }

  ngOnInit(): void {
    if (this.ilaId !== null && this.ilaId !== undefined && this.ilaId !== '') {
      this.readyClassData();
      this.readyCourseData();
      this.readyResources();
    }
  }

  async readyResources() {
    this.resourceLoader = true;
    this.ilaFiles = await this.ilaService.getUploadedFiles(this.ilaId);
    this.downloadSpinner = Array(this.ilaFiles.length).fill(false);
    this.resourceLoader = false;
  }

  async readyClassData() {
    this.classSpinner = true;
    this.classInfo = await this.dashboardService.getClassInfo(this.classId);
    var startDateString = this.datePipe.transform(
      this.classInfo.startDate,
      'yyyy-MM-dd hh:mm a'
    );

    const utcStartDateTime = new Date(startDateString.toString() + ' UTC');
    // Convert UTC date and time to local time
    const localstartDateTimeString = utcStartDateTime.toLocaleString();
    this.classInfo.startDate = new Date(Date.parse(localstartDateTimeString));

    var endDateString = this.datePipe.transform(
      this.classInfo.endDate,
      'yyyy-MM-dd hh:mm a'
    );

    const utcEndDateTime = new Date(endDateString.toString() + ' UTC');
    // Convert UTC date and time to local time
    const localendDateTimeString = utcEndDateTime.toLocaleString();
    this.classInfo.endDate = new Date(Date.parse(localendDateTimeString));
    // this.classInfo.startDate = this.convertUtcTimeToLocalTime(this.classInfo.startDate);
    // this.classInfo.endDate = this.convertUtcTimeToLocalTime(this.classInfo.endDate);
    this.classSpinner = false;
  }

  async readyCourseData() {
    this.courseSpinner = true;
    this.courseInfo = await this.dashboardService.getCourseInfo(this.ilaId);
    this.eoSource.data = this.courseInfo.learningObjectivesList;
    this.courseSpinner = false;
  }

  convertUtcTimeToLocalTime(datetimeObj: any) {
    const utcStartDateTime = new Date(datetimeObj);
    // Convert UTC date and time to local time
    const localstartDateTimeString = utcStartDateTime.toLocaleString();
    var localDateTime = new Date(Date.parse(localstartDateTimeString));
    //var parsedDate = this.datePipe.transform(localstartDateTimeString,"yyyy-MM-dd hh:MM a")
    return localDateTime;
  }

  async downloadFile(file: any,index:any) {
    // const linkSource = file.fileAsBase64;
    // const downloadLink = document.createElement("a");
    // const fileName = file.fileName;
    // downloadLink.href = linkSource;
    // downloadLink.download = fileName;
    // downloadLink.click();
    this.downloadSpinner[index] =true;
    await this.ilaService.getDownloadData(this.ilaId, file.id).then((res: any) => {
      const linkSource = res.fileAsBase64;
      const downloadLink = document.createElement("a");
      const fileName = res.fileName;
      downloadLink.href = linkSource;
      this.downloadSpinner[index] =false;
      downloadLink.download = fileName;
      downloadLink.click();
    }).catch((err) => {
      this.downloadSpinner[index] =false;
      console.error(err);
    })

  }
}
