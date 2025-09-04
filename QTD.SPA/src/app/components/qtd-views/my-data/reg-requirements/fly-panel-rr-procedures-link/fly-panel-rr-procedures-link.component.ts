import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, OnInit, Output,Input } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { RR_Procedure_LinkOptions } from 'src/app/_DtoModels/RegulatoryRequirements/RR_Procedure_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-rr-procedures-link',
  templateUrl: './fly-panel-rr-procedures-link.component.html',
  styleUrls: ['./fly-panel-rr-procedures-link.component.scss'],
})
export class FlyPanelRRProceduresLinkComponent implements OnInit {
  showLoader=true;
  filterProcString: string;
  linkProcs: boolean = true;
  addProc: boolean = false;
  showActive: boolean = true;
  @Output() closed = new EventEmitter<any>();
  @Input() alreadyLinked:any[] = [];
  linkedIds: any[] = [];
  treeControl = new NestedTreeControl<ProcTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ProcTree>();
  originalDataSource = new MatTreeNestedDataSource<ProcTree>();
  hasChild = (_: number, node: ProcTree) =>
    !!node.children && node.children.length > 0;
  rrId: any;
  TreeCheckListSelection = new SelectionModel<ProcTree>(true);
  showLinkLoader: boolean = false;
  subsink = new SubSink();
  treeData: ProcTree[] = [];
  showOnlySelected: boolean;
  filterProcedureString:string;
  constructor(
    private procIAService: IssuingAuthoritiesService,
    private rrSrvc: RegulatoryRequirementService,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) {}

  ngOnInit(): void {
    this.subsink.sink = this.route.params.subscribe((res) => {
      this.rrId = res.id;
      this.readyProcsTreeData();
    });
  }

  ngOnDestroy(): void {
    this.subsink.unsubscribe();
  }

  clearFilter(){
    this.filterProcedureString = '';
    this.readyProcsTreeData();
  }


  async readyProcsTreeData() {
    this.showLinkLoader = true;
    this.showLoader=true;
    await this.procIAService
      .getAll()
      .then((res) => {

        this.makeTreeDataSource(res);
      })
      .finally(() => {
        this.showLinkLoader = false;
        this.showLoader=false;
      });
  }

  makeTreeDataSource(data: Procedure_IssuingAuthority[]) {
    this.treeData = [];

    let i = 1;
    for (let index = 0; index < data.length; index++) {
      let procTree = new ProcTree();
      if(data[index].active){

        procTree.description = i + ' - ' + data[index].title;
        procTree.checkbox = true;
        procTree.children = [];
        i++;

        data[index].procedures.forEach((proc) => {
          if(proc.active)
          procTree.children?.push({
            id: proc.id,
            description:proc.number + ' - ' + proc.title,
            checkbox: !this.alreadyLinked.includes(proc.id),
            active: proc.active
          });
        });
        this.treeData.push(procTree);
      }
    }
    this.dataSource.data = this.treeData;
    this.originalDataSource = Object.assign(this.treeData);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalDataSource[key], undefined);
    });
    this.filterActive(true);
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
    
    if (toFilter.data === null) {
      this.dataSource.data = this.treeData;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
      });
    } else {
      this.dataSource.data = [
        ...this.dataSource.data.map((d) => {
          return {
            ...d,
            children: d.children?.filter((x) =>
              x.description
                .toLowerCase()
                .includes(String(toFilter.data).toLowerCase())
            ),
          };
        }),
      ];
      this.treeControl.dataNodes = this.dataSource.data;
      this.treeControl.expandAll();
    }
    // this.dataSource.data.forEach((x: any) => {
    //   x.children = x.children.forEach((element: any) => {
    //     return element.children.includes(this.filterProcString)
    //       ? element.children
    //       : { children: {} };
    //   });
    // });


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
      if (checked && node.checkbox) {
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
 /*    node.selected = checked;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add EOIds to list
      if (checked) this.linkedIds.push(node.id);
      else this.linkedIds.splice(this.linkedIds.indexOf(node.id), 1);
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node); */
  }

  filterActive(makeActive: boolean){
    let temparr = [
      ...this.originalDataSource.data.map((element) => {

         return {
          ...element,
          children: element.children?.filter((c) =>
            c.active === makeActive
          ),
        };
      }),
    ];
    this.showActive = makeActive;
    this.dataSource.data = temparr;
  }



  linkProcsToRR() {

    let option: RR_Procedure_LinkOptions = {
      procedureIds: this.linkedIds,
      regulatoryRequirementId: this.rrId,
    };
    this.rrSrvc.linkProcedures(this.rrId, option).then(async (res) => {
      this.linkedIds = [];
      this.alert.successToast(await this.labelPipe.transform('Procedure') + 's linked with ' + await this.labelPipe.transform('Regulatory Requirement') + 's');
      this.closed.emit('proc linked with RR');
    });
  }

  public hideLeafNode(node: any) {
    return this.showOnlySelected && !node.selected
      ? true
      : new RegExp(this.filterProcedureString, 'i').test(node.description) === false;
  }

  public hideParentNode(node: any){
    return this.treeControl
        .getDescendants(node)
        .filter(node => node.children == null || node.children.length === 0)
        .every(node => this.hideLeafNode(node));
  }
}

class ProcTree {
  id: any;
  description: string;
  active?:boolean;
  children?: ProcTree[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: ProcTree;
}
