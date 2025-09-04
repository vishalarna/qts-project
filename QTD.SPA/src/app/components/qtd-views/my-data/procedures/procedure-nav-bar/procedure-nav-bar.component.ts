import { TemplatePortal } from '@angular/cdk/portal';
import { Router, Params } from '@angular/router';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  Output,
  ViewContainerRef,
} from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { ProcedureNavBarMenuItem } from 'src/app/_DtoModels/ProcedureNavBarMenuItem';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import {
  sideBarClose,
  sideBarOpen,
  sideBarToggle,
} from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';
import { Procedures } from '../../../implementation/ila/ila.component';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTreeNestedDataSource } from '@angular/material/tree';

@Component({
  selector: 'app-procedure-nav-bar',
  templateUrl: './procedure-nav-bar.component.html',
  styleUrls: ['./procedure-nav-bar.component.scss'],
})
export class ProcedureNavBarComponent implements OnInit, OnDestroy, AfterViewInit {
  isLoading: boolean = false;
  navList: InstructorNavBar[] = [];
  // to perform filteration on copy rather then original data
  toFilter: InstructorNavBar[] = [];
  showActive: boolean = true;
  filter = '';
  subscription = new SubSink();
  selectedId: any;
  IconName: string;
  HasChildren: boolean = false;
  Collapsed: boolean = false;
  procedureCheck: boolean = false;
  iaCheck: boolean = false;
  newData: any;
  selectedItem: InstructorNavBar;
  treeControl = new NestedTreeControl<InstructorNavBar>(
    (node) => node.Children
  );
  dataSource = new MatTreeNestedDataSource<InstructorNavBar>();
  hasChild = (_: number, node: InstructorNavBar) =>
    !!node.Children && node.Children.length > 0;

  constructor(
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    private store: Store,
    private procIAService: IssuingAuthoritiesService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router
  ) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    this.createList();
    this.subscription.sink =
      this.dataBroadcastService.updateMyDataNavBar.subscribe((res: any) => {
        this.navList = this.navList.map((data: ProcedureNavBarMenuItem) => {
          if (data.id === res.id) {
            data.active = !data.active;
          }
          return data;
        });
      });

    this.subscription.sink =
      this.dataBroadcastService.updateProcedureInNavBar.subscribe(
        (res: any) => {
          this.refreshNavBarData();
        }
      );

    this.subscription.sink = this.dataBroadcastService.navigateOnChange.subscribe((res) => {
      this.newData = res;
      this.createList(true);
    })
    /*   this.subscription.sink = this.dataBroadcastService.updateProcedureInNavBar.subscribe((res: any) => {

        this.refreshNavBarData();
      }); */
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  clearFilter(){

    this.filter = '';
    this.createList(true);
  }

  async createList(checkSelected = false) {
    this.isLoading = true;
    await this.procIAService
      .getAll()
      .then((res: Procedure_IssuingAuthority[]) => {
        /*  let tempArray = [
           ...res.filter((data)=>{
             return data.active === this.showActive || data.procedures?.some(child =>{
               child.active === this.showActive;
             })

           }).map((ia) => {
             return {
               ...ia,
               procedures: ia.procedures.filter(
                 (p) => p.active === this.showActive
               ),
             };
           }),
         ]; */
        this.navList = [];

        res.forEach((data: any, index: any) => {
          this.navList.push({
            id: data.id,
            /*         Collapsed: true, */
            Title: index + 1 + ' - ' + data.title,
            RoutePath: `/my-data/procedures/ia/${data.id}`,
            active: data.active,
            RouteParams: data.id,
            Children: [],
            type: "ia",
            Collapsed: false
          });
          data.procedures.sort((a, b) => {
            const numberA = a.number.toUpperCase();
            const numberB = b.number.toUpperCase();

            if (numberA < numberB) {
              return -1;
            }
            if (numberA > numberB) {
              return 1;
            }
            return 0;
          });
          data.procedures.forEach((proc: Procedure) => {
            this.navList[index].Children?.push({
              id: proc.id,
              Title: proc.number + ' - ' + proc.title,
              RoutePath: `/my-data/procedures/proc/${proc.id}`,
              selected: false,
              active: proc.active,
              type: "proc",
              Collapsed: false

            });
          });
        });
        this.toFilter = this.navList;
        this.dataSource.data = this.navList;
        this.treeControl.dataNodes = this.navList;
        this.toggleActiveFilter(this.showActive);
        if (checkSelected) {
          this.highlightData();
        }

      })
      .finally(() => {
        this.isLoading = false;
      });
  }

  highlightData() {
    ;
    switch (this.newData.type) {

      case "proc":
        this.selectedItem = new InstructorNavBar();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "proc";
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.Collapsed = false;
        this.treeControl.dataNodes.forEach((data) => {
          data.Children?.forEach((ins) => {
            if (data.id === this.selectedItem.id) {
              this.selectedItem.RoutePath = `/my-data/procedures/proc/${this.newData.data.id}`;
              this.treeControl.expand(data);
              this.treeControl.expand(ins);
              this.router.navigate([this.selectedItem.RoutePath]);
            }
          })
        })
        break;

      case "ia":
        this.selectedItem = new InstructorNavBar();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "ia";
        this.selectedItem.Collapsed = false;
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.RoutePath = `/my-data/procedures/ia/${this.newData.data.id}`;
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
    this.toFilter = this.navList.filter((x) => 
      {return x.active === this.showActive || x.Children?.some(child => child.active === this.showActive && child.Title.trim().toLowerCase().includes(this.filter.trim().toLowerCase()))})
    .map(insCat => {
      const filteredChildren = insCat.Children?.filter(child => 
        child.active === isActive && 
        child.Title.trim().toLowerCase().includes(this.filter)
      ) || [];

      return {
        ...insCat,
        Children: filteredChildren
      };
    });
    
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



  toggleMainMenu() {
    //this.DataBroadcastService.ToggleMainMenu.next('');
    this.store.dispatch(sideBarToggle());
  }

  searchFilter(event: any) {
    // this.toFilter = this.navList.filter((data: ProcedureNavBarMenuItem) => {
    //   return data.Title.toLowerCase().includes(event.toLowerCase());
    // });
    this.toFilter = [
      ...this.navList.filter((x)=>{
        return x.active === this.showActive || x.Children?.some(child => child.active === this.showActive && child.Title.trim().toLowerCase().includes(this.filter.trim().toLowerCase()))
      }).map((n) => {
        return {
          ...n,
          Collapsed: true,
          Children: n.Children?.filter((c) =>
            c.Title.toLowerCase().includes(String(this.filter).toLowerCase()) && c.active === this.showActive
          ),
        };
      }),
    ];
    this.dataSource.data = this.toFilter.filter((x) => {return x.Children !== null && x.Children !== undefined && x.Children.length > 0});
    this.treeControl.dataNodes = this.dataSource.data;
    this.filter.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
    //this.dataSource.data = this.toFilter;
  }

  bulkEdit() {
    this.router.navigate(['/bulkedit']);
  }

  async openFlyInPanel(templateRef: any, name: string) {
    if (name === 'procedure') {
      this.procedureCheck = true;
    }
    else if (name === 'iaProc') {
      this.iaCheck = true;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  refreshNavBarData() {
    this.navList = [];
    this.dataBroadcastService.refreshOverviewData.next(null);
    this.createList();
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
  selected?: boolean;
}

