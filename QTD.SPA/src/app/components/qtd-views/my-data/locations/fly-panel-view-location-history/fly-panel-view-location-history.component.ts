import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { LocationService } from 'src/app/_Services/QTD/location.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { FlyPanelViewLocationHistoryModule } from './fly-panel-view-location-history.module';

@Component({
  selector: 'app-fly-panel-view-location-history',
  templateUrl: './fly-panel-view-location-history.component.html',
  styleUrls: ['./fly-panel-view-location-history.component.scss']
})
export class FlyPanelViewLocationHistoryComponent implements OnInit {
  //displayedColumns: string[] = ['num','name', 'desc', 'modifyDate'];
  displayedColumns: string[] = ['name', 'desc', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  appSpinner:boolean=false;

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private locService:LocationService,
  ) {}

  ngOnInit(): void {
    this.getLatestActivity();
  }

  async getLatestActivity() {
    this.appSpinner=true;

    await this.locService
      .getStatusHistory()
      .then((res) => {
        let tempSrc: any[] = [];
        res.forEach((h, i) => {
          tempSrc.push({
            index: i,
            id: h.locId,
            num:h.locNumber,
            name: h.locName,
            desc: h.activityDesc,
            modifyBy: h.createdBy,
            modifyDate: h.createdDate,
          });
        });
        this.dataSource = new MatTableDataSource(tempSrc);
      })
      .catch(() => (this.dataSource = new MatTableDataSource())).finally(() =>{
        this.appSpinner=false;
      });;
  }
  filterData(e: any) {
    
    this.dataSource.filter = e.target.value;
  }

  clearSearch:string;
  clearFilter(){
    this.dataSource.filter = '';
    this.clearSearch = null;
  }
}
