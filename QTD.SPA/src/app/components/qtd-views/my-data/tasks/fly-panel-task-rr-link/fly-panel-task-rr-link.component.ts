import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { RR_IssuingAuthorityCompact } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityCompact';
import { Task_RR_LinkOptions } from 'src/app/_DtoModels/Task_RR_Link/Task_RR_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-task-rr-link',
  templateUrl: './fly-panel-task-rr-link.component.html',
  styleUrls: ['./fly-panel-task-rr-link.component.scss'],
})
export class FlyPanelTaskRrLinkComponent implements OnInit {
  filterRRString: string = "";
  linkRRs: boolean = true;
  addRR: boolean = false;
  showActive: boolean = true;
  @Output() closed = new EventEmitter<any>();
  @Output() refreshRRLinks = new EventEmitter<any>();
  @Input() alreadyLinkedIds: any[] = [];
  linkedIds: any[] = [];
  parentSelected: number = 0;
  treeControl = new NestedTreeControl<RRTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<RRTree>();
  originalData = new MatTreeNestedDataSource<RRTree>();
  hasChild = (_: number, node: RRTree) =>
    !!node.children && node.children.length > 0;

  RRCheckListSelection = new SelectionModel<RRTree>(true);
  showLinkRRLoader: boolean = false;

  taskId = '';
  subscription = new SubSink();

  constructor(
    private route: ActivatedRoute,
    private rrIAService: RRIssuingAuthorityService,
    private alert: SweetAlertService,
    private taskSrvc: TasksService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.taskId = String(res.id).split('-')[0];
      this.getRRsData();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  clearFilter(){
    this.filterRRString = '';
    this.getRRsData();
  }
  showRRLoader = false;
  async getRRsData() {
    this.showRRLoader = true;
    this.linkedIds = [];
    this.parentSelected = 0;
    let treeData: RRTree[] = [];

    await this.rrIAService
      .GetRRWithIA()
      .then((res: RR_IssuingAuthorityCompact[]) => {

        res.forEach((item: RR_IssuingAuthorityCompact, index: any) => {
          if(item.active){
              treeData.push({
            id: item.id,
            checkbox: true,
            description: item.title,
            children: [],
          });
          item.regulatoryRequirementCompacts.forEach(
            (rr: RegulatoryRequirementsCompact) => {
              if(rr.active){
                treeData[index].children?.push({
                id: rr.id,
                description: `${rr.number} ` + rr.title,
                active: rr.active,
                checkbox: !this.alreadyLinkedIds.includes(rr.id),
              });
              }

            }
          );
          }

        });

        this.dataSource.data = treeData;
        Object.assign(this.originalData.data, this.dataSource.data);
        Object.keys(this.dataSource.data).forEach((key: any) => {
          this.setParent(this.dataSource.data[key], undefined);
          this.setParent(this.originalData.data[key], undefined);
        });
        this.filterActive(true);
      }).finally(()=>{
        this.showRRLoader = false;
      });
  }

  onRRChange(event: any, node: any) {
    if (event.checked) {
      this.RRCheckListSelection.select(node);
    } else {
      this.RRCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }
  filtered(node: any) {

    return node.description.includes(this.filterRRString);
  }

  filterData(data: any, toFilter: any) {
    let temparr = [
      ...this.originalData.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => {
            return c.description
              .toLowerCase()
              .match(String(this.filterRRString).toLowerCase()) && c.active === this.showActive
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
    this.dataSource.data = temparr;
    this.treeControl.dataNodes = this.dataSource.data;
    this.filterRRString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  filterActive(makeActive: boolean) {
    this.showActive = makeActive;
    this.filterData("", "");
  }

  itemToggle(checked: boolean, node: RRTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined) {
      // Increment and decrement selected parent count
      checked ? this.parentSelected++ : this.parentSelected--;

      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add RRIds to list
      if (node.selected && node.checkbox) {
        this.linkedIds.push(node.id);
      } else {
        var index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      }
    }
    //Removes duplicate Ids just in case
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  private setParent(node: RRTree, parent: RRTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: RRTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every(
        (child) => child.selected && child.checkbox
      );
      node.parent.indeterminate = descendants.some(
        (child) => child.selected && child.checkbox
      );
      this.checkAllParents(node.parent);
    }
  }

  linkToTask() {
    this.showLinkRRLoader = true;
    let options: Task_RR_LinkOptions = {
      regulatoryRequirementIds: this.linkedIds,
      taskId: this.taskId,
    };
    this.taskSrvc.LinkRR(this.taskId, options).then(async (res) => {
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + ' linked with '+ await this.transformTitle('Task'));
      this.closed.emit('rr-linked with '+ await this.transformTitle('Task'));
      this.refreshRRLinks.emit('refresh linked RRs table');
    }).finally(() => {
      this.showLinkRRLoader = false;
    })
  }

  refreshRRData() {

    this.linkedIds = [];
    this.RRCheckListSelection = new SelectionModel<RRTree>(true);
    this.getRRsData();
  }
}

class RRTree {
  id: any;
  description: string;
  children?: RRTree[];
  checkbox?: boolean;
  selected?: boolean;
  active?: boolean;
  indeterminate?: boolean;
  parent?: RRTree;
}
