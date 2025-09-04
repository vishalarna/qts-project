import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { RR_EO_LinkOptions } from 'src/app/_DtoModels/RR_EnablingObjective/RR_EO_LinkOptions';
import { RR_ILA_LinkOptions } from 'src/app/_DtoModels/RR_ILA/RR_ILA_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-rr-ilas-link',
  templateUrl: './fly-panel-rr-ilas-link.component.html',
  styleUrls: ['./fly-panel-rr-ilas-link.component.scss']
})
export class FlyPanelRRIlasLinkComponent implements OnInit, AfterViewInit {
  showLoader=true;
  filterBy: string = 'Provider';
  filterIlaString: string;
  linkIlas: boolean = true;
  addIla: boolean = false;
  showActive: boolean = true;
  isILALoading: boolean = false;
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
  @Input() alreadyLinked:any[] = [];
  @Output() refresh = new EventEmitter<any>();
  subscription = new SubSink();

  constructor(private route: ActivatedRoute,
    private rrService: RegulatoryRequirementService,
    private ilaService: IlaService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe) {}

  ngOnInit(): void {
    this.readyIlasTreeData();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.id = res.id;
    })

    // To refresh ILAs when we link from fly panel.
    this.subscription.sink =
      this.dataBroadcastService.updateProcILALink.subscribe((res: any) => {
        this.getProviderData();
      });
  }

  clearFilter(){
    this.filterIlaString = '';
    this.dataSource.data = this.originalSource.data;
  }

  async readyIlasTreeData() {
    this.getProviderData();
  }

  async getProviderData(){
    this.isILALoading = true;
    this.showLoader=true;
    this.dataSource.data = [];
    await this.rrService
      .getProviderWithILAs()
      .then((res) => {
        var provData = res.map((x)=>{
          return{
            id:x.providerId,
            name:x.providerName,
            active:x.providerActive,
            ilAs:x.ilaDetails
          }
        })
        this.makeIlaTreeDataSource(provData);
      })
      .catch((err: any) => {
        console.error(err);
      }).finally(()=>{
        this.showLoader=false;
    
          });
  }

  async getTopicData(){
    this.dataSource.data = [];
    this.isILALoading = true;
    await this.rrService
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
     ;
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
           ;
          for(var data1 in treeData[data]['children']){
            treeData[data]['children'][data1]['checkbox'] = this.alreadyLinked.includes(treeData[data]['children'][data1]['id']) ? false:true;
            treeData[data]['children'][data1]['description'] =  treeData[data]['children'][data1]['number']+ ' - ' + treeData[data]['children'][data1]['name'];
          }
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
    let temparr = [
      ...this.originalSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) =>
          c.active == makeActive
          ),
        };
      }),
    ];
    this.showActive = makeActive;
    
    this.dataSource.data = temparr;
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
    if (this.filterIlaString.length > 0) {
      let temparr = [
        ...this.originalSource.data.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) =>
              c.description.toLowerCase().match(String(this.filterIlaString).toLowerCase())
            ),
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
      this.dataSource.data = this.originalSource.data;
    }
  }

  itemToggle(checked: boolean, node: IlaTree) {
     ;
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

  linkILAToRR() {
     ;
    this.showLinkIlaLoader = true;
    var options = new RR_ILA_LinkOptions();
    options.regRequirementId = this.id;
    options.ilaIds = this.linkedIds;
    
    this.rrService.LinkILA(this.id, options).then(async (res: any) => {
      this.refresh.emit();
      //this.refreshLinkedTasks();
      this.alert.successToast(await this.labelPipe.transform('ILA') +"s Linked To " + await this.labelPipe.transform('Regulatory Requirement') );
      this.closed.emit('fp-rr-ila-link-closed');
    }).finally(()=>{
      this.showLinkIlaLoader = false;
    });
  }

  refreshILAsTree(){
    this.IlaCheckListSelection.clear();
    this.linkedIds = [];
    this.readyIlasTreeData();
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
