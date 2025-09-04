import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { DataTableDirective } from 'angular-datatables';
import { from, Subject } from 'rxjs';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { AuthService } from 'src/app/_Services/Auth/auth.service';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { Task_StepCreateOptions } from 'src/app/_DtoModels/Task_Step/Task_StepCreateOptions';
import { EnablingObjective } from 'src/app/_DtoModels/EnablingObjective/EnablingObjective';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { SaftyHazard } from 'src/app/_DtoModels/SaftyHazard/SaftyHazard';
import { TaskUpdateOptions } from 'src/app/_DtoModels/Task/TaskUpdateOptions';
import { ToolGroupsService } from 'src/app/_Services/QTD/tool-groups.service';
import { ToolGroup } from 'src/app/_DtoModels/ToolGroup/ToolGroup';
import { Tool } from 'src/app/_DtoModels/Tool/Tool';
import { ToolCreateOptions } from 'src/app/_DtoModels/Tool/ToolCreateOptions';
import { ToolGroupCreateOptions } from 'src/app/_DtoModels/ToolGroup/ToolGroupCreateOptions';
import { ToolAddOptions } from 'src/app/_DtoModels/Tool/ToolAddOptions';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { jwtAuthHelper } from 'src/app/_Shared/Utils/jwtauth.helper';
import { Task_Question } from 'src/app/_DtoModels/Task_Question/Task_Question';
import { QuestionCreateOptions } from 'src/app/_DtoModels/Task_Question/QuestionCreateOptions';
import { Task_Step } from 'src/app/_DtoModels/Task_Step/Task_Step';
import { Task_StepUpdateOptions } from 'src/app/_DtoModels/Task_Step/Task_StepUpdateOptions';
import { NavBarMenuItem } from 'src/app/_DtoModels/NavBarMenuItem';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatSort } from '@angular/material/sort';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { DynamicLabelReplacementPipe } from 'src/app/_Pipes/dynamic-label-replacement.pipe';
@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.scss'],
})
export class TaskDetailComponent implements OnInit {
  addStep: boolean = false;
  editStep: boolean = false;
  OJTFullWidth: boolean = false;
  editOjtTime: boolean = false;
  editConditions: boolean = false;
  editReferences: boolean = false;
  editCriteria: boolean = false;
  editTools: boolean = false;
  addNewTool: boolean = false;
  addNewToolGroup: boolean = false;
  showToolTabs: boolean = false;
  addQuestion: boolean = false;
  showAnswer: string = '';
  toolsList: any[] = [];
  toolsGroup: any[] = [];
  showToolGroups: boolean = false;
  viewToolGroup: boolean = false;

  dtElement!: DataTableDirective;
  daNumber!: number;
  sdaNumber!: number;
  tNumber!: number;
  taskId!: any;
  DutyAreaList: DutyArea[] = [];
  taskDetail!: Task | undefined;
  stepDescription: string = '';
  LinkedSaftyHazards: SaftyHazard[] = [];
  LinkedProcedures: Procedure[] = [];
  toolToAddInGroup: string = '';
  toolGroups: ToolGroup[] = [];
  tools: Tool[] = [];
  newTool: string = '';
  newToolGroup: string = '';
  selectedToolGroup!: ToolGroup | null;
  username!: string;
  LinkedEOs: EnablingObjective[] = [];
  taskQuestions: Task_Question[] = [];
  stepEditId!: number;
  questonCreateOpt: QuestionCreateOptions = new QuestionCreateOptions();
  listDA: NavBarMenuItem[] = [];
  displayColumns: string[] = ['number', 'description', 'usage'];
  eoDataSource: MatTableDataSource<any>;
  procDataSource: MatTableDataSource<any>;
  shDataSource: MatTableDataSource<any>;

  @ViewChild('eoSort') eoSort: MatSort;
  @ViewChild('eoPaging') eoPaging: MatPaginator;

  @ViewChild('procSort') procSort: MatSort;
  @ViewChild('procPaging') procPaging: MatPaginator;
  @ViewChild('shSort') shSort: MatSort;
  @ViewChild('shPaging') shPaging: MatPaginator;

  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private taskSrvc: TasksService,
    private toolSrvc: ToolsService,
    private toolGroupSrvc: ToolGroupsService,
    private dynamicLabelReplacementPipe: DynamicLabelReplacementPipe
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);

    this.username = jwtAuthHelper.LoggedInUser;
  }

  ngOnInit(): void {

    this.activatedRoute.queryParams.subscribe((data) => {
      if (data.taskId != undefined) {

        this.taskId = data.taskId;
        this.toggleTab(0);
      }
    });
  }

  toggleTab(index: any) {

    switch (index) {
      case 0:
        this.getTaskDetail();
        break;
      case 1:
        this.getLinkedEO();
        break;
      case 2:
        this.getLinkedProcedures();
        break;
      case 3:
        this.getLinkedSaftyHazards();
        break;
      case 4:
        this.getOJTDetails();
        break;
    }
  }
  signOut() {
    this.authService.logout();
  }
  ViewOjtLink() {
    this.router.navigate(['/analysis/jta/task-ojt-link']);
  }

  AddTool() {
    this.addNewTool = true;
    this.showToolTabs = false;
    this.viewToolGroup = false;
    this.showToolGroups = false;
  }

  AddToolGroup() {
    this.addNewToolGroup = true;
    this.showToolTabs = false;
    this.viewToolGroup = false;
    this.showToolGroups = false;
  }

  ViewTool() {
    this.addNewTool = false;
    this.showToolTabs = true;
    this.viewToolGroup = false;
    this.showToolGroups = false;
    this.toolToAddInGroup = '';
    this.selectedToolGroup = null;
    this.toolsList = [];
  }

  EditTools() {
    this.editTools = true;
    this.showToolTabs = true;
    this.viewToolGroup = false;
    this.showToolGroups = false;
    this.addNewTool = false;
    this.addNewToolGroup = false;
    this.changeToolTab(0);
  }

  addToToolsList(target: any) {
    const index = this.toolsList.indexOf(target, 0);
    if (index == -1) this.toolsList.push(target);
    else this.toolsList.splice(index, 1);
  }

  addToolsToGroupList(target: any) {
    const index = this.toolsGroup.indexOf(target, 0);
    if (index == -1) this.toolsGroup.push(target);
    else this.toolsGroup.splice(index, 1);
  }

  ViewGroups(toolName: string) {
    this.addNewTool = false;
    this.showToolTabs = false;
    this.showToolGroups = true;
    this.viewToolGroup = false;
    this.toolToAddInGroup = toolName;
    this.addNewToolGroup = false;
    this.selectedToolGroup = null;
    this.getToolGroups();
  }

  ViewToolGroup_Tools(toolGroupName: string) {
    this.viewToolGroup = true;
    this.addNewTool = false;
    this.showToolTabs = false;
    this.showToolGroups = false;
    this.selectedToolGroup =
      this.toolGroups.find((x) => x.description == toolGroupName) ??
      new ToolGroup();
  }

  async addTask_Step() {
    var stepsOptions: Task_StepCreateOptions = {
      description: this.stepDescription,
      isSignificant: false,
      number: 1,
    };
    await this.taskSrvc.createSteps(this.taskId, stepsOptions).then((res) => {
      if (res) {
        this.getTaskDetail();
        this.editStep = false;
        this.addStep = false;
        this.alert.successToast(this.translate.instant('L.recAdded'));
      }
    });
  }

  async removeTask_Step(stepId: any) {
    let opt: TaskOptions = {
      isSignificant: false,
      actionType: 'delete',
      taskIds:[]
    };
    await this.taskSrvc.deleteSteps(this.taskId, stepId, opt).then((res) => {
      if (res) {
        this.getTaskDetail();
        this.alert.successToast(this.translate.instant('L.recdelete'));
      }
    });
  }

  async changeTask_StepStatus(step: Task_Step, action: string) {
    let opt: TaskOptions = {
      isSignificant: false,
      actionType: action.toLowerCase(),
      taskIds:[],
    };
    await this.taskSrvc.deleteSteps(this.taskId, step.id, opt).then((res) => {
      if (res) {
        this.getTaskDetail();
        this.alert.successToast(this.translate.instant('L.recUpdated'));
      }
    });
  }
  async updateTask_step() {
    let opt: Task_StepUpdateOptions = {
      description:""
    };
    await this.taskSrvc
      .updateSteps(this.taskId, this.stepEditId, opt)
      .then((res) => {
        if (res) {
          this.editStep = false;
          this.addStep = false;
          this.getTaskDetail();
          this.alert.successToast(this.translate.instant('L.recUpdated'));
        }
      });
  }

  editTask_Step(step: Task_Step) {
    this.editStep = true;
    this.addStep = false;
    this.stepDescription = step.description;
    this.stepEditId = step.id;
  }

  async getTaskDetail() {

    await this.taskSrvc.get(this.taskId).then((res) => {
      this.taskDetail = res;
      this.daNumber = this.taskDetail.subdutyArea.dutyArea.number;
      this.sdaNumber = this.taskDetail.subdutyArea.subNumber;
      this.tNumber = this.taskDetail.number;
    });
  }

  async getLinkedEO() {
    await this.taskSrvc.getEnablingObjectives(this.taskId).then((res) => {
      this.LinkedEOs = res;
      let tempSrc: any[] = [];
      this.LinkedEOs.forEach((eo) => {
        tempSrc.push({
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

  async getLinkedSaftyHazards() {
    await this.taskSrvc.getLinkedSaftyHazards(this.taskId).then((res) => {
      this.LinkedSaftyHazards = res;
      let tempSrc: any[] = [];
      this.LinkedSaftyHazards.forEach((sh) => {
        tempSrc.push({
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

  async getLinkedProcedures() {
    await this.taskSrvc.getLinkedProcedures(this.taskId).then((res) => {
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

  async SaveTaskDetails() {
    let opt: TaskUpdateOptions = new TaskUpdateOptions();
    Object.assign(opt, this.taskDetail);
    await this.taskSrvc.update(this.taskId, opt).then((res) => {
      if (res) {
        this.alert.successToast(this.translate.instant('L.recUpdated'));
        this.editConditions = this.editReferences = this.editCriteria = false;
        this.getTaskDetail();
      }
    });
  }

  async getToolGroups() {
    await this.toolGroupSrvc.getAll().then((res) => {
      this.toolGroups = res;
    });
  }

  async getTools() {
    await this.toolSrvc.getAll().then((res) => {
      this.tools = res;
    });
  }

  changeToolTab(index: any) {
    switch (index) {
      case 0:
        this.getTools();
        break;
      case 1:
        this.getToolGroups();
        break;
    }
  }

  async addTool() {
    let opt: any = {
      description: this.newTool,
      active: true,
    };
    await this.toolSrvc.create(opt).then(async (res) => {
      if (res) {
        if (this.selectedToolGroup?.description) {
          this.toolsList.push(res.id);
          await this.addToolsToGroup();
        }
        this.alert.successToast(this.translate.instant('L.recAdded'));
        this.addNewTool = false;
        this.newTool = '';
        this.toolsList = [];
        this.EditTools();
      }
    });
  }

  async addToolGroup() {
    let opt: ToolGroupCreateOptions = {
      description: this.newToolGroup,
      active: true,
    };
    await this.toolGroupSrvc.create(opt).then((res) => {
      if (res) {
        this.alert.successToast(this.translate.instant('L.recAdded'));
        this.addNewToolGroup = false;
        this.newToolGroup = '';
        this.EditTools();
      }
    });
  }

  async addSingleToolToMultipleGroups() {
    let toolId = this.tools.find(
      (x) => x.description == this.toolToAddInGroup
    )?.id;
    this.toolsList.push(toolId);
    let result: Tool[] = [];
    await Promise.all(
      this.toolsGroup.map(async (item) => {
        let opt: ToolAddOptions = {
          toolIds: this.toolsList,
        };
        await this.toolGroupSrvc.addTool(item, opt).then((res) => {
          if (res) {
            result.push(res);
          }
        });
      })
    );

    if (result) {
      this.alert.successToast(this.translate.instant('L.recAdded'));
      this.addNewTool = false;
      this.addNewToolGroup = false;
      this.toolToAddInGroup = '';
      this.toolsGroup = [];
      this.toolsList = [];
      this.EditTools();
    }
  }

  async addToolsToGroup() {
    let opt: ToolAddOptions = {
      toolIds: this.toolsList,
    };
    await this.toolGroupSrvc
      .addTool(this.selectedToolGroup?.id, opt)
      .then(async (res) => {
        if (res) {
          this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.ToolAddedToGroup')));
          this.addNewTool = false;
          this.addNewToolGroup = false;
          this.toolToAddInGroup = '';
          this.toolsGroup = [];
          this.toolsList = [];
          this.selectedToolGroup = new ToolGroup();
          this.EditTools();
        }
      });
  }

  async RemoveToolFromToolGroup(toolId: any, groupId: any) {
    await this.toolGroupSrvc.removeTool(groupId, toolId).then(async (res) => {
      if (res) {
        this.alert.successToast(
          await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.ToolRemovedFromGroup'))
        );
        this.addNewTool = false;
        this.addNewToolGroup = false;
        this.toolToAddInGroup = '';
        this.toolsGroup = [];
        this.toolsList = [];
        this.EditTools();
      }
    });
  }

  async LinkToolsToTask(fromToolGroup: boolean) {
    if (fromToolGroup) {
      this.toolsList = [];
      this.toolsGroup.forEach((group) => {
        let tg = this.toolGroups.find((x) => x.id == group)?.toolGroup_Tools;

        tg?.forEach((x) => this.toolsList.push(x.tool.id));
      });

      let opt: ToolAddOptions = {
        toolIds: this.toolsList,
      };
      await this.taskSrvc.LinkTool(this.taskId, opt).then(async (res) => {
        if (res) {
          this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.ToolLinkedToTask')));
          this.getTaskDetail();
          this.addNewTool = false;
          this.addNewToolGroup = false;
          this.toolToAddInGroup = '';
          this.toolsGroup = [];
          this.toolsList = [];
          this.EditTools();
        }
      });
    } else {
      let opt: ToolAddOptions = {
        toolIds: this.toolsList,
      };
      await this.taskSrvc.LinkTool(this.taskId, opt).then(async (res) => {
        if (res) {
          this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.ToolLinkedToTask')));
          this.getTaskDetail();
          this.addNewTool = false;
          this.addNewToolGroup = false;
          this.toolToAddInGroup = '';
          this.toolsGroup = [];
          this.toolsList = [];
          this.EditTools();
        }
      });
    }
  }

  async UnlinkToolFromTask(toolId: any) {
    let opt: TaskOptions = {
      isSignificant: false,
      taskIds:[]
    };
    await this.taskSrvc.UnlinkTool(this.taskId, toolId, opt).then(async (res) => {
      if (res) {
        this.alert.successToast(await this.dynamicLabelReplacementPipe.transform(this.translate.instant('L.ToolUnlinkedToTask')));
        this.getTaskDetail();
        this.addNewTool = false;
        this.addNewToolGroup = false;
        this.toolToAddInGroup = '';
        this.toolsGroup = [];
        this.toolsList = [];
        this.EditTools();
      }
    });
  }

  async addTaskQuestion() {
    await this.taskSrvc
      .addQuestion(this.taskId, this.questonCreateOpt)
      .then((res) => {
        if (res) {
          this.addQuestion = false;
          this.questonCreateOpt = new QuestionCreateOptions();
          this.getOJTDetails();
        }
      });
  }

  async removeTaskQuestion(questionId: any) {
    let opt: TaskOptions = {
      isSignificant: false,
      taskIds:[],
    };
    await this.taskSrvc
      .removeQuestion(this.taskId, questionId, opt)
      .then((res) => {
        if (res) {
          this.getOJTDetails();
        }
      });
  }

  async getOJTDetails() {
    await this.taskSrvc.getTaskQuestions(this.taskId).then((res) => {
      if (res) {
        this.taskQuestions = res;
      }
    });
  }

  showQuestionAns(answer: string) {
    if (this.showAnswer == answer) this.showAnswer = '';
    else this.showAnswer = answer;
  }

  async changeTaskStatus(status: string) {
    let options: TaskOptions = new TaskOptions();
    options.changeNotes = "";
    options.taskIds.push(this.taskId);
    options.actionType = status.toLowerCase();
    await this.taskSrvc.delete(options).then((res) => {
      if (res) {
        this.getTaskDetail();
      }
    });
  }

  filterEO(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.eoDataSource.filter = filter;
  }
  filterProc(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.procDataSource.filter = filter;
  }
  filterSh(e: Event) {
    let filter = (e.target as HTMLInputElement).value;
    this.shDataSource.filter = filter;
  }
}
