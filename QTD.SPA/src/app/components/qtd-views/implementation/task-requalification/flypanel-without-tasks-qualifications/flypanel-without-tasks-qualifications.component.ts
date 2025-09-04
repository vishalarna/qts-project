import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TaskQuaificationWithoutPosDateVM } from 'src/app/_DtoModels/TaskQualification/TaskQuaificationWithoutPosDateVM';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';

@Component({
  selector: 'app-flypanel-without-tasks-qualifications',
  templateUrl: './flypanel-without-tasks-qualifications.component.html',
  styleUrls: ['./flypanel-without-tasks-qualifications.component.scss']
})
export class FlypanelWithoutTasksQualificationsComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  dataSource!:MatTableDataSource<TaskQuaificationWithoutPosDateVM>;
  displayedColumns = ['emp', 'count'];
  neededForView = false;
  employees:TaskQuaificationWithoutPosDateVM[] = [];
  placeHolderImg = "../../../../../../assets/img/ImageNotFound.jpg";
  selectedId = "";
  selectedName = "";
  selectedImage = "";

  constructor(
    private tqService : TaskRequalificationService,
  ) { }

  ngOnInit(): void {
    this.setData();
  }

  async setData() {
    this.employees = await this.tqService.getEmployeeDataWithoutTQRecords();
    if(this.dataSource){
      this.dataSource.data = this.employees;
    }
    else{
      this.dataSource = new MatTableDataSource(this.employees);
    }
  }

}

export class WithoutTQData {
  id?: any;
  count?: number;
  empFName?: string;
  empLName?: string;
  empEmail?: string;
  empPicture?: string;
}
