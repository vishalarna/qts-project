import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { RegulatoryRequirement } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirement';
import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-linked-rr',
  templateUrl: './fly-panel-linked-rr.component.html',
  styleUrls: ['./fly-panel-linked-rr.component.scss'],
})
export class FlyPanelLinkedRRComponent implements OnInit, AfterViewInit {
  @Output() closed = new EventEmitter<any>();
  @Input() Id: any; //! Use this Id to fetch the linked regulatory requirement and map into linkedProcedures and show the data in html then

  //! Pass the Title as input from selected Tab
  @Input() Title: string;
  @Input() selectedType: string;
  @Input() rrTask:any;
  @Input() shNumber:any;
  @Input() ilaNumber:any;
  @Input() procedureNumber:any;
  @Input() eoNumber:any;
  linkedRRs: RegulatoryRequirementsCompact[];
  selectedTypeNumber:any;

  constructor(private rrService: RegulatoryRequirementService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    
    switch (this.selectedType) {
      case 'Safety Hazard':
        this.readyRRLinkedWithSH();
        break;
      case 'Task':
        this.readyRRLinkedWithTask();
        break;
      case 'Procedure':
        this.readyProcedureData();
        break;
      case 'ILA':
        this.readyILAData();
        break;
      case 'Enabling Objective':
        this.readyEOData();
        break;
    }
  }
  readyProcedureData() {
    this.rrService.getRRsLinkedProcedure(this.Id).then((res) => {
      this.linkedRRs = res;
    });
    this.selectedTypeNumber = this.procedureNumber;
  }
  async readyRRLinkedWithSH() {
    await this.rrService
      .getRRLinkedToSh(this.Id)
      .then((res: RegulatoryRequirementsCompact[]) => {
        this.linkedRRs = res;
      });
    this.selectedTypeNumber = this.shNumber;
  }

  async readyRRLinkedWithTask() {
    await this.rrService
      .getRRLinkedToTask(this.Id)
      .then((res: RegulatoryRequirementsCompact[]) => {
        this.linkedRRs = res;
       
      }); 
      this.selectedTypeNumber = this.rrTask;
  }

  async readyILAData(){
    
    await this.rrService.getRRLinkedToILA(this.Id).then((res:RegulatoryRequirementsCompact[])=>{
      
      this.linkedRRs = res;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching " + await this.labelPipe.transform('Regulatory Requirement') + "s That " + await this.labelPipe.transform('ILA') + " is linked to ");
    });
    this.selectedTypeNumber = this.ilaNumber;
  }

  async readyEOData() {
    await this.rrService.getRRLinkedToEO(this.Id).then((res: RegulatoryRequirementsCompact[]) => {
      this.linkedRRs = res;
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Fetching " + await this.labelPipe.transform('Regulatory Requirement') + "s That " + await this.transformTitle('Enabling Objective') + " is linked to ");
    });
    this.selectedTypeNumber = this.eoNumber;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
