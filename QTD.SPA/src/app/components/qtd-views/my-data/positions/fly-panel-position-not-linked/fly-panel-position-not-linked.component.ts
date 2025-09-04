import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';

import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { SaftyHazard_Category } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_Category';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-position-not-linked',
  templateUrl: './fly-panel-position-not-linked.component.html',
  styleUrls: ['./fly-panel-position-not-linked.component.scss']
})
export class FlyPanelPositionNotLinkedComponent implements OnInit {
  @Input() NotLinkedToName: any;
  @Input() activeInactiveCheck: boolean;
  showActive: boolean = true;
  spinner = false;
  treedataSource = new MatTreeNestedDataSource<TreeData>();
  tableDataSource = new MatTableDataSource<any>();
  treeControl = new NestedTreeControl<TreeData>(
    (node: any) => node.children
  );
  hasTaskChild = (_: number, node: TreeData) =>
    !!node.children && node.children.length > 0;

  unlinkHeader = '';
  unlinkDescription = '';
  unlinkIds: any[] = [];
  srctaskList: TreeData[] = [];
  searchText = "";
  position: Position[] = [];
  srcList: TreeData[] = [];
  isLoading: boolean = false;
  @ViewChild(MatSort) sort!:MatSort;
  @ViewChild(MatPaginator) paginator!:MatPaginator;
  displayColumns = ["positionNumber", "positionAbbreviation", "positionTitle"];
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    public dialog: MatDialog,
    private posService: PositionsService,
    private alert: SweetAlertService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {

    if (this.activeInactiveCheck === true) {
      this.getNames();
    } else if (this.activeInactiveCheck === false) {
      this.getUnlinkedPosition();
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async getUnlinkedPosition() {
    this.isLoading = true;
    await this.posService.getNotLinkedWith(this.NotLinkedToName).then((res) => {

      this.position = res;

      this.generateTree(res);
    })
      .finally(() => {
        this.isLoading = false;
      });
  }

  generateTree(sourceData: Position[]) {
    let tempTree: TreeData[] = [];
    let tempArr = [
      ...sourceData.map((ia) => {
        return ia.active == this.showActive;

      }),
    ];
    for (let i = 0; i < tempArr.length; i++) {
      if (tempArr[i] != null) {
        tempTree[i] = new TreeData();
        tempTree[i].id = sourceData[i].id;
        tempTree[i].description = sourceData[i].positionNumber + ' - ' + sourceData[i].positionAbbreviation + ' - ' + sourceData[i].positionTitle;
        tempTree[i].children = [];
      }
    }

    this.srcList = tempTree;
    this.treedataSource.data = tempTree;
  }

  async populateData() {

    let temp: TreeData[] = [];
    this.spinner = true;
    var data: Position[] = await this.posService.getNotLinkedWith(this.NotLinkedToName)
      .finally(() => {
        this.spinner = false;
      });
    data.forEach((data: Position, index: any) => {
      temp.push({
        id: data.id,
        description: data.positionTitle,
        children: [],
      });
    });
    this.treedataSource.data = Object.assign([], temp);
    this.srctaskList = Object.assign([], temp);
    //this.toggleActiveFilter(this.showActive);
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

  /* toggleActiveFilter(isActive: boolean) {

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
    }
    else {
      this.treedataSource.data = [...this.srctaskList];
    }
  } */
  /*  filterData(e: Event) {

     let filterString = (e.target as HTMLInputElement).value;
     this.filteredList = [
       ...this.employees.filter((x) =>
         x.person.firstName.toLowerCase().includes(String(filterString).toLowerCase()) ||
         x.person.lastName.toLowerCase().includes(String(filterString).toLowerCase())
       ),
     ];
   }
  */
  filterRecord(e: any) {
    if (e) {
      let filterString = e;
      let temparr = [
        ...this.srcList.filter((x) =>
          x.description.toLowerCase().includes(String(filterString).toLowerCase())
        ),
      ];
      /*  this.srctaskList.filter((c) =>
             c.description.toLowerCase().includes(String(e.data).toLowerCase())
           )*/

      this.treedataSource.data = temparr;
    } else {
      this.treedataSource.data = this.srcList;
    }
  }

  moduleName: string;
  async getNames() {
    switch (this.NotLinkedToName) {
      case 'Active':
        this.moduleName = 'Active ' + await this.transformTitle('Position') +'s';
        break;

      case 'Inactive':
        this.moduleName = 'Inactive ' + await this.transformTitle('Position') +'s';
        break;
    }
    this.isLoading = true;

    this.posService.getActiveInactiveList(this.NotLinkedToName).then((data) => {
      this.treedataSource.data = data;
      this.tableDataSource.data = data;
      this.isLoading = false;
      setTimeout(() => {
        this.tableDataSource.sort = this.sort;
        this.tableDataSource.paginator = this.paginator;
      }, 1)
    }).finally(() => {
      this.isLoading = false;
    });
  }

  clearSearch: string;
  clearFilter() {
    this.treedataSource.data = this.srcList;
    this.clearSearch = null;
  }
}


/* filterData(e: Event) {

  let filterString = (e.target as HTMLInputElement).value;
  this.filteredList = [
    ...this.employees.filter((x) =>
      x.person.firstName.toLowerCase().includes(String(filterString).toLowerCase()) ||
      x.person.lastName.toLowerCase().includes(String(filterString).toLowerCase())
    ),
  ];
} */




class TreeData {
  id: any;
  description: string;
  active?: boolean;
  children?: TreeData[];
}

