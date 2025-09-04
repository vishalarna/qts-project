import { SelectionModel } from '@angular/cdk/collections';
import { TemplatePortal } from '@angular/cdk/portal';
import { Component, OnDestroy, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyPaginator as MatPaginator } from '@angular/material/legacy-paginator';
import { Sort, SortDirection } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { TaskQualificationTabVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationTabVM';
import { TaskQualificationFilterOptions } from 'src/app/_DtoModels/TaskQualificationFilter/TaskQualificationFilterOptions';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { navigateTQ } from 'src/app/_Statemanagement/action/state.componentcommunication';

@Component({
  selector: 'app-task-qualification',
  templateUrl: './task-qualification.component.html',
  styleUrls: ['./task-qualification.component.scss']
})
export class TaskQualificationComponent implements OnInit, OnDestroy {
  filterString = "";
  displayedColumns = ['taskNumber', 'taskDescription', 'empLinkCount', 'requalificationRequired', 'dueDate', 'action'];
  displayedColumnsPos = ['taskNumber', 'taskDescription','position' , 'empLinkCount', 'requalificationRequired', 'dueDate', 'action'];

  dataSource = new MatTableDataSource<TaskQualificationTabVM>();
  originalDataSource = new MatTableDataSource<TaskQualificationTabVM>();
  selection = new SelectionModel<TaskQualificationTabVM>(true, []);
  filterOptions!:TaskQualificationFilterOptions | null;
  hasPositionFilter = false;
  filterBy:string | null = null;
  @ViewChild(MatPaginator) paginator!:MatPaginator;
  empId = '';

  constructor(
    private router:Router,
    public flyPanelService : FlyInPanelService,
    private vcf : ViewContainerRef,
    private taskReQualificationService : TaskRequalificationService,
    private store:Store,
    private taskSortPipe:TaskSortPipePipe,
    private labelPipe:LabelReplacementPipe
  ) { }

  ngOnInit(): void {
    var filter = localStorage.getItem('filter');
    if(filter){
      this.filterOptions = JSON.parse(filter);
      this.getWithFilter(this.filterOptions,false);
      localStorage.removeItem('filter');
    }
  }

  ngOnDestroy(): void {
    
  }

  checkChange(event: any, row: TaskQualificationTabVM) {
    if (event.checked) {
      this.selection.select(row)
    }
    else {
      this.selection.deselect(row);
    }
  }

  masterToggle(event: any) {
    this.dataSource.data.forEach((element) => {
      if (event.checked) {
        this.selection.select(element);
      }
      else {
        this.selection.deselect(element);
      }
    });
  }

  inputChange(){
    this.dataSource.filter = this.filterString.trim().toLowerCase();
  }

  routeToRequal(row:TaskQualificationTabVM){
    localStorage.setItem('filter',JSON.stringify(this.filterOptions));
    this.router.navigate(['/implementation/taskReQualification/linkedEmp/' + row.taskId])
  }

  openFlypanel(templateRef:any){
    const portal = new TemplatePortal(templateRef,this.vcf);
    this.flyPanelService.open(portal);
  }

  active!:string;
  direction!:SortDirection;
  async getWithFilter(data:TaskQualificationFilterOptions,shouldSave:boolean = true){
    this.filterOptions = data;
    this.filterBy = null;
    switch(this.filterOptions.filterBy.trim().toLowerCase()){
      case 'position':
        var temp = await this.taskReQualificationService.filterByPosition(this.filterOptions);
        this.dataSource.data = temp;
        this.originalDataSource.data = Object.assign((temp));
        this.hasPositionFilter = true;
        this.filterBy = "By " + await this.labelPipe.transform('Position') ;
        break;
      case 'task':
        var temp = await this.taskReQualificationService.filterByTask(this.filterOptions);
        this.dataSource.data = temp;
        this.originalDataSource.data = Object.assign((temp));
        this.hasPositionFilter = false;
        this.filterBy = "By Duty Area / Subduty Area / " + await this.labelPipe.transform('Task');
        break;
      case 'sq':
        var temp = await this.taskReQualificationService.filterBySQ(this.filterOptions);
        this.dataSource.data = temp;
        this.originalDataSource.data = Object.assign((temp));
        this.hasPositionFilter = false;
        this.filterBy = "By Category / Subcategory / Topic / Skill Qualification";
        break;
      case 'group':
        var temp = await this.taskReQualificationService.filterByGroup(this.filterOptions);
        this.dataSource.data = temp;
        this.originalDataSource.data = Object.assign((temp));
        this.hasPositionFilter = false;
        this.filterBy = "By Training Group";
        break;
      case 'emp':
        var temp = await this.taskReQualificationService.filterByEMP(this.filterOptions);
        this.dataSource.data = temp;
        this.originalDataSource.data = Object.assign((temp));
        this.hasPositionFilter = false;
        this.filterBy = "By " + await this.labelPipe.transform('Employee');
        break;
      case 'eval':
        var temp = await this.taskReQualificationService.filterByEval(this.filterOptions);
        this.dataSource.data = temp;
        this.originalDataSource.data = Object.assign((temp));
        this.hasPositionFilter = false;
        this.filterBy = "By Evaluator";
        break;
    }
    
    this.selection.clear();
    if(shouldSave){
      localStorage.setItem('filter',JSON.stringify(this.filterOptions));
    }

    setTimeout(()=>{
      this.dataSource.paginator = this.paginator;
    },1);
    this.direction = 'asc';
    this.active = 'taskNumber';
    this.dataSource.data = this.taskSortPipe.transform(this.originalDataSource.data,this.direction,this.active).data;
  }

  sortChange(sort:Sort){
    if(this.dataSource.data){
      this.dataSource.data = this.taskSortPipe.transform(this.originalDataSource.data,sort.direction,sort.active).data;
    }
  }
}

export class TaskQualificationData {
  id?: any;
  number?: any;
  description?: string;
  empLinked?: number;
  reqRequired?: boolean;
  dueDate?: any;
}
