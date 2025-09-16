import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { sideBarOpen } from 'src/app/_Statemanagement/action/state.menutoggle';

@Component({
  selector: 'app-fly-panel-task-re-qualification-comp-feedback',
  templateUrl: './fly-panel-task-re-qualification-comp-feedback.component.html',
  styleUrls: ['./fly-panel-task-re-qualification-comp-feedback.component.scss']
})
export class FlyPanelTaskReQualificationCompFeedbackComponent implements OnInit {
  tempIds: string = '';
  qualificationId: string = '';
  traineeId:string =''
  images: any[] = [];
  collapseImage: boolean[] = [];
  datePipe = new DatePipe('en-us');
  taskStepList:number;
  taskQuestionAnswer:number;
  checkType:string;
  skillQualificationId: string = '';
  taskFeedbackData:any={};
  skillFeedackData:any={};
  skillStepList:number;
  skillQuestionAnswer:number;

  constructor(
    private _router: Router,
    private store: Store<{ toggle: string }>,
    private route: ActivatedRoute,
    private taskService: TasksService,
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: any) => {
      if (params.hasOwnProperty('id')) {
        this.tempIds = params['id'];
        const idParts = String(this.tempIds).split('-');
        const empAndType = idParts[1].replace('§_', '');
        this.traineeId = empAndType.split('.')[0];
        this.checkType = empAndType.split('.')[1]; 

        if(this.checkType == 'eo'){
           this.skillQualificationId = idParts[0];
           this.getSQFeedback();
        }
        if(this.checkType == 'task'){
             this.qualificationId = idParts[0];
             this.getTaskFeedback();
        }
      }
    });
  }

  getTaskFeedback() {
    this.taskService.getTaskFeedback(this.qualificationId,this.traineeId).then((res) => {
      this.taskFeedbackData = res;
      this.taskStepList = this.taskFeedbackData.stepsList.length;
      this.taskQuestionAnswer = this.taskFeedbackData.quesionAnswerList.length;
      this.taskFeedbackData.stepsList = [... new Set(res.stepsList)]
      this.taskFeedbackData.quesionAnswerList = [... new Set(res.quesionAnswerList)]
    }).catch((res: any) => {
    })
  }

  getSQFeedback() {
    this.taskService.getSQFeedback(this.skillQualificationId,this.traineeId).then((res) => {
      this.skillFeedackData = res;
      this.skillStepList = this.skillFeedackData.stepsList.length;
      this.skillQuestionAnswer = this.skillFeedackData.quesionAnswerList.length;
      this.skillFeedackData.stepsList = [... new Set(res.stepsList)]
      this.skillFeedackData.quesionAnswerList = [... new Set(res.quesionAnswerList)]
      
    }).catch((res: any) => {
      console.error(res);
    })
  }
  async goBack() {

    this.store.dispatch(sideBarOpen());
    this._router.navigate(['emp/task-re-qualification/overview']);
  }
}
