import { TemplatePortal } from '@angular/cdk/portal';
import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { Position } from 'src/app/_DtoModels/Position/Position';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-task-position-link',
  templateUrl: './task-position-link.component.html',
  styleUrls: ['./task-position-link.component.scss'],
})
export class TaskPositionLinkComponent
  implements AfterViewInit, OnDestroy, OnInit
{
  LinkPosMenu: boolean = false;
  AddEditPosMenu: boolean = false;
  PosToLink: any[] = [];
  PosToUnlink: any[] = [];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  @ViewChild(DataTableDirective, { static: false })
  dtElement!: DataTableDirective;

  daNumber!: number;
  sdaNumber!: number;
  taskId!: any;
  tNumber!: number;
  LinkedPositions: Position[] = [];
  taskDetail!: Task;
  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private dataBroadcastService: DataBroadcastService,
    private activatedRoute: ActivatedRoute,
    private taskSrvc: TasksService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
    this.dataBroadcastService.ShowMenuSideBar.next(false);
  }
  ngAfterViewInit(): void {
    this.dtTrigger.next(null);
  }
  ngOnDestroy(): void {
    // this.dtTrigger.unsubscribe();
  }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
    };

    this.activatedRoute.queryParams.subscribe((data) => {

      if (data.taskId != undefined) {
        this.taskId = data.taskId;
        this.getTaskDetail();
        this.getLinkedPositions();
      }
    });

    this.dataBroadcastService.positionsToLink.subscribe((res) => {
      this.PosToLink = res;
      this.linkPositionsToTask();
    });
  }
  async getLinkedPositions() {
    await this.taskSrvc.getLinkedpositions(this.taskId).then((res) => {
      res.forEach((p) => this.LinkedPositions.push(p.position));
      this.rerender();
    });
  }

  async getTaskDetail() {
    await this.taskSrvc.get(this.taskId).then((res) => {
      this.taskDetail = res;
      this.daNumber = this.taskDetail.subdutyArea.dutyArea.number;
      this.sdaNumber = this.taskDetail.subdutyArea.subNumber;
      this.tNumber = this.taskDetail.number;
    });
  }

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      if (dtInstance != undefined) {
        //   dtInstance.draw().clear();
        // Destroy the table first
        dtInstance.destroy();
        // Call the dtTrigger to rerender again
        this.dtTrigger.next(null);
      }
    });
  }

  async linkPositionsToTask() {
    let opt: TaskOptions = {
      isSignificant: false,
      positionIds: this.PosToLink,
      taskIds:[]
    };
    await this.taskSrvc.Linkpositions(this.taskId, opt).then(async (res) => {
      if (res) {
        this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.PosLinkedToTask')));
        this.getLinkedPositions();
        this.PosToLink = [];
      }
    });
  }

  async UnlinkPositionsToTask(id?: any) {
    if (id) this.PosToUnlink.push(id);
    let opt: TaskOptions = {
      isSignificant: false,
      positionIds: this.PosToUnlink,
      taskIds:[]
    };
    await this.taskSrvc.Unlinkpositions(this.taskId, opt).then(async (res) => {
      if (res) {
        this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.PosUnlinkedToTask')));
        this.getLinkedPositions();
        this.PosToUnlink = [];
      }
    });
  }

  RemoveFromLinkList(id: any) {
    const index = this.PosToUnlink.indexOf(id, 0);
    if (index == -1) this.PosToUnlink.push(id);
    else this.PosToUnlink.splice(index, 1);
  }

  async openFlyInPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
}
