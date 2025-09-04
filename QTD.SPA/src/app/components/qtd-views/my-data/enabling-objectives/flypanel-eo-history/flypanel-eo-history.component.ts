import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { EOLatestActivityVM } from 'src/app/_DtoModels/EnablingObjective/EOLatestActivityVM';
import { Version_EnablingObjective } from 'src/app/_DtoModels/Version_EnablingObjective/Version_EnablingObjective';
import { EnablingObjectivesService } from 'src/app/_Services/QTD/enabling-objectives.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-flypanel-eo-history',
  templateUrl: './flypanel-eo-history.component.html',
  styleUrls: ['./flypanel-eo-history.component.scss']
})
export class FlypanelEoHistoryComponent implements OnInit {
  displayedColumns: string[] = ['title', 'activityDesc', 'createdDate'];
  displayedColumnsHistory : string[] = ['versionNumber','createdDate','createdBy','actions']
  dataSource: MatTableDataSource<any>;
  appSpinner:boolean = false;
  @ViewChild(MatSort) sort : MatSort/*(sort: MatSort)  {
    if (sort) this.DataSource.sort = sort;
  } */
  @ViewChild(MatPaginator) tblPaging : MatPaginator;/* (paginator: MatPaginator) {
    if (paginator) this.dataSource.paginator = paginator;
  } */

  @ViewChild('verSort') verSort!:MatSort;

  @Input() isVersion = false;
  @Input() Title = "";
  @Input() Number = "";
  @Input() eoId = "";
  @Output() closed = new EventEmitter<any>();

  eoActivity:EOLatestActivityVM[];

  selection = new SelectionModel<any>(true,[]);
  viewVersion = false;
  compareVersion = false;
  toView!:Version_EnablingObjective;
  toCompare: Version_EnablingObjective[] = [];
  orderedVersions : any[] = [];

  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcf: ViewContainerRef,
    private eoService : EnablingObjectivesService,
  ) { }

  ngOnInit(): void {
    this.isVersion ? this.readyVersion() : this.readyHistory();
  }

  async readyVersion(){
    await this.eoService.getVersions(this.eoId).then((res)=>{
      this.dataSource = new MatTableDataSource(res);
      setTimeout(()=>{
        this.dataSource.sort = this.verSort;
      },1)
    });
  }

  sortData(sort: Sort) {
    this.dataSource.sort = this.sort;
  }

  async readyHistory(){
    this.appSpinner = true;
    this.eoActivity = await this.eoService.getLatestHistory().finally(() => {
      this.appSpinner = false;
    });
    this.dataSource = new MatTableDataSource(this.eoActivity);
  }

  filterData(e: any) {

    this.dataSource.filter = e.target.value;
  }

  selectionChanged(event:any,row:any){

    if(event.checked){
      if(this.selection.selected.length > 1){
        this.selection.deselect(this.selection.selected[1]);
        this.selection.select(row);
      }
      else{
        this.selection.select(row);
        if(this.selection.selected.length == 2){
        }
      }
    }
    else{
      this.selection.deselect(row);
    }

  }

  compareLogic(){
    this.compareVersion = true;
    if(this.selection.selected[0].versionNumber > this.selection.selected[1].versionNumber){
      this.toView = this.selection.selected[0];
      this.orderedVersions[0] = this.selection.selected[0].versionNumber;
      this.orderedVersions[1] = this.selection.selected[1].versionNumber;
    }
    else{
      this.toView = this.selection.selected[1];
      this.orderedVersions[0] = this.selection.selected[1].versionNumber;
      this.orderedVersions[1] = this.selection.selected[0].versionNumber;
    }
  }

  clearSearch:string;
  clearFilter(){
    this.dataSource.filter = '';
    this.clearSearch = null;
  }
}
