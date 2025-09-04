import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { InstructorStatsVM } from 'src/app/_DtoModels/Instructors/InstructorsStatsVM';
import { InstructorService } from 'src/app/_Services/QTD/instructor.service';
import { DataBroadcastService } from 'src/app/_Shared/services/DataBroadcast.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SubSink } from 'subsink';
export interface PeriodicElement {
  name: string;
  position: number;
  weight: string;
  symbol: string;
  modifiedate:string;
}
// const ELEMENT_DATA: PeriodicElement[] = [
//   {position: 12, name: 'John Smith', weight: 'Change Instructor admin status', symbol: 'Sara Johnson',modifiedate:"25-09-1994"},
//   {position: 13, name: 'Jessica Albert', weight:'Change Instructor Email', symbol: 'Tara Johnson',modifiedate:"28-09-1994"},
// ];
@Component({
  selector: 'app-instructors-overview',
  templateUrl: './instructors-overview.component.html',
  styleUrls: ['./instructors-overview.component.scss']
})
export class InstructorsOverviewComponent implements OnInit {
  isLoading: boolean = false;
  catCompleted: any
  catIncompleted: any
  //displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  //dataSource = ELEMENT_DATA;
  displayedColumns: string[] = ['num','name', 'desc', 'modifyDate'];
  dataSource: MatTableDataSource<any>;
  instructorVM:InstructorStatsVM;
  subscription = new SubSink();
  @ViewChild(MatSort) set tblSort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }
  @ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  }
  constructor(private insService:InstructorService,
    private dataBroadcastService: DataBroadcastService,
    public flyPanelService: FlyInPanelService,
    private vcf: ViewContainerRef) { }

  ngOnInit(): void
   {
    
  this.getInstructorStats();
  this.getLatestActivity();
    // this.catCompleted = 10
    // this. catIncompleted = 24
  }
  ngAfterViewInit(): void {
    this.subscription.sink = this.dataBroadcastService.refreshOverviewData.subscribe((res:any)=>{
      this.getInstructorStats();

    });
  }
  async getInstructorStats() {
    this.isLoading = true;
    await this.insService.getStatsCount()
      .then((res:any) => {
        this.instructorVM = res;
      })
      .finally(() => (this.isLoading = false));
  }
  async getLatestActivity() {
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
        this.dataSource = new MatTableDataSource(tempSrc.slice(0,5));
      })
      .catch(() => (this.dataSource = new MatTableDataSource()));
  }
  async openFlyInPanel(templateRef: any)
  {
    
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  moduleName:string;
  openFlyInPanelList(templateRef: any,name:string){
    this.moduleName = name;
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }
}
