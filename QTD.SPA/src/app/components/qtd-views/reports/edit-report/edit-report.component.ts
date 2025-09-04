import { Component, Input, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { UntypedFormBuilder } from '@angular/forms';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { Router } from '@angular/router';
import { filterPropertyTypeEnum, filterValueTypeEnum } from 'src/app/_DtoModels/ReportSkeleton/ReportSkeletonFilter';
import { ReportUpdateOptions } from 'src/app/_DtoModels/Report/ReportUpdateOptions';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { ReportFilterOption } from 'src/app/_DtoModels/Report/ReportFilterOption';
import { TranslateService } from '@ngx-translate/core';
import { Subject } from 'rxjs';
import { ReportSkeleton } from 'src/app/_DtoModels/ReportSkeleton/ReportSkeleton';
import { Location } from '@angular/common';
import { ReportListFilterOption } from 'src/app/_DtoModels/Report/ReportListFilterOption';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'app-edit-report',
  templateUrl: './edit-report.component.html',
  styleUrls: ['./edit-report.component.scss']
})
export class EditReportComponent implements OnInit {

  @Input() reportSkeletons: ReportSkeleton[];
  @Input() route: string;
  public reportSkeletonData: ReportSkeleton;
  public reportSkeletonFilters;
  public selectedReportSkeleton;
  public selectedReport;
  public positionFilterData;
  public reportsData;
  public reportId;
  public reportSkeletonId;
  public reportMode: string;
  public reportUpdateOptions: ReportUpdateOptions;
  public listEnabled;
  public listDisabled;
  public listDetails;
  public tempTitle:string = "";
  public spinner:boolean;
  public isRunReport: boolean = false;
  public reportViewContent;
  public isDataColumnHidden: boolean = true;
  filterListSubject: Subject<Array<number>> = new Subject<Array<number>>();
  titleError:boolean;

  constructor(private router: Router, private alertService: SweetAlertService,
    private translate: TranslateService, private reportService: ApiReportsService, private fb: UntypedFormBuilder,
    private location: Location,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe) { }

  ngOnInit() {
    this.getUrlDetails();
    this.spinner = false;
    this.initializeUpdateOrCreateOptions();
    this.titleError = false;
  }

  initializeUpdateOrCreateOptions(): void {
    this.reportUpdateOptions = new ReportUpdateOptions();
  }

  public async getUrlDetails() {
    let segments = this.router.url.split('/');
    if (segments.includes('update')) {
      this.reportMode = 'update';
      this.reportId = segments[segments.length - 1];
      this.getEditPageDetails();
    }
    else if ((segments.includes('create'))) {
      this.reportMode = 'create';
      this.reportSkeletonId = segments[segments.length - 1];
      this.createPageDetails();
    }
  }

  public async getEditPageDetails(): Promise<void> {
    await this.reportService.getReportsByIdAsync(this.reportId).then(async res => {
      this.selectedReport = res;
      this.reportSkeletonData = await this.reportService.getReportSkeletonAsync(this.selectedReport.reportSkeletonId);
      this.reportSkeletonData.displayColumns = this.reportSkeletonData.displayColumns.map(skeletonColumn => {
        let displayColumn = this.selectedReport.displayColumns.find(dc => dc.columnName === skeletonColumn.columnName);
        return { 
            ...skeletonColumn, 
            display: displayColumn ? displayColumn.display : true 
        };
      });
      this.reportSkeletonData.availableFilters = this.reportSkeletonData.availableFilters.map(filter => {
        let reportFilter = this.selectedReport.filters.find(x=>x.name == filter.name && x.propertyType == filter.propertyType && x.valueType == filter.valueType);
        return { ...filter, value: reportFilter?.value };
      });
      this.reportSkeletonFilters = this.reportSkeletonData.availableFilters;
      this.listDetails = this.reportSkeletonData.displayColumns.map((record, index) => ({ ...record, sequence: index + 1 }));;
      this.listEnabled = this.listDetails.filter(dd => dd.display === true);
      this.listDisabled = this.listDetails.filter(dd => dd.display === false);
    });
  }

  public getReportSkeletonFilterOption(filter: any){
    if(this.reportMode === "update"){
    const reportSkeleton =  this.reportSkeletonData?.availableFilters?.filter(dd => dd.name == filter.name)[0];
    if(reportSkeleton?.filterOption){
      return reportSkeleton.filterOption;
    }} else{
      if(filter?.filterOption){
        return filter.filterOption;
      }
    }
  }

  public async createPageDetails(): Promise<void> {
    await this.reportService.getReportSkeletonAsync(this.reportSkeletonId).then((response) => {
      this.selectedReportSkeleton = response;
      this.reportSkeletonFilters = this.selectedReportSkeleton.availableFilters;
      this.listDetails = this.selectedReportSkeleton.displayColumns.map((record, index) => ({ ...record, sequence: index + 1 }));;
      this.listEnabled = this.listDetails;
      this.listDisabled = [];
    });
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
  }

  public getRunReportValue(event){
    this.isRunReport = event;
    if(this.reportMode == 'update'){
      this.selectedReport.filters.map((x) => {
        const filter = this.reportUpdateOptions.filters.filter(r => r.name == x.name);
        if(filter.length > 0){
          x.value = filter[0].value;
        }
      });
    }
  }
  public internalReportTitleChange(title: string) {
    this.tempTitle = title;
    if(this.tempTitle.trim() !== ""){
      this.titleError = false;
    }else{
      this.titleError = true;
    }
    if (this.reportMode === "update") {
      this.selectedReport.internalReportTitle = title;
      this.reportUpdateOptions.getInternalReportTitle(this.selectedReport.internalReportTitle);
    } else {
      this.selectedReportSkeleton.internalReportTitle = title;
      this.reportUpdateOptions.getInternalReportTitle(this.selectedReportSkeleton.internalReportTitle);
    }

  }
  public getValueTypeName(index: number) {
    return filterValueTypeEnum[index].toLowerCase();
  }
  public getPropertyTypeName(index: number){
    return filterPropertyTypeEnum[index].toLowerCase();
  }

  public getFilterOptionChange(reportOption: ReportFilterOption) {
    this.reportUpdateOptions.getFilters(reportOption);
  }

  async goBack() {
    this.location.back();
  }

  public async onRunReport(): Promise<void> {
    this.spinner = true;
    this.getDisplayColumnsList();
    this.reportUpdateOptions.reportSkeletonId = this.reportSkeletonId; 
    if (this.reportMode == 'create') {
      if (this.reportUpdateOptions.internalReportTitle == null || this.reportUpdateOptions.internalReportTitle.trim() === "") {
        var transformedValue = await this.dynamicLabelReplacementPipe.transform(this.selectedReportSkeleton.defaultTitle);
        this.reportUpdateOptions.getInternalReportTitle(transformedValue);
   }
     await this.reportService.generateReportAsync(this.reportUpdateOptions).then((res) => {
        this.reportViewContent = res.content;
        this.isRunReport = true;
      }).finally(()=>{
        this.spinner=false;
      });
    }
    else if (this.reportMode == 'update') {
      this.reportUpdateOptions.getInternalReportTitle(this.selectedReport.internalReportTitle);
      await this.reportService.generateReportByIdAsync(this.reportUpdateOptions, this.reportId).then((res) => {
        this.reportViewContent = res.content;
        this.spinner = false;
        this.isRunReport = true;
      }).finally(()=>{
        this.spinner=false;
      });;
    }
  }
  public onToggleChanged(event: any, item: any) {
    if (event.target.checked == false) {
      this.listDisabled.push(item);
      this.listEnabled = this.listEnabled.filter(x => x != item);
    }
    else {
      item.display = true;
      this.listEnabled.push(item);
      this.listDisabled = this.listDisabled.filter(x => x != item);
    }
    this.listEnabled = this.listEnabled.sort((a, b) => a.sequence - b.sequence);
  }

  public getDisplayColumnsList() {
    this.reportUpdateOptions.displayColumns=[];
    this.listEnabled.forEach((element) => {
      this.reportUpdateOptions.getDisplayColumns(element.columnName);
    });
  }

  public async onSaveReport() {
    this.getDisplayColumnsList();
    if (this.reportMode == 'create') {
      if(this.tempTitle === ""){
        this.titleError = true;
        this.reportUpdateOptions.internalReportTitle = null;
        return;
      }
      this.reportUpdateOptions.reportSkeletonId = this.reportSkeletonId;
      await this.reportService.createReportAsync(this.reportUpdateOptions);
      this.alertService.notificationSuccessToast(this.translate.instant("Saved Successfully"));
    }
    else {
      await this.reportService.updateReportAsync(this.reportUpdateOptions, this.reportId);
      this.alertService.notificationSuccessToast(this.translate.instant("Saved Successfully"));
      this.getEditPageDetails();
    }
  }
}

