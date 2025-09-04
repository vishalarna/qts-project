import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ILASafetyHazardLinkOptions } from 'src/app/_DtoModels/ILA_SafetyHazard_Link/ILA_SafetyHazard_LinkOptions';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SafetyHazardCategoryService } from 'src/app/_Services/QTD/safety-hazard-category.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { IlaSafteyHazardLinkService } from 'src/app/_Services/QTD/ila-safety-hazard-link.service';
import { Router } from '@angular/router';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { SelectionModel } from '@angular/cdk/collections';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-ila-safety-hazard',
  templateUrl: './fly-panel-ila-safety-hazard.component.html',
  styleUrls: ['./fly-panel-ila-safety-hazard.component.scss']
})
export class FlyaPanelILASafetyHazardComponent implements OnInit {
  @Output() safetyhazard = new EventEmitter<any>();
  @Input() safteyHazard_array: any[] = [];
  @Input() ILAID: any;
  linkProcs: boolean = true;
  filterProcString: any;
  showActive: boolean = true;
  addProc: boolean = false;
  linkedIds: any[] = [];
  linkedNames: any[] = [];
  treeControl = new NestedTreeControl<ProcTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ProcTree>();
  originalSource = new MatTreeNestedDataSource<ProcTree>();
  hasChild = (_: number, node: ProcTree) =>
    !!node.children && node.children.length > 0;
  myData: any[] = [];
  toFilter:any;

  sh_category_topics: ProcTree[] = [];
  sh_category: SafetyHazard_Categories[] = [];
  count_safetyhazards: number = 0;
  checked_boxes: any[] = [];
  addSaftyHazard: boolean = false;

  countCheckBoxes = 0;
  countSubCheckBoxes = 0;
  positionIds: any[] = [];
  alreadyLinked: any[] = [];

  spinner = false;

  isSafetyHazardVisible: any;
  CategorySortCheck = false;
  SubCategorySortCheck = false;
  EOTreeCheckListSelection = new SelectionModel<ProcTree>(true);
  treeData: ProcTree[] = [];
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    public safetyHazardCategoryService: SafetyHazardCategoryService,
    private safetyHazardLinkService: IlaService,
    private router: Router,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit() {
    if (this.safteyHazard_array !== undefined) {
      this.alreadyLinked = this.safteyHazard_array.
        map((res: any) => {
          return res.id;
        })

      this.linkedIds = this.alreadyLinked;
    }
    else {
      this.safteyHazard_array = [];
    }
    this.makeProcTreeDataSource();
  }

  async makeProcTreeDataSource() {
    await this.safetyHazardCategoryService.getAll().then((res) => {
      for (let index = 0; index < res.length; index++) {
        let procTree = new ProcTree();
        procTree.description = res[index].title;
        procTree.checkbox = true;
        procTree.children = [];

        res[index].saftyHazards.forEach((proc) => {
          procTree.children?.push({
            id: proc.id,
            description: proc.number + ' - ' + proc.title,
            checkbox: true,
            active: proc.active,
            selected: this.alreadyLinked.includes(proc.id),
          });
        });

        this.treeData.push(procTree);
      }

      this.dataSource.data = this.treeData;
      this.myData = Object.assign(this.treeData);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.myData[key], undefined);
      });

      this.treeControl.dataNodes = Object.assign(this.myData);
      this.dataSource.data.forEach((data) => {
        data.children.forEach((elm) => {
          if (this.alreadyLinked.includes(elm.id)) {
            this.treeControl.expand(data);
          }
          this.checkAllParents(elm);
        });
      })
    })

  }

  private setParent(node: ProcTree, parent: ProcTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  itemToggle(checked: boolean, node: ProcTree) {
    node.selected = node.checkbox ? checked : false;
    if (node.children) {
      node.children.forEach((child) => {
        this.itemToggle(checked, child);
      });
    } else {
      // Add EOIds to list
      if (node.selected) {
        this.linkedIds.push(node.id);
        this.linkedNames.push(node.description)
      }
      else {
        const index = this.linkedIds.indexOf(node.id);
        if (index > -1) {
          this.linkedIds.splice(index, 1);
          this.linkedNames.splice(index, 1);
        }
      };
    }
    this.linkedIds = [...new Set(this.linkedIds)];
    this.linkedNames = [...new Set(this.linkedNames)];
    this.checkAllParents(node);
  }

  private checkAllParents(node: ProcTree) {
    if (node.parent) {
      const descendants = this.treeControl.getDescendants(node.parent);
      node.parent.selected = descendants.every((child) => child.selected);
      node.parent.indeterminate = descendants.some((child) => child.selected);
      this.checkAllParents(node.parent);
    }
  }

  onProcChange(event: any, node: any) {
    if (event.checked) {
      this.EOTreeCheckListSelection.select(node);
    } else {
      this.EOTreeCheckListSelection.deselect(node);
    }
    this.itemToggle(event.checked, node);
  }


  //counting the number of rows for each category
  getIDCount() {
    let i = 0;
    while (i < this.sh_category.length) {
      let count1 = this.sh_category_topics.filter((item) => item.id === this.sh_category[i].id).length;
      this.sh_category[i].count = count1;
      i++;
      
    }
  }

  ///link to db
  async addSafetyHazardLink() {
    this.spinner = true;
    var linkOptions: ILASafetyHazardLinkOptions = new ILASafetyHazardLinkOptions;
    linkOptions.ILAId = this.ILAID;


    //first time when fly panel is clicked
    /*  if(this.safteyHazard_array.length === 0){ */
    this.positionIds = [];

    linkOptions.SafetyHazardIds = this.linkedIds;
    

    //link to db
    await this.safetyHazardLinkService.LinkSafetyHazard(this.ILAID, linkOptions).then(async (res) => {
      this.safetyhazard.emit(this.linkedNames);
      this.alert.successToast(`${await this.labelPipe.transform("Safety Hazard")}(s) Linked To ` + await this.labelPipe.transform('ILA'));
      this.flyPanelSrvc.close();
      return res;
    }).finally(()=>{
      this.spinner = false;
    })
  }
  //after edit is clicked for fly panel
  /*   else if(this.safteyHazard_array.length > 0){
      
      

      linkOptions.SafetyHazardIds = this.safteyHazard_array;
       */

  //unlink previous id
  /*  await this.safetyHazardLinkService.delete(this.ILAID,linkOptions).then((res)=>{
     
     return res;
   }).catch((err)=>{
     
   })

   this.positionIds=[];
   this.checked_boxes.forEach((element: any) => {
     this.positionIds.push(element.id);
   });
    */

  //link new id
  /*  linkOptions.SafetyHazardIds = this.positionIds;
   await this.safetyHazardLinkService.create(this.ILAID,linkOptions).then((res)=>{
     
     return res;
   }).catch((err)=>{
     
   })
 } */




  LinkToILA() {
    //  this.checked_boxes = this.sh_category_topics.filter(i => i.checked == true);
    // 
    this.addSafetyHazardLink();

  }

  recieveClosed() {
    this.addSaftyHazard = false;
    this.ngOnInit();
  }
  filterData(data: any, toFilter: any) {
    debugger;
    if (this.filterProcString === null || this.filterProcString.length === 0) {
      this.dataSource.data = this.treeData;
      this.dataSource.data.forEach((data) => {
        data.children.forEach((elm) => {
          if (this.alreadyLinked.includes(elm.id)) {
            this.treeControl.expand(data);
          }
          this.checkAllParents(elm);
        });
      })
      /* Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
      }); */
    } else {
      this.toFilter = [
        ...this.myData.map((d) => {
          return {
            ...d,
            children: d.children?.filter((x) =>
              x.description
                .toLowerCase()
                .includes(String(this.filterProcString).toLowerCase())
            ),
          };
        }),
      ];
      this.dataSource.data = this.toFilter.filter((x) => {return x.children !== null && x.children !== undefined && x.children.length > 0});
      this.treeControl.dataNodes = this.dataSource.data;
      this.filterProcString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
    }
    // this.dataSource.data.forEach((x: any) => {
    //   x.children = x.children.forEach((element: any) => {
    //     return element.children.includes(this.filterProcString)
    //       ? element.children
    //       : { children: {} };
    //   });
    // });


  }

}

//classes
export class SafetyHazard_Categories {
  id: any;
  description: string;
  number: number;
  count?: number;
}

/* export class SafetyHazard_SubCategory{
  id:any;
  description:any;
  category_id:any;
  number:number;
  count?:number;
}

export class SafetyHazard_Topics{
  id:any;
  description:string;
  subcategory_id:any;
  number:number;
  checked?:boolean;
}
 */
class ProcTree {
  id: any;
  description: string;
  children?: ProcTree[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: ProcTree;
  active?: boolean;

}





