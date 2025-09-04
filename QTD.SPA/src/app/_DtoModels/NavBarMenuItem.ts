import { Params } from '@angular/router';

export class NavBarMenuItem {
  id?: any;
  Title: any;

  RoutePath: string;

  RouteParams?: Params;

  IconName?: string;

  Children?: NavBarMenuItem[] = [];
  HasChildren?: boolean;
  Collapsed?: boolean = false;
  hasHover?:boolean = false;
  disabled!:boolean;
  isVisible!: boolean;
  tooltip?:string='';
}
