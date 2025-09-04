import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { CertificationStatsVM } from 'src/app/_DtoModels/Certification/CertificationStatsVM';
import { CertificationService } from 'src/app/_Services/QTD/certification.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';
// import { FlyPanelViewLocationHistoryModule } from '../fly-panel-view-location-history/fly-panel-view-location-history.module';
export interface PeriodicElement {
  name: string;
  position: number;
  weight: string;
  symbol: string;
  modifiedate: string;
}
@Component({
  selector: 'app-certification-overview',
  templateUrl: './certification-overview.component.html',
  styleUrls: ['./certification-overview.component.scss']
})
export class CertificationOverviewComponent implements OnInit {

  isLoading: boolean = false;
  catCompleted: any
  catIncompleted: any
  //displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  //dataSource = ELEMENT_DATA;
  displayedColumns: string[] = ['Locnum', 'LocName', 'notes', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  certificationVM: CertificationStatsVM;
  subscription = new SubSink();
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  // @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
  //   if (paginator) this.dataSource.paginator = paginator;
  // }
  constructor(private certService: CertificationService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef) { }

  ngOnInit(): void {
    this.getCertificationStats();
    this.getLatestActivity();


  }
  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshOverviewData.subscribe((res: any) => {
      if(res){
        this.getCertificationStats();
        this.getLatestActivity();
      }

    });
  }


  async getCertificationStats() {
    this.isLoading = true;
    await this.certService.getStatsCount()
      .then((res: any) => {
        this.certificationVM = res;
      })
      .finally(() => (this.isLoading = false));
  }

  async getLatestActivity() {
    await this.certService
      .getStatusHistory(true)
      .then((res) => {
        let tempSrc: any[] = [];
        let tempArr = res;
        tempArr.forEach((h, i) => {
          tempSrc.push({
            index: i,
            id: h.certId,
            Locnum: h.certAcronym,
            LocName: h.name,
            notes: h.activityDesc,
            modifyBy: h.createdBy,
            modifyDate: h.createdDate,
          });
        });
        this.dataSource = new MatTableDataSource(tempSrc);
      })
      .catch(() => (this.dataSource = new MatTableDataSource()));
  }

  async openFlyInPanel(templateRef: any) {

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  moduleName:string;
  openFlyInPanelList(templateRef: any,name:string) {
    this.moduleName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

}
