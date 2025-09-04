import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Store } from '@ngrx/store';
import { InstructorCompactOptions } from 'src/app/_DtoModels/Instructors/InstructorCompactOptions';
import { InstructorNavBarMenuItem } from 'src/app/_DtoModels/Instructors/InstructorNavBarMenuItem';
import { InstructorCategoryCompactOptions } from 'src/app/_DtoModels/Instructor_Category/InstructorCategoryCompactOptions';
import { InstanceService } from 'src/app/_Services/Auth/instance.service';
import { InstructorService } from 'src/app/_Services/QTD/instructor.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { Params, Router } from '@angular/router';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-instructors-navbar',
  templateUrl: './instructors-navbar.component.html',
  styleUrls: ['./instructors-navbar.component.scss']
})
export class InstructorsNavbarComponent implements OnInit {
  isLoading: boolean = false;
  filter = '';
  showActive: boolean = true;
  toFilter: InstructorNavBar[] = [];
  navList: InstructorNavBar[];
  selectedId: any;
  selectedItem: InstructorNavBar;
  isMetaCheck: boolean = false;
  newData!: any;
  treeControl = new NestedTreeControl<InstructorNavBar>(
    (node) => node.Children
  );
  dataSource = new MatTreeNestedDataSource<InstructorNavBar>();
  hasChild = (_: number, node: InstructorNavBar) =>
    !!node.Children && node.Children.length > 0;


  IconName: string;
  HasChildren: boolean = false;
  Collapsed: boolean = false;
  instructorCheck: boolean = false;
  instructorCategoryCheck: boolean = false;
  routerCheck: any;
  subscription = new SubSink();
  textSearch: string = "";

  constructor(private store: Store<{ toggle: string }>, private vcf: ViewContainerRef,
    private insService: InstructorService,
    public flyPanelService: FlyInPanelService,
    private databroadcastService: DataBroadcastService,
    private router: Router) { }

  ngOnInit(): void {
    this.createList();
    this.databroadcastService.updateMyDataNavBar.subscribe(() => this.createList());

    this.subscription.sink = this.databroadcastService.navigateOnChange.subscribe((res) => {
      this.newData = res;
      this.createList(true);
    })
  }
  toggleMainMenu() {
    this.store.dispatch(sideBarToggle());
  }
  clearFilter(){
    this.textSearch = '';
    this.createList(true);
  }
  searchFilter() {


    this.toFilter = [
      ...this.navList.map((n) => {
        return {
          ...n,
          Collapsed: true,
          Children: n.Children?.filter((c) =>
            c.Title.toLowerCase().includes(String(this.textSearch).toLowerCase()) && (c.active === this.showActive)
          ),
        };
      }),
    ];
    this.dataSource.data = this.toFilter.filter((x) => {return x.Children !== null && x.Children !== undefined && x.Children.length > 0});
    this.treeControl.dataNodes = this.dataSource.data;
    this.textSearch.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  async createList(checkSelected = false) {

    let childIndex = 1;
    this.insService.getInsCategoryWithIns().then((res: any[]) => {

      this.navList = [];
      res.forEach((data: InstructorCategoryCompactOptions, index: any) => {
        this.navList.push({
          id: data.instructor_Category.id,
          Title: data.instructor_Category.iCategoryTitle,
          active: data.instructor_Category.active,
          RoutePath: `/my-data/instructors/category-details/${data.instructor_Category.id}`,
          Children: [],
          RouteParams: data.instructor_Category.id,
          type: "cat",
          Collapsed: false
        });
        data.instructorCompactOptions.forEach((x: InstructorCompactOptions) => {
          this.navList[index]['Children']?.push({
            id: x.id,
            Title: x.number + ' - ' + x.title,
            active: x.active,
            RoutePath: `/my-data/instructors/details/${x.id}`,
            RouteParams: x.id,
            number: x.number,

            type: "ins",

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
    }).finally(() => {
    });
  }

  highlightData() {
    ;
    switch (this.newData.type) {

      case "ins":
        this.selectedItem = new InstructorNavBar();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "ins";
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.Collapsed = false;
        this.treeControl.dataNodes.forEach((data) => {
          data.Children?.forEach((ins) => {
            if (data.id === this.selectedItem.id) {
              this.selectedItem.RoutePath = `/my-data/instructors/details/${this.newData.data.id}`;
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
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.RoutePath = `/my-data/instructors/category-details/${this.newData.data.id}`;
        this.treeControl.dataNodes.forEach((data) => {
          if (data.type === this.selectedItem.type && data.id === this.selectedItem.id) {
            this.treeControl.expand(data);
            this.router.navigate([this.selectedItem.RoutePath]);
          }
        })
        break;
    }
  }

  // sortInstructorWithNumber(insCat: any, isActive: boolean) {
  //   return insCat.Children?.filter((child) => child.active === isActive)
  //     .sort((a, b) => {
  //       if (a.number === b.number) {
  //         return a.number.localeCompare(b.number);
  //       } else if (isNaN(a.number) && isNaN(b.number)) {
  //         return a.number.localeCompare(b.number);
  //       } else if (isNaN(a.number)) {
  //         return -1;
  //       } else if (isNaN(b.number)) {
  //         return 1;
  //       } else {
  //         return a.number - b.number;
  //       }
  //     });
  // }

  toggleActiveFilter(isActive: boolean) {

    this.toFilter = [
      ...this.navList.filter((data) => {
        return data.active === isActive || data.Children?.some((child) => child.active === isActive)
      }).map((insCat) => {
        return {
          ...insCat,
          Children: insCat.Children?.filter((child) => child.active === isActive && child.Title.toLowerCase().includes(String(this.textSearch).toLowerCase())),
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
  async openFlyInPanel(templateRef: any, name: string) {
    if (name === 'Category') {
      this.instructorCategoryCheck = true;
    } else if (name === 'Instructor') {
      this.instructorCheck = true;
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
