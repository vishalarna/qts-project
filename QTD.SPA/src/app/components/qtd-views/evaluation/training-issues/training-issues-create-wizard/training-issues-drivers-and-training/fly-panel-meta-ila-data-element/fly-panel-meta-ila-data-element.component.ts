import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MetaILA } from '@models/MetaILA/MetaILA';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { MetaILAService } from 'src/app/_Services/QTD/meta-ila.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-meta-ila-data-element',
  templateUrl: './fly-panel-meta-ila-data-element.component.html',
  styleUrls: ['./fly-panel-meta-ila-data-element.component.scss']
})
export class FlyPanelMetaIlaDataElementComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  checkListSelection = new SelectionModel<MetaILA>(false);
  metaILAList: MetaILA[] = [];
  originalMetaILAList: MetaILA[] = [];
  filterSearchString: string = '';
  linkedId: string;
  loader: boolean = false;
  showActive: boolean = true;

  constructor(
    public flyPanelService: FlyInPanelService,
    public metaILAService: MetaILAService,
  ) { }

  ngOnInit(): void {
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.getMetaILAAllAsync();
  }

  async getMetaILAAllAsync() {
    this.loader = true;
    await this.metaILAService.getAll().then((res: any) => {
      this.metaILAList = res;
      this.originalMetaILAList = res;
      this.filterActive(this.showActive);
      this.loader = false;
    })
      .catch((err) => {
        this.loader = false;
      });
  }

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterMetaILAs();
  }

  clearSearchString() {
    this.filterSearchString = "";
    this.filterMetaILAs();
  }

  filterMetaILAs() {
    const searchTerm = this.filterSearchString.trim().toLowerCase();
    this.metaILAList = this.originalMetaILAList.filter(item => {
      return (
        (item?.name?.trim().toLowerCase().includes(searchTerm) && item?.active == this.showActive)
      );
    });
  }

  filterActive(filterType: boolean) {
    this.showActive = filterType ;
    this.filterMetaILAs();
  }

  onChangeMetaILA(selected: boolean, metaILA: MetaILA) {
    this.checkListSelection.clear();
    if (selected) {
      this.checkListSelection.select(metaILA);
      this.linkedId = metaILA.id;
    }
  }

  linkMetaILA() {
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }

}
