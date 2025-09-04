import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { RR_IssuingAuthorityCompact } from '@models/RR_IssuingAuthority/RR_IssuingAuthorityCompact';
import { RegulatoryRequirementsCompact } from '@models/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { RegulatoryRequirementsTree } from '@models/RegulatoryRequirements/RegulatoryRequirementsTree';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-regulatory-requirement-data-element',
  templateUrl: './fly-panel-regulatory-requirement-data-element.component.html',
  styleUrls: ['./fly-panel-regulatory-requirement-data-element.component.scss']
})
export class FlyPanelRegulatoryRequirementDataElementComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  treeControl = new NestedTreeControl<RegulatoryRequirementsTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<RegulatoryRequirementsTree>();
  originalDataSource : RegulatoryRequirementsTree[] = [];
  treeCheckListSelection = new SelectionModel<RegulatoryRequirementsTree>(false);
  hasChild = (_: number, node: RegulatoryRequirementsTree) => !!node.children && node.children.length > 0;
  filterSearchString: string;
  linkedId: string;
  showLinkRRLoader: boolean = false;
  loader: boolean = false;
  showActive: boolean = true;


  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private rrIAService: RRIssuingAuthorityService,
  ) { }

  ngOnInit(): void {
    this.filterSearchString = '';
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.loadAsync()
  }

  async loadAsync() {
    await this.getRegulatoryRequirementsAsync();
  }

  async getRegulatoryRequirementsAsync() {
    let treeData: RegulatoryRequirementsTree[] = [];
    this.loader = true;
    await this.rrIAService
      .GetRRWithIA()
      .then((res: RR_IssuingAuthorityCompact[]) => {
        res.forEach((item: RR_IssuingAuthorityCompact, index: any) => {
          if (item.active) {
            treeData.push({
              id: item.id,
              description: item.title,
              children: [],
            });
            item.regulatoryRequirementCompacts.forEach(
              (rr: RegulatoryRequirementsCompact) => {
                  treeData[index].children?.push({
                    id: rr.id,
                    description: `${rr.number} ` + ' - ' + rr.title,
                    active: rr.active,
                  });
              }
            );
          }
        });
        this.dataSource.data = treeData;
      this.originalDataSource = Object.assign(treeData);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalDataSource[key], undefined);
      });
      this.treeControl.dataNodes = Object.assign(this.originalDataSource);
  
    this.filterRegularityRequirement();
      }).finally(() => {
        this.loader = false;
      });
  }

  private setParent(node: RegulatoryRequirementsTree, parent: RegulatoryRequirementsTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  onChangeRegulatoryRequirement(selected: boolean, node: RegulatoryRequirementsTree) {
    this.treeCheckListSelection.clear();
    if (selected) {
      this.treeCheckListSelection.select(node);
      this.linkedId = node.id;
    }
  }

  clearSearchString() {
    this.filterSearchString = '';
    this.filterRegularityRequirement();
  }

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? '';
    this.filterRegularityRequirement();
  }

  filterRegularityRequirement() {
    let tempData = [
      ...this.originalDataSource.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => {
            return c.active == this.showActive && (this.filterSearchString.length > 0
                ? c.description.trim().toLowerCase().includes(this.filterSearchString.trim().toLowerCase()) : true);
          }
          ),
        };
      }),
    ];
    this.dataSource.data = tempData.filter((x) => { return x.children !== null && x.children !== undefined && x.children.length > 0 });
    this.treeControl.dataNodes = this.dataSource.data;
    this.filterSearchString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
    this.dataSource.data.forEach((data) => {
      data.children.forEach((elm) => {
        if (this.inputTrainingIssueDataElementVM?.dataElementId == elm.id) {
          this.treeControl.expand(data);
        }
      });
    })
  }

  filterActive(filterType: boolean) {
    this.showActive = filterType ;
    this.filterRegularityRequirement();
  }

  isRegulatoryRequirementSelected(node: RegulatoryRequirementsTree) {
    return this.linkedId != null ? this.linkedId == node.id : false;
  }

  isIssuingAuthoritySelected(node: RegulatoryRequirementsTree) {
    return this.linkedId != null ? node.children.some(x => x.id == this.linkedId) : false;
  }

  linkRegulatoryRequirement() {
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }

}
