import { Component, EventEmitter, Input, OnInit, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Observable, Subscription } from 'rxjs';
import { ReportFilterOption } from 'src/app/_DtoModels/Report/ReportFilterOption';
import { ReportListFilterOption } from 'src/app/_DtoModels/Report/ReportListFilterOption';
import { ReportSkeletonFilter } from 'src/app/_DtoModels/ReportSkeleton/ReportSkeletonFilter';
import { ApiReportsService } from 'src/app/_Services/QTD/Reports/api.reports.service';
import { cloneDeep } from 'lodash';

@Component({
    selector: 'app-filter-list',
    templateUrl: './filter-list.component.html',
    styleUrls: ['./filter-list.component.scss']
})
export class FilterListComponent implements OnInit {

    public positionData: Array<ReportFilterOption>;
    public filterPositionData: Array<ReportFilterOption>;
    @Input() reportSkeletonFilter: ReportSkeletonFilter;
    @Input() reportMode;
    @Input() filterOption;
    public reportFilters;
    public positionIdList: Array<number>;
    @Output() OnSelectedListUpdated: EventEmitter<any> = new EventEmitter();
    @Output() closed: EventEmitter<any> = new EventEmitter();
    public selectedFilterOptions:ReportFilterOption[];
    public reportListFilterOption:ReportListFilterOption=new ReportListFilterOption();

    public ilaFilterOptionData: Array<ReportFilterOption>;
    length: number;
    isFilterOptionParent: boolean = false;
    isAllOptionChecked : boolean = false;
    public optionValueObj : Map<string, string|undefined> = new Map<string, string|undefined>() ;
    public optionDropdownObj : Map<string, any[]> = new Map<string, any[]>() ;
    filterListData:MatTableDataSource<ReportFilterOption> = new MatTableDataSource();
    filterListColumns=['checkbox','name'];
    searchString:string='';
    isFilterHasExtraColumns: boolean = false;
    maxAllowedSelections:number;
    maxAllowedSelectionExceed:boolean = false;
    public originalOptionDropdownObj: Map<string, any[]> = new Map<string, any[]>();
    searchParentTextMap: Map<string, string> = new Map<string, string>();
    @ViewChildren('parentSelect') parentSelects!: QueryList<MatSelect>;
    @ViewChild('filterListPaginator') set filterListPaginator(paging: MatPaginator) {
        if (paging) this.filterListData.paginator = paging;
      }
    
    @ViewChild(MatSort) sort: MatSort;
    constructor(private reportService: ApiReportsService) { }

    async ngOnInit() {
        const self = this;
        await this.reportService.getReportFilterOptionsAsync(this.filterOption).then(async (response) => {
            self.positionData = response.map((item) => {
                item.id = Number(item.value);
                return item;
            });
            let filterValue = this.reportSkeletonFilter.value ?? this.reportSkeletonFilter.defaultValue;
            this.maxAllowedSelections = this.reportSkeletonFilter.maxAllowedSelections;
            self.selectedFilterOptions=[];
            if (filterValue && filterValue !== '') {
                if(filterValue == 'listAllSelected'){
                    this.positionData.forEach(x=>this.selectedFilterOptions.push(x));
                }else{
                    filterValue.split(",").map(Number).forEach(x=>
                        {
                            let option = self.positionData.find(s=>s.id==x);
                            this.selectedFilterOptions.push(option);
                        })
                }
            }
            this.length = self.positionData.length;
            this.reportListFilterOption.dataCount=this.length;
            this.filterPositionData = [...this.positionData];
            this.filterListData.data = [...this.filterPositionData];
            if(this.filterOption && (this.filterOption.toLowerCase() !== 'classschedule' && this.filterOption.toLowerCase() !== 'courses' && this.filterOption.toLowerCase() !== 'classrosterschedule' && this.filterOption.toLowerCase() !== 'positions' && this.filterOption.toLowerCase() !== 'taskeoincludeoption')){
                this.filterPositionData = this.filterPositionData.sort((a,b) => a.name?.trim().toLowerCase().localeCompare(b.name?.trim().toLowerCase(), 'en-US', { numeric: true }));
            }
            if (response[0]?.filterOptionParents != null) {
                this.isFilterOptionParent = true;
                this.filterPositionData[0].filterOptionParents.forEach(
                    (option,index) =>
                    {   if(option.isTableVisible){
                            this.filterListColumns.push(option.name);
                        }
                        this.optionDropdownObj.set(option.name, this.getParentDropdownValues(option.name));
                        this.originalOptionDropdownObj = cloneDeep(this.optionDropdownObj);
                        this.searchParentTextMap.set(option.name, '');
                        const isFirstOption = index === 0;
                        const isOptionCascade = isFirstOption ? option.isCascade : this.filterPositionData[0].filterOptionParents[index - 1].isCascade;
                        this.optionValueObj.set(option.name.toUpperCase(), isOptionCascade ? undefined : "-- All --");
                        if((option.name.toUpperCase() == 'ACTIVE STATUS' || option.name.toUpperCase() == 'TASK STATUS' || option.name.toUpperCase() == 'STATUS' || option.name.toUpperCase() == 'ACTIVE/INACTIVE STATUS') && !isOptionCascade){
                            this.optionValueObj.set(option.name.toUpperCase(),"Active");
                        }
                        if((option.name.toUpperCase() == 'ISSUE STATUS') && !isOptionCascade){
                            this.optionValueObj.set(option.name.toUpperCase(),"Open");
                        }
                    }
                );
            }
            if(response[0]?.filterTableColumns != null){
                this.isFilterHasExtraColumns=true;
                this.filterPositionData[0].filterTableColumns.forEach(
                    (option,index) =>
                    {   
                        this.filterListColumns.push(option.name);
                    }
                );
            }
            this.filterListData = new MatTableDataSource<ReportFilterOption>(this.filterPositionData);  
            setTimeout(()=>{
                this.filterListData.sort = this.sort;
              },1);    
            this.filterListData.sortingDataAccessor = this.customSortAccessor;
        });
        this.getFilteredData();
    }
    

    customSortAccessor = (reportFilterOption: ReportFilterOption, column: string): any => 
    {
     if(column != 'name')
     {
        const filterOptionParent = reportFilterOption.filterOptionParents?.find(x => x.name == column);
        const filterTableColumn = reportFilterOption.filterTableColumns?.find(x => x.name == column);
        if(filterOptionParent != null)
        {
           return filterOptionParent.values.join(', ').toLowerCase();
        }
        else if(filterTableColumn != null)
        {
            if (column === 'Start DateTime' || column === 'End DateTime') {
                let dateValues: Date[] = [];
                dateValues = filterTableColumn.values.map(value => this.parseDate(value));
                return dateValues.length === 1 ? dateValues[0].getTime().toString() : dateValues.map(d => d.getTime()).toString();
              } else {
                return filterTableColumn.values.join(', ');
              }
        }
        else
        {
           return "";
        }
     }
     else
     {
        if(this.filterOption && (this.filterOption.toLowerCase() == 'positions')){
            const numberPart = parseInt(reportFilterOption.name.split('-')[0].trim(), 10);
            return numberPart;
        }else{
            return reportFilterOption[column];
        }
     }

    };

     parseDate(dateString: string): Date {
        const [month, day, yearTime] = dateString.split('/');
        const [year, time] = yearTime.split(' ');
      
        const hours = time ? parseInt(time.split(':')[0], 10) : 0;
        const minutes = time ? parseInt(time.split(':')[1], 10) : 0;
      
        return new Date(parseInt(year, 10), parseInt(month, 10) - 1, parseInt(day, 10), hours, minutes);
     }

    getFilterCheckedValues(id: number) {
        const checkValue = this.selectedFilterOptions && this.selectedFilterOptions.findIndex(dd => dd?.id === id);
        if (checkValue !== -1) {
            return true;
        } else {
            return false;
        }
    }

    OnFilterValueChange(positionId: number) {
        const index = this.selectedFilterOptions?.findIndex(d=>d.id==positionId);
        if (index !== -1) {
            this.selectedFilterOptions.splice(index, 1);
        } else {
            let option = this.filterPositionData.find(x=>x.id==positionId);
            this.selectedFilterOptions.push(option);
        }
        this.updateMaxSelectionState();
        this.reportListFilterOption.selectedFilterOptions=this.selectedFilterOptions;
        this.isAllOptionChecked = this.checkAllOptionChecked();
    }

    searchFilter(event: any) {
        this.searchString = event.target.value;
        this.getFilteredData();
    }
    clearFilter() {
        this.searchString='';
        this.getFilteredData();
      }

    getParentDropdownValues(name: string) {
        const uniqueValues: string[] = [];
        const allOption = "-- All --";
        uniqueValues.push("-- All --");
        this.positionData.forEach(item => {
            const parentOptionValue = item.filterOptionParents.find(parent => parent.name?.toLowerCase() === name?.toLowerCase());
            if (parentOptionValue && parentOptionValue?.values) {
                const providerValues = parentOptionValue.values.filter(value => value !== null && value !== undefined);
                uniqueValues.push(...providerValues);
            }
        });
        const finalValues = [...new Set(uniqueValues)];
        if(name.toLowerCase() != 'provider'){
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
        const isOptionDisabled = isFirstOption ? false :this.filterPositionData[0].filterOptionParents[index-1].isCascade? values[index-1] === undefined:false;
        return isOptionDisabled;
    }

    setDropdownValue(event: any,filterOptionName:string) {
        this.optionValueObj.set(filterOptionName.toUpperCase(),event.value);
        let dropDowns = Array.from(this.optionDropdownObj.keys());
        let options = Array.from(this.optionValueObj.keys());
        let index = dropDowns.indexOf(filterOptionName);
        for(let i = index ; i <dropDowns.length ; i++){
            if(this.filterPositionData[0].filterOptionParents[i].isCascade){
                this.optionValueObj.set(options[i+1],undefined);  
            }else{break;}
        }
        this.getFilteredData();
        for(let i = index ; i <dropDowns.length ; i++){
            if(this.filterPositionData[0].filterOptionParents[i].isCascade){
                let cascadedValues = this.getCascadedValues(i);
                this.updateOptionValues(i + 1, dropDowns[i + 1], options[i + 1], cascadedValues);
            }else{break;}
        }
    }
    setCheckBoxValue(event : any, filterOptionName:string){
        this.optionValueObj.set(filterOptionName.toUpperCase(),event.checked ? "true" : "-- All --");
        this.getFilteredData();
    }

    getCascadedValues(index: number): string[] {
        return this.ilaFilterOptionData.map(y => y.filterOptionParents[index + 1].values).reduce((acc, val) => acc.concat(val), []);;
    }

    updateOptionValues(nextIndex: number, nextDropDown: string, nextOption: string, cascadedValues: string[]): void {
        cascadedValues.unshift("-- All --");
        cascadedValues = [...new Set(cascadedValues)];
        cascadedValues.sort((a, b) => {
            if (a === "-- All --") return -1;
            if (b === "-- All --") return 1;  
        
            if (a == null) return 1; 
            if (b == null) return -1; 
        
            return a.localeCompare(b, 'en-US', { numeric: true });
        });
        this.optionDropdownObj.set(nextDropDown, cascadedValues);
        this.originalOptionDropdownObj.set(nextDropDown, cascadedValues);
        this.optionValueObj.set(nextOption, undefined);
    }
    getFilteredData(){
        this.ilaFilterOptionData = this.filterPositionData;
        if(this.isFilterOptionParent){
            for (const [key, value] of this.optionValueObj) {
                if(key.toUpperCase() == "ACTIVE STATUS"){
                    if(value && value.toUpperCase() == "ACTIVE"){
                        this.ilaFilterOptionData = this.ilaFilterOptionData.filter(r => r.active === true);
                    }
                    else if(value && value.toUpperCase() == "INACTIVE"){
                        this.ilaFilterOptionData = this.ilaFilterOptionData.filter(r => r.active === false);
                    }
                    continue;
                }
                if(value && value.toUpperCase() != "-- ALL --"){
                    this.ilaFilterOptionData = this.ilaFilterOptionData.filter(item =>
                    this.filterMatch(item.filterOptionParents.find(option=>option.name.toUpperCase() == key.toUpperCase()).values, value));
                }
            }
        }

        this.ilaFilterOptionData = this.ilaFilterOptionData.filter((c) =>
            c.name.toLowerCase().includes(this.searchString.toLowerCase())
        );
        this.reportListFilterOption.selectedFilterOptions=this.selectedFilterOptions;
        this.length = this.ilaFilterOptionData.length;
        this.filterListData.data =this.ilaFilterOptionData;
        this.isAllOptionChecked = this.checkAllOptionChecked();
    }
    filterMatch(values: string[], selectedValue: string): boolean {
        return !selectedValue || values.includes(selectedValue);
    }

    onSelectAll() {
        if (this.checkAllOptionChecked()) {
            this.filterListData.data.forEach(option=> {
                let removeItemIndex = this.selectedFilterOptions.findIndex(x=>x.id == option.id);
                if(removeItemIndex !== -1){
                    this.selectedFilterOptions.splice(removeItemIndex,1);
                }
            })
        }
        else {
            for (const item of this.filterListData.data) {
                let addItemIndex = this.selectedFilterOptions.findIndex(x=>x.id == item.id);
                if(addItemIndex === -1){
                    this.selectedFilterOptions.push(item);
                }
            }
        }
        this.updateMaxSelectionState();
        this.isAllOptionChecked =this.checkAllOptionChecked();
        this.reportListFilterOption.selectedFilterOptions=this.selectedFilterOptions;
    }
    
    getOptionDataForTable(row:ReportFilterOption,name:string):string {
        let value ="";
        let filter =row.filterOptionParents.find(x=>x.name.toLowerCase()==name.toLowerCase());
        value= filter?filter.values.join(', '):"";
        return value;
    }

    filterSave(){
        this.OnSelectedListUpdated.emit(this.reportListFilterOption);
        this.closed.emit();
    }

    checkAllOptionChecked(){
        if (this.filterListData.data.length === 0) {
            return false; 
        }
        for (let index = 0; index < this.filterListData.data.length; index++) {
            let option = this.filterListData.data[index];
            if (this.selectedFilterOptions?.findIndex(x => x.id === option.id) === -1) {
                return false;
            }
        }
        return true;
    }
    getColumnsFromFilters(){
       return  this.filterPositionData[0]?.filterOptionParents.filter(x=>x.isTableVisible);
    } 
    getExtraTableColumns(){
        return  this.filterPositionData[0]?.filterTableColumns;
    }
    getExtraColumnDataForTable(row:ReportFilterOption,name:string):string{
        let value ="";
        let filter =row.filterTableColumns.find(x=>x.name.toLowerCase()==name.toLowerCase());
        value= filter?filter.values.join(', '):"";
        return value;
    }

    updateMaxSelectionState() {
        if (this.maxAllowedSelections && this.selectedFilterOptions.length > this.maxAllowedSelections) {
            this.maxAllowedSelectionExceed = true;
        } else {
            this.maxAllowedSelectionExceed = false;
        }
    }

    parentDropdownSearch(e: any, filterParentName: string) {
        let searchValue = e.target.value.trim();
        this.searchParentTextMap.set(filterParentName, searchValue);
        this.filterParentSearch(searchValue, filterParentName);
    }

    filterParentSearch(searchString: string, filterParentName: string) {
        if (!searchString) {
            this.optionDropdownObj.set(filterParentName, cloneDeep(this.originalOptionDropdownObj.get(filterParentName) || []));
        } else {
            let filteredValues = this.originalOptionDropdownObj.get(filterParentName)?.filter(x => 
                x.toLowerCase().includes(searchString.toLowerCase())
            ) || [];
            this.optionDropdownObj.set(filterParentName, filteredValues);
        }
    }

    resetSearch(filterParentName: string) {
        setTimeout(() => {
            this.searchParentTextMap.set(filterParentName, '');
            this.filterParentSearch('', filterParentName);
        }, 500);
    }

    handleKeydown(event: KeyboardEvent) {
        if (event.key === ' ') {
            event.stopPropagation(); 
        }
    }
    
}
