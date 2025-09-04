import { TemplatePortal } from '@angular/cdk/portal';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, OnInit, ViewContainerRef, AfterViewInit, OnDestroy, Output, EventEmitter } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { Title } from '@angular/platform-browser';
import { Params, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { NavBarMenuItem } from 'src/app/_DtoModels/NavBarMenuItem';
import { EOCatTreeVM, EOTreeVM } from 'src/app/_DtoModels/TreeVMs/EOTreeVM';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { sideBarToggle } from 'src/app/_Statemanagement/action/state.menutoggle';
import { SubSink } from 'subsink';


@Component({
  selector: 'app-enabling-objectives-navbar',
  templateUrl: './enabling-objectives-navbar.component.html',
  styleUrls: ['./enabling-objectives-navbar.component.scss']
})
export class EnablingObjectivesNavbarComponent implements OnInit, AfterViewInit, OnDestroy {
  @Output() urlEmitter = new EventEmitter<any>();
  baseUrl = "My Data / Enabling Objectives / ";
  modifiedUrl = this.baseUrl;
  showActive = true;
  filter = "";
  isLoading = false;
  navList: EONavBarMenuItem[];
  selectedItem!: EONavBarMenuItem;
  isMetaEoCheck: boolean = false;
  tempItem: EONavBarMenuItem[] = [];
  hasSearchWord = false;
  newData!:any;

  notTopicEOs = 0;

  subscription = new SubSink();

  treeControl = new NestedTreeControl<EONavBarMenuItem>(
    (node) => node.Children
  );
  dataSource = new MatTreeNestedDataSource<EONavBarMenuItem>();
  originalDatasource = new MatTreeNestedDataSource<EONavBarMenuItem>();
  hasChild = (_: number, node: EONavBarMenuItem) =>
    !!node.Children && node.Children.length > 0;

  constructor(
    private store: Store,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private eoService: EnablingObjectivesService,
    private dataBroadcastService: DataBroadcastService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.createList();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.updateMyDataNavBar.subscribe((_) => {
      this.createList();
    });

    // this.subscription.sink = this.dataBroadcastService.eoDeleted.subscribe((_)=>{
    //   this.router.navigate(['my-data/enabling-objectives/overview']);
    // })

    this.subscription.sink = this.dataBroadcastService.navigateOnChange.subscribe((res: any) => {
      this.newData = res;
      this.createList(true);
    })

    this.subscription.sink = this.dataBroadcastService.refreshStats.subscribe((_) => {
      this.createList();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  toggleMainMenu() {
    //this.DataBroadcastService.ToggleMainMenu.next('');
    this.store.dispatch(sideBarToggle());
  }

  clearFilter(){
    this.filter = '';
    this.textSearchCheck = false;

    this.filterData(this.showActive);
  }

  async createList(shouldSelect = false) {
    this.isLoading = true;
    await this.eoService.getMinimizedForTree().then((data: EOCatTreeVM[]) => {
      this.makeEOTreeDataSource(data);
      if(shouldSelect){
        this.makeSelectionForNewData();
      }
    }).finally(()=>{
      this.isLoading = false;
    })
  }

  makeSelectionForNewData(){
    switch(this.newData.type){
      case "EO":
        this.selectedItem = new EONavBarMenuItem();
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.RoutePath = `/my-data/enabling-objectives/eo/${this.newData.data.number}.-${this.newData.data.id}`;
        this.selectedItem.type = "EO";
        this.selectedItem.id = this.newData.data.id;
        this.router.navigate([this.selectedItem.RoutePath]);
        this.urlEmitter.emit(this.baseUrl + this.newData.data['number'] + ". " + this.newData.data['description']);
        this.treeControl.dataNodes.forEach((node)=>{
          node.Children.forEach((subCat)=>{
            subCat.Children.forEach((eo)=>{
              if(eo.IsEO && eo.id === this.newData.data.id){
                this.treeControl.expand(node);
                this.treeControl.expand(subCat);
              }
              else{
                eo.Children.forEach((lastEO)=>{
                  if(lastEO.id === this.newData.data.id){
                    this.treeControl.expand(node);
                    this.treeControl.expand(subCat);
                    this.treeControl.expand(eo);
                  }
                })
              }
            })
          })
        })
        break;
      case "CAT":
        this.selectedItem = new EONavBarMenuItem();
        this.selectedItem.IsEO = false;
        this.selectedItem.IsMeta = false;
        this.selectedItem.IsRR = false;
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.RoutePath = `/my-data/enabling-objectives/category/${this.newData.data.id}`;
        this.selectedItem.type = "CAT";
        this.selectedItem.id = this.newData.data.id;
        this.router.navigate([this.selectedItem.RoutePath]);
        this.urlEmitter.emit(this.baseUrl + this.newData.data['number'] + ". " + this.newData.data['title']);
        break;
      case "SUB":
        this.selectedItem = new EONavBarMenuItem();
        this.selectedItem.IsEO = false;
        this.selectedItem.IsMeta = false;
        this.selectedItem.IsRR = false;
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.RoutePath = `/my-data/enabling-objectives/sub-category/${this.newData.data.enablingObjectives_Category['number']}.-${this.newData.data.id}`;
        this.selectedItem.type = "SUBCAT";
        this.selectedItem.id = this.newData.data.id;
        this.router.navigate([this.selectedItem.RoutePath]);
        this.urlEmitter.emit(this.baseUrl +`${this.newData.data.enablingObjectives_Category['number']}.${this.newData.data['number']} ` + this.newData.data['title']);
        this.treeControl.dataNodes.forEach((node)=>{
          node.Children.forEach((subCat)=>{
           if(subCat.id === this.newData.data.id){
              this.treeControl.expand(node);
           }
          })
        })
        break;
      case "TOPIC":
        this.selectedItem = new EONavBarMenuItem();
        this.selectedItem.IsEO = false;
        this.selectedItem.IsMeta = false;
        this.selectedItem.IsRR = false;
        this.selectedItem.RouteParams = this.newData.data.id;
        this.selectedItem.RoutePath = `/my-data/enabling-objectives/topic/${this.newData.data.enablingObjectives_SubCategory.enablingObjectives_Category['number']}.${this.newData.data.enablingObjectives_SubCategory['number']}.-${this.newData.data.id}`;
        this.selectedItem.type = "TOPIC";
        this.selectedItem.id = this.newData.data.id;
        this.router.navigate([this.selectedItem.RoutePath]);
        this.urlEmitter.emit(this.baseUrl +`${this.newData.data.enablingObjectives_SubCategory.enablingObjectives_Category['number']}.${this.newData.data.enablingObjectives_SubCategory['number']}.${this.newData.data['number']} ${this.newData.data['title']}`);
        this.treeControl.dataNodes.forEach((node)=>{
          node.Children.forEach((subCat)=>{
            subCat.Children.forEach((eo)=>{
              if(eo.id === this.newData.data.id){
                this.treeControl.expand(node);
                this.treeControl.expand(subCat);
              }
            })
          })
        })

        break;
    }
  }

  makeEOTreeDataSource(res: EOCatTreeVM[]) {
    this.notTopicEOs = 0;
    if (res.length == 0) {
      this.dataSource.data = [];
    } else {
      this.isLoading = true;
      var treeData: EONavBarMenuItem[] = [];


      // for (var i in res) {

      //   treeData[i] = Object.assign({}, {
      //     id: res[i]['id'],
      //     Title: res[i]['number'] + ". " + res[i]['description'],
      //     Children: Object.assign([], res[i]['enablingObjective_SubCategories']),
      //     Collapsed: true,
      //     RoutePath: `/my-data/enabling-objectives/cat/${res[i].id}`,
      //     RouteParams: res[i].id,
      //   });
      //   for (var data1 in treeData[i]['children']) {
      //     treeData[i]['children'][data1] = Object.assign({}, {
      //       id: res[i]['enablingObjective_SubCategories'][data1]['id'],
      //       Title: `${res[i]['number']}.${res[i]['enablingObjective_SubCategories'][data1]['number']} ` + res[i]['enablingObjective_SubCategories'][data1]['description'],
      //       Children: Object.assign([], res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics']),
      //       Collapsed: true,
      //       RoutePath: `/my-data/enabling-objectives/subCat/${res[i]['enablingObjective_SubCategories'][data1].id}`,
      //       RouteParams: res[i]['enablingObjective_SubCategories'][data1].id,
      //     });
      //     for (var data2 in treeData[i]['children'][data1]['children']) {
      //       treeData[i]['children'][data1]['children'][data2] = Object.assign({}, {
      //         id: res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2]['id'],
      //         Title: `${res[i]['number']}.${res[i]['enablingObjective_SubCategories'][data1]['number']}.${res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2]['number']} ` + res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2]['description'],
      //         Children: Object.assign([], res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2]['enablingObjectives']),
      //         Collapsed: true,
      //         RoutePath: `/my-data/enabling-objectives/topic/${res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2].id}`,
      //         RouteParams: res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2].id,
      //       });
      //       for (var data3 in treeData[i]['children'][data1]['children'][data2]['children']) {

      //         treeData[i]['children'][data1]['children'][data2]['children'][data3] = Object.assign({}, {
      //           id: res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2]['enablingObjectives'][data3].id,
      //           Title: res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2]['enablingObjectives'][data3]['number'] + " " + res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2]['enablingObjectives'][data3]['description'],
      //           RoutePath: `/my-data/enabling-objectives/eo/${res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2]['enablingObjectives'][data3].id}`,
      //           RouteParams: res[i]['enablingObjective_SubCategories'][data1]['enablingObjective_Topics'][data2]['enablingObjectives'][data3].id,
      //         })

      //       }
      //     }
      //   }
      // }

      res.forEach((cat, i) => {
        treeData.push({
          Children: [],
          Title: cat.number + ". " + cat.title,
          RoutePath: `/my-data/enabling-objectives/category/${cat.id}`,
          Collapsed: true,
          number:cat.number,
          RouteParams: cat.id,
          id: cat.id,
          IsEO: false,
          type: "CAT",
          active:cat.active,
        })
        cat.enablingObjective_SubCategories.forEach((subCat, j) => {
          treeData[i].Children?.push({
            Children: [],
            Title: `${cat.number}.${subCat.number} ` + subCat.title,
            RoutePath: `/my-data/enabling-objectives/sub-category/${cat.number}.-${subCat.id}`,
            RouteParams: subCat.id,
            id: subCat.id,
            IsEO: false,
            number:subCat.number,
            type: "SUBCAT",
            active:subCat.active,
          });
          if (subCat.enablingObjectives) {
            subCat.enablingObjectives.forEach((eo:EOTreeVM) => {
              treeData[i].Children[j].Children?.push({
                Children: [],
                Title:eo.number.includes('.') ? `${eo.number} ${eo.description}`:`${cat.number}.${subCat.number}.0.${eo.number} ${eo.description}`,
                RouteParams: eo.id,
                RoutePath: `/my-data/enabling-objectives/eo/${eo.number}.-${eo.id}`,
                id: eo.id,
                active: eo.active,
                IsMeta: eo.isMetaEO,
                IsEO: true,
                number:eo.number,
                isSkill: eo.isSkillQualification,
                type: "EO"
              })
              this.notTopicEOs++;
            });
          }
          subCat.enablingObjective_Topics.forEach((topic, k) => {
            treeData[i].Children[j].Children?.push({
              Children: [],
              Title: `${cat.number}.${subCat.number}.${topic.number} ${topic.title}`,
              RoutePath: `/my-data/enabling-objectives/topic/${cat.number}.${subCat.number}.-${topic.id}`,
              RouteParams: topic.id,
              id: topic.id,
              IsEO: false,
              type: "TOPIC",
              active:topic.active,
            });
            topic.enablingObjectives.forEach((eo:EOTreeVM, l) => {
              treeData[i].Children[j].Children[k + this.notTopicEOs].Children.push({
                Children: [],
                Title: eo.number.includes('.') ? `${eo.number} ${eo.description}`:`${cat.number}.${subCat.number}.${topic.number}.${eo.number} ${eo.description}`,
                RoutePath: `/my-data/enabling-objectives/eo/${eo.number}.-${eo.id}`,
                RouteParams: eo.id,
                id: eo.id,
                active: eo.active,
                IsMeta: eo.isMetaEO,
                IsEO: true,
                number:eo.number,
                isSkill: eo.isSkillQualification,
                type: "EO"
              })
            });
          });
          this.notTopicEOs = 0;
        });
      })


      this.treeControl.dataNodes = Object.assign([], treeData);
      this.dataSource.data = Object.assign([], treeData);
      this.originalDatasource.data = Object.assign([], treeData);
      this.filterData(this.showActive);
      this.isLoading = false;
    }
  }

  textSearchCheck:boolean=false;
  filterData(filterActive: boolean) {
    this.showActive = filterActive;
    let filterArray:any;
    let temparr = [
      ...this.originalDatasource.data.filter((x)=>{
        return x.active === this.showActive || x.Children?.some((s)=>{
          return s.active === this.showActive || s.Children?.some((y)=>{
            return y.active === this.showActive || y.Children.some((z)=>{
              return z.active === this.showActive;
            });
          })
        })
      }).map((element) => {
        return {
          ...element,
          Children: element.Children?.filter((r)=>{
            return r.active === this.showActive || r.Children?.some((s)=>{
              return s.active === this.showActive || s.Children.some((y)=>{
                return y.active === this.showActive;
              });
            })
          })?.map((e) => {
            return {
              ...e,
              Children: e.Children?.filter((s)=>{
                return s.active === this.showActive || s.Children.some((k)=>{
                  return k.active === this.showActive;
                });
              })?.map((c) => {
                if (c.IsEO) {
                  if (c.Title.toLowerCase().match(String(this.filter).toLowerCase()) && c.active === this.showActive) {
                    return {
                      ...c,
                      Children: [],
                    }
                  }
                  else {
                    return {
                      Title: "",
                      RoutePath: "",
                      Children: [],
                    }
                  }
                }
                else {
                  return {
                    ...c,
                    Children: c.Children?.filter((f) => {
                      return f.Title.toLowerCase().match(String(this.filter).toLowerCase()) && f.active === this.showActive;
                    })
                  }
                }

              })
            }
          }
          ),
        };
      }),
    ];

    filterArray = temparr;

    temparr = [
      ...temparr.map((element) => {
        return {
          ...element,
          Children: element.Children.map((x) => {
            return {
              ...x,
              Children: x.Children.filter((x) => {
                return x.Title !== "";
              })
            }
          })
        }
      })
    ]


    if(this.textSearchCheck ===  true){
      this.dataSource.data = temparr.filter((x) => {
        return (
          x.Children !== null &&
          x.Children !== undefined &&
          x.Children.length > 0 &&
          x.Children.some((y) => {
            return (
              y?.Children !== null &&
              y?.Children !== undefined &&
              y?.Children.length > 0 &&
              y?.Children.some((z) => {
                return z?.Children !== null  &&
                z?.Children !== undefined &&
                z?.Children.length > 0 || z?.IsEO === true
              })
            );
          })
        );
      });
      this.treeControl.dataNodes = this.dataSource.data;

    } else{
      this.dataSource.data = temparr;
    }
   
    if (this.filter.length > 0) {
      this.treeControl.expandAll();
      this.hasSearchWord = true;
    } else {
      this.treeControl.collapseAll();
      this.hasSearchWord = false;
    }
  }

  openAddnewFlypanel(templateRef: any, name: string) {
    if (name === 'EO') {
      this.isMetaEoCheck = true;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  private setParent(node: EONavBarMenuItem, parent: EONavBarMenuItem | undefined) {
    node.parent = parent;
    if (node.Children) {
      node.Children.forEach((childNode) => {
        this.setParent(childNode, node);
      });
    }
  }

}

class EONavBarMenuItem {
  id?: any;
  Title: string;

  RoutePath: string;

  RouteParams?: Params;

  IconName?: string;
  active?: boolean;

  Children: EONavBarMenuItem[] = [];
  HasChildren?: boolean;
  parent?: EONavBarMenuItem | undefined;
  Collapsed?: boolean = false;
  IsMeta?: boolean = false;
  IsRR?: boolean = false;
  IsEO?: boolean = false;
  isSkill?: boolean = false;
  type?: string;
  number?:string;
}
