import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnDestroy, OnInit, ViewContainerRef } from '@angular/core';
import { Store } from '@ngrx/store';
import { Task_RequalificationStatsVM } from 'src/app/_DtoModels/Task_Requalification/Task_RequalificationStatsVM';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-requalification-overview',
  templateUrl: './task-requalification-overview.component.html',
  styleUrls: ['./task-requalification-overview.component.scss']
})
export class TaskRequalificationOverviewComponent implements OnInit, OnDestroy {

  taskRequalificationStats!: Task_RequalificationStatsVM;
  empId = '';
  subscription = new SubSink();
  selectedIndex = 0;

  constructor(
    public flyPanelService : FlyInPanelService,
    private vcf : ViewContainerRef,
    private taskRequalificationService : TaskRequalificationService,
    private dataBroadcastService : DataBroadcastService,
    private store : Store<any>,
  ) { }

  ngOnInit(): void {

    var data = localStorage.getItem('filter');
    var empNavigate = localStorage.getItem('empNav');
    if(data !== null || empNavigate !== null){
      this.selectedIndex = 1;
      localStorage.removeItem('empNav');
      //localStorage.removeItem('filter');
    }

    this.readyStats();

    this.subscription.sink = this.dataBroadcastService.filterByEmp.subscribe((data)=>{
      this.empId = data;
    });

    this.subscription.sink = this.dataBroadcastService.refreshTQStats.subscribe((_)=>{
      this.readyStats();
    })
  }

  ngOnDestroy(): void {

  }

  gototqStep(){
    this.selectedIndex = 1;
  }

  async readyStats() {
    this.taskRequalificationStats = await this.taskRequalificationService.getStats();
  }

  openFlyPanel(templateRef:any){
    const portal = new TemplatePortal(templateRef,this.vcf);
    this.flyPanelService.open(portal);
  }

}
