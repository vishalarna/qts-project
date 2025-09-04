import { TemplatePortal } from '@angular/cdk/portal';
import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { ProcedureCreateOptions } from 'src/app/_DtoModels/Procedure/ProcedureCreateOptions';
import { ProcedureOptions } from 'src/app/_DtoModels/Procedure/ProcedureOptions';
import { ProcedureUpdateOptions } from 'src/app/_DtoModels/Procedure/ProcedureUpdateOptions';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-task-procedure-link',
  templateUrl: './task-procedure-link.component.html',
  styleUrls: ['./task-procedure-link.component.scss'],
})
export class TaskProcedureLinkComponent
  implements AfterViewInit, OnDestroy, OnInit
{
  tNumber!: number;
  daNumber!: number;
  sdaNumber!: number;
  taskId!: any;
  LinkedProcedures!: Procedure[];
  procId: any;
  procToLink: any[] = [];
  procToUnLink: any[] = [];
  taskDetail!: Task;

  procDataSource: MatTableDataSource<any>;
  displayColumns: string[] = ['id', 'number', 'description', 'usage'];
  @ViewChild('procSort') procSort: MatSort;
  @ViewChild('procPaging') procPaging: MatPaginator;
  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private activatedRoute: ActivatedRoute,
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
        this.getLinkedProcedures();
      }
    });
  }

  async getLinkedProcedures() {
    await this.taskService.getLinkedProcedures(this.taskId).then((res) => {
      this.LinkedProcedures = res;
      let tempSrc: any[] = [];
      this.LinkedProcedures.forEach((p) => {
        tempSrc.push({
          number: `${this.daNumber}.${this.sdaNumber}.${this.tNumber}.${p.number}`,
          description: p.title,
          usage: p.task_Procedure_Links.length,
        });
      });

      this.procDataSource = new MatTableDataSource(tempSrc);
      this.procDataSource.sort = this.procSort;
      this.procDataSource.paginator = this.procPaging;
    });
  }

  async LinkProcedureToTask() {
    let opt: TaskOptions = {
      isSignificant: false,
      procedureIds: this.procToLink,
      taskIds:[],
    };
    await this.taskService.LinkProcedures(this.taskId, opt).then(async (res) => {
      if (res) {
        this.procToLink = [];
        this.getLinkedProcedures();
        this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.ProcLinkedToTask')));
      }
    });
  }

  async UnLinkProcedureFromTask() {
    let opt: TaskOptions = {
      isSignificant: false,
      procedureIds: this.procToUnLink,
      taskIds:[],
    };
    await this.taskService.UnlinkProcedures(this.taskId, opt).then(async (res) => {
      if (res) {
        this.getLinkedProcedures();
        this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.ProcUnlinkedToTask')));
        this.procToUnLink = [];
      }
    });
  }

  AddToLinkList(id: any) {
    const index = this.procToLink.indexOf(id, 0);
    if (index == -1) this.procToLink.push(id);
    else this.procToLink.splice(index, 1);
  }

  RemoveFromLinkList(id: any) {
    const index = this.procToUnLink.indexOf(id, 0);
    if (index == -1) this.procToUnLink.push(id);
    else this.procToUnLink.splice(index, 1);
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
    this.procDataSource.filter = filter;
  }

}
