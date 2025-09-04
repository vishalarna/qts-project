import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-flypanel-certification-expiration-table-filter',
  templateUrl: './flypanel-certification-expiration-table-filter.component.html',
  styleUrls: ['./flypanel-certification-expiration-table-filter.component.scss']
})
export class FlypanelCertificationExpirationTableFilterComponent implements OnInit {

  @Input() selectedFilters:any[] = [];
  @Output() closed = new EventEmitter<any>();
  @Output() filterApplied = new EventEmitter<any>();
  certificateFilters:string[] = [];
  constructor() { }

  ngOnInit(): void {
    this.certificateFilters = ["All","Pending Expiration","Suspended","Expired"];
  }

  onFilterChange(selected:any[]){
    this.selectedFilters = selected;
  }

  onFilterApplied(){
    this.filterApplied.emit(this.selectedFilters);
    this.closed.emit();
  }

}
