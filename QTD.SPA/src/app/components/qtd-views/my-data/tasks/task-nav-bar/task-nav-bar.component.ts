import { TemplatePortal } from '@angular/cdk/portal';
import { NestedTreeControl } from '@angular/cdk/tree';
import { TitleCasePipe } from '@angular/common';
import {
  Component,
  OnInit,
  ViewContainerRef,
  AfterViewInit,
  OnDestroy,
  EventEmitter,
  Output,
} from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Params, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { NavBarMenuItem } from 'src/app/_DtoModels/NavBarMenuItem';
import { DutyAreaTreeVM } from 'src/app/_DtoModels/TreeVMs/TaskTreeVM';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-nav-bar',
  templateUrl: './task-nav-bar.component.html',
  styleUrls: ['./task-nav-bar.component.scss'],
})
export class TaskNavBarComponent implements OnInit, OnDestroy, AfterViewInit {
  isLoading: boolean = false;
  navList: TaskNavBarMenuItem[];
  toFilter: TaskNavBarMenuItem[] = [];
  showActive: boolean = true;
  subscription = new SubSink();
  textSearch = '';
  selectedId: any;
  selectedItem: TaskNavBarMenuItem;
  isMetaCheck: boolean = false;
  newData!: any;

  treeControl = new NestedTreeControl<TaskNavBarMenuItem>(
    (node) => node.Children
  );
  dataSource = new MatTreeNestedDataSource<TaskNavBarMenuItem>();
  hasChild = (_: number, node: TaskNavBarMenuItem) =>
    node.Children !== null && node.Children !== undefined  && node.Children.length > 0;
  isTask = (_: number, node: TaskNavBarMenuItem) =>
    node.type === 'TASK';

  constructor(
    private vcf: ViewContainerRef,
    public flyPanelService: FlyInPanelService,
    private store: Store,
    private dataBroadcastService: DataBroadcastService,
    private dutyAreaService: DutyAreaService,
    private titleCase: TitleCasePipe,
    private router: Router,
  ) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    this.createList();
    this.subscription.sink =
      this.dataBroadcastService.updateMyDataNavBar.subscribe((res) => {
        this.createList();
      });

    this.subscription.sink = this.dataBroadcastService.navigateOnChange.subscribe((res) => {
      this.newData = res;
      this.createList(true);
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  toggleMainMenu() {
    this.store.dispatch(sideBarToggle());
  }

  clearFilter() {
    this.textSearch = '';
    this.createList(true);
  }

  async openFlyInPanel(templateRef: any, name: string) {
    if (name === 'Task') {
      this.isMetaCheck = true;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  textFilter(event: any) {

    this.dataSource.data = [
      ...this.toFilter.filter((f) => {
        return f.active === this.showActive || f.Children?.some((s) => {
          return s.active === this.showActive || s.Children?.some((x) => {
            return x.active === this.showActive;
          });
        })
      })?.map((n) => {
        return {
          ...n,
          Collapsed: true,
          Children: n.Children?.filter((f) => {
            return f.active === this.showActive || f.Children?.some((k) => {
              return k.active === this.showActive;
            })
          })?.map((s) => {
            return {
              ...s,
              Collapsed: true,
              Children: s.Children?.filter((c) => {
                return (
                  c.Title.toLowerCase()
                    .trim()
                    .includes(String(this.textSearch).toLowerCase().trim()) &&
                  c.active === this.showActive
                );
              }),
            };
          }),
        };
      }),
    ];

    this.treeControl.dataNodes = this.dataSource.data;
    this.textSearch.length > 0
      ? this.treeControl.expandAll()
      : this.treeControl.collapseAll();


  }

  filterData(filter: boolean) {
    this.showActive = !!filter;
    this.textFilter(this.textSearch);
  }

  async createList(checkSelected = false) {
    await this.dutyAreaService.getMinimizedDataForTree().then((res) => {

      let tempArray: DutyAreaTreeVM[] = [
        ...res.map((da) => {
          return {
            ...da,
            subdutyAreas: da.subdutyAreas.map((sda) => {
              return {
                ...sda,
                tasks: sda.tasks,
              };
            }),
          };
        }),
      ];
      this.navList = [];
      tempArray.forEach((data, index: any) => {
        this.navList.push({
          id: data.id,
          Collapsed: true,
          Title: `${data.letter} ${data.number}. ${this.titleCase.transform(
            data.title
          )} `,
          RoutePath: `/my-data/tasks/da/${data.id}`,
          RouteParams: data.id,
          Children: [],
          type: "DA",
          letter: data.letter,
          number: data.number,
          active: data.active,
        });
        data.subdutyAreas.forEach((obj, i) => {
          this.navList[index].Children?.push({
            id: obj.id,
            Title:
              data.letter +
              ' ' +
              data.number +
              '.' +
              obj.subNumber +
              ' ' +
              obj.title,
            RoutePath: `/my-data/tasks/sda/${obj.id}-${data.letter + '_' + data.number
              }`,
            Children: [],
            number: obj.subNumber,
            letter: data.letter,
            type: "SDA",
            active: obj.active,
          });
          if (obj.tasks.length > 0) {
            let sda = this.navList[index].Children;

            obj.tasks.forEach((t) => {
              if (sda) {
                sda[i].Children?.push({
                  Title:
                    data.letter +
                    data.number +
                    '.' +
                    obj.subNumber +
                    '.' +
                    t.number +
                    ' ' +
                    t.description,
                  RoutePath: `/my-data/tasks/detail/${t.id}-${data.letter +
                    '_' +
                    data.number +
                    '.' +
                    obj.subNumber +
                    '.' +
                    t.number
                    }`,
                  IsMeta: t.isMeta,
                  IsRR: t.isReliability,
                  active: t.active,
                  id: t.id,
                  number: t.number,
                  letter: data.letter,
                  type: "TASK",
                });
              }
            });
          }
        });
      });
      this.toFilter = Object.assign([], this.navList);
      
      this.dataSource.data = this.toFilter;
      this.treeControl.dataNodes = this.toFilter;
      this.textFilter('');
      if (checkSelected) {
        this.checkSelectedData();
      }
    });
  }

  async checkSelectedData() {
    await this.createList();
    switch (this.newData.type) {
      case "DA":
        this.selectedItem = new TaskNavBarMenuItem();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "DA";
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.RoutePath = `/my-data/tasks/da/${this.newData.data.id}`;
        this.treeControl.dataNodes.forEach((data) => {
          if (data.type === this.selectedItem.type && data.id === this.selectedItem.id) {
            this.treeControl.expand(data);
            this.router.navigate([this.selectedItem.RoutePath]);
          }
        })
        // this.router.navigate([this.selectedItem.RoutePath]);
        break;
      case "SDA":
        this.selectedItem = new TaskNavBarMenuItem();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "SDA";
        this.selectedItem.RouteParams = this.newData.data.id;

        this.treeControl.dataNodes.forEach((data) => {
          data.Children?.forEach((sda) => {
            if (sda.id === this.newData.data.id) {
              this.selectedItem.RoutePath = `/my-data/tasks/sda/${this.newData.data.id}-${data.letter + '_' + data.number}`;
              this.treeControl.expand(data);
              this.treeControl.expand(sda);
              this.router.navigate([this.selectedItem.RoutePath]);
            }
          })
        })

        break;
      case "TASK":
        this.selectedItem = new TaskNavBarMenuItem();
        this.selectedItem.id = this.newData.data.id;
        this.selectedItem.type = "TASK";
        this.selectedItem.RouteParams = this.newData.data.id;
        this.treeControl.dataNodes.forEach((data) => {
          data.Children?.forEach((sda) => {
            sda.Children?.forEach((da) => {
              if (da.id === this.newData.data.id) {
                this.selectedItem.RoutePath = `/my-data/tasks/detail/${this.newData.data.id}-${data.letter + '_' + data.number + '.' + sda.number + '.' + this.newData.data.number}`;
                this.treeControl.expand(data);
                this.treeControl.expand(sda);
                this.treeControl.expand(da);
                this.router.navigate([this.selectedItem.RoutePath]);
              }
            })
          })
        })
        break;
    }

  }

  emitLetter(data: any) {

  }

  testClick(id: any) {
    this.selectedItem = id;

  }

  itemClicked(node: TaskNavBarMenuItem) {
    this.selectedItem = node;
    this.router.navigate([node.RoutePath]);
  }
}

export class TaskNavBarMenuItem {
  id?: any;
  Title: string;

  RoutePath: string;

  RouteParams?: Params;

  IconName?: string;
  active?: boolean;

  Children?: TaskNavBarMenuItem[] = [];
  HasChildren?: boolean;
  Collapsed?: boolean = false;
  IsMeta?: boolean = false;
  IsRR?: boolean = false;
  type?: string;
  letter?: string;
  number?: any;
}
