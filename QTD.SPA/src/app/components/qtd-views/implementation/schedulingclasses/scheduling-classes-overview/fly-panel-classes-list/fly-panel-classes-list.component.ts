import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output, ViewContainerRef } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ActivatedRoute } from '@angular/router';
import { TrainingService } from 'src/app/_Services/QTD/training.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-classes-list',
  templateUrl: './fly-panel-classes-list.component.html',
  styleUrls: ['./fly-panel-classes-list.component.scss']
})
export class FlyPanelClassesListComponent implements OnInit {

  showSpinner = false;
  classes: any[];
  @Input() appClassesList: any[] = [];
  filteredList: any[];
  @Output() closed = new EventEmitter<any>();
  dataSource = new MatTableDataSource<any>();
  selection = new SelectionModel<any>(true, []);
  @Output() idSelected = new EventEmitter<any>();
  ilaId:number=0;
  datePipe = new DatePipe('en-us');
  displayedColumns: string[] = [
    "select",
    "employees",
  ];
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private trainingSevc: TrainingService,
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {

    this.getClassesByIlaId();
  }

    /** Whether the number of selected elements matches the total number of rows. */
    isAllSelected() {
      const numSelected = this.selection.selected.length;
      const numRows = this.dataSource.data.length;
      return numSelected === numRows;
    }
  
    /** Selects all rows if they are not all selected; otherwise clear selection. */
    masterToggle() {
      this.isAllSelected() ?
          this.selection.clear() :
          this.dataSource.data.forEach(row => this.selection.select(row));
    }
  closeProvider() {
   this.flyPanelSrvc.close();
  }


  addToClass(event){
    this.idSelected.emit(event);
    this.closeProvider()

  }
  openFlyInPanelAssignScore(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  async getClassesByIlaId() {
    
    this.dataSource.data=this.appClassesList;
  }

}
