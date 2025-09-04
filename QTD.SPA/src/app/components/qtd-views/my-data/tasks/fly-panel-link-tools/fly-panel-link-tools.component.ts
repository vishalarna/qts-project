import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { ToolAddOptions } from 'src/app/_DtoModels/Tool/ToolAddOptions';
import { ToolCategory } from 'src/app/_DtoModels/ToolCategory/ToolCategory';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-link-tools',
  templateUrl: './fly-panel-link-tools.component.html',
  styleUrls: ['./fly-panel-link-tools.component.scss']
})
export class FlyPanelLinkToolsComponent implements OnInit {
  linkTools:boolean = true;
  filterToolString = "";
  addTool:boolean = false;
  showActive: boolean = true;
  linkedIds: any[] = [];
  spinner = false;
  i=0;

  treeControl = new NestedTreeControl<ToolTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ToolTree>();
  originalDataSource = new MatTreeNestedDataSource<ToolTree>();
  TaskTreeCheckListSelection = new SelectionModel<ToolTree>(true);

  hasChild = (_: number, node: ToolTree) =>
    !!node.children && node.children.length > 0;

  @Output() closed = new EventEmitter<any>();
  @Input() alreadyLinked: any[] = [];
  @Input() taskId = "";

  constructor(
    private toolService : ToolsService,
    private TaskService : TasksService,
    private alert : SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.readyData();
  }

  clearFilter(){
    this.filterToolString = null;
    this.readyData();
  }

  async readyData(){
    this.TaskTreeCheckListSelection.clear();
    this.linkedIds = [];
    var data = await this.toolService.getAllToolCategories(true);
    this.makeToolTreeData(data);
  }

  makeToolTreeData(res: ToolCategory[]) {

    var treeData: any[] = [];
    res.forEach((toolCat: ToolCategory, index: any) => {
      
      treeData.push({ description: toolCat.title, checkbox: true });
      treeData[index]["children"] = [];
      toolCat.tools.forEach((tool: Tool) => {
        treeData[index]["children"].push({ description: index+1 + '.' + tool.number + ' - ' + tool.name, active: tool.active, checkbox: !this.alreadyLinked.includes(tool.id), id: tool.id });
      })
    });
    this.dataSource.data = [];
    this.dataSource.data = Object.assign([],treeData);
    this.originalDataSource.data = Object.assign([],this.dataSource.data);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalDataSource.data[key], undefined);
    });

    this.filterActive(true);
  }

  filterData(data: any, toFilter: any) {
    if (this.filterToolString.length > 0) {
      let temparr = [
        ...this.originalDataSource.data.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) => {
              return (c.description.toLowerCase().match(String(this.filterToolString).toLowerCase()))
                && c.active == this.showActive;
            }
            ),
          };
        }),
      ];

      this.dataSource.data = temparr;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
      });

      this.dataSource.data.forEach((node) => {
        node.children?.forEach((child) => {
          this.checkAllParents(child);
        })
      });
      this.treeControl.dataNodes = this.dataSource.data;
      this.filterToolString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
    } else {
      this.dataSource.data = this.originalDataSource.data;
    }
  }

  private setParent(node: ToolTree, parent: ToolTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: ToolTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  filterActive(makeActive: boolean) {

    let temparr = [
      ...this.originalDataSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => {
            return c.active == makeActive && (this.filterToolString.length > 0
                ? c.description.trim().toLowerCase().includes(this.filterToolString.trim().toLowerCase()) : true);
          }
          ),
        };
      }),
    ];
    this.showActive = makeActive;
    this.dataSource.data = temparr;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((node) => {
      node.children?.forEach((child) => {
        this.checkAllParents(child);
      })
    })
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.TaskTreeCheckListSelection.select(node);
    } else {
      this.TaskTreeCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  itemToggle(checked: boolean, node: ToolTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add EOIds to list
      if (checked && node.checkbox) {
        this.linkedIds.push(node.id);
      }
      else {
        var index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      }
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  linkToTask(){
    this.spinner = true;
    var options = new ToolAddOptions();
    options.toolIds = this.linkedIds;
    this.TaskService.LinkTool(this.taskId,options).then(async (_)=>{
      this.alert.successToast("Linked Selected " + await this.labelPipe.transform('Tool') + "s To " + await this.transformTitle('Task'));
      this.closed.emit('fp-task-tool-link-closed');
    }).finally(()=>{
      this.spinner = false;
    })
  }

}

class ToolTree {
  id: any;
  description: string;
  children?: ToolTree[];
  checkbox?: boolean;
  active?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: ToolTree;
}
