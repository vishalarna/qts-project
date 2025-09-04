import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Tool } from '@models/Tool/Tool';
import { ToolTree } from '@models/Tool/ToolTree';
import { ToolCategory } from '@models/ToolCategory/ToolCategory';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-tool-data-element',
  templateUrl: './fly-panel-tool-data-element.component.html',
  styleUrls: ['./fly-panel-tool-data-element.component.scss']
})
export class FlyPanelToolDataElementComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  treeControl = new NestedTreeControl<ToolTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ToolTree>();
  originalDataSource: ToolTree[] =[];
  treeCheckListSelection = new SelectionModel<ToolTree>(false);
  hasChild = (_: number, node: ToolTree) => !!node.children && node.children.length > 0;
  filterSearchString = "";
  linkedId: string;
  loader = false;
  showActive: boolean = true;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private toolService: ToolsService,
  ) { }

  ngOnInit(): void {
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.readyToolDataAsync();
  }

  clearSearchString() {
    this.filterSearchString = '';
    this.filterTool();
  }

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterTool();
  }

  filterTool() {
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
    this.filterTool();
  }


  async readyToolDataAsync() {
    this.loader = true;
    this.treeCheckListSelection.clear();
    var data = await this.toolService.getAllToolCategories(true);
    const filteredCategory = data.filter(category =>category.tools && category.tools.length > 0)
    this.makeToolTreeData(filteredCategory);
    this.loader = false;
  }

  makeToolTreeData(res: ToolCategory[]) {
    var treeData: any[] = [];
    res.forEach((toolCat: ToolCategory, index: any) => {
      treeData.push({ description: toolCat.title });
      treeData[index]["children"] = [];
      toolCat.tools.forEach((tool: Tool) => {
        treeData[index]["children"].push({ description: index + 1 + '.' + tool.number + ' - ' + tool.name, active: tool.active, id: tool.id });
      })
      treeData.forEach((data) => {
        data.children.forEach((elm) => {
          if (this.inputTrainingIssueDataElementVM?.dataElementId == elm.id) {
            this.treeControl.expand(data);
          }
        });
      })
    });
    this.dataSource.data = treeData;
      this.originalDataSource = Object.assign(treeData);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalDataSource[key], undefined);
      });
      this.treeControl.dataNodes = Object.assign(this.originalDataSource);
    this.filterTool();
  }

  private setParent(node: ToolTree, parent: ToolTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  onChangeTool(selected: boolean, node: ToolTree) {
    this.treeCheckListSelection.clear();
    if (selected) {
      this.treeCheckListSelection.select(node);
      this.linkedId = node.id;
    }
  }

  isCategorySelected(node: ToolTree) {
    return this.linkedId != null ?  node.children.some(x => x.id == this.linkedId) : false;
  }

  isToolSelected(node: ToolTree) {
    return this.linkedId != null ?  this.linkedId == node.id : false;
  }

  linkTool() {
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }

}