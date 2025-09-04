import { TemplatePortal } from '@angular/cdk/portal';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Params, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { SaftyHazardCompactOption } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCompactOptions';
import { SaftyHazard_CategoryCompactOptions } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_CategoryCompactOptions';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-sh-nav-bar',
  templateUrl: './sh-nav-bar.component.html',
  styleUrls: ['./sh-nav-bar.component.scss'],
})
export class ShNavBarComponent implements OnInit,OnDestroy,AfterViewInit {
  isLoading: boolean = false;
  navList: SafetyHazardNavBarMenuItem[];
  showActive: boolean = true;
  originalDataSource = new MatTreeNestedDataSource<SafetyHazardNavBarMenuItem>();
  searchText = "";
  subscription = new SubSink();
  safetyHazardCheck:boolean=false;
  safetyHazardCategoryCheck:boolean=false;
  selectedId: any;
  newData!: any;
  selectedItem: SafetyHazardNavBarMenuItem;
  treeControl = new NestedTreeControl<SafetyHazardNavBarMenuItem>(
    (node) => node.Children
  );
  dataSource = new MatTreeNestedDataSource<SafetyHazardNavBarMenuItem>();
  hasChild = (_: number, node: SafetyHazardNavBarMenuItem) =>
    !!node.Children && node.Children.length > 0;
  toFilter:SafetyHazardNavBarMenuItem[];

  constructor(
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    private store: Store,
    private shService: SafetyHazardsService,
    private dataBroadcastService : DataBroadcastService,
    private router:Router
  ) { }

  ngOnInit(): void {
    this.createList();
    this.subscription.sink = this.dataBroadcastService.updateMyDataNavBar.subscribe((res:any)=>{
      this.createList();
    })

    this.subscription.sink = this.dataBroadcastService.navigateOnChange.subscribe((res) => {
      this.newData = res;
      this.createList(true);
    })
  }

  ngAfterViewInit(): void {

  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  clearFilter(){
    this.searchText = '';
    this.createList(true);
  }

  createList(checkSelected = false) {
    this.isLoading=true;
    this.shService.getSHCategoryWithSH().then((res: SaftyHazard_CategoryCompactOptions[]) => {

      this.navList = [];
      this.originalDataSource.data = [];
      /* res.forEach((data: SaftyHazard_CategoryCompactOptions, index: any) => {
        this.navList.push({
          id: data.saftyHazard_Category.id,
          Title: data.saftyHazard_Category.number + ' - ' + data.saftyHazard_Category.title,
          active: data.saftyHazard_Category.active,
          RoutePath: `/my-data/safety-hazards/cat/${data.saftyHazard_Category.id}`,
          Children: [],
          RouteParams: data.saftyHazard_Category.id,
          type: "cat",
          Collapsed:false
        });
        data.saftyHazardCompactOptions.forEach((x: SaftyHazardCompactOption) => {
          this.navList[index]['Children']?.push({
            id: x.id,
            Title:x.number + ' - ' + x.title,
            active: x.active,
            RoutePath: `/my-data/safety-hazards/sh/${x.id}`,
            RouteParams: x.id,
            number:x.number,
            type: "sh",
            Collapsed:false
          });
        })
      }); */

      res.forEach((data: SaftyHazard_CategoryCompactOptions, index: any) => {
        this.navList.push({
          id: data.saftyHazard_Category.id,
          Title:  data.saftyHazard_Category.number + ' - ' + data.saftyHazard_Category.title,
          active: data.saftyHazard_Category.active,
          RoutePath: `/my-data/safety-hazards/cat/${data.saftyHazard_Category.id}`,
          Children: [],
          RouteParams: data.saftyHazard_Category.id,
          type: "cat",
          Collapsed:false
        });
        data.saftyHazardCompactOptions.forEach((x: SaftyHazardCompactOption) => {
          this.navList[index]['Children']?.push({
            id: x.id,
            Title:x.number + ' - ' + x.title,
            active: x.active,
            RoutePath: `/my-data/safety-hazards/sh/${x.id}`,
            RouteParams: x.id,
            number:x.number,

            type: "sh",

          });
        });
      });


      this.toFilter = Object.assign([], this.navList);
      this.dataSource.data = this.navList;
      this.originalDataSource.data = Object.assign(this.navList);
      this.treeControl.dataNodes = this.navList;
      this.toggleActiveFilter(this.showActive);
      if (checkSelected) {
        this.highlightData();
      }
    }).finally(() => {
    this.isLoading=false;

    });
  }


  highlightData() {
    ;
    switch (this.newData.type) {

      case "sh":
        this.selectedItem = new SafetyHazardNavBarMenuItem();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "sh";
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.Collapsed=false;
        this.treeControl.dataNodes.forEach((data) => {
          data.Children?.forEach((ins) => {
              if (ins.id ===  this.selectedItem.id) {
                this.selectedItem.RoutePath = `/my-data/safety-hazards/sh/${this.newData.data.id}`;
                this.treeControl.expand(data);
                this.treeControl.expand(ins);
                this.router.navigate([this.selectedItem.RoutePath]);
              }
          })
        })
        break;

        case "cat":
          this.selectedItem = new SafetyHazardNavBarMenuItem();
          this.selectedItem.id = this.newData.data.id;
          this.selectedItem.type = "cat";
          this.selectedItem.RouteParams = this.newData.data.id;
          this.selectedItem.RoutePath = `/my-data/safety-hazards/cat/${this.newData.data.id}`;
          this.treeControl.dataNodes.forEach((data) => {
            if (data.type === this.selectedItem.type && data.id === this.selectedItem.id) {
              this.treeControl.expand(data);
              this.router.navigate([this.selectedItem.RoutePath]);
            }
          })
          break;
    }
  }


  toggleMainMenu() {
    //this.DataBroadcastService.ToggleMainMenu.next('');
    this.store.dispatch(sideBarToggle());
  }

  async openFlyInPanel(templateRef: any, name:string) {
    if(name === 'sh'){
       this.safetyHazardCheck = true;
    }
    else if(name === 'shCategory'){
      this.safetyHazardCategoryCheck = true;
    }

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  toggleActiveFilter(isActive: boolean) {
    this.toFilter = [
      ...this.navList.filter(data => {
        return data.active === isActive || data.Children?.some(child => child.active === isActive)
      }).map(insCat =>{
        return {
          ...insCat,
          Children: insCat.Children?.filter((child) => {return child.active === isActive})
        }
      })]
    this.showActive = isActive;
    this.treeControl.dataNodes = this.toFilter;
    this.dataSource.data = this.toFilter;
  }

  searchFilter() {
      this.navList = [
        ...this.originalDataSource.data.filter((cat)=>{
          return cat.active === this.showActive || cat?.Children.some((s) => {return s.active === this.showActive && s.Title.trim().toLowerCase().includes(this.searchText.trim().toLowerCase())});
        }).map((shCat) => {
          return {
            ...shCat,
            Children: shCat.Children?.filter((sh) => {
              return sh.Title.trim().toLowerCase().includes(this.searchText.trim().toLowerCase()) && (sh.active === this.showActive);
            })
          }
        })
      ];

      this.dataSource.data = this.navList.filter((x) => {return x.Children !== null && x.Children !== undefined && x.Children.length > 0});
      this.treeControl.dataNodes = this.dataSource.data;
      this.searchText.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }


  private setParent(node: any, parent: any | undefined) {
    node.parent = parent;
    if (node.Children) {
      node.Children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  refresh() {
    this.showActive = true;
    this.createList();
    this.dataBroadcastService.refreshOverviewData.next(null);
  }
}

class SafetyHazardNavBarMenuItem {
  id?: any;
  Title: string;

  RoutePath: string;

  RouteParams?: Params;

  IconName?: string;
  active?: boolean;

  Children?: SafetyHazardNavBarMenuItem[] = [];
  HasChildren?: boolean;
  Collapsed?: boolean = false;
  type?: string;
  number?: any;
}


