import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { SaftyHazardCompactOption } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCompactOptions';
import { SaftyHazard_CategoryCompactOptions } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryCompactOptions';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-task-sh-link',
  templateUrl: './fly-panel-task-sh-link.component.html',
  styleUrls: ['./fly-panel-task-sh-link.component.scss']
})
export class FlyPanelTaskShLinkComponent implements OnInit,OnDestroy,AfterViewInit {

  filterSHString: string = "";
  linkSHs: boolean = true;
  addSH: boolean = false;
  showActive: boolean = true;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinked: any[] = [];
  subscription = new SubSink();
  shCatWithSh: SaftyHazard_CategoryCompactOptions[] = [];
  taskId = "";

  linkedIds: any[] = [];
  treeControl = new NestedTreeControl<SHTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<SHTree>();
  originalDataSource = new MatTreeNestedDataSource<SHTree>();
  hasChild = (_: number, node: SHTree) =>
    !!node.children && node.children.length > 0;

  EOTreeCheckListSelection = new SelectionModel<SHTree>(true);
  showLinkEOLoader: boolean = false;

  constructor(
    private shService: SafetyHazardsService,
    private taskService: TasksService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      this.taskId = String(res.id).split('-')[0];
      this.readySHsTreeData();
    })
  }

  clearFilter(){
    this.filterSHString = '';
    this.readySHsTreeData();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  dataLoader = false;

  async readySHsTreeData() {
    this.dataLoader = true;
    await this.shService.getSHCategoryWithSH().then((res: SaftyHazard_CategoryCompactOptions[]) => {
      this.makeSHTreeDataSource(res);
      Object.assign(this.shCatWithSh, res);
    }).finally(()=>{
      this.dataLoader = false;
    })
  }

  makeSHTreeDataSource(res: SaftyHazard_CategoryCompactOptions[]) {

    var treeData: any[] = [];
    for (let index = 0; index < res.length; index++) {
      let procTree = new SHTree();
       if(res[index].saftyHazard_Category.active){

         procTree.description = res[index].saftyHazard_Category.title;
         procTree.checkbox = true;
         procTree.children = [];

         res[index].saftyHazardCompactOptions.forEach((proc) => {
           if(proc.active)
           procTree.children?.push({
             id: proc.id,
             description:proc.number + ' - ' + proc.title,
             checkbox: !this.alreadyLinked.includes(proc.id),
             active:proc.active,
           });
         });

         treeData.push(procTree);

       }


     }
    /* res.forEach((shCat: SaftyHazard_CategoryCompactOptions, index: any) => {


          treeData.push({ description: shCat.saftyHazard_Category.title, checkbox: true });
      treeData[index]["children"] = [];
      shCat.saftyHazardCompactOptions.forEach((sh: SaftyHazardCompactOption) => {

          treeData[index]["children"].push({ description:  `${sh.number} ` + sh.title, active: sh.active, checkbox: !this.alreadyLinked.includes(sh.id), id: sh.id });

      })


    }); */
    this.dataSource.data = [];
    this.dataSource.data = Object.assign([],treeData);
    this.originalDataSource.data = Object.assign([],this.dataSource.data);
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
      this.setParent(this.originalDataSource.data[key], undefined);
    });

    this.filterActive(true);
  }

  private setParent(node: SHTree, parent: SHTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  private checkAllParents(node: SHTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  filterData(data: any, toFilter: any) {
      let temparr = [
        ...this.originalDataSource.data.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) => {
              return (c.description.toLowerCase().match(String(this.filterSHString).toLowerCase()))
                && c.active == this.showActive;
            }
            ),
          };
        }),
      ];

      this.dataSource.data = temparr;
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
      });

      this.dataSource.data.forEach((node) => {
        node.children?.forEach((child) => {
          this.checkAllParents(child);
        })
      });
      this.treeControl.dataNodes = this.dataSource.data;
      this.filterSHString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();

  }

  onSHChange(event: any, node: any) {
    if (event.checked) {
      this.EOTreeCheckListSelection.select(node);
    } else {
      this.EOTreeCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }

  itemToggle(checked: boolean, node: SHTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add EOIds to list
      if (checked && node.checkbox) {
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
  }

  filtered(node: any) {

    return node.description.includes(this.filterSHString);
  }

  refreshLinkedIds() {
    this.EOTreeCheckListSelection.clear();
    this.linkedIds.forEach((id: any) => {
      this.alreadyLinked.push(id);
    });
    this.linkedIds = [];
    this.makeSHTreeDataSource(this.shCatWithSh);
  }

  refreshSHData() {
    this.EOTreeCheckListSelection.clear();
    this.linkedIds = [];
    this.readySHsTreeData();
  }

  filterActive(makeActive: boolean) {
    this.showActive = makeActive;
    this.filterData("","");
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
  
  async linkToTask(){
    this.showLinkEOLoader = true;
    var options = new TaskOptions();
    options.safetyHazardIds = this.linkedIds;
    await this.taskService.LinkSaftyHazards(this.taskId, options).then(async (_)=>{
      this.alert.successToast("Selected " +  await this.transformTitle('Safety Hazard') + "s linked to " + await this.transformTitle('Task'));
      this.refresh.emit();
      this.closed.emit('fp-task-sh-link-closed');
    }).finally(()=>{
      this.showLinkEOLoader = false;
    })
  }
}

class SHTree {
  id: any;
  description: string;
  children?: SHTree[];
  checkbox?: boolean;
  active?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: SHTree;
}
