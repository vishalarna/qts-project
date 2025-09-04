import { TemplatePortal } from '@angular/cdk/portal';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { id } from 'date-fns/locale';
import { CertificationCompactOptions } from 'src/app/_DtoModels/Certification/CertificationCompactOptions';
import { CertificationNavBarMenuItem } from 'src/app/_DtoModels/Certification/CertificationNavBarMenuItem';
import { CertifyingBodyCompactOptions } from 'src/app/_DtoModels/CertifyingBody/CertifyingBodyCompactOptions';
import { InstanceService } from 'src/app/_Services/Auth/instance.service';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';


@Component({
  selector: 'app-certification-navbar',
  templateUrl: './certification-navbar.component.html',
  styleUrls: ['./certification-navbar.component.scss'],
})

export class CertificationNavbarComponent implements OnInit {
  isLoading: boolean = false;
  filter = '';
  showActive: boolean = true;
  toFilter: CertificationNavBarMenuItem[] = [];
  navList: CertificationNavBarMenuItem[];
  selectedId: any;
  selectedItem: any;
  showOnlySelected = false;
  subscription = new SubSink();
  iaCheck: boolean = false;
  certCheck: boolean = false
  treeControl = new NestedTreeControl<CertificationNavBarMenuItem>(
    (node) => node.Children
  );
  dataSource = new MatTreeNestedDataSource<CertificationNavBarMenuItem>();
  hasChild = (_: number, node: CertificationNavBarMenuItem) =>
    !!node.Children && node.Children.length > 0;
  newData: any;

  constructor(
    private store: Store<{ toggle: string }>,
    private vcf: ViewContainerRef,
    private certService: CertificationService,
    public flyPanelService: FlyInPanelService,
    private databroadcastService: DataBroadcastService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.createList();
    this.databroadcastService.updateMyDataNavBar.subscribe(() =>
      this.createList()
    );

    this.subscription.sink = this.databroadcastService.navigateOnChange.subscribe((res) => {
      this.newData = res;
      this.createList(true);
    })
  }

  toggleMainMenu() {
    this.store.dispatch(sideBarToggle());
  }

  searchFilter(event: any) {

    this.toFilter = [
      ...this.navList.filter((x)=>{
        return x.active === this.showActive || x.Children?.some((child) => child.active === this.showActive)
      }).map((n) => {
        return {
          ...n,
          Collapsed: false,
          Children: n.Children?.filter((c) =>
            c.Title.toLowerCase().includes(String(this.filter).toLowerCase()) && c.active === this.showActive
          ),
        };
      }),
    ];
    this.dataSource.data = this.toFilter.filter((x) => {return x.Children !== null && x.Children !== undefined && x.Children.length > 0});
    this.treeControl.dataNodes = this.dataSource.data;
    this.filter.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  clearFilter() {
    debugger
    this.filter = '';

    this.createList(true);


  }

  async createList(checkSelected = false) {

    this.certService
      .getCertCategoryWithCert()
      .then((res: CertifyingBodyCompactOptions[]) => {


        this.navList = [];
        res.forEach((data, index: any) => {
          this.navList.push({
            id: data.certifyingBody.id,
            Title: data.certifyingBody.name,
            active: data.certifyingBody.active,
            RoutePath: `/my-data/certifications/issuingauthority/${data.certifyingBody.id}`,
            Children: [],
            RouteParams: data.certifyingBody.id,
            Collapsed: false,
            isNERCCheck: data.certifyingBody.isNERC,
            type: 'ia'
          });
          data.certificationCompactOptions.forEach((x: CertificationCompactOptions, index1) => {
            this.navList[index]['Children']?.push({
              id: x.id,
              Title: x.certAcronym + ' - ' + x.name,
              active: x.active,
              RoutePath: `/my-data/certifications/details/${x.id}`,
              RouteParams: x.id,
              type: 'cert'

            });
          });
        });
        this.toFilter = Object.assign([], this.navList);
        this.dataSource.data = this.navList;
        this.treeControl.dataNodes = this.navList;
        this.toggleActiveFilter(this.showActive);
        if (checkSelected) {
          this.highlightData();
        }
      })
      .finally(() => { });
  }

  highlightData() {
    ;
    switch (this.newData.type) {

      case "cert":

        this.selectedItem = new CertificationNavBarMenuItem();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "cert";
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.Collapsed = false;
        this.treeControl.dataNodes.forEach((data) => {
          data.Children?.forEach((ins) => {
            if (ins.id === this.selectedItem.id) {
              this.selectedItem.RoutePath = `/my-data/certifications/details/${this.newData.data.id}`;
              this.treeControl.expand(data);
              //this.treeControl.expand(ins);
              this.router.navigate([this.selectedItem.RoutePath]);
            }
          })
        })
        break;

      case "ia":
        this.selectedItem = new CertificationNavBarMenuItem();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "ia";
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.RoutePath = `/my-data/certifications/issuingauthority/${this.newData.data.id}`;
        this.treeControl.dataNodes.forEach((data) => {
          if (data.type === this.selectedItem.type && data.id === this.selectedItem.id) {
            this.treeControl.expand(data);
            this.router.navigate([this.selectedItem.RoutePath]);
          }
        })
        break;
    }
  }

  toggleActiveFilter(isActive: boolean) {

    this.toFilter = [
      ...this.navList.filter((data) => {
        return data.active === isActive || data.Children?.some((child) => child.active === isActive)
      }).map((insCat) => {
        return {
          ...insCat,
          Children: insCat.Children?.filter((child) => { return child.active === isActive && child.Title.toLowerCase().includes(String(this.filter).toLowerCase()) })
        }
      })]
    /*  ...this.navList.map((insCat) => {
       return insCat.active === isActive || insCat
       return {
         ...insCat,
         Children: insCat.Children?.filter((ins) => {
           return ins.active === isActive;
         })
       }
     })
   ]; */
    this.showActive = isActive;
    this.treeControl.dataNodes = this.toFilter;
    this.dataSource.data = this.toFilter;
  }

  /*  toggleActiveFilter(isActive: boolean) {

     this.toFilter = [
       ...this.navList.map((certCat) => {
         return {
           ...certCat,
           Children: certCat.Children?.filter((certCat) => {
             return certCat.active === isActive;
           }),
         };
       }),
     ];
     this.showActive = isActive;
   } */
  async openFlyInPanel(templateRef: any, name: string) {
    if (name === 'IA') {
      this.iaCheck = true;
    }
    else if (name === 'CERT') {
      this.certCheck = true;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  refreshNavBarData() {
    this.databroadcastService.refreshOverviewData.next(null);
    this.navList = [];
    this.createList();
  }

  ngAfterViewInit(): void {
    this.createList();
  }

  public hideLeafNode(node: any) {
    return this.showOnlySelected && !node.selected
      ? true
      : new RegExp(this.filter, 'i').test(node.Title) === false;
  }

  public hideParentNode(node: any) {
    return this.treeControl
      .getDescendants(node)
      .filter(node => node.Children == null || node.Children.length === 0)
      .every(node => this.hideLeafNode(node));
  }
}
