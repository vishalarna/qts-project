import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EOCatTreeVM } from 'src/app/_DtoModels/TreeVMs/EOTreeVM';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';

@Component({
  selector: 'app-flypanel-select-eo',
  templateUrl: './flypanel-select-eo.component.html',
  styleUrls: ['./flypanel-select-eo.component.scss']
})
export class FlypanelSelectEoComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() idSelected = new EventEmitter<any>();
  @Input() selectedId = "";
  filterEOString = "";
  length = 0;

  hasChild = (_: number, node: EOTree) =>
    !!node.children && node.children.length > 0;

  notTopicEOs = 0;

  showActive = true;
  treeControl = new NestedTreeControl<EOTree>((node: any) => node.children);
  dataSourceWithTopic = new MatTreeNestedDataSource<EOTree>();
  originalSource = new MatTreeNestedDataSource<EOTree>();
  isLoading = false;

  constructor(
    private eoService: EnablingObjectivesService,
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.getEOData();
  }

  async getEOData() {
    await this.eoService
      .getMinimizedForTree()
      .then((res: EOCatTreeVM[]) => {

        this.makeEOTreeDataSource(res);
      }).finally(()=>{
        this.isLoading = false;
      });
  }

  makeEOTreeDataSource(res: EOCatTreeVM[]) {
    this.notTopicEOs = 0;
    this.isLoading = true;
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
                checkbox: this.selectedId !== eo.id,
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
                checkbox: this.selectedId !== eo.id,
                IsEO: true,
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
    this.isLoading = false;
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

    this.filterEOString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  private setParent(node: EOTree, parent: EOTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  filterData(filterActive: boolean) {
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
  }

}

class EOTree {
  id!: any;
  description!: string;
  children!: EOTree[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: EOTree;
  active?: boolean;
  IsEO?: boolean;
}
