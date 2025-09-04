import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { EO_LinkOptions } from 'src/app/_DtoModels/EnablingObjective/EO_LinkOptions';
import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { RR_IssuingAuthorityCompact } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityCompact';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-flypanel-eo-rr-link',
  templateUrl: './flypanel-eo-rr-link.component.html',
  styleUrls: ['./flypanel-eo-rr-link.component.scss']
})
export class FlypanelEoRrLinkComponent implements OnInit,OnDestroy,AfterViewInit {
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

  eoId = '';
  subscription = new SubSink();

  constructor(
    private route: ActivatedRoute,
    private rrIAService: RRIssuingAuthorityService,
    private alert: SweetAlertService,
    private eoService : EnablingObjectivesService,
    private dataBroadcastService : DataBroadcastService,
  ) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.eoId = String(res.id).split('-')[1];
      this.getRRsData();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  clearFilter(){
    this.filterRRString = '';
    this.getRRsData();
  }

  dataLoader = false;

  async getRRsData() {
    this.linkedIds = [];
    this.parentSelected = 0;
    let treeData: RRTree[] = [];
    this.dataLoader = true;
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
                description: `${rr.number} ` + ' - ' + rr.title,
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
        this.dataLoader = false;
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
  refreshRRData() {

    this.linkedIds = [];
    this.RRCheckListSelection = new SelectionModel<RRTree>(true);
    this.getRRsData();
  }

  async linkRR(){
    this.showLinkRRLoader = true;
    var options = new EO_LinkOptions();
    options.eoId = this.eoId;
    options.rrIds = this.linkedIds;
    await this.eoService.linkRR(options).then((_)=>{
      this.alert.successToast("RRs Linked to EO");
      this.refreshRRLinks.emit();
      this.dataBroadcastService.refreshStats.next(null);
    }).finally(()=>{
      this.showLinkRRLoader = false;
    })
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
