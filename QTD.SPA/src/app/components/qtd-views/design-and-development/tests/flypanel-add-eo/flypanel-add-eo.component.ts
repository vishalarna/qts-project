import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { IlaService } from 'src/app/_Services/QTD/ila.service';

@Component({
  selector: 'app-flypanel-add-eo',
  templateUrl: './flypanel-add-eo.component.html',
  styleUrls: ['./flypanel-add-eo.component.scss']
})
export class FlypanelAddEoComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Output() idSelected = new EventEmitter<any>();
  @Input() selectedId = "";
  @Input() ilaId!: any;
  isLoading = false;

  filterEOString = "";
  length = 0;
  selection = new SelectionModel<any>(false);

  hasChild = (_: number, node: EOTreeNew) =>
    !!node.children && node.children.length > 0;

  showActive = true;
  treeControl = new NestedTreeControl<EOTreeNew>((node: any) => node.children);
  dataSourceWithTopic = new MatTreeNestedDataSource<EOTreeNew>();
  originalSource = new MatTreeNestedDataSource<EOTreeNew>();
  notTopicEOs = 0;

  constructor(
    private eoService: EnablingObjectivesService,
    private ilaService: IlaService
  ) { }

  ngOnInit(): void {
    this.isLoading = true;
    if (this.ilaId!=undefined && this.ilaId!=null) {
      this.getEODataByIla()
    }else{
      this.getEOData();
    }
  }


  async getEODataByIla(){
      await this.ilaService.getLinkedEOWithEOCategories(this.ilaId)
      .then((res: EnablingObjective[]) => {
        
        this.makeEOTreeDataSource(res);
      }).finally(()=>{
        this.isLoading = false;
      });
  }

  async getEOData() {
    await this.eoService
      .getAll()
      .then((res: EnablingObjective[]) => {
        
        this.makeEOTreeDataSource(res);
      }).finally(()=>{
        this.isLoading = false;
      });
  }

  makeEOTreeDataSource(res: EnablingObjective[]) {
    this.notTopicEOs = 0;
    this.isLoading = true;
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
            if (eo['active'] === true) {
              treeData[i].children[j].children?.push({
                children: [],
                description: `${eo['number']} ${eo['description']}`,
                id: eo.id,
                active: eo['active'],
                IsEO: true,
                IsSkillQualification: eo.isSkillQualification,
                IsMetaEO: eo.isMetaEO,
                checkbox:true,
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
              eo['active'] === true ? treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                children: [],
                description: `${eo['number']} ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
                IsEO: true,
                IsSkillQualification: eo.isSkillQualification,
                IsMetaEO: eo.isMetaEO,
                checkbox:true,
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
    this.selectNode();
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
                      IsSkillQualification: c.IsSkillQualification,
                      IsMetaEO: c.IsMetaEO,
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

    this.dataSourceWithTopic.data.forEach((cat) => {
      cat.children?.forEach((subCat) => {
        subCat.children?.forEach((topic) => {
          
          if (topic.IsEO) {
            // this.linkedIds.includes(topic.id) ? topic.selected = true:"";
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

  private checkAllParents(node: EOTreeNew) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  onEOChange(event: any, node: EOTreeNew) {
    if (event.checked) {
      this.selection.clear();
      this.selection.select(node);
    }
    else {
      this.selection.deselect(node);
    }
  }

  selectNode() {
    
    this.dataSourceWithTopic.data.forEach((cat) => {
      cat.children.forEach((subCat) => {
        subCat.children.forEach((eo) => {
          if (eo.IsEO && eo.id === this.selectedId) {
            this.selection.clear();
            this.selection.select(eo);
          }
          eo.children.forEach((rootEO) => {
            if (rootEO.IsEO && rootEO.id === this.selectedId) {
              this.selection.clear();
              this.selection.select(rootEO);
            }
          })
        });
      })
    })
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
  IsSkillQualification?: boolean = false;
  IsMetaEO?: boolean = false;
}
