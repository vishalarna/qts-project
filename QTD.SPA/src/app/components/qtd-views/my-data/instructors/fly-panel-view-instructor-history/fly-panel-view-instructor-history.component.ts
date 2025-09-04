import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { InstructorService } from 'src/app/_Services/QTD/instructor.service';
import { RegulatoryRequirementService } from 'src/app/_Services/QTD/regulatory-requirement.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-view-instructor-history',
  templateUrl: './fly-panel-view-instructor-history.component.html',
  styleUrls: ['./fly-panel-view-instructor-history.component.scss']
})
export class FlyPanelViewInstructorHistoryComponent implements OnInit {
  //displayedColumns: string[] = ['num','name', 'desc', 'modifyDate'];
  displayedColumns: string[] = ['name', 'desc', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  appSpinner:boolean=false;

  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private insService:InstructorService,
  ) {}

  ngOnInit(): void {
    this.getLatestActivity();
  }

  async getLatestActivity() {
    this.appSpinner=true;

    await this.insService
      .getStatusHistory()
      .then((res) => {
        let tempSrc: any[] = [];
        res.forEach((h, i) => {
          tempSrc.push({
            index: i,
            id: h.id,
            num:h.instructorNum,
            name: h.instructorName,
            desc: h.activityDesc,
            modifyBy: h.createdBy,
            modifyDate: h.createdDate,
          });
        });
        this.dataSource = new MatTableDataSource(tempSrc);
      })
      .catch(() => (this.dataSource = new MatTableDataSource())).finally(() =>{
        this.appSpinner=false;
      });;
  }
  filterData(e: any) {
    
    this.dataSource.filter = e.target.value;
  }

  clearSearch:string;
  clearFilter(){
    this.dataSource.filter = '';
    this.clearSearch = null;
  }
}
