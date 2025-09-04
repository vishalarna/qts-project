import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-linked-positions',
  templateUrl: './fly-panel-linked-positions.component.html',
  styleUrls: ['./fly-panel-linked-positions.component.scss']
})
export class FlyPanelLinkedPositionsComponent implements OnInit, AfterViewInit {

  @Output() closed = new EventEmitter<any>();
  @Input() Id: any; //! Use this Id to fetch the linked procedures and map into linkedProcedures and show the data in html then

  //! Pass the Title as input from selected Tab
  @Input() Title: string;
  @Input() selectedType: string;
  @Input() ilaNumber:any;
  @Input() shNumber:any;
  @Input() eoNumber:any;
  linkedPositions: Position[] = [];
  selectedTypeNumber:any;
  isLoading: boolean = false;

  constructor(
    private posService: PositionsService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    switch (this.selectedType) {
      case "Task":
        this.readyTaskData();
        break;
        case "SQ":
        this.readySQData();
        break;
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async readyTaskData(){
    this.isLoading = true;
    await this.posService.getPositionsLinkedToTask(this.Id).then((res:Position[])=>{
      this.linkedPositions = res;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching " + await this.transformTitle('Position') + "s That " +  await this.transformTitle('Task')  + " is linked to " + err);
    })
    .finally(() => {
      this.isLoading = false;
    });
  }
  async readySQData(){
    this.isLoading = true;
    await this.posService.getEOLinkedWithPositions(this.Id).then((res:Position[])=>{
      this.linkedPositions = res;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching" + await this.transformTitle('Position') + "s That SQ is linked to " + err);
    })
    .finally(() => {
      this.isLoading = false;
    });
  }

}
