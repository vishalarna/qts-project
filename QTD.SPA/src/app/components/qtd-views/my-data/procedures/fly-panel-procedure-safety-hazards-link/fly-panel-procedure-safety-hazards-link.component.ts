import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute } from '@angular/router';
import { Procedure_SaftyHazard_Link } from 'src/app/_DtoModels/Procedure_SaftyHazard_Link/Procedure_SaftyHazard_Link';
import { Procedure_SaftyHazard_LinkOptions } from 'src/app/_DtoModels/Procedure_SaftyHazard_Link/Procedure_SaftyHazard_LinkOptions';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { SaftyHazardCompactOption } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCompactOptions';
import { SaftyHazard_Category } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_Category';
import { SaftyHazard_CategoryCompactOptions } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryCompactOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-procedure-safety-hazards-link',
  templateUrl: './fly-panel-procedure-safety-hazards-link.component.html',
  styleUrls: ['./fly-panel-procedure-safety-hazards-link.component.scss']
})
export class FlyPanelProcedureSafetyHazardsLinkComponent implements OnInit, OnDestroy, AfterViewInit {
  showLoader = true;
  procId = "";
  subscriptions = new SubSink();
  filterSHString: string;
  linkSHs: boolean = true;
  addSH: boolean = false;
  showActive: boolean = true;
  @Output() closed = new EventEmitter<any>();
  @Output() refreshSHLinks = new EventEmitter<any>();
  @Input() alreadyLinkedIds: any[] = [];

  linkedIds: any[] = [];
  treeControl = new NestedTreeControl<SHTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<SHTree>();
  originalSource = new MatTreeNestedDataSource<SHTree>();
  hasChild = (_: number, node: SHTree) =>
    !!node.children && node.children.length > 0;

  EOTreeCheckListSelection = new SelectionModel<SHTree>(true);
  showLinkEOLoader: boolean = false;
  constructor(
    private shService: SafetyHazardsService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private procService: ProceduresService,
    private dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.readySHsTreeData();
  }

  ngAfterViewInit(): void {
    this.subscriptions.sink = this.route.params.subscribe((res: any) => {
      this.procId = res.id;
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  clearFilter(){
    this.filterSHString = '';
    this.dataSource.data = this.originalSource.data;
  }

  async readySHsTreeData() {
    this.showLoader = true;
    await this.shService.getSHCategoryWithSH().then((res: SaftyHazard_CategoryCompactOptions[]) => {
       
      this.makeSHTreeDataSource(res);
    }).catch(async (err: any) => {
      console.error(err);
      this.alert.errorToast("Error Fetching " +await this.labelPipe.transform("Safety Hazard") + " Data " + err?.message);
    }).finally(()=>{
      this.showLoader=false;
  
        });;
  }

  makeSHTreeDataSource(res: SaftyHazard_CategoryCompactOptions[]) {
    var treeData: any = [];
   // 
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
             checkbox: !this.alreadyLinkedIds.includes(proc.id),
             active:proc.active,
           });
         });

         treeData.push(procTree);

       }


     }
  /*   data.forEach((shCat: SaftyHazard_CategoryCompactOptions, index: any) => {

        treeData.push({ description: shCat.saftyHazard_Category.title, checkbox: true,selected:false });
        treeData[index]["children"] = [];
        shCat.saftyHazardCompactOptions.forEach((sh: SaftyHazardCompactOption) => {

            treeData[index]["children"].push({ description:sh.number + ' - ' + sh.title,selected:false ,active:sh.active, checkbox: this.alreadyLinkedIds.includes(sh.id) ? false : true, id: sh.id });

        })


    }); */
    this.dataSource.data = treeData;
    Object.keys(this.dataSource.data).forEach((key: any) => {
      this.setParent(this.dataSource.data[key], undefined);
    });
    this.originalSource.data = this.dataSource.data;

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
      node.parent.selected = descendants.every((child) => { return (child.selected && child.checkbox)});
      node.parent.indeterminate = descendants.some((child) => { return (child.selected && child.checkbox)});
      this.checkAllParents(node.parent);
    }
  }

  filterData(data: any, toFilter: any) {
    if (this.filterSHString.length > 0) {
      let temparr = [
        ...this.originalSource.data.map((element) => {
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
      this.dataSource.data = this.originalSource.data;
    }
  }

  onSHChange(event: any, node: any) {
    if (event.checked) {
      this.EOTreeCheckListSelection.select(node);

      // this.linkedIds.push(node.id);
    } else {
      this.EOTreeCheckListSelection.deselect(node);
      // this.linkedIds.splice(this.linkedIds.indexOf(node.id), 1)
    }
    this.itemToggle(event.checked, node);
  }

  itemToggle(checked: boolean, node: SHTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children !== undefined) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add EOIds to list
      if (node.checkbox && node.selected) {

        this.linkedIds.push(node.id)
      }
      else {
        var index = this.linkedIds.indexOf(node.id);
        if(index > -1){
          this.linkedIds.splice(index, 1);
        }
      }

    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.checkAllParents(node);
  }

  filterActive(makeActive: boolean) {
    let temparr = [
      ...this.originalSource.data.map((element) => {
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

  filtered(node: any) {
    return node.description.includes(this.filterSHString);
  }

  linkToProcedure() {
    var options = new Procedure_SaftyHazard_Link();
    options.procedureId = this.procId;
    options.saftyHazardIds = this.linkedIds;
    this.procService.linkSafetyHazard(this.procId, options).then(async (res: any) => {
      this.alert.successToast("All Selected " + await this.transformTitle('Safety Hazard') + "s linked to " + await this.transformTitle('Procedure'));
      this.dataBroadcastService.updateProcSHLink.next(null);
      this.refreshSHLinks.emit();
    }).finally(() => {
      this.closed.emit('fp-proc-sh-link-closed');
    });
  }

  refreshData() {
    this.linkedIds = [];
    this.EOTreeCheckListSelection = new SelectionModel<SHTree>(true);
    this.readySHsTreeData();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}

class SHTree {
  id: any;
  description: string;
  children?: SHTree[];
  checkbox?: boolean;
  selected?: boolean;
  active?:boolean;
  indeterminate?: boolean;
  parent?: SHTree;
}
