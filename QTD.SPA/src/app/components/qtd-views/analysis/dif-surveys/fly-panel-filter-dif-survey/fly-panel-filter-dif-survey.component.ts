import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { DIFSurveyOverview_DIFSurvey_VM } from '@models/DIFSurvey/DIFSurveyOverview_DIFSurvey_VM';
import { Position } from '@models/Position/Position';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-filter-dif-survey',
  templateUrl: './fly-panel-filter-dif-survey.component.html',
  styleUrls: ['./fly-panel-filter-dif-survey.component.scss'],
})
export class FlyPanelFilterDifSurveyComponent implements OnInit {
  filterDifSurveyForm: UntypedFormGroup;
  positions:Position[];
  datepipe = new DatePipe('en-us');
  @Input() filterDifValue:any;
  @Output() difFilterChange = new EventEmitter<any>();
  constructor(
    private fb: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    private posService:PositionsService,
  ) {}

  ngOnInit(): void {
    this.initializeSurveyForm();
    this.getAllActivePositions();
  }

  initializeSurveyForm() {
    this.filterDifSurveyForm = this.fb.group({
      position: new UntypedFormControl(this.filterDifValue?.position),
      startDate: new UntypedFormControl(this.datepipe.transform(this.filterDifValue?.startDate, 'yyyy-MM-dd')),
      dueDate: new UntypedFormControl(this.datepipe.transform(this.filterDifValue?.dueDate, 'yyyy-MM-dd')),
      surveyStatus: new UntypedFormControl(this.filterDifValue?.surveyStatus),
      devStatus: new UntypedFormControl(this.filterDifValue?.devStatus),
      activeStatus: new UntypedFormControl(this.filterDifValue?.activeStatus),
    });
  }

  clearPositionSelection() {
    this.filterDifSurveyForm.get('position').setValue(null);
  }
  clearStartDateSelection(){
    this.filterDifSurveyForm.get('startDate').setValue(null);
  }
  clearDueDateSelection(){
    this.filterDifSurveyForm.get('dueDate').setValue(null);
  }
  clearSurveyStatusSelection(){
    this.filterDifSurveyForm.get('surveyStatus').setValue(null);
  }
  clearDevStatusSelection(){
    this.filterDifSurveyForm.get('devStatus').setValue(null);
  }
  clearActiveStatusSelection(){
    this.filterDifSurveyForm.get('activeStatus').setValue(null);
  }

  getFilterValues(){
    const startDate = this.filterDifSurveyForm.get("startDate").value;
    const dueDate = this.filterDifSurveyForm.get("dueDate").value;
  
    // Append "0" time to not let filter value shift due to ISO time difference, this would cause incorrect filtering by dates once its converted to new Date()
    const startDateWithTime = startDate ? startDate + 'T00:00:00' : null;
    const dueDateWithTime = dueDate ? dueDate + 'T00:00:00' : null;
  
    this.filterDifValue = {
      position: this.filterDifSurveyForm.get("position").value,
      startDate: startDateWithTime,
      dueDate: dueDateWithTime,
      surveyStatus: this.filterDifSurveyForm.get("surveyStatus").value,
      devStatus: this.filterDifSurveyForm.get("devStatus").value,
      activeStatus: this.filterDifSurveyForm.get("activeStatus").value
    }
  }

  applyFiltersClick(){
    this.getFilterValues();
    this.difFilterChange.emit(this.filterDifValue);
    this.flyPanelService.close()
  }

  closeFlyPanel() {
    this.flyPanelService.close();
  }

  async getAllActivePositions(){
    this.positions = (await this.posService.getAllWithoutIncludes()).filter(x=>x.active);    
  }

  // onPositionChange(event: any) {
  //   this.inputPositionFilter = event.value;
  //   this.inputPositionFilterChange.emit(this.inputPositionFilter);
  // }

  // onStartDateChange(event:any){
  //   this.inputStartDateFilter =event.target.value; 
  //   this.inputStartDateChangeFilter.emit(this.inputStartDateFilter);
  // }

  // onDueDateChange(event:any){
  //   this.inputDueDateFilter = event.target.value;
  //   this.inputDueDateChangeFilter.emit(this.inputDueDateFilter);
  // }

  // onDevStatusChange(event:any){
  //   this.inputDevStatusFilter = event.value;
  //   this.inputDevStatusChangeFilter.emit(this.inputDevStatusFilter);
  // }

  // onSurveyStatusChange(event:any){
  //   this.inputSurveyStatusFilter = event.value;
  //   this.inputSurveyStatusChangeFilter.emit(this.inputSurveyStatusFilter);
  // }

}
