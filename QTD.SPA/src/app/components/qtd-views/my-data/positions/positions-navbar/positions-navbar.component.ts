import { TemplatePortal } from '@angular/cdk/portal';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, Input, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { PositionNavBarMenuItem } from 'src/app/_DtoModels/Position/PositionNavBarMenuItem';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-positions-navbar',
  templateUrl: './positions-navbar.component.html',
  styleUrls: ['./positions-navbar.component.scss']
})
export class PositionsNavbarComponent implements OnInit, AfterViewInit, OnDestroy {
  filter :string= '';
  showActive: boolean = true;
  navList: PositionNavBarMenuItem[] = [];
  toFilter: PositionNavBarMenuItem[] = [];
  originalDataSource: PositionNavBarMenuItem[];
  subscription = new SubSink();
  isNavBarLoading = false;
  positionCheck:boolean=false;
  @Input() isBulkEdit: boolean = false;

  selectedItem: PositionNavBarMenuItem;
  selectedId:any;
  newData: any;

  constructor(private vcf: ViewContainerRef,
              public flyPanelService: FlyInPanelService,
              private dataBroadcastService: DataBroadcastService,
              private positionService: PositionsService,
              private store: Store,
              private route: Router,) { }

  ngOnInit(): void {
    this.createList();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.updateMyDataNavBar.subscribe((res:any)=>{
      this.createList();
    })

    this.subscription.sink =
      this.dataBroadcastService.updatePositionInNavBar.subscribe(
        (res: any) => {
          this.refreshNavBarData();
        }
      );

      this.subscription.sink = this.dataBroadcastService.navigateOnChange.subscribe((res) => {
        this.newData = res;
        this.createList(true);
      })
  }

  bulkEdit() {
    this.route.navigate['/my-data/positions/bulkedit']
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  toggleMainMenu() {
    this.store.dispatch(sideBarToggle());
  }

  searchFilter(event: any) {

    this.toFilter = [
      ...this.navList.filter((c) =>
            c.Title.toLowerCase().includes(String(event).toLowerCase()) && c.active===this.showActive
          ),
    ];
  }

  async createList(checkSelected = false) {
    this.navList = [];
    this.isNavBarLoading = true;
    this.positionService.getAllWithoutIncludes().then((res: Position[]) => {
      this.navList = [];
      this.originalDataSource = [];
      res.sort((a, b) => a.positionNumber - b.positionNumber);
      res.forEach((data: Position, index: any) => {
        this.navList.push({
          id: data.id,
          Title:data.positionNumber + ' - ' + data.positionAbbreviation + ' - ' + data.positionTitle,
          active: data.active,
          RoutePath: `/my-data/positions/details/${data.id}`,
          RouteParams: data.id,
          type:"pos"
        });

      });

      this.toFilter = Object.assign([], this.navList);
      this.toggleActiveFilter(this.showActive);
      if (checkSelected) {
        this.highlightData();
      }
    }).finally(() => {
      this.isNavBarLoading = false;
    });
  }

  highlightData() {
    ;
    switch (this.newData.type) {

      case "pos":
        this.selectedItem = new PositionNavBarMenuItem();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "pos";
        this.selectedItem.RouteParams = this.newData.data.id;
        //this.selectedItem.Collapsed=false;

        this.toFilter.forEach((data) => {

              if (data.id ===  this.selectedItem.id) {
                this.selectedItem.RoutePath = `/my-data/positions/details/${this.newData.data.id}`;

                this.route.navigate([this.selectedItem.RoutePath]);
              }

        })
        break;
    }
  }



  toggleActiveFilter(isActive: boolean) {
    this.toFilter = [
      ...this.navList.filter((pos) => {
        return pos.active == isActive && pos.Title.toLowerCase().includes(String(this.filter).toLowerCase());
      })
    ];
    this.showActive = isActive;
  }

  async openFlyInPanel(templateRef: any) {
    this.positionCheck = true;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  refreshNavBarData() {
    this.navList = [];
    this.createList();
  }


  clearFilter(){
    this.filter = '';
    this.createList(true);

  }

}
