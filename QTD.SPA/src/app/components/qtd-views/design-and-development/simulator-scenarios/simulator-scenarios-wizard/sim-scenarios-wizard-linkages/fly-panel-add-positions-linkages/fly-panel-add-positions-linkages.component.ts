import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { SimulatorScenario_Position_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Position_VM';
import { SimulatorScenario_UpdatePositions_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdatePositions_VM';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-add-positions-linkages',
  templateUrl: './fly-panel-add-positions-linkages.component.html',
  styleUrls: ['./fly-panel-add-positions-linkages.component.scss']
})
export class FlyPanelAddPositionsLinkagesComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() newPositionLinked = new EventEmitter<SimulatorScenario_Position_VM[]>();
  @Input() inputSimScenariosId: string;
  @Input() linkedPositionIds: any[] = [];
  positionsToLink: SimulatorScenario_UpdatePositions_VM = new SimulatorScenario_UpdatePositions_VM;
  positions: Position[];
  positionsList: Position[];
  inputValue: string = '';

  constructor(
    private positionService: PositionsService,
  ) { }

  async ngOnInit(): Promise<void> {
    await this.getPositionsAsync();
  }

  async getPositionsAsync() {
    await this.positionService.getAllWithoutIncludes().then((res) => {
      this.positions = res.filter((x) => x.active);
      this.positionsList = this.positions;
    });
  }

  searchPosition(e: Event) {
    this.inputValue = (e.target as HTMLInputElement).value;
    this.filterPosition();
  }
  
  filterPosition(){
    this.positionsList = [
      ...this.positions.filter((x) =>
        x.positionTitle.toLowerCase().includes(String(this.inputValue).toLowerCase())
      ),
    ];
  }

  clearSearchString(){
    this.inputValue = "";
    this.filterPosition();
  }

  selectedPositions(checked: boolean, row: any) {
    if (checked) {
      const newSelectedPosition: SimulatorScenario_Position_VM = new SimulatorScenario_Position_VM();
      newSelectedPosition.positionId = row.id;
      newSelectedPosition.positionTitle = row.positionTitle;
      this.positionsToLink.positions.push(newSelectedPosition);
    } else {
      this.positionsToLink.positions = this.positionsToLink.positions.filter(pos => pos.positionId !== row.id);
    }
    this.positionsToLink.positions = [...new Map(this.positionsToLink.positions.map(pos => [pos.positionId, pos])).values()];
  }

  linkPosToScenarios(){
    this.newPositionLinked.emit(this.positionsToLink.positions);
    this.closed.emit();
  }
}
