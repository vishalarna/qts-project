import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DutyArea } from 'src/app/_DtoModels/DutyArea/DutyArea';
import { DutyAreaCreateOptions } from 'src/app/_DtoModels/DutyArea/DutyAreaCreateOption';
import { NavBarMenuItem } from 'src/app/_DtoModels/NavBarMenuItem';
import { SubdutyAreaCreateOptions } from 'src/app/_DtoModels/SubdutyArea/SubdutyAreaCreateOptions';
import { TaskCreateOptions } from 'src/app/_DtoModels/Task/TaskCreateOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { DutyAreaService } from 'src/app/_Services/QTD/duty-area.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-jta-nav-bar',
  templateUrl: './jta-nav-bar.component.html',
  styleUrls: ['./jta-nav-bar.component.scss'],
})
export class JtaNavBarComponent implements OnInit {
  listDA: DutyArea[] = [];
  @Input() navList: NavBarMenuItem[] = [];
  @Output() MenuAdded = new EventEmitter<any>();
  addNewDA: boolean = false;
  isLoading: boolean = false;
  constructor(
    private dataBroadcastService: DataBroadcastService,
    private _DutyAreaService: DutyAreaService,
    private _taskService: TasksService,
    private _alert: SweetAlertService,
    private translate: TranslateService,
    private labelPipe: LabelReplacementPipe
  ) {
    const browserLang = localStorage.getItem('lang') ?? 'en';
    this.translate.use(browserLang);
  }

  ngOnInit(): void {
    this.getDutyAreas();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async getDutyAreas() {
    this.isLoading = true;
    await this._DutyAreaService
      .getAll()
      .then(async (res) => {
        this.listDA = res;
        this.navList = [];
        await res.forEach((da) => {
          let m = new NavBarMenuItem();
          m.Title = da.number + '. ' + da.letter + ' ' + da.description;
          m.RoutePath = '';

          da.subdutyAreas.forEach((sda) => {
            let child = new NavBarMenuItem();
            child.Title =
              da.number + '.' + sda.subNumber + ' ' + sda.description;
            child.RoutePath = '';

            sda.tasks.forEach((t) => {
              child.HasChildren = true;
              child.Children?.push({
                Title:
                  da.number +
                  '.' +
                  sda.subNumber +
                  ' ' +
                  t.number +
                  ' ' +
                  t.description,
                RoutePath: '/analysis/jta/task-detail/',
                disabled:t.active,
                RouteParams: { taskId: t.id },
                isVisible: true,
              });
            });
            m.HasChildren = true;
            m.Children?.push(child);
          });

          this.navList.push(m);
        });
      })
      .finally(() => (this.isLoading = false));
  }

  async AddNewSubDutyArea(daName: any, sdaName: any, ref?: any) {
    
    let addNewSDAForDA: string = String(
      daName.replace(/[^a-zA-Z ]/g, '')
    ).trim();
    let id;
    this.listDA.some((x) => {
      if (x.description == addNewSDAForDA) {
        id = x.id;
        return true;
      }
      return false;
    });

    if (!id) {
      this._alert.errorAlert('Cannot find the parent DutyArea for SubdutyArea');
      return;
    }
    let opt: SubdutyAreaCreateOptions = {
      description: sdaName,
      subNumber: 1,
    };
    await this._DutyAreaService.addSubDutyArea(id, opt).then((res) => {
      if (res) {
        this.getDutyAreas();
        this.MenuAdded.emit(res);
        this._alert.successToast(this.translate.instant('L.recAdded'));
      }
    });
  }

  async AddNewTask(sdaName: any, taskName: string, ref?: any) {
    
    let addNewTaskForSDA = String(sdaName.replace(/[^a-zA-Z ]/g, '')).trim();
    let id = 0;

    this.listDA.some((x) => {
      id = x.subdutyAreas.find((y) => y.description == addNewTaskForSDA)?.id;
      if (id) return true;
      else return false;
    });
    if (!id) {
      this._alert.errorAlert('Cannot find the parent SubDutyArea for ' + await this.transformTitle('Task'));
      return;
    }
    let opt: TaskCreateOptions = {
      conditions: '',
      critical: false,
      description: taskName,
      isSignificant: false,
      majorVersion: 1,
      minorVersion: 1,
      number: 1,
      references: '',
      requiredTime: 0,
      standards: '',
      tools: '',
      subdutyAreaId: id,
      isMeta:false,
      isReliability:false,
    };
    await this._taskService.create(opt).then((res) => {
      if (res) {
        this.getDutyAreas();
        this.MenuAdded.emit(res);
        this._alert.successToast(this.translate.instant('L.recAdded'));
      }
    });
  }

  async AddNewDutyArea(newDA: any, ref?: any) {
    
    let opt: DutyAreaCreateOptions = {
      description: newDA,
      letter: '',
      number: 1,
      effectiveDate: '',
      reasonForRevision: '',
      title: '',
    };
    await this._DutyAreaService.create(opt).then((res) => {
      if (res) {
        this.addNewDA = false;
        this.MenuAdded.emit(res);
        this.getDutyAreas();
        this._alert.successToast(this.translate.instant('L.recAdded'));
      }
    });
  }

  AddNewMenu(jsonD: string) {
    

    var d = JSON.parse(jsonD);
    var level = String(d.level).split('_')[0];
    switch (level) {
      case 'SDA':
        this.AddNewSubDutyArea(d.parent, d.data, this);
        break;
      case 'TASK':
        this.AddNewTask(d.parent, d.data, this);
        break;
    }
  }
}
