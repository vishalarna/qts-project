import { Params } from '@angular/router';

export class PositionNavBarMenuItem {
  id?: any;

  Title: string;

  RoutePath: string;

  RouteParams?: Params;

  active?: boolean;

  type?: string;
  number?: any;
}
