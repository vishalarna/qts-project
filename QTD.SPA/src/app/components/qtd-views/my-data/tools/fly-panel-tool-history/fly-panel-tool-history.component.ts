import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ToolsService } from 'src/app/_Services/QTD/tools.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-fly-panel-tool-history',
  templateUrl: './fly-panel-tool-history.component.html',
  styleUrls: ['./fly-panel-tool-history.component.scss']
})
export class FlyPanelToolHistoryComponent implements OnInit {

  stats:boolean=false

  displayedColumns: string[] = ['id','title', 'activityDesc', 'modifyBy'];
  spinner: boolean;
  subscription = new SubSink();
  dataSource: MatTableDataSource<any>;
  appSpinner:boolean=false;
    @ViewChild(MatPaginator) tblPaging:MatPaginator;


  //@ViewChild(MatPaginator) set tblPaging(paginator: MatPaginator) {
  //  if (paginator) this.dataSource.paginator = paginator;
  //}

  @ViewChild(MatSort) set sort(sort: MatSort) {
    if (sort) this.dataSource.sort = sort;
  }


  constructor(
    private toolService: ToolsService,
    private alert: SweetAlertService,
    public flyPanelSrvc : FlyInPanelService,
    private vcf: ViewContainerRef,
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {

      this.getStatusHistoryData();

  }

  ngOnDestroy(): void {

  }

  historyData:any =[];
  async getStatusHistoryData(){
    this.appSpinner=true;

  this.historyData =  await this.toolService.getAllToolStatus().then((res) => {
    let tempSrc: any[] = [];
    res.forEach((h, i) => {
      tempSrc.push({
        index: h.id,
        id: h.id + " . " + h.number,
        title: h.title,
        activityDesc: h.activityDesc,
        modifiedBy: h.modifiedBy,
        modifiedDate: h.modifiedDate,
      });
    });
    this.dataSource = new MatTableDataSource(tempSrc);
    setTimeout(()=>{
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.tblPaging;
    },1)  }).finally(() =>{
      this.appSpinner=false;
    });



   
  }

  sortData(sort: Sort) {
    this.dataSource.sort = this.sort;
  }

  clearSearch:string;
  clearFilter(){
    this.dataSource.filter = '';
    this.clearSearch = null;
  }

  filterData(e: any) {
    this.dataSource.filter = e.target.value;
  }
}
