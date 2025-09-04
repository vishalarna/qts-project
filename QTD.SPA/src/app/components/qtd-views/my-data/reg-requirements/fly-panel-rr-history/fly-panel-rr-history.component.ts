import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-rr-history',
  templateUrl: './fly-panel-rr-history.component.html',
  styleUrls: ['./fly-panel-rr-history.component.scss'],
})
export class FlyPanelRrHistoryComponent implements OnInit {
  displayedColumns: string[] = ['name', 'desc', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  appSpinner:boolean=false;
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild(MatPaginator) tblPaging: MatPaginator/* ) {
    if (paginator) this.dataSource.paginator = paginator;
  } */
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private rrSrvc: RegulatoryRequirementService
  ) {}

  ngOnInit(): void {
    this.getHistory();
  }

  async getHistory() {
    this.appSpinner=true;

    await this.rrSrvc
      .getStatusHistory()
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
