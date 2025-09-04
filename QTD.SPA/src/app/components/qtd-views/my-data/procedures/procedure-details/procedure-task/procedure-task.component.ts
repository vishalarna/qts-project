import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort, SortDirection } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { Procedure_StatusHistory } from 'src/app/_DtoModels/Procedure_StatusHistory/Procedure_StatusHistory';
import { Procedure_StatusHistoryCreateOptions } from 'src/app/_DtoModels/Procedure_StatusHistory/Procedure_StatusHistoryCreateOptions';
import { Procedure_Task_LinkOptions } from 'src/app/_DtoModels/Procedure_Task_Link/Procedure_Task_LinkOptions';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TaskWithCountOptions } from 'src/app/_DtoModels/Task/TaskWithCountOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-procedure-task',
  templateUrl: './procedure-task.component.html',
  styleUrls: ['./procedure-task.component.scss'],
})
export class ProcedureTaskComponent
  implements OnInit, AfterViewInit, OnDestroy {
  @Output() procedureDeleteCheck = new EventEmitter<any>();
  @Input() isActive: any;
  @Input() procTitle: any;
  displayColumns: string[] = ['id', 'number', 'description', 'linkageCount'];
  DataSource: MatTableDataSource<any>;
  OriginaldataSource: MatTableDataSource<any>;
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  addTask: boolean = false;
  unlinkDescription = '';
  taskId: any[] = [];
  srcList: any[] = [];
  subscription = new SubSink();
  title = '';
  taskIdToShow = '';
  linkedIds: any[] = [];
  taskNumber: any;

  // @ViewChild(MatSort) sort: MatSort
  /*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) paginator:MatPaginator;

  @Input() id = '';

  constructor(
    public flypanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    public dialog: MatDialog,
    public procService: ProceduresService,
    private alert: SweetAlertService,
    private route: ActivatedRoute,
    private dataBroadcastService: DataBroadcastService,
    private taskSortPipe : TaskSortPipePipe,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void { }

  ngAfterViewInit(): void {
    // To Get ID from route parameter
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.id = res.id;
      this.getTaskLinkages();
    });

    // To refresh tasks when we link from fly panel.
    this.subscription.sink =
      this.dataBroadcastService.updateProcTaskLink.subscribe((res: any) => {
        this.getTaskLinkages();
      });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  sortData(sort: Sort) {
    // if (sort.active === "number" && sort.direction !== '') {
    //   this.DataSource.data = this.OriginaldataSource.data.sort((a, b) => {
    //     var splitNumA = a.number.split('.');
    //     var splitNumB = b.number.split('.');
    //     var letterA = splitNumA[0][0];
    //     var letterB = splitNumB[0][1];
    //     var daNumberA = Number.parseInt(splitNumA[1]);
    //     var daNumberB = Number.parseInt(splitNumB[1]);
    //     var sdaNumA = Number.parseInt(splitNumA[1]);
    //     var sdaNumB = Number.parseInt(splitNumB[1]);
    //     var taskNumA = Number.parseInt(splitNumA[2]);
    //     var taskNumB = Number.parseInt(splitNumB[2]);
    //     // if (sort.direction === 'asc') {
    //     //   if (letterA < letterB) {
    //     //     return -1
    //     //   } else if (letterA > letterB) {
    //     //     return 1;
    //     //   }

    //     //   if (daNumberA < daNumberB) {
    //     //     return -1
    //     //   }
    //     //   else if (daNumberA > daNumberB) {
    //     //     return 1
    //     //   }

    //     //   if (Number.parseInt(sdaNumA) < Number.parseInt(sdaNumB)) {
    //     //     return -1
    //     //   }
    //     //   else if (Number.parseInt(sdaNumA) > Number.parseInt(sdaNumB)) {
    //     //     return 1
    //     //   }

    //     //   if (Number.parseInt(taskNumA) < Number.parseInt(taskNumB)) {
    //     //     return -1
    //     //   }
    //     //   else if (Number.parseInt(taskNumA) > Number.parseInt(taskNumB)) {
    //     //     return 1
    //     //   }
    //     //   else {
    //     //     return 0;
    //     //   }
    //     // }
    //     // else {
    //     //   if (letterA < letterB) {
    //     //     return 1
    //     //   } else if (letterA > letterB) {
    //     //     return -1;
    //     //   }

    //     //   if (Number.parseInt(daNumberA) < Number.parseInt(daNumberB)) {
    //     //     return 1
    //     //   }
    //     //   else if (Number.parseInt(daNumberA) > Number.parseInt(daNumberB)) {
    //     //     return -1
    //     //   }

    //     //   if (Number.parseInt(sdaNumA) < Number.parseInt(sdaNumB)) {
    //     //     return 1
    //     //   }
    //     //   else if (Number.parseInt(sdaNumA) > Number.parseInt(sdaNumB)) {
    //     //     return -1
    //     //   }

    //     //   if (Number.parseInt(taskNumA) < Number.parseInt(taskNumB)) {
    //     //     return 1
    //     //   }
    //     //   else if (Number.parseInt(taskNumA) > Number.parseInt(taskNumB)) {
    //     //     return -1
    //     //   }
    //     //   else {
    //     //     return 0;
    //     //   }
    //     // }
    //     // return this.compare(letterA,letterB,sort.direction) && this.compare(daNumberA,daNumberB,sort.direction)
    //     //     && this.compare(sdaNumA,sdaNumB,sort.direction) && this.compare(taskNumA,taskNumB,sort.direction);
    //     //return 0;
    //   })
    // }
    // else {
      this.DataSource.data = this.taskSortPipe.transform(this.OriginaldataSource.data,sort.direction,sort.active).data;
    // }
  }

  compare(a: any, b: any, order: SortDirection) {
    if (order === 'asc') {
      if (a < b) return +1;
      if (a > b) return -1;
      return 0;
    }
    else {
      if (a < b) return -1;
      if (a > b) return +1;
      return 0;
    }
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  async getTaskLinkages() {
    let tempSrc: any[] = [];
    this.linkedIds = [];
    await this.procService
      .getLinkedTasks(this.id)
      .then((res: TaskWithCountOptions[]) => {
        res.forEach((data: TaskWithCountOptions) => {
          this.linkedIds.push(data.id);
          tempSrc.push({
            number: (data.isRR ? "*":"") + data.number,
            description: data.description,
            linkageCount: data.linkCount,
            id: data.id,
            active: data.active,
            isRR:data.isRR,
          });
        });

        this.srcList = tempSrc;
        //tempSrc = this.sortTasks(tempSrc);
        this.DataSource = new MatTableDataSource(tempSrc);
        this.OriginaldataSource = new MatTableDataSource(tempSrc);

        if (tempSrc.length > 0) {
          this.procedureDeleteCheck.emit(true);
        }
        else {
          this.procedureDeleteCheck.emit(false);
        }
        this.DataSource.data = this.taskSortPipe.transform(this.OriginaldataSource.data,'asc','number').data;
        setTimeout(()=>{
          this.DataSource.paginator = this.paginator;
        },1)
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Fetching linked' + await this.transformTitle('Task')+ 's');
      });
  }

  // sortTasks(inputArr : any[]){
  //   return  inputArr.sort((a, b) => {
  //      const inputA = a.number.split('.').map(Number);
  //      const inputB = b.number.split('.').map(Number);

  //      for (let i = 0; i < Math.max(inputA.length, inputB.length); i++) {
  //        const partA = inputA[i] || 0;
  //        const partB = inputB[i] || 0;

  //        if (partA !== partB) {
  //          return partA - partB;
  //        }
  //      }

  //      return 0;
  //    });
  //  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  openFlypanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  openProcedureLinkedViewFlyPanel(templateRef: any, data: any) {

    this.title = data.description;
    this.taskIdToShow = data.id;
    this.taskNumber = data.number;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flypanelSrvc.open(portal);
  }

  // data containing effective date and reason to store in history table and then unlink
  async getData(e: any) {

    if (this.taskId.length > 0) {
      var options = new Procedure_Task_LinkOptions();
      options.procedureId = this.id;
      options.taskIds = this.taskId;
      await this.procService
        .UnlinkTasks(this.id, options)
        .then(async (res: any) => {
          this.selection.clear();
          this.unlinkIds = [];
          this.removeFromLinked();
          this.saveHistory(e);
          this.getTaskLinkages();
          this.alert.successToast(
            'Successfully Unlinked ' + await this.transformTitle('Task') + '(s) from ' + await this.transformTitle('Procedure') 
          );
        })
        .catch(async (err: any) => {
          this.alert.errorToast('Error Unlinking ' + await this.transformTitle('Task')  + err);
        });
    }
  }

  removeFromLinked() {
    

    this.linkedIds = this.linkedIds.filter((id: any) => {
      return !this.taskId.includes(id);
    });

  }

  async saveHistory(e: any) {
    var options = new Procedure_StatusHistoryCreateOptions();

    var data = JSON.parse(e);
    options.changeEffectiveDate = data['effectiveDate'];
    options.changeNotes = data['reason'];
    options.oldStatus = true;
    options.newStatus = false;
    options.procedureIds.push(this.id);
    await this.procService
      .saveStatusHistory(options)
      .then(async (res: Procedure_StatusHistory) => {
        this.alert.successAlert(await this.transformTitle('Task') +' Unlinked And History Saved');
      })
      .catch(async (err: any) => {
        this.alert.errorToast('Error Saving ' + await this.transformTitle('Procedure') + ' Status History ' + err);
      });
  }

  async unlinkItemsModal(templateRef: any, id?: any) {
    this.unlinkDescription = 'You are selecting to unlink the following '+ await this.transformTitle('Task') +'s\n';
    this.taskId = [];
    if (id) {
      this.taskId.push(id);
      this.unlinkDescription +=
        this.srcList.find((x) => x.id == id).number + ' - ' + this.srcList.find((x) => x.id == id).description + '\n';
    } else {
      this.unlinkIds.forEach((d, i) => {
        this.taskId.push(d);
        this.unlinkDescription +=
          this.srcList.find((x) => x.id == d).number + ' - ' + this.srcList.find((x) => x.id == d).description + '\n';
      });
      // this.unlinkDescription = this.removeLastWord(this.unlinkDescription);
    }
    this.unlinkDescription += ' \n' + 'from ' + await this.transformTitle('Procedure') + 's ' + this.procTitle;

    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((res: any) => {

    });
  }

  removeLastWord(str: string) {
    const lastIndexOfSpace = str.lastIndexOf(' ');

    if (lastIndexOfSpace === -1) {
      return str;
    }
    return str.substring(0, lastIndexOfSpace);
  }

}
