import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { Position } from '@models/Position/Position';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';

@Component({
  selector: 'app-fly-panel-link-position-filter',
  templateUrl: './fly-panel-link-position-filter.component.html',
  styleUrls: ['./fly-panel-link-position-filter.component.scss'],
})
export class FlyPanelLinkPositionFilterComponent implements OnInit {
  @Output() clicked = new EventEmitter<boolean>();
  @Output() setButtonClicked = new EventEmitter<boolean>();
  @Output() DutyAreaId = new EventEmitter<string>();
  @Output() subDutyAreaId = new EventEmitter<string>();
  @Output() selectedPosition = new EventEmitter<Position>();
  @Input() inputDutyAreaId: string;
  @Input() inputSubDutyAreaId: string;
  @Input() inputSelectedPosition:Position;
  filterPositionForm: UntypedFormGroup;
  positionList: any;
  dutyAreaList: any;
  subDutyAreaList: any;
  isLoading = false;
  
  constructor(
    private posService: PositionsService,
    private fb: UntypedFormBuilder,
    private dutyAreaService: DutyAreaService
  ) {}

  ngOnInit(): void {
    this.initializationForm();
    this.loadAsync();
  }

  initializationForm() {
    this.filterPositionForm = this.fb.group({
      position: new UntypedFormControl(''),
      dutyArea: new UntypedFormControl(''),
      subDutyArea: new UntypedFormControl(''),
    });
  }
  async loadAsync() {
    this.isLoading = true;
    await this.getPositionData();
    await this.getDutyArea();
    await this.filterSubDutyAreas(this.inputDutyAreaId);
       
    if (this.positionList && this.positionList.length > 0) {
      let positionData = this.positionList.find((c) => c.id == this.inputSelectedPosition?.id);
      this.filterPositionForm.get('position').setValue(positionData);
    }
    if (this.dutyAreaList && this.dutyAreaList.length > 0) {
      let dutyAreaData=this.dutyAreaList.find(c=>c.id==this.inputDutyAreaId);
      this.filterPositionForm.get('dutyArea').setValue(dutyAreaData);
    }
    if (this.subDutyAreaList && this.subDutyAreaList.length > 0) {
      let subDutyAreaData=this.subDutyAreaList.find(c=>c.id==this.inputSubDutyAreaId);
      this.filterPositionForm.get('subDutyArea').setValue(subDutyAreaData);
    }
    this.isLoading = false;
  }
  async getPositionData() {
    await this.posService.getPositionTaskAsync().then((res) => {
      this.positionList = res;
    });
  }

  async getDutyArea() {
    await this.dutyAreaService.getAllOrderBy('num').then((res) => {
      this.dutyAreaList = res;
    });
  }

  async onDutyAreaChange(selectedId: any) {
    await this.filterSubDutyAreas(selectedId?.value.id);
  }

  async filterSubDutyAreas(selectedId: any) {
    await this.dutyAreaService.getAllSubDutyAreas().then((res) => {
      this.subDutyAreaList = res.filter(
        (subDutyArea) => subDutyArea.dutyAreaId === selectedId
      ).sort((a, b) => a.subNumber - b.subNumber);
    });
  }

  handleCloseButtonClick(): void {
    this.clicked.emit(false);
  }
  
  onSelectButtonClick() {
    const selectedValues = {
      position: this.filterPositionForm.get('position')?.value,
      dutyArea: this.filterPositionForm.get('dutyArea')?.value,
      subDutyArea: this.filterPositionForm.get('subDutyArea')?.value,
    };
  
    this.selectedPosition.emit(selectedValues?.position);
    this.DutyAreaId.emit(selectedValues.dutyArea?.id);
    this.subDutyAreaId.emit(selectedValues.subDutyArea?.id);
    this.setButtonClicked.emit();
    this.handleCloseButtonClick();
  }

  onClearButtonClick(event: Event) {
    event.stopPropagation(); 
    this.filterPositionForm.get('position').setValue(null);
    this.inputSelectedPosition=null;
  }

  onDutyAreaClearButtonClick(event: Event, controlName: string) {
    event.stopPropagation();
    if (controlName === 'DutyArea') {
      this.filterPositionForm.get('dutyArea').setValue(null);
      this.filterPositionForm.get('subDutyArea').setValue(null);
      this.inputDutyAreaId = null;
      this.inputSubDutyAreaId = null;
    } else if (controlName === 'SubDutyArea') {
      this.filterPositionForm.get('subDutyArea').setValue(null);
      this.inputSubDutyAreaId = null;
    }
  }
  
  
}
