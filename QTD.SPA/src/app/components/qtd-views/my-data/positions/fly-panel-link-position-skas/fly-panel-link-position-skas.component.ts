import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { PositionOptions } from 'src/app/_DtoModels/Position/PositionOptions';
import { EOCatTreeVM } from 'src/app/_DtoModels/TreeVMs/EOTreeVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-link-position-skas',
  templateUrl: './fly-panel-link-position-skas.component.html',
  styleUrls: ['./fly-panel-link-position-skas.component.scss']
})
export class FlyPanelLinkPositionSkasComponent implements OnInit, AfterViewInit, OnDestroy {
  filterEOString: string;
  linkEOs: boolean = true;
  addEO: boolean = false;
  showActive: boolean = true;
  isEOLoading: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  linkedIds: any[] = [];
  treeControl = new NestedTreeControl<EOTree>((node: any) => node.children);
  dataSourceWithTopic = new MatTreeNestedDataSource<EOTree>();
  dataSourceWithoutTopic = new MatTreeNestedDataSource<EOTree>();
  originalSourceWithTopic = new MatTreeNestedDataSource<EOTree>();
  originalSourceWithoutTopic = new MatTreeNestedDataSource<EOTree>();

  sqPosition:boolean=false;
  myDataWithTopic:any[]=[];
  myDataWithoutTopic:any[]=[];
  eOCheckListSelection = new SelectionModel<EOTree>(true);
  positionId = "";

  @Input() alreadyLinked: any[] = [];
  hasChild = (_: number, node: EOTree) =>
    !!node.children && node.children.length > 0;

  EOTreeCheckListSelection = new SelectionModel<EOTree>(true);
  showLinkEOLoader: boolean = false;
  @Input() id: any;
  subscription = new SubSink();
  notTopicEOs: number;
  showOnlySelected = false;


  constructor(public eoService: EnablingObjectivesService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private positionService:PositionsService,
    private labelPipe: LabelReplacementPipe,
    ) {}

    ngOnInit(): void {
      this.readyEOsWithTopicTreeData();
      //this.readyEOsWithoutTopicTreeData();
    }

    ngAfterViewInit(): void {
      this.subscription.sink = this.route.params.subscribe((res:any)=>{
        this.positionId = String(res.id).split('-')[0];
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
        .getMinimizedForTree()
        .then((res: EOCatTreeVM[]) => {

          this.makeEOTreeDataSource(res);
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
        var treeData: EOTree[] = [];

        res.forEach((cat, i) => {
          treeData.push({
            children: [],
            description: cat['number'] + ". " + cat['title'],
            id: cat.id,
            checkbox:true
          })
          cat['enablingObjective_SubCategories'].forEach((subCat, j) => {
            treeData[i].children?.push({
              children: [],
              description: `${cat['number']}.${subCat['number']} ` + subCat['title'],
              id: subCat.id,
              checkbox:true
            });
            subCat['enablingObjectives'].forEach((eo) => {
              if (eo['isSkillQualification']) {
                treeData[i].children[j].children?.push({
                  children: [],
                  description: `${eo['number']} ${eo['description']}`,
                  id: eo.id,
                  active: eo['active'],
                  checkbox: !this.alreadyLinked.includes(eo.id),
                  isEO: true,
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
                eo['isSkillQualification'] && eo['active'] ? treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                  children: [],
                  description: `${cat['number']}.${subCat['number']}.${topic['number']}.${l++} ${eo['description']}`,
                  active: eo['active'],
                  id: eo['id'],
                  checkbox: !this.alreadyLinked.includes(eo.id),
                  isEO: true,
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

        // this.filterDataNew(this.showActive);
      }
    }



    /*  makeEOTreeDataSource(res: any) {

      if(res.length == 0)
      {
        this.dataSourceWithTopic.data = [];
        this.isEOLoading = false;
      }
      else {
        this.isEOLoading = true;
        var treeDatawithTopic: any = [{}];

        for (var data in res) {

          treeDatawithTopic[data] = {
            id: res[data]['id'],
            description: res[data]['number'] + ' - ' + res[data]['description'],
            children: res[data]['enablingObjective_SubCategories'],
            checkbox: true,
          };
          for (var data1 in treeDatawithTopic[data]['children']) {

            treeDatawithTopic[data]['children'][data1] = {
              id: res[data]['enablingObjective_SubCategories'][data1]['id'],
              description:res[data]['number'] + '.' + res[data]['enablingObjective_SubCategories'][data1]['number'] + ' - ' + res[data]['enablingObjective_SubCategories'][data1]['title'],
              children: res[data]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'],
              checkbox: true,
            };
            for(var data2 in treeDatawithTopic[data]['children'][data1]['children']){
              let index=1;
                treeDatawithTopic[data]['children'][data1]['children'][data2] = {
                  id: res[data]['enablingObjective_SubCategories'][data1]['children'][data2]['id'],
                  description:res[data]['number'] + '.' + index  + '.' + res[data]['enablingObjective_SubCategories'][data1]['children'][data2]['number'] + ' - ' + res[data]['enablingObjective_SubCategories'][data1]['children'][data2]['title'],
                  children: res[data]['enablingObjective_SubCategories'][data1]['children'][data2]['enablingObjectives'],
                  checkbox: true,
                };
                index++;
                for(var data3 in treeDatawithTopic[data]['children'][data1]['children'][data2]['children']){
                  treeDatawithTopic[data]['children'][data1]['children'][data2]['children'][data3]['checkbox'] = this.alreadyLinked.includes(treeDatawithTopic[data]['children'][data1]['children'][data2]['children'][data3]['id']) ? false:true;
                  treeDatawithTopic[data]['children'][data1]['children'][data2]['children'][data3]['description'] = treeDatawithTopic[data]['children'][data1]['children'][data2]['children'][data3]['number'] +' - ' + treeDatawithTopic[data]['children'][data1]['children'][data2]['children'][data3]['description'];
                }
            }
          }
        }

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
    } */

    /* makeEOTreeWithoutTopicDataSource(res: any) {
      var treeDatawithoutTopic: any = [{}];
      for (var data in res) {
        treeDatawithoutTopic[data] = {
          id: res[data]['id'],
          description:res[data]['number'] + ' - ' + res[data]['description'],
          children: res[data]['enablingObjective_SubCategories'],
          checkbox: true,
        };

        for (var data1 in treeDatawithoutTopic[data]['children']) {


          treeDatawithoutTopic[data]['children'][data1] = {
            id: res[data]['enablingObjective_SubCategories'][data1]['id'],
            description:res[data]['number'] + '.' + res[data]['enablingObjective_SubCategories'][data1]['number'] + ' - ' + res[data]['enablingObjective_SubCategories'][data1]['title'],
            children: res[data]['enablingObjective_SubCategories'][data1]['enablingObjectives'],
            checkbox: true,
          };
          for(var data2 in treeDatawithoutTopic[data]['children'][data1]['children']){

            let isTrue = this.alreadyLinked.includes(treeDatawithoutTopic[data]['children'][data1]['children'][data2]) ? false:true;

            treeDatawithoutTopic[data]['children'][data1]['children'][data2]['checkbox'] = this.alreadyLinked.includes(treeDatawithoutTopic[data]['children'][data1]['children'][data2]['id']) ? false:true;
            treeDatawithoutTopic[data]['children'][data1]['children'][data2]['description'] = treeDatawithoutTopic[data]['children'][data1]['children'][data2]['number'] +' - ' + treeDatawithoutTopic[data]['children'][data1]['children'][data2]['description'];

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
    }  */

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
    } */

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

      this.dataSourceWithTopic.data.forEach((node:EOTree)=>{
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

      this.dataSourceWithoutTopic.data.forEach((node:EOTree)=>{
        node.children?.forEach((child)=>{
          this.checkAllParents(child);
        })
      })

      this.showActive = isActive;
    }

    temparr:any;
    filterDataNew(filterActive: boolean) {
      this.showActive = filterActive;
      this.temparr = [
        ...this.originalSourceWithTopic.data.map((element) => {
          return {
            ...element,
            children: element.children?.map((e) => {
              return {
                ...e,
                children: e.children?.map((c) => {
                  if (c.isEO) {
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

      this.temparr = [
        ...this.temparr.map((element) => {
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
      this.dataSourceWithTopic.data = Object.assign(this.temparr);
      this.treeControl.dataNodes = Object.assign(this.temparr);
      Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
        this.setParent(this.dataSourceWithTopic.data[key], undefined);
      });

      this.dataSourceWithTopic.data.forEach((cat) => {
        cat.children?.forEach((subCat) => {
          subCat.children?.forEach((topic) => {

            if (topic.isEO) {
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

   /*  filterData() {
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

      this.dataSourceWithTopic.data.forEach((node:EOTree)=>{
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

      this.dataSourceWithoutTopic.data.forEach((node:EOTree)=>{
        node.children?.forEach((child)=>{
          this.checkAllParents(child);
        })
      })
    }
   */
    // filterActive(makeActive: boolean) {
    //   let temparr = [
    //     ...this.originalSource.data.map((element) => {
    //       return {
    //         ...element,
    //         children: element.children?.map((e) => {

    //           return {
    //             ...e,
    //             children: e.children?.map((c) => {
    //               return {
    //                 ...c,
    //                 children: c.children?.filter((f) => {
    //                   return f.active == makeActive;
    //                 })
    //               }

    //             })
    //           }
    //         }
    //         ),
    //       };
    //     }),
    //   ];
    //   this.showActive = makeActive;
    //   this.dataSourceWithTopic.data = temparr;
    //
    //   Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
    //     this.setParent(this.dataSourceWithTopic.data[key], undefined);
    //   });

    //   this.dataSourceWithTopic.data.forEach((cat) => {
    //     cat.children?.forEach((subCat) => {
    //       subCat.children?.forEach((topic) => {
    //         topic.children?.forEach((eo) => {
    //
    //           this.checkAllParents(eo);
    //         })
    //       })
    //     })
    //   });
    // }

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

    // filterData(data: any, toFilter: any) {
    //
    //   if (this.filterEOString.length > 0) {
    //     let temparr = [
    //       ...this.originalSource.data.map((element) => {
    //         return {
    //           ...element,
    //           children: element.children?.map((e) => {

    //             return {
    //               ...e,
    //               children: e.children?.map((c) => {
    //                 return {
    //                   ...c,
    //                   children: c.children?.filter((f) => {
    //                     return f.description.toLowerCase().match(String(this.filterEOString).toLowerCase());
    //                   })
    //                 }

    //               })
    //             }
    //           }
    //           ),
    //         };
    //       }),
    //     ];
    //
    //     this.dataSourceWithTopic.data = temparr;


    //   } else {
    //     this.dataSourceWithTopic.data = this.originalSource.data;
    //   }

    //   Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
    //     this.setParent(this.dataSourceWithTopic.data[key], undefined);
    //   });

    //   this.dataSourceWithTopic.data.forEach((cat) => {
    //     cat.children?.forEach((subCat) => {
    //       subCat.children?.forEach((topic) => {
    //         topic.children?.forEach((eo) => {
    //
    //           this.checkAllParents(eo);
    //         })
    //       })
    //     })
    //   });
    // }

/*     itemToggle(checked: boolean, node: EOTree) {
      node.selected = node.checkbox ? checked : false;
      if (node.children !== undefined) {
        node.children.forEach((child) => {
          this.itemToggle(checked, child);
        });
      } else {
        // Add TaskIds to list
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
    } */

    itemToggle(checked: boolean, node: EOTree) {
      node.selected = checked;
      if (node.children.length > 0) {
        node.children.forEach((child) => {
          this.itemToggle(checked, child);
        });
      } else {
        if (node.selected && node.checkbox && node.isEO) {

          this.linkedIds.push(node.id);
        } else if(node.isEO && !node.selected && node.checkbox) {
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
    //  this.readyEOsWithoutTopicTreeData();
      this.linkedIds = [];
      this.eOCheckListSelection.clear();
    }

    async transformTitle(title: string) {
      const labelName = await this.labelPipe.transform(title);
      return labelName;
    } 

    linkToPosition()
    {

       var options = new PositionOptions();
      options.EOIds = this.linkedIds;

      this.positionService.LinkEnablingObjective(this.positionId,options).then(async (res:any)=>{
        this.alert.successToast("Selected " + await this.transformTitle('Enabling Objective') + "s linked to "+ await this.transformTitle('Position'));
        this.refreshLinkedEOs();
        this.refresh.emit();
        this.closed.emit('fp-position-eo-link-closed');
      });
    }
    refreshLinkedEOs() {
      this.EOTreeCheckListSelection.clear();
      this.linkedIds.forEach((id: any) => {
        this.alreadyLinked.push(id);
      });
      this.linkedIds = [];
    }

    public hideLeafNode(node: any) {
      return this.showOnlySelected && !node.selected
        ? true
        : new RegExp(this.filterEOString, 'i').test(node.description) === false;
    }

    public hideParentNode(node: any){
      return this.treeControl
          .getDescendants(node)
          .filter(node => node.children == null || node.children.length === 0)
          .every(node => this.hideLeafNode(node));
    }
}

class EOTree {
  id: any;
  description: string;
  children!: EOTree[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: EOTree;
  active?: boolean;
  isEO?: boolean;
}

