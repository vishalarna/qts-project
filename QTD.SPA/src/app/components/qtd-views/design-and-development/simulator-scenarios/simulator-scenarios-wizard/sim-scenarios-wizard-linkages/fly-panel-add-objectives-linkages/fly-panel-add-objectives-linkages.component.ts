import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { DutyArea } from '@models/DutyArea/DutyArea';
import { EOTree } from '../../../../../../../_DtoModels/EnablingObjective/EOTree';
import { EnablingObjective } from '@models/EnablingObjective/EnablingObjective';
import { SimulatorScenario_EnablingObjective_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_EnablingObjective_VM';
import { SimulatorScenario_Task_VM } from '@models/SimulatorScenarios_New/SimulatorScenario_Task_VM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SimulatorScenariosService } from 'src/app/_Services/QTD/SimulatorScenarios/simulator-scenarios.service';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { PositionIdsModel } from '@models/Position/PositionIdsModel';

@Component({
  selector: 'app-fly-panel-add-objectives-linkages',
  templateUrl: './fly-panel-add-objectives-linkages.component.html',
  styleUrls: ['./fly-panel-add-objectives-linkages.component.scss']
})
export class FlyPanelAddObjectivesLinkagesComponent implements OnInit {
  @Output() closed = new EventEmitter<Event>();
  @Output() newObjectivesLinked = new EventEmitter<{ selected: SimulatorScenario_EnablingObjective_VM[], includeMetaEO: boolean}>();
  @Output() newTasksLinked = new EventEmitter<[SimulatorScenario_Task_VM[], boolean, boolean]>();
  @Input() inputSimScenariosId: string;
  @Input() linkedObjectiveIds: Array<string>;
  @Input() linkedTaskIds: Array<string>;
  @Input() linkedPositionIds: any[] = [];
  @Input() alreadyLinkedTasks: any[] = [];
  enablingObjectiveDataSource = new MatTreeNestedDataSource<EOTree>();
  originalTaskDataSource = new MatTreeNestedDataSource<any>();
  eoTreeControl = new NestedTreeControl<EOTree>((node: any) => node.children);
  originalSource = new MatTreeNestedDataSource<EOTree>();
  taskCheckListSelection = new SelectionModel<any>(true);
  eoCheckListSelection = new SelectionModel<any>(true);
  treeControl = new NestedTreeControl<any>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<any>();
  hasChild = (_: number, node: EOTree) =>
    !!node.children && node.children.length > 0;

  hasChildTask = (_: number, node: any) =>
    !!node.children && node.children.length > 0;
  addTask: boolean = false;
  showLoader = false;
  showLinkTaskLoader = false;
  includeEos: boolean = false;
  includeProcedures: boolean = false;
  isTaskLoading: boolean;
  addEO: boolean = false;
  filterTaskString: string;
  filterEOString: string = "";
  notTopicEOs: number = 0;
  searchText: string;
  selectedFilter:string;
  showOnlySelected: boolean;
  isIncludeMetaEO:boolean = false;

  constructor(
    public eoSrvc: EnablingObjectivesService,
    private dutyAreaService: DutyAreaService,
    private simSceariosService: SimulatorScenariosService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  async ngOnInit(): Promise<void> {
    this.filterTaskString='';
    this.searchText = '';
    this.showOnlySelected = false;
    this.isTaskLoading = false;
    await this.readyTasksTreeData();
  }

  onChange(event: any) {
    if (this.enablingObjectiveDataSource.data.length === 0 && event.index === 1) {
      this.showLoader = true;
      this.readyEnablingObjectiveTreeData();
    }
  }

  async readyEnablingObjectiveTreeData() {
    await this.eoSrvc.getAll().then((res: EnablingObjective[]) => {
      res = res.filter(x => x.active === true);
      this.readyEOTreeData(res);
      this.showLoader = false;
    });
  }


  readyEOTreeData(res: any[]) {
    this.notTopicEOs = 0;
    if (res.length === 0) {
      this.enablingObjectiveDataSource.data = [];
    } else {
      var treeData: EOTree[] = [];
      res.forEach((cat, i) => {
        treeData.push({
          children: [],
          description: cat['number'] + ". " + cat['title'],
          id: cat.id,
          IsEO:false,
        })
        cat['enablingObjective_SubCategories'].forEach((subCat, j) => {
          treeData[i].children?.push({
            children: [],
            description: `${cat['number']}.${subCat['number']} ` + subCat['title'],
            id: subCat.id,
            IsEO:false,
          });
          subCat['enablingObjectives'].forEach((eo) => {
            treeData[i].children[j].children?.push({
              children: [],
              description: `${eo['number']} ${eo['description']}`,
              id: eo.id,
              active: eo['active'],
              checkbox: !this.linkedObjectiveIds.includes(eo.id),
           //   selected: this.linkedObjectiveIds.includes(eo.id), 
              IsEO: true,
              isMeta: eo['isMetaEO'],
              isSkillQualification: eo['isSkillQualification'],
              
            })
            this.notTopicEOs++;
          });
          subCat['enablingObjective_Topics'].forEach((topic, k) => {
            treeData[i]?.children[j]?.children?.push({
              children: [],
              description: `${cat['number']}.${subCat['number']}.${topic['number']} ${topic['title']}`,
              id: topic.id,
              IsEO:false,
            });
            topic['enablingObjectives'].forEach((eo, l) => {
                treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                children: [],
                description: `${eo['number']} ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
                checkbox: !this.linkedObjectiveIds.includes(eo.id),
            //    selected: this.linkedObjectiveIds.includes(eo.id), 
                IsEO: true,
                isMeta: eo['isMetaEO'],
                isSkillQualification: eo['isSkillQualification'],
              });
              if(this.linkedObjectiveIds.includes(eo.id)){

              }

            });
          });
          this.notTopicEOs = 0;
        });
      })
      
      this.eoTreeControl.dataNodes = Object.assign(treeData);
      this.enablingObjectiveDataSource.data = Object.assign([], treeData);
      this.originalSource.data = Object.assign([], treeData);
      Object.keys(this.enablingObjectiveDataSource.data).forEach((key: any) => {
        this.setParent(this.enablingObjectiveDataSource.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
      });
      this.updateEONodeSelectionState(treeData);
    }
  }

  private setParent(node: EOTree, parent: EOTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  searchEO(event : any){
    const searchTerm = event.target.value.toLowerCase();
    this.searchText = searchTerm;
    if (this.selectedFilter) {
      this.applyEOFilterAndSearch(this.selectedFilter);
    } else {
      this.setEOFilteredDataSource(null);
    }
  }

  clearEOSearchString(){
    this.filterEOString = "";
    this.filterEO(this.filterEOString);
  }

  filterEO(s: string) {
    this.selectedFilter = s;
    this.searchEO({ target: { value: this.searchText } });    
  }

  private checkAllParents(node: EOTree) {
    if (node.parent) {
      const descendants = this.eoTreeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  onEOChange(event: any, node: any) {
    if (event.checked) {
      this.eoCheckListSelection.select(node);
    }
    else {
      this.eoCheckListSelection.deselect(node);
    }
    this.itemToggle2(event.checked, node)
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.taskCheckListSelection.select(node);
    }
    else {
      this.taskCheckListSelection.deselect(node);
    }
    this.toggleTaskItem(event.checked, node);
  }

  toggleTaskItem(checked: boolean, node: any) {
  node.selected = checked;  

  if (node.children && node.children.length > 0) {
    node.children.forEach((child) => {
      this.toggleTaskItem(checked, child);
    });
  } else {
    if (node.checkbox) {
      if (checked) {
        this.taskCheckListSelection.select(node);
      } else {
        this.taskCheckListSelection.deselect(node);
      }
    }
  }

  this.checkAllParentTasks(node);
}


 private checkAllParentTasks(node: any) {
  if (node.parent) {
    const descendants = this.treeControl.getDescendants(node.parent);

    node.parent.selected = descendants.length > 0 && descendants.every(c => c.selected);
    node.parent.indeterminate = !node.parent.selected && descendants.some(c => c.selected);

    this.checkAllParentTasks(node.parent);
  }
}

  searchTask(event : any){
    const searchTerm = event.target.value.toLowerCase();
    this.searchText = searchTerm;
    if (this.selectedFilter) {
      this.applyFilterAndSearch(this.selectedFilter);
    } else {
      this.setFilteredDataSource(null);
    }
  }

  setFilteredDataSource(condition: ((item: any) => boolean) | null) {
    function filterTree(node) {
      if (!node.children || node.children.length === 0) return node;
  
      node.children = node.children
        .map(filterTree)
        .filter(child => child.children?.length > 0 || (child.children?.length === 0 && !child.children));
  
      if (node.children.length === 0 && node.children !== undefined) {
        return null;
      }
      return node;
    }
  
    let temparr = this.originalTaskDataSource.data.map((element) => {
      let filteredChildren = element.children?.map((e) => {
        let filteredGrandchildren = e.children?.filter((c) => {
          const matchesSearch = c.description.toLowerCase().includes(String(this.searchText).toLowerCase());
          const matchesCondition = condition ? condition(c) : true;
          return matchesSearch && matchesCondition;
        }) || [];
        return {
          ...e,
          children: filteredGrandchildren
        };
      }).filter(e => e.children && e.children.length > 0) || [];
  
      return {
        ...element,
        children: filteredChildren
      };
    }).filter(element => element.children && element.children.length > 0);
  
    temparr.forEach(node => {
      this.setTaskParent(node, undefined);
      this.updateNodeSelectionState(node); 
    });
    this.dataSource.data = temparr;
    this.treeControl.dataNodes = this.dataSource.data;
    this.searchText.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }
  

  public hideLeafNode(node: any) {
    return this.showOnlySelected && !node.selected
      ? true
      : new RegExp(this.searchText, 'i').test(node.description) === false || (!(node?.isTask ?? false));
  }

  public hideParentNode(node: any) {
    return this.treeControl
      .getDescendants(node)
      .filter((node) => node.children == null || node.children.length === 0)
      .every((node) => this.hideLeafNode(node));
  }

  clearTaskSearchString(){
    this.filterTaskString = "" ;
    this.searchTask(this.filterTaskString);
  }
  
 

  itemToggle2(checked: boolean, node: EOTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined && node.children?.length > 0) {
      node.children.forEach((child) => {
        this.itemToggle2(checked, child);
      });
    } else {
      if (node.checkbox) {
        if (checked) {
          node.selected = true;
          this.eoCheckListSelection.select(node);
        }
        else {
          node.selected = false;
          this.eoCheckListSelection.deselect(node);
        }
      }
    }
    this.checkAllParents(node);
  }

  async readyTasksTreeData() {
    this.isTaskLoading = true;
    var option = new PositionIdsModel();
        option.positionIds = [...this.linkedPositionIds];
    await this.dutyAreaService.getTaskTreeDataByPositionAsync(option).then((res) => {
        this.makeTaskTreeDataSource(res);
      }).catch((err: any) => {
      console.error(err);
    }).finally(() => {
        this.isTaskLoading = false;
      });
  }

  makeTaskTreeDataSource(res: any[]) {
    var treeData = [];
    res.forEach((da, i) => {
      treeData.push({
        description: `${da.letter} ${da.number} - ${da.title}`,
        id: da.id,
        children: [],
        checkbox: true,
        selected: false,
      });
      da.subdutyAreas.forEach((sda, j) => {
        treeData[i].children?.push({
          description: `${da.letter} ${da.number}.${sda.subNumber} - ${sda.title}`,
          id: sda.id,
          children: [],
          checkbox: true,
          selected: false,
        });
        sda.tasks.forEach((task) => {
          if (task.active) {
            treeData[i]?.children[j]?.children?.push({
              description: `${da.letter} ${da.number}.${sda.subNumber}.${task.number} - ${task.description}`,
              id: task.id,
              checkbox: !this.alreadyLinkedTasks.includes(task.id),
           //   selected: this.alreadyLinkedTasks.includes(task.id),
              isTask: true,
              isMeta: task.isMeta,
              isReliablity: task.isReliability,
            });
          }
        });
      });
    });
    this.dataSource.data = treeData;
    this.originalTaskDataSource.data = this.dataSource.data;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setTaskParent(this.dataSource.data[key], undefined);
      this.setTaskParent(this.originalTaskDataSource.data[key], undefined);
    });
    this.updateNodeSelectionState(treeData);
    this.isTaskLoading = true;
  }

  private setTaskParent(node: any, parent: any | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setTaskParent(childNode, node);
      });
    }
  }

  updateNodeSelectionState(nodes: any[] | any) {
    nodes.forEach(node => {
          if (node.children && node.children.length > 0) {

              this.updateNodeSelectionState(node.children);

              const allChildrenSelectedOrLinked = node.children.every(
                  (child: any) => child.selected || this.alreadyLinkedTasks.includes(child.id)
              );
              const anyChildSelectedOrLinked = node.children.some(
                  (child: any) => child.selected || this.alreadyLinkedTasks.includes(child.id) || child.indeterminate
              );

              node.selected = allChildrenSelectedOrLinked;
              node.indeterminate = !allChildrenSelectedOrLinked && anyChildSelectedOrLinked;
          }
      });
  }


  updateEONodeSelectionState(nodes: any[] | any) {
  const nodeArray = Array.isArray(nodes) ? nodes : [nodes];

  nodeArray.forEach(node => {
    if (node.children && node.children.length > 0) {
      this.updateEONodeSelectionState(node.children);

      const allChildrenSelectedOrLinked = node.children.every(
        (child: any) => child.selected || child.indeterminate
      );
      const anyChildSelectedOrLinked = node.children.some(
        (child: any) => child.selected || child.indeterminate
      );

      node.selected = allChildrenSelectedOrLinked; 
      node.indeterminate = !allChildrenSelectedOrLinked && anyChildSelectedOrLinked;
    }
  });
}

  toggleIncludeEos() {
    this.includeEos = !this.includeEos;
  }

  toggleincludeProcedures() {
    this.includeProcedures = !this.includeProcedures;
  }

  linkTasksToScenario() {
    this.newTasksLinked.emit([this.taskCheckListSelection.selected, this.includeEos, this.includeProcedures]);
    this.closed.emit();
  }

  linkObjectivesToScenario() {
  this.newObjectivesLinked.emit({
    selected: this.eoCheckListSelection.selected,
    includeMetaEO: this.isIncludeMetaEO
  });
  this.closed.emit();
}

  filterTask(s: string) {
    this.selectedFilter = s;
    this.searchTask({ target: { value: this.searchText } });
  }

  applyFilterAndSearch(filter: string) {
    switch (filter) {
      case 'rr':
        this.setFilteredDataSource(item=>item.isReliablity);
        break;
      case 'non-rr':
        this.setFilteredDataSource(item=>!item.isReliablity);
        break;
      case 'meta':
        this.setFilteredDataSource(item=>item.isMeta);
        break;
      default:
        this.setFilteredDataSource(null);
        break;
    }
  }

  applyEOFilterAndSearch(filter: string) {
    switch (filter) {
      case 'eo':
        this.setEOFilteredDataSource(item=>!item.isSkillQualification && !item.isMeta);
        break;
      case 'sq':
        this.setEOFilteredDataSource(item=>item.isSkillQualification);
        break;
      case 'meta':
        this.setEOFilteredDataSource(item=>item.isMeta);
        break;
      default:
        this.setEOFilteredDataSource(null);
        break;
    }
  }

  setEOFilteredDataSource(condition: ((item: any) => boolean) | null) {
  const searchText = String(this.searchText).toLowerCase();

  function filterNode(node): any | null {
    const matchesSearch = node.description?.toLowerCase().includes(searchText) ?? true;
    const matchesCondition = condition ? condition(node) : true;
    const selfMatches = matchesSearch && matchesCondition;

    const filteredChildren = node.children
      ? node.children.map(filterNode).filter(c => c !== null)
      : [];

    if (selfMatches || filteredChildren.length > 0) {
      return {
        ...node,
        children: filteredChildren
      };
    }
    return null;
  }

  const temparr = this.originalSource.data
    .map(filterNode)
    .filter(node => node !== null);

  temparr.forEach(node => {
    this.setParent(node, undefined);
    this.updateEONodeSelectionState(node); 
  });

  this.enablingObjectiveDataSource.data = temparr;
  this.eoTreeControl.dataNodes = this.enablingObjectiveDataSource.data;
  this.searchText.length > 0 ? this.eoTreeControl.expandAll() : this.eoTreeControl.collapseAll();
}

  onIncludeMetaEOChange(event: any){
    this.isIncludeMetaEO = event.checked;
  }
}
