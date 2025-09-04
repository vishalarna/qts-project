import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { Link_Tool_Options } from 'src/app/_DtoModels/Tool/Link_Tool_Options';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-tool-skill-link',
  templateUrl: './fly-panel-tool-skill-link.component.html',
  styleUrls: ['./fly-panel-tool-skill-link.component.scss']
})
export class FlyPanelToolSkillLinkComponent implements OnInit {

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
  @Input() Id:any;
  @Input() Title:any;
  hasChild = (_: number, node: EOTree) =>
    !!node.children && node.children.length > 0;

  EOTreeCheckListSelection = new SelectionModel<EOTree>(true);
  showLinkEOLoader: boolean = false;
  @Input() id: any;
  subscription = new SubSink();
  notTopicEOs: number;
  showOnlySelected = false;
  toolId: string;
  
  constructor(public eoService: EnablingObjectivesService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private toolService: ToolsService,
    public flyPanelClose : FlyInPanelService,
    private labelPipe: LabelReplacementPipe, ) { }

    ngOnInit(): void {
      this.readyEOsWithTopicTreeData();
      //this.readyEOsWithoutTopicTreeData();
    }
  
    ngAfterViewInit(): void {
      this.subscription.sink = this.route.params.subscribe((res:any)=>{
        this.toolId = String(res.id).split('-')[0];
      })
    }
  
    ngOnDestroy(): void {
      this.subscription.unsubscribe();
    }

    linkToTool(){
      var options = new Link_Tool_Options();
      options.toolIds = [];
      options.toolIds.push(this.toolId);
      options.linkedIds = this.linkedIds;
      
      this.toolService.LinkEOs(options).then(async (res:any)=>{
        this.alert.successToast("Selected " + await this.transformTitle('Enabling Objective') + "s linked to " + await this.labelPipe.transform('Tool') + "s");
        this.refreshLinkedEOs();
      });
    }

    refreshLinkedEOs() {
      this.EOTreeCheckListSelection.clear();
      this.linkedIds.forEach((id: any) => {
        this.alreadyLinked.push(id);
      });
      this.linkedIds = [];
      this.flyPanelClose.close();
      this.closed.emit();
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
