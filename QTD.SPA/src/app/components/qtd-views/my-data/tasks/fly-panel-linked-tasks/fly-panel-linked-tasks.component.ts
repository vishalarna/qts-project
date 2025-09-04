import {
  AfterViewInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { Task } from 'src/app/_DtoModels/Task/Task';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';

@Component({
  selector: 'app-fly-panel-linked-tasks',
  templateUrl: './fly-panel-linked-tasks.component.html',
  styleUrls: ['./fly-panel-linked-tasks.component.scss'],
})
export class FlyPanelLinkedTasksComponent implements OnInit, AfterViewInit {
  @Input() Title = '';
  @Input() EONumber:any;
  @Input() RRNumber:any;
  @Input() taskNumber:any;
  @Input() SHNumber:any;
  @Input() ilaNumber:any;
  @Input() procedureNumber:any;
  @Input() selectedType = '';
  @Input() posAbbrevation:any;
  @Output() closed = new EventEmitter<any>();
  linkedTasks: Task[];
  @Input() id = '';
  number:any; 
  titleNumber:any;
  constructor(public taskService: TasksService) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    switch (this.selectedType.trim().toLowerCase()) {
      case 'enabling objective':
        this.readyEOData();
        break;
      case 'procedure':
        this.readyProcData();
        break;
      case "ila":
        this.readyILAData();
        break;
      case 'regulatory requirement':
        this.readyRRData();
        break;
      case 'position':
        this.readyPositionData();
        break;
      case 'safety hazard':
        this.readySHData();
        break;
    }
  }

  async readyEOData() {
    this.linkedTasks = await this.taskService.getTasksLinkedTOEO(this.id);
    
    this.titleNumber = this.EONumber;
  }

  async readyProcData() {
    this.linkedTasks = await this.taskService.getTasksLinkedToProc(this.id);
    this.titleNumber = this.procedureNumber;
  }

  async readyRRData() {
    this.linkedTasks = await this.taskService.getLinkedRR(this.id);
    this.titleNumber = this.RRNumber;
  }

  async readyPositionData() {
    this.linkedTasks = await this.taskService.getTaskLinkedTopositions(this.id);
    this.titleNumber = this.posAbbrevation;
  }

  async readyILAData(){
    this.linkedTasks = await this.taskService.getTasksLinkedToILA(this.id);
    this.titleNumber = this.ilaNumber;
  }

  async readySHData(){
    this.linkedTasks = await this.taskService.GetTasksLinkedtoSH(this.id);
    this.titleNumber = this.SHNumber 
  }
}
