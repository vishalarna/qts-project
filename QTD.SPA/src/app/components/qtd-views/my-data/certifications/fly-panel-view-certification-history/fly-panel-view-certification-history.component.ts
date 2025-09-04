import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { FlyPanelViewCertificationHistoryModule } from './fly-panel-view-certification-history.module';

@Component({
  selector: 'app-fly-panel-view-certification-history',
  templateUrl: './fly-panel-view-certification-history.component.html',
  styleUrls: ['./fly-panel-view-certification-history.component.scss']
})
export class FlyPanelViewCertificationHistoryComponent implements OnInit {
  //displayedColumns: string[] = ['num','name', 'desc', 'modifyDate'];
  displayedColumns: string[] = ['LocName', 'notes', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  clearSearch: string;
  appSpinner:boolean=false;

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }

  @ViewChild(MatPaginator)  tblPaging: MatPaginator;
/*   @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  } */
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private certService:CertificationService,
  ) {}

  ngOnInit(): void {
    this.getLatestActivity();
  }

  async getLatestActivity() {
    this.appSpinner = true;
    await this.certService
      .getStatusHistory(false)
      .then((res) => {
        let tempSrc: any[] = [];
        res.forEach((h, i) => {
          tempSrc.push({
            index: i,
            id: h.certId,
            Locnum:h.certAcronym,
            LocName: h.name,
            notes: h.activityDesc,
            modifyBy: h.createdBy,
            modifyDate: h.createdDate,
          });
        });
        this.dataSource = new MatTableDataSource(tempSrc);
        setTimeout(()=>{
          this.dataSource.paginator = this.tblPaging;
        },1) 
      })
      .catch(() => (this.dataSource = new MatTableDataSource())).finally(() =>{
        this.appSpinner = false;
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
