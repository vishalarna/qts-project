import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { Procedure_RegulatoryRequirement_LinkOptions } from 'src/app/_DtoModels/Procedure_RegulatoryRequirement_Link/Procedure_RegulatoryRequirement_LinkOptions';
import { RegulatoryRequirement } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirement';
import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { RRIssuingAuthority } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthority';
import { RR_IssuingAuthorityCompact } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityCompact';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-procedure-rr-link',
  templateUrl: './fly-panel-procedure-rr-link.component.html',
  styleUrls: ['./fly-panel-procedure-rr-link.component.scss'],
})
export class FlyPanelProcedureRrLinkComponent
  implements OnInit, OnDestroy, AfterViewInit
{
  showLoader=true
  filterRRString: string;
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

  procId = '';
  subscription = new SubSink();

  constructor(
    private route: ActivatedRoute,
    private rrIAService: RRIssuingAuthorityService,
    private alert: SweetAlertService,
    private procService: ProceduresService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.procId = res.id;
      this.getRRsData();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  clearFilter(){
    this.filterRRString = '';
    this.dataSource.data = this.originalData.data;
  }

  async getRRsData() {
  
      this.showLoader=true;
  
   
    this.linkedIds = [];
    this.parentSelected = 0;
    let treeData: RRTree[] = [];

    await this.rrIAService.GetRRWithIA().then((res: RR_IssuingAuthorityCompact[]) => {

      res.forEach((item: RR_IssuingAuthorityCompact, index: any) => {
        if(item.active){
          treeData.push({ id: item.id, checkbox: true, description: (index+1) + ' - ' + item.title, children: [] });
        item.regulatoryRequirementCompacts.forEach((rr: RegulatoryRequirementsCompact) => {
          treeData[index].children?.push({ id: rr.id, description: rr.number + ' - ' + rr.title, active: rr.active, checkbox: !this.alreadyLinkedIds.includes(rr.id) });
        })
        }

      })

      this.dataSource.data = treeData;
      Object.assign(this.originalData.data,this.dataSource.data);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalData.data[key], undefined);
      });
      this.filterActive(true);
    });
    this.showLoader=false;
  }

  // Make a specialized and simplified data source for the mat tree structure
  makeRRTreeDataSource(res: any) {
    var treeData: any = [{}];

    for (var data in res) {
      treeData[data] = {
        id: res[data]['id'],
        description: res[data]['description'],
        children: res[data]['subdutyAreas'],
        checkbox: true,
        selected: false,
      };

      for (var data1 in treeData[data]['children']) {
        treeData[data]['children'][data1] = {
          id: res[data]['subdutyAreas'][data1]['id'],
          description: res[data]['subdutyAreas'][data1]['description'],
          children: res[data]['subdutyAreas'][data1]['tasks'],
          checkbox: true,
          selected:false,
        };
      }
    }
    this.dataSource.data = treeData;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
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
    
    if (this.filterRRString.length > 0) {
      let temparr = [
        ...this.originalData.data.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) =>
              c.description
                .toLowerCase()
                .match(String(this.filterRRString).toLowerCase())
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
      this.filterRRString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
    } else {
      this.dataSource.data = this.originalData.data;
    }
  }

  filterActive(makeActive: boolean) {
    let temparr = [
      ...this.originalData.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => c.active === makeActive),
        };
      }),
    ];
    this.showActive = makeActive;
    this.dataSource.data = temparr;
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

  linkToProcedure() {
    this.showLinkRRLoader = true;
    var options = new Procedure_RegulatoryRequirement_LinkOptions();
    options.procedureId = this.procId;
    options.regulatoryRequirementIds = this.linkedIds;
    this.procService.linkRR(this.procId, options).then(async (res: any) => {
      this.alert.successToast("Successfully Linked " + await this.labelPipe.transform('Regulatory Requirement') + "s to " + await this.transformTitle('Procedure') + "s");
      this.showLinkRRLoader = false;
      this.dataBroadcastService.updateProcRRLink.next(null);
      this.refreshRRLinks.emit();
    }).finally(() => {
      this.closed.emit('fp-proc-RR-link-closed');
      this.showLinkRRLoader = false;
    });
  }

  refreshRRData() {

    this.linkedIds = [];
    this.RRCheckListSelection = new SelectionModel<RRTree>(true);
    this.getRRsData();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
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
