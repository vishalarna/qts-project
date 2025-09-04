import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { RR_SaftyHazard_LinkOptions } from 'src/app/_DtoModels/RR_SaftyHazard_Link/RR_SaftyHazard_LinkOptions';
import { SaftyHazardCompactOption } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCompactOptions';
import { SaftyHazard_CategoryCompactOptions } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryCompactOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-rr-safety-hazard-link',
  templateUrl: './fly-panel-rr-safety-hazard-link.component.html',
  styleUrls: ['./fly-panel-rr-safety-hazard-link.component.scss'],
})
export class FlyPanelRRSafetyHazardLinkComponent implements OnInit, AfterViewInit, OnDestroy {
  showLoader=true;
  filterSHString: string;
  linkSHs: boolean = true;
  addSH: boolean = false;
  showActive: boolean = false;
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  @Input() alreadyLinked: any[] = [];
  subscription = new SubSink();
  shCatWithSh: SaftyHazard_CategoryCompactOptions[] = [];
  rrId = "";

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
    private rrService:RegulatoryRequirementService,
    private alert:SweetAlertService,
    private route:ActivatedRoute,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.route.params.subscribe((res:any)=>{
      this.rrId = res.id;
      this.readySHsTreeData();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  clearFilter(){
    this.filterSHString = '';
    this.dataSource.data = this.originalDataSource.data;
  }

  async readySHsTreeData() {
    await this.shService.getSHCategoryWithSH().then((res: SaftyHazard_CategoryCompactOptions[]) => {
      this.showLoader=true;
      this.makeSHTreeDataSource(res);
      Object.assign(this.shCatWithSh,res);
      this.showLoader=false;
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
  /*   res.forEach((shCat: SaftyHazard_CategoryCompactOptions, index: any) => {
      

        treeData.push({ description: shCat.saftyHazard_Category.title, checkbox: true });
        treeData[index]["children"] = [];
        shCat.saftyHazardCompactOptions.forEach((sh: SaftyHazardCompactOption) => {

            treeData[index]["children"].push({ description: sh.number + ' - ' + sh.title, active: sh.active, checkbox: !this.alreadyLinked.includes(sh.id), id: sh.id});

        })


    }); */
    this.dataSource.data = [];
    this.dataSource.data = treeData;
    this.originalDataSource.data = this.dataSource.data;
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
    if (this.filterSHString.length > 0) {
      let temparr = [
        ...this.originalDataSource.data.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) =>
              c.description.toLowerCase().match(String(this.filterSHString).toLowerCase())
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
    } else {
      this.dataSource.data = this.originalDataSource.data;
    }
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

  linkShData(){
    this.showLinkEOLoader = true;
    var options = new RR_SaftyHazard_LinkOptions();
    options.regulatoryRequirementId = this.rrId;
    options.safetyHazardIds = this.linkedIds;
    this.rrService.LinkSH(this.rrId,options).then(async (res:any)=>{
      //this.refreshLinkedIds();
      this.refresh.emit();
      this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard") }s Linked to ` + await this.labelPipe.transform('Regulatory Requirement'));
    }).finally(()=>{
      this.showLinkEOLoader = false;
      this.closed.emit('fp-rr-sh-link-closed');
    });
  }

  refreshLinkedIds(){
    this.EOTreeCheckListSelection.clear();
    this.linkedIds.forEach((id:any)=>{
      this.alreadyLinked.push(id);
    });
    this.linkedIds = [];
    this.makeSHTreeDataSource(this.shCatWithSh);
  }

  refreshSHData(){
    this.EOTreeCheckListSelection.clear();
    this.linkedIds = [];
    this.readySHsTreeData();
  }

  filterActive(makeActive: boolean){
    let temparr = [
      ...this.originalDataSource.data.map((element) => {

        return {
          ...element,
          children: element.children?.filter((c) =>
            c.active === makeActive
          ),
        };
      }),
    ];
    this.showActive = makeActive;
    this.dataSource.data = temparr;
  }
}

class SHTree {
  id: any;
  description: string;
  children?: SHTree[];
  checkbox?: boolean;
  active?:boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: SHTree;
}
