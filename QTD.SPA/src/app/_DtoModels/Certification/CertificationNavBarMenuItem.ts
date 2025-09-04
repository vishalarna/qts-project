import { Params } from '@angular/router';

export class CertificationNavBarMenuItem {
  id?: any;
  Title: string;

  RoutePath: string;

  RouteParams?: Params;

  IconName?: string;
  active?: boolean;

  Children?: CertificationNavBarMenuItem[] = [];
  HasChildren?: boolean;
  Collapsed?: boolean = false;
  type?: string;
  number?: any;
  isNERCCheck?:boolean=false;
}