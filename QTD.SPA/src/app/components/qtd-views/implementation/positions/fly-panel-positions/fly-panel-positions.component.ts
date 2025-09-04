import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { PositionUpdateOptions } from 'src/app/_DtoModels/Position/PositionUpdateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarBackDrop } from 'src/app/_Statemanagement/action/state.menutoggle';
import { menubackdropState } from 'src/app/_Statemanagement/reducer/state.menureducer';

@Component({
  selector: 'app-fly-panel-positions',
  templateUrl: './fly-panel-positions.component.html',
  styleUrls: ['./fly-panel-positions.component.scss'],
})
export class FlyPanelPositionsComponent implements OnInit {
  showlistPosition: boolean = true;
  @Input() showLinkBtn: boolean = false;
  posHeader: string = '';

  PosUpdateOpt!: PositionUpdateOptions;
  tempPosList: Position[] = [];

  positions: Position[] = [];
  positionsToLink: any[] = [];
  listPosition: Position[];
  acronymName: string;
  posName: string;
  positionPanelVisible: boolean = false;
  editPosId: any;

  @Output()
  closed = new EventEmitter<any>();

  constructor(
    private DataBroadcastService: DataBroadcastService,
    private positionService: PositionsService,
    private alert: SweetAlertService,
    public translate: TranslateService,
    public flyPanelSrvc: FlyInPanelService,
    private store: Store,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {
    this.getPositionList();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }  

  async getPositionList() {
    await this.positionService
      .getAll()
      .then((data) => (this.listPosition = this.tempPosList = data));

    this.posHeader = await this.transformTitle('Position') +'s';
    this.showlistPosition = true;
  }

  filterPositions(filter: any) {
    let f = filter.target as HTMLInputElement;
    this.listPosition = this.tempPosList;
    this.listPosition = this.listPosition.filter(
      (item) => item.name.toLowerCase().indexOf(f.value.toLowerCase()) > -1
    );
  }

  addPosition() {
    this.posName = '';
    this.acronymName = '';
    this.posHeader = 'Add New '+ this.transformTitle('Position');
    this.showlistPosition = false;
  }
  editPosition(pos: Position) {
    this.posHeader = 'Edit '+ this.transformTitle('Position');
    this.PosUpdateOpt = new PositionUpdateOptions();
    this.PosUpdateOpt.name = pos.name;
    this.PosUpdateOpt.acronym = pos.acronym;
    this.editPosId = pos.id;
    this.showlistPosition = false;
  }
  deletePosition(id: any) {}

  addPositionsToLinkList(id: any) {
    // this.DataBroadcastService.positionsToLink
    //   .asObservable()
    //   .subscribe((res) => (this.positionsToLink = res));
    const index = this.positionsToLink.indexOf(id, 0);
    if (index == -1) this.positionsToLink.push(id);
    else this.positionsToLink.splice(index, 1);
  }

  LinkPositionToTask() {
    this.DataBroadcastService.positionsToLink.next(this.positionsToLink);
    this.positionsToLink = [];
  }

  async createNewPosition() {
    // await this.positionService
    //   .create({ positionName: this.posName, acronym: this.acronymName, positionNumber: 
    //   '', positionDescription: '' })
    //   .then((res) => {
    //     if (res) {
    //       this.listPosition = [];
    //       this.getPositionList();
    //       this.posName = '';
    //       this.acronymName = '';
    //       this.DataBroadcastService.refreshListName.next('positions');

    //       this.alert.successToast(this.translate.instant(`L.recAdded`));
    //     }
    //   });
  }

  async updatePosition() {
    await this.positionService
      .update(this.editPosId, this.PosUpdateOpt)
      .then((res) => {
        if (res) {
          // this.posName = '';
          this.listPosition = [];
          this.alert.successToast(this.translate.instant(`L.recUpdated`));
          this.DataBroadcastService.refreshListName.next('positions');
        }
      });
  }
}
