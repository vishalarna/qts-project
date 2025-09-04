import { Params } from '@angular/router';

export class LocationNavBarMenuItem {
  id?: any;

  Title: string;

  RoutePath: string;

  RouteParams?: Params;

  active?: boolean;

  Children?: LocationNavBarMenuItem[] = [];
  HasChildren?: boolean;

  Collapsed?: boolean = false;
}