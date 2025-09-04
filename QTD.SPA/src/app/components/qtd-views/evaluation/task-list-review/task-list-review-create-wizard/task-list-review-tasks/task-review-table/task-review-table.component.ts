import { SelectionModel } from '@angular/cdk/collections';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { MatLegacySelect as MatSelect, MatLegacySelectChange as MatSelectChange } from '@angular/material/legacy-select';
import { MatSort, Sort } from '@angular/material/sort';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Router } from '@angular/router';
import { TaskReview_ReviewerOption } from '@models/Task_Review/TaskReview_ReviewerOption';
import { TaskReviewOptions } from '@models/Task_Review/TaskReviewoptions';
import { TaskListReview_TaskReview_VM } from '@models/TaskListReview/TaskListReview_TaskReview_VM';
import { LabelReplacementPipe } from 'src/app/_Pipes/label-replacement.pipe';
import { QTDService } from 'src/app/_Services/QTD/qtd.service';
import { ApiTaskReviewService } from 'src/app/_Services/QTD/TaskReview/api.taskReview.service';
import { SweetAlertService } from 'src/app/_Shared/services/sweetalert.service';

@Component({
  selector: 'app-task-review-table',
  templateUrl: './task-review-table.component.html',
  styleUrls: ['./task-review-table.component.scss']
})
export class TaskReviewTableComponent implements OnInit {
  @Input() inputTaskReviewVMs : TaskListReview_TaskReview_VM[] ;
  @Input() inputTaskListReviewId :string;
  @Output () updateTableData = new EventEmitter<TaskListReview_TaskReview_VM[]>();
  taskReviewDisplayColumn: string[];
  taskListReviewId:string;
  isComingFromWizard:boolean = false;
  searchText:string;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('deleteTaskReview') deleteTaskReview: any;
  @ViewChild('unlinkModal') unlinkModal: any;
  deletedTaskReviewId:string;
  filteredTaskReviewVMs: TaskListReview_TaskReview_VM[] = [];
  selection = new SelectionModel<any>(true, []);
  unlinkIds: any[] = [];
  taskReviewOptions : TaskReviewOptions = new TaskReviewOptions();
  reviewerData: any[];
  selectedReviewers: any[] = [];
  @ViewChild('selectControl', { static: false }) selectControl!: MatSelect;
  selectedTaskReviewers: Map<string, any[]> = new Map();
  taskReviewForms: Map<number, UntypedFormGroup> = new Map();
  originalReviewerData:any[];
  constructor(private _router: Router,
    private taskReviewService : ApiTaskReviewService,
    private alert : SweetAlertService,
    private labelPipe:LabelReplacementPipe,
    private dialog:MatDialog,    
    private fb: UntypedFormBuilder) { }


  ngOnInit(): void {
    this.searchText = '';
    let urlSegment = this._router.url;
    this.isComingFromWizard = !urlSegment.includes("overview");
    this.taskReviewDisplayColumn = this.isComingFromWizard  ? ['id', 'number', 'statement', 'recentHistoryDate', 'assignedReviewer', 'reviewDate', 'status', 'findings', 'action'] : ['number', 'statement', 'recentHistoryDate', 'assignedReviewer', 'reviewDate', 'status', 'findings', 'action'];
    this.filteredTaskReviewVMs = [...this.inputTaskReviewVMs];
    this.initializeReviewForms(this.filteredTaskReviewVMs);
    this.getReviewersData();
  } 

 ngOnChanges() {
  this.filteredTaskReviewVMs = [...this.inputTaskReviewVMs];
 }

  createTaskReviewForm(row: any): UntypedFormGroup {
  const initialReviewers = Array.isArray(row.reviewers) ? this.mapReviewersToReviewerData(row.reviewers) : [];
  return this.fb.group({
    reviewedBy: [initialReviewers],
    searchReviewerTxt: ['']
  });
 }

 initializeReviewForms(data: any[]) {
  data.forEach(row => {
    const form = this.createTaskReviewForm(row);
    this.taskReviewForms.set(row.id, form);
    const reviewers = form.get('reviewedBy')?.value ?? [];
    this.selectedTaskReviewers.set(row.id, reviewers);
  });
 }

  getTaskReviewDataSource(){
    return new MatTableDataSource(this.filteredTaskReviewVMs);
  }

  openTaskReviewEditWizard(id:string) {
    this._router.navigate([`/evaluation/task-list-review/${this.inputTaskListReviewId}/edit/taskReview/${id}`],{ queryParams: { isComingFromWizard: this.isComingFromWizard} });
  }

  async deleteTaskReviewAsync(id:string) {
    await this.taskReviewService.deleteAsync(id).then(async res=>{
      this.inputTaskReviewVMs =this.inputTaskReviewVMs.filter(x=>x.id != id);
      this.updateTableData.emit(this.inputTaskReviewVMs);
      this.filteredTaskReviewVMs = this.inputTaskReviewVMs;
      this.alert.successToast(await this.labelPipe.transform('Task') + " Review deleted successfully")
    })
  }

  searchUpdate(event: any) {
    const searchText = event.target.value.toLowerCase();
    this.searchText = searchText;

    this.filteredTaskReviewVMs = this.inputTaskReviewVMs.filter(task => {
    const number = task.number?.toLowerCase() || '';
    const statement = task.statement?.toLowerCase() || '';
    return number.includes(searchText) || statement.includes(searchText);
   });

  }

  sortTaskReviewData(sort: Sort) {
    this.getTaskReviewDataSource().sort = this.sort;
    const data = this.getTaskReviewDataSource().data;
    if (!sort.active || sort.direction === '') {
      this.getTaskReviewDataSource().data = data;
      return;
    }

    this.getTaskReviewDataSource().data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'number':
          return this.compare(a.number, b.number, isAsc);
        case 'statement':
          return this.compare(a.statement, b.statement, isAsc);
        case 'recentHistoryDate':
          return this.compare(a.recentHistoryDate, b.recentHistoryDate, isAsc);
        default:
          return 0;
      }
    });
  }

  compare(a: number | string |Date , b: number | string | Date, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  deleteTaskReviewDialog(){
    this.dialog.open(this.deleteTaskReview, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
  }

  unlinkItemsModal(){
   this.dialog.open(this.unlinkModal, {
      width: '600px',
      height: 'auto',
      hasBackdrop: true,
      disableClose: true,
    });
   }

   selectrow(row: any) {
    this.selection.toggle(row);
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.filteredTaskReviewVMs.length;
    return numSelected === numRows;
  }

  masterToggle() {
    this.isAllSelected() ? this.selection.clear(): this.filteredTaskReviewVMs.forEach((row) => this.selection.select(row));
    this.unlinkIds = [];
    this.selection.selected.forEach((v, i) => {
      this.unlinkIds.push(v.id);
    });
  }

  async UnlinkTaskReviewsAsync(){
   this.taskReviewOptions.taskReviewIds=this.unlinkIds;
    await this.taskReviewService.unlinkTaskReviewsAsync(this.taskReviewOptions).then(async res=>{
    this.inputTaskReviewVMs = this.inputTaskReviewVMs.filter(x => !this.unlinkIds.includes(x.id));
    this.updateTableData.emit(this.inputTaskReviewVMs);
    this.filteredTaskReviewVMs = this.inputTaskReviewVMs;
    this.unlinkIds = [];
    this.selection.clear();
    this.alert.successToast(await this.labelPipe.transform('Task') + " Review Unlink successfully")
    })
  }

   async getReviewersData() {
    this.selection.clear();
    this.reviewerData = this.inputTaskReviewVMs.map(vm => vm.reviewers || []).reduce((acc, curr) => acc.concat(curr), []).filter((r, index, self) => r && self.findIndex(x => x.id === r.id) === index);
    this.originalReviewerData = Object.assign(this.reviewerData);
  }

  removeReviewer(reviewer: any, row: any) {
  const currentReviewers = this.selectedTaskReviewers.get(row.id) || [];
  const updatedReviewers = currentReviewers.filter(r => r.id !== reviewer.id);
  this.selectedTaskReviewers.set(row.id, updatedReviewers);
  const form = this.taskReviewForms.get(row.id);
  form?.get('reviewedBy')?.setValue(updatedReviewers);
  this.taskReviewService.deleteTaskReviewReviewerAsync(row.id, reviewer.id);
 }

 async onReviewedByClick(id: any, row: any) {
  const currentReviewers = this.selectedTaskReviewers.get(row.id) || [];
  const alreadySelected = currentReviewers.some((r) => r.id == id);
  if (!alreadySelected) {
    const option = new TaskReview_ReviewerOption();
    option.qtdUserId = id;
    await this.taskReviewService.createTaskReviewReviewerAsync(row.id, option);
    const newReviewer = this.reviewerData.find((r) => r.id === id);
    const updatedReviewers = [...currentReviewers, newReviewer];
    this.selectedTaskReviewers.set(row.id, updatedReviewers);
    const form = this.taskReviewForms.get(row.id);
    form?.get('reviewedBy')?.setValue(updatedReviewers);
  }
 }

 getDisplayName(i: any): string {
  const fullName = [i?.person?.firstName, i?.person?.lastName].filter(v => !!v).join(' ');
  return fullName || i?.name || '';
 }

  reviewerSearch(rowId: number) {
  const form = this.taskReviewForms.get(rowId);
  let searchValue = form?.get('searchReviewerTxt')?.value;
  searchValue = searchValue?.toLowerCase() || '';
  this.reviewerData = this.originalReviewerData.filter((x) => x.name?.trim().toLowerCase().includes(searchValue));
 }

  handleKeydown(event: KeyboardEvent) {
    this.selectControl._handleKeydown = (event: KeyboardEvent) => {
      if (event.key === 'SPACE') return;
    };
  }

  resetSearch(rowId: number) {
  const form = this.taskReviewForms.get(rowId);
  setTimeout(() => {
    form?.get('searchReviewerTxt')?.setValue('');
    this.reviewerSearch(rowId);
  }, 500);
 }

  mapReviewersToReviewerData(reviewers: any[]): any[] {
    return reviewers.map(r => ({
      id: r.id,
      person: {
        firstName: r.name?.split(' ')[0] ?? '',
        lastName: r.name?.split(' ')[1] ?? ''
      }
    }));
  }

  compareReviewers = (a: any, b: any): boolean => {
    if (!a || !b) return false;
    return a.id === b.id;
  };
}
