import { TemplatePortal } from '@angular/cdk/portal';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild, ViewContainerRef } from '@angular/core';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { Employee } from 'src/app/_DtoModels/Employee/Employee';
import { EmployeeWithPositionVM } from 'src/app/_DtoModels/Employee/EmployeeWithPositionVM';
import { TaskQualificationEmpVM } from 'src/app/_DtoModels/TaskQualification/TaskQualificationEmpVM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { TaskSortPipePipe } from 'src/app/_Pipes/task-sort-pipe.pipe';
import { EmployeesService } from 'src/app/_Services/QTD/employees.service';
import { TaskRequalificationService } from 'src/app/_Services/QTD/task-requalification.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-task-requall-by-emp',
  templateUrl: './task-requall-by-emp.component.html',
  styleUrls: ['./task-requall-by-emp.component.scss']
})
export class TaskRequallByEmpComponent implements OnInit {
  @Output() closed = new EventEmitter<any>();

  @Input() empId = '';

  description = "";
  searchText:string = "";

  dataSource = new MatTableDataSource<any>();
  originaldata = new MatTableDataSource<any>();

  displayedColumns = ['taskNumber', 'taskDescription','posNames', 'evaluatorName', 'empReleaseDate', 'dueDate', 'criteriaMet', 'status', 'comments', 'actions'];

  mode: 'add' | 'edit' = 'add';
  selectedData!: TaskQualificationEmpVM;
  linkedTQData: TaskQualificationEmpVM[] = [];

  placeHolder = '/assets/img/ImageNotFound.jpg';

  employee!: EmployeeWithPositionVM;
  empLoader = false;
  filterValues:any;
  displayFilterValues:any;
  empPositionIds:any[];
  empPos:any[];

  @ViewChild(MatSort) sort!:MatSort;

  constructor(
    private empService: EmployeesService,
    public flyPanelService: FlyInPanelService,
    public dialog: MatDialog,
    private vcf: ViewContainerRef,
    private tqService: TaskRequalificationService,
    private alert: SweetAlertService,
    private router: Router,
    private taskSortPipe:TaskSortPipePipe,
    private labelPipe: LabelReplacementPipe,
  ) { }

  ngOnInit(): void {
    this.readyEMPData();
    this.readyTQDataForEMP();
    this.readyEmpPositions();
  }

  async readyTQDataForEMP() {
    this.empLoader = true;
    var responseData = await this.tqService.getTQByEMP(this.empId);
    this.linkedTQData = responseData.map((item)=>{
      var qualDate = ""
      if(item.qualificationDate == null){
        qualDate = null;
      }else{
        var updatedDate = new Date(item.qualificationDate + "Z");
        qualDate = new Date(updatedDate).toLocaleString();
      } 
      return{...item,qualificationDate:qualDate}
    });
    this.dataSource.data = this.linkedTQData
    this.originaldata.data = Object.assign(this.linkedTQData);
    this.sortChanged({active:'taskNumber',direction:'asc'})
    this.empLoader = false;
  }

  ngAfterViewInit(): void{
    this.dataSource.sortingDataAccessor = (data, sortHeaderId) => {
      if (sortHeaderId === 'taskDescription') {
        return data.taskDescription.toLowerCase(); 
      }
      if(sortHeaderId === 'taskNumber') {
        return null; // Handle in sortChanged
      }
      return data[sortHeaderId]; 
    };
  }

  async readyEMPData() {
    this.employee = await this.empService.getEmployeeWithPosition(this.empId);
  }

  async readyEmpPositions(){
    this.empPos = await this.empService.getPositions(this.empId,"all");
  }

  async refreshData(event: any) {
    await this.readyTQDataForEMP();
    this.filterData();
    if (event.close) {
      this.flyPanelService.close()
    }
  }

  openFlyPanel(templateRef: any) {
    const portal = new TemplatePortal(templateRef, this.vcf);
    this.flyPanelService.open(portal);
  }

  async transformTitle(title: string) {
    const labelName = await this.labelPipe.transform(title);
    return labelName;
  } 

  async deleteData(event: any) {
    if (this.selectedData.id) {
      await this.tqService.delete(this.selectedData.id).then(async (_) => {
        this.alert.successToast(await this.transformTitle('Task') + " Qualification Deleted Successfully");
        this.readyTQDataForEMP();
      }).finally(() => {

      })
    }
    else {
      this.alert.successToast(await this.transformTitle('Task') +" Qualification Deleted Successfully");
    }
  }

  async openDeleteDialog(templateRef: any) {
    this.description = `You are selecting to Delete ` + await this.transformTitle('Task') +`  Qualification Record ${this.selectedData.taskNumber} for ` + await this.labelPipe.transform('Employee') + ` ${this.selectedData.empName}.`
    this.dialog.open(templateRef, {
      width: '800px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  navigateToTask(data: TaskQualificationEmpVM) {
    localStorage.setItem('empNav',"true");
    this.router.navigate([`/implementation/taskReQualification/linkedEmp/${data.taskId}`]);
  }

  filterData(){
    this.originaldata.data = this.linkedTQData;
    if (this.searchText) {
      const searchTextLower = this.searchText.toLowerCase();
      this.originaldata.data = this.originaldata.data.filter((item) => {
        return (
          item?.taskDescription?.toLowerCase().includes(searchTextLower) ||
          item?.taskNumber?.toLowerCase().includes(searchTextLower) ||
          item?.posNames?.toLowerCase().includes(searchTextLower)
        );
      });
    }

    if (this.filterValues?.TasksLinkedTo) {
      this.applyPositionStatusFilter();
    }

    if (this.filterValues?.ReliabilityRelated) {
      this.applyReliabilityFilter();
    }

    if (this.filterValues?.Status) {
     this.applyStatusFilter();
    }

    if (this.filterValues?.CriteriaMet) {
      this.applyCriteriaFilter();
    }

    if (this.filterValues?.QualificationStatus) {
      this.applyQualificationStatusFilter();
    }

    this.dataSource = new MatTableDataSource(this.originaldata.data);
    setTimeout(()=>{
      this.dataSource.sort = this.sort;
    },1);
  }

  sortChanged(sort:any){
    this.dataSource.sort = this.sort;
    const data = this.dataSource.data;
    if(sort.active=='taskNumber' && sort.direction !== ''){
      this.dataSource.data = (this.taskSortPipe.transform(data, sort.direction,'number')).data;
    }
    if (!sort.active || sort.direction === '') {
      this.originaldata.data = data;
      return;
    }
  }

  getEmpTaskQualFilterValues(value:any){
    this.filterValues = value;
    var filterValueArray = this.getNonNullValues(this.filterValues)
    this.displayFilterValues = filterValueArray;
    this.filterData();
  }

  getNonNullValues = (obj) => {
    const addSpaceBeforeUppercase = (str) => {
        return str.replace(/([A-Z])/g, ' $1').trim(); 
    };

    return Object.keys(obj).filter(key => obj[key] !== null) .map(key => `${addSpaceBeforeUppercase(key)}: ${obj[key]}`) .join(', '); 
};

  searchUpdate(event: any) {
    const searchText = event.target.value.toLowerCase();
    this.searchText = searchText;
    this.filterData();
  }

  applyPositionStatusFilter(){
    if(this.filterValues?.TasksLinkedTo=="Current Position"){
      this.empPositionIds = this.empPos.filter(x=>x.active).map(m=>m.positionId);
      this.originaldata.data = this.originaldata.data.filter((item)=>item.posIds.some(posId => this.empPositionIds.includes(posId)))
    }
  }

  applyReliabilityFilter(){
    if(this.filterValues?.ReliabilityRelated=="R-R"){
      this.originaldata.data = this.originaldata.data.filter((item)=>item.isReliability)
    }
    if(this.filterValues?.ReliabilityRelated=="Non R-R"){
      this.originaldata.data = this.originaldata.data.filter((item)=>!item.isReliability)
    }
  }

  applyStatusFilter(){
    if(this.filterValues?.Status =="Active"){
      this.originaldata.data = this.originaldata.data.filter((item)=>item.active)
    }
    if(this.filterValues?.Status =="Inactive"){
      this.originaldata.data = this.originaldata.data.filter((item)=>!item.active)
    }
  }

  applyCriteriaFilter(){
    if(this.filterValues?.CriteriaMet =="Yes"){
      this.originaldata.data = this.originaldata.data.filter((item)=>item.criteriaMet)
    }
    if(this.filterValues?.CriteriaMet =="No"){
      this.originaldata.data = this.originaldata.data.filter((item)=>!item.criteriaMet)
    }
  }

  applyQualificationStatusFilter(){
    if(this.filterValues?.QualificationStatus =="Pending"){
      this.originaldata.data = this.originaldata.data.filter((item)=>item.status == "Pending")
    }
    if(this.filterValues?.QualificationStatus =="On Time"){
      this.originaldata.data = this.originaldata.data.filter((item)=>item.status == "On Time")
    }
    if(this.filterValues?.QualificationStatus =="Trainee Initial Qualification"){
      this.originaldata.data = this.originaldata.data.filter((item)=>item.status == "Trainee Initial Qualification")
    }
    if(this.filterValues?.QualificationStatus =="Delayed"){
      this.originaldata.data = this.originaldata.data.filter((item)=>item.status == "Delayed")
    }
  }

}
