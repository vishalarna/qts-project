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

@Component({
  selector: 'app-fly-panel-add-objectives-linkages',
  templateUrl: './fly-panel-add-objectives-linkages.component.html',
  styleUrls: ['./fly-panel-add-objectives-linkages.component.scss']
})
export class FlyPanelAddObjectivesLinkagesComponent implements OnInit {
  @Output() closed = new EventEmitter<Event>();
  @Output() newObjectivesLinked = new EventEmitter<SimulatorScenario_EnablingObjective_VM[]>();
  @Output() newTasksLinked = new EventEmitter<[SimulatorScenario_Task_VM[], boolean, boolean]>();
  @Input() inputSimScenariosId: string;
  @Input() linkedObjectiveIds: Array<string>;
  @Input() linkedTaskIds: Array<string>;
  enablingObjectiveDataSource = new MatTreeNestedDataSource<EOTree>();
  originalTaskDataSource = new MatTreeNestedDataSource<any>();
  eoTreeControl = new NestedTreeControl<EOTree>((node: any) => node.children);
  originalSource = new MatTreeNestedDataSource<EOTree>();
  taskCheckListSelection = new SelectionModel<any>(true);
  eoCheckListSelection = new SelectionModel<any>(true);
  treeControl = new NestedTreeControl<any>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<any>();
  linkedEOIds: string[] = [];
  hasChild = (_: number, node: EOTree) =>
    !!node.children && node.children.length > 0;

  hasChildTask = (_: number, node: any) =>
    !!node.children && node.children.length > 0;
  addTask: boolean = false;
  showLoader = false;
  showLinkTaskLoader = false;
  includeEos: boolean = false;
  includeProcedures: boolean = false;
  addEO: boolean = false;
  filterTaskString: string;
  filterEOString: string = "";
  notTopicEOs: number = 0;

  constructor(
    public eoSrvc: EnablingObjectivesService,
    private dutyAreaService: DutyAreaService,
    private simSceariosService: SimulatorScenariosService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe
  ) { }

  async ngOnInit(): Promise<void> {
    this.filterTaskString='';
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
          IsEO: false,
        })
        cat['enablingObjective_SubCategories'].forEach((subCat, j) => {
          treeData[i].children?.push({
            children: [],
            description: `${cat['number']}.${subCat['number']} ` + subCat['title'],
            id: subCat.id,
            IsEO: false,
          });
          subCat['enablingObjectives'].forEach((eo) => {
            if (!eo['isMetaEO'] && !eo['isSkillQualification']) {
              treeData[i].children[j].children?.push({
                children: [],
                description: `${cat['number']}.${subCat['number']}.0.${eo['number']} ${eo['description']}`,
                id: eo.id,
                active: eo['active'],
                checkbox: !this.linkedObjectiveIds.includes(eo.id),
                IsEO: true,
              })
              this.notTopicEOs++;
            }
          });
          subCat['enablingObjective_Topics'].forEach((topic, k) => {
            treeData[i]?.children[j]?.children?.push({
              children: [],
              description: `${cat['number']}.${subCat['number']}.${topic['number']} ${topic['title']}`,
              id: topic.id,
              IsEO: false,
            });
            topic['enablingObjectives'].forEach((eo, l) => {
              !eo['isMetaEO'] && !eo['isSkillQualification'] ? treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                children: [],
                description: `${cat['number']}.${subCat['number']}.${topic['number']}.${eo['number']} ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
                checkbox:!this.linkedObjectiveIds.includes(eo.id), 
                IsEO: true,
              }) : '';
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
      this.filterEO();
    }
  }

  setParent(node: EOTree, parent: EOTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  searchEO(event : any){
    this.filterEOString = event?.target?.value;
    this.filterEO();
  }

  clearEOSearchString(){
    this.filterEOString = "";
    this.filterEO();
  }

  filterEO() {
    let temparr = [
      ...this.originalSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {
            return {
              ...e,
              children: e.children?.map((c) => {
                if (c.IsEO) {
                  if (c.description.toLowerCase().match(String(this.filterEOString).toLowerCase()) && c.active === true) {
                    return {
                      ...c,
                      selected: this.eoCheckListSelection.selected.map((x) => { return x.id }).includes(c.id),
                      children: [],
                    }
                  }
                  else {
                    return {
                      description: "",
                      children: [],
                      id: "",
                      IsEO: true,
                    }
                  }
                }
                else {
                  return {
                    ...c,
                    selected: this.eoCheckListSelection.selected.map((x) => { return x.id }).includes(c.id),
                    children: c.children?.filter((f) => {
                      return f.description.toLowerCase().match(String(this.filterEOString).toLowerCase()) && f.active === true;
                    })
                  }
                }

              })
            }
          }
          ),
        };
      }),
    ];

    temparr = [
      ...temparr.map((element) => {
        return {
          ...element,
          children: element.children.map((x) => {
            return {
              ...x,
              children: x.children.filter((x) => {
                return x.description !== "";
              })
            }
          })
        }
      })
    ]
    this.enablingObjectiveDataSource.data = Object.assign(temparr);
    this.eoTreeControl.dataNodes = Object.assign(temparr);
    Object.keys(this.enablingObjectiveDataSource.data).forEach((key: any) => {
      this.setParent(this.enablingObjectiveDataSource.data[key], undefined);
    });

    this.enablingObjectiveDataSource.data.forEach((cat) => {
      cat.children?.forEach((subCat) => {
        subCat.children?.forEach((topic) => {
          if (topic.IsEO) {
            this.eoCheckListSelection.selected.map((x) => x.id).includes(topic.id) ? topic.selected = true : "";
            this.checkAllParents(topic);
          }
          topic.children?.forEach((eo) => {
            this.checkAllParents(eo);
          });
        });
      });
    });

    this.filterEOString.length > 0 ? this.eoTreeControl.expandAll() : this.eoTreeControl.collapseAll();
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
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.taskCheckListSelection.select(node);
    }
    else {
      this.taskCheckListSelection.deselect(node);
    }
  }

  searchTask(event : any){
    this.filterTaskString = event?.target?.value;
    this.filterTask();
  }
  clearTaskSearchString(){
    this.filterTaskString = "" ;
    this.filterTask();
  }
  filterTask() {
    let temparr = [
      ...this.originalTaskDataSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {

            return {
              ...e,
              children: e.children?.filter((c) => {
                return c.active  && (c.description.toLowerCase().trim().includes(this.filterTaskString.trim().toLowerCase()));
              })
            }
          }
          ),
        };
      }),
    ];

    this.dataSource.data = temparr;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((da)=>{
      da.children?.forEach((sda)=>{
        sda.children?.forEach((task)=>{
          this.checkAllParents(task);
        })
      })
    })

    this.treeControl.dataNodes = temparr;

    this.filterTaskString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
  }

  itemToggle(checked: boolean, node: EOTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      if (!node.checkbox) {
        if (checked) {
          node.selected = true;
          this.taskCheckListSelection.select(node);
        }
        else {
          node.selected = false;
          this.taskCheckListSelection.deselect(node);
        }
      }
    }
    this.checkAllParents(node);
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
    await this.dutyAreaService.getWithSubdutyAreas().then((res: DutyArea[]) => {
      res = res.filter(x => x.active === true);
      this.makeTaskTreeDataSource(res);
    }).catch((err: any) => {
      console.error(err);
    })
  }

  makeTaskTreeDataSource(res: DutyArea[]) {
    var treeData: any = [];
    res.forEach((da, i) => {
      treeData.push({
        description: da.letter + da.number + "." + da.title,
        children: [],
        checkbox: da.id,
        id: da.id,
      });
      da.subdutyAreas.forEach((sda, j) => {
        treeData[i].children.push(({
          description: da.letter + ' ' + da.number + '.' + sda.subNumber + ' ' + sda.title,
          checkbox: true,
          children: [],
          id: sda.id,
        }));
        sda.tasks.forEach((task) => {
          if (!task.isMeta) {
            treeData[i].children[j].children.push({
              description: da.letter + da.number + "." + sda.subNumber + "." + task.number + " - " + task.description,
              number: da.letter + da.number + "." + sda.subNumber + "." + task.number,
              checkbox: this.linkedTaskIds.includes(task.id),
              active: task.active,
              id: task.id,
            })
          }
        })
      })
    });
    this.dataSource.data = Object.assign(treeData);
    this.originalTaskDataSource.data = Object.assign(treeData);
    this.treeControl.dataNodes = this.dataSource.data;
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
    this.newObjectivesLinked.emit(this.eoCheckListSelection.selected);
    this.closed.emit();
  }

}
