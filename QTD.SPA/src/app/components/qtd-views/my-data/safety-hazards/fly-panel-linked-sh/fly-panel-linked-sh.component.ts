import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SaftyHazardCompactOption } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCompactOptions';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-linked-sh',
  templateUrl: './fly-panel-linked-sh.component.html',
  styleUrls: ['./fly-panel-linked-sh.component.scss'],
})
export class FlyPanelLinkedShComponent implements OnInit, AfterViewInit {
  @Output() closed = new EventEmitter<any>();
  @Input() Id: any; //! Use this Id to fetch the linked procedures and map into linkedProcedures and show the data in html then
  linkedSHs: SaftyHazardCompactOption[];
  selectedNumber:any;
  @Input() procedureNumber:any;
  @Input() selectedTypeNumber:any;
  @Input() rrNumber:any;


  //! Pass the Title as input from selected Tab
  @Input() Title: string;
  @Input() selectedType: string;
  linkedSafetyHazards: any[] = [];
  constructor(private shService: SafetyHazardsService,
    private alert: SweetAlertService,) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {

    
    switch (this.selectedType) {
      case 'Task':
        this.readySHLinkedWithTask();
        break;
      case 'Enabling Objective':
        this.readySHLinkedWithEO();
        break;
      case 'ILA':
        this.readySHLinkedWithILA();
        break;
      case 'Procedure':
        this.readySHLinkedWithProcedures();
        break;
      case 'Regulatory Requirement':
        this.readyShLinkedWithRR();
        break;
    }
  }

  async readySHLinkedWithTask() {
    await this.shService
      .getSHLinkedToTask(this.Id)
      .then((res: SaftyHazardCompactOption[]) => {
        this.linkedSHs = res;
      });
      this.selectedNumber = this.selectedTypeNumber;
  }

  async readySHLinkedWithEO() {
    await this.shService
      .getSHLinkedToEO(this.Id)
      .then((res: SaftyHazardCompactOption[]) => {
        this.linkedSHs = res;
      });
      this.selectedNumber = this.selectedTypeNumber;
  }

  async readySHLinkedWithILA() {
    await this.shService
      .getSHLinkedToILA(this.Id)
      .then((res: SaftyHazardCompactOption[]) => {
        this.linkedSHs = res;
      });
      this.selectedNumber = this.selectedTypeNumber;
  }

  async readySHLinkedWithProcedures(){
    await this.shService
      .getSHLinkedToProcedure(this.Id)
      .then((res: SaftyHazardCompactOption[]) => {
        this.linkedSHs = res;
      });
      this.selectedNumber = this.procedureNumber;
  }

  async readyShLinkedWithRR(){
    await this.shService.getSHLinkedToRR(this.Id) .then((res: SaftyHazardCompactOption[]) => {
      this.linkedSHs = res;
    });
    this.selectedNumber = this.rrNumber;
  }
}
