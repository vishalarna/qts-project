import { Params } from "@angular/router";

export class ToolNavBarMenuItem{
    id?: any;
    Title: string;
  
    RoutePath: string;
  
    RouteParams?: Params;
  
    IconName?: string;
    active?: boolean;
  
    Children?: ToolNavBarMenuItem[] = [];
    HasChildren?: boolean;
    Collapsed?: boolean = false;
    type?: string;
    number?: any;
    parent?: ToolNavBarMenuItem;

    constructor(id: any, type?: string, collapsed?: boolean, routeParams?: Params){
        this.id = id;
        this.type = type;
        this.Collapsed = collapsed;
        this.RouteParams = routeParams;
    }
}