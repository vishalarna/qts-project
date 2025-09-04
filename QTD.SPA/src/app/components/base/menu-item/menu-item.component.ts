import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Params } from '@angular/router';
import { NavBarMenuItem } from 'src/app/_DtoModels/NavBarMenuItem';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrls: ['./menu-item.component.scss'],
})
export class MenuItemComponent implements OnInit,OnDestroy {
  @Input() id:any;
  constructor(private dataBroadcastService:DataBroadcastService) {

  }

  /**
   * Only used if *HasChildren* is true
   * Determines if the submenu is collapsed
   */
  @Input()
  Collapsed: boolean;

  @Input()
  AddNew: boolean;

  @Input()
  disabled:boolean = false;

  /*
   * [
   *  {
   *   Entity: 'foo', Id: {fooId}
   *  },
   *  {
   *   Entity: 'bar', Id: {barId}
   *  },
   *  ]
   * In the MenuItemClickHandle build the URl like this
   * foo/{fooId}/bar/{barId}
   */
  @Input()
  Ancestors: [];

  /**
   * if the item has chidlren then show the collapse/expand chevrons to the left
   */
  @Input()
  Level: string;

  /**
   * if the item has chidlren then show the collapse/expand chevrons to the left
   */
  @Input()
  HasChildren: boolean;

  /**
   * The title of the item
   */
  @Input()
  Title: string;

  /**
   * the numbers to the left of the title
   */
  @Input()
  TitleIndex: string;

  /**
   * The icon to display along the title
   */
  @Input()
  IconName: string;

  @Input()
  Mode: 'read' | 'write' = 'read';

  /**
   * Path to route to specific component
   */
  @Input()
  RoutePath: string;

  /**
   * Query Params to attach with a route path
   */
  @Input()
  RouteParams?: Params;

  @Input()
  Children?: NavBarMenuItem[];

  @Input()
  tooltip: string ='';

  /**
   * Will Emit event on saving new menu item
   */
  @Output()
  ItemSaved = new EventEmitter<any>();

  @Input()
  hasHover:boolean = false;
  subscriptions = new SubSink();

  selectedItem: string;
  selectedId:any;

  @Input()
  isVisible:boolean

  MenuSaved(parent: any, level: any, d: any) {
    let data = {
      parent: parent,
      level: level,
      data: d,
    };
    this.ItemSaved.emit(JSON.stringify(data));
    this.Level = '';
  }

  emitSelected(title:any,id:any){
    this.dataBroadcastService.qtdMenuItemSelected.next({title:title,id:id});
  }

  ngOnInit(): void {
    this.subscriptions.sink = this.dataBroadcastService.qtdMenuItemSelected.subscribe((data)=>{
      this.selectedItem = data.title;
      this.selectedId = data.id;
      
    })
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

}
