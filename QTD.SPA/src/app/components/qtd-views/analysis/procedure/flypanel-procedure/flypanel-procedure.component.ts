import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Procedure } from 'src/app/_DtoModels/Procedure/Procedure';
import { ProcedureCreateOptions } from 'src/app/_DtoModels/Procedure/ProcedureCreateOptions';
import { ProcedureOptions } from 'src/app/_DtoModels/Procedure/ProcedureOptions';
import { ProcedureUpdateOptions } from 'src/app/_DtoModels/Procedure/ProcedureUpdateOptions';
import { Procedure_IssuingAuthority } from 'src/app/_DtoModels/Procedure_IssuingAuthority/Procedure_IssuingAuthority';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { IssuingAuthoritiesService } from 'src/app/_Services/QTD/issuing-authorities.service';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-flypanel-procedure',
  templateUrl: './flypanel-procedure.component.html',
  styleUrls: ['./flypanel-procedure.component.scss'],
})
export class FlypanelProcedureComponent implements OnInit {
  procObj: Procedure = new Procedure();

  IssuingAuthorityWithProc: Procedure_IssuingAuthority[] = [];
  tempIAProcList: Procedure_IssuingAuthority[] = [];

  AddEditProcMenu: boolean = false;

  isSignificant: boolean = false;
  ProcMenuMode: string = 'add';

  procId: any;
  procToLink: any[] = [];
  procToUnLink: any[] = [];
  @Input() taskId: any;

  @Output()
  TaskLinked = new EventEmitter<Event>();
  constructor(
    private translate: TranslateService,
    private alert: SweetAlertService,

    private issuingAuthSrvc: IssuingAuthoritiesService,
    private procedureSrvc: ProceduresService,
    private taskService: TasksService,
    public flyPanelSrvc: FlyInPanelService
  ) {}

  ngOnInit(): void {
    //this.getProcedures();
  }

  //filtering with procedures and procedures list
  filterProcedures(e: EventTarget | null) {
    let f = (e as HTMLInputElement).value;

    this.IssuingAuthorityWithProc = [
      ...JSON.parse(JSON.stringify(this.tempIAProcList)),
    ];

    if (f != '') {
      this.IssuingAuthorityWithProc.map((i) => {
        if (i.description.toLowerCase().includes(f.toLowerCase())) {
          i = i;
        } else {
          i.procedures = i.procedures.filter((c) =>
            c.title.toLowerCase().includes(f.toLowerCase())
          );
        } //inner if else
        return i;
      }); //outer if else
    }
  }

  // async addEditProcedure() {
    // if (this.ProcMenuMode == 'add') {
    //   let procedures = await this.procedureSrvc.getAll();
    //   let procNumber =
    //     procedures
    //       .filter(
    //         (x) => x.issuingAuthorityId == this.procObj.issuingAuthorityId
    //       )
    //       .pop()?.number ?? 0;
    //   let procOpt: ProcedureCreateOptions = {
    //     isActive: true,
    //     issuingAuthorityId: this.procObj.issuingAuthorityId,
    //     number: procNumber + 1,
    //     title: this.procObj.title,
    //     description: this.procObj.des
    //   };
    //   await this.procedureSrvc.create(procOpt).then((res) => {
    //     if (res) {
    //       this.getProcedures();
    //       this.procObj = new Procedure();
    //       this.alert.successToast(this.translate.instant('L.recAdded'));
    //       this.AddEditProcMenu = false;
    //     }
    //   });
    // } else {
    //   let procOpt: ProcedureUpdateOptions = {};
    //   await this.procedureSrvc.update(this.procId, procOpt).then((res) => {
    //     if (res) {
    //       this.getProcedures();
    //       this.procObj = new Procedure();
    //       this.alert.successToast(this.translate.instant('L.recUpdated'));
    //       this.AddEditProcMenu = false;
    //     }
    //   });
    //}
  //}

  // async getProcedures() {
  //   await this.issuingAuthSrvc.getAll().then((res) => {
  //     this.IssuingAuthorityWithProc = this.tempIAProcList = res;
  //   });
  // }

  // async LinkProcedureToTask() {
  //   let opt: TaskOptions = {
  //     isSignificant: false,
  //     procedureIds: this.procToLink,
  //   };
  //   await this.taskService.LinkProcedures(this.taskId, opt).then((res) => {
  //     if (res) {
  //       this.procToLink = [];
  //       this.alert.successToast(this.translate.instant('L.ProcLinkedToTask'));
  //       this.TaskLinked.emit();
  //     }
  //   });
  // }

  // async UnLinkProcedureFromTask() {
  //   let opt: TaskOptions = {
  //     isSignificant: false,
  //     procedureIds: this.procToUnLink,
  //   };
  //   await this.taskService.UnlinkProcedures(this.taskId, opt).then((res) => {
  //     if (res) {
  //       this.alert.successToast(this.translate.instant('L.ProcUnlinkedToTask'));
  //       this.procToUnLink = [];
  //     }
  //   });
  // }

  // openProcedurePanel(mode: string, editData?: Procedure) {
  //   this.AddEditProcMenu = true;
  //   this.ProcMenuMode = mode;
  //   this.procObj = new Procedure();
  //   if (editData) {
  //     Object.assign(this.procObj, editData);
  //     this.procId = this.procObj.id;
  //   }
  //   if (!this.IssuingAuthorityWithProc) this.getProcedures();
  // }

  // async modifyProcedureStatus(item: Procedure, action: string) {
  //   let opt: ProcedureOptions = {
  //     isSignificant: false,
  //     actionType: action,
  //   };
  //   await this.procedureSrvc.delete(item.id, opt).then((res) => {
  //     if (res) {
  //       this.getProcedures();
  //       this.alert.successToast(this.translate.instant('L.rec' + action));
  //     }
  //   });
  // }

  // AddToLinkList(id: any) {
  //   const index = this.procToLink.indexOf(id, 0);
  //   if (index == -1) this.procToLink.push(id);
  //   else this.procToLink.splice(index, 1);
  // }

  // RemoveFromLinkList(id: any) {
  //   const index = this.procToUnLink.indexOf(id, 0);
  //   if (index == -1) this.procToUnLink.push(id);
  //   else this.procToUnLink.splice(index, 1);
  // }
}
