import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { SH_EO_LinkOptions } from 'src/app/_DtoModels/SH_EO_Link/SH_EO_LinkOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-sh-eos-link',
  templateUrl: './fly-panel-sh-eos-link.component.html',
  styleUrls: ['./fly-panel-sh-eos-link.component.scss'],
})
export class FlyPanelShEosLinkComponent implements OnInit, AfterViewInit, OnDestroy {
  filterEOString: string;
  linkEOs: boolean = true;
  addEO: boolean = false;
  showActive: boolean = true;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  linkedIds: any[] = [];
  treeControl = new NestedTreeControl<EOTreeNew>((node: any) => node.children);
  dataSourceWithTopic = new MatTreeNestedDataSource<EOTreeNew>();
  dataSourceWithoutTopic = new MatTreeNestedDataSource<EOTreeNew>();
  hasChild = (_: number, node: EOTreeNew) =>
    !!node.children && node.children.length > 0;

  EOTreeCheckListSelection = new SelectionModel<EOTreeNew>(true);
  originalSourceWithTopic = new MatTreeNestedDataSource<EOTreeNew>();
  originalSourceWithoutTopic = new MatTreeNestedDataSource<EOTreeNew>();
  showLinkEOLoader: boolean = false;
  isEOLoading: boolean = false;
  subscription = new SubSink();
  @Input() alreadyLinked:any[] = [];
  shId = "";
  myDataWithTopic:any[]=[];
  myDataWithoutTopic:any[]=[];
  notTopicEOs = 0;

  constructor(public eoService: EnablingObjectivesService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private shService: SafetyHazardsService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe:LabelReplacementPipe) {}

  ngOnInit(): void {
    this.readyEOsWithTopicTreeData();
    //this.readyEOsWithoutTopicTreeData()
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.shId = res.id;
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  clearFilter(){
    this.filterEOString = '';
    this.readyEOsWithTopicTreeData();
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
      }).finally(()=>{
    this.isEOLoading = false;

      });
  }

  makeEOTreeDataSource(res: any) {
    this.notTopicEOs = 0;
    if (res.length == 0) {
      this.dataSourceWithTopic.data = [];
    } else {
      var treeData: EOTree[] = [];

      res.forEach((cat, i) => {
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
                description: `${eo['number']} ${eo['description']}`,
                id: eo.id,
                active: eo['active'],
                checkbox: !this.alreadyLinked.includes(eo.id),
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
            });
            topic['enablingObjectives'].forEach((eo, l) => {
              l++;
              treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                children: [],
                description: `${cat['number']}.${subCat['number']}.${topic['number']}.${l++} ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
                checkbox: !this.alreadyLinked.includes(eo.id),
                IsEO: !eo['isMetaEO'] && !eo['isSkillQualification'],
                IsMeta: eo['isMetaEO'],
                IsSq: eo['isSkillQualification']
              });
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

      this.isEOLoading = false;
      // this.filterDataNew(this.showActive);
    }
  }

  filterDataNew(filterActive: boolean) {
    this.showActive = filterActive;
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
                      selected: this.linkedIds.includes(c.id),
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
                    selected: this.linkedIds.includes(c.id),
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
    this.dataSourceWithTopic.data = Object.assign(temparr);
    this.treeControl.dataNodes = Object.assign(temparr);
    Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
      this.setParent(this.dataSourceWithTopic.data[key], undefined);
    });

    this.dataSourceWithTopic.data.forEach((cat) => {
      cat.children?.forEach((subCat) => {
        subCat.children?.forEach((topic) => {

          if (topic.IsEO) {
            this.linkedIds.includes(topic.id) ? topic.selected = true:"";
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

 /*  async readyEOsWithoutTopicTreeData() {
    this.isEOLoading = true;
    await this.eoService
      .getAll()
      .then((res: EnablingObjective[]) => {

        this.makeEOTreeWithoutTopicDataSource(res);
      })
      .catch((err: any) => {
        console.error(err);
      });
  }

  makeEOTreeDataSource(res: any) {
    this.notTopicEOs = 0;
    if (res.length == 0) {
      this.dataSourceWithTopic.data = [];
    } else {
      var treeData: EOTreeNew[] = [];

      res.forEach((cat, i) => {
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
                description: `${eo['number']} ${eo['description']}`,
                id: eo.id,
                active: eo['active'],
                checkbox: !this.alreadyLinked.includes(eo.id),
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
            });
            topic['enablingObjectives'].forEach((eo, l) => {
              !eo['isMetaEO'] && !eo['isSkillQualification'] ? treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                children: [],
                description: `${eo['number']} ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
                checkbox: !this.alreadyLinked.includes(eo.id),
                IsEO: true,
              }) : '';
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

      this.filterDataNew(this.showActive);
    }
    this.isEOLoading = false;
  }

  makeEOTreeWithoutTopicDataSource(res: any) {
    var treeDatawithoutTopic: any = [{}];
    for (var data in res) {
      treeDatawithoutTopic[data] = {
        id: res[data]['id'],
        description: res[data]['description'],
        children: res[data]['enablingObjective_SubCategories'],
        checkbox: true,
      };

      for (var data1 in treeDatawithoutTopic[data]['children']) {


        treeDatawithoutTopic[data]['children'][data1] = {
          id: res[data]['enablingObjective_SubCategories'][data1]['id'],
          description: res[data]['enablingObjective_SubCategories'][data1]['description'],
          children: res[data]['enablingObjective_SubCategories'][data1]['enablingObjectives'],
          checkbox: true,
        };
        for(var data2 in treeDatawithoutTopic[data]['children'][data1]['children']){

          let isTrue = this.alreadyLinked.includes(treeDatawithoutTopic[data]['children'][data1]['children'][data2]) ? false:true;

          treeDatawithoutTopic[data]['children'][data1]['children'][data2]['checkbox'] = this.alreadyLinked.includes(treeDatawithoutTopic[data]['children'][data1]['children'][data2]['id']) ? false:true;

        }
      }
    }

    this.dataSourceWithoutTopic.data = treeDatawithoutTopic;
    this.originalSourceWithoutTopic.data = treeDatawithoutTopic;
    this.myDataWithoutTopic = Object.assign(treeDatawithoutTopic);
    Object.keys(this.dataSourceWithoutTopic.data).forEach((key: any) => {
      this.setParent(this.dataSourceWithoutTopic.data[key], undefined);
      this.setParent(this.originalSourceWithoutTopic.data[key], undefined);
      this.setParent(this.myDataWithoutTopic[key], undefined);
    });
  } */



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

    this.dataSourceWithTopic.data.forEach((node:EOTreeNew)=>{
      node.children?.forEach((child)=>{
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

    this.dataSourceWithoutTopic.data.forEach((node:EOTreeNew)=>{
      node.children?.forEach((child)=>{
        this.checkAllParents(child);
      })
    })

    this.treeControl.dataNodes = this.dataSourceWithTopic.data || this.dataSourceWithoutTopic.data;
    this.filterEOString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
  }

  filterActive(makeActive: boolean) {
    let temparr = [
      ...this.originalSourceWithTopic.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {

            return {
              ...e,
              children: e.children?.map((c) => {
                return {
                  ...c,
                  children: c.children?.filter((f) => {
                    return f.active == makeActive;
                  })
                }

              })
            }
          }
          ),
        };
      }),
    ];
    this.showActive = makeActive;
    this.dataSourceWithTopic.data = temparr;
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

  toggleFilter(isActive: boolean) {
    var temp:any[] = Object.assign([],this.myDataWithTopic);
    this.dataSourceWithTopic.data = [
      ...temp.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {

            return {
              ...e,
              children: e.children?.map((c) => {
                return {
                  ...c,
                  children: c.children?.filter((f) => f.active == isActive),
                }

              })
            }
          }
          ),
        };
      }),
    ];

    Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
      this.setParent(this.dataSourceWithTopic.data[key], undefined);
    });

    this.dataSourceWithTopic.data.forEach((node:EOTreeNew)=>{
      node.children?.forEach((child)=>{
        this.checkAllParents(child);
      })
    })

    var temp:any[] = Object.assign([],this.myDataWithoutTopic);
    this.dataSourceWithoutTopic.data = [
      ...temp.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {
            return {
              ...e,
              children: e.children?.filter((f) => f.active == isActive),
            }
          }
          ),
        };
      }),
    ];

    Object.keys(this.dataSourceWithoutTopic.data).forEach((key: any) => {
      this.setParent(this.dataSourceWithoutTopic.data[key], undefined);
    });

    this.dataSourceWithoutTopic.data.forEach((node:EOTreeNew)=>{
      node.children?.forEach((child)=>{
        this.checkAllParents(child);
      })
    })

    this.showActive = isActive;
  }

  itemToggle(checked: boolean, node: EOTreeNew) {
    node.selected = checked;
    if (node.children.length > 0) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      if (node.selected && node.checkbox && (node.IsEO || node.IsMeta || node.IsSq)) {

        this.linkedIds.push(node.id);
      } else if((node.IsEO || node.IsMeta || node.IsSq) && !node.selected && node.checkbox) {
        var index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      }
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

 /*  filterDataNew(filterActive: boolean) {
    this.showActive = filterActive;
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
                      selected: this.linkedIds.includes(c.id),
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
                    selected: this.linkedIds.includes(c.id),
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
    this.dataSourceWithTopic.data = Object.assign(temparr);
    this.treeControl.dataNodes = Object.assign(temparr);
    Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
      this.setParent(this.dataSourceWithTopic.data[key], undefined);
    });

    this.dataSourceWithTopic.data.forEach((cat) => {
      cat.children?.forEach((subCat) => {
        subCat.children?.forEach((topic) => {

          if (topic.IsEO) {
            this.linkedIds.includes(topic.id) ? topic.selected = true:"";
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
 */
  private setParent(node: EOTreeNew, parent: EOTreeNew | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: EOTreeNew) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  refreshData() {
    this.EOTreeCheckListSelection.clear();
    this.linkedIds = [];
    this.readyEOsWithTopicTreeData();
   // this.readyEOsWithoutTopicTreeData();
  }

  linkEOToSafetyHazard() {
    var options = new SH_EO_LinkOptions();
    this.showLinkEOLoader=true;
    options.safetyHazardId = this.shId;
    options.eOIds = [];
    options.eOIds = this.linkedIds;
    this.shService.LinkEOs(this.shId, options).then(async (res: any) => {
      this.refresh.emit();
      this.refreshLinkedEOs();
      //this.refreshLinkedTasks();
      this.alert.successToast(`EO(s) Linked To ${await this.labelPipe.transform("Safety Hazard")}`);
      this.closed.emit('fp-sh-ila-link-closed');
    }).finally(()=>{
    this.showLinkEOLoader=false;
    });
  }

  refreshLinkedEOs() {
    this.EOTreeCheckListSelection.clear();
    this.linkedIds.forEach((id: any) => {
      this.alreadyLinked.push(id);
    });
    this.linkedIds = [];
  }
}

/* class EOTree {
  id: any;
  description: string;
  children!: EOTree[];
  active?: boolean;
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: EOTree;
  IsEO!:boolean;
}
 */

class EOTree {
  id: any;
  description: string;
  children!: EOTree[];
  active?: boolean;
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: EOTree;
  IsEO?: boolean = false;
  IsMeta?: boolean = false;
  IsSq? : boolean = false;
}

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
  IsMeta?: boolean = false;
  IsSq? : boolean = false;
}
