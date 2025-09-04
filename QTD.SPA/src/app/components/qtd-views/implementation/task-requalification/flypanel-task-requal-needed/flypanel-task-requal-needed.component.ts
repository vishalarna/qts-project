import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TaskWithNumberVM } from 'src/app/_DtoModels/Task/TaskWithNumberVM';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';

@Component({
  selector: 'app-flypanel-task-requal-needed',
  templateUrl: './flypanel-task-requal-needed.component.html',
  styleUrls: ['./flypanel-task-requal-needed.component.scss']
})
export class FlypanelTaskRequalNeededComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Input() empId = "";
  @Input() empName = "";
  @Input() empImage = "";
  tempData:TaskWithNumberVM[] = [];
  isLoading = false;
  constructor(
    private tqService : TaskRequalificationService,
  ) { }

  ngOnInit(): void {
    this.readyData();
  }

  async readyData(){
    this.isLoading = true;
    this.tempData = await this.tqService.getPendingTaskRequalForEmp(this.empId);
    this.isLoading = false;
  }

}
