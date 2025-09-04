import { TemplatePortal } from '@angular/cdk/portal';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { UntypedFormBuilder } from '@angular/forms';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { TaskStatsCount } from 'src/app/_DtoModels/Task/TaskStatsCount';
import { TaskWithNumberVM } from 'src/app/_DtoModels/Task/TaskWithNumberVM';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-task-overview',
  templateUrl: './task-overview.component.html',
  styleUrls: ['./task-overview.component.scss'],
})
export class TaskOverviewComponent implements OnInit, AfterViewInit, OnDestroy {
  displayedColumns: string[] = ['name', 'desc', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  NotLinkedToPanelName: string;
  stats = new TaskStatsCount();
  TaskData: TaskWithNumberVM[] = [];
  spinner: boolean = false;
  datePipe = new DatePipe('en-us');

  subscription = new SubSink();

  @ViewChild(MatSort,{static: false}) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private fb: UntypedFormBuilder,
    private taskSrvc: TasksService,
    private router: Router,
    private dataBroadcastService: DataBroadcastService,
  ) { }

  ngOnInit(): void {
    this.getStats();
    this.getLatestActivity();
    this.getPendingTasks();
  }

  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.updateMyDataNavBar.subscribe((res) => {
      this.getStats();
      this.getLatestActivity();
      this.getPendingTasks();
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async getLatestActivity() {
    this.spinner = true;
    await this.taskSrvc
      .getlatestActivity(true)
      .then((res) => {
        let tempSrc: any[] = [];
        let tempArr = [...res];
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
      .finally(() => {
        this.spinner = false;
      });
  }
activeInactivecheck:boolean;
  async openFlyInPanel(templateRef: any, name: string) {
    if(name === 'Active' || name === 'Inactive'){
      this.activeInactivecheck = true;
    }else{
      this.activeInactivecheck = false;
    }
    this.NotLinkedToPanelName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelSrvc.open(portal);
  }

  async getPendingTasks() {
    this.TaskData = [];
    this.TaskData = await this.taskSrvc.GetPendingTasks();

  }

  redirectToTask(data: TaskWithNumberVM) {
    this.router.navigate([`/my-data/tasks/detail/${data.task.id}-${data.letter +
      ' ' +
      data.daNumber +
      '.' +
      data.sdaNumber +
      '.' +
      data.task.number
      }`]);
  }

  getStats() {
    this.spinner = true;
    this.taskSrvc
      .getOverviewStats()
      .then((res) => {
        this.stats = res;
      })
      .finally(() => {
        this.spinner = false;
      });
  }
}
