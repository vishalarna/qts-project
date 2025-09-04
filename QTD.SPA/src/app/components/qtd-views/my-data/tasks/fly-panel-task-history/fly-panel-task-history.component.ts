import { SelectionModel } from '@angular/cdk/collections';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { Version_Task } from 'src/app/_DtoModels/Version_Task/Version_Task';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { TaskRequalificationOptions } from 'src/app/_DtoModels/Task/TaskRequalificationOptions';
import { DatePipe } from '@angular/common';
import { Version_TaskUpdateOptions } from 'src/app/_DtoModels/Version_Task/Version_TaskUpdateOptions';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-task-history',
  templateUrl: './fly-panel-task-history.component.html',
  styleUrls: ['./fly-panel-task-history.component.scss'],
})
export class FlyPanelTaskHistoryComponent implements OnInit {
  displayedColumns: string[] = ['name', 'desc', 'modifyDate', 'effectiveDate'];
  displayedColumnsHistory: string[] = ['versionNumber', 'effectiveDate', 'modifiedBy', 'actions','requalificationRequired']
  dataSource: MatTableDataSource<any>;
  datepipe = new DatePipe('en-us');
  requalificationItem: any;
  spinner: boolean;
  restoreSpinner = false;
  mode: string = "update";
  effectiveDate!: any;
  @Input() TaskId: any;
  @Input() task !: Task;
  @Input() number: string = "";
  @Output() refresh = new EventEmitter<any>();
  @ViewChild('sort', { static: false }) sort!: MatSort
  @ViewChild('sort1', { static: false }) sort1!: MatSort
  @ViewChild(MatPaginator, { static: false }) set tblPaging(paginator: MatPaginator) {
    if (paginator && this.dataSource) this.dataSource.paginator = paginator;
  }

  selection = new SelectionModel<any>(true, []);

  viewVersion = false;
  isEditable = false;
  compareVersion = false;
  toView!: Version_Task;
  toCompare: Version_Task[] = [];
  linkedPossitions: string[] = [];
  orderedVersions: any[] = [];
  RequalificationhistoryForm = new UntypedFormGroup({});
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private taskSrvc: TasksService,
    private alert: SweetAlertService,
    private router: Router,
    public dialog: MatDialog,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    if (this.TaskId) {
      this.readyForm();
    } else {
      this.getLatestActivity();
    }
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async readyForm() {
    this.RequalificationhistoryForm.addControl('requalificationDueDate', new UntypedFormControl(''));
    this.RequalificationhistoryForm.addControl('requalificationRequired', new UntypedFormControl(false));
    this.RequalificationhistoryForm.addControl('requalificationFormNotes', new UntypedFormControl(''));
    // this.RequalificationhistoryForm.controls['requalificationFormNotes'].disable()
    this.mode = "update";
    await this.getTaskHistory();
  }

  readyFormInsertion() {
    this.mode = this.requalificationItem.requalificationRequired === true ? "update" : "new";
    this.isEditable = this.requalificationItem.requalificationRequired !== null ? false : true;
    var effectiveDate = new Date(this.requalificationItem.effectiveDate);
    var reqDate = this.requalificationItem.requalificationDueDate === null ? effectiveDate.setMonth((new Date(this.requalificationItem.effectiveDate)).getMonth() + 6) : this.requalificationItem.requalificationDueDate
    this.RequalificationhistoryForm.get('requalificationDueDate')?.setValue(this.datepipe.transform(reqDate, 'yyyy-MM-dd'));
    this.RequalificationhistoryForm.get('requalificationRequired')?.setValue(this.requalificationItem.requalificationRequired ?? false);
    this.RequalificationhistoryForm.get('requalificationFormNotes')?.setValue(this.requalificationItem.requalificationNotes ?? "");

  }

  EditableClicked() {
    this.isEditable = true;

  }
  async getLatestActivity() {
    this.spinner = true;
    await this.taskSrvc
      .getlatestActivity()
      .then((res) => {

        let tempSrc: any[] = [];
        res.forEach((h, i) => {
          tempSrc.push({
            index: i,
            id: h.id,
            name: h.title,
            desc: h.activityDesc,
            modifyBy: h.createdBy,
            modifyDate: h.createdDate,
            effectiveDate: h.effectiveDate,
          });
        });
        this.dataSource = new MatTableDataSource(tempSrc);
        setTimeout(()=>{

          this.dataSource.sort = this.sort;
        },1)
      })
      .finally(() => {
        this.spinner = false;
      });
  }
  async updateTaskVersionData(templateRef: any) {
    if (this.mode === 'update' && this.requalificationItem.requalificationRequired === true && this.RequalificationhistoryForm.get("requalificationRequired")?.value === false) {
      this.dialog.open(templateRef, {
        width: '600px',
        height: 'auto',
        hasBackdrop: true,
        disableClose: true,
      })
    }
    else {
      this.updateRequal();
    }
  }

  async updateRequal() {
    let opt: TaskRequalificationOptions = new TaskRequalificationOptions();
    opt.RequalificationDueDate = this.RequalificationhistoryForm.get("requalificationDueDate")?.value;
    opt.RequalificationNotes = this.RequalificationhistoryForm.get("requalificationFormNotes")?.value;
    opt.RequalificationRequired = this.RequalificationhistoryForm.get("requalificationRequired")?.value;
    opt.effectiveDate = this.effectiveDate;
    opt.versionId = this.toView.id;

    await this.taskSrvc.updateTaskRequalificationInfo(this.requalificationItem.taskId, opt)
      .then(async (res) => {
        this.alert.successToast(await this.transformTitle('Task') +' updated Successfully');
        this.getTaskHistory();
      this.toView.effectiveDate = opt.effectiveDate;
        this.refresh.emit();
      });
  }

  async getTaskHistory() {
    this.spinner = true;
    await this.taskSrvc
      .getTaskVersions(this.TaskId)
      .then(async (res) => {
        res.sort((a, b) => {
          if (b.versionNumber !== a.versionNumber) {
            return b.versionNumber - a.versionNumber;
          }
        
          const dateA = new Date(a.effectiveDate).getTime();
          const dateB = new Date(b.effectiveDate).getTime();
          return dateB - dateA;
        });
        
        const tempSrc = res.map((h, i) => ({
          index: i,
          id: h.id,
          name: h.description,
          modifyBy: h.createdBy,
          modifyDate: h.createdDate,
          versionNumber: h.versionNumber,
          inUse: h.isInUse,
          requalificationRequired: h.requalificationRequired,
          requalificationDueDate: h.requalificationDueDate,
          requalificationNotes: h.requalificationNotes,
          taskId: h.taskId,
          effectiveDate: h.effectiveDate,
        }));
        this.requalificationItem = tempSrc.find((x) => x.inUse === true);
        this.dataSource = new MatTableDataSource(res);
        var selected = this.dataSource.data.find((x) => x.isInUse === true);
        if (selected !== null && selected !== undefined) {
          this.selection.select(selected);
        }


        if (!this.task.isMeta) {
          if (this.requalificationItem !== undefined) {
            var positions = await this.taskSrvc.getLinkedpositions(this.requalificationItem.taskId);
            positions.forEach((x) => {
              this.linkedPossitions.push(x.position.positionTitle);
            });
          }
        }
        else {
          var positions = await this.taskSrvc.getLinkedPositionWithMetaTaskCount(this.requalificationItem.taskId);
          positions.forEach((pos) => {
            this.linkedPossitions.push(pos.position.positionTitle);
          })
        }
        if (this.requalificationItem !== undefined) {
          this.readyFormInsertion();
        }

        setTimeout(() => {

          this.dataSource.sort = this.sort1;
        }, 1)
      })
      .finally(() => {
        this.spinner = false;
      });
  }

  filterData(e: any) {
    this.dataSource.filter = e.target.value;
  }

  async restoreHistory(histId: any) {

    this.restoreSpinner = true;
    await this.taskSrvc.restoreHistory(this.TaskId, histId).then(async (_) => {
      this.alert.successToast(await this.transformTitle('Task') +" Restored To Selected version");
      this.refresh.emit();
    }).finally(() => {
      this.restoreSpinner = false;
    })
  }

  selectionChanged(event: any, row: any) {

    if (event.checked) {
      if (this.selection.selected.length > 1) {
        this.selection.deselect(this.selection.selected[1]);
        this.selection.select(row);
      }
      else {
        this.selection.select(row);
        this.requalificationItem = this.selection.selected[0];
        this.readyFormInsertion();
      }
    }
    else {
      this.selection.deselect(row);
      if (this.selection.selected.length >= 1) {
        this.requalificationItem = this.selection.selected[0];
        this.readyFormInsertion();
      }
    }

  }

  compareLogic() {
    this.compareVersion = true;
    if (this.selection.selected[0].versionNumber > this.selection.selected[1].versionNumber) {
      this.toView = this.selection.selected[0];
      this.orderedVersions[0] = this.selection.selected[0].versionNumber;
      this.orderedVersions[1] = this.selection.selected[1].versionNumber;
    }
    else {
      this.toView = this.selection.selected[1];
      this.orderedVersions[0] = this.selection.selected[1].versionNumber;
      this.orderedVersions[1] = this.selection.selected[0].versionNumber;
    }
  }

  changeView(row: Version_Task) {
    this.toView = row;
    this.effectiveDate = this.datepipe.transform(this.toView.effectiveDate, 'yyyy-MM-dd');
    this.RequalificationhistoryForm.get('requalificationRequired')?.setValue(row.requalificationRequired ?? false);
    this.RequalificationhistoryForm.get('requalificationDueDate')?.setValue(this.datepipe.transform(row.requalificationDueDate, 'yyyy-MM-dd'));
    this.RequalificationhistoryForm.get('requalificationFormNotes')?.setValue(row.requalificationNotes);
    this.viewVersion = true;
    this.isEditable = false;
  }
  
  clearSearch: string;
  clearFilter(){
    this.dataSource.filter = '';
    this.clearSearch = null;
  }
}
