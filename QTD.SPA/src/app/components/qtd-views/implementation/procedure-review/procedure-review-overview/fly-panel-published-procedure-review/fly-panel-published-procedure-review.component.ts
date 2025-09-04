import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { ProceduresService } from 'src/app/_Services/QTD/procedures.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-fly-panel-published-procedure-review',
  templateUrl: './fly-panel-published-procedure-review.component.html',
  styleUrls: ['./fly-panel-published-procedure-review.component.scss']
})
export class FlyPanelPublishedProcedureReviewComponent implements OnInit {
  showSpinner = false;
  @Output() closed = new EventEmitter<any>();
  searchString = "";
  employeeData!: any[];
  employeeDataCopy!: any[];
  ProcedureReviewDataSource = new MatTableDataSource<any>();
  displayedColumns: string[] = [
    'index',
    'procedure',
    'title',
    'name',
  ];
  @ViewChild('roSort') eoSort = new MatSort();
  @ViewChild('eoPaging') eoPaging: MatPaginator;
  constructor(
    private procedureService : ProceduresService,
    public flyin : FlyInPanelService,
    private alert : SweetAlertService,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.getPublishedList();
  }

  spinner:boolean=false;
  async getPublishedList(){
    this.spinner= false;
    await this.procedureService.getPublishedList().then((data)=>{
      this.ProcedureReviewDataSource.data= this.employeeData = data;
      
    }).catch(async (error)=>{
      this.alert.errorToast('Error Fetching ' + await this.transformTitle('Procedure') + ' Review')
    }).finally(()=>{
      this.spinner = true
    });

    setTimeout(()=>{
      this.ProcedureReviewDataSource.sort = this.eoSort;
      this.ProcedureReviewDataSource.paginator = this.eoPaging;    },1)

   
  }

  sortData(sort: Sort) { 
    this.ProcedureReviewDataSource.sort = this.eoSort; 
  }
  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  }

}
