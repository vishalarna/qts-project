import { SelectionModel } from '@angular/cdk/collections';
import { DatePipe, formatDate } from '@angular/common';
import {Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {
  UntypedFormBuilder,
  UntypedFormControl,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { ILAProviderVM } from '@models/ILA/ILAProviderVM';
import { ILA } from 'src/app/_DtoModels/ILA/ILA';
import { CehUploadGetOptions } from 'src/app/_DtoModels/NERC/CEHUploadGetOptions';
import { ClassSchedules } from 'src/app/_DtoModels/SchedulesClassses/ClassSchedules';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ApiClientSettingsService } from 'src/app/_Services/QTD/ClientSettings/api.clientsettings.service';
import { ClassScheduleService } from 'src/app/_Services/QTD/class-schedule.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { NercService } from 'src/app/_Services/QTD/nerc.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';

@Component({
  selector: 'app-cehupload',
  templateUrl: './cehupload.component.html',
  styleUrls: ['./cehupload.component.scss'],
})
export class CehuploadComponent implements OnInit {
  cehUploadForm: UntypedFormGroup;
  uploadOptions: CehUploadGetOptions;
  selectedClassSchedule: number[];
  ilaList: ILAProviderVM[];
  datePipe = new DatePipe('en-US');
  ilaList_Original:ILAProviderVM[];
  filteredList : ILAProviderVM[];
  providersList: any;
  provider_list_original: any
  dialogHeader: string;
  dialogDescription: string;
  classSchedules : ClassSchedules[];
  dataSource: MatTableDataSource<any>;
  isTableVisible: boolean = false;
  cehUploadData: any;
  showDialog: boolean = false;
  selection_CehUploads = new SelectionModel<any>(true, []);
  featureData:any[];
  cehEnabled:boolean;
  invalidClassScheduleIds:any[] = [];
  invalidClassScheduleDates:any[] = [];
  allInvalid:boolean;

  @ViewChild('createFile')
  noRecordsTemplate: TemplateRef<any>;

  displayedExpandedColumns: string[] = [
    'course ID',
    'enrollment Date',
    'certification Number',
    'operating Topics CEH',
    'standards',
    'simulation'
  ];
  classesColumns: string[] = [
    'index',
    'Start Date',
    'End Date',
    'Locations',
    'Instructor'
  ];
  loader:boolean = false;

  constructor(
    private fb: UntypedFormBuilder,
    private dialog: MatDialog,
    private _ilaService: IlaService,
    private _nercProvider: ProviderService,
    private _classScheduleService: ClassScheduleService,
    private _nercService: NercService,
    private clientSettingsService: ApiClientSettingsService,
    private labelPipe: LabelReplacementPipe,
    private router: Router) { }

  async ngOnInit() {    
    this.featureData = [];
    this.getFeatureData();
    this.uploadOptions = new CehUploadGetOptions();
    this.cehUploadData = [];
    this.initializeCEHForm();
    this.providersList = await this._nercProvider.getActiveProviders();
    this.providersList = this.providersList.filter(provider => provider.isNERC === true);
    this.provider_list_original = Object.assign(this.providersList);    
  }

  async getILAsByProvider(event: any)
  {
    this.ilaList_Original = await this._ilaService.getByProvider(event.value);
    var selfPacedLocal =this.cehUploadForm.get("isSelfPaced").value;
    this.ilaList = this.ilaList_Original.filter(r => r.isSelfPaced == selfPacedLocal && r.active == true);
    this.filteredList=this.ilaList;
  }

  async getFeatureData(){
    this.featureData = await this.clientSettingsService.getAllFeatureAsync();
    this.checkDisabled();
  }

  async checkDisabled(){
    this.cehEnabled = this.featureData.find(item=>item.feature=="CEH Upload")?.enabled;
    if(!this.cehEnabled){
      this.router.navigateByUrl('/disabledFeature')
    }
  }

  initializeCEHForm() {
    this.cehUploadForm = this.fb.group({
      provider: new UntypedFormControl(null, Validators.required),
      isSelfPaced: new UntypedFormControl(false, Validators.required),
      ila: new UntypedFormControl(null, Validators.required),
      startDate: new UntypedFormControl(this.datePipe.transform(Date.now(), 'MM-dd-YYYY'), Validators.required),
      endDate: new UntypedFormControl(this.datePipe.transform(Date.now(), 'MM-dd-YYYY'), Validators.required),
      searchTxt: new UntypedFormControl(''),
      ilaSearch:new UntypedFormControl('')
    });
  }

  async getClassScheduleByILAId(){
    this.selection_CehUploads.clear();
    var ilaId = this.cehUploadForm.get("ila").value;
    this.classSchedules = await this._classScheduleService.GetByILAIdAsync(ilaId);
    this.dataSource = new MatTableDataSource<any>(this.classSchedules);
  }

  filterClassSchedule(){
    var startDate = this.cehUploadForm.get("startDate").value;
    var endDate = this.cehUploadForm.get("endDate").value;

    if(startDate && !endDate){
      var tempList = this.classSchedules?.filter(r => r.startDateTime >= startDate );
      this.dataSource = new MatTableDataSource<any>(tempList);
    } else if(startDate && endDate){
      var tempList = this.classSchedules?.filter(r => r.startDateTime >= startDate && r.endDateTime <= endDate);
      this.dataSource = new MatTableDataSource<any>(tempList);
    } else {
      this.dataSource = new MatTableDataSource<any>(this.classSchedules);
    }
  }

  radioChange(event:any){
    this.filteredList = this.ilaList_Original;
    this.filteredList =  this.filteredList.filter(r => r.isSelfPaced == event.value && r.active == true);
    this.ilaList = this.filteredList;
  }

  async setUploadOptions(){
    this.loader = true;
    this.invalidClassScheduleDates = [];
    this.selectedClassSchedule = [];
    this.selection_CehUploads.selected.forEach(value => {
      this.selectedClassSchedule.push(value.id)
    });
    
    this.uploadOptions.SeClassScheduleIds(this.selectedClassSchedule);
    await this._nercService.getCehUploadAsync(this.uploadOptions).then((res) => {
      this.cehUploadData = res.filter(x=>x.isValid=="True");
      this.invalidClassScheduleIds = res.filter(x=>x.isValid=="False").map(item => item.classScheduleId);
      this.invalidClassScheduleIds = [...new Set(this.invalidClassScheduleIds)]
      this.invalidClassScheduleIds.forEach(item=>{
        var cs = this.classSchedules.find(m=>m.id==item);
        if(cs!=null){
          const dates = {
                     startDate: cs.startDateTime,
                     endDate: cs.endDateTime
                    }
          this.invalidClassScheduleDates.push(dates);
        }
      })
    });
    this.allInvalid = this.selection_CehUploads.selected.length==this.invalidClassScheduleIds.length ? true: false;
    if(this.invalidClassScheduleIds?.length > 0){
      this.makeCEHDialogActive(this.noRecordsTemplate);
    }else{
      this.isTableVisible = true;
    }
    this.loader = false;
  }

  isAllSelectedProviderList() {
    const numSelectedCeh = this.selection_CehUploads.selected.length;
    const dataSourceLength = this.selectedDataLength();
    const numRowsCEHs = dataSourceLength;
    return numSelectedCeh === numRowsCEHs;
  }

  masterToggleProviderList() {

    this.isAllSelectedProviderList()
      ? this.selection_CehUploads.clear()
      : this.dataSource?.data.forEach((row) => {
          if (!this.isDateInFuture(row.startDateTime)) {
            this.selection_CehUploads.select(row);
          }
        });
  }

  selectedDataLength(){
    return this.dataSource?.data.filter(item => {
      const presentDate = new Date();
      const itemStartDate = new Date(item.startDateTime);
      const presentDateString = this.datePipe.transform(presentDate, 'yyyy-MM-dd');
      const itemStartDateString = this.datePipe.transform(itemStartDate, 'yyyy-MM-dd');
      return itemStartDateString <= presentDateString;
  }).length;
  }

  getBackValue(event :any){
    this.isTableVisible= event;
  }

  formatDate(dateString: string): string {
    const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return new Date(dateString).toLocaleDateString('en-US', options);
  }

  async makeCEHDialogActive(dialog: any) {
    this.dialogHeader = "Missing Data";
    let dialogDescription = `
        <p>The CEH Upload File will not include records from the following classes due to missing information. Review the ${await this.labelPipe.transform('ILA')} and Class to confirm the following information is entered:</p>
        <ul style='list-style-type:disc;'>
            <li>CEHs</li>
            <li>Employees are enrolled</li>
            <li>Grades are entered</li>
        </ul>
        <br>
        <p><strong>Classes that have errors:</strong></p>
        <p>Start Date &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;End Date</p>
        <ul style='list-style-type:none;'>
    `;
    this.invalidClassScheduleDates.forEach(schedule => {
        dialogDescription += `<li>${this.formatDate(schedule.startDate)} &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ${this.formatDate(schedule.endDate)}</li>`;
      });
    dialogDescription += "</ul>";
    this.dialogDescription = dialogDescription;
    const dialogRef = this.dialog.open(dialog, {
      width: '380px',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  providerSearch(value: any) {
    var filterString = this.cehUploadForm.get('searchTxt')?.value;

    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    }
    else {
      filterString = "";
    }
    this.providersList = this.provider_list_original.filter((f) => {
      return f.name.toLowerCase().trim().includes(filterString);
    });
  }

  onSearchInput(value: any) {
    var filterString = this.cehUploadForm.get('ilaSearch')?.value;
    if (filterString !== undefined && filterString !== null) {
      filterString = String(filterString).trim().toLowerCase();
    }
    else {
      filterString = "";
    }
    this.ilaList = this.filteredList.filter((f) => {
      return f.name.toLowerCase().trim().includes(filterString);
    });
  }

  isDateInFuture(startDateTime: string): boolean {
    const formattedDate = this.datePipe.transform(startDateTime , 'yyyy-MM-dd');
    const currentDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd');
    return formattedDate > currentDate;
  }

  onSearchKeydown(event: KeyboardEvent): void {
    if (event.key === ' ' || event.code === 'Space') {
      event.stopPropagation();
    }
  }
  
}
