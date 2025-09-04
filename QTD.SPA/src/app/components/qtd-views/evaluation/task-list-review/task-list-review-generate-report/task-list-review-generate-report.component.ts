import { HttpResponse } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ReportExportOptions, ReportExportType } from '@models/Report/ReportExportOptions';
import { ReportFilterOption } from '@models/Report/ReportFilterOption';
import { ReportUpdateOptions } from '@models/Report/ReportUpdateOptions';
import { ReportSkeleton } from '@models/ReportSkeleton/ReportSkeleton';
import { ReportSkeletonColumn } from '@models/ReportSkeleton/ReportSkeletonColumn';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { ApiTaskListReviewService } from 'src/app/_Services/QTD/TaskListReview/api.tasklistreview.service';


@Component({
  selector: 'app-task-list-review-generate-report',
  templateUrl: './task-list-review-generate-report.component.html',
  styleUrls: ['./task-list-review-generate-report.component.scss']
})
export class TaskListReviewGenerateReportComponent implements OnInit {

  @Input() inputTaskListReviewId: string;
  @Input() inputTaskListReviewStatus: boolean;
  @Output() closed = new EventEmitter<any>();
  reportSkeleton: ReportSkeleton;
  reportSkeletonName: string;
  reportCreateorUpdate:ReportUpdateOptions;
  displayColumns:ReportSkeletonColumn[];
  spinner:boolean = false ;
  constructor(
    private apireportService: ApiReportsService,
    private taskListReviewService: ApiTaskListReviewService
  ) { }

  ngOnInit(): void {
    this.reportSkeletonName = 'Annual Positions Task List Review';
    this.getReportSkeletonData();
  }


  async getReportSkeletonData() {
    this.reportSkeleton = await this.apireportService.getReportSkeletonByNameAsync(this.reportSkeletonName);
    this.displayColumns =  Object.assign(this.reportSkeleton?.displayColumns);
    this.getFilteredData();
  }

  async getFilteredData(){
    this.displayColumns = await this.displayColumns.filter((x)=>{
      return x.columnName != 'Action Items' && x.columnName != 'Task History'
    })
  }

  getCreateUpdateOptions(){
    var reportCreateOptions = new ReportUpdateOptions();
    this.displayColumns.map(item=>{
      reportCreateOptions.getDisplayColumns(item.columnName)
    })
    var reportFilters = Array<ReportFilterOption>();
    var taskListReviewFilter  = this.reportSkeleton?.availableFilters?.find(x=>x.name.toLowerCase() =='task list reviews');
    const taskListReviewIdFilter = new ReportFilterOption(taskListReviewFilter.name,this.inputTaskListReviewId);
    reportFilters.push(taskListReviewIdFilter);
    var statusFilter  = this.reportSkeleton?.availableFilters?.find(x=>x.name.toLowerCase() =='status');
    const taskListReviewStatusFilter = new ReportFilterOption(statusFilter?.name, this.inputTaskListReviewStatus?'ACTIVE':'INACTIVE');
    reportFilters.push(taskListReviewStatusFilter);
    reportCreateOptions.filters = reportFilters;
    reportCreateOptions.reportSkeletonId = this.reportSkeleton?.id;
    reportCreateOptions.internalReportTitle = this.reportSkeletonName;
    this.reportCreateorUpdate = reportCreateOptions;
  }
  
  async onGenerateReportAsync(){
    this.spinner = true;
    this.getCreateUpdateOptions();
    var reportExportOption = new ReportExportOptions();
    reportExportOption.exportType = ReportExportType.Pdf;
    reportExportOption.options = this.reportCreateorUpdate;
    await this.taskListReviewService.generateReportAsync(reportExportOption).then((res) => {
          this.createAndDownloadBlobFile(res);
    });
    this.spinner = false;
    this.closed.emit();
  }

  public createAndDownloadBlobFile(response: HttpResponse<Blob>) {
    const contentDispositionHeader = response.headers.get(
      'content-disposition'
    );
    const defaultFileName = 'downloaded-file.pdf';
    let fileName = defaultFileName;

    if (contentDispositionHeader) {
      const match = contentDispositionHeader.match(
        /filename=['"]?([^'";]+)['"]?/
      );
      fileName = match ? match[1] : defaultFileName;
    }
    const nameAndDateMatch = fileName.match(/^(.*?)\s?(\d{8})/);
    const name = nameAndDateMatch ? nameAndDateMatch[1] : defaultFileName;
    const date = nameAndDateMatch ? nameAndDateMatch[2] : '';
    fileName = `${name.replace(/\s+/g, '')}${date ? date : ''}.pdf`;

    const blob = new Blob([response.body!], {
      type: 'application/octet-stream',
    });
    const url = window.URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }

  filterDisplayColumns(value:any){
    this.displayColumns = [...this.reportSkeleton.displayColumns];
    if(value=='1'){
      this.getFilteredData();
    }
    else if(value=='2'){
      this.displayColumns = this.displayColumns.filter((x)=>{
        return x.columnName != 'Task History'
      })
    }
    this.getCreateUpdateOptions();
  }
  

}
