import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { RegulatoryRequirement } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirement';
import { RRIssuingAuthority } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthority';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-rr-not-linked',
  templateUrl: './fly-panel-rr-not-linked.component.html',
  styleUrls: ['./fly-panel-rr-not-linked.component.scss'],
})
export class FlyPanelRRNotLinkedComponent implements OnInit {
  @Input() NotLinkedToName: any;
  @Input() activeInactiveCheck;

  searchText = "";
  dataSource:any[]=[];
  showActive: boolean = true;
  treedataSource = new MatTreeNestedDataSource<TreeData>();
  originalDataSource = new MatTreeNestedDataSource<TreeData>();
  tasktreeControl = new NestedTreeControl<TreeData>(
    (node: any) => node.children
  );
  hasTaskChild = (_: number, node: TreeData) =>
    !!node.children && node.children.length > 0;

  unlinkHeader = '';
  unlinkDescription = '';
  unlinkIds: any[] = [];
  srctaskList: TreeData[] = [];
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    public dialog: MatDialog,
    private rrService: RegulatoryRequirementService,
    private alert: SweetAlertService
  ) { }

  ngOnInit(): void {


    if(this.activeInactiveCheck === true){
      this.getNames();
    }else if(this.activeInactiveCheck === false){
      this.readyNotLinkedData();
    }
   // this.filterActive(false);
  }

  clearFilter(){
    this.searchText = '';
    this.readyNotLinkedData();
  }

  async readyNotLinkedData() {
    debugger;
    this.spinner = true;
    let temp:any[] = [];
    var data:RRIssuingAuthority[] = await this.rrService.getNotLinkedWith(this.NotLinkedToName).finally(()=>{
    });

    data=data.filter(x=>x.active===this.showActive);
    data.forEach((data: RRIssuingAuthority, index: any) => {
      temp.push({
        id: data.id,
        description:index+1 + ' - ' + data.title,
        isRR : false,
        children: [],
        active:data.active
      });
      data.regulatoryRequirements.forEach((x: RegulatoryRequirement) => {
        temp[index]['children']?.push({
          id: x.id,
          description:x.number + ' - ' + x.title,
          active:x.active,
          isRR : true
        });
      })
    });
    this.dataSource=temp;
    this.treedataSource.data = Object.assign([],temp);
    this.spinner = false;
   // this.srctaskList = Object.assign([],temp);
    //this.toggleActiveFilter(this.showActive);
  /*   this.rrService.getNotLinkedWith(this.NotLinkedToName).then((res:RRIssuingAuthority[])=>{
      let tempTree:any[] = [];
      let tempArray = [
        ...res.map((ia) => {
          return {
            ...ia,
            regulatoryRequirementCompacts: ia.regulatoryRequirements
          };
        }),
      ];

      this.treedataSource.data = [];
      tempArray.forEach((data: RRIssuingAuthority, index: any) => {
        tempTree.push({
          id: data.id,
          description:data.title,
          active:data.active,
          children: [],
        });


        data.regulatoryRequirements.forEach((rr: any) => {
          tempTree[index].children?.push({
            description:rr.number + ' - ' + rr.title,
            active:rr.active,
          });
        });
      });
      this.treedataSource.data = tempTree;
      this.originalDataSource.data = tempTree;
      Object.keys(this.treedataSource.data).forEach((key: any) => {
        this.setParent(this.treedataSource.data[key], undefined);
        this.setParent(this.originalDataSource.data[key], undefined);
      });
    }) */
  }

  private setParent(node: TreeData, parent: TreeData | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  closeFlyPanel() {
    this.flyPanelSrvc.close();
  }

  searchFilter(){
    debugger;
    if(this.searchText.length > 0){

      this.treedataSource.data = [
        ...this.dataSource.map((n) => {
          return {
            ...n,
            children: n.children?.filter((c) =>
              c.description.trim().toLowerCase().includes(this.searchText.trim().toLowerCase()),
            ),
          };
        }),
      ];
      this.tasktreeControl.dataNodes = this.treedataSource.data;

      this.tasktreeControl.expandAll();
    }
    else{
      this.treedataSource.data = this.dataSource;
    }
  }

  filtered(node: any) {

  }

  unlinkItemsModal(templateRef: any, id: any) {
    this.unlinkDescription = 'You are selecting to unlink the';
    let data = this.srctaskList.map((x) => {
      return x.children?.find((y) => y.id == id)?.description;
    })[0];

    this.unlinkDescription += ' ' + data;

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  filterActive(active:boolean){

    this.treedataSource.data = [
      ...this.originalDataSource.data.map((n) => {
        return {
          ...n,
          children: n.children?.filter((c) =>
            c.active === active,
          ),
        };
      }),
    ];
    this.showActive = !!active;
  }

  removeLastWord(str: string) {
    const lastIndexOfSpace = str.lastIndexOf(' ');

    if (lastIndexOfSpace === -1) {
      return str;
    }
    return str.substring(0, lastIndexOfSpace);
  }

  getData(e: any) {
    // window.alert(e);
  }

  moduleName:string;
  spinner:boolean;
  getNames(){
    debugger;
    let temp:any[] = [];

    switch(this.NotLinkedToName){
      case 'Catactive':
        this.spinner = true;

        this.moduleName = 'Active Categories';
        this.rrService.getcatList(this.NotLinkedToName).then(data=>{
          data.forEach((category,index)=>{
            temp.push({
              id: category.id,
              title:index+1 + ' - ' + category.title,
            });
          })
          this.treedataSource.data = Object.assign([],temp);

        }).catch(err => {
          this.alert.errorToast('Error Fecthing Categories');
        }).finally(() => {
          this.spinner = false;
        });
        break;

      case 'Catinactive':
        this.spinner = true;

        this.moduleName = 'Inative Categories';
        this.rrService.getcatList(this.NotLinkedToName).then(data=>{
          data.forEach((category,index)=>{
            temp.push({
              id: category.id,
              title:index+1 + ' - ' + category.title,
            });
          })
          this.treedataSource.data = Object.assign([],temp);
        }).catch(err => {
          this.alert.errorToast('Error Fecthing Categories');
        }).finally(() => {
          this.spinner = false;
        });
        break;

      case 'Rractive':
        this.spinner = true;

        this.moduleName = 'Active Regulatory Requirements';
        this.rrService.getrrList(this.NotLinkedToName).then(data=>{
          this.treedataSource.data = Object.assign([],data);

        }).catch(err => {
          this.alert.errorToast('Error Fecthing Categories');
        }).finally(() => {
          this.spinner = false;
        });
        break;

      case 'Rrinactive':
        this.spinner = true;

        this.moduleName = 'Inactive Regulatory Requirements';
        this.rrService.getrrList(this.NotLinkedToName).then(data=>{
          this.treedataSource.data = Object.assign([],data);
        }).catch(err => {
          this.alert.errorToast('Error Fecthing Categories');
        }).finally(() => {
          this.spinner = false;
        });
        break;
    }

  }
}

class TreeData {
  id: any;
  description: string;
  children?: TreeData[];
  parent?: TreeData;
  active?:boolean;
  isRR: boolean;
}
