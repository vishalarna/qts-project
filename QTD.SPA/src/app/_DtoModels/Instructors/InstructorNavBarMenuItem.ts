import { Params } from '@angular/router';

export class InstructorNavBarMenuItem {
  id?: any;

  Title: string;

  RoutePath: string;

  RouteParams?: Params;

  active?: boolean;

  Children?: InstructorNavBarMenuItem[] = [];
  HasChildren?: boolean;

  Collapsed?: boolean = false;
  disabled!:boolean;
  isVisible!:boolean
}
