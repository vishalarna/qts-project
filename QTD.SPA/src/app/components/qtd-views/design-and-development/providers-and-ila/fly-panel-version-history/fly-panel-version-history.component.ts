import { TemplatePortal } from '@angular/cdk/portal';
import { Component, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Version_ILAModel } from '@models/Version_ILA/Version_ILA';
import { IlaService } from 'src/app/_Services/QTD/ila.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-version-history',
  templateUrl: './fly-panel-version-history.component.html',
  styleUrls: ['./fly-panel-version-history.component.scss']
})
export class FlyPanelVersionHistoryComponent implements OnInit {
  dataSource: any;
  displayColumns: string[];
  @Input() ilaId!: any;
  spinner:boolean;
  versionDetails:any;
  versionDate:any;
  isDetailFlyPanelOpen:boolean = false;
  versionNumber:any;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private ilaService: IlaService,
  ) { }

  ngOnInit(): void {
    this.displayColumns = ['versionNumber','publishedDate','changedBy','actions'];
    this.dataSource = new MatTableDataSource<Version_ILAModel>();
    this.loadAsync();
  }

  async loadAsync() {
    this.spinner=true;
    await this.ilaService.getIlaVersions(this.ilaId,false).then(res => {
      this.spinner = false;
      this.versionDetails=res;
      this.dataSource = new MatTableDataSource<Version_ILAModel>(res);
      });
  }

  sortVersionIlaData(sort: Sort) {
    this.dataSource.sort = this.sort;
    const data = this.dataSource.data;
    if (!sort.active || sort.direction === '') {
      this.dataSource.data = data;
      return;
    }

    this.dataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'publishedDate':
          return this.compare(a.effectiveDate, b.effectiveDate, isAsc);
        default:
          return 0;
      }
    });
  }

  compare(a: number | string | Date, b: number | string | Date, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  openFlyInPanelVersionHistoryDetails(row:any){
    this.versionNumber = row.versionNumber;
    this.versionDate = row.effectiveDate;
    this.isDetailFlyPanelOpen = true;  
  }


}

