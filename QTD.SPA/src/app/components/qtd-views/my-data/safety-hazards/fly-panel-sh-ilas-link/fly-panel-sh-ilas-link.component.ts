import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { SH_ILA_LinkOptions } from 'src/app/_DtoModels/SH_ILA_Link/SH_ILA_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-sh-ilas-link',
  templateUrl: './fly-panel-sh-ilas-link.component.html',
  styleUrls: ['./fly-panel-sh-ilas-link.component.scss'],
})
export class FlyPanelShIlasLinkComponent implements  OnInit, AfterViewInit {
  filterBy: string = 'Provider';
  filterIlaString: string;
  linkIlas: boolean = true;
  addIla: boolean = false;
  showActive: boolean = true;
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
  subscription = new SubSink();
  @Input() id: any;
  isILALoading: boolean = false;
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinked:any[] = [];
  myData:any[]=[];

  constructor(private route: ActivatedRoute,
    private shService: SafetyHazardsService,
    private ilaService: IlaService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe:LabelReplacementPipe) {}

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.id = res.id;
    });

    this.getProviderData();
  }

  clearFilter(){
    this.filterIlaString = '';
    this.dataSource.data = this.originalSource.data
  }

  async readyIlasTreeData() {
    this.getProviderData();
  }

  async getProviderData(){
    this.isILALoading = true;
    this.dataSource.data = [];

    await this.shService
      .getProviderWithILAs()
      .then((res) => {


        this.makeIlaTreeDataSource(res);
      })
      .catch((err: any) => {
        console.error(err);
      });
  }

  async getTopicData(){
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

    let childIndex = 0;
    if(res.length == 0)
    {
      this.dataSource.data = [];
      this.isILALoading = false;
    }
    else
    {
      this.isILALoading = true;
      var treeData: any = [{}];
      for (var data in res) {


        treeData[data] = {
          id: res[data]['id'],
          description: res[data]['name'],
          children: res[data]['ilAs'],
          checkbox: true,
        };

        for(var data1 in treeData[data]['children']){

         //
          treeData[data]['children'][data1]['checkbox'] = this.alreadyLinked.includes(treeData[data]['children'][data1]['id']) ? false:true;
          //treeData[data]['children'][data1]['checkbox']=  (childIndex+1) + ' - ' + treeData[data]['children'][data1]['description'];
          treeData[data]['children'][data1]['description'] =  treeData[data]['children'][data1]['number']+ ' - ' + treeData[data]['children'][data1]['name'];

        }
      }
      this.dataSource.data = treeData;
      this.originalSource.data = treeData;
      this.myData = Object.assign(treeData);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
        this.setParent(this.myData[key], undefined);
      });
      this.toggleFilter(true);


      this.isILALoading = false;
    }

  }

  toggleFilter(isActive: boolean) {
    var temp:any[] = Object.assign([],this.myData);
    this.dataSource.data = [
      ...temp.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => c.active === isActive),
        };
      }),
    ];
    //this.filterData();
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((node:IlaTree)=>{
      node.children?.forEach((child)=>{
        this.checkAllParents(child);
      })
    })
    this.showActive = isActive;
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

  filterData() {
    if (this.filterIlaString.length > 0) {
      let temparr = [
        ...this.myData.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) => {return c.description.toLowerCase().trim()
              .includes(this.filterIlaString.toLowerCase().trim())}),
          };
        }),
      ];
      this.dataSource.data = temparr;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
      });

      this.dataSource.data.forEach((node:IlaTree)=>{
        node.children?.forEach((child)=>{
          this.checkAllParents(child);
        })
      })
      this.treeControl.dataNodes = this.dataSource.data;
      this.filterIlaString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
    }
    else {
      this.dataSource.data = this.myData;
    }


  }

  itemToggle(checked: boolean, node: IlaTree) {
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
        const index = this.linkedIds.indexOf(node.id);
        if(index > -1){
          this.linkedIds.splice(index, 1);
        }
      };
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
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  linkILAToSH() {
    this.showLinkIlaLoader=true;
    var options = new SH_ILA_LinkOptions();
    options.safetyHazardId = this.id;
    options.iLAIds = this.linkedIds;

    this.shService.LinkILA(this.id, options).then(async (res: any) => {
      this.refresh.emit();
      this.refreshLinkedILAs();
      this.alert.successToast( await this.labelPipe.transform('ILA') + `(s) Linked To ${await this.labelPipe.transform("Safety Hazard")}`);
      this.closed.emit('fp-sh-ila-link-closed');
    }).finally(()=>{
    this.showLinkIlaLoader=false;
    });
  }

  refreshLinkedILAs() {

    this.IlaCheckListSelection.clear();
    this.linkedIds.forEach((id: any) => {
      this.alreadyLinked.push(id);
    });
    this.readyIlasTreeData();
    this.linkedIds = [];
  }

  addILA(){
    this.linkedIds = [];
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
