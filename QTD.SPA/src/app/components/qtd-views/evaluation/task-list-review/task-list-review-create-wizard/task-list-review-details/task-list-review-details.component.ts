import { TemplatePortal } from '@angular/cdk/portal';
import { DatePipe } from '@angular/common';
import { Component, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacySelect as MatSelect } from '@angular/material/legacy-select';
import { MatSort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { QtdUserVM } from '@models/QtdUser/QtdUserVM';
import { TaskListReviewType_VM } from '@models/TaskListReview/TaskListReviewType_VM';
import { TaskListReview_GeneralReviewer_VM } from '@models/TaskListReview/TaskListReview_GeneralReviewer_VM';
import { TaskListReview_VM } from '@models/TaskListReview/TaskListReview_VM';
import { ApiTaskListReviewTypeService } from 'src/app/_Services/QTD/TaskListReviewType/api.tasklistreviewtype.service';
import { PositionsService } from 'src/app/_Services/QTD/positions.service';
import { QTDService } from 'src/app/_Services/QTD/qtd.service';
import { FlyInPanelService } from 'src/app/_Shared/services/flyInPanel.service';

@Component({
  selector: 'app-task-list-review-details',
  templateUrl: './task-list-review-details.component.html',
  styleUrls: ['./task-list-review-details.component.scss'],
})
export class TaskListReviewDetailsComponent implements OnInit {
  @ViewChild('posSelect', { static: false }) posSelect!: MatSelect;
  @ViewChild('positionSelect', { static: false }) positionSelect!: MatSelect;
  @Input() inputTaskListReviewVM : TaskListReview_VM ;
  @Input() mode : string ;
  searchText: string;
  tableColumns: string[];
  linkedReviewerIds: string[];
  dataSource: MatTableDataSource<TaskListReview_GeneralReviewer_VM>;
  createTaskListReviewForm: UntypedFormGroup;
  reviewTypes: TaskListReviewType_VM[];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('deleteGeneralReviewer') deleteGeneralReviewer: any;
  deletedGeneralReviewer:TaskListReview_GeneralReviewer_VM;
  isLoading:boolean;
  allPositions:any[];
  selectedPositionIds: any[] = [];
  constructor(
    private qtdService: QTDService,
    private fb: UntypedFormBuilder,
    public flyPanelService: FlyInPanelService,
    public taskListReviewTypeService: ApiTaskListReviewTypeService,
    private datepipe: DatePipe,
    public dialog: MatDialog,
    private positionService:PositionsService
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.initializeCreateReview();
    this.linkedReviewerIds = [];
    this.loadAsync();
    this.tableColumns = ['name', 'remove'];
    this.searchText = '';
    this.dataSource = new MatTableDataSource<TaskListReview_GeneralReviewer_VM>();
  }

  initializeCreateReview(){
    this.createTaskListReviewForm = this.fb.group({
      title: new UntypedFormControl(null, [Validators.required]),
      reviewType: new UntypedFormControl(null,[Validators.required]),
      startDate: new UntypedFormControl(null,[Validators.required]),
      endDate: new UntypedFormControl(null,[Validators.required]),
      positions: new UntypedFormControl([]),
      positionSearchText: new UntypedFormControl(''),
      reviewedBy: new UntypedFormControl(null),
    });
  }

  setFormValues(){
    this.createTaskListReviewForm.patchValue({
      title: this.inputTaskListReviewVM?.title,
      reviewType: this.inputTaskListReviewVM?.typeId,
      startDate: this.inputTaskListReviewVM?.startDate != null ? this.datepipe.transform(this.inputTaskListReviewVM?.startDate,'yyyy-MM-dd') :null,
      endDate: this.inputTaskListReviewVM?.endDate != null ? this.datepipe.transform(this.inputTaskListReviewVM?.endDate,'yyyy-MM-dd') :null,
      positions: this.inputTaskListReviewVM?.positionIds || [],
      reviewedBy: this.inputTaskListReviewVM?.reviewedBy
    });
    let positions = this.createTaskListReviewForm.get('positions')?.value;
    this.selectedPositionIds = [...positions];
    this.dataSource = new MatTableDataSource(this.inputTaskListReviewVM?.generalReviewers);
    this.linkedReviewerIds = this.dataSource.data.map(x=>x.qtdUserId);
    this.isLoading = false;
  }

  async loadAsync(){
    await this.getAllPositions();
    this.reviewTypes = await this.taskListReviewTypeService.getAllAsync();
    if(this.mode =="create"){
      this.isLoading=false;
    }
    setTimeout(()=>{
      this.dataSource.sort = this.sort;
    },1);
  }

  async getAllPositions(){
    this.allPositions = await this.positionService.getActiveInactiveList('active');
    this.allPositions?.sort((a, b) => {
      return a.positionTitle.localeCompare(b.positionTitle);
  });
  }

  titleChange() {
    let title = this.createTaskListReviewForm.get('title').value;
    this.inputTaskListReviewVM.title = title;
  }

  reviewTypeChange() {
    let reviewType = this.createTaskListReviewForm.get('reviewType').value;
    this.inputTaskListReviewVM.typeId = reviewType;
  }

  reviewedByChange() {
    let reviewedBy = this.createTaskListReviewForm.get('reviewedBy').value;
    this.inputTaskListReviewVM.reviewedBy = reviewedBy;
  }

  startDateChange() {
    let startDate = this.createTaskListReviewForm.get('startDate').value;
    this.inputTaskListReviewVM.startDate = startDate;
  }

  endDateChange() {
    let endDate = this.createTaskListReviewForm.get('endDate').value;
    this.inputTaskListReviewVM.endDate = endDate;
  }

  positionChange() {
    let positions = this.createTaskListReviewForm.get('positions')?.value;
    this.selectedPositionIds = [...positions];
    this.inputTaskListReviewVM.positionIds = [...this.selectedPositionIds];
  }

  openFlyInPanelAddReviewers(templateRef: TemplateRef<any>) {
    const portal = new TemplatePortal(templateRef, null);
    this.flyPanelService.open(portal);
  }

  searchUpdate(e:any) {
    const searchValue = e.target.value;
    this.searchText = searchValue;
    const data = this.inputTaskListReviewVM?.generalReviewers.filter(x=>x.name.trim().toLowerCase().includes(this.searchText.trim().toLowerCase()));
    this.dataSource.data = data;
  }

  getQTDUsersfromFlyPanel(qtdUsers: QtdUserVM[]) {
  }
  
  getReviewersfromFlyPanel(reviewers: QtdUserVM[]) {
    var newIds = reviewers.map(x=>x.id);
    this.linkedReviewerIds = Array.from(new Set([...this.linkedReviewerIds, ...newIds]));
    this.dataSource.data = [...this.dataSource.data , ...this.mapQTDUserToGeneralReviewer(reviewers)];
    this.inputTaskListReviewVM.generalReviewers= this.dataSource.data;
  }

  removeReviewer(row: TaskListReview_GeneralReviewer_VM) {
    this.linkedReviewerIds = this.linkedReviewerIds.filter( x=> x != row.qtdUserId);
    this.dataSource.data = this.dataSource.data.filter(x=>x.qtdUserId !=  row.qtdUserId);
    this.inputTaskListReviewVM.generalReviewers= this.dataSource.data;
  }

  mapQTDUserToGeneralReviewer(reviewers : QtdUserVM[]){
   return reviewers.map(x=>{
      var generalReviewer = new TaskListReview_GeneralReviewer_VM();
      generalReviewer.qtdUserId = x.id;
      generalReviewer.name =`${x.person?.firstName} ${x.person?.lastName}`;
      return generalReviewer;
    });
  }

  deleteGeneralReviewerDialog(){
  this.dialog.open(this.deleteGeneralReviewer, {
    width: '600px',
    height: 'auto',
    hasBackdrop: true,
    disableClose: true,
  });
}

resetSearch(){
  setTimeout(() => {
    this.createTaskListReviewForm.get('positionSearchText')?.setValue('');
  }, 1000);
}


  handleKeydown(event: KeyboardEvent) {
    this.positionSelect._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

  removePosition(posId: any) {
    const positions = this.createTaskListReviewForm.get('positions')?.value || [];
    const updated = positions?.filter((p: any) => p != posId);
    this.createTaskListReviewForm.get('positions')?.setValue(updated);
    this.selectedPositionIds = [...updated];
    this.inputTaskListReviewVM.positionIds = [...this.selectedPositionIds];
  }

  getPositionName(posId: any): string {
    const found = this.allPositions?.find(p => p.id == posId);
    return found ? found.positionTitle : '';
  }

  compareById(o1: any, o2: any): boolean {
    return o1 === o2;
  }

  filterMatch(pos: any): boolean {
    const search = this.createTaskListReviewForm.get('positionSearchText')?.value?.toLowerCase() || '';
    return pos.positionTitle.toLowerCase().includes(search);
  }

}
 