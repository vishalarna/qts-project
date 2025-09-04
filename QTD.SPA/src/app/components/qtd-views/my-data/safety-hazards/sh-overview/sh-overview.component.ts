import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { SHLatestActivityVM } from 'src/app/_DtoModels/SafetyHazard_StatusHistory/SHLatestActivity';
import { SHStatsVM } from 'src/app/_DtoModels/SaftyHazard/SH_StatsVM';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-sh-overview',
  templateUrl: './sh-overview.component.html',
  styleUrls: ['./sh-overview.component.scss'],
})
export class ShOverviewComponent implements OnInit, AfterViewInit, OnDestroy {
  displayedColumns: string[] = ['title', 'activityDesc', 'createdDate'];
  dataSource: MatTableDataSource<any>;
  NotLinkedToPanelName: string;
  spinner = false;
  stats: SHStatsVM | undefined;
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
    private shService: SafetyHazardsService,
    private dataBroadcastService: DataBroadcastService,
  ) { }

  ngOnInit(): void {
    this.getLatestActivity();
    this.getStats();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshOverviewData.subscribe((_) => {
      this.stats = undefined;
      this.dataSource = new MatTableDataSource();
      this.getLatestActivity();
      this.getStats();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getLatestActivity() {
    let tempSrc: any[] = [];
    this.spinner = true;
    var data: SHLatestActivityVM[] = await this.shService.getLatestActivity(true).finally(() => {
      this.spinner = false;
    });
    this.dataSource = new MatTableDataSource(data);
  }

  async getStats() {
    this.spinner = true;
    this.stats = await this.shService.getStats().finally(() => {
      this.spinner = false;
    })
  }

  activeInactiveCheck:boolean=false;
  async openFlyInPanel(templateRef: any, name: string) {
    if(name === 'Catactive' || name === 'Catinactive' || name === 'Shactive' || name === 'Shinactive'){
      this.activeInactiveCheck = true;
    }else{
      this.activeInactiveCheck = false;
    }
    this.NotLinkedToPanelName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  async openHistFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }
}
