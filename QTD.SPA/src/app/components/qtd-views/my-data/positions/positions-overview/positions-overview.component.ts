import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Position_StatsVM } from 'src/app/_DtoModels/Position/Position_StatsVM';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-positions-overview',
  templateUrl: './positions-overview.component.html',
  styleUrls: ['./positions-overview.component.scss']
})
export class PositionsOverviewComponent implements OnInit {
  NotLinkedToPanelName : any;
  isLoading: boolean = false;
  catCompleted: any
  catIncompleted: any
  displayedColumns: string[] = ['name', 'desc', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  stats: Position_StatsVM | undefined;
  spinner = false;

  subscription = new SubSink();

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }

  constructor(public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private posService: PositionsService,
    private fb: UntypedFormBuilder,
    private dataBroadcastService: DataBroadcastService,) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.getStats();
    this.getLatestActivity();

    this.subscription.sink = this.dataBroadcastService.refreshOverviewData.subscribe((res:any)=>{
      this.getLatestActivity();
    });

    this.subscription.sink =
      this.dataBroadcastService.refreshPositionStats.subscribe(
        (res: any) => {

          this.getStats();
        }
      );
  }

  async getLatestActivity() {
    await this.posService
      .getStatusHistory(true)
      .then((res) => {

        let tempSrc: any[] = [];
        res.forEach((r) => {
          tempSrc.push({
            id: r.id,
            position: r.positionNum,
            name: r.positionName,
            desc: r.activityDesc,
            modifyBy: r.createdBy,
            modifyDate: r.createdDate,
          });
        });
        // res.forEach((h, i) => {
        //   tempSrc.push({
        //     index: i,
        //     id: h.id,
        //     name: h.positionName,
        //     desc: h.activityDesc,
        //     modifyBy: h.createdBy,
        //     modifyDate: h.createdDate,
        //   });
        // });
        tempSrc = [...new Set(tempSrc)];
        this.dataSource = new MatTableDataSource(tempSrc);

      })
      .catch(() => (this.dataSource = new MatTableDataSource()));
  }

  activeInactiveCheck:boolean=false;
  async openFlyInPanel(templateRef: any, name: string) {
    if(name == 'Active' || name == 'Inactive'){
      this.activeInactiveCheck = true;
    }else{
      this.activeInactiveCheck = false;
    }
    this.NotLinkedToPanelName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  async getStats() {
    this.spinner = true;
    this.posService.getStats()
      .then((res: Position_StatsVM) =>{

        this.stats = res;
      })
      .finally(() => {
      this.spinner = false;
    })
  }

}
