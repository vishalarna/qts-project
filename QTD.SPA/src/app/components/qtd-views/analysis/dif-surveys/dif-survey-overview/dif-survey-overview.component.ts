import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { DIFSurveyOverview_DIFSurvey_VM } from '@models/DIFSurvey/DIFSurveyOverview_DIFSurvey_VM';
import { DIFSurveyOverview_VM } from '@models/DIFSurvey/DifSurveyOverview_VM';
import { Store } from '@ngrx/store';
import { ApiDifSurveyService } from 'src/app/_Services/QTD/DifSurvey/api.difsurvey.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-dif-survey-overview',
  templateUrl: './dif-survey-overview.component.html',
  styleUrls: ['./dif-survey-overview.component.scss'],
})
export class DifSurveyOverviewComponent implements OnInit {
  dataSource:any;
  difSurveyOverview_VM: DIFSurveyOverview_VM; 
  datePipe = new DatePipe('en-us');
  tableColumns: string[];
  searchText:string;
  filterValues: any;
  deleteDescription:string;
  deleteDIFSurveyId:string;
  difSurveys:DIFSurveyOverview_DIFSurvey_VM[];
  filteredDifOverviews:DIFSurveyOverview_DIFSurvey_VM[];
  @ViewChild(MatSort) sort: MatSort;
  
  constructor(
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    private _router: Router,
    private difSurveyService: ApiDifSurveyService,
    private alert: SweetAlertService,
    public dialog: MatDialog,
    private store: Store<{ toggle: string }>,
  ) {}

  ngOnInit(): void {
    this.store.dispatch(sideBarOpen());
    this.tableColumns = ['CheckBox', 'surveyTitle','positionTitle','startDate','dueDate','surveyStatus','devStatus','action'];
    this.dataSource = new MatTableDataSource<DIFSurveyOverview_DIFSurvey_VM>();
    this.searchText = '';
    this.loadAsync();
  }
  searchUpdate(event: any) {
    const searchText = event.target.value.trim().toLowerCase();
    this.searchText = searchText;
    this.getDIFFilterValues(this.searchText);
  }

  openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf, {sideBarBackDrop: true});
    this.flyPanelService.open(portal);
  }

  openDifSurvey() {
    this._router.navigate(['/analysis/dif-survey/create']);
  }

  openViewEnrollmentPage(item:any){
    this._router.navigate(['/analysis/dif-survey', item.id, 'enrollments']);
  }

  openViewDifResultsPage(item:any){
    this._router.navigate(['/analysis/dif-survey', item.id, 'results']);
  }

  openImportDifSurveyPage(item:any){
    this._router.navigate(['/analysis/dif-survey', item.id, 'import-results']);
  }

  async loadAsync(){
    this.difSurveyOverview_VM = await this.difSurveyService.getAllAsync();
    this.difSurveys = this.difSurveyOverview_VM?.difSurveys;   
    this.dataSource = new MatTableDataSource<DIFSurveyOverview_DIFSurvey_VM>(this.difSurveys);
    setTimeout(()=>{
      this.dataSource.sort = this.sort;
    },1);
  }
  
  getDIFFilterValues(value:any){
    this.filterValues = value;
    this.filteredDifOverviews = this.difSurveys;
    if(this.searchText){
      this.filteredDifOverviews = this.filteredDifOverviews.filter(item=>{
        return item?.surveyTitle?.trim()?.toLowerCase()?.includes(this.searchText) || item?.positionTitle?.substring(0,item.positionTitle.length-2)?.trim()?.toLowerCase()?.includes(this.searchText);
      });  
    }
    if(this.filterValues?.position){
      this.applyPositionFilter();
    }
    if(this.filterValues?.startDate){
      this.applyStartDateFilter();
    }
    if(this.filterValues?.dueDate){
      this.applyDueDateFilter();
    }
    if(this.filterValues?.devStatus){
      this.applyDevStatusFilter();
    }
    if(this.filterValues?.surveyStatus){
      this.applySurveyStatusFilter();
    }
    if(this.filterValues?.activeStatus){
      this.applyActiveStatusFilter();
    }
    this.dataSource = new MatTableDataSource(this.filteredDifOverviews);
    setTimeout(()=>{
      this.dataSource.sort = this.sort;
    },1);
  }

  applyPositionFilter(){
    this.filteredDifOverviews = this.filteredDifOverviews.filter(x => x.positionTitle === this.filterValues?.position.toString());
  }

  applyDevStatusFilter(){
    this.filteredDifOverviews = this.filteredDifOverviews.filter(x => x.devStatus.toString() === this.filterValues?.devStatus.toString())
  }

  applySurveyStatusFilter(){
      this.filteredDifOverviews = this.filteredDifOverviews.filter(x => x.surveyStatus.toString() === this.filterValues?.surveyStatus.toString())
  }

  applyActiveStatusFilter(){
    if(this.filterValues?.activeStatus == "Active"){
      this.filteredDifOverviews = this.filteredDifOverviews.filter(x => x.isActive == true);
    }
    if(this.filterValues?.activeStatus == "Inactive"){
      this.filteredDifOverviews = this.filteredDifOverviews.filter(x =>  x.isActive == false);
    }
}

  applyStartDateFilter(){
    this.filteredDifOverviews = this.filteredDifOverviews.filter(x => new Date(x.startDate) >= new Date(this.filterValues?.startDate));
  }

  applyDueDateFilter(){
    this.filteredDifOverviews = this.filteredDifOverviews.filter(x => new Date(x.dueDate) <= new Date(this.filterValues?.dueDate));
  }

  editDifSurvey(id:string) {
    this._router.navigate(['/analysis/dif-survey/edit/'+id]);
  }

  deleteDIFSurvey(templateRef: any, row: DIFSurveyOverview_DIFSurvey_VM) {
    this.deleteDescription = `You are selecting to delete DIF Survey <b>${row.surveyTitle}</b>.`;
    this.deleteDIFSurveyId = row.id
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  async editDifSurveyAsync(id: string, editType: string) {
    return this.difSurveyService.editDIFSurveyAsync(id, editType)
      .then(async res => {
          await this.loadAsync();
          this.getDIFFilterValues(this.filterValues);
          if(editType == 'delete'){
            this.alert.successToast("DIF Survey Successfully Deleted");
          }else if(editType == 'inactive' || editType == 'active'){
            this.alert.successToast("DIF Survey Status Successfully Updated");
          }
      })
  }  
}
