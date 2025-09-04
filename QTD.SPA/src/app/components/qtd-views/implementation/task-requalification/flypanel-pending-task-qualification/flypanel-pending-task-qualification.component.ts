import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TaskQuaificationWithoutPosDateVM } from 'src/app/_DtoModels/TaskQualification/TaskQuaificationWithoutPosDateVM';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';

@Component({
  selector: 'app-flypanel-pending-task-qualification',
  templateUrl: './flypanel-pending-task-qualification.component.html',
  styleUrls: ['./flypanel-pending-task-qualification.component.scss']
})
export class FlypanelPendingTaskQualificationComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  dataSource!:MatTableDataSource<TaskQuaificationWithoutPosDateVM>;
  displayedColumns = ['emp','count'];
  pendingQuals:TaskQuaificationWithoutPosDateVM[] = [];
  placeHolderImg = "../../../../../../assets/img/ImageNotFound.jpg";
  neededForView = false;
  selectedName = "";
  selectedId = "";
  selectedImage = "";


  constructor(
    private tqService : TaskRequalificationService,
  ) { }

  ngOnInit(): void {
    this.setData();
  }

  async setData(){
    this.pendingQuals = await this.tqService.getpendingTaskQualifications();
    if(this.dataSource){
      this.dataSource.data = this.pendingQuals
    }
    else{
      this.dataSource = new MatTableDataSource<TaskQuaificationWithoutPosDateVM>(this.pendingQuals);
    }
  }

}

export class PendingTQData{
  id?:any;
  count?:number;
  empFName?:string;
  empLName?:string;
  empEmail?:string;
  empPicture?:string;
}
