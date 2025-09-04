import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { SafetyHazardTree } from '@models/SaftyHazard/SafetyHazardTree';
import { SaftyHazard_CategoryCompactOptions } from '@models/SaftyHazard_Category/SaftyHazard_CategoryCompactOptions';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-safety-hazard-data-element',
  templateUrl: './fly-panel-safety-hazard-data-element.component.html',
  styleUrls: ['./fly-panel-safety-hazard-data-element.component.scss']
})
export class FlyPanelSafetyHazardDataElementComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  filterSearchString: string = "";
  shCatWithSh: SaftyHazard_CategoryCompactOptions[] = [];
  linkedId: string;
  treeControl = new NestedTreeControl<SafetyHazardTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<SafetyHazardTree>();
  originalDataSource : SafetyHazardTree[] =[];
  hasChild = (_: number, node: SafetyHazardTree) => !!node.children && node.children.length > 0;
  treeCheckListSelection = new SelectionModel<SafetyHazardTree>(false);
  loader: boolean = false;
  showActive: boolean = true;

  constructor(
    private shService: SafetyHazardsService,
    public flyPanelSrvc: FlyInPanelService
  ) { }

  ngOnInit(): void {
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.getSafetyHazardCategoryDetails();
  }

  clearSearchString() {
    this.filterSearchString = '';
    this.filterSafetyHazard();
  }

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterSafetyHazard();
  }

  filterSafetyHazard() {
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
    this.filterSafetyHazard();
  }

  async getSafetyHazardCategoryDetails() {
    this.loader = true;
    await this.shService.getSHCategoryWithSH().then((res: SaftyHazard_CategoryCompactOptions[]) => {
      this.makeSafetyHazardTreeDataSource(res);
      Object.assign(this.shCatWithSh, res);
    }).finally(() => {
      this.loader = false;
    });
  }

  makeSafetyHazardTreeDataSource(res: SaftyHazard_CategoryCompactOptions[]) {
    var treeData: any[] = [];
    for (let index = 0; index < res.length; index++) {
      let procTree = new SafetyHazardTree();
      if (res[index].saftyHazard_Category.active) {
        procTree.description = res[index].saftyHazard_Category.title;
        procTree.children = [];

        res[index].saftyHazardCompactOptions.forEach((proc) => {
            procTree.children?.push({
              id: proc.id,
              description: proc.number + ' - ' + proc.title,
              active: proc.active,
            });
        });
        treeData.push(procTree);
        treeData.forEach((data) => {
          data.children.forEach((elm) => {
            if (this.inputTrainingIssueDataElementVM?.dataElementId == elm.id) {
              this.treeControl.expand(data);
            }
          });
        })
      }
    }

    this.dataSource.data = treeData;
    this.originalDataSource = Object.assign(treeData);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalDataSource[key], undefined);
    });
    this.treeControl.dataNodes = Object.assign(this.originalDataSource);
  
this.filterSafetyHazard();
  }

  private setParent(node: SafetyHazardTree, parent: SafetyHazardTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  onChangeHazard(selected: boolean, node: SafetyHazardTree) {
    this.treeCheckListSelection.clear();
    if (selected) {
      this.treeCheckListSelection.select(node);
      this.linkedId = node.id;
    }
  }

  isHazardOptionSelected(node: SafetyHazardTree) {
    return this.linkedId != null ? this.linkedId == node.id : false;
  }

  isHazardCategorySelected(node: SafetyHazardTree) {
    return this.linkedId != null ? node.children.some(x => x.id == this.linkedId) : false;
  }
  
  linkSafetyHazard() {
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }
}

