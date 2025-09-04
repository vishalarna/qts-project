import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { EO_LinkOptions } from 'src/app/_DtoModels/EnablingObjective/EO_LinkOptions';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-flypanel-eo-procedure-link',
  templateUrl: './flypanel-eo-procedure-link.component.html',
  styleUrls: ['./flypanel-eo-procedure-link.component.scss']
})
export class FlypanelEoProcedureLinkComponent implements OnInit,OnDestroy,AfterViewInit {
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

  eoId = "";
  subscription = new SubSink();

  constructor(
    private procIAService: IssuingAuthoritiesService,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    private eoService : EnablingObjectivesService,
    private dataBroadcastService : DataBroadcastService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.readyProcsTreeData();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.eoId = String(res.id).split('-')[1];
    })
  }

  ngOnDestroy(): void {

  }

  async readyProcsTreeData() {
    this.showLinkLoader = true;
    this.TreeCheckListSelection.clear();
    this.linkedIds = [];
    await this.procIAService
      .getAll()
      .then((res) => {
        
        this.makeTreeDataSource(res);
      })
      .finally(() => {
        this.showLinkLoader = false;
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

      data[index].procedures?.forEach((proc) => {
        if(proc.active){
          procTree[index].children?.push({
          id: proc.id,
          description: `${proc.number} - ` + proc.title,
          checkbox: !this.alreadyLinked.includes(proc.id),
          active: proc.active,
        });
        }
        
      });
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

  private setParent(node: ProcTree, parent: ProcTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: ProcTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
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

  itemToggle(checked: boolean, node: ProcTree) {
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

  async linkProcedures(){
    this.showLinkLoader = true;
    var options = new EO_LinkOptions();
    options.eoId = this.eoId;
    options.procedureIds = this.linkedIds;
    await this.eoService.linkProcedure(options).then(async (_)=>{
      this.alert.successToast(await this.transformTitle('Procedure') + "s linked to EOs");
      this.refresh.emit();
      this.dataBroadcastService.refreshStats.next(null);
    }).finally(()=>{
      this.showLinkLoader = false;
    })
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
