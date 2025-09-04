import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { Procedure_ILA_LinkOptions } from 'src/app/_DtoModels/Procedure/Procedure_ILA_Link/Procedure_ILA_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-add-procedure-ilas-link',
  templateUrl: './fly-panel-procedure-ilas-link.component.html',
  styleUrls: ['./fly-panel-procedure-ilas-link.component.scss'],
})
export class FlyPanelProcedureIlasLinkComponent implements OnInit {
  showLoader=true
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
  @Input() id: any;
  subscription = new SubSink();
  IlaCheckListSelection = new SelectionModel<IlaTree>(true);
  showLinkIlaLoader: boolean = false;

  @Input() alreadyLinked:any[] = [];

  constructor(private route: ActivatedRoute,
    private procService: ProceduresService,
    private ilaService: IlaService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,) {}

  ngOnInit(): void {
    //this.getDummyILAsData();
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
    this.showLoader=true;
    await this.procService
      .getProviderWithILAs()
      .then((res) => {
        var provData = res.map((x)=>{
          return{
            id:x.providerId,
            name:x.providerName,
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
    await this.procService
      .getTopicWithILAs()
      .then((res) => {
        var tempSrc =[];
        res.forEach(topic=>{
          tempSrc.push({
            id:topic.topicId,
            name:topic.topicName,
            ilAs:topic.ilaDetails,
          })
        })
        this.makeIlaTreeDataSource(tempSrc);
      })
      .catch((err: any) => {
        console.error(err);
      });
  }

  makeIlaTreeDataSource(res: any) {
    
    if(res.length == 0)
    {
      this.dataSource.data = [];
    }
    else
    {
      var treeData: any = [{}];
      for (var data in res) {

        treeData[data] = {
          id: res[data]['id'],
          description: res[data]['name'],
          children: res[data]['ilAs'],
          checkbox: true,
        };
        
        for(var data1 in treeData[data]['children']){
          treeData[data]['children'][data1]['checkbox'] = this.alreadyLinked.includes(treeData[data]['children'][data1]['id']) ? false:true;
          treeData[data]['children'][data1]['description'] =treeData[data]['children'][data1]['name'];
        }
       }


      this.dataSource.data = treeData;

      this.originalSource.data = treeData;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
      });
      


      this.filterActive(true);
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

      this.dataSource.data.forEach((node) => {
        node.children?.forEach((child) => {
          this.checkAllParents(child);
        })
      });
      this.treeControl.dataNodes = this.dataSource.data;
      this.filterIlaString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
    } else {
      this.dataSource.data = this.originalSource.data;
    }
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
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  LinkToProcedure() {
    this.showLinkIlaLoader = true;
    var options = new Procedure_ILA_LinkOptions();
    options.procedureId = this.id;
    options.iLAIds = this.linkedIds;
    this.procService.LinkILAs(this.id, options).then(async (res: any) => {
      this.alert.successToast("Linked All Selected "  + await this.labelPipe.transform('ILA') +"s");
      this.showLinkIlaLoader = false;
      this.dataBroadcastService.updateProcILALink.next(null);
      this.closed.emit();
    }).catch(async (err: any) => {
      this.alert.errorToast("Error Linking "  + await this.labelPipe.transform('ILA') +" to " + await this.transformTitle('Procedure') + " " + err);
      this.showLinkIlaLoader = false;
    })
  }

  addILA(){
    this.linkedIds = [];
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
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
