import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Store } from '@ngrx/store';
import { LocationCompactOptions } from 'src/app/_DtoModels/Locations/LocationCompactOptions';
import { LocationNavBarMenuItem } from 'src/app/_DtoModels/Locations/LocationNavBarMenuItem';
import { LocationCategoryCompactOptions } from 'src/app/_DtoModels/Location_Category/LocationCategoryCompactOptions';
import { InstanceService } from 'src/app/_Services/Auth/instance.service';
import { LocationService } from 'src/app/_Services/QTD/location.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';
import { Params, Router } from '@angular/router';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';

@Component({
  selector: 'app-locations-navbar',
  templateUrl: './locations-navbar.component.html',
  styleUrls: ['./locations-navbar.component.scss'],
})
export class LocationsNavbarComponent implements OnInit {
  isLoading: boolean = false;
  filter = '';
  showActive: boolean = true;
  locationCheck:boolean=false;
  locationCategoryCheck:boolean=false;
  subscription = new SubSink();
  toFilter: InstructorNavBar[] = [];
  navList: InstructorNavBar[];
  selectedId: any;
  newData!: any;
  selectedItem: InstructorNavBar;
  treeControl = new NestedTreeControl<InstructorNavBar>(
    (node) => node.Children
  );
  dataSource = new MatTreeNestedDataSource<InstructorNavBar>();
  hasChild = (_: number, node: InstructorNavBar) =>
    !!node.Children && node.Children.length > 0;

  constructor(
    private store: Store<{ toggle: string }>,
    private vcf: ViewContainerRef,
    private locService: LocationService,
    public flyPanelService: FlyInPanelService,
    private databroadcastService: DataBroadcastService,
    private router:Router
  ) {}

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

  clearFilter(){
    this.filter = null;
    this.dataSource.data = this.navList;
  }

  searchFilter(event:any){
      this.toFilter = [
        ...this.navList.map((n) => {
          return {
            ...n,
            Collapsed: false,
            Children: n.Children?.filter((c) =>
              c.Title.toLowerCase().includes(String(event).toLowerCase()) && c.active===this.showActive
            ),
          };
        }),
      ];
      this.dataSource.data = this.toFilter.filter((x) => {return x.Children !== null && x.Children !== undefined && x.Children.length > 0});
      this.treeControl.dataNodes = this.dataSource.data;
      this.filter.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  async createList(checkSelected = false) {
    this.locService
      .getLocCategoryWithLoc()
      .then((res: LocationCategoryCompactOptions[]) => {
        this.navList = [];
        res.forEach((data, index: any) => {
          this.navList.push({
            id: data.location_Category.id,
            Title: data.location_Category.locCategoryTitle,
            active: data.location_Category.active,
            RoutePath: `/my-data/locations/category-details/${data.location_Category.id}`,
            Children: [],
            RouteParams: data.location_Category.id,
            type: "cat",
            Collapsed:false
          });
          data.locationCompactOptions.sort((a: LocationCompactOptions, b: LocationCompactOptions) => {
            return a.name.localeCompare(b.name);
          });
          data.locationCompactOptions.forEach((x: LocationCompactOptions) => {
              this.navList[index]['Children']?.push({
                id: x.id,
                Title:x.name,
                active: x.active,
                RoutePath: `/my-data/locations/details/${x.id}`,
                RouteParams: x.id,
                type: "loc",
              });
          });
        });
        this.toFilter = Object.assign([], this.navList);
        this.dataSource.data = this.navList;
        this.treeControl.dataNodes = this.navList;
        this.searchFilter("");
        this. toggleActiveFilter(this.showActive);
        if (checkSelected) {
          this.highlightData();
        }
      })
      .finally(() => {});
  }

  highlightData() {
    switch (this.newData.type) {
      case "loc":
        this.selectedItem = new InstructorNavBar();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "loc";
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.Collapsed=false;
        this.treeControl.dataNodes.forEach((data) => {
          data.Children?.forEach((ins) => {
              if (data.id ===  this.selectedItem.id) {
                this.selectedItem.RoutePath = `/my-data/locations/details/${this.newData.data.id}`;
                this.treeControl.expand(data);
                this.treeControl.expand(ins);
                this.router.navigate([this.selectedItem.RoutePath]);
              }
          })
        })
        break;
        case "cat":
          this.selectedItem = new InstructorNavBar();
          this.selectedItem.id = this.newData.data.id;
          this.selectedItem.type = "cat";
          this.selectedItem.Collapsed=false;
          this.selectedItem.RouteParams = this.newData.data.id;
          this.selectedItem.RoutePath = `/my-data/locations/category-details/${this.newData.data.id}`;
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
      ...this.navList.filter(data => {
        return data.active === isActive || data.Children?.some(child => child.active === isActive)
      }).map(insCat =>{
        return {
          ...insCat,
          Children: insCat.Children?.filter((child) => {return child.active === isActive && child.Title.toLowerCase().includes(String(this.filter).toLowerCase())})
        }
      })]
    this.showActive = isActive;
    this.treeControl.dataNodes = this.toFilter;
    this.dataSource.data = this.toFilter;
   // this.createList();
  }
  async openFlyInPanel(templateRef: any,name:string) {
    if(name === 'Category'){
      this.locationCategoryCheck = true;
    }else if(name === 'Location'){
      this.locationCheck = true;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  refreshNavBarData() {
    this.navList = [];
    this.createList();
  }

  ngAfterViewInit(): void {
    this.createList();
    // this.subscription.sink =
    //   this.dataBroadcastService.updateMyDataNavBar.subscribe((res: any) => {
    //
    //     this.navList = this.navList.map((data: ProcedureNavBarMenuItem) => {
    //
    //       if (data.id === res.id) {
    //         data.active = !data.active;
    //       }
    //       return data;
    //     });
    //   });

    // this.subscription.sink =
    //   this.dataBroadcastService.updateProcedureInNavBar.subscribe(
    //     (res: any) => {
    //       this.refreshNavBarData();
    //     }
    //   );
    // this.subscription.sink = this.dataBroadcastService.updateProcedureInNavBar.subscribe((res: any) => {
    //   this.refreshNavBarData();
    // });
  }
}

export class InstructorNavBar {
  id?: any;
  Title: string;
  RoutePath: string;
  RouteParams?: Params;
  IconName?: string;
  active?: boolean;
  Children?: InstructorNavBar[] = [];
  HasChildren?: boolean;
  Collapsed?: boolean = false;
  type?: string;
  number?: any;
}
