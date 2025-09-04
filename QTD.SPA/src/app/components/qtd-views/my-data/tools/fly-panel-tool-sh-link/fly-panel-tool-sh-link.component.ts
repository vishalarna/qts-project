import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, EventEmitter, Input, OnInit, Output, OnDestroy, AfterViewInit } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { SaftyHazard_CategoryCompactOptions } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryCompactOptions';
import { Link_Tool_Options } from 'src/app/_DtoModels/Tool/Link_Tool_Options';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

class SHTree {
  id: any;
  description: string;
  children?: SHTree[];
  checkbox?: boolean;
  selected?: boolean;
  active?:boolean;
  indeterminate?: boolean;
  parent?: SHTree;
  safetyHazardSetIds?: number[];
  visible: boolean;
}

@Component({
  selector: 'app-fly-panel-tool-sh-link',
  templateUrl: './fly-panel-tool-sh-link.component.html',
  styleUrls: ['./fly-panel-tool-sh-link.component.scss']
})
export class FlyPanelToolShLinkComponent implements OnInit, OnDestroy, AfterViewInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refreshSHLinks = new EventEmitter<any>();
  @Input() alreadyLinked: any[] = [];
  subscriptions = new SubSink();
  filterSHString: any;
  linkSHs: boolean = true;
  addSH: boolean = false;
  categoryIds: any[] = [];
  safetyHazardIds: any[] = [];
  hazardSetIds: any[] = [];
  treeControl = new NestedTreeControl<SHTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<SHTree>();
  showLinkEOLoader: boolean = false;
  showActive:boolean;
  id:any

  hasChild = (_: number, node: SHTree) =>
    !!node.children && node.children.length > 0;

  constructor(
    private route:ActivatedRoute,
    private shService: SafetyHazardsService,
    private alert: SweetAlertService,
    public flyService: FlyInPanelService,
    private toolService:ToolsService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    this.readySHsTreeData();
  }

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.route.params.subscribe((res: any) => {
      this.id = res.id;
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  async readySHsTreeData() {
    await this.shService.getSHCategoryWithSH().then((res: SaftyHazard_CategoryCompactOptions[]) => {
      this.makeSHTreeDataSource(res);
    }).catch(async (err: any) => {
      console.error(err);
      this.alert.errorToast("Error Fetching "+await this.labelPipe.transform("Safety Hazard")+" Data " + err?.message);
    })
  }

  makeSHTreeDataSource(res: SaftyHazard_CategoryCompactOptions[]) {
    var treeData: any = [];
   
    for (let index = 0; index < res.length; index++) {
      let procTree = new SHTree();
       if(res[index].saftyHazard_Category.active){
        procTree.id = res[index].saftyHazard_Category.id;
        procTree.description = res[index].saftyHazard_Category.title;
        procTree.selected = false;
        procTree.indeterminate = false;
        procTree.checkbox = true;
        procTree.children = [];
        procTree.visible = true;

        res[index].saftyHazardCompactOptions.forEach((proc) => {
          if(proc.active)
          {
            procTree.children?.push({
              id: proc.id,
              description:proc.number + ' - ' + proc.title,
              selected: false,
              checkbox: !this.alreadyLinked.includes(proc.id),
              active:proc.active,
              safetyHazardSetIds: proc.safetyHazardSetIds,
              visible: true
            });
          }
        });
        treeData.push(procTree);
       }
     }
    this.dataSource.data = treeData;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });
    this.treeControl.dataNodes = this.dataSource.data; // Set so treeControl.collapse/expand commands work
  }

  private setParent(node: SHTree, parent: SHTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  filterData() {
    if (this.filterSHString.length > 0) {
      this.dataSource.data.forEach((category)=>{
        let childVisible = false;
        category.children?.forEach((safetyHazard)=>{
          safetyHazard.visible = safetyHazard.description.toLowerCase().includes(String(this.filterSHString).toLowerCase());
          childVisible = childVisible || safetyHazard.visible;
        });
        category.visible = childVisible || category.description.toLowerCase().includes(String(this.filterSHString).toLowerCase());
      });
      this.treeControl.expandAll();
    }else{
      this.dataSource.data.forEach((category)=>{
        category.children?.forEach((safetyHazard)=>{
          safetyHazard.visible = true;
        });
        category.visible = true;
      });
      this.treeControl.collapseAll();
    }
  }

  onNodeChange(event: any, node: SHTree) {
    this.itemToggle(event.checked, node);
    this.calculateSelectedCounts();
  }
  
  itemToggle(checked: boolean, node: SHTree) {
    node.selected = node.checkbox ? checked : false;
    
    // itemToggle any children
    if (!!node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } 

    // Update parent's selected/indeterminate status given this node changed status
    this.checkAllParents(node);
  }

  private checkAllParents(node: SHTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => { return (child.selected || !child.checkbox)});
      node.parent.indeterminate = descendants.some((child) => { return (child.selected)});
      this.checkAllParents(node.parent);
    }
  }

  private calculateSelectedCounts(){
    this.categoryIds = [];
    this.safetyHazardIds = [];
    this.hazardSetIds = [];

    // Add all Category, Safety Hazard, and Hazard Set ids to property arrays
    this.dataSource.data.forEach(category => {
      if(category.selected || category.indeterminate){
        this.categoryIds.push(category.id);
      }
      category.children?.forEach(safetyHazard => {
        if(safetyHazard.selected){
          this.safetyHazardIds.push(safetyHazard.id);
          safetyHazard.safetyHazardSetIds.forEach(hazardSetId=> {
            this.hazardSetIds.push(hazardSetId);
          });
        }
      });
    });

    // Deduplicate any redundant ids via Sets
    this.categoryIds = [...new Set(this.categoryIds)];
    this.safetyHazardIds = [...new Set(this.safetyHazardIds)];
    this.hazardSetIds = [...new Set(this.hazardSetIds)];
  }

  refreshData() {
    this.categoryIds = [];
    this.safetyHazardIds = [];
    this.hazardSetIds = [];
    this.readySHsTreeData();
  }

  linkSHsToTools() {
    var options = new Link_Tool_Options();
    options.toolIds = [];
    options.toolIds.push(this.id);
    options.linkedIds = this.safetyHazardIds;

    this.toolService.LinkSHs(options).then(async (res: any) => {
      this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard")} Linked To ` + await this.labelPipe.transform('Tool') + `s`);
      this.closed.emit('fp-rr-task-link-closed');
    });
  }

}