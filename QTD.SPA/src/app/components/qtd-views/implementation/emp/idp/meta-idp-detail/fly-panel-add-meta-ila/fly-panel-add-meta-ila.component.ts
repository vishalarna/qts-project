import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MetaILAVM } from '@models/MetaILA/MetaILAVM';
import { MetaILAService } from 'src/app/_Services/QTD/meta-ila.service';

@Component({
  selector: 'app-fly-panel-add-meta-ila',
  templateUrl: './fly-panel-add-meta-ila.component.html',
  styleUrls: ['./fly-panel-add-meta-ila.component.scss']
})
export class FlyPanelAddMetaIlaComponent implements OnInit {
  @Input() alreadyLinkedMetaILAsIds:any[] = [];
  @Output() closed = new EventEmitter<any>();
  @Output() linkMetaILAIds = new EventEmitter<any>();
  selectedMetaILAIds:string[]=[];
  searchText: string = '';
  metaILAData:MetaILAVM[] = [];
  metaILADataSource: MatTableDataSource<any> = new MatTableDataSource();
  originalMetaILADataSource: MatTableDataSource<any> = new MatTableDataSource();
  displayedColumns:string[] = ["id","name"];
  constructor(
    private metaILAService:MetaILAService
  ) { }

  ngOnInit(): void {
    this.getAllMetaILA();
  }

  async getAllMetaILA() {
    await this.metaILAService.getAll().then((res:any)=>{
      this.metaILAData = res;
      this.makeMetaILADataSource(this.metaILAData);
      this.originalMetaILADataSource.data = this.metaILADataSource.data;
    });
  }

  searchMetaILA(event: any) {
    const searchTerm = event.target.value.toLowerCase();
    this.searchText = searchTerm;
    var filteredData = this.metaILAData.filter(m=>m.name.toLowerCase().includes(this.searchText));
    this.makeMetaILADataSource(filteredData);
  }

  masterToggle() {
    const currentRows = this.metaILADataSource.data.filter(row => !this.alreadyLinkedMetaILAsIds?.includes(row.id));
    const allSelected = currentRows.every(row => this.selectedMetaILAIds.includes(row.id));
  
    if (allSelected) {
      currentRows.forEach(row => {
        const index = this.selectedMetaILAIds.indexOf(row.id);
        if (index > -1) {
          this.selectedMetaILAIds.splice(index, 1);
        }
      });
    } else {
      currentRows.forEach(row => {
        if (!this.selectedMetaILAIds.includes(row.id)) {
          this.selectedMetaILAIds.push(row.id);
        }
      });
    }
  }
  
  isAllSelected() {
    const currentRows = this.metaILADataSource.data.filter(row => !this.alreadyLinkedMetaILAsIds?.includes(row.id));
    return currentRows.length > 0 && currentRows.every(row => this.selectedMetaILAIds.includes(row.id));
  }
  
  
  makeMetaILADataSource(data) {
    this.metaILADataSource = new MatTableDataSource(data);
  }
  
  isLinked(id: string): boolean {
    return this.alreadyLinkedMetaILAsIds?.some(item => item === id);
  }
  
  isSelected(id: string): boolean {
    return this.selectedMetaILAIds.includes(id);
  }
  
  metaILASelectionChange(id: string) {
    const index = this.selectedMetaILAIds.indexOf(id);
    if (index === -1) {
      this.selectedMetaILAIds.push(id);
    } else {
      this.selectedMetaILAIds.splice(index, 1);
    }
  }

  addMetaILA(){
    this.linkMetaILAIds.emit(this.selectedMetaILAIds);
    this.closed.emit();
  }

}
