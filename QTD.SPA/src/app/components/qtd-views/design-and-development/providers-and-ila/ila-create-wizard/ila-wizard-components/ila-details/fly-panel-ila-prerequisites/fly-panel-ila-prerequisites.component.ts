import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { first, map } from 'rxjs/operators';
import { IlaPreRequisitesLinkService } from 'src/app/_Services/QTD/ila-pre-requisites-link.service';
import { ILAPrerequisitesLinkOptions } from 'src/app/_DtoModels/ILA_Prerequisites_Link/ILA_Prerequisites_LinkOptions';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { TopicService } from 'src/app/_Services/QTD/ila_topic.service';
import { ProviderService } from 'src/app/_Services/QTD/provider.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { SelectionModel } from '@angular/cdk/collections';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';


@Component({
  selector: 'app-fly-panel-ila-prerequisites',
  templateUrl: './fly-panel-ila-prerequisites.component.html',
  styleUrls: ['./fly-panel-ila-prerequisites.component.scss'],
})
export class FlyPanelIlaPrerequisitesComponent implements OnInit, OnDestroy {
  @Output() prerequisites = new EventEmitter<any>();
  @Output() preqList = new EventEmitter<any>();
  @Input() edit_pre: any;
  @Input() ILAID: any;
 
  showActive: boolean = true;
  Topics: Prerequisites_Categories[] = [];
  TopicILAs: Prerequisites_Topics[] = [];
  Providers: Prerequisites_Categories[] = [];
  ProviderILAs: Prerequisites_Topics[] = [];
  count_ilas: number = 0;
  checked_boxes: any[] = [];
  checkboxes: any[] = [];
  selectedFilter = "Provider";
  providerCount = 0;
  topicCount = 0;
  pre_category: Prerequisites_Categories[] = [];
  pre_topics: Prerequisites_Topics[] = [];
  positionIds: any[] = [];
  array1: any[] = [];
  filterIlaString: string = "";
  linkedIds: any[] = [];
  loading: boolean = false;
  IlaCheckListSelection = new SelectionModel<IlaTree>(true);
  originalSource = new MatTreeNestedDataSource<IlaTree>();
  dataSource = new MatTreeNestedDataSource<IlaTree>();
  treeControl = new NestedTreeControl<IlaTree>((node: any) => node.children);
  showLinkIlaLoader: boolean = false;
  hasChild = (_: number, node: IlaTree) =>
    !!node.children && node.children.length > 0;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    public topicService: TopicService,
    public providerService: ProviderService,
    private prerequsitesLinkService: IlaService,
    private rrService: RegulatoryRequirementService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    
    this.readyProviders();    
    if (this.edit_pre !== undefined) {
      this.edit_pre = this.edit_pre.map((data: any) => { return data.id });
      
    }
    else {
      this.edit_pre = [];
    }
    this.positionIds = this.edit_pre
  }

  ngOnDestroy(): void {
    
  }

  SelectedItem(item: any) {
    this.selectedFilter = item;
  }  

  async readyProviders() {
    this.loading = true;
    await this.rrService.getProviderWithILAs().then((res) => {
      var provData = res.map((x)=>{
        return{
          id:x.providerId,
          name:x.providerName,
          active:x.providerActive,
          ilAs:x.ilaDetails
        }
      })
      this.makeIlaTreeDataSource(provData);
      this.Providers = [];
      this.ProviderILAs = [];     
    })  
    this.loading = false;
  }

  makeIlaTreeDataSource(res: any) {
    this.IlaCheckListSelection.clear();
    this.linkedIds = [];
    if (res.length == 0) {
      this.dataSource.data = [];
    } else {
      var treeData: any = [{}];
      const filteredProviders = res.map(provider => {
        if (provider.ilAs) {
          const activeIlas = provider.ilAs.filter(ila => ila.active && ila.id !==this.ILAID);
          provider.ilAs = activeIlas;
        }
        return provider;
      }).filter(provider => provider.active && provider.ilAs && provider.ilAs.length > 0);
      filteredProviders.forEach((data, index) => {
        treeData[index] = {
          id: data.id,
          description: data.name,
          children: [],
          checkbox: true,         
        };
        
        data.ilAs.forEach(element => {
          treeData[index]['children'].push({
            id: element.id,
            description: element.number + " " + element.name,
            checkbox: !this.edit_pre.includes(element.id),            
            active: element.active,
          });
        });
      });
      this.dataSource.data = treeData;
      this.originalSource.data = treeData;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
      });           
      this.filterActive(true);
    }
  }

  filterData(data: any, toFilter: any) {
    let temparr = [
      ...this.originalSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => {
            return c.description.toLowerCase().match(String(this.filterIlaString).toLowerCase()) 
            && c.active == this.showActive;           
          }
          ),
        };
      }),
    ];
    temparr = temparr.filter((item)=>item.children && item.children.length>0)
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

  itemToggle(checked: boolean, node: IlaTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
     
      if (node.selected && node.checkbox) {
        this.linkedIds.push(node);
      }
      else {
        var index = this.linkedIds.indexOf(node);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      }
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  private checkAllParents(node: IlaTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => (child.checkbox && child.selected));
      node.parent.indeterminate = descendants.some((child) => (child.checkbox && child.selected));
      this.checkAllParents(node.parent);
    }
  }

  private setParent(node: IlaTree, parent: IlaTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  async addPreRequisitesLink() {
    var linkOptions: ILAPrerequisitesLinkOptions = new ILAPrerequisitesLinkOptions;
    
    this.positionIds = [];
    for (var i of this.checked_boxes) {
      this.positionIds.push(i.map((i: any) => i.id));
    }
    

    this.array1 = this.positionIds[0];
    this.array1 = this.array1.concat(this.positionIds[1]);
    this.edit_pre.map((item)=>{
      this.array1.push(item)
    })
    
    this.array1 = [...new Set(this.array1)];

    
    linkOptions.ILAId = this.ILAID;
    linkOptions.PreRequisiteIds = this.array1;

    await this.prerequsitesLinkService.LinkPrerequisites(this.ILAID, linkOptions).then(async (res) => {
      this.alert.successToast("Prerequisites Linked To " + await this.labelPipe.transform('ILA'));
      this.preqList.emit();      
      this.flyPanelSrvc.close();
      return res;
    }).catch((err) => {
      
    })

  }

  LinkToILA() {
    this.showLinkIlaLoader = true;
    this.checked_boxes = [];
    this.checkboxes = [];
    this.checked_boxes.push(this.linkedIds.filter((data) => { return data.selected }));
    this.checked_boxes.push(this.TopicILAs.filter((data) => { return data.checked }));
        
    var check: any[] = [];
    this.checkboxes = [];
    this.checked_boxes.forEach((e) => {
      e.forEach((i: any) => {
        if (this.checkboxes == null) {          
          check.push(i);
        }
        else {
          if (!check.find(x => x.id == i.id)) {
            check.push(i);
          }
        }
      });      
    });
    
    
    
    this.addPreRequisitesLink();  
    this.showLinkIlaLoader = false;   
  }
}

export class Prerequisites_Categories {
  id: any;
  description: string;
  count?: number;
  number?: number;
}

export class Prerequisites_Topics {
  id: any;
  description: string;
  number?: string;
  category_id: any;
  checked?: boolean;
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
