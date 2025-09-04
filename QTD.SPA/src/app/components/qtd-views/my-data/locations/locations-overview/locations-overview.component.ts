import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { LocationStatsVM } from 'src/app/_DtoModels/Locations/LocationStatsVM';
import { LocationService } from 'src/app/_Services/QTD/location.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';
import { FlyPanelViewLocationHistoryModule } from '../fly-panel-view-location-history/fly-panel-view-location-history.module';
export interface PeriodicElement {
  name: string;
  position: number;
  weight: string;
  symbol: string;
  modifiedate:string;
}

@Component({
  selector: 'app-locations-overview',
  templateUrl: './locations-overview.component.html',
  styleUrls: ['./locations-overview.component.scss']
})
export class LocationsOverviewComponent implements OnInit {
  isLoading: boolean = false;
  catCompleted: any
  catIncompleted: any
  //displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  //dataSource = ELEMENT_DATA;
  displayedColumns: string[] = ['Locnum','LocName', 'desc', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  locationVM:LocationStatsVM;
  subscription = new SubSink();
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }
  constructor(private locService:LocationService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef) { }

  ngOnInit(): void
   {

  this.getLocationStats();
  this.getLatestActivity();

    // this.catCompleted = 10
    // this. catIncompleted = 24
  }
  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshOverviewData.subscribe((res:any)=>{
      this.getLocationStats();

    });
  }
  async getLocationStats() {
    this.isLoading = true;
    await this.locService.getStatsCount()
      .then((res:any) => {
        this.locationVM = res;
      })
      .finally(() => (this.isLoading = false));
  }

  async getLatestActivity() {
    await this.locService
      .getStatusHistory()
      .then((res) => {
        let tempSrc: any[] = [];
        res.forEach((h, i) => {
          tempSrc.push({
            index: i,
            id: h.locId,
            Locnum:h.locNumber,
            LocName: h.locName,
            desc: h.activityDesc,
            modifyBy: h.createdBy,
            modifyDate: h.createdDate,
          });

        });
        this.dataSource = new MatTableDataSource(tempSrc.slice(0,5));
      })
      .catch(() => (this.dataSource = new MatTableDataSource()));
  }


  async openFlyInPanel(templateRef: any)
  {

    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  moduleName:any;
  openFlyInPanelList(templateRef: any, name:string) {
    this.moduleName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
}
