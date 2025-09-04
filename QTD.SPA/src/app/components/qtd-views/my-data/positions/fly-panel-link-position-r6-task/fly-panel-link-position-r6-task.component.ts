import {Component,EventEmitter,Input,OnInit,Output,ViewChild} from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { PositionTaskService } from 'src/app/_Services/QTD/Position_Task/api.positiontask.service';
import { UpdateMarkAsR6Model } from 'src/app/_DtoModels/Position_Task/UpdateMarkAsR6Model';
import { Position_HistoryCreateOptions } from 'src/app/_DtoModels/PositionHistory/PositionHistoryCreateOptions';
import { SubSink } from 'subsink';
import { ActivatedRoute } from '@angular/router';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-link-position-r6-task',
  templateUrl: './fly-panel-link-position-r6-task.component.html',
  styleUrls: ['./fly-panel-link-position-r6-task.component.scss'],
})
export class FlyPanelLinkPositionR6TaskComponent implements OnInit {
  @Input() selectedTasks: any[] = [];
  selectedTaskData = new MatTableDataSource<any>();
  @ViewChild(MatSort) sort: MatSort;

  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  displayColumns: string[] = ['number', 'description'];
  modalHeader: string = '';
  modalReason: string = '';
  modalDescription: string = '';
  positionTaskIds : string[];
  updateMarkAsR6Model: UpdateMarkAsR6Model;
  posId = '';
  subscription = new SubSink();
  isLoading = false;

  constructor(
    private alert: SweetAlertService,
    public taskSortPipe: TaskSortPipePipe,
    public dialog: MatDialog,
    private posTaskService: PositionTaskService,
    private route: ActivatedRoute,
    private labelPipe: LabelReplacementPipe
    ) {}

  ngOnInit(): void {
    this.initializeUpdateMarkAsR6Model();
    this.selectedTaskData = new MatTableDataSource(this.selectedTasks);
  }

  ngAfterViewInit(): void {
    // To Get ID from route parameter
    this.subscription.sink = this.route.params.subscribe((res: any) => {
      this.posId = res.id;
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  initializeUpdateMarkAsR6Model(){
    this.updateMarkAsR6Model = new UpdateMarkAsR6Model();
  }

  sortDataTest() {
    var data = this.selectedTaskData.data;
    if (this.sort.direction === '') {
      this.selectedTaskData = new MatTableDataSource(this.selectedTasks);
    } else {
      this.selectedTaskData = this.taskSortPipe.transform(
        data,
        this.sort.direction,
        this.sort.active
      );
    }
  }
  async getDataAsync(e: any) {
      let response = JSON.parse(e);
      let effectiveDate = response.effectiveDate;
      let reason = response.reason;
      this.updateMarkAsR6Model.positionTaskIds = this.positionTaskIds;
      this.updateMarkAsR6Model.position_HistoryCreateOptions = new Position_HistoryCreateOptions();
      this.updateMarkAsR6Model.position_HistoryCreateOptions.positionId = this.posId;
      this.updateMarkAsR6Model.position_HistoryCreateOptions.effectiveDate = effectiveDate;
      this.updateMarkAsR6Model.position_HistoryCreateOptions.changeNotes = reason;
      
      this.isLoading = true;
      await this.posTaskService
      .updateMarkAsR6Async(this.updateMarkAsR6Model)
      .then(async (res: any) => {
        this.refresh.emit();
        this.alert.successToast(
          'Successfully marked '+ await this.transformTitle('Task') + '(s) as R6 Impacted'
        );
        this.closed.emit('fp-pos-r6-task-link-closed');
      })
      .finally(() => {
        this.isLoading = false;
      });
  }

  async openR6MarkDialogue(templateRef: any) {
    this.positionTaskIds=[];
    this.modalHeader = 'Link R-R '+ await this.transformTitle('Task') +'s';
    this.modalReason ='Please provide Effective Date and Reason for this change';
    this.modalDescription = 'You are selecting to mark the following ' + await this.transformTitle('Task')+ 's as R6 impacted\n\n';
      this.selectedTasks.forEach(async (d) => {
        this.modalDescription += await this.transformTitle('Task') + d.number + ' - ' + d.description + '\n';
        this.positionTaskIds.push(d.positionTaskId);
      });
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
}
