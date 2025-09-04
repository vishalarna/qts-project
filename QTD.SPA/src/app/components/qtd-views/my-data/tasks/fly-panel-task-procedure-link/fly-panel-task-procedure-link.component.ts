import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output, AfterViewInit, OnDestroy } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-task-procedure-link',
  templateUrl: './fly-panel-task-procedure-link.component.html',
  styleUrls: ['./fly-panel-task-procedure-link.component.scss']
})
export class FlyPanelTaskProcedureLinkComponent implements OnInit, AfterViewInit, OnDestroy {
  filterProcString: string = "";
  linkProcs: boolean = true;
  addProc: boolean = false;
  showActive: boolean = true;
  @Output() closed = new EventEmitter<any>();
  @Input() alreadyLinked: any[] = [];
  @Output() refresh = new EventEmitter<any>();
  linkedIds: any[] = [];
  treeControl = new NestedTreeControl<ProcTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ProcTree>();
  hasChild = (_: number, node: ProcTree) =>
    node.children && node.children.length > 0;
  rrId: any;
  TreeCheckListSelection = new SelectionModel<ProcTree>(true);
  showLinkLoader: boolean = false;
  subsink = new SubSink();
  treeData: ProcTree[] = [];

  taskId = "";
  subscription = new SubSink();

  constructor(
    private procIAService: IssuingAuthoritiesService,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    private taskService: TasksService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.readyProcsTreeData();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.taskId = String(res.id).split('-')[0];
    })
  }

  ngOnDestroy(): void {

  }

  clearFilter(){
    this.filterProcString = null;
    this.readyProcsTreeData();
  }

  dataLoader = false;

  async readyProcsTreeData() {
    this.dataLoader = true;
    this.TreeCheckListSelection.clear();
    this.linkedIds = [];
    await this.procIAService
      .getAll()
      .then((res) => {

        this.makeTreeDataSource(res);
      })
      .finally(() => {
        this.dataLoader = false;
      });
  }

  makeTreeDataSource(data: Procedure_IssuingAuthority[]) {
    var treeData: any[] = [];
    let procTree: any[] = [];
    for (let index = 0; index < data.length; index++) {
      if(data[index].active){
        procTree[index] = {
        description: data[index].title,
        checkbox: true,
        children: [],
      }

      data[index].procedures.forEach((proc) => {
        if(proc.active){
             procTree[index].children?.push({
          id: proc.id,
          description: `${proc.number} - ` + proc.title,
          checkbox: !this.alreadyLinked.includes(proc.id),
          active: proc.active,
        });
        }

      });
      treeData.push(procTree);

      }

    }
    this.treeData = Object.assign(treeData, procTree);

    this.dataSource.data = Object.assign([], this.treeData);
    this.treeControl.dataNodes = Object.assign([], this.dataSource.data);
    Object.keys(this.dataSource.data).forEach((key: any) => {

      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.treeData[key], undefined);
    });

    this.toggleFilter(this.showActive);
  }

  private setParent(node: any, parent: any | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: any) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.filter((f)=>{
        return !this.alreadyLinked.includes(f.id);
      }).every((child) => child.selected);
      node.parent.indeterminate = descendants.filter((f)=>{
        return !this.alreadyLinked.includes(f.id);
      }).some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  filterData(data: any, toFilter: any) {
    var tempArr = [
      ...this.treeData.map((d) => {
        return {
          ...d,
          children: d.children?.filter((x) => {
            return x.description
              .toLowerCase().trim()
              .includes(String(this.filterProcString.trim().toLowerCase())) && x.active == this.showActive
          }
          ),
        };
      }),
    ];

    this.dataSource.data = tempArr;

    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((node: ProcTree) => {
      node.children?.forEach((child) => {
        this.checkAllParents(child);
      })
    });

    this.treeControl.dataNodes = this.dataSource.data;
    this.filterProcString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  toggleFilter(active: boolean) {
    this.showActive = active;
    this.filterData("", "");
  }

  onProcChange(event: any, node: any) {
    if (event.checked) {
      this.TreeCheckListSelection.select(node);
    } else {
      this.TreeCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  itemToggle(checked: boolean, node: any) {
    node.selected = node.checkbox ? checked : false;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add EOIds to list
      if (node.selected) {
        this.linkedIds.push(node.id)
      }
      else {
        var index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1)
        }
      };
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  linkToTask() {
    this.showLinkLoader = true;
    var options = new TaskOptions();
    options.procedureIds = this.linkedIds;
    this.taskService.LinkProcedures(this.taskId, options).then(async (_) => {
      this.alert.successToast("Successfully Linked Selected " + await this.transformTitle('Procedure') + "s to " + await this.labelPipe.transform('Task'));
      this.closed.emit('fp-rr-Proc-link-closed');
      this.refresh.emit();
    }).finally(() => {
      this.showLinkLoader = false;
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

}

class ProcTree {
  id: any;
  description: string;
  children?: ProcTree[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: ProcTree;
  active?: boolean;
}
