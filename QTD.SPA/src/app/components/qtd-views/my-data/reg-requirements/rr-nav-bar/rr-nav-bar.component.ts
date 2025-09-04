import { TemplatePortal } from '@angular/cdk/portal';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Params, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { RegulatoryRequirementsCompact } from 'src/app/_DtoModels/RegulatoryRequirements/RegulatoryRequirementsCompact';
import { RR_IssuingAuthorityCompact } from 'src/app/_DtoModels/RR_IssuingAuthority/RR_IssuingAuthorityCompact';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { RRIssuingAuthorityService } from 'src/app/_Services/QTD/rr-issuing-authority.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-rr-nav-bar',
  templateUrl: './rr-nav-bar.component.html',
  styleUrls: ['./rr-nav-bar.component.scss'],
})
export class RRNavBarComponent implements OnInit, AfterViewInit,OnDestroy {
  isLoading: boolean = false;
  navList: RegulatoryRequirementNavBarMenuItem[];
  toFilter: RegulatoryRequirementNavBarMenuItem[] = [];
  showActive: boolean = true;
  subscription = new SubSink();
  selectedId:any;
  rrCheck:boolean=false;
  iaRRCheck:boolean=false;
  newData: any;
  selectedItem: RegulatoryRequirementNavBarMenuItem;
  treeControl = new NestedTreeControl<RegulatoryRequirementNavBarMenuItem>(
    (node) => node.Children
  );
  dataSource = new MatTreeNestedDataSource<RegulatoryRequirementNavBarMenuItem>();
  hasChild = (_: number, node: RegulatoryRequirementNavBarMenuItem) =>
    !!node.Children && node.Children.length > 0;
  showOnlySelected = false;
  filterRRString:string="";

  constructor(
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    private store: Store,
    private rrIAService:RRIssuingAuthorityService,
    private dataBroadcastService:DataBroadcastService,
    private router:Router
  ) {}

  ngOnInit(): void {
    this.createList();

  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.updateMyDataNavBar.subscribe((res:any)=>{
      this.createList();
    });

    this.subscription.sink = this.dataBroadcastService.updateRRIA.subscribe((res:any)=>{
      if(res === undefined){
        this.createList();
      }
    });

    this.subscription.sink = this.dataBroadcastService.updateRR.subscribe((res:any)=>{
      this.createList();
    })

    this.subscription.sink = this.dataBroadcastService.navigateOnChange.subscribe((res) => {
      this.newData = res;
      this.createList(true);
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  clearFilter(){
    this.filterRRString = '';
    this.dataSource.data = this.navList
    this.toggleActiveFilter(this.showActive);
  }

  filterData(filter:boolean){
    this.showActive = filter;
    this.createList();

  }

  async createList(checkSelected = false) {
    await this.rrIAService.GetRRWithIA().then((res:RR_IssuingAuthorityCompact[])=>{


      this.navList = [];
      res.forEach((data: RR_IssuingAuthorityCompact, index: any) => {
        this.navList.push({
          id: data.id,
          Collapsed: true,
          Title: data.title,
          RoutePath: `/my-data/reg-requirements/ia/${data.id}`,
          RouteParams: data.id,
          Children: [],
          type: "iarr",
          active:data.active
        });

        data.regulatoryRequirementCompacts.forEach((x: RegulatoryRequirementsCompact) => {

          this.navList[index].Children?.push({
            id: x.id,
            Title:x.number + ' - ' + x.title,
            active: x.active,
            RoutePath: `/my-data/reg-requirements/rr/${x.id}`,
            RouteParams: x.id,
            type: "rr",
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

  }

  toggleActiveFilter(isActive: boolean) {
    this.toFilter = [
      ...this.navList.filter(data => {
        return data.active === isActive || data.Children?.some(child => child.active === isActive)
      }).map(insCat =>{
        return {
          ...insCat,
          Children: insCat.Children?.filter((child) => {return child.active === isActive && child.Title.toLowerCase().includes(String(this.filterRRString).toLowerCase())})
        }
      })]
    this.showActive = isActive;
    this.treeControl.dataNodes = this.toFilter;
    this.dataSource.data = this.toFilter;
   // this.createList();
  }

  highlightData() {
    ;
    switch (this.newData.type) {

      case "rr":
        this.selectedItem = new RegulatoryRequirementNavBarMenuItem();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "rr";
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.Collapsed=false;
        this.treeControl.dataNodes.forEach((data) => {
          data.Children?.forEach((ins) => {
              if (ins.id ===  this.selectedItem.id) {
                this.selectedItem.RoutePath = `/my-data/reg-requirements/rr/${this.newData.data.id}`;
                this.treeControl.expand(data);
                //this.treeControl.expand(ins);
                this.router.navigate([this.selectedItem.RoutePath]);
              }
          })
        })
        break;

        case "iarr":
          this.selectedItem = new RegulatoryRequirementNavBarMenuItem();
          this.selectedItem.id = this.newData.data.id;
          this.selectedItem.type = "iarr";
          this.selectedItem.Collapsed=false;
          this.selectedItem.RouteParams = this.newData.data.id;
          this.selectedItem.RoutePath = `/my-data/reg-requirements/ia/${this.newData.data.id}`;
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
    if(name === 'iaRR'){
      this.iaRRCheck=true;
    }else if(name === 'RR'){
      this.rrCheck = true;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  textFilter(event:any){
    this.toFilter = [
      ...this.navList.map((n) => {
        return {
          ...n,
          Collapsed: true,
          Children: n.Children?.filter((c) =>
            c.Title.toLowerCase().includes(String(event).toLowerCase()) && c.active===this.showActive
          ),
        };
      }),
    ];

    this.dataSource.data = this.toFilter.filter((x) => {return x.Children !== null && x.Children !== undefined && x.Children.length > 0});
    this.treeControl.dataNodes = this.dataSource.data;
    this.filterRRString.length > 0 ? this.treeControl.expandAll() : this.treeControl.collapseAll();
  }

  private setParent(node: RegulatoryRequirementNavBarMenuItem, parent: RegulatoryRequirementNavBarMenuItem | undefined) {
    node.parent = parent;
    if (node.Children) {
      node.Children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

  public hideLeafNode(node: any) {
    return this.showOnlySelected && !node.selected
      ? true
      : new RegExp(this.filterRRString, 'i').test(node.Title) === false;
  }

  public hideParentNode(node: any){
    return this.treeControl
        .getDescendants(node)
        .filter(node => node.Children == null || node.Children.length === 0)
        .every(node => this.hideLeafNode(node));
  }




  refreshData(){
    this.dataBroadcastService.refreshOverviewData.next(null);
    this.createList();
  }
}

class RegulatoryRequirementNavBarMenuItem {
  id?: any;
  Title: string;

  RoutePath: string;

  RouteParams?: Params;

  IconName?: string;
  active?: boolean;

  Children?: RegulatoryRequirementNavBarMenuItem[] = [];
  HasChildren?: boolean;
  Collapsed?: boolean = false;
  type?: string;
  number?: any;
  parent?: RegulatoryRequirementNavBarMenuItem;
}
