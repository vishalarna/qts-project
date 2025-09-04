export class FilterByOptions{
  filter!:string;
  doInclude!:'include'|'exclude';
  activeStatus:boolean;
  activeILAStatus:boolean;
  providerIds: any[];
}
