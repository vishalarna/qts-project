import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-procedures-history',
  templateUrl: './fly-panel-procedures-history.component.html',
  styleUrls: ['./fly-panel-procedures-history.component.scss'],
})
export class FlyPanelProceduresHistoryComponent implements OnInit {
  displayedColumns: string[] = ['name', 'desc', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  appSpinner:boolean=false;
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild(MatPaginator) tblPaging: MatPaginator/* ) {
    if (paginator) this.dataSource.paginator = paginator;
  } */
  clearSearch:string='';
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private procSrvc: ProceduresService
  ) {}

  ngOnInit(): void {
    this.getHistory();
  }

  async getHistory() {
    this.appSpinner=true;
    await this.procSrvc
      .getStatusHistory(false)
      .then((res) => {
        let tempSrc: any[] = [];
        res.forEach((h, i) => {
          const utcDate = new Date(h.createdDate + 'Z');
          const localDate = new Date(utcDate).toLocaleString();
          tempSrc.push({
            index: i,
            id: h.id,
            name: h.procedureTitle,
            desc: h.activityDesc,
            modifyBy: h.createdBy,
            modifyDate: localDate,
          });
        });
        this.dataSource = new MatTableDataSource(tempSrc);
      })
      .catch(() => (this.dataSource = new MatTableDataSource())).finally(() =>{
        this.appSpinner=false;
      });
  }

  filterData(e: any) {
    
    this.dataSource.filter = e.target.value;
  }

  clearFilter(){
    this.dataSource.filter = '';
    this.clearSearch = null;
  }
}
