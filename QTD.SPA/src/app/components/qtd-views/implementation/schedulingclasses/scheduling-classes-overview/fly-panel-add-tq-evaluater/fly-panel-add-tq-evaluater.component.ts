import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, OnInit, Output, ViewContainerRef } from '@angular/core';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { StudentEvaluation } from 'src/app/_DtoModels/StudentEvaluation/StudentEvaluation';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { StudentEvaluationService } from 'src/app/_Services/QTD/student-evaluation.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-fly-panel-add-tq-evaluater',
  templateUrl: './fly-panel-add-tq-evaluater.component.html',
  styleUrls: ['./fly-panel-add-tq-evaluater.component.scss']
})
export class FlyPanelAddTqEvaluaterComponent implements OnInit {

  isLoading: boolean = false;
  showSpinner = false;

  @Output() closed = new EventEmitter<any>();
  dataSource = new MatTableDataSource<any>();
  selection = new SelectionModel<any>(true, []);
  @Output() idSelected = new EventEmitter<any>();
  displayedColumns: string[] = [
    "select",
    "title",
  ];
  constructor(
    public flyPanelSrvc: FlyInPanelService,
    private vcr: ViewContainerRef,
    private empSrvc: EmployeesService,
    private studentEvaluationService:StudentEvaluationService,
  ) { }

  ngOnInit(): void {
    this.getAllEvaluations();
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


  addToTQRelease(event){
    this.idSelected.emit(event);
    this.closeProvider()

  }
  openFlyInPanelAssignScore(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcr);
    this.flyPanelSrvc.open(portal);
  }

  getAllEvaluations()
  {   this.isLoading = true
    this.studentEvaluationService.getPublishedEvals().then((res: any[]) => {
      
       this.dataSource.data = res;
     }).finally(() => {
      
      this.isLoading = false
    });
  }

}
