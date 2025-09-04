import { TemplatePortal } from '@angular/cdk/portal';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ActivatedRoute, Router } from '@angular/router';
import { ToolNavBarMenuItem } from '@models/Tool/ToolNavBarMenuItem';
import { Store } from '@ngrx/store';
import { NavBarMenuItem } from 'src/app/_DtoModels/NavBarMenuItem';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-tool-navbar',
  templateUrl: './tool-navbar.component.html',
  styleUrls: ['./tool-navbar.component.scss'],
})
export class ToolNavbarComponent implements OnInit, OnDestroy {
  isLoading: boolean = false;
  navList: ToolNavBarMenuItem[];
  showActive: boolean = true;
  subscription = new SubSink();
  selectedItem: ToolNavBarMenuItem;
  showOnlySelected = false;
  filterToolString:string="";
  dataSource = new MatTreeNestedDataSource<ToolNavBarMenuItem>();
  newData: any;
  searchText: string = "";
  treeControl = new NestedTreeControl<ToolNavBarMenuItem>(
    (node) => node.Children
  );
  hasChild = (_: number, node: ToolNavBarMenuItem) =>
  !!node.Children && node.Children.length > 0;

  constructor(
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    private store: Store,
    private dataBroadcastService: DataBroadcastService,
    private toolSrvc: ToolsService,
    private router:Router,
    private route: ActivatedRoute
  ) {}

  async ngOnInit(): Promise<void> {
    this.isLoading = true;
    this.setToolCategorySelection();
    await this.createList();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.updateMyDataNavBar.subscribe((res:any)=>{
      this.isLoading = true;
      this.createList();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  filterData(filter: boolean) {
    this.isLoading = true;
    this.showActive = !!filter;
    this.createList();
  }

  async createList() {
    this.toolSrvc.getAllToolCategories(true).then((res) => {
      res.sort((a, b) => {
        const nameA = a.title.toLowerCase();
        const nameB = b.title.toLowerCase();
        if (nameA < nameB) {
          return -1;
        }
        if (nameA > nameB) {
          return 1;
        }
        return 0; // names must be equal
      });

      var filteredCategories = res
      .map(category => ({
        ...category,
        tools: category.tools.filter(tool => tool.active === this.showActive)
      }));

      if(!this.showActive){
        filteredCategories = filteredCategories.filter(r => r.tools.length !== 0);
      }
      
      this.navList = [];
      filteredCategories.forEach((data, index: any) => {
        this.navList.push({
          id: data.id,
          Collapsed: true,
          Title: data.title,
          RoutePath: `/my-data/tools/cat-detail/${data.id}`,
          RouteParams: data.id,
          Children: [],
          type: "category",
          active: data.active
        });
        data.tools.forEach((obj) => {
          this.navList[index].Children?.push({
            id: obj.id,
            Title: obj.name,
            active: obj.active,
            RoutePath: `/my-data/tools/detail/${obj.id}`,
            RouteParams: obj.id,
            type: "tool"
          });
        });
      });

      this.filterBySearchText();
    });
  }

  filterBySearchText() {
    const _searchText = this.searchText.trim().toLowerCase();

    if(!_searchText){
      this.dataSource.data = this.navList;
      this.treeControl.dataNodes = this.navList;
      this.setExpandedTreeNode();
    } else {
      let filteredNavList: ToolNavBarMenuItem[] = [];
      filteredNavList = this.navList.map((category) => {
        const filteredChildren = category.Children?.filter((c) =>
          c.Title.toLowerCase().includes(_searchText)
        );
    
        if (filteredChildren && filteredChildren.length > 0) {
          return {
            ...category,
            Collapsed: true,
            Children: filteredChildren,
          };
        }
    
        return null;
      })
      .filter((category) => category !== null);

      this.dataSource.data = filteredNavList;
      this.treeControl.dataNodes = filteredNavList;
      this.treeControl.expandAll();
    }
    this.isLoading = false;
  }

  public hideLeafNode(node: any) {
    return this.showOnlySelected && !node.selected
      ? true
      : new RegExp(this.filterToolString, 'i').test(node.Title) === false;
  }

  public hideParentNode(node: any){
    return this.treeControl
        .getDescendants(node)
        .filter(node => node.Children == null || node.Children.length === 0)
        .every(node => this.hideLeafNode(node));
  }

  toggleMainMenu() {
    this.store.dispatch(sideBarToggle());
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
  setToolCategorySelection(){
    if(this.router.url.includes('/my-data/tools/detail/')){
      let selectedToolId = this.router.url.split("/").reverse()[0];
      this.setSelectedItem(selectedToolId,"tool");
    }
    if(this.router.url.includes('/my-data/tools/cat-detail/')){
      let selectedCategoryId = this.router.url.split("/").reverse()[0];
      this.setSelectedItem(selectedCategoryId,"category");
    }
  }
  setSelectedItem(id:string,type:string){
    this.selectedItem = new ToolNavBarMenuItem(id, type);
  }
  setExpandedTreeNode(){
    if(this.selectedItem && this.selectedItem.type == 'tool' && this.navList ){
      let selectedCategory = this.navList.find(item => item.Children.some(child => child.id === this.selectedItem.id));
      if(selectedCategory != null){
        this.treeControl.expand(selectedCategory);
      }
    }
  }
}
