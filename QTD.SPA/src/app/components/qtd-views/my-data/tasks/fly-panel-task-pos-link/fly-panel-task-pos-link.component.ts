import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-task-pos-link',
  templateUrl: './fly-panel-task-pos-link.component.html',
  styleUrls: ['./fly-panel-task-pos-link.component.scss'],
})
export class FlyPanelTaskPosLinkComponent implements OnInit {
  linkPos: boolean = true;
  showActive: boolean = true;
  isLoading: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinkedIds: any[] = [];
  linkedIds: any[] = [];
  positions: Position[];
  filteredList: Position[];
  subscription = new SubSink();
  taskId = '';
  showLinkLoader: boolean = false;
  constructor(
    private posSrvc: PositionsService,
    private taskSrvc: TasksService,
    private activatedRoute: ActivatedRoute,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.subscription.sink = this.activatedRoute.params.subscribe((res) => {
      this.taskId = String(res.id).split('-')[0];
    });
    this.getPositions();
  }

  clearSearch:any
  clearFilter(){
    this.clearSearch = null;
    this.getPositions();
  }

  filterData(e: Event) {

    let filterString = (e.target as HTMLInputElement).value;
    this.filteredList = [
      ...this.positions.filter((x) =>
        x.positionTitle.toLowerCase().includes(String(filterString).toLowerCase())
      ),
    ];
  }

  filterStatus(active: boolean) {
    this.filteredList = [...this.positions.filter((x) => x.active == active)];
    this.showActive = active;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  linkToTask() {
    this.showLinkLoader = true;
    let option:any = {
      positionIds: this.linkedIds,
    };
    this.taskSrvc.LinkpositionsWithoutUnlink(this.taskId, option).then(async (res) => {
      this.alert.successToast( await this.transformTitle('Position') +'s linked to ' + await this.transformTitle('Task'));
      this.closed.emit('fp-link-pos-task-closed');
      this.refresh.emit('refresh position tbl');
    }).finally(() => {
      this.showLinkLoader = false;
    });
  }

  positionChecked(checked: boolean, id: any) {
    if (checked) {
      this.linkedIds.push(id);
    } else {
      this.linkedIds.splice(this.linkedIds.indexOf(id), 1);
    }
    this.linkedIds = [...new Set(this.linkedIds)];
  }

  async getPositions() {
    await this.posSrvc.getAllWithoutIncludes().then((res) => {
      this.positions = res;
      this.filteredList = res;

      this.filterStatus(this.showActive);
    });
  }
}
