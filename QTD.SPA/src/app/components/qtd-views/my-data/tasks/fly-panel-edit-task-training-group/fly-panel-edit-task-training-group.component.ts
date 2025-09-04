import { SelectionModel } from '@angular/cdk/collections';
import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Task_TrainingGroupOptions } from 'src/app/_DtoModels/Task_TrainingGroup/Task_TrainingGroupOptions';
import { TrainingGroup } from 'src/app/_DtoModels/TrainingGroup/TrainingGroup';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-edit-task-training-group',
  templateUrl: './fly-panel-edit-task-training-group.component.html',
  styleUrls: ['./fly-panel-edit-task-training-group.component.scss']
})
export class FlyPanelEditTaskTrainingGroupComponent implements OnInit,AfterViewInit {
  @Output() closed = new EventEmitter<any>();
  @Output() refresh = new EventEmitter<any>();
  alreadyLinked: any[] = [];
  @Input() taskId = "";
  @Input() taskDesc:any;
  unlinkDescription = "";

  trainingGroupIds:any[] = [];

  spinner = false;
  selection = new SelectionModel<any>(true, []);
  DataSource: MatTableDataSource<any>;
  unlinkIds: any[] = [];
  displayColumns: string[] = ['id', 'name'];
  trainingGroups : TrainingGroup[];
  showGroups = true;
  linkGroups = false;

  constructor(
    private alert: SweetAlertService,
    private taskService: TasksService,
    private dataBroadcastService : DataBroadcastService,
    public dialog : MatDialog,
    private labelPipe: LabelReplacementPipe
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.readyLinkData();
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async readyLinkData(){
    this.selection.clear();
    this.unlinkIds = [];
    this.trainingGroups = await this.taskService.getLinkedTrainingGroups(this.taskId);
    this.alreadyLinked = [];
    this.trainingGroups.forEach((data)=>{
      this.alreadyLinked.push(data.id);
    });
    this.DataSource = new MatTableDataSource(this.trainingGroups);
  

    
  }

  refreshStats(){
    this.dataBroadcastService.refreshTaskStats.next(null);
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.DataSource.data.length;
    return numSelected === numRows;
  }

  selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.DataSource.data.forEach((row) => this.selection.select(row));

    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  async openUnlinkDialog(templateRef:any){
    this.unlinkDescription = 'You are selecting to unlink the follwoing Training Groups\n';
    this.trainingGroupIds = [];

      this.unlinkIds.forEach((d, i) => {
        this.trainingGroupIds.push(d);
        this.unlinkDescription +=
          ` ${i + 1} - ` +
          this.trainingGroups.find((x) => x.id == d)?.groupName +
          '\n';
      });
      this.unlinkDescription +='\n' + 'from ' + await this.transformTitle('Task') + this.taskDesc;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  getData(e:any){
    this.unlinkTrainingGroups();
  }

  async unlinkTrainingGroups(){
    this.spinner = true;
    var options = new Task_TrainingGroupOptions();
    options.taskId = this.taskId;
    options.trainingGroupIds = this.unlinkIds;
    await this.taskService.unlinkTrainingGroups(options).then((_)=>{
      this.alert.successToast("Training Groups unlinked");
      this.refresh.emit();
      this.refreshStats();
    }).finally(()=>{
      this.spinner = false;
    })
  }

}
