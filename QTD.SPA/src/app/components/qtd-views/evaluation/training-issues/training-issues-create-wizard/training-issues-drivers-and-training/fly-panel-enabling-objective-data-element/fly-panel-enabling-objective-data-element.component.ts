import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { EOTree } from '@models/EnablingObjective/EOTree';
import { EnablingObjective } from '@models/EnablingObjective/EnablingObjective';
import { TrainingIssue_DataElement_VM } from '@models/TrainingIssues/TrainingIssue_DataElement_VM';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-enabling-objective-data-element',
  templateUrl: './fly-panel-enabling-objective-data-element.component.html',
  styleUrls: ['./fly-panel-enabling-objective-data-element.component.scss']
})
export class FlyPanelEnablingObjectiveDataElementComponent implements OnInit {

  @Output() closed = new EventEmitter<any>();
  @Output() linkDataElement = new EventEmitter<TrainingIssue_DataElement_VM>();
  @Input() inputTrainingIssueDataElementVM: TrainingIssue_DataElement_VM = new TrainingIssue_DataElement_VM();
  enablingObjectiveDataSource = new MatTreeNestedDataSource<EOTree>();
  eoTreeControl = new NestedTreeControl<EOTree>((node: any) => node.children);
  originalSource = new MatTreeNestedDataSource<EOTree>();
  eoCheckListSelection = new SelectionModel<any>(true);
  loader:boolean = false;
  filterSearchString:string = '';
  showActive:boolean = true;
  notTopicEOs: number = 0;
  linkedId:string;
  hasChild = (_: number, node: EOTree) =>
    !!node.children && node.children.length > 0;
  constructor(
    public flyPanelService:FlyInPanelService,
    public eoSrvc: EnablingObjectivesService,
  ) { }

   ngOnInit() {
    this.linkedId = this.inputTrainingIssueDataElementVM?.dataElementId;
    this.readyEnablingObjectiveTreeData();
    
  }

  async readyEnablingObjectiveTreeData() {
    this.loader = true;
    await this.eoSrvc.getAll().then((res: EnablingObjective[]) => {
      this.readyEOTreeData(res);
      this.loader = false;
    });
  }

  expandAndSelectNodeById(linkedId: string) {
    const findAndExpand = (node: EOTree): boolean => {
      if (node.id === linkedId) {
        this.linkedId = node.id;
        return true;
      }
  
      if (node.children) {
        for (let child of node.children) {
          if (findAndExpand(child)) {
            this.eoTreeControl.expand(node);
            return true;
          }
        }
      }
  
      return false;
    };
  
    this.enablingObjectiveDataSource.data.forEach((data) => {
      findAndExpand(data);
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
          level:"category"
        })
        cat['enablingObjective_SubCategories'].forEach((subCat, j) => {
          treeData[i].children?.push({
            children: [],
            description: `${cat['number']}.${subCat['number']} ` + subCat['title'],
            id: subCat.id,
            IsEO: false,
            level:"subCategory"
          });
          subCat['enablingObjectives'].forEach((eo) => {
            if (!eo['isMetaEO'] && !eo['isSkillQualification']) {
              treeData[i].children[j].children?.push({
                children: [],
                description: `${cat['number']}.${subCat['number']}.0.${eo['number']} ${eo['description']}`,
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
              IsEO: false,
              level:"topic"
            });
            topic['enablingObjectives'].forEach((eo, l) => {
              !eo['isMetaEO'] && !eo['isSkillQualification'] ? treeData[i].children[j]?.children[k + this.notTopicEOs]?.children?.push({
                children: [],
                description: `${cat['number']}.${subCat['number']}.${topic['number']}.${eo['number']} ${eo['description']}`,
                active: eo['active'],
                id: eo['id'],
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

  searchFilter(event: any) {
    this.filterSearchString = event?.target?.value ?? "";
    this.filterEO()
  }

  clearSearchString() {
    this.filterSearchString = '';
    this.filterEO();
  }

  filterActive(filterType: boolean) {
    this.showActive = filterType;
    this.filterEO();
  }

  linkEnablingObjective(){
    this.inputTrainingIssueDataElementVM.dataElementId = this.linkedId;
    this.linkDataElement.emit(this.inputTrainingIssueDataElementVM);
    this.closed.emit();
  }

  onEOChange(selected: any, node: EOTree) {
    this.eoCheckListSelection.clear();
    if (selected) {
      this.eoCheckListSelection.select(node);
      this.linkedId = node.id;
    }
  }
  
  isNestedTreeNode(node: any) {
    if (node.level === 'topic') {
      return this.filterTopicNode(node);
    } else if (node.level === 'subCategory') {
      return this.filterSubCatNode(node);
    } else if (node.level === 'category') {
      return this.filterCatNode(node);
    }
  }
  
  filterCatNode(node: any) {
    return node.children?.some((child: any) => this.filterSubCatNode(child));
  }
  
  filterSubCatNode(node:any){
    var eoChild  = node.children.filter(x=>x.IsEO)
    var notEoChild =  node.children.filter(x=>!x.IsEo)
    if(eoChild !=null){
      return eoChild.some(x=>this.isEoChecked(x));
    }
    if(notEoChild != null){
      return notEoChild.some(item=>this.filterTopicNode(item));
    }
  }
  
  filterTopicNode(node: any) {
    return node.children?.some((x: any) => this.isEoChecked(x));
  }

  isEoChecked(node:EOTree){
    return this.linkedId != null ? this.linkedId == node.id : false;
  }

  filterEO() {
    const filterNode = (node) => {
      if (node.IsEO) {
        return node.description.toLowerCase().includes(this.filterSearchString.toLowerCase()) && node.active === this.showActive ? { ...node, children: [] } : null;
      }
  
      const filteredChildren = node.children?.map(filterNode).filter((child) => child !== null);
  
      if (filteredChildren && filteredChildren.length > 0) {
        return {
          ...node,
          children: filteredChildren
        };
      } else {
        return null;
      }
    };
  
    const filteredData = this.originalSource.data.map(filterNode).filter((element) => element !== null);
    this.enablingObjectiveDataSource.data = filteredData;
    this.eoTreeControl.dataNodes = filteredData;
  
    this.enablingObjectiveDataSource.data.forEach((data) => {
      this.setParent(data, undefined);
    });
  
    if (this.filterSearchString.length > 0) {
      this.eoTreeControl.expandAll();
    } else {
      this.eoTreeControl.collapseAll();
    }

    this.expandAndSelectNodeById(this.linkedId);
  
  }


}
