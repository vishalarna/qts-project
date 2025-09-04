import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { SaftyHazard_Category } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_Category';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-sh-not-linked',
  templateUrl: './fly-panel-sh-not-linked.component.html',
  styleUrls: ['./fly-panel-sh-not-linked.component.scss'],
})
export class FlyPanelShNotLinkedComponent implements OnInit {
  @Input() NotLinkedToName: any;
  @Input() activeInactiveCheck:boolean;
  showActive: boolean = true;
  spinner = false;
  treedataSource = new MatTreeNestedDataSource<TreeData>();
  tasktreeControl = new NestedTreeControl<TreeData>(
    (node: any) => node.children
  );
  hasTaskChild = (_: number, node: TreeData) =>
    !!node.children && node.children.length > 0;

  unlinkHeader = '';
  unlinkDescription = '';
  unlinkIds: any[] = [];
  srctaskList: TreeData[] = [];
  searchText = "";

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    public dialog: MatDialog,
    private shService:SafetyHazardsService,
    private alert: SweetAlertService,
    private labelPipe:LabelReplacementPipe
  ) {}

  ngOnInit(): void {


    if(this.activeInactiveCheck === true){
      this.getNames();
    }else if(this.activeInactiveCheck === false){
      this.populateData();
    }
  }

  clearFilter(){
    this.searchText = '';
    this.populateData();
  }

  async populateData(){
    let temp: TreeData[] = [];
    this.spinner = true;
    var data:SaftyHazard_Category[] = await this.shService.getNotLinked(this.NotLinkedToName).finally(()=>{
      this.spinner = false;
    });
    data=data.filter(x=>x.active===this.showActive);
    data.forEach((data: SaftyHazard_Category, index: any) => {
      temp.push({
        id: data.id,
        description:index+1 + ' - ' + data.title,
        isSafetyHazard : false,
        children: [],
      });
      data.saftyHazards.forEach((x: SaftyHazard) => {
        temp[index]['children']?.push({
          id: x.id,
          description:x.number + ' - ' + x.title,
          active:x.active,
          isSafetyHazard : true
        });
      })
    });
    this.treedataSource.data = Object.assign([],temp);
    this.srctaskList = Object.assign([],temp);
    this.toggleActiveFilter(this.showActive);
  }

  closeFlyPanel() {
    this.flyPanelSrvc.close();
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

  toggleActiveFilter(isActive: boolean) {

    this.treedataSource.data = [
      ...this.srctaskList.map((shCat) => {
        return {
          ...shCat,
          children: shCat.children?.filter((sh) => {
            return sh.active === isActive && sh.description.trim().toLowerCase().includes(this.searchText.trim().toLowerCase());
          })
        }
      })
    ];

    this.showActive = isActive;
  }

  searchFilter() {
    if (this.searchText.length > 0) {
      this.treedataSource.data = [
        ...this.srctaskList.map((shCat) => {
          return {
            ...shCat,
            children: shCat.children?.filter((sh) => {
              return sh.description.trim().toLowerCase().includes(this.searchText.trim().toLowerCase()) && sh.active === this.showActive;
            })
          }
        })
      ];

      Object.keys(  this.treedataSource.data ).forEach((key: any) => {
        this.setParent(  this.treedataSource.data [key], undefined);
      });
      this.tasktreeControl.dataNodes =   this.treedataSource.data ;
      this.searchText.length > 0 ? this.tasktreeControl.expandAll(): this.tasktreeControl.collapseAll();
    } else {
      this.treedataSource.data = [...this.srctaskList];
    }
  }

  private setParent(node: any, parent: any | undefined) {
    node.parent = parent;
    if (node.children) {
      node.children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  moduleName:string;
  async getNames(){
    switch(this.NotLinkedToName){
      case 'Catactive':
        this.moduleName = 'Active Categories';
        this.getIssusingAuthorityList();
        break;

      case 'Catinactive':
        this.moduleName = 'Inactive Categories';
        this.getIssusingAuthorityList();
        break;

      case 'Shactive':
        this.moduleName = `Active ${await this.labelPipe.transform("Safety Hazard")}s`;
        this.getSafetyHazardActiveInactiveList();

        break;

      case 'Shinactive':
        this.moduleName = `Inative ${await this.labelPipe.transform("Safety Hazard")}s`;
        this.getSafetyHazardActiveInactiveList();
        break;
    }
  }

  getSafetyHazardActiveInactiveList(){
    this.spinner = true;
    this.shService.getshList(this.NotLinkedToName).then((data)=>{
      this.treedataSource.data = Object.assign([],data);
    }).catch((error)=>{
      this.alert.errorToast('Error Fetching Categories Data');
    }).finally(()=>{
      this.spinner = false;
    });
  }

  getIssusingAuthorityList(){
    this.spinner = true;
    this.shService.getcatList(this.NotLinkedToName).then((data)=>{
      this.treedataSource.data = Object.assign([],data);
    }).catch((error)=>{
      this.alert.errorToast('Error Fetching Categories Data');
    }).finally(()=>{
      this.spinner = false;
    });
  }
}



class TreeData {
  id: any;
  description: string;
  active?:boolean;
  children?: TreeData[];
  isSafetyHazard?:boolean;
}
