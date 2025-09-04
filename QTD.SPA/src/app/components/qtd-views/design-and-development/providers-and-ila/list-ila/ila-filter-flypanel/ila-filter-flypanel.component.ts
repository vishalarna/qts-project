import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-ila-filter-flypanel',
  templateUrl: './ila-filter-flypanel.component.html',
  styleUrls: ['./ila-filter-flypanel.component.scss']
})
export class IlaFilterFlypanelComponent implements OnInit {

  @Input() providerList: any[];
  @Output() closed  = new EventEmitter<any>();
  @Output() applyFilter  = new EventEmitter<any>();
  @Output() providerName  = new EventEmitter<any>();
  @Output() providerIdDetail  = new EventEmitter<any>();
  @Input() providerAraray: any=[];
  @Input() providerIdList: number[];

  constructor() { }

  ngOnInit(): void {
  }
  closeTopic(){
      this.closed.emit();
  }

  ApplyFiltersClicked(){
    this.applyFilter.emit(this.providerList);
    this.providerName.emit(this.providerAraray);
    this.providerIdDetail.emit(this.providerIdList);
    this.closed.emit();
  }
  SelectionChange(value: any) {
    const nameIndex = this.providerAraray.findIndex(item => item.toLowerCase() === value.provider.toLowerCase());
    
    if (nameIndex !== -1 && !value.checked) {
      this.providerAraray.splice(nameIndex, 1);
    } else if (nameIndex === -1 && value.checked) {
      this.providerAraray.push(value.provider);
    }

    const indexId = this.providerIdList.findIndex(item => Number(item) === Number(value.id));
    
    if (indexId !== -1 && !value.checked) {
      this.providerIdList.splice(indexId, 1);
    } else if (indexId === -1 && value.checked) {
      this.providerIdList.push(value.id);
    }
        
  }
}
