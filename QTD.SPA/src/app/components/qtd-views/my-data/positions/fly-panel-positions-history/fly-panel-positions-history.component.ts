import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-positions-history',
  templateUrl: './fly-panel-positions-history.component.html',
  styleUrls: ['./fly-panel-positions-history.component.scss']
})
export class FlyPanelPositionsHistoryComponent implements OnInit {
  displayedColumns: string[] = ['position', 'name', 'desc', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  clearSearch: string;
  appSpinner:boolean=false;
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }

  @ViewChild(MatPaginator) tblPaging: MatPaginator;

/*    @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator && data) this.dataSource.paginator = paginator;
  } */ 

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private posService: PositionsService

  ) { }

  ngOnInit(): void {
    this.getLatestActivity();
  }

  async getLatestActivity() {
    this.appSpinner=true;
    await this.posService
      .getStatusHistory()
      .then((res) => {
        let tempSrc: any[] = [];
        res.forEach((r, i) => {
          tempSrc.push({
            id: r.id,
            position: r.positionNum,
            name: r.positionName,
            desc: r.activityDesc,
            modifyBy: r.createdBy,
            modifyDate: r.createdDate,
          });
        });
        this.dataSource = new MatTableDataSource(tempSrc);
      })
      .catch(() => (this.dataSource = new MatTableDataSource())).finally(() =>{
        this.appSpinner=false;
      });
      this.dataSource.paginator = this.tblPaging;

  }
  filterData(e: any) {
    
    this.dataSource.filter = e.target.value;
  }

  clearFilter(){
    this.dataSource.filter = '';
    this.clearSearch = null;
  }

}
