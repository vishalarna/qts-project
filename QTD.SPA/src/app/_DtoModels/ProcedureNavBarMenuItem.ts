import { Params } from '@angular/router';

export class ProcedureNavBarMenuItem {
  id?: any;

  Title: string;

  RoutePath: string;

  RouteParams?: Params;

  active?: boolean;

  Children?: ProcedureNavBarMenuItem[] = [];
  HasChildren?: boolean;

  Collapsed?: boolean = false;
  selected?:boolean = false;
}
