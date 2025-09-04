import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { ILA_Procedure_LinkOptions } from 'src/app/_DtoModels/ILA_Procedure_Link/ILA_Procedure_LinkOptions';
import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { Router } from '@angular/router';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { SelectionModel } from '@angular/cdk/collections';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';


@Component({
  selector: 'app-fly-panel-ila-procedures',
  templateUrl: './fly-panel-ila-procedures.component.html',
  styleUrls: ['./fly-panel-ila-procedures.component.scss'],
})
export class FlyPanelIlaProceduresComponent implements OnInit {
  @Output() procedures = new EventEmitter<any>();
  @Output() closed = new EventEmitter<any>();
  //@Input() alreadyLinked:any;
  @Input() ILAID: any;
  @Input() alreadyLinked: any[] = [];
  linkProcs: boolean = true;
  filterProcString: string;
  showActive: boolean = true;
  addProc: boolean = false;
  linkedIds: any[] = [];
  linkedNames: ProcTree[] = [];
  treeControl = new NestedTreeControl<ProcTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ProcTree>();
  hasChild = (_: number, node: ProcTree) =>
    !!node.children && node.children.length > 0;
  myData: any[] = [];
  toFilter:any;

  spinner = false;

  pre_category: any[] = [];
  pre_topics: any[] = [];
  count_ilas: number = 0;
  checked_boxes: any;
  positionIds: any[] = [];
  addProcedure: boolean = false;
  EOTreeCheckListSelection = new SelectionModel<ProcTree>(true);

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    public issuingAuthoritiesService: IssuingAuthoritiesService,
    private procedureLinkService: IlaService,
    private router: Router,
    private alert : SweetAlertService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    
    if (this.alreadyLinked===undefined || this.alreadyLinked===null) {
      this.alreadyLinked=[];
    }
    this.alreadyLinked = this.alreadyLinked?.map((x) => {
      return x.id;
    })

    this.linkedIds = this.alreadyLinked;
    this.makeProcTreeDataSource();
    /*  if(this.alreadyLinked !== undefined){
       this.alreadyLinked = this.alreadyLinked.map((data:any)=>{
         return data.id;
       })
     }
     else{
       this.alreadyLinked = [];
     } */
  }

  /* filterData() {
    if (this.filterProcString.length > 0) {
      let temparr = [
        ...this.myData.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) => {return c.description.toLowerCase().trim()
              .includes(this.filterProcString.toLowerCase().trim())}),
          };
        }),
      ];
      this.dataSource.data = temparr;
      
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
      this.filterProcString.length > 0 ? this.treeControl.expandAll(): this.treeControl.collapseAll();
    }
    else {
      this.dataSource.data = this.myData;
    }
}
 */
  async makeProcTreeDataSource() {
    await this.issuingAuthoritiesService.getAll().then((res) => {
      var treeData: any[] = [];
      for (let index = 0; index < res.length; index++) {
        let procTree = new ProcTree();
        procTree.description = res[index].title;
        procTree.checkbox = true;
        procTree.children = [];

        res[index].procedures.forEach((proc) => {
          if(proc.active){
            procTree.children?.push({
              id: proc.id,
              description: proc.title,
              checkbox: true,
              active: proc.active,
              number: proc.number,
              selected:this.alreadyLinked.includes(proc.id),
            });
          }
        });

        treeData.push(procTree);
      }

      this.dataSource.data = treeData;
      this.myData = Object.assign(treeData);
      Object.keys(this.dataSource.data).forEach((key: any) => {
        this.setParent(this.dataSource.data[key], undefined);
        this.setParent(this.myData[key], undefined);
      });
      this.treeControl.dataNodes = Object.assign(this.myData);
      this.dataSource.data.forEach((data)=>{
        data.children.forEach((elm) => {
          if(this.alreadyLinked.includes(elm.id)){
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
        this.linkedNames.push(node)
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

  searchFilter(){
    if(this.filterProcString.length > 0){
      this.toFilter = [
        ...this.myData.map((n) => {
          return {
            ...n,
            children: n.children?.filter((c) =>
              c.description.trim().toLowerCase().includes(this.filterProcString.trim().toLowerCase()) || 
            c.number.trim().toLowerCase().includes(this.filterProcString.trim().toLowerCase()),
            ),
          };
        }),
      ];
      this.dataSource.data = this.toFilter.filter((x) => {return x.children !== null && x.children !== undefined && x.children.length > 0});
      this.treeControl.dataNodes = this.dataSource.data;
      this.filterProcString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
    }
    else{
      this.makeProcTreeDataSource()
    }
  }


  /*  async readyProcedures() {
     
       await this.issuingAuthoritiesService.getAll().then((res) => {
         this.pre_category = [];
         this.pre_topics = [];
         for (var issuingAuthority of res) {
           this.pre_category.push({ id: issuingAuthority.id, description: issuingAuthority.description });

           for (var proc of issuingAuthority.procedures) {
             this.pre_topics.push({ id: proc.id, category_id: proc.issuingAuthorityId, description: proc.title });
           }
         }
         this.count_ilas = this.pre_topics.length
         this.getCategoriesCount();
         this.pre_topics.forEach((data: any) => {
           data.checked = this.edit_proc.includes(data.id);
         })
       }).catch((err) => {
         
       });
   }
  */
  getCategoriesCount() {
    let i = 0;
    while (i < this.pre_category.length) {
      let count1 = this.pre_topics.filter((item) => item.category_id === this.pre_category[i].id).length;
      this.pre_category[i].count = count1;
      i++;
      
    }
  }

  ///link to db
  async addProcedureLink() {
    this.spinner = true;
    var linkOptions: ILA_Procedure_LinkOptions = new ILA_Procedure_LinkOptions;
    linkOptions.ILAId = this.ILAID;


    //first time when fly panel is clicked
    /*  if(this.edit_proc.length === 0){ */
    /*       this.positionIds=[];
          this.checked_boxes.forEach((element: any) => {
            this.positionIds.push(element.id);
          });
           */

    linkOptions.ProcedureIds = this.linkedIds;
    /*   */

    //link to db
    await this.procedureLinkService.LinkProcedure(this.ILAID, linkOptions).then(async (res) => {
      this.procedures.emit();
      this.flyPanelSrvc.close();
      this.alert.successToast(await this.transformTitle('Procedure') + "(s) Linked To " + await this.labelPipe.transform('ILA'));
      return res;
    }).finally(()=>{
      this.spinner = false;
    })
    /*  } */
    //after edit is clicked for fly panel
    /*    else if(this.edit_proc.length > 0){
         
         

         linkOptions.ProcedureIds = this.edit_proc;
          */

    //unlink previous id
    /*   await this.procedureLinkService.delete(this.ILAID,linkOptions).then((res)=>{
        
        return res;
      }).catch((err)=>{
        
      })

      this.positionIds=[];
      this.checked_boxes.forEach((element: any) => {
        this.positionIds.push(element.id);
      });
       */

    //link new id
    /*   linkOptions.ProcedureIds = this.positionIds;
      await this.procedureLinkService.create(this.ILAID,linkOptions).then((res)=>{
        
        return res;
      }).catch((err)=>{
        
      }) */
    /* } */
  }



  LinkToILA() {
    //   this.checked_boxes = this.pre_topics.filter(i => i.checked == true);
    //  
    this.addProcedureLink();
  }

  receiveClosed() {
    this.addProcedure = false;
    this.ngOnInit();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

}

/* export class Procedures_Categories {
  id: any;
  description: string;
  count?: number;
}

export class Procedures_Topics {
  id: any;
  description: string;
  category_id: any;
  checked?: boolean;
} */

class ProcTree {
  id: any;
  description: string;
  children?: ProcTree[];
  checkbox?: boolean;
  selected?: boolean;
  indeterminate?: boolean;
  parent?: ProcTree;
  active?: boolean;
  number?: any;
}
