import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-task-ila-link',
  templateUrl: './fly-panel-task-ila-link.component.html',
  styleUrls: ['./fly-panel-task-ila-link.component.scss']
})
export class FlyPanelTaskIlaLinkComponent implements OnInit, AfterViewInit, OnDestroy {
  filterBy: string = 'Provider';
  filterIlaString: string = "";
  linkIlas: boolean = true;
  addIla: boolean = false;
  showActive: boolean = true;
  isILALoading: boolean = false;
  isProvider = true;
  @Output() closed = new EventEmitter<any>();
  linkedIds: any[] = [];
  parentSelected: number = 0;
  treeControl = new NestedTreeControl<IlaTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<IlaTree>();
  originalSource = new MatTreeNestedDataSource<IlaTree>();
  hasChild = (_: number, node: IlaTree) =>
    !!node.children && node.children.length > 0;

  IlaCheckListSelection = new SelectionModel<IlaTree>(true);
  showLinkIlaLoader: boolean = false;
  @Input() id: any;
  @Input() alreadyLinked: any[] = [];
  @Output() refresh = new EventEmitter<any>();
  subscription = new SubSink();
  constructor(
    private route: ActivatedRoute,
    private ilaService: IlaService,
    private shService: SafetyHazardsService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private taskService: TasksService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.id = String(res.id).split('-')[0];
      this.readyIlasTreeData();
    })


    // To refresh ILAs when we link from fly panel.
    this.subscription.sink =
      this.dataBroadcastService.updateProcILALink.subscribe((res: any) => {
        this.getProviderData();
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  clearCheck:boolean=true;
  clearFilter(name:string){
    switch(name){
      case 'search':
        this.filterIlaString = '';
        this.readyIlasTreeData();
        break;

      case 'provider':
        this.clearCheck = false;
        break;

      case 'topic':
        this.clearCheck = false;
        break;

    }

  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async readyIlasTreeData() {
    this.getProviderData();
  }

  async getProviderData() {
    this.isILALoading = true;
    this.dataSource.data = [];
    await this.shService
      .getProviderWithILAs()
      .then((res) => {

        this.makeIlaTreeDataSource(res);
      }).finally(()=>{
        this.isILALoading = false;
      });
  }

  async getTopicData() {
    this.dataSource.data = [];
    this.isILALoading = true;
    await this.shService
      .getTopicWithILAs()
      .then((res) => {
        var tempSrc =[];
        res.forEach(topic=>{
          tempSrc.push({
            id:topic.topicId,
            name:topic.topicName,
            active:topic.topicActive,
            ilAs:topic.ilaDetails
          })
        })
        this.makeIlaTreeDataSource(tempSrc);
      })
      .catch((err: any) => {
        console.error(err);
      });
  }

  makeIlaTreeDataSource(res: any) {
    this.IlaCheckListSelection.clear();
    this.linkedIds = [];
    if (res.length == 0) {
      this.dataSource.data = [];
      this.isILALoading = false;
    }
    else {
      this.isILALoading = true;
      var treeData: any = [{}];

      for (var data in res) {

        treeData[data] = {
          id: res[data]['id'],
          description: res[data]['name'],
          children: [],
          checkbox: true,
        };

        res[data].ilAs.forEach(element => {
          treeData[data]['children'].push({
            id: element.id,
            description: element.number + " - " + element.name,
            checkbox: !this.alreadyLinked.includes(element.id),
            active: element.active,
          })
        });
      }

      this.dataSource.data = treeData;
      this.originalSource.data = treeData;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
      });
      this.filterActive(true);


      this.isILALoading = false;
    }

  }

  filterActive(makeActive: boolean) {
    this.showActive = makeActive;
    this.filterData("", "");
  }

  onIlaChange(event: any, node: any) {
    if (event.checked) {
      this.IlaCheckListSelection.select(node);
    } else {
      this.IlaCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }
  filtered(node: any) {
    return node.description.includes(this.filterIlaString);
  }

  filterData(data: any, toFilter: any) {
    let temparr = [
      ...this.originalSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => {
            return c.description.toLowerCase().match(String(this.filterIlaString).toLowerCase()) &&
              c.active == this.showActive;
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
      node.children?.forEach((ila) => {
        this.checkAllParents(ila);
      });
    });

    this.treeControl.dataNodes = this.dataSource.data;
    this.filterIlaString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  itemToggle(checked: boolean, node: IlaTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add TaskIds to list
      if (node.selected && node.checkbox) {
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

  private setParent(node: IlaTree, parent: IlaTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: IlaTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => (child.checkbox && child.selected));
      node.parent.indeterminate = descendants.some((child) => (child.checkbox && child.selected));
      this.checkAllParents(node.parent);
    }
  }
  refreshILAsTree() {
    this.IlaCheckListSelection.clear();
    this.linkedIds = [];
    this.readyIlasTreeData();
  }

  addILA() {
    this.linkedIds = [];
  }

  linkILA() {
    this.showLinkIlaLoader = true;
    var options = new TaskOptions();
    options.ilaIds = this.linkedIds;
    this.taskService.linkILA(this.id, options).then(async (_) => {
      this.alert.successToast("Linked Selected " + await this.labelPipe.transform('ILA') + "s to " + await this.transformTitle('Task') );
      this.refresh.emit();
      this.closed.emit();
    }).finally(() => {
      this.showLinkIlaLoader = false;
    })
  }
}

class IlaTree {
  id: any;
  description: string;
  children?: IlaTree[];
  active?: boolean;
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: IlaTree;
}
