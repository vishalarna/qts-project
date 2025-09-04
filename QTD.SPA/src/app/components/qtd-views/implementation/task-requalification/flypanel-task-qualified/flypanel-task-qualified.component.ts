import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TaskQualificationEmpVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationEmpVM';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';

@Component({
  selector: 'app-flypanel-task-qualified',
  templateUrl: './flypanel-task-qualified.component.html',
  styleUrls: ['./flypanel-task-qualified.component.scss']
})
export class FlypanelTaskQualifiedComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  @Input() empId = "";
  @Input() taskId = "";
  dataSource!:MatTableDataSource<TaskQualificationEmpVM>;
  displayedColumns = ["releaseDate",'qualDate','dueDate','criteria','qualStatus','isRecalled','comments'];
  constructor(
    private taskQualificationService : TaskRequalificationService,
  ) { }

  ngOnInit(): void {
    this.readyData();
  }

  async readyData(){
    var taskQualificationData = await this.taskQualificationService.getAllQualificationsForEMP(this.empId,this.taskId);
    this.dataSource = new MatTableDataSource(taskQualificationData)
  }
}

