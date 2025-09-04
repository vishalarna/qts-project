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
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { SaftyHazardCreateOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardCreateOptions';
import { SaftyHazardOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardOptions';
import { SaftyHazardUpdateOptions } from 'src/app/_DtoModels/SaftyHazard/SaftyHazardUpdateOptions';
import { SaftyHazard_Category } from 'src/app/_DtoModels/SaftyHazard_Category/SaftyHazard_Category';
import { LinkSaftyHazardOptions } from 'src/app/_DtoModels/Task/LinkSaftyHazardOptions';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { SafetyHazardsService } from 'src/app/_Services/QTD/safety-hazards.service';
import { SafetyHazardCategoryService } from 'src/app/_Services/QTD/safety-hazard-category.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { TemplatePortal } from '@angular/cdk/portal';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';

@Component({
  selector: 'app-task-safty-hazard-link',
  templateUrl: './task-safty-hazard-link.component.html',
  styleUrls: ['./task-safty-hazard-link.component.scss'],
})
export class TaskSaftyHazardLinkComponent
  implements AfterViewInit, OnDestroy, OnInit
{
  tNumber!: number;
  daNumber!: number;
  sdaNumber!: number;
  taskId!: any;
  LinkedSaftyHazards: SaftyHazard[] = [];

  shToLink: any[] = [];
  shToUnLink: any[] = [];
  taskDetail!: Task;
  shDataSource: MatTableDataSource<any>;
  displayColumns: string[] = ['id', 'number', 'description', 'usage'];
  @ViewChild('shSort') shSort: MatSort;
  @ViewChild('shPaging') shPaging: MatPaginator;
  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private activatedRoute: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private taskService: TasksService,
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }
  ngAfterViewInit(): void {}
  ngOnDestroy(): void {
    // this.dtTrigger.unsubscribe();
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe((data) => {
      if (data.taskId != undefined) {
        this.taskId = data.taskId;

        this.getTaskDetail();
        this.getLinkedSaftyHazards();
      }
    });
  }

  async getLinkedSaftyHazards() {
    await this.taskService.getLinkedSaftyHazards(this.taskId).then((res) => {
      this.LinkedSaftyHazards = res;
      let tempSrc: any[] = [];
      this.LinkedSaftyHazards.forEach((sh) => {
        tempSrc.push({
          id: sh.id,
          number: `${this.daNumber}.${this.sdaNumber}.${this.tNumber}.${sh.number}`,
          description: sh.title,
          usage: sh.task_SaftyHazard_Links.length,
        });
      });

      this.shDataSource = new MatTableDataSource(tempSrc);
      this.shDataSource.sort = this.shSort;
      this.shDataSource.paginator = this.shPaging;
    });
  }

  async LinkSaftyHazardToTask() {
    let opt: TaskOptions = {
      safetyHazardIds: this.shToLink,
      isSignificant: false,
      taskIds:[],
    };
    await this.taskService.LinkSaftyHazards(this.taskId, opt).then(async (res) => {
      if (res) {
        this.getLinkedSaftyHazards();
        this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.SHLinkedToTask')));
        this.shToLink = [];
      }
    });
  }

  async UnlinkSaftyHazardFromTask() {
    let opt: TaskOptions = {
      safetyHazardIds: this.shToUnLink,
      isSignificant: false,
      taskIds:[]
    };
    await this.taskService
      .UnlinkSaftyHazards(this.taskId, opt)
      .then(async (res) => {
        if (res) {
          await this.getLinkedSaftyHazards();
          this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.SHUnlinkedToTask')));
          this.shToUnLink = [];
        }
      });
  }

  AddToLinkList(id: any) {
    const index = this.shToLink.indexOf(id, 0);
    if (index == -1) this.shToLink.push(id);
    else this.shToLink.splice(index, 1);
  }

  RemoveFromLinkList(id: any) {
    const index = this.shToUnLink.indexOf(id, 0);
    if (index == -1) this.shToUnLink.push(id);
    else this.shToUnLink.splice(index, 1);
  }

  async getTaskDetail() {
    await this.taskService.get(this.taskId).then((res) => {
      this.taskDetail = res;
      this.daNumber = this.taskDetail.subdutyArea.dutyArea.number;
      this.sdaNumber = this.taskDetail.subdutyArea.subNumber;
      this.tNumber = this.taskDetail.number;
    });
  }

  openFlyInPanel(templateRef: any) {
    let portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }
  filterTable(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.shDataSource.filter = filter;
  }
}
