import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { TrainingGroup } from 'src/app/_DtoModels/TrainingGroup/TrainingGroup';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-linked-training-groups',
  templateUrl: './fly-panel-linked-training-groups.component.html',
  styleUrls: ['./fly-panel-linked-training-groups.component.scss']
})
export class FlyPanelLinkedTrainingGroupsComponent implements OnInit, AfterViewInit {

  @Output() closed = new EventEmitter<any>();
  @Input() Id: any;
  @Input() Title: string;
  linkedTrainingGroups: TrainingGroup[] = [];
  constructor(private posService: PositionsService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.readyTaskTrainingGroupData();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async readyTaskTrainingGroupData(){
    await this.posService.getPositionsLinkedToTrainingGroup(this.Id).then((res:TrainingGroup[])=>{
      this.linkedTrainingGroups = res;
    }).catch(async (err:any)=>{
      this.alert.errorToast("Error Fetching"+ await this.transformTitle('Position') + "s That "+  await this.transformTitle('Task') + " is linked to Training Groups" + err);
    })
  }

}
