import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EOCatTreeVM } from 'src/app/_DtoModels/TreeVMs/EOTreeVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-eo-not-linked',
  templateUrl: './flypanel-eo-not-linked.component.html',
  styleUrls: ['./flypanel-eo-not-linked.component.scss']
})
export class FlypanelEoNotLinkedComponent implements OnInit {
  filterEOString: string = "";
  @Input() NotLinkedToName: string;
  @Output() closed = new EventEmitter<any>();
  @Input() activeInactiveCheck: any;

  isEOLoading = true;
  notTopicEOs = 0;

  showActive: boolean = true;
  dataSource = new MatTreeNestedDataSource<EOTreeNew>();
  tableDataSource = new MatTableDataSource<any>();
  displayColumns = ["number","description"];
  originalDatasource = new MatTreeNestedDataSource<EOTreeNew>();
  treeControl = new NestedTreeControl<EOTreeNew>(
    (node: any) => node.children
  );
  hasChild = (_: number, node: EOTreeNew) =>
    !!node.children && node.children.length > 0;

  linkedIds: any[] = [];
  srctaskList: EOTree[] = [];
  @ViewChild(MatPaginator) paginator!:MatPaginator;
  @ViewChild(MatSort) sort!:MatSort;
  constructor(
    private eoService: EnablingObjectivesService,
    private alert: SweetAlertService,
    public flyin: FlyInPanelService,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    if (this.activeInactiveCheck === true) {
      this.getNames();
    } else {
      this.getLinkedIds();
    }
  }

  async getLinkedIds() {
    this.spinner = true;

    this.linkedIds = await this.eoService.getLinkedIds(this.NotLinkedToName);
    this.eoService.getMinimizedForTree().then((res: EOCatTreeVM[]) => {
      this.getTreeData(res);
    }).finally(() => {
      this.spinner = false;
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  clearFilter() {
    this.filterEOString = '';
/*     this.getLinkedIds(); */  

    this.dataSource.data = this.originalDatasource.data;
  }

  async getTreeData(res: EOCatTreeVM[]) {
    this.notTopicEOs = 0;
    if (res.length == 0) {
      this.dataSource.data = [];
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
            if (!eo['isMetaEO'] && !eo['isSkillQualification'] && !this.linkedIds.includes(eo.id)) {
              treeData[i].children[j].children?.push({
                children: [],
                description: `${eo['number']} ${eo['description']}`,
                id: eo.id,
                active: eo['active'],
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
              (!eo['isMetaEO'] && !eo['isSkillQualification'] && !this.linkedIds.includes(eo.id)) ? treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                children: [],
                description: `${eo['number']} ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
                IsEO: true,
              }) : '';
            });
          });
          this.notTopicEOs = 0;
        });
      })

      this.treeControl.dataNodes = Object.assign([], treeData);
      this.dataSource.data = Object.assign([], treeData);
      this.originalDatasource.data = Object.assign([], treeData);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.originalDatasource.data[key], undefined);
      });

      this.filterDataNew(this.showActive);
    }
    this.isEOLoading = false;
  }

  filterDataNew(filterActive: boolean) {
    this.showActive = filterActive;
    let temparr = [
      ...this.originalDatasource.data.map((element) => {
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
    this.dataSource.data = Object.assign(temparr);
    this.treeControl.dataNodes = Object.assign(temparr);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });

    this.dataSource.data.forEach((cat) => {
      cat.children?.forEach((subCat) => {
        subCat.children?.forEach((topic) => {

          if (topic.IsEO) {
            this.linkedIds.includes(topic.id) ? topic.selected = true : "";
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

  private checkAllParents(node: EOTreeNew) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  // filterData(active: boolean) {
  //   this.showActive = active;
  //   let temparr = [
  //     ...this.originalDatasource.data.map((element) => {
  //       return {
  //         ...element,
  //         children: element.children?.map((e) => {
  //           return {
  //             ...e,
  //             children: e.children?.map((c) => {
  //               return {
  //                 ...c,
  //                 children: c.children?.filter((f) => {
  //                   return f.description.toLowerCase().match(String(this.filterEOString).toLowerCase()) && f.active === this.showActive;
  //                 })
  //               }
  //             })
  //           }
  //         }
  //         ),
  //       };
  //     }),
  //   ];
  //   this.dataSource.data = temparr;
  //   this.treeControl.dataNodes = temparr;
  //   this.filterEOString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  // }

  private setParent(node: EOTreeNew, parent: EOTreeNew | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  name: any;
  spinner: boolean;
  getNames() {
    switch (this.NotLinkedToName) {
      case 'Active':
        this.name = 'Active EOs';
        this.spinner = true;
        var temp = [];
        this.eoService.getEOsACtiveInactive(this.NotLinkedToName).then((data) => {
          data.forEach((task, index) => {
            temp.push({
              id: task.id,
              description: task.description,
              number: task.number
            })
          })
          this.tableDataSource.data = temp;

          this.spinner = false;
          setTimeout(()=>{
            this.tableDataSource.sort = this.sort;
            this.tableDataSource.paginator = this.paginator;
          },1)
        }).finally(() => {
          this.spinner = false;
        })
        break;

      case 'Inactive':
        this.name = 'Inactive EOs';
        this.spinner = true;
        var temp = [];
        this.eoService.getEOsACtiveInactive(this.NotLinkedToName).then((data) => {
          data.forEach((task, index) => {
            temp.push({
              id: task.id,
              description: task.description,
              number: task.number
            })
          })
          this.tableDataSource.data = temp;
          this.spinner = false;
          setTimeout(()=>{
            this.tableDataSource.sort = this.sort;
            this.tableDataSource.paginator = this.paginator;
          },1)
        }).catch(async (err) => {
          this.alert.errorToast('Error Fetching ' + await this.transformTitle('Task') + 's');
        }).finally(() => {
          this.spinner = false;
        })
        break;
    }
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
}

class EOTree {
  id: any;
  description: string;
  children: EOTree[];
  checkbox?: boolean;
  checked?: boolean;
  active?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: EOTree;
  isEO?: boolean;
}
