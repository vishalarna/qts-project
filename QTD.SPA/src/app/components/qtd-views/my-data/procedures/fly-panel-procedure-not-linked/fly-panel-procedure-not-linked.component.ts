import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { isConstructorDeclaration } from 'typescript';

@Component({
  selector: 'app-fly-panel-add-procedure-not-linked',
  templateUrl: './fly-panel-procedure-not-linked.component.html',
  styleUrls: ['./fly-panel-procedure-not-linked.component.scss'],
})
export class FlyPanelProcedureNotLinkedComponent implements OnInit {
  @Input() NotLinkedToName: any;
  @Input() activeInactiveCheck:boolean;
  showActive: boolean = true;

  treedataSource = new MatTreeNestedDataSource<TreeData>();
  treeControl = new NestedTreeControl<TreeData>((node: any) => node.children);
  hasTaskChild = (_: number, node: TreeData) =>
    !!node.children && node.children.length > 0;

  srcList: TreeData[] = [];
  procedures: Procedure_IssuingAuthority[] = [];
  unlinkContainer: string | undefined;
  unlinkDescription = '';
  unlinkIds: any[] = [];
  spinner:boolean;
  tableDataSource = new MatTableDataSource<any>();
  displayColumns = ["id","number","name"];
  IADisplayedCols = ["id","name"];

  @ViewChild(MatSort) matSort!:MatSort;
  @ViewChild(MatPaginator) matPaginator!:MatPaginator;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    public dialog: MatDialog,
    private procSrvc: ProceduresService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
  ) {}

  ngOnInit(): void {

    if(this.activeInactiveCheck === true){
      this.getNames();
    }

    else if(this.activeInactiveCheck === false){
      this.getUnlinkedProcedures();
    }
  }

  clearSearch:string = '';
  clearFilter(){
    this.clearSearch = '';
    this.getUnlinkedProcedures();
  }

  filterActive(makeActive: boolean) {

    let temparr = [
      ...this.treedataSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.filter((c) =>
          {return  c.active === this.showActive && c.description.toLowerCase().includes(String(this.clearSearch).toLowerCase());}
          ),
        };
      }),
    ];

    /* this.treedataSource.data = temparr;
  } else {
    this.treedataSource.data = this.srcList;
  } */










   /*
    let temparr = [
      ...this.treedataSource.data.map((element) => {
        return {
          ...element,
          children: element.children?.map((e) => {
            return e.active == makeActive;
          }),
        };
      }),
    ];
    */ this.showActive = makeActive;

    this.treedataSource.data = temparr;
  }

  filterRecord(e: any) {
    debugger;
    if (this.clearSearch) {
      let temparr = [
        ...this.srcList.map((element) => {
          return {
            ...element,
            children: element.children?.filter((c) =>
              c.description.toLowerCase().includes(String(this.clearSearch).toLowerCase()) && c.active === this.showActive
            ),
          };
        }),
      ];

      this.treedataSource.data = temparr;
    } else {
      this.treedataSource.data = this.srcList;
    }
  }

  async getUnlinkedProcedures() {
    this.spinner = true;

    await this.procSrvc.getNotLinkedWith(this.NotLinkedToName).then((res) => {

      this.procedures = res;

      this.generateTree(res);
    }).finally(() => {
      this.spinner = false;
    });
  }

  generateTree(data: any[]) {
    let tempTree: TreeData[] = [];
    data=data.filter(x=>x.active===this.showActive);
    data.forEach((data: any, index: any) => {
      tempTree.push({
        id: data.id,
        description:index+1 + ' - ' + data.title,
        isProcedure : false,
        children: [],
        active:data.active
      });
      data.procedures.forEach((x: Procedure) => {
        tempTree[index]['children']?.push({
          id: x.id,
          description:x.number + ' - ' + x.title,
          active:x.active,
          isProcedure : true
        });
      })
    });
    this.treedataSource.data = Object.assign([],tempTree);
    this.srcList = Object.assign([],tempTree);
    this.filterActive(true);
   // this.srctaskList = Object.assign([],temp);
  /*   let tempArr = [
      ...sourceData.map((ia) => {
        return {
          ...ia ,
          procedures: ia.procedures.filter((p) => p.active == this.showActive),
        };
      }),
    ];

    for (let i = 0; i < tempArr.length; i++) {
      debugger;
      if (tempArr[i].procedures.length > 0) {
        tempTree[i] = new TreeData();
        tempTree[i].id = sourceData[i].id;
        tempTree[i].description = sourceData[i].title;
        tempTree[i].children = [];
        tempArr[i].procedures.forEach((p) => {
          if (tempTree[i].children) {
            tempTree[i].children?.push({
              id: p.id,
              description:p.number + ' - ' + p.title,
              active:p.active
            });
          }
        });
      }
    }
    this.srcList = tempTree;
    this.treedataSource.data = tempTree; */
  }

  closeFlyPanel() {
    this.flyPanelSrvc.close();
  }

  nameOfModule:string
  async getNames(){

    switch(this.NotLinkedToName){
      case "Activeia":
        this.nameOfModule = "Active Issuing Auhtorities";
        this.getIssuingAuthorityActiveInactiveList();
        break;

      case "Inactiveia":
        this.nameOfModule = "Inactive Issuing Auhtorities";
        this.getIssuingAuthorityActiveInactiveList();
        break;

      case "Activeproc":
        this.nameOfModule = "Active " + await this.transformTitle('Procedure') + "s";
        this.getProcedureActiveInactiveList();
        break;

      case "Inactiveproc":
        this.nameOfModule = "Inactive " + await this.transformTitle('Procedure') + "s";
        this.getProcedureActiveInactiveList();
        break;
    }

  }

  getIssuingAuthorityActiveInactiveList(){
    this.spinner = true;
    this.procSrvc.getActiveInactiveIA(this.NotLinkedToName).then((data)=>{

      this.tableDataSource.data = data.map((x)=>{
        return {
          id:x.id,
          name:x.title,
        }
      });

      this.spinner = false;

      setTimeout(()=>{
        this.tableDataSource.paginator = this.matPaginator;
        this.tableDataSource.sort = this.matSort;
      },1)
    }).finally(()=>{
      this.spinner = false;
    });
  }

  getProcedureActiveInactiveList(){
    this.spinner = true;
    this.procSrvc.getActiveInactiveProc(this.NotLinkedToName).then((data)=>{

      this.tableDataSource.data = data.map((x)=>{
        return {
          id:x.id,
          name:x.title,
          number:x.number
        }
      });
      this.spinner = false;

      setTimeout(()=>{
        this.tableDataSource.paginator = this.matPaginator;
        this.tableDataSource.sort = this.matSort;
      },1)
    }).finally(()=>{
      this.spinner = false;
    });
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}

class TreeData {
  id: any;
  description: string;
  children?: TreeData[];
  active!:boolean;
  isProcedure!:boolean;
}
