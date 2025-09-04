import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { select, Store } from '@ngrx/store';
import { CustomEOWithNumVM } from 'src/app/_DtoModels/CustomEnablingObjective/CustomEOWithNumVM';
import { CustomEnablingObjective } from 'src/app/_DtoModels/CustomEnablingObjective/CustomEnablingObjective';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-link-scenario-objective',
  templateUrl: './fly-panel-link-scenario-objective.component.html',
  styleUrls: ['./fly-panel-link-scenario-objective.component.scss'],
})
export class FlyPanelLinkScenarioObjectiveComponent
  implements OnInit, OnDestroy {
  @Input() isCustomEO: boolean = true;
  @Input() flyPanelCheck: boolean = false;
  @Input() selectedIds: any;
  @Input() linkedTaskIds: any[] = [];
  @Input() linkedEOIds: any[] = [];
  @Input() linkedCOIds:any[] = [];
  @Input() otherSelectedObjectives:any;
  @Input() segmentTitle:string;
  @Output() sendId: EventEmitter<any> = new EventEmitter();
  check: boolean = true;
  spinner: boolean = true;
  dataSource = new MatTreeNestedDataSource<any>();
  objectiveDataSource: MatTableDataSource<any> | undefined;
  treeControl = new NestedTreeControl<any>((node) => node.children);
  enablingObjectiveDataSource = new MatTreeNestedDataSource<EOTreeNew>();
  originalSource = new MatTreeNestedDataSource<EOTreeNew>();
  EOTreeControl = new NestedTreeControl<EOTreeNew>((node: any) => node.children);
  customEOSource = new MatTableDataSource<CustomEOWithNumVM>();
  customEOLoader = false;
  customEODisplayColumns = ["select","number","description"];
  displayedLearningObjectiveColumns: string[] = ['index', 'type', 'number', 'description'];
  hasChild = (_: number, node: Tasks) =>
    !!node.children && node.children.length > 0;

  ilaId: any;
  taskIds: any;
  searchString = "";
  learningObjectiveIds: any[] = [];
  tempObjectiveDatsource: any [] | undefined = [];

  subscriptions: SubSink = new SubSink();

  taskCheckListSelection = new SelectionModel<any>(true,[]);
  EOCheckListSelection = new SelectionModel<any>(true,[]);
  COCheckListSelection = new SelectionModel<any>(true,[]);
  selection = new SelectionModel<any>(true,[]);
  @Output() closed = new EventEmitter<any>();
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    public ilaService: IlaService,
    private vcr: ViewContainerRef,
    public dutyAreaService: DutyAreaService,
    private alert: SweetAlertService,
    private getServices: IlaService,
    private saveStore: Store<{ saveIla: any }>,
    private labelPipe: LabelReplacementPipe
  ) { }

  @ViewChild(MatSort, { static: false }) sort: MatSort;
  
 async ngOnInit(): Promise<void> {
    //this.dataSource.data = TREE_DATA;
    this.subscriptions.sink = this.saveStore
      .pipe(select('saveIla'))
      .subscribe((res) => {
        this.ilaId = res['saveData']?.result?.id;
        this.readyLinkedTasks();
      });

    this.flyPanelSrvc.setViewContainerRef(this.vcr);
    await this.getObjectivesLinkedToILA();
    
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  changedTab(event:any) {
    
    if (event.index === 1 && this.enablingObjectiveDataSource.data.length < 1) {
      this.readyLinkedEO();
    }
    else if(event.index === 2 && this.customEOSource.data.length < 1){
      this.readyCustomEOs()
    }
  }



  filterChange(event: any) {
    
    
  }

  OnClose(event: any) {
    var id: any[] = [{ task: {}, eo: {} }];
    if (this.flyPanelCheck !== true) {
      this.flyPanelSrvc.close();
    }
    this.subscriptions.unsubscribe();
    this.closed.emit(event);
  }

  async readyLinkedTasks() { 
    await this.ilaService
      .getLinkedTaskObjectivesWithDutyAreas(this.ilaId)
      .then((res: any) => {
        this.makeTaskTreeDataSource(res);
      })
      .catch((err: any) => {
        console.error(err);
      });
  }

  async readyLinkedEO() {
    await this.ilaService
      .getLinkedEOWithEOCategories(this.ilaId)
      .then((res: any) => {
        
        this.readyEOTreeData(res);
      })
      .catch((err: any) => {
        console.error('error', err);
      });
  }

  // Make a specialized and simplified data source for the mat tree structure
  makeTaskTreeDataSource(res: any) {
    var treeData: any = [];
    res.forEach((da, i) => {
      treeData.push({
        description: da.title,
        children: [],
        id: da.id,
      });
      da.subdutyAreas.forEach((sda, j) => {
        treeData[i].children.push(({
          description: sda.title,
          children: [],
          id: sda.id,
        }));
        sda.tasks.forEach((task) => {
          if (!task.isMeta) {
            var dto={
              description: da.letter + da.number + "." + sda.subNumber + "." + task.number + " - " + task.description,
              number: da.letter + da.number + "." + sda.subNumber + "." + task.number,
              checkbox: this.linkedTaskIds.includes(task.id),
              active: task.active,
              id: task.id,
            };
            treeData[i].children[j].children.push(dto);
            if (this.linkedTaskIds.includes(task.id)) {
              this.taskCheckListSelection.select(dto);
            }


          }
        })
      })
    });

    
    // for (var data in res) {
    //   
    //   treeData[data] = { description: res[data]['title'], children: res[data]['subdutyAreas'], checkbox: true };
    //   
    //   for (var data1 in treeData[data]['children']) {
    //     treeData[data]['children'][data1] = { description: res[data]['subdutyAreas'][data1]['title'], children: res[data]['subdutyAreas'][data1]['tasks'], checkbox: true };
    //   }
    // }

    this.dataSource.data = Object.assign(treeData);
    this.treeControl.dataNodes = this.dataSource.data;
  }

  notTopicEOs = 0;
  readyEOTreeData(eo: any) {
    
    this.notTopicEOs = 0;
    if (eo.length === 0) {
      this.enablingObjectiveDataSource.data = [];
    } else {
      var treeData: EOTreeNew[] = [];
      eo.forEach((cat, i) => {
        treeData.push({
          children: [],
          description: cat['number'] + ". " + cat['title'],
          id: cat.id,
        })
        cat['enablingObjective_SubCategories'].forEach((subCat, j) => {
          treeData[i].children?.push({
            children: [],
            description: `${cat['number']}.${subCat['number']} ` + subCat['title'],
            id: subCat.id,
          });
          subCat['enablingObjectives'].forEach((eo) => {
            if (!eo['isMetaEO'] && !eo['isSkillQualification']) {
              treeData[i].children[j].children?.push({
                children: [],
                description: `${cat['number']}.${subCat['number']}.${eo.number} - ${eo['description']}`,
                id: eo.id,
                active: eo['active'],
                checkbox: !this.linkedEOIds.includes(eo.id),
                IsEO: true,
                number:`${cat['number']}.${subCat['number']}.${eo.number}`,
              })
              this.notTopicEOs++;
              if (this.linkedEOIds.includes(eo.id)) {
              this.EOCheckListSelection.select({
                children: [],
                description: `${cat['number']}.${subCat['number']}.${eo.number} - ${eo['description']}`,
                id: eo.id,
                active: eo['active'],
                checkbox: !this.linkedEOIds.includes(eo.id),
                IsEO: true,
                number:`${cat['number']}.${subCat['number']}.${eo.number}`,
              });

              }
            }
          });
          subCat['enablingObjective_Topics'].forEach((topic, k) => {
            treeData[i]?.children[j]?.children?.push({
              children: [],
              description: `${cat['number']}.${subCat['number']}.${topic['number']} ${topic['title']}`,
              id: topic.id,
            });
            topic['enablingObjectives'].forEach((eo, l) => {
              !eo['isMetaEO'] && !eo['isSkillQualification'] ? treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                children: [],
                description: `${cat['number']}.${subCat['number']}.${topic['number']}.${eo.number} - ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
                checkbox: !this.linkedEOIds.includes(eo.id),
                IsEO: true,
                number: `${cat['number']}.${subCat['number']}.${topic['number']}.${eo.number}`,
              }) : '';
            });
          });
          this.notTopicEOs = 0;
        });
      })

      this.treeControl.dataNodes = Object.assign([], treeData);
      this.enablingObjectiveDataSource.data = Object.assign([], treeData);

      this.originalSource.data = Object.assign([], treeData);
      Object.keys(this.enablingObjectiveDataSource.data).forEach((key: any) => {
        this.setParent(this.enablingObjectiveDataSource.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
      });

      
      
      this.filterDataNew(true);
      
    }
  }

  async readyCustomEOs(){
    this.customEOLoader = true;
    var data = await this.ilaService.getLinkedCustomObjectivesWithNumber(this.ilaId);
    this.customEOSource.data = data;
    
    this.customEOLoader = false;
  }

  private checkAllParents(node: EOTreeNew) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  filterEOString = "";
  filterDataNew(filterActive: boolean) {
    //this.showActive = filterActive;
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
                      selected: this.EOCheckListSelection.selected.map((x) => { return x.id }).includes(c.id),
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
                    selected: this.EOCheckListSelection.selected.map((x) => { return x.id }).includes(c.id),
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
    this.treeControl.dataNodes = Object.assign(temparr);
    Object.keys(this.enablingObjectiveDataSource.data).forEach((key: any) => {
      this.setParent(this.enablingObjectiveDataSource.data[key], undefined);
    });

    this.enablingObjectiveDataSource.data.forEach((cat) => {
      cat.children?.forEach((subCat) => {
        subCat.children?.forEach((topic) => {
          if (topic.IsEO) {
            this.EOCheckListSelection.selected.map((x) => x.id ).includes(topic.id) ? topic.selected = true : "";
            this.checkAllParents(topic);
          }
          topic.children?.forEach((eo) => {
            this.checkAllParents(eo);
          });
        });
      });
    });
    this.filterEOString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  private setParent(node: EOTreeNew, parent: EOTreeNew | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  filterData(data: any, toFilter: any) {
    
    data.filter((o: any) => {
      if (o.children) o.children = this.filterData(o.children, toFilter);
      return o.description.includes(toFilter);
    });
  }

  onTaskChange(event: any, node: any) {
    if (event.checked) {
      this.taskCheckListSelection.select(node);
    } else {
      this.taskCheckListSelection.deselect(node);
    }
  }

  onEOChange(event: any, node: any) {
    
    if (event.checked) {
      this.EOCheckListSelection.select(node);
    } else {
      this.EOCheckListSelection.deselect(node);
    }
  }

  addToSegment() {
    this.sendId.emit(this.selection.selected);
    this.alert.successToast('Segment Objectives Added');
    this.subscriptions.unsubscribe();
    this.closed.emit();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async addToSegmentTask() {
    
    this.taskCheckListSelection.selected.forEach((element: any) => {
      element.type = 'Task';
    });

    this.EOCheckListSelection.selected.forEach((element: any) => {
      element.type = "EO";
    })

    this.COCheckListSelection.selected.forEach((element:any)=>{
      element.type = "Custom"
    })

    var selectedTasks: any = this.taskCheckListSelection.selected.concat(
      this.EOCheckListSelection.selected
    );
    this.sendId.emit(selectedTasks);
    this.alert.successToast(await this.transformTitle('Task') +'s Linked');
  }

  checkAllCO(){
    this.customEOSource.data.forEach((data)=>{
      if(!this.linkedCOIds.includes(data.id)){
        this.COCheckListSelection.select(data);
      }
    })
  }

  checkChangeCO(event:any,id:any){
    var data = this.customEOSource.data.find(x => {
      return x.id === id;
    })
    if(data !== undefined && data !== null){
      if(event.checked){
        this.COCheckListSelection.select(data);
      }
      else{
        this.COCheckListSelection.deselect(data);
      }
    }
  }

  filterCO:string = "";
  filterCOData(){
    this.customEOSource.filter = this.filterCO.trim().toLowerCase();
  }

masterToggle() {
  this.isAllSelected() ?
    this.selection.clear() : this.objectiveDataSource.data.filter(x => x.linked == false).forEach((row) => {
      this.learningObjectiveIds.includes(row.id) ? '' : this.selection.select(row)
    });
}

isAllSelected() {
  const numSelected = this.selection.selected.length;
  const filteredData= this.objectiveDataSource.data.filter(x => x.linked == false);
  const numRows = filteredData.length;
  return numSelected === numRows;
}

sortData(sort: Sort) {
  this.objectiveDataSource.sort = this.sort;
}

async getObjectivesLinkedToILA() {
  this.spinner = true;
  this.getServices.getAllObjectives(this.ilaId).then((res) => {
    let tempSrc: any[] = [];
    for (const [index, data] of res.entries()) {
        var isLinked =false;
        var isLinkedToOther =false;
        switch(data.type.toLowerCase()){
          case 'eo' :
           isLinked = this.linkedEOIds.some(id => id === data.id);
           isLinkedToOther = this.otherSelectedObjectives?.find(x => x.id === data.id && x.type.trim().toLowerCase() == "eo");
           break;
          case 'custom' :
           isLinked = this.linkedCOIds.some(id => id === data.id);
           isLinkedToOther = this.otherSelectedObjectives?.find(x => x.id === data.id && x.type.trim().toLowerCase() == "custom");
           break;
          case 'task' :
           isLinked = this.linkedTaskIds.some(id => id === data.id);
           isLinkedToOther = this.otherSelectedObjectives?.find(x => x.id === data.id && x.type.trim().toLowerCase() == "task");
           break;
        }
      tempSrc.push({
        index: index,
        id:data.id,
        type : data.type,
        number: data.number,
        description: data.description,
        linked: isLinked,
        linkedToOther : isLinkedToOther
      });
    }
    this.tempObjectiveDatsource = tempSrc;
    this.objectiveDataSource = new MatTableDataSource(this.tempObjectiveDatsource);
    this.objectiveDataSource.sort = this.sort;
    this.spinner = false;
  })
}

searchObjectives(event: any) {
  this.searchString = event.target.value;
  this.filterObjectives();
}

filterObjectives() {
  const searchTerm = this.searchString.trim().toLowerCase();
  this.objectiveDataSource.data = this.tempObjectiveDatsource.filter(item => {
    return (
      (item?.number?.trim().toLowerCase().includes(searchTerm) ||
        item?.type?.trim().toLowerCase().includes(searchTerm) ||
        item?.description?.trim().toLowerCase().includes(searchTerm) 
        )
    );
  });
}

}

class Tasks {
  description: string;
  children?: Tasks[];
  checkbox?: boolean;
  type: string = '';
}

// const TREE_DATA: Tasks[] = [
//   {
//     name: '$1. Transmission Operations',
//     children: [
//       {
//         name: '$1.1 Voltage Control',
//         children: [],
//       },
//       {
//         name: '$1.2 Facility Control',
//         children: [],
//       },
//       {
//         name: '$1.3 System Monitoring and Cont...',
//         children: [
//           {
//             name: '$1.3.1 LSO/SO Continuously monitor all pertinent conditions... ',
//             checkbox: true,
//           },
//           {
//             name: '$1.3.2 Respond to actual or potential rating violations... ',
//             checkbox: true,
//           },
//         ],
//       },
//     ],
//   },
// ];

class EOTreeNew {
  id: any;
  description: string;
  children!: EOTreeNew[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: EOTreeNew;
  active?: boolean;
  isLink?: boolean;
  IsEO?: boolean = false;
  number?:any;
}
