import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute, Router } from '@angular/router';
import { emptyObjectsAreNotAllowedMsg } from '@ngrx/store/src/models';
import { TasksService } from 'src/app/_Services/QTD/tasks.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-task-employee',
  templateUrl: './task-employee.component.html',
  styleUrls: ['./task-employee.component.scss'],
})
export class TaskEmployeeComponent implements OnInit {
  taskId = '';
  subscription = new SubSink();
  displayColumns: string[] = [
    'name',
    'position',
    'empNum',
    'lastQualification',
    'status',
  ];
  @Input() isMeta:any;
  DataSource: MatTableDataSource<any>;

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.DataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.DataSource.paginator = paginator;
  }

  constructor(private taskSrvc: TasksService, private route: ActivatedRoute,private router:Router) {}

  ngOnInit(): void {
    this.subscription.sink = this.route.params.subscribe((res) => {
      if(this.router.url.includes('task-suggestions')){
        this.taskId = String(res.id).split('-')[1].replace('§_', '').split('.')[0];;
       }
       else{
         this.taskId = String(res.id).split('-')[0];
        }
      this.getTaskEmpsByPosition();
    });
  }

  async getTaskEmpsByPosition() {
    if (!this.isMeta) {
      await this.taskSrvc.getLinkedEmployeesWithMetaTaskCount(this.taskId).then((res) => {
        let tempSrc: any[] = [];
        res.forEach((item) => {
          tempSrc.push({
            name: item.employeeName,
            position: item.positionName,
            empNum: item.empNumber,
            lastQualification: item.lastQualification,
            status: item.qualificationStatus,
            empEmail: item.empEmail,
          });
        });

        this.DataSource = new MatTableDataSource(tempSrc);
      });
    }
    else{
      await this.taskSrvc.getTaskPositionEmployees(this.taskId).then((res) => {
        
        let tempSrc: any[] = [];
        res.forEach((item) => {
          tempSrc.push({
            name: item.employeeName,
            position: item.positionName,
            empNum: item.empNumber,
            lastQualification: item.lastQualification,
            status: item.qualificationStatus,
            empEmail: item.empEmail,
            active:item.active
          });
        });

        this.DataSource = new MatTableDataSource(tempSrc);
      });
    }
  }
}
