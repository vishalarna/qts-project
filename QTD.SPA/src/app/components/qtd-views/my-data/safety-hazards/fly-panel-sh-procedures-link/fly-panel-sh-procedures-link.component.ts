import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { SaftyHazard_ProcedureLinkOptions } from 'src/app/_DtoModels/SaftyHazard_ProcedureLink/SaftyHazard_ProcedureLinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { ProcedureIssuingAuthorityService } from 'src/app/_Services/QTD/procedure-issuing-authority.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-sh-procedures-link',
  templateUrl: './fly-panel-sh-procedures-link.component.html',
  styleUrls: ['./fly-panel-sh-procedures-link.component.scss'],
})
export class FlyPanelShProceduresLinkComponent implements OnInit, AfterViewInit, OnDestroy {
  filterProcString: string;
  linkProcs: boolean = true;
  addProc: boolean = false;
  showActive: boolean = true;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinked: any[] = [];

  linkedIds: any[] = [];
  isLoading:boolean=false;
  treeControl = new NestedTreeControl<ProcTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ProcTree>();
  myData:any[]=[];
  hasChild = (_: number, node: ProcTree) =>
    !!node.children && node.children.length > 0;

  EOTreeCheckListSelection = new SelectionModel<ProcTree>(true);
  showLinkEOLoader: boolean = false;

  subscription = new SubSink();
  shId = "";

  constructor(
    private route: ActivatedRoute,
    private procIAService: IssuingAuthoritiesService,
    private shService: SafetyHazardsService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
      this.readyProcsTreeData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  clearFilter(){
    this.filterProcString = '';
    this.readyProcsTreeData();
  }

  async readyProcsTreeData() {
    this.isLoading=true;
    await this.procIAService.getAll().then((res: Procedure_IssuingAuthority[]) => {
      this.makeProcTreeDataSource(res);
    }).finally(()=>{
      this.isLoading=false;

    });
  }

  makeProcTreeDataSource(res: Procedure_IssuingAuthority[]) {
    var treeData: any[] = [];
    for (let index = 0; index < res.length; index++) {
      let procTree = new ProcTree();
      if(res[index].active){

        procTree.description = res[index].title;
        procTree.checkbox = true;
        procTree.children = [];

        res[index].procedures.forEach((proc) => {
          if(proc.active)
          procTree.children?.push({
            id: proc.id,
            description:proc.number + ' - ' + proc.title,
            checkbox: !this.alreadyLinked.includes(proc.id),
            active:proc.active,
          });
        });

        treeData.push(procTree);

      }


    }

    this.dataSource.data = treeData;
    this.myData = Object.assign(treeData);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.myData[key], undefined);
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

    this.dataSource.data.forEach((node:ProcTree)=>{
      node.children?.forEach((child)=>{
        this.checkAllParents(child);
      })
    })
    this.showActive = isActive;
  }

  filterData() {
    if (this.filterProcString.length > 0) {
      let temparr = [
        ...this.myData.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) => {return c.description.toLowerCase().trim()
              .includes(this.filterProcString.toLowerCase().trim())}),
          };
        }),
      ];
      this.dataSource.data = temparr;

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
      this.filterProcString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
    }
    else {
      this.dataSource.data = this.myData;
    }

  /*   Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((node:ProcTree)=>{
      node.children?.forEach((child)=>{
        this.checkAllParents(child);
      })
    }) */
  }

  onProcChange(event: any, node: any) {
    if (event.checked) {
      this.EOTreeCheckListSelection.select(node);
    } else {
      this.EOTreeCheckListSelection.deselect(node);
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
        const index = this.linkedIds.indexOf(node.id);
        if(index > -1){
          this.linkedIds.splice(index, 1);
        }
      };
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  linkProcedure() {
    this.showLinkEOLoader = true;
    var options = new SaftyHazard_ProcedureLinkOptions();
    options.procedureIds = this.linkedIds;
    this.shService.linkProcedures(this.shId, options).then(async (res: any) => {
      this.alert.successToast("Selected " + await this.transformTitle('Procedure') + "s Successfully Linked To "+ await this.transformTitle('Safety Hazard') + "s");
      this.refresh.emit();
      this.closed.emit('fp-sh-proc-link-closed');
    }).finally(() => {
      this.showLinkEOLoader = false;
    })
  }

  refreshData() {
    this.EOTreeCheckListSelection.clear();
    this.linkedIds = [];
    this.readyProcsTreeData();
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
  active?:boolean;
}
