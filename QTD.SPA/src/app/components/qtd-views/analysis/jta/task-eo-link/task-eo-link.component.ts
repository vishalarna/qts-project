import { TemplatePortal } from '@angular/cdk/portal';
import {
  AfterContentInit,
  AfterViewInit,
  Component,
  Input,
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
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { EnablingObjectiveCreateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveCreateOptions';
import { EnablingObjectiveOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveOption';
import { EnablingObjectiveUpdateOptions } from 'src/app/_DtoModels/EnablingObjective/EnablingObjectiveUpdateOptions';
import { EnablingObjective_Category } from 'src/app/_DtoModels/EnablingObjective_Category/EnablingObjective_Category';
import { EnablingObjective_SubCategory } from 'src/app/_DtoModels/EnablingObjective_SubCategory/EnablingObjective_SubCategory';
import { EnablingObjective_Topic } from 'src/app/_DtoModels/EnablingObjective_Topic/EnablingObjective_Topic';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
import { EnablingObjectivesCategoryService } from 'src/app/_Services/QTD/enabling-objectives-category.service';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-task-eo-link',
  templateUrl: './task-eo-link.component.html',
  styleUrls: ['./task-eo-link.component.scss'],
})
export class TaskEOLinkComponent
  implements AfterContentInit, OnDestroy, OnInit
{
  daNumber!: number;
  sdaNumber!: number;
  tNumber!: number;
  taskId!: any;
  LinkedEOs: EnablingObjective[] = [];
  taskDetail!: Task;
  EOToLink: any[] = [];
  EOToUnLink: any[] = [];

  @ViewChild('eoSort') eoSort: MatSort;
  @ViewChild('eoPaging') eoPaging: MatPaginator;
  displayColumns: string[] = ['id', 'number', 'description', 'usage'];
  eoDataSource: MatTableDataSource<any>;

  constructor(
    private translate: TranslateService,
    private activatedRoute: ActivatedRoute,
    private taskService: TasksService,
    private alert: SweetAlertService,
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private labelPipe: DynamicLabelReplacementPipe,
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }
  ngAfterContentInit(): void {}
  ngOnDestroy(): void {
    // this.dtTrigger.unsubscribe();
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(async (data) => {
      if (data.taskId != undefined) {

        this.taskId = data.taskId;
        await this.getTaskDetail().then((_) => this.getLinkedEO());
      }
    });
  }

  async getLinkedEO() {
    await this.taskService.getEnablingObjectives(this.taskId).then((res) => {
      this.LinkedEOs = res;
      let tempSrc: any[] = [];
      this.LinkedEOs.forEach((eo) => {
        tempSrc.push({
          id: eo.id,
          number: `${this.daNumber}.${this.sdaNumber}.${this.tNumber}.${eo.number}`,
          description: eo.statement,
          usage: eo.task_EnablingObjective_Links.length,
        });
      });

      this.eoDataSource = new MatTableDataSource(tempSrc);
      this.eoDataSource.sort = this.eoSort;
      this.eoDataSource.paginator = this.eoPaging;
    });
  }
  AddToLinkList(id: any) {
    const index = this.EOToLink.indexOf(id, 0);
    if (index == -1) this.EOToLink.push(id);
    else this.EOToLink.splice(index, 1);
  }

  RemoveFromLinkList(id: any) {
    const index = this.EOToUnLink.indexOf(id, 0);
    if (index == -1) this.EOToUnLink.push(id);
    else this.EOToUnLink.splice(index, 1);
  }

  async getTaskDetail() {
    await this.taskService.get(this.taskId).then((res) => {
      this.taskDetail = res;
      this.daNumber = this.taskDetail.subdutyArea.dutyArea.number;
      this.sdaNumber = this.taskDetail.subdutyArea.subNumber;
      this.tNumber = this.taskDetail.number;
    });
  }

  async UnlinkEOFromTask() {
    let opt: TaskOptions = {
      isSignificant: false,
      enablingObjectiveIds: this.EOToUnLink,
      taskIds:[]
    };
    await this.taskService
      .UnlinkEnablingObjective(this.taskId, opt)
      .then(async (res) => {
        if (res) {
          this.alert.successToast(await this.transformTitle(this.translate.instant('L.EOLinkedToTask')));
          this.EOToUnLink = [];
        }
      });
  }

  openFlyInPanel(templateRef: any) {
    let portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  filterEO(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.eoDataSource.filter = filter;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }
}
