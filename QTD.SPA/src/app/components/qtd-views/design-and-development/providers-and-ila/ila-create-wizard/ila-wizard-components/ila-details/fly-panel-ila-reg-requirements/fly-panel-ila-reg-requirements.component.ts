import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { Component, OnInit, Output, EventEmitter, Input, AfterViewInit } from '@angular/core';
import { ILARegRequirementsLinkOptions } from 'src/app/_DtoModels/ILA_RegRequirements_Link/ILA_RegRequirements_LinkOptions';
import { IlaRegRequirementsLinkService } from 'src/app/_Services/QTD/ila-reg-requirements-link.service';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { Router } from '@angular/router';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { SelectionModel } from '@angular/cdk/collections';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-ila-regulatory-requirements',
  templateUrl: './fly-panel-ila-reg-requirements.component.html',
  styleUrls: ['./fly-panel-ila-reg-requirements.component.scss'],
})
export class FlyPanelIlaRegulatoryRequirementsComponent implements OnInit, AfterViewInit {
  @Output() regulatorty_req = new EventEmitter<any>();
  @Input() regrequirement_array: any;
  @Input() ILAID: any;
  @Input() regulatoryrequirements_edit: boolean = false;
  @Output() refreshLink = new EventEmitter<any>();
  linkProcs: boolean = true;
  filterProcString: any;
  showActive: boolean = true;
  addProc: boolean = false;
  linkedIds: any[] = [];
  linkedNames: any[] = [];
  treeControl = new NestedTreeControl<ProcTree>((node: any) => node.children);
  dataSource = new MatTreeNestedDataSource<ProcTree>();
  hasChild = (_: number, node: ProcTree) =>
    !!node.children && node.children.length > 0;
  myData: any[] = [];
  alreadyLinked: any[] = [];
  EOTreeCheckListSelection = new SelectionModel<ProcTree>(true);
  spinner = false;
  toFilter:any;
  navList:any;



  checked_boxes: any;
  sh_category_topics: ProcTree[] = [];
  sh_category: ProcTree[] = [];
  count_safetyhazards: number = 0;
  positionIds: any[] = [];
  previousPositionIds: any[] = [];
  positionIds1: any[] = [];
  addRR: boolean = false;



  constructor(
    public flyPanelSrvc: FlyInPanelService,
    public rrIssuingAuthorityService: RRIssuingAuthorityService,
    private regReqLinkService: IlaService,
    private router: Router,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit() {
    if (this.regrequirement_array !== undefined) {
      this.alreadyLinked = this.regrequirement_array.
        map((res: any) => {
          return res.id;
        })
      this.linkedIds = this.alreadyLinked;
    }
    else {
      this.regrequirement_array = [];
    }
    this.makeProcTreeDataSource();
    this.getIDCount();

    /*   if(this.ILAID !== undefined){
        
      } */

    /* this.rrIssuingAuthorityService.getAll().then((res) => {
      this.sh_category = [];
      this.sh_category_topics = [];
      for (var rrIAS of res) {
        this.sh_category.push({ id: rrIAS.id, description: rrIAS.title });
        for (var rr of rrIAS.regulatoryRequirements) {
          this.sh_category_topics.push({ id: rr.id, category_id: rr.issuingAuthorityId, description: rr.description });
        }
      }

      this.sh_category_topics.forEach((i:any)=>
      i.checked = this.regrequirement_array.includes(i.id));

      this.count_safetyhazards = this.sh_category_topics.length
      this.getIDCount();
    }).catch((err) => {
      
    }) */


  }

  ngAfterViewInit(): void {
    
  }
  async makeProcTreeDataSource() {
    this.rrIssuingAuthorityService.getAll().then((res) => {
      var treeData: any[] = [];
      for (let index = 0; index < res.length; index++) {
        let procTree = new ProcTree();
        procTree.description = res[index].title;
        procTree.checkbox = true;
        procTree.children = [];

        res[index].regulatoryRequirements.forEach((proc) => {
          procTree.children?.push({
            id: proc.id,
            description: proc.number + ' - ' + proc.title,
            checkbox: true,
            active: proc.active,
            selected: this.alreadyLinked.includes(proc.id)
          });
        });

        treeData.push(procTree);
      }

      this.dataSource.data = Object.assign(treeData);
      this.myData = Object.assign(treeData);
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

  filterData(data: any, toFilter: any) {
    debugger;
    if (this.filterProcString === null || this.filterProcString.length === 0) {
      this.dataSource.data = this.myData;
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


  }

  /**
   * This function sets the parent of a given node in a tree data structure and recursively sets the
   * parent of all its children.
   * @param {ProcTree} node - The node parameter is a ProcTree object that represents a node in a tree
   * data structure.
   * @param {ProcTree | undefined} parent - The "parent" parameter is a reference to the parent node of
   * the current node being processed in a tree data structure. It can be either a reference to a valid
   * parent node or undefined if the current node is the root of the tree.
   */
  private setParent(node: ProcTree, parent: ProcTree | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  /**
   * The function toggles the selection of a node and its children in a tree structure, updates a list
   * of selected node IDs and names, and checks all parent nodes.
   * @param {boolean} checked - A boolean value indicating whether the checkbox associated with the
   * node is checked or not.
   * @param {ProcTree} node - ProcTree - a node in a tree data structure that represents a process or a
   * group of processes.
   */
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


  //counting the number of records for each category
  getIDCount() {
    let i = 0;
    while (i < this.sh_category.length) {
      let count1 = this.sh_category_topics.filter((item) => item.id === this.sh_category[i].id).length;
      this.sh_category[i].count = count1;
      i++;
      
    }
  }

  //link to db
  async addRegRequirementLink() {
    this.spinner = true;
    var linkOptions: ILARegRequirementsLinkOptions = new ILARegRequirementsLinkOptions;
    linkOptions.ILAId = this.ILAID;

    //first time when fly panel is clicked
    /*  if(this.regrequirement_array.length === 0){ */

    linkOptions.RegulatoryRequirementIds = this.linkedIds;
    

    //link to db
    await this.regReqLinkService.LinkRegRequirement(this.ILAID, linkOptions).then(async (res) => {
      this.regulatorty_req.emit(this.linkedNames);
      this.alert.successToast(await this.labelPipe.transform('Regulatory Requirement') + "(s) Linked To " + await this.labelPipe.transform('ILA'));
      this.flyPanelSrvc.close();
      return res;
    }).finally(()=>{
      this.spinner = false;
    })
  }
  //after edit is clicked for fly panel
  /*   else if(this.regrequirement_array.length > 0){
      
      

      linkOptions.RegulatoryRequirementIds = this.regrequirement_array;
       */

  //unlink previous id
  /*   await this.regReqLinkService.delete(this.ILAID,linkOptions).then((res)=>{
      
      return res;
    }).catch((err)=>{
      
    })

    this.positionIds=[];
    this.checked_boxes.forEach((element: any) => {
      this.positionIds.push(element.id);
    });
    
*/
  //link new id
  /*   linkOptions.RegulatoryRequirementIds = this.positionIds;
    await this.regReqLinkService.create(this.ILAID,linkOptions).then((res)=>{
      
      return res;
    }).catch((err)=>{
      
    })
  }
} */

  LinkToILA() {
    // this.checked_boxes = this.sh_category_topics.filter(i => i.checked == true);
    //  

    this.addRegRequirementLink();
  }

  recieveClosed() {
    this.addRR = false;
    this.ngOnInit();
  }


}

//classes
/* export class RegulatoryRequiremnets_Categories {
  id: any;
  description: string;
  count?: number;
}

export class RegulatoryRequiremnets_Topics {
  id: any;
  category_id: any;
  description: string;
  checked?: boolean;
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
  count?: any;
}






