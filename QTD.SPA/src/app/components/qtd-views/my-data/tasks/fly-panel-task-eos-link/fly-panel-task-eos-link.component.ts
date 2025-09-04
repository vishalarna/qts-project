import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { EOCatTreeVM } from 'src/app/_DtoModels/TreeVMs/EOTreeVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-task-eos-link',
  templateUrl: './fly-panel-task-eos-link.component.html',
  styleUrls: ['./fly-panel-task-eos-link.component.scss'],
})
export class FlyPanelTaskEosLinkComponent
  implements OnInit, OnDestroy, AfterViewInit {
  filterEOString: string = "";
  linkEOs: boolean = true;
  addEO: boolean = false;
  showActive: boolean = true;
  isEOLoading: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  linkedIds: any[] = [];
  treeControl = new NestedTreeControl<EOTreeNew>((node: any) => node.children);
  dataSourceWithTopic = new MatTreeNestedDataSource<EOTreeNew>();
  originalSource = new MatTreeNestedDataSource<EOTreeNew>();
  myDataWithTopic: any[] = []
  eOCheckListSelection = new SelectionModel<EOTreeNew>(true);
  taskId = '';
  myDataWithoutTopic: any[] = [];
  originalSourceWithoutTopic = new MatTreeNestedDataSource<EOTreeNew>();
  dataSourceWithoutTopic = new MatTreeNestedDataSource<EOTreeNew>();
  showLinkLoader: boolean = false;
  notTopicEOs = 0;

  @Input() alreadyLinked: any[] = [];
  hasChild = (_: number, node: EOTreeNew) =>
    !!node.children && node.children.length > 0;

  EOTreeCheckListSelection = new SelectionModel<EOTreeNew>(true);
  showLinkEOLoader: boolean = false;
  @Input() id: any;
  subscription = new SubSink();

  constructor(
    public eoService: EnablingObjectivesService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private taskService: TasksService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.getEOData();
  }

  ngAfterViewInit(): void {

    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.taskId = String(res.id).split('-')[0];
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async getEOData() {
    this.isEOLoading = true;
    await this.eoService
      .getMinimizedForTree()
      .then((res: EOCatTreeVM[]) => {
        this.makeEOTreeDataSource(res);
        // this.makeEOTreeWithoutTopicDataSource(res);
      })
      .finally(() => {
        this.isEOLoading = false;
      });
  }

  clearFilter(){
    this.filterEOString = null;
    this.getEOData();
  }

  makeEOTreeDataSource(res: EOCatTreeVM[]) {
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
            if (!eo['isMetaEO']) {
              treeData[i].children[j].children?.push({
                children: [],
                description: `${cat['number']}.${subCat['number']}.0.${eo['number']} ${eo['description']}`,
                id: eo.id,
                active: eo['active'],
                checkbox: !this.alreadyLinked.includes(eo.id),
                IsEO: true,
                IsSQ:eo['isSkillQualification'],
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
              !eo['isMetaEO'] ? treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                children: [],
                description: `${cat['number']}.${subCat['number']}.${topic['number']}.${eo['number']} ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
                checkbox: !this.alreadyLinked.includes(eo.id),
                IsEO: true,
                IsSQ:eo['isSkillQualification'],
              }) : '';
            });
          });
          this.notTopicEOs = 0;
        });
      })

      this.treeControl.dataNodes = Object.assign([], treeData);
      this.dataSourceWithTopic.data = Object.assign([], treeData);
      this.originalSource.data = Object.assign([], treeData);
      Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
        this.setParent(this.dataSourceWithTopic.data[key], undefined);
        this.setParent(this.originalSource.data[key], undefined);
      });

      this.filterDataNew(this.showActive);
    }
  }

  filterDataNew(filterActive: boolean) {
    this.showActive = filterActive;
    let temparr = [
      ...this.originalSource.data.map((element) => {
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

  filterData(data: any, toFilter: any) {

    let temparr = [
      ...this.originalSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {

            return {
              ...e,
              children: e.children?.map((c) => {
                return {
                  ...c,
                  children: c.children?.filter((f) => {
                    return f.description.toLowerCase().match(String(this.filterEOString).toLowerCase()) && f.active === this.showActive;
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

    Object.keys(this.dataSourceWithTopic.data).forEach((key: any) => {
      this.setParent(this.dataSourceWithTopic.data[key], undefined);
    });

    this.dataSourceWithTopic.data.forEach((cat) => {
      cat.children?.forEach((subCat) => {
        if (subCat.IsEO) {
          this.checkAllParents(subCat);
        }
        subCat.children?.forEach((topic) => {
          topic.children?.forEach((eo) => {

            this.checkAllParents(eo);
          });
        });
      });
    });

    this.treeControl.dataNodes = this.dataSourceWithTopic.data;
    this.filterEOString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  itemToggle(checked: boolean, node: EOTreeNew) {
    node.selected = checked;
    if (node.children.length > 0) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      if (node.selected && node.checkbox && node.IsEO) {

        this.linkedIds.push(node.id);
      } else if(node.IsEO && !node.selected && node.checkbox) {
        var index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
        }
      }
    }
    this.linkedIds = [...new Set(this.linkedIds)];

    this.checkAllParents(node);
  }

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
    this.getEOData();
    this.linkedIds = [];
    this.eOCheckListSelection.clear();
  }

  linkToTask() {
    this.showLinkLoader = true;
    var options = new TaskOptions();
    options.enablingObjectiveIds = this.linkedIds;

    this.taskService
      .LinkEnablingObjective(this.taskId, options)
      .then(async (res: any) => {
        this.alert.successToast('Selected ' + await this.transformTitle('Enabling Objective') + 's linked to ' +  await this.transformTitle('Task'));
        this.refresh.emit();
        this.closed.emit('fp-task-eo-link-closed');
      }).finally(() => {
        this.showLinkLoader = false;
      });
  }

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
  IsSQ?:boolean = false;
}
