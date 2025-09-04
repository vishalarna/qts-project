import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { RR_IssuingAuthorityCompact } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityCompact';
import { SaftyHazard_RR_LinkOptions } from 'src/app/_DtoModels/SaftyHazard_RR_Link/SaftyHazard_RR_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-sh-rr-link',
  templateUrl: './fly-panel-sh-rr-link.component.html',
  styleUrls: ['./fly-panel-sh-rr-link.component.scss'],
})
export class FlyPanelShRrLinkComponent implements OnInit, AfterViewInit, OnDestroy {
  filterRRString: string = "";
  linkRRs: boolean = true;
  isLoading: boolean = false;
  addRR: boolean = false;
  showActive: boolean = true;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();

  @Input() alreadyLinkedIds: any[] = [];
  linkedIds: any[] = [];
  parentSelected: number = 0;
  treeControl = new NestedTreeControl<RRTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<RRTree>();
  originalData = new MatTreeNestedDataSource<RRTree>();

  subscriptions = new SubSink();
  shId = "";
  myData:any[]= [];

  hasChild = (_: number, node: RRTree) =>
    !!node.children && node.children.length > 0;

  RRCheckListSelection = new SelectionModel<RRTree>(true);
  showLinkRRLoader: boolean = false;

  constructor(
    private rrIAService: RRIssuingAuthorityService,
    private route: ActivatedRoute,
    private shService: SafetyHazardsService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
      this.readyRRTreeData();
    })
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  clearFilter(){
    this.filterRRString = '';
    this.readyRRTreeData();
  }

  async readyRRTreeData() {
    this.linkedIds = [];
    this.parentSelected = 0;
    let treeData: RRTree[] = [];
    this.isLoading=true;
    await this.rrIAService.GetRRWithIA().then((res: RR_IssuingAuthorityCompact[]) => {

      res.forEach((item: RR_IssuingAuthorityCompact, index: any) => {
        if(item.active){
          treeData.push({ id: item.id, checkbox: true, description: item.title, children: [],indeterminate:false,selected:false });
          item.regulatoryRequirementCompacts.forEach((rr: RegulatoryRequirementsCompact) => {
            treeData[index].children?.push({ id: rr.id, description: rr.number + '-' +rr.title, active: rr.active, checkbox: !this.alreadyLinkedIds.includes(rr.id),selected:false });
          })
        }

      })

      this.dataSource.data = treeData;
      this.myData = Object.assign([],treeData);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.myData[key], undefined);
      });
      this.toggleFilter(this.showActive);
    }).finally(()=>{
    this.isLoading=false;
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
  // filtered(node: any) {
  //   return node.description.includes(this.filterRRString);
  // }

  itemToggle(checked: boolean, node: RRTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children) {
      // Increment and decrement selected parent count
      checked ? this.parentSelected++ : this.parentSelected--;

      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add RRIds to list
      if (node.selected) {
        this.linkedIds.push(node.id);
      }
      else {
        const index = this.linkedIds.indexOf(node.id);
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
      node.parent['selected'] = descendants.every((child) => child.selected);
      node.parent['indeterminate'] = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  async linkRR() {
    this.showLinkRRLoader = true;
    var options = new SaftyHazard_RR_LinkOptions();
    options.regulatoryRequirementIds = this.linkedIds;
    options.safetyHazardId = this.shId;
    await this.shService.linkRR(this.shId, options).then(async (res: any) => {
      this.alert.successToast(`Regulations Linked To ${await this.labelPipe.transform("Safety Hazard")} Successfully`);
      this.refresh.emit();
      this.closed.emit('fp-sh-RR-link-closed');
    }).finally(() => {
      this.showLinkRRLoader = false;
    })
  }

  refreshData() {
    this.linkedIds = [];
    this.parentSelected = 0;
    this.RRCheckListSelection.clear();
    this.readyRRTreeData();
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

    this.dataSource.data.forEach((node)=>{
      node.children?.forEach((child)=>{
        this.checkAllParents(child);
      })
    })
    this.showActive = isActive;
  }

  filterData() {
    if (this.filterRRString.length > 0) {
      let temparr = [
        ...this.myData.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) => {return c.description.toLowerCase().trim()
              .includes(this.filterRRString.toLowerCase().trim())}),
          };
        }),
      ];
      this.dataSource.data = temparr;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
      });

      this.dataSource.data.forEach((node)=>{
        node.children?.forEach((child)=>{
          this.checkAllParents(child);
        })
      })
      this.treeControl.dataNodes = this.dataSource.data;
      this.filterRRString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
    }
    else {
      this.dataSource.data = this.myData;
    }


  }



  ///// WORK ON THIS FOR BETTER FILTERATION /////////////
  filterRecursive(isActive: boolean, array: any[], property: string) {
    let filteredData;

    //make a copy of the data so we don't mutate the original
    function copy(o: any) {
      return Object.assign({}, o);
    }

    // has string
      // need the string to match the property value
      // copy obj so we don't mutate it and filter
      filteredData = array.map(copy).filter(function x(y) {
        if (y[property] === isActive) {
          return true;
        }
        // if children match
        if (y.children) {
          return (y.children = y.children.map(copy).filter(x)).length;
        }
      });
      // no string, return whole array

    return filteredData;
  }
}

class RRTree {
  id: any;
  description: string;
  children?: RRTree[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: RRTree;
  active?: boolean;
}
