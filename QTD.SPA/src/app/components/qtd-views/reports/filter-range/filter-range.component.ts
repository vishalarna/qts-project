import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ReportFilterOption } from 'src/app/_DtoModels/Report/ReportFilterOption';
import { ReportSkeletonFilter, filterPropertyTypeEnum } from 'src/app/_DtoModels/ReportSkeleton/ReportSkeletonFilter';

import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';

@Component({
  selector: 'app-filter-range',
  templateUrl: './filter-range.component.html',
  styleUrls: ['./filter-range.component.scss']
})
export class FilterRangeComponent implements OnInit {
  datePipe = new DatePipe('en-us');
  @Input() reportSkeletonFilter: ReportSkeletonFilter;
  @Input() reportMode: string;
  public reportsData;
  public firstValue;
  public secondValue;
  public filterRangeList: UntypedFormGroup;
  private reportOptions: ReportFilterOption;
  @Output() OnFilterUpdatedEvent: EventEmitter<ReportFilterOption> = new EventEmitter();
  @Output() closed: EventEmitter<any> = new EventEmitter();
  endDateError:string='';
  startDateError:string='';
  filterRangeData = '';
  constructor(private fb: UntypedFormBuilder,private reportService:ApiReportsService) { }

  async ngOnInit(): Promise<void>{
    if(this.reportSkeletonFilter.value && this.reportSkeletonFilter.value !== ""){
      this.reportsData = this.reportSkeletonFilter.value.split(",");
      this.firstValue = this.reportSkeletonFilter.valueType === 2 ? this.datePipe.transform(this.reportsData[0], "yyyy-MM-dd")
                        : this.firstValue;
      this.secondValue = this.reportSkeletonFilter.valueType === 2 ? this.datePipe.transform(this.reportsData[1], "yyyy-MM-dd")
                        : this.secondValue;
    } else {
      this.firstValue = this.reportSkeletonFilter.valueType === 2 ? ""
      : this.firstValue
    }
  }

  public getFilterForm() {
    this.filterRangeList = this.fb.group({
      start: new UntypedFormControl('', [Validators.required]),
      end: new UntypedFormControl('', [Validators.required]),
    })
  }

  public getPropertyTypeName(index: number){
    return filterPropertyTypeEnum[index].toLowerCase();
  }

  OnDateInput(position:number,event:any){
    let value =event;
    if(position == 1){
      if (value && new Date(value) > new Date(this.secondValue)) {
        this.firstValue = null;
        this.startDateError="Start date cannot be greater than end date";
      }else{
        this.firstValue = value;
        this.startDateError='';
      }
    } else {
      if (value && new Date(value) < new Date(this.firstValue)) {
        this.secondValue = null;
        this.endDateError="End date cannot be less than start date";
      }else{
        this.secondValue = value;
        this.endDateError='';
      }
    }

    if(this.firstValue ==''  || this.secondValue ==''){
      this.filterRangeData = '';
    }else{
      this.filterRangeData = this.firstValue + "," + this.secondValue;
    }
    const filterRangeDisplayName = "From " + this.datePipe.transform(this.firstValue, "MM-dd-yyyy") + " to " +this.datePipe.transform(this.secondValue, "MM-dd-yyyy")
    this.reportOptions = new ReportFilterOption(filterRangeDisplayName, this.filterRangeData);
  }

  filterSave(){
    this.OnFilterUpdatedEvent.emit(this.reportOptions);
    this.closed.emit();
  }
}
