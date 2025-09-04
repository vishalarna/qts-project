import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TaskQuaificationWithoutPosDateVM } from 'src/app/_DtoModels/TaskQualification/TaskQuaificationWithoutPosDateVM';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';

@Component({
  selector: 'app-flypanel-without-position',
  templateUrl: './flypanel-without-position.component.html',
  styleUrls: ['./flypanel-without-position.component.scss']
})
export class FlypanelWithoutPositionComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();
  dataSource = new MatTableDataSource<TaskQuaificationWithoutPosDateVM>();
  neededForView = false;
  displayedColumns = ['emp','count'];
  withouPositionData:TaskQuaificationWithoutPosDateVM[] = [];
  placeHolderImg = "../../../../../../assets/img/ImageNotFound.jpg";
  selectedImage = "";
  isLoading = false;
  selectedId = "";
  selectedName = "";

  constructor(
    private taskRequalService : TaskRequalificationService,
  ) { }

  ngOnInit(): void {
    this.readyData();
  }

  async readyData(){
    this.isLoading = true;
    this.withouPositionData = await this.taskRequalService.getEmpWithoutPositionQualDate();
    this.dataSource.data = this.withouPositionData;
    this.isLoading = false;
  }
}
