import { DatePipe, formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { TrainingProgramType } from 'src/app/_DtoModels/TrainingProgramType/TrainingProgramType';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { TrainingProgramTypeService } from 'src/app/_Services/QTD/training-program-type.service';

@Component({
  selector: 'app-fly-panel-overview-filter',
  templateUrl: './fly-panel-overview-filter.component.html',
  styleUrls: ['./fly-panel-overview-filter.component.scss']
})
export class FlyPanelOverviewFilterComponent implements OnInit {
  
  filterTrainingProgramForm: UntypedFormGroup = new UntypedFormGroup({});
  datePipe = new DatePipe('en-us');
  position: Position[] = [];
  trainingProgramTypes:TrainingProgramType[]=[];
  dateError = false;
  isPanelOpen: boolean = true;
  @Input() inputPositionIdFilter : number | null;
  @Output() inputPositionIdFilterChange = new EventEmitter<number | null>();
  @Input() inputTrainingProgramTypeIdFilter : string | null;
  @Output() inputTrainingProgramTypeIdFilterChange = new EventEmitter<string | null>();
  @Input() inputStartDateMaxFilter : Date | null;
  @Output() inputStartDateMaxFilterChange = new EventEmitter<Date | null>();
  @Input() inputEndDateMinFilter : Date | null;
  @Output() inputEndDateMinFilterChange = new EventEmitter<Date | null>();
  @Input() inputPublishFilter : string | null;
  @Output() inputPublishFilterChange = new EventEmitter<string | null>();
  @Input() inputActiveFilter : string | null = "Active";
  @Output() inputActiveFilterChange = new EventEmitter<string | null>();
  @Input() handleLoad: (e) => void;
  @Input() handleXClick: () => void;
  @Input() handleApplyFiltersClick: () => void;
  

  constructor(private fb: UntypedFormBuilder,
    private positionService: PositionsService,
    private trainingProgramTypeService : TrainingProgramTypeService) {    }

  async ngOnInit(): Promise<void> {
    this.initializeTrainingProgramForm();
    await this.load();
  }

  initializeTrainingProgramForm() {
    this.filterTrainingProgramForm = this.fb.group({ 
      position: new UntypedFormControl(this.inputPositionIdFilter, ),
      programType: new UntypedFormControl(this.inputTrainingProgramTypeIdFilter),
      reviewStatus: new UntypedFormControl( this.inputPublishFilter),
      activeStatus: new UntypedFormControl(this.inputActiveFilter),
      reviewStartDate: new UntypedFormControl(this.datePipe.transform(this.inputEndDateMinFilter, 'yyyy-MM-dd')),
      reviewEndDate: new UntypedFormControl( this.datePipe.transform(this.inputStartDateMaxFilter, 'yyyy-MM-dd'))
    });
  }

  _handleLoad(e:any){
    if (this.handleLoad && typeof this.handleLoad === 'function') {
      this.handleLoad(e);
    }
  }
  _handleXClick(){
    if (this.handleXClick && typeof this.handleXClick === 'function') {
      this.handleXClick();
    }
  }
  _handleApplyFiltersClick(){
    if (this.handleApplyFiltersClick && typeof this.handleApplyFiltersClick === 'function') {
      this.handleApplyFiltersClick();
    }
  }

  async load(){
    await this.positionService.getAllWithoutIncludes().then((x) => {
      this.position = x;
    });
    await this.trainingProgramTypeService.getAll().then((x) => {
      this.trainingProgramTypes = x;
    });
  }

  clearPositionSelection() {
    this.filterTrainingProgramForm.get('position').setValue(null);
    this.inputPositionIdFilterChange.emit(null);
  }

  clearProgramTypeSelection() {
    this.filterTrainingProgramForm.get('programType').setValue(null);
    this.inputTrainingProgramTypeIdFilterChange.emit(null);
  }
  clearDateRange(){
    this.filterTrainingProgramForm.get('reviewStartDate').setValue(null);
    this.filterTrainingProgramForm.get('reviewEndDate').setValue(null);
    this.inputStartDateMaxFilterChange.emit(null);
    this.inputEndDateMinFilterChange.emit(null);
  }
  clearReviewStatus(){
    this.filterTrainingProgramForm.get('reviewStatus').setValue(null);
    this.inputPublishFilterChange.emit(null);
  }
  clearActiveStatus(){
    this.filterTrainingProgramForm.get('activeStatus').setValue(null);
    this.inputActiveFilterChange.emit(null);
  }

  xClick() {
    this._handleXClick();
  }

  applyFiltersClick(){
    this._handleApplyFiltersClick();
    this._handleXClick();
  }

  onPositionChange(event: any) {
    this.inputPositionIdFilter = event.value;
    this.inputPositionIdFilterChange.emit(this.inputPositionIdFilter);
  }

  onProgramTypeChange(event : any){
    this.inputTrainingProgramTypeIdFilter = event.value;
    this.inputTrainingProgramTypeIdFilterChange.emit(this.inputTrainingProgramTypeIdFilter);
  }

  onPublishChange(event : any){
    this.inputPublishFilter = event.value;
    this.inputPublishFilterChange.emit(this.inputPublishFilter);
  }

  onActiveStatusChange(event : any){
    this.inputActiveFilter = event.value;
    this.inputActiveFilterChange.emit(this.inputActiveFilter);
  }

  dateRangeChange(){
    this.inputEndDateMinFilter = this.filterTrainingProgramForm.get('reviewStartDate').value;
    this.inputStartDateMaxFilter = this.filterTrainingProgramForm.get('reviewEndDate').value;
    this.inputEndDateMinFilterChange.emit(this.inputEndDateMinFilter);
    this.inputStartDateMaxFilterChange.emit(this.inputStartDateMaxFilter);
  }
}
