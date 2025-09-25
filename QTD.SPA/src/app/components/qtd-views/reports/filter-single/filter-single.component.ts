import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { ReportFilterOption } from 'src/app/_DtoModels/Report/ReportFilterOption';
import { ReportSkeletonFilter, filterPropertyTypeEnum } from 'src/app/_DtoModels/ReportSkeleton/ReportSkeletonFilter';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { cloneDeep } from 'lodash';

@Component({
  selector: 'app-filter-single',
  templateUrl: './filter-single.component.html',
  styleUrls: ['./filter-single.component.scss']
})
export class FilterSingleComponent implements OnInit {

  @Input() reportSkeletonFilter: ReportSkeletonFilter;
  @Input() reportMode: string;
  @Input() filterOption: string;
  @Input() reportData: any;
  public reportsData;
  @Output() OnFilterUpdated: EventEmitter<ReportFilterOption> = new EventEmitter();
  @Output() closed: EventEmitter<any> = new EventEmitter();
  private reportOptions: ReportFilterOption;
  public filterOptionData:Array<any>;
  public positionFilterOptionData: Array<ReportFilterOption>;
  isFilterOptionParent: boolean = false;
  public positionStatus: string;
  public optionValueObj : Map<string, string|undefined> = new Map<string, string|undefined>() ;
  public optionDropdownObj : Map<string, any[]> = new Map<string, any[]>() ;
  isDefaultOrder:Boolean = false;
  noneSelectedFlag:boolean = false;
  searchText:string='';
  originalFilterOptionData: any[] = []; 

  constructor(private fb: UntypedFormBuilder, private reportService:ApiReportsService) { }

  async ngOnInit() {
    if(this.filterOption){
      await this.reportService.getReportFilterOptionsAsync(this.filterOption).then(async (response) => {
        this.filterOptionData = response.map((item) => {
          this.isDefaultOrder=item.isDefaultOrder;
          item.id = Number(item.value);
          return item;
        });
        if (response[0].filterOptionParents != null) {
          this.isFilterOptionParent = true;
          this.filterOptionData[0].filterOptionParents.forEach(
            (option,index) =>
              {   
                this.optionDropdownObj.set(option.name, this.getParentDropdownValues(option.name));
                const isFirstOption = index === 0;
                const isOptionCascade = isFirstOption ? option.isCascade : this.filterOptionData[0].filterOptionParents[index - 1].isCascade;
                this.optionValueObj.set(option.name.toUpperCase(), isOptionCascade ? undefined : "-- All --");
                if((option.name.toUpperCase() == 'ACTIVE STATUS' || option.name.toUpperCase() == 'TASK STATUS' || option.name.toUpperCase() == 'STATUS') && !isOptionCascade){
                  this.optionValueObj.set(option.name.toUpperCase(),"Active");
                }
              }
            );
          }
        });
      }
      
      this.reportsData =  this.reportSkeletonFilter.value ?? this.reportSkeletonFilter.defaultValue;
      this.reportOptions = new ReportFilterOption(this.reportsData , this.reportsData );
      if(this.filterOption){
        this.setReportOptions(this.filterOptionData);
        if(this.filterOption.toLowerCase() == 'employeename'){
          const [first, ...rest] = this.filterOptionData;
          const sortedRest = rest.sort((a, b) =>a.name.trim().toUpperCase().localeCompare(b.name.trim().toUpperCase(),'en-US',{ numeric: true }));
          this.filterOptionData = [first, ...sortedRest]; 
          this.originalFilterOptionData = cloneDeep(this.filterOptionData);
      }
      if(this.filterOption.toLowerCase() !== 'providers' && this.filterOption.toLowerCase() != 'employeename'){
        this.filterOptionData = this.isDefaultOrder ? this.filterOptionData : this.filterOptionData.sort((a, b) => a.name.trim().toUpperCase().localeCompare(b.name.trim().toUpperCase(), 'en-US', { numeric: true }));
      }
      if(this.filterOptionData[0].filterOptionParents !=null){
        this.getFilteredData();
      }
    }
   
  }

  public getPropertyTypeName(index: number){
    return filterPropertyTypeEnum[index].toLowerCase();
  }

  onFilterSelectChange(event:any, type: string){
    if(type === 'boolean'){
      this.reportOptions = new ReportFilterOption(event.target.checked, event.target.checked);
    } 
    else if (type== 'string'){
      this.reportsData=event?.value;
      this.reportOptions = this.filterOptionData.find(x=>x.value == event?.value);
      this.noneSelectedFlag = this.reportOptions?.value=="" ? true:false;
    }
    else {
      this.reportOptions = new ReportFilterOption(event.target.value, event.target.value);
    }
    
  }
  getParentDropdownValues(name: string) {
    const uniqueValues: string[] = [];
    const allOption = "-- All --";
    uniqueValues.push("-- All --");
    this.filterOptionData.forEach(item => {
      const parentOptionValue = item.filterOptionParents.find(parent => parent.name.toLowerCase() === name.toLowerCase());
      if (parentOptionValue && parentOptionValue?.values) {
        const providerValues = parentOptionValue.values.filter(value => value !== null && value !== undefined);
        uniqueValues.push(...providerValues);
      }
    });
    const finalValues = [...new Set(uniqueValues)];
    if(name.toLowerCase() !== 'provider'){
      finalValues.sort((a, b) => 
      a === allOption ? -1 : b === allOption ? 1 : a.localeCompare(b, 'en-US', { numeric: true }));
    }
    return finalValues;
  }
  getSelectedValue(filterOptionName:string){
    return this.optionValueObj.get(filterOptionName.toUpperCase());
  }

  getCurrentState(filterOptionName:string){
      let options = Array.from(this.optionValueObj.keys());
      let values = Array.from(this.optionValueObj.values());
      let index = options.indexOf(filterOptionName.toUpperCase());
      const isFirstOption = index === 0;
      const isOptionDisabled = isFirstOption ? false :this.filterOptionData[0].filterOptionParents[index-1].isCascade? values[index-1] === undefined:false;
      return isOptionDisabled;
  }

  setDropdownValue(event: any,filterOptionName:string) {
    this.optionValueObj.set(filterOptionName.toUpperCase(),event.value);
    let dropDowns = Array.from(this.optionDropdownObj.keys());
    let options = Array.from(this.optionValueObj.keys());
    let index = dropDowns.indexOf(filterOptionName);
    for(let i = index ; i <dropDowns.length ; i++){
        if(this.filterOptionData[0].filterOptionParents[i].isCascade){
            this.optionValueObj.set(options[i+1],undefined);  
        }else{break;}
    }
    this.getFilteredData();
    for(let i = index ; i <dropDowns.length ; i++){
        if(this.filterOptionData[0].filterOptionParents[i].isCascade){
            let cascadedValues = this.getCascadedValues(i);
            this.updateOptionValues(i + 1, dropDowns[i + 1], options[i + 1], cascadedValues);
        }else{break;}
    }
  }

  getCascadedValues(index: number): string[] {
    return this.positionFilterOptionData?.map(y => {
      if (
        Array.isArray(y.filterOptionParents) &&
        y.filterOptionParents.length > index + 1 &&
        y.filterOptionParents[index + 1] &&
        Array.isArray(y.filterOptionParents[index + 1].values)
      ) {
        return y.filterOptionParents[index + 1].values;
      }
      return [];
    }).reduce((acc, val) => acc.concat(val), []);
  }
  

  updateOptionValues(nextIndex: number, nextDropDown: string, nextOption: string, cascadedValues: string[]): void {
      cascadedValues.unshift("-- All --");
      cascadedValues = [...new Set(cascadedValues)];
      cascadedValues.sort((a, b) => 
            a === "-- All --" ? -1 : b === "-- All --" ? 1 : a.localeCompare(b, 'en-US', { numeric: true }));
      this.optionDropdownObj.set(nextDropDown, cascadedValues);
      this.optionValueObj.set(nextOption, undefined);
  }

   getFilteredData(){
    this.positionFilterOptionData = this.filterOptionData;
    for (const [key, value] of this.optionValueObj) {

      if(key.toUpperCase() == "ACTIVE STATUS"){
        if(value && value.toUpperCase() == "ACTIVE"){
          this.positionFilterOptionData = this.positionFilterOptionData.filter(r => r.active === true);
        }
        else if(value && value.toUpperCase() == "INACTIVE"){
          this.positionFilterOptionData = this.positionFilterOptionData.filter(r => r.active === false);
        }
        continue;
      }
      if(value && value.toUpperCase() != "-- ALL --"){
        this.positionFilterOptionData = this.positionFilterOptionData.filter(item =>
          this.filterMatch(item.filterOptionParents?.find(option=>option.name.toUpperCase() == key.toUpperCase()).values, value));
      }
    }
    this.setReportOptions(this.positionFilterOptionData);
  }

  setReportOptions(filterOptionData : ReportFilterOption[]){
    console.log("this.reportsData",this.reportsData)
    console.log("this.filterOptionData",this.filterOptionData)
    let positionValue =new ReportFilterOption("","");
    if(filterOptionData.length != 0)
    {
      let findIndex =filterOptionData.findIndex(position=>position.value == this.reportsData);
      if(findIndex==-1 && (this.filterOption == 'certifications' || this.filterOption == 'trainingprogramtype' || this.filterOption == 'employeename')){
        var noneSelected = new ReportFilterOption("None Selected","");
        filterOptionData.unshift(noneSelected);
        this.noneSelectedFlag = true;
      }
      positionValue = findIndex == -1 ? filterOptionData[0] : filterOptionData[findIndex];
      this.reportsData = positionValue.value;
    }
    this.reportOptions=positionValue;
  }
  

  filterMatch(values: string[], selectedValue: string): boolean {
    return !selectedValue || values?.includes(selectedValue);
  }

  filterSave(){
    this.OnFilterUpdated.emit(this.reportOptions);
    this.closed.emit();
  }

  clearSelection(){
    let reportOption= new ReportFilterOption("","");
    this.OnFilterUpdated.emit(reportOption);
    this.closed.emit();
  }

  handleKeydown(event: KeyboardEvent) {
    if (event.key === ' ') {
        event.stopPropagation(); 
    }
  }

  dropDownSearch(e:any){
    const searchValue = e?.target?.value?.trim() ?? '';
    this.searchText = searchValue;
    if(this.searchText.trim() == ''){
      this.filterOptionData = [...this.originalFilterOptionData];
    }else{
      this.filterOptionData = this.originalFilterOptionData.filter(x =>x.name.toLowerCase().includes(this.searchText.toLowerCase()));
    }
  }

  resetDropDownSearch(){
    setTimeout(() => {
      this.searchText = '';
      this.filterOptionData = [...this.originalFilterOptionData];
    }, 500);
  }


}
