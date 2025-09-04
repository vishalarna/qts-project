import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { EOLatestActivityVM } from 'src/app/_DtoModels/EnablingObjective/EOLatestActivityVM';
import { EOLinkStats } from 'src/app/_DtoModels/EnablingObjective/EOLinkStats';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-enabling-objectives-overview',
  templateUrl: './enabling-objectives-overview.component.html',
  styleUrls: ['./enabling-objectives-overview.component.scss']
})
export class EnablingObjectivesOverviewComponent implements OnInit, AfterViewInit, OnDestroy {
  isLoading = false;
  dataSource: MatTableDataSource<any>;
  displayedColumns = ['title', 'activityDesc', 'createdDate'];
  notLinkedName = "";
  @ViewChild(MatSort) sort!:MatSort;
  subscription = new SubSink();
  eoStats: EOLinkStats;
  eoActivity: EOLatestActivityVM[];

  constructor(
    private eoService: EnablingObjectivesService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
  ) { }

  ngOnInit(): void {
    this.getStats();
    this.getLatestActivity();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshStats.subscribe((_) => {
      this.getStats();
      this.getLatestActivity();
    })
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getStats() {
    this.eoStats = await this.eoService.getNotLinkedStats();

  }

  async getLatestActivity() {
    this.eoActivity = await this.eoService.getLatestHistory(true);
    var tempArr: EOLatestActivityVM[] = [];
    tempArr = this.eoActivity;
    if(this.dataSource){
      this.dataSource.data = tempArr;
    }
    else{
      this.dataSource = new MatTableDataSource(tempArr);
    }

    setTimeout(()=>{
      this.dataSource.sort = this.sort;
    },1)
  }

  activeInactiveCheck:boolean;
  openFlyPanel(templateRef: any,name:string) {
    this.notLinkedName = name;
    if(this.notLinkedName === 'Active' || this.notLinkedName === 'Inactive'){
      this.activeInactiveCheck = true;
    }else{
      this.activeInactiveCheck = false;
    }
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  openFlyPanelHistory(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  refreshData(){
    this.getStats();
    this.getLatestActivity();
  }

}
