import { Component, Input, OnInit } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { RetakeStatusesVM } from 'src/app/_DtoModels/SchedulesClassses/Rosters/RetakeStatusesVM';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-flypanel-retake-status',
  templateUrl: './flypanel-retake-status.component.html',
  styleUrls: ['./flypanel-retake-status.component.scss']
})
export class FlypanelRetakeStatusComponent implements OnInit {
  @Input() empId!:any;
  @Input() classId!:any;
  statusData:RetakeStatusesVM[] = [];
  dataSource = new MatTableDataSource<RetakeStatusesVM>();
  displayedCols = ["employee","title","status"];
  constructor(
    public flyPanelSrvc:FlyInPanelService,
    private testService:TestsService,
  ) { }

  ngOnInit(): void {
    this.readyRetakeData();
  }

  async readyRetakeData(){
    
    this.statusData = await this.testService.ReadyRetakeStatusData(this.empId,this.classId);
    this.dataSource.data = this.statusData;
  }

}
