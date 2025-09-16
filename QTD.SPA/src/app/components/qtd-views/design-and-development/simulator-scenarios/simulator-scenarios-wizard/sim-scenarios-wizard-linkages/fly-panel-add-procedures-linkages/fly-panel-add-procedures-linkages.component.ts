import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ProcedureTree } from '../../../../../../../_DtoModels/Procedure/ProcedureTree';
import { SimulatorScenario_UpdateProcedures_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdateProcedures_VM';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-add-procedures-linkages',
  templateUrl: './fly-panel-add-procedures-linkages.component.html',
  styleUrls: ['./fly-panel-add-procedures-linkages.component.scss']
})
export class FlyPanelAddProceduresLinkagesComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() newProceduresLinked = new EventEmitter<any>();
  @Input() linkedProceduresIds: any[] = [];
  treeControl = new NestedTreeControl<ProcedureTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ProcedureTree>();
  treeCheckListSelection = new SelectionModel<ProcedureTree>(true);
  proceduresToLink: SimulatorScenario_UpdateProcedures_VM = new SimulatorScenario_UpdateProcedures_VM;
  hasChild = (_: number, node: ProcedureTree) => !!node.children && node.children.length > 0;
  linkedNames: ProcedureTree[] = [];
  treeData: ProcedureTree[] = [];
  linkProcs: boolean = true;
  showActive: boolean = true;
  spinner: boolean = true;
  showLinkLoader: boolean = true;
  filterProcString: string;
  myData: any[] = [];
  linkedIds: any[] = [];
  toFilter: any;

  constructor(
    public issuingAuthoritiesService: IssuingAuthoritiesService,
    public flyPanelSrvc: FlyInPanelService
  ) { }

  ngOnInit(): void {
    this.filterProcString='';
    this.makeProcTreeDataSource();
  }

  searchFilter(event : any) {
    this.filterProcString = event?.target?.value ?? "";
    this.filterProcedure();
  }
  clearSearchString(){
    this.filterProcString = "";
    this.filterProcedure();
  }
  filterProcedure(){ 
    const search = this.filterProcString.trim().toLowerCase();
    this.toFilter = [...this.myData.map((n) => {
        return {
          ...n,
          children: n.children?.filter((c) =>
            c.description.trim().toLowerCase().includes(search) ||
            c.number?.toString().trim().toLowerCase().includes(search)
         ),
        };
      }),
    ];
    this.dataSource.data = this.toFilter.filter((x) => { return x.children !== null && x.children !== undefined && x.children.length > 0 });
    this.treeControl.dataNodes = this.dataSource.data;
    this.filterProcString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();

  }

  async makeProcTreeDataSource() {

    await this.issuingAuthoritiesService.getAll().then((res) => {
      res.forEach(res => {
        res.procedures = res.procedures.filter(procedure => procedure.active === true);
    });
      var treeData: any[] = [];
      for (let index = 0; index < res.length; index++) {
        let procTree = new ProcedureTree();
        procTree.description = res[index].title;
        procTree.checkbox = true;
        procTree.children = [];

        res[index].procedures.forEach((proc) => {
          procTree.children?.push({
            id: proc.id,
            description: proc.title,
            checkbox:!this.linkedProceduresIds.includes(proc.id),
            active: proc.active,
            number: proc.number,
            selected:this.linkedProceduresIds.includes(proc.id),
          });
        });

        treeData.push(procTree);
      }

      this.dataSource.data = treeData;
      this.myData = Object.assign(treeData);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.myData[key], undefined);
      });
      this.treeControl.dataNodes = Object.assign(this.myData);
      this.dataSource.data.forEach((data)=>{
        data.children.forEach((elm) => {
          if(this.linkedProceduresIds.includes(elm.id)){
            this.treeControl.expand(data);
          }
          this.checkAllParents(elm);
        });
      })

    })
  }


  private setParent(node: ProcedureTree, parent: ProcedureTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: ProcedureTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent).filter(x=>x.checkbox);
      node.parent.selected = descendants.every((child) =>child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  itemToggle(checked: boolean, node: ProcedureTree) {
    if (checked) {
      this.treeCheckListSelection.select(node);
    } else {
      this.treeCheckListSelection.deselect(node);
    }

    node.selected = checked;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add EOIds to list
      if(node.checkbox){
        if (checked) {
          this.linkedIds.push(node.id);
          this.linkedNames.push(node)
        }
        else {
          const index = this.linkedIds.indexOf(node.id);
          if (index > -1) {
            this.linkedIds.splice(index, 1);
            this.linkedNames.splice(index, 1);
          }
        };
      }
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.linkedNames = [...new Set(this.linkedNames)];
    this.checkAllParents(node);
  }

  async linkProcToScenariosAsync() {
    this.showLinkLoader = true;
    const procedureSelected = this.treeCheckListSelection.selected.filter(obj => obj.parent !== undefined);
    this.newProceduresLinked.emit(procedureSelected);
    this.closed.emit();
  }

}

