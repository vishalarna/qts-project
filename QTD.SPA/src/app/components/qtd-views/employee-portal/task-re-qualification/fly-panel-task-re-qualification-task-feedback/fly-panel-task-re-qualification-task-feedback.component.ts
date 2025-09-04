import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { TaskOptions } from 'src/app/_DtoModels/Task/TaskOptions';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { TaskStatsCount } from 'src/app/_DtoModels/Task/TaskStatsCount';
import { GetTaskWithAllLinkData } from '@models/Task/GetTaskWithAllLinkData';
import jsPDF from 'jspdf';
import { asBlob } from 'html-docx-js-typescript';
import saveAs from 'file-saver';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';

@Component({
  selector: 'app-fly-panel-task-re-qualification-task-feedback',
  templateUrl: './fly-panel-task-re-qualification-task-feedback.component.html',
  styleUrls: ['./fly-panel-task-re-qualification-task-feedback.component.scss']
})
export class FlyPanelTaskReQualificationTaskFeedbackComponent implements OnInit {
  @Input() isOpenInFlyPanel:boolean=false;
  @Input() passedTempTaskUrl:string='';
  @Output() closed = new EventEmitter<any>();
  task: Task;
  number: any;
  isActive = true;
  dutyAreaDisable:boolean;
  subDutyAreaDisable:boolean;
  taskId = '';
  tempTaskId:string='';
  tqId:string='';
  stats: TaskStatsCount = new TaskStatsCount();
  hasLinks = 0;
  dialogTitle = '';
  dialogDesc = '';
  showMeta = false;
  regex = /<img (.*?)>/;
  allTaskData:GetTaskWithAllLinkData = new GetTaskWithAllLinkData();
  editLoader = false;
  @ViewChild('pdf') pdf: HTMLElement;
  isSuggestionsVisible:boolean=false;
  isQuestionsVisible:boolean=false;
  allTaskDataLoader:boolean = false;
  
  constructor(
    private alert: SweetAlertService,
    private store: Store<{ toggle: string }>,
    private _router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private taskService: TasksService,
    public dataBroadcastService: DataBroadcastService,
    private labelPipe: LabelReplacementPipe

  ) { }

  async ngOnInit() {
    if(this.isOpenInFlyPanel){
      this.showMeta = false;
      this.tempTaskId =this.passedTempTaskUrl;
      await this.loadTaskData();      
    }
    else{
      this.route.params.subscribe(async (params: any) => {
        if (params.hasOwnProperty('id')) {
          this.showMeta = false;
          this.tempTaskId = params['id'];
          await this.loadTaskData();
        }
      });
    }   
  }

  async getAllTaskData(){
    this.allTaskDataLoader = true;
    await this.taskService.GetAllTaskData(this.taskId).then((res:GetTaskWithAllLinkData)=>{
      this.allTaskData = res; 
    }).finally(()=>{
      this.allTaskDataLoader = false;
    })    
   }

  async loadTaskData(){
    this.taskId = String(this.tempTaskId).split('-')[0];
    this.tqId = String(this.tempTaskId).split('-')[2];
    await this.getTaskData().then(async (_) => {
      await this.getAllTaskData();
    }).finally(()=>{
      this.showMeta=true;
    });
  }

  async getTaskData() {
    this.task = await this.taskService.get(this.taskId);
    this.number = this.task.fullNumber;
    this.dutyAreaDisable = this.task.subdutyArea.dutyArea.active;
    this.subDutyAreaDisable = this.task.subdutyArea.active;
    this.isActive = this.task.active;
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

  async getSuggestionBitAsync(){
    await this.taskService.getSuggestionBit(this.tqId).then((res)=>{
      this.isSuggestionsVisible = res;
    });
  }

  async getQuestionBitAsync(){
    await this.taskService.getQuestionBit(this.tqId).then((res)=>{
      this.isQuestionsVisible = res;
    });
  }
  toggleActive(e: any) {
    let data = JSON.parse(e);
    var options: TaskOptions = new TaskOptions();

    options.actionType = this.isActive ? 'inactive' : 'active';
    options.changeNotes = data['reason'];
    options.effectiveDate = data['effectiveDate'];
    options.taskIds.push(this.task.id);

    this.taskService.delete(options).then(async (res) => {
      this.isActive = !this.isActive;
      this.alert.successToast(await this.transformTitle('Task') +` is ` + options.actionType,true);
      this.dataBroadcastService.updateMyDataNavBar.next(null);
    });
  }
  async getStats() {
    this.task.isMeta ? this.getMetaStats():this.getNonMetaStats();
  }

  async getNonMetaStats(){
    await this.taskService.getLinkedStats(this.taskId).then((res) => {
      this.stats = res;
      this.hasLinks = Object.values(this.stats).reduce((a,b) => a+b,0);

    });
  }

  async getMetaStats(){
    await this.taskService.getLinkedMetaStats(this.taskId).then((res) => {
      this.stats = res;
      this.hasLinks = Object.values(this.stats).reduce((a, b) => a + b, 0);
    });
  }
  async openActiveDialog(templateRef: any) {

    if(this.task.isMeta){
      this.dialogTitle = 'Make Meta ' + await this.transformTitle('Task') +  (!this.isActive ? 'Active' : 'Inactive');
    }
    else{
      this.dialogTitle = 'Make ' + await this.transformTitle('Task') + (!this.isActive ? 'Active' : 'Inactive');
    }
    this.dialogDesc = `You are selecting to make ` + await this.transformTitle('Task') +` "${this.number} - ${
      this.task.description
    }" ${!this.isActive ? 'Active' : 'Inactive'}`;
    const dialogRef = this.dialog.open(templateRef, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }
  async goBack() {
      this.store.dispatch(sideBarOpen());
      this._router.navigate(['emp/task-re-qualification/overview']);
  }

  async downloadPdf() {
    let doc = new jsPDF('landscape', 'pt', 'a4',true);
    doc.setFontSize(10);
    doc.setFont("sans-serif", "normal");
    doc.html(this.pdf['nativeElement'], {
      autoPaging: 'text',
      width: 100,
      margin: [5, 10, 10, 5],
      html2canvas: {
        scale: 0.7
      },
    }).then((res: any) => {
      doc.save('test.pdf');
    })
  }

  async downloadDocs() {

    const opt = {
      margin: {
        top: 100
      },
      orientation: 'landscape' as const // type error: because typescript automatically widen this type to 'string' but not 'Orient' - 'string literal type'
    }
    var doc = await asBlob(String(this.pdf['nativeElement']['innerHTML']), opt) as Blob;
    saveAs(doc, "test.docx");
  }

}
