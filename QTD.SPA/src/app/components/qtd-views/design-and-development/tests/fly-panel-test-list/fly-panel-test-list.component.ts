import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit,ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { TestsService } from 'src/app/_Services/QTD/tests.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-test-list',
  templateUrl: './fly-panel-test-list.component.html',
  styleUrls: ['./fly-panel-test-list.component.scss']
})
export class FlyPanelTestListComponent implements OnInit {
  @Input() moduleName:any;
  displayedColumns = [ "testTitle"];
  treedataSource = new MatTableDataSource<any>();
  @ViewChild(MatSort) sort!:MatSort;
  @ViewChild('eoPaging') eoPaging: MatPaginator;


  constructor(
    public flyInClose : FlyInPanelService,
    private testService: TestsService,
    private alert: SweetAlertService
  ) { }

  ngOnInit(): void {
    this.getNames();
  }

  name:any;
  spinner:boolean;
  async getNames(){
    switch(this.moduleName){
      case 'Published':
        this.name = 'Published Tests';
        this.spinner = true
        break;

      case 'In Development':
        this.name = 'In Development Tests';
        this.spinner = true
        break;
      
      case 'Inactive':
        this.name = 'Inactive Tests';
        this.spinner = true
        break;
    }
    var tempData: any[] = [];
    let data =await this.testService.getTestList(this.moduleName);
      data.forEach((element, i) => {
        tempData.push({
          id: element.id,
          testTitle: element.testTitle,
        });
      });
      this.treedataSource.data = tempData;
      this.spinner = false;

    setTimeout(()=>{
      this.treedataSource.sort = this.sort;
      this.treedataSource.paginator = this.eoPaging;

    },1)
  }



}
