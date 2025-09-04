import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { TrainingProgramsService } from 'src/app/_Services/QTD/training-programs.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-active-inactive-list',
  templateUrl: './fly-panel-active-inactive-list.component.html',
  styleUrls: ['./fly-panel-active-inactive-list.component.scss']
})
export class FlyPanelActiveInactiveListComponent implements OnInit,AfterViewInit {
  @Input() TrainingProgramName:string;
  @Input() TrainingProgramStatus:boolean;
  @ViewChild(MatPaginator) paginator!:MatPaginator;
  @ViewChild(MatSort) sort!:MatSort;
  dataSourceTrainingPrograms: MatTableDataSource<any>;
  displayedColumns: string[] = ['positionTitle', 'trainingProgramTypeTitle', 'tpVersionNo', 'startDate'];
  spinner:boolean;

  constructor(
    public flyInService: FlyInPanelService,
    private trainingProgramService: TrainingProgramsService,
    private alert: SweetAlertService
  ) { }

  ngOnInit(): void {

    
  
  }

  ngAfterViewInit(): void {
    this.getList();
  }

  async getList(){
    var list:any;
    this.spinner = true;
    await this.trainingProgramService.getNotLinkedWith(this.TrainingProgramName,this.TrainingProgramStatus).then((data)=>{
      
      list = data;
      this.dataSourceTrainingPrograms = new MatTableDataSource(list);
    }).catch((error)=>{
      this.alert.errorToast('Error fetching Training Program data');
    }).finally(()=>{
      this.spinner = false;
    })

    setTimeout(()=>{
      this.dataSourceTrainingPrograms.sort = this.sort;
      this.dataSourceTrainingPrograms.paginator = this.paginator;
    },1)

    
  }

}
