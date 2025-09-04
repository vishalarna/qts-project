import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { RR_StatsVM } from 'src/app/_DtoModels/RegulatoryRequirements/RR_StatsVM';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-rr-overview',
  templateUrl: './rr-overview.component.html',
  styleUrls: ['./rr-overview.component.scss'],
})
export class RROverviewComponent implements OnInit,OnDestroy,AfterViewInit {
  displayedColumns: string[] = ['name', 'desc', 'modifyBy'];
  dataSource: MatTableDataSource<any>;
  NotLinkedToPanelName: string;
  stats: RR_StatsVM;
  isLoading: boolean = false;
  subscription = new SubSink();

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private rrSrvc: RegulatoryRequirementService,
    private dataBroadcastService : DataBroadcastService,
  ) {}

  ngOnInit(): void {
    this.getLatestActivity();
    this.getStats();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshOverviewData.subscribe((res:any)=>{
      this.getLatestActivity();
      this.getStats();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getLatestActivity() {
    await this.rrSrvc
      .getStatusHistory()
      .then((res) => {
        let tempSrc: any[] = [];
        let tempArr = [...res.slice(0, 5)];
        tempArr.forEach((h, i) => {
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
      .catch(() => (this.dataSource = new MatTableDataSource()));
  }

  activeInactiveCheck:boolean=false;
  async openFlyInPanel(templateRef: any, name: string) {
    if(name === 'Catactive' || name==='Catinactive' || name==='Rractive' || name==='Rrinactive'){
      this.activeInactiveCheck = true;
    }else{
      this.activeInactiveCheck=false;
    }
    this.NotLinkedToPanelName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  getStats() {
    this.isLoading = true;
    this.rrSrvc
      .getStatsCount()
      .then((res) => {
        this.stats = res;
      })
      .finally(() => {
        this.isLoading = false;
      });
  }
}
