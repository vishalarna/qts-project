import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EO_LinkOptions } from 'src/app/_DtoModels/EnablingObjective/EO_LinkOptions';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-flypanel-eo-position-link',
  templateUrl: './flypanel-eo-position-link.component.html',
  styleUrls: ['./flypanel-eo-position-link.component.scss']
})
export class FlypanelEoPositionLinkComponent implements OnInit {
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
  eoId = '';
  constructor(
    private posSrvc: PositionsService,
    private eoService : EnablingObjectivesService,
    private activatedRoute: ActivatedRoute,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {
    this.subscription.sink = this.activatedRoute.params.subscribe((res) => {
      this.eoId = String(res.id).split('-')[1];
    });
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

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async linkPositions(){
    this.isLoading = true;
    var options = new EO_LinkOptions();
    options.eoId = this.eoId;
    options.positionIds = this.linkedIds;
    await this.eoService.linkPositions(options).then(async (_)=>{
      this.alert.successToast(await this.transformTitle('Position')+"s Linked to Selected SQ");
      this.refresh.emit();
      this.closed.emit();
    }).finally(()=>{
      this.isLoading = false;
    })
  }

}
