import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { of } from 'rxjs';
import { ProcedureStatsVM } from 'src/app/_DtoModels/Procedure/ProcedureStatsVM';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-procedure-overview',
  templateUrl: './procedure-overview.component.html',
  styleUrls: ['./procedure-overview.component.scss'],
})
export class ProcedureOverviewComponent implements OnInit,OnDestroy,AfterViewInit {
  displayedColumns: string[] = ['name', 'desc', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  NotLinkedToPanelName: string;
  statsVM: ProcedureStatsVM;
  isLoading: boolean = false;
  subscription = new SubSink();
  activeInactiveCheck:boolean=false;

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
    private procSrvc: ProceduresService,
    private dataBroadcastService: DataBroadcastService,
  ) {}

  ngOnInit(): void {
    this.getProcedureStats();
    this.getLatestActivity();
  }

  ngAfterViewInit(): void {
    
    this.subscription.sink = this.dataBroadcastService.refreshOverviewData.subscribe((res:any)=>{
      this.getProcedureStats();
      this.getLatestActivity();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getLatestActivity() {
    
    await this.procSrvc
      .getStatusHistory(true)
      .then((res) => {
        let tempSrc: any[] = [];
        let tempArr = res;
        tempArr.forEach((h, i) => {
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
      .catch(() => (this.dataSource = new MatTableDataSource()));
  }

  async openFlyInPanel(templateRef: any, name: string) {

    if(name == 'Activeia' || name == 'Inactiveia' || name == 'Activeproc' || name == 'Inactiveproc'){
      this.activeInactiveCheck = true;
    }else{
      this.activeInactiveCheck = false;
    }
    this.NotLinkedToPanelName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  async getProcedureStats() {
    this.isLoading = true;
    await this.procSrvc
      .getStatsCount()
      .then((res) => {

        this.statsVM = res;
      })
      .finally(() => (this.isLoading = false));
  }
}
