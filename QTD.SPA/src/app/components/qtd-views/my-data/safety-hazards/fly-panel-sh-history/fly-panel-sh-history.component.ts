import { Component, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-sh-history',
  templateUrl: './fly-panel-sh-history.component.html',
  styleUrls: ['./fly-panel-sh-history.component.scss']
})
export class FlyPanelShHistoryComponent implements OnInit {
  dataSource: MatTableDataSource<any>;
  displayedColumns: string[] = ['name', 'desc', 'modifyDate'];

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private shService: SafetyHazardsService,
  ) { }

  ngOnInit(): void {
    this.getHistory();
  }

  filterData(e: any) {
    this.dataSource.filter = e.target.value;
    
  }

  async getHistory() {
    await this.shService
      .getLatestActivity(false)
      .then((res) => {
        let tempSrc: any[] = [];
        
        res.forEach((h, i) => {
          tempSrc.push({
            index: i,
            id: h.id,
            name: h.title,
            desc: h.activityDesc,
            modifyBy: h.createdBy,
            modifyDate: h.createdDate,
          });
        });
        
        this.dataSource = new MatTableDataSource(tempSrc);
        
      })
      .finally(() =>{});
  }

  clearSearch:string;
  clearFilter(){
    this.dataSource.filter = '';
    this.clearSearch = null;
  }

}
