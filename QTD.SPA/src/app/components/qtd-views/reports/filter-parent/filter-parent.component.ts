import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { ReportFilterOption } from 'src/app/_DtoModels/Report/ReportFilterOption';
import { ReportListFilterOption } from 'src/app/_DtoModels/Report/ReportListFilterOption';
import { ReportSkeletonFilter } from 'src/app/_DtoModels/ReportSkeleton/ReportSkeletonFilter';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-filter-parent',
  templateUrl: './filter-parent.component.html',
  styleUrls: ['./filter-parent.component.scss'],
})
export class FilterParentComponent implements OnInit {
  @Input() reportSkeletonFilter: ReportSkeletonFilter;
  @Input() reportMode;
  @Input() filterOption;
  @Input() valueType:string;
  @Input() propertyType:string;
  @Input() reportData:any;
  @Output() OnFilterUpdated: EventEmitter<ReportFilterOption> = new EventEmitter();
  reportSkeletonFilterData:ReportSkeletonFilter;
  public reportListFilterOption:ReportListFilterOption=new ReportListFilterOption();
  filterListData: ReportFilterOption[] = [];
  filterListCount: number = 0;
  datePipe = new DatePipe('en-us');

  constructor(
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private reportService: ApiReportsService) {}

  async ngOnInit() { 
    console.log("paernt report data",this.reportData)
    this.filterListData=[];
    this.reportSkeletonFilterData=this.reportSkeletonFilter;
    await this.getOptionsDisplayData();
    if(this.reportMode == 'update'){
      let reportValue = this.reportData?.filters?.find((x)=>x.name == this.reportSkeletonFilter.name)
      let reportOption;
      if(reportValue == undefined){
        reportOption= new ReportFilterOption(this.reportSkeletonFilter.name,this.reportSkeletonFilter.value ?? this.reportSkeletonFilter?.defaultValue);
      }else{
        reportOption= new ReportFilterOption(this.reportSkeletonFilter.name,reportValue.value);
      }
      this.OnFilterUpdated.emit(reportOption);
    }
  }

  async getOptionsDisplayData(){
    switch (this.valueType) {
      case 'array':
        let filterListValue;
        let allSelectedIds = [];
        if(this.reportMode == 'update'){
          var reportValue = this.reportData?.filters?.find((x)=>x.name == this.reportSkeletonFilter.name)
          filterListValue = reportValue != null ? reportValue.value : this.reportSkeletonFilter.defaultValue;
        }else{
          filterListValue = this.reportSkeletonFilter.value ?? this.reportSkeletonFilter.defaultValue;
        }
        await this.reportService.getReportFilterOptionsAsync(this.filterOption).then((response) => {
          let positionData = response.map((item) => {
            item.id = Number(item.value);
            return item;
          });
          if (filterListValue && filterListValue !== '') {
            if (filterListValue === 'listAllSelected') {
              positionData.forEach(x => {
                  this.filterListData.push(x);
                  allSelectedIds?.push(x.id);
              });
          }else{
              filterListValue.split(",").map(Number).forEach(x=>
                {
                    let option = positionData.find(s=>s.id ==x);
                    if(option){
                      this.filterListData.push(option);
                    }
                })
            }
                }
                  this.filterListData.sort((a,b)=>a.name.toUpperCase()<b.name.toUpperCase()?-1:1);
                  this.filterListCount=positionData.length;
                });
                if(filterListValue == 'listAllSelected'){
                  var allSelectedIdsString = allSelectedIds?.join(",");
                  this.OnFilterUpdated.emit(new ReportFilterOption(this.reportSkeletonFilter.name,allSelectedIdsString))
                }else{
                  this.OnFilterUpdated.emit(new ReportFilterOption(this.reportSkeletonFilter.name,filterListValue))
                }
        break;
      case 'single':
        this.filterListData =[];
        let positionData =[];
        let filterValue = this.reportSkeletonFilter.value ?? this.reportSkeletonFilter.defaultValue;
        let isDefaultOrder=false;
        if(this.filterOption){
          await this.reportService.getReportFilterOptionsAsync(this.filterOption).then(async (response) => {
            positionData = response.map((item) => {
             isDefaultOrder=item.isDefaultOrder;
              item.id = Number(item.value);
              return item;
            });
            if (!isDefaultOrder) {
              positionData.sort((a, b) => a.name.toUpperCase() < b.name.toUpperCase() ? -1 : 1);
            }
            if (this.propertyType === 'string') {
              this.filterListData =[];
              this.OnFilterUpdated.emit(new ReportFilterOption(this.reportSkeletonFilter.name,filterValue))
            }
            if (filterValue) {
              let option = positionData.find(s=>s.value == filterValue);
              if(option){
                this.filterListData=[option];
                this.reportSkeletonFilterData.value=option.value;
                this.OnFilterUpdated.emit(new ReportFilterOption(this.reportSkeletonFilter.name,option.value))
              }
            }
          });
        }
        if(this.propertyType === 'boolean'){
          const defaultValue = filterValue === null ? 'false' : filterValue;
          this.filterListData=[new ReportFilterOption(defaultValue,defaultValue)];
          this.reportSkeletonFilterData.value=defaultValue;
          this.OnFilterUpdated.emit(new ReportFilterOption(this.reportSkeletonFilter.name,defaultValue))
        }
        if(filterValue && this.propertyType !== 'string'){
          this.filterListData=[new ReportFilterOption(filterValue,filterValue)];
          this.reportSkeletonFilterData.value = filterValue;
          this.OnFilterUpdated.emit(new ReportFilterOption(this.reportSkeletonFilter.name,filterValue))
        }
       
        break;
      case 'range':
        if(this.reportSkeletonFilter.value  && this.reportSkeletonFilter.value !== ''){
          let reportsData = this.reportSkeletonFilter.value.split(",");
          let firstValue = reportsData[0] != 'undefined' ? (this.reportSkeletonFilter.valueType === 2 ? this.datePipe.transform(reportsData[0], "MM-dd-yyyy") : reportsData[0]):null;
          let secondValue = reportsData[1]!= 'undefined' ? (this.reportSkeletonFilter.valueType === 2 ? this.datePipe.transform(reportsData[1], "MM-dd-yyyy") : reportsData[1]):null;
          const filterRangeDisplayName = "From " + firstValue + " to " +secondValue;
          this.filterListData.push(new ReportFilterOption(filterRangeDisplayName,this.reportSkeletonFilter.value))
        } 
        break;
    
      default:
        break;
    }
  }

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  public getFilterListDataChange(filterOption: ReportListFilterOption) {
    this.reportSkeletonFilterData.value= filterOption.selectedFilterOptions.map(d=>d?.id).toString();
    this.filterListData=filterOption.selectedFilterOptions.sort((a,b)=>a.name.toUpperCase()<b.name.toUpperCase()?-1:1);
    this.filterListCount=filterOption.dataCount;
    let reportOption= new ReportFilterOption(this.reportSkeletonFilter.name,filterOption.selectedFilterOptions.map(d=>d.id).toString());
    this.OnFilterUpdated.emit(reportOption)
  }
  public getFilterOptionChange(filterOption: ReportFilterOption) {
    this.filterListData =[];
    this.reportSkeletonFilterData.value=filterOption.value;
    if(filterOption.value != ''){
      this.filterListData.push(filterOption);
    }
    if(typeof(filterOption.value)=='boolean'){
      this.filterListData =[];
      this.filterListData.push(filterOption);
    }
    let reportOption= new ReportFilterOption(this.reportSkeletonFilter.name,filterOption.value);
    this.OnFilterUpdated.emit(reportOption)
  }

  sortFilterValues(values :ReportFilterOption[]){
    return values.sort((a,b)=>a?.name?.toUpperCase().localeCompare(b?.name?.toUpperCase(), undefined, { numeric: true }));
  }
}
