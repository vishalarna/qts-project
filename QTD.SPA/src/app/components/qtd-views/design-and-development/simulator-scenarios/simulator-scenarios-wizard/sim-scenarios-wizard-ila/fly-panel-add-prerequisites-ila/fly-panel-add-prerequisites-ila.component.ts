import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ILATree } from '@models/ILA/ILATree';
import { SimulatorScenario_UpdatePrerequisites_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_UpdatePrerequisites_VM';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-add-prerequisites-ila',
  templateUrl: './fly-panel-add-prerequisites-ila.component.html',
  styleUrls: ['./fly-panel-add-prerequisites-ila.component.scss']
})
export class FlyPanelAddPrerequisitesIlaComponent implements OnInit {
  @Output() closed = new EventEmitter<Event>();
  @Output() newPreRequisitesLinked = new EventEmitter<any>();
  @Input() linkedPreRequisitesIds: Array<string>; 
  @Input() inputSimScenariosId: string; 
  treeControl = new NestedTreeControl<ILATree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ILATree>();
  originalSource = new MatTreeNestedDataSource<ILATree>();
  prerquisitiesCheckListSelection = new SelectionModel<any>(true);
  preReqToLink = new SimulatorScenario_UpdatePrerequisites_VM();
  hasChild = (_: number, node: ILATree) =>
    !!node.children && node.children.length > 0;
  isILALoading: boolean = false;
  linkIlas: boolean = true;
  showLinkPreReqLoader: boolean = false;
  seacrhILAString: string = "";
  linkedIds: any[] = [];
  parentSelected: number = 0;

  constructor(
    public flyPanelService: FlyInPanelService,
    private shService: SafetyHazardsService,
  ) { }

  ngOnInit(): void {
    this.readyIlasTreeDataAsync();
  }

  searchILA(event : any){
    this.seacrhILAString = event.target?.value ?? "";
    this.filterILA();
  }

  filterILA() {
    let temparr = [
      ...this.originalSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) => {
            return c.description.toLowerCase().match(String(this.seacrhILAString).toLowerCase()) && c.active ;
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
      node.children?.forEach((ila) => {
        this.checkAllParents(ila);
      });
    });
    this.treeControl.dataNodes = this.dataSource.data;
    this?.seacrhILAString?.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }


  clearFilter() {
    this.seacrhILAString = '';
    this.filterILA();
  }

  async readyIlasTreeDataAsync() {
    this.isILALoading = true;
    this.dataSource.data = [];
    await this.shService
      .getProviderWithILAs()
      .then((res) => {
  
        this.makeIlaTreeDataSource(res);
      }).finally(() => {
        this.isILALoading = false;
      });
  }


  makeIlaTreeDataSource(res: any) {
    this.prerquisitiesCheckListSelection.clear();
    this.linkedIds = [];
    if (res.length == 0) {
      this.dataSource.data = [];
      this.isILALoading = false;
    }
    else {
      this.isILALoading = true;
      var treeData: any = [{}];

      for (var data in res) {

        treeData[data] = {
          id: res[data]['id'],
          description: res[data]['name'],
          children: [],
          checkbox: true,
        };

        var sortedIlas = res[data].ilAs.sort((a, b) => 
          a.number.localeCompare(b.number, undefined, { numeric: true, sensitivity: 'base' })
        );

        sortedIlas.forEach(element => {
          treeData[data]['children'].push({
            id: element.id,
            description: element.number + " - " + element.name,
            checkbox: !this.linkedPreRequisitesIds.includes(element.id),
            active: element.active,
          })
        });
      }

      this.dataSource.data = treeData;
      this.originalSource.data = treeData;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
      });
      this.filterILA();
      this.isILALoading = false;
    }

  }

  itemToggle(checked: boolean, node: ILATree) {
    node.selected = node.checkbox ? checked : false;
    if (checked) {
      this.prerquisitiesCheckListSelection.select(node);
    } else {
      this.prerquisitiesCheckListSelection.deselect(node);
    }
    if (node.children !== undefined) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
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

  onIlaChange(event: any, node: any) {
    if (event.checked) {
      this.prerquisitiesCheckListSelection.select(node);
    } else {
      this.prerquisitiesCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  setParent(node: ILATree, parent: ILATree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  checkAllParents(node: ILATree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => (child.checkbox && child.selected));
      node.parent.indeterminate = descendants.some((child) => (child.checkbox && child.selected));
      this.checkAllParents(node.parent);
    }
  }

  async linkPreReqsToScenarioAsync() {
    this.showLinkPreReqLoader = true;
    const preReqSelected = this.prerquisitiesCheckListSelection.selected.filter(obj => obj.parent !== undefined);
    this.newPreRequisitesLinked.emit(preReqSelected);
    this.closed.emit();
  }

}

