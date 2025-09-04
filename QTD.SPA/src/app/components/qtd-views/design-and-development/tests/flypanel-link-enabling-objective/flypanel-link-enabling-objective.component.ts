import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjective_Topic } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-flypanel-link-enabling-objective',
  templateUrl: './flypanel-link-enabling-objective.component.html',
  styleUrls: ['./flypanel-link-enabling-objective.component.scss']
})
export class FlypanelLinkEnablingObjectiveComponent implements OnInit {

  filterEOString: string = "";
  linkEOs: boolean = true;
  addEO: boolean = false;
  showActive: boolean = true;
  isEOLoading: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() linkedEOs = new EventEmitter<any>();

  linkedIds: any[] = [];
  treeControl = new NestedTreeControl<EOTree>((node: any) => node.children);
  dataSourceWithTopic = new MatTreeNestedDataSource<EOTree>();
  dataSourceWithoutTopic = new MatTreeNestedDataSource<EOTree>();
  originalSourceWithTopic = new MatTreeNestedDataSource<EOTree>();
  originalSourceWithoutTopic = new MatTreeNestedDataSource<EOTree>();

  myDataWithTopic: any[] = [];
  myDataWithoutTopic: any[] = [];
  eOCheckListSelection = new SelectionModel<EOTree>(true);
  positionId = "";
  length = 0
  testId: any = 0;
  eoList: any[] = [];
  @Input() alreadyLinked: any[] = [];
  hasChild = (_: number, node: EOTree) =>
    !!node.children && node.children.length > 0;

  EOTreeCheckListSelection = new SelectionModel<EOTree>(true);
  showLinkEOLoader: boolean = false;
  @Input() id: any;
  subscription = new SubSink();
  notTopicEOs: number;


  constructor(public eoService: EnablingObjectivesService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private testService: TestsService,) { }

  ngOnInit(): void {
    this.linkedIds = [...this.alreadyLinked];
    this.readyEOsWithTopicTreeData();
    //this.readyEOsWithoutTopicTreeData();
  }

  ngAfterViewInit(): void {
    
    this.subscription.sink = this.route.params.subscribe(async (res: any) => {
      if (res.id !== undefined) {
        this.testId = res.id;
        this.getEOsLinkedToILA(this.testId);

      }


    });
  }

  ngOnDestroy(): void {

  }

  filterDataSource() {
    this.eoList.forEach((res: any) => {
      var index = this.dataSourceWithTopic.data.findIndex(x => x.id == res.id);
      if (index === -1) {
        this.dataSourceWithTopic.data.splice(index, 1);
        //this.dataSourceWithTopic._updateChangeSubscription();
      }

    });
  }

  async readyEOsWithTopicTreeData() {
    this.isEOLoading = true;
    await this.eoService
      .getAll()
      .then((res: EnablingObjective[]) => {
        this.makeEOTreeDataSource(res);
      })
      .catch((err: any) => {
        console.error(err);
      });


  }

  async getEOsLinkedToILA(id: any) {
    await this.testService
      .getEOsLinkedToILA(id)
      .then((res: any[]) => {
        this.eoList = res;
        //this.makeEOTreeDataSource(res);
      })
      .catch((err: any) => {

      })
      .finally(() => {

      })
  }

  // async readyEOsWithoutTopicTreeData() {
  //   this.isEOLoading = true;
  //   await this.eoService
  //     .getAll()
  //     .then((res: EnablingObjective[]) => {
  //       this.makeEOTreeWithoutTopicDataSource(res);
  //     })
  //     .catch((err: any) => {
  //       console.error(err);
  //     });
  // }.trainingProgramList.trainingProgramList

  makeEOTreeDataSource(res: any) {
    this.notTopicEOs = 0;
    if (res.length == 0) {
      this.dataSourceWithTopic.data = [];
    } else {
      var treeData: EOTree[] = [];
      
      res.forEach((cat: EnablingObjective_Category, i) => {
        treeData.push({
          children: [],
          description: cat['number'] + ". " + cat['title'],
          id: cat.id,
        })
        cat['enablingObjective_SubCategories'].forEach((subCat: EnablingObjective_SubCategory, j) => {
          treeData[i].children?.push({
            children: [],
            description: `${cat['number']}.${subCat['number']} ` + subCat['title'],
            id: subCat.id,
          });
          subCat['enablingObjectives'].forEach((eo: EnablingObjective) => {
            if (!eo['isMetaEO'] && !eo['isSkillQualification'] && eo.active) {
              treeData[i].children[j].children?.push({
                children: [],
                description: `${eo['number'].includes('.') ? eo.number : cat.number + '.' + subCat.number + '.0.' + eo['number']} ${eo.description}`,
                id: eo.id,
                active: eo['active'],
                checkbox: true,
                selected: this.alreadyLinked.includes(eo.id),
                IsEO: true,
              })
              this.notTopicEOs++;
            }
          });
          subCat['enablingObjective_Topics'].forEach((topic: EnablingObjective_Topic, k) => {
            treeData[i]?.children[j]?.children?.push({
              children: [],
              description: `${cat['number']}.${subCat['number']}.${topic['number']} ${topic['title']}`,
              id: topic.id,
            });
            topic['enablingObjectives'].forEach((eo: EnablingObjective, l) => {
              if (!eo['isMetaEO'] && !eo['isSkillQualification'] && eo.active) {
                treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                  children: [],
                  description: `${eo['number'].includes('.') ? eo['number'] : `${cat['number']}.${subCat['number']}.${topic['number']}.${eo['number']}`} ${eo.description}`,
                  active: eo['active'],
                  id: eo['id'],
                  checkbox: true,
                  selected: this.alreadyLinked.includes(eo.id),
                  IsEO: true,
                })
              };
            });
          });
          this.notTopicEOs = 0;
        });
      })

      this.treeControl.dataNodes = Object.assign([], treeData);
      this.dataSourceWithTopic.data = Object.assign([], treeData);
      this.originalSourceWithTopic.data = Object.assign([], treeData);
      Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
        this.setParent(this.dataSourceWithTopic.data[key], undefined);
        this.setParent(this.originalSourceWithTopic.data[key], undefined);
      });
      this.dataSourceWithTopic.data.forEach((cat)=>{
        cat.children.forEach((subCat)=>{
          subCat.children.forEach((canBeEo)=>{
            if(canBeEo.IsEO){
              this.checkAllParents(canBeEo);
            }
            else{
              canBeEo.children.forEach((eo)=>{
                if(eo.IsEO){
                  this.checkAllParents(eo);
                }
              })
            }
          })
        })
      })
      
      this.isEOLoading = false;
      // this.filterDataNew(this.showActive);
    }
  }



  /* makeEOTreeDataSource(res: any) {
    
    if (res.length === 0) {
      
      this.dataSourceWithTopic.data = [];
      this.isEOLoading = false;
      
    }
    else {
      this.isEOLoading = true;
      var treeDatawithTopic: EOTree[] = [];
      
      res.forEach((cat, i) => {
        treeDatawithTopic.push({
          children: [],
          description: cat['number'] + " - " + cat['title'],
          id: cat.id,
          IsEO: false,
        })
        cat['enablingObjective_SubCategories'].forEach((subCat, j) => {
          treeDatawithTopic[i].children?.push({
            children: [],
            description: `${cat['number']}.${subCat['number']} - ` + subCat['title'],
            id: subCat.id,
            IsEO: false,
          });
          subCat['enablingObjectives'].forEach((eo, k) => {
            (!eo.isMetaEO || eo.isSkillQualification) ? treeDatawithTopic[i].children[j].children?.push({
              description: `${eo['number']} - ${eo['description']}`,
              id: eo.id,
              active: eo['active'],
              checkbox: true,
              selected:false,
              IsEO: true,
              children:[]
            }) : '';
          });
          subCat['enablingObjective_Topics'].forEach((topic, k) => {
            treeDatawithTopic[i]?.children[j]?.children?.push({
              children: [],
              description: `${cat['number']}.${subCat['number']}.${topic['number']} - ${topic['title']}`,
              id: topic.id,
              IsEO: false,
            });
            this.length = treeDatawithTopic[i].children[j].children.length;
            topic['enablingObjectives'].forEach((eo, l) => {
              (!eo.isMetaEO || !eo.isSkillQualification) ? treeDatawithTopic[i].children[j].children[k + this.length - 1].children.push({
                description: `${eo['number']} - ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
                checkbox: true,
                selected:false,
                IsEO: true,
                children:[],
              }) : '';
            });
          });
        });
      })
      
      this.dataSourceWithTopic.data = treeDatawithTopic;

      this.originalSourceWithTopic.data = treeDatawithTopic;

      this.myDataWithTopic = Object.assign(treeDatawithTopic);

      Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
        this.setParent(this.dataSourceWithTopic.data[key], undefined);
        this.setParent(this.originalSourceWithTopic.data[key], undefined);
        this.setParent(this.myDataWithTopic[key], undefined);
      });
      this.toggleFilter(true);
      this.isEOLoading = false;
    }
  }

  makeEOTreeWithoutTopicDataSource(res: any) {
    var treeDatawithoutTopic: any = [];
    res.forEach((cat, i) => {
      treeDatawithoutTopic.push({
        children: [],
        description: cat['number'] + " - " + cat['title'],
        id: cat.id,
        IsEO: false,
      })
      cat['enablingObjective_SubCategories'].forEach((subCat, j) => {
        treeDatawithoutTopic[i].children?.push({
          children: [],
          description: `${cat['number']}.${subCat['number']} - ` + subCat['title'],
          id: subCat.id,
          IsEo: false,
        });
        subCat['enablingObjectives'].forEach((sEo: EnablingObjective, e) => {
          
          (!sEo.isMetaEO) ? treeDatawithoutTopic[i].children[j].children?.push({
            description: `${sEo['number']} - ${sEo['description']}`,
            id: sEo.id,
            selected: false,
            active: sEo['active'],
            checkbox: true,
            IsEO: true,
          }) : '';
        });
      });
    })

    this.dataSourceWithoutTopic.data = Object.assign(treeDatawithoutTopic);
    this.originalSourceWithoutTopic.data = Object.assign(treeDatawithoutTopic);
    this.myDataWithoutTopic = Object.assign(treeDatawithoutTopic);
    Object.keys(this.dataSourceWithoutTopic.data).forEach((key: any) => {
      this.setParent(this.dataSourceWithoutTopic.data[key], undefined);
      this.setParent(this.originalSourceWithoutTopic.data[key], undefined);
      this.setParent(this.myDataWithoutTopic[key], undefined);
    });

    this.isEOLoading = false;
  } */

  toggleFilter(isActive: boolean) {
    this.showActive = isActive;
    let temparr = [
      ...this.originalSourceWithTopic.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {
            return {
              ...e,
              children: e.children?.map((c) => {
                if (c.IsEO) {
                  if (c.description.toLowerCase().match(String(this.filterEOString).toLowerCase()) && c.active === this.showActive) {
                    return {
                      ...c,
                      children: [],
                    }
                  }
                  else {
                    return {
                      id: "",
                      description: "",
                      checkbox: false,
                      children: [],
                    }
                  }
                }
                else {
                  return {
                    ...c,
                    children: c.children?.filter((f) => {
                      return f.description.toLowerCase().match(String(this.filterEOString).toLowerCase()) && f.active === this.showActive;
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
    
    this.dataSourceWithTopic.data = temparr;
    this.treeControl.dataNodes = temparr;
    this.filterEOString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();

    Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
      this.setParent(this.dataSourceWithTopic.data[key], undefined);
    });

    this.dataSourceWithTopic.data.forEach((node: EOTree) => {
      node.children?.forEach((child) => {
        this.checkAllParents(child);
      })
    })

    this.showActive = isActive;
  }

  filterData() {
    if (this.filterEOString.length > 0) {
      let temparr = [
        ...this.myDataWithTopic.map((element) => {
          return {
            ...element,
            children: element.children?.map((e) => {

              return {
                ...e,
                children: e.children?.map((c) => {
                  return {
                    ...c,
                    children: c.children?.filter((f) => {
                      return f.description.toLowerCase().match(String(this.filterEOString).toLowerCase());
                    })
                  }

                })
              }
            }
            ),
          };
        }),
      ];
      this.dataSourceWithTopic.data = temparr;
    }
    else {
      this.dataSourceWithTopic.data = this.myDataWithTopic;
    }

    Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
      this.setParent(this.dataSourceWithTopic.data[key], undefined);
    });

    this.dataSourceWithTopic.data.forEach((node: EOTree) => {
      node.children?.forEach((child) => {
        this.checkAllParents(child);
      })
    })

    if (this.filterEOString.length > 0) {
      let temparr = [
        ...this.myDataWithoutTopic.map((element) => {
          return {
            ...element,
            children: element.children?.map((e) => {
              return {
                ...e,
                children: e.children?.filter((f) => {
                  return f.description.toLowerCase().match(String(this.filterEOString).toLowerCase());
                })
              }
            }
            ),
          };
        }),
      ];
      this.dataSourceWithoutTopic.data = temparr;
    }
    else {
      this.dataSourceWithoutTopic.data = this.myDataWithoutTopic;
    }

    Object.keys(this.dataSourceWithoutTopic.data).forEach((key: any) => {
      this.setParent(this.dataSourceWithoutTopic.data[key], undefined);
    });

    this.dataSourceWithoutTopic.data.forEach((node: EOTree) => {
      node.children?.forEach((child) => {
        this.checkAllParents(child);
      })
    })
  }

  onEOChange(event: any, node: any) {
    if (event.checked) {
      this.EOTreeCheckListSelection.select(node);
    } else {
      this.EOTreeCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }
  filtered(node: any) {
    return node.description.includes(this.filterEOString);
  }

  itemToggle(checked: boolean, node: EOTree) {
    node.selected = checked;
    if (node.children.length > 0) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      if (node.selected && node.checkbox && node.IsEO) {
        
        this.linkedIds.push(node.id);
      } else if (node.IsEO && !node.selected && node.checkbox) {
        var index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      }
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    
    this.checkAllParents(node);
  }

  private setParent(node: EOTree, parent: EOTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: EOTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  refreshData() {
    this.readyEOsWithTopicTreeData();
    // this.readyEOsWithoutTopicTreeData();
    this.linkedIds = [];
    this.eOCheckListSelection.clear();
  }


  refreshLinkedEOs() {
    this.EOTreeCheckListSelection.clear();
    this.linkedIds.forEach((id: any) => {
      this.alreadyLinked.push(id);
    });
    this.linkedIds = [];
  }

  ViewLinkedQuestions() {
    this.linkedEOs.emit(this.linkedIds);
    this.closed.emit();

  }
}

class EOTree {
  id!: any;
  description!: string;
  children?: EOTree[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: EOTree;
  active?: boolean;
  IsEO?: boolean;
}
